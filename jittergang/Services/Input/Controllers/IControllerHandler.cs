namespace jittergang.Services.Input.Controllers
{
    public interface IControllerHandler : IDisposable
    {
        bool IsRightTriggerPressed { get; }
        bool IsLeftTriggerPressed { get; }

        void StartPolling();
        void StopPolling();
    }
}
