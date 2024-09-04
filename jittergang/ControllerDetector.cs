using SharpDX.DirectInput;
using SharpDX.XInput;
using System.Diagnostics;
using DirectInputDeviceType = SharpDX.DirectInput.DeviceType;

namespace JitterGang
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

            throw new InvalidOperationException("No compatible controller found.");
        }
    }

    public abstract class ControllerHandler : IDisposable
    {
        protected Task pollingTask;
        protected bool isRunning;

        public bool IsRightTriggerPressed { get; protected set; }
        public bool IsLeftTriggerPressed { get; protected set; }

        public abstract void StartPolling();
        public abstract void StopPolling();
        public abstract void Dispose();
    }
    
    public class DirectInputHandler : ControllerHandler
    {
        private readonly DirectInput directInput;
        private Joystick joystick;
        private readonly Guid joystickGuid;
        private const int ReconnectionDelayMs = 1000;

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
                pollingTask = Task.Run(PollingLoopAsync);
            }
        }

        public override void StopPolling()
        {
            isRunning = false;
            pollingTask?.Wait();
        }

        private async Task PollingLoopAsync()
        {
            while (isRunning)
            {
                try
                {
                    if (joystick != null && IsJoystickConnected())
                    {
                        var state = joystick.GetCurrentState();
                        IsRightTriggerPressed = state.Buttons[7];
                        IsLeftTriggerPressed = state.Buttons[6];
                    }
                    else
                    {
                        Debug.WriteLine("DirectInput controller disconnected. Waiting for reconnection...");
                        InitializeJoystick();
                        await Task.Delay(ReconnectionDelayMs);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error polling DirectInput controller: {ex.Message}");
                    joystick = null;
                    await Task.Delay(ReconnectionDelayMs);
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
        private readonly Controller controller;
        private const float TriggerThreshold = 0.5f;
        private const int ReconnectionDelayMs = 1000;

        public XInputHandler(UserIndex userIndex)
        {
            controller = new Controller(userIndex);
            if (!controller.IsConnected)
            {
                Debug.WriteLine($"XInput controller {userIndex} is not connected. Will wait for connection.");
            }
        }

        public override void StartPolling()
        {
            if (!isRunning)
            {
                isRunning = true;
                pollingTask = Task.Run(PollingLoopAsync);
            }
        }

        public override void StopPolling()
        {
            isRunning = false;
            pollingTask?.Wait();
        }

        private async Task PollingLoopAsync()
        {
            while (isRunning)
            {
                try
                {
                    if (controller.IsConnected)
                    {
                        var state = controller.GetState();
                        IsRightTriggerPressed = state.Gamepad.RightTrigger > TriggerThreshold * 255;
                        IsLeftTriggerPressed = state.Gamepad.LeftTrigger > TriggerThreshold * 255;
                    }
                    else
                    {
                        Debug.WriteLine("XInput controller disconnected. Waiting for reconnection...");
                        await Task.Delay(ReconnectionDelayMs);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error polling XInput controller: {ex.Message}");
                    await Task.Delay(ReconnectionDelayMs);
                }
            }
        }

        public override void Dispose()
        {
            StopPolling();
        }
    }
}














