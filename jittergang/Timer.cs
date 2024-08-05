using System.Diagnostics;


namespace JitterGang
{
    public class HighPrecisionTimer : IDisposable
    {
        private readonly Action _callback;
        private readonly long _intervalTicks;
        private readonly Stopwatch _stopwatch;
        private readonly CancellationTokenSource _cts;
        private Task _timerTask;

        public HighPrecisionTimer(TimeSpan interval, Action callback)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _intervalTicks = (long)(interval.TotalSeconds * Stopwatch.Frequency);
            _stopwatch = new Stopwatch();
            _cts = new CancellationTokenSource();
        }

        public void Start()
        {
            if (_timerTask != null && !_timerTask.IsCompleted)
            {
                throw new InvalidOperationException("Timer is already running.");
            }

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

            while (!_cts.Token.IsCancellationRequested)
            {
                nextTick += _intervalTicks;
                long waitTicks = nextTick - _stopwatch.ElapsedTicks;

                if (waitTicks > 0)
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
        }
    }

    public class JitterTimer : IDisposable
    {
        private readonly JitterLogic _jitterLogic;
        private HighPrecisionTimer _timer;

        public JitterTimer(JitterLogic jitterLogic)
        {
            _jitterLogic = jitterLogic ?? throw new ArgumentNullException(nameof(jitterLogic));
        }

        public void Start(TimeSpan interval)
        {
            Stop();
            _timer = new HighPrecisionTimer(interval, _jitterLogic.HandleShakeTimerTick);
            _timer.Start();
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }

        public bool IsRunning => _timer != null;

        public void Dispose()
        {
            Stop();
        }
    }
}