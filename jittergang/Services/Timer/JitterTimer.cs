namespace JitterGang.Services.Timer;

public class JitterTimer : ITimer
{
    private readonly IJitterService _jitterService;
    private HighPrecisionTimer? _timer;

    public bool IsRunning => _timer?.IsRunning ?? false;

    public JitterTimer(IJitterService jitterService)
    {
        _jitterService = jitterService ?? throw new ArgumentNullException(nameof(jitterService));
    }

    public void Start(TimeSpan interval)
    {
        Stop();
        _timer = new HighPrecisionTimer(_jitterService.HandleShakeTimerTick); // Изменено здесь - передаем только callback
        _timer.Start(interval);
    }

    public void Stop()
    {
        if (_timer is not null)
        {
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
        }
    }

    public void Dispose()
    {
        Stop();
        GC.SuppressFinalize(this);
    }
}