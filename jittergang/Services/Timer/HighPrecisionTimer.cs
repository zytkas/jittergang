using System.Diagnostics;

namespace JitterGang.Services.Timer;

public class HighPrecisionTimer : ITimer
{
    private readonly Action _callback;
    private long _intervalTicks;
    private readonly Stopwatch _stopwatch;
    private readonly CancellationTokenSource _cts;
    private Task? _timerTask;

    public bool IsRunning => _timerTask != null && !_timerTask.IsCompleted;

    public HighPrecisionTimer(Action callback)
    {
        _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        _stopwatch = new Stopwatch();
        _cts = new CancellationTokenSource();
    }

    public void Start(TimeSpan interval)
    {
        if (_timerTask != null && !_timerTask.IsCompleted)
        {
            throw new InvalidOperationException("Timer is already running.");
        }

        _intervalTicks = (long)(interval.TotalSeconds * Stopwatch.Frequency) / 2;
        _stopwatch.Start();
        _timerTask = Task.Run(TimerLoopAsync, _cts.Token);
    }

    public void Stop()
    {
        _cts.Cancel();
        try
        {
            _timerTask?.Wait();
        }
        catch (AggregateException ae)
        {
            if (!ae.InnerExceptions.All(e => e is TaskCanceledException))
            {
                throw;
            }
        }
        _stopwatch.Stop();
    }

    private async Task TimerLoopAsync()
    {
        long nextTick = _stopwatch.ElapsedTicks;
        long minWaitTicks = Stopwatch.Frequency / 2000;

        while (!_cts.Token.IsCancellationRequested)
        {
            nextTick += _intervalTicks;
            long waitTicks = nextTick - _stopwatch.ElapsedTicks;

            if (waitTicks > minWaitTicks)
            {
                double waitMs = (double)waitTicks / Stopwatch.Frequency * 1000;
                try
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(waitMs), _cts.Token);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }

            if (!_cts.Token.IsCancellationRequested)
            {
                _callback();
            }
        }
    }

    public void Dispose()
    {
        Stop();
        _cts.Dispose();
        GC.SuppressFinalize(this);
    }
}