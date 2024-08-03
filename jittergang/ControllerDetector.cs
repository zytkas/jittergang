using SharpDX.DirectInput;
using SharpDX.XInput;
using DirectInputDeviceType = SharpDX.DirectInput.DeviceType;


namespace jittergang
{
    public static class ControllerDetector
    {
        public static ControllerHandler DetectController()
        {
            for (int i = 0; i < 4; i++)
            {
                var controller = new Controller((UserIndex)i);
                if (controller.IsConnected)
                {
                    return new XInputHandler((UserIndex)i);
                }
            }

            var directInput = new DirectInput();
            foreach (var deviceInstance in directInput.GetDevices(DirectInputDeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                return new DirectInputHandler(deviceInstance.InstanceGuid);
            }
            foreach (var deviceInstance in directInput.GetDevices(DirectInputDeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
            {
                return new DirectInputHandler(deviceInstance.InstanceGuid);
            }

            throw new Exception("No compatible controller found.");
        }
    }

    public abstract class ControllerHandler : IDisposable
    {
        protected Thread? pollingThread;
        protected bool isRunning = false;

        public bool IsButtonPressed { get; protected set; }

        public abstract void StartPolling();
        public abstract void StopPolling();
        public abstract void Dispose();
    }


public class DirectInputHandler : ControllerHandler
    {
        private DirectInput directInput;
        private Joystick joystick;
        private readonly Guid joystickGuid;
        private const int ReconnectionDelay = 1000; // 1 second delay for reconnection attempts

        public DirectInputHandler(Guid joystickGuid)
        {
             
            this.joystickGuid = joystickGuid;
            directInput = new DirectInput();
            InitializeJoystick();
        }

        private void InitializeJoystick()
        {
            try
            {
                joystick = new Joystick(directInput, joystickGuid);
                joystick.Properties.BufferSize = 128;
                joystick.Acquire();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize DirectInput joystick: {ex.Message}");
                joystick = null;
            }
        }

        public override void StartPolling()
        {
            if (!isRunning)
            {
                isRunning = true;
                pollingThread = new Thread(PollingLoop);
                pollingThread.Start();
            }
        }

        public override void StopPolling()
        {
            isRunning = false;
            pollingThread?.Join();
        }

        private void PollingLoop()
        {
            while (isRunning)
            {
                try
                {
                    if (joystick != null && IsJoystickConnected())
                    {
                        var state = joystick.GetCurrentState();
                        IsButtonPressed = state.Buttons[7]; // RT 5, RT BUMPER 7
                    }
                    else
                    {
                        Console.WriteLine("DirectInput controller disconnected. Waiting for reconnection...");
                        InitializeJoystick();
                        Thread.Sleep(ReconnectionDelay); 
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error polling DirectInput controller: {ex.Message}");
                    joystick = null; 
                    Thread.Sleep(ReconnectionDelay); 
                }
            }
        }

        private bool IsJoystickConnected()
        {
            try
            {
                joystick.Poll();
                return true;
            }
            catch (SharpDX.SharpDXException)
            {
                return false;
            }
        }

        public override void Dispose()
        {
            StopPolling();
            joystick?.Unacquire();
            joystick?.Dispose();
            directInput?.Dispose();
        }
    }

    public class XInputHandler : ControllerHandler
    {
        private Controller controller;
        private const float TriggerThreshold = 0.5f;
        private const int ReconnectionDelay = 1000; // 1 second delay for reconnection attempts

        public XInputHandler(UserIndex userIndex)
        {
            controller = new Controller(userIndex);
            if (!controller.IsConnected)
            {
                Console.WriteLine($"XInput controller {userIndex} is not connected. Will wait for connection.");
            }
        }

        public override void StartPolling()
        {
            if (!isRunning)
            {
                isRunning = true;
                pollingThread = new Thread(PollingLoop);
                pollingThread.Start();
            }
        }

        public override void StopPolling()
        {
            isRunning = false;
            pollingThread?.Join();
        }

        private void PollingLoop()
        {
            while (isRunning)
            {
                try
                {
                    if (controller.IsConnected)
                    {
                        var state = controller.GetState();
                        IsButtonPressed = state.Gamepad.RightTrigger > TriggerThreshold * 255;
                    }
                    else
                    {
                        Console.WriteLine("Controller disconnected. Waiting for reconnection...");
                        Thread.Sleep(ReconnectionDelay);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error polling XInput controller: {ex.Message}");
                    Thread.Sleep(ReconnectionDelay); 
                }
            }
        }

        public override void Dispose()
        {
            StopPolling();
        }
    }
}