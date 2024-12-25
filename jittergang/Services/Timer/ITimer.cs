namespace JitterGang.Services.Timer;

public interface ITimer : IDisposable
{
    void Start(TimeSpan interval);
    void Stop();
    bool IsRunning { get; }
}
