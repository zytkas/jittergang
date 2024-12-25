using SharpDX.DirectInput;
using System.Diagnostics;

namespace JitterGang.Services.Input.Controllers;

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
            Debug.WriteLine($"Failed to initialize DirectInput joystick: {ex.Message}");
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