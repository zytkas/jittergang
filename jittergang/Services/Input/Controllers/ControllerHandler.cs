using jittergang.Services.Input.Controllers;

namespace JitterGang.Services.Input.Controllers;

public abstract class ControllerHandler : IControllerHandler
{
    protected Task pollingTask;
    protected bool isRunning;

    public bool IsRightTriggerPressed { get; protected set; }
    public bool IsLeftTriggerPressed { get; protected set; }

    public abstract void StartPolling();
    public abstract void StopPolling();
    public abstract void Dispose();
}