using SharpDX.XInput;
using System.Diagnostics;

namespace JitterGang.Services.Input.Controllers;

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