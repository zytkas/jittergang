using JitterGang.Services.Input.Controllers;
using JitterGang.Services.Jitter;
using JitterGang.Services.Timer;
using JitterGang.libs;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JitterGang.Services;

public class JitterService : IJitterService
{
    private bool _jitterEnabled;
    private int _toggleKey;
    private int _delay;
    private bool _toggleKeyPressed;
    private string? _selectedProcessName;
    private bool _isJitterActivated;
    private readonly JitterTimer? _jitterTimer;

    private LeftRightJitter? _leftRightJitter;
    private SmoothLeftRightJitter? _smoothLeftRightJitter;
    private CircleJitter? _circleJitter;
    private PullDownJitter? _pullDownJitter;
    private ControllerHandler? _controllerHandler;

    public int Strength { get; private set; }
    public int PullDownStrength { get; private set; }
    public bool UseController { get; private set; }
    public bool IsCircleJitterActive { get; set; }
    public bool UseAdsOnly { get; set; }
    public bool IsRunning => _jitterTimer?.IsRunning ?? false;
    public void SetToggleKey(int keyCode) { _toggleKey = keyCode; }

    public JitterService()
    {
        _delay = 1;
        Strength = 0;
        PullDownStrength = 0;
        UseController = false;
        IsCircleJitterActive = false;
        _jitterTimer = new JitterTimer(this);
        UpdateJitters();
    }

    public void Start()
    {
        _jitterEnabled = true;
        if (_jitterTimer != null)
        {
            var interval = TimeSpan.FromMilliseconds(_delay);
            _jitterTimer.Start(interval);
        }
    }

    public void Stop()
    {
        _jitterEnabled = false;
        _jitterTimer?.Stop();
        _isJitterActivated = false;
    }

    public void UpdateStrength(int newStrength)
    {
        if (Strength != newStrength)
        {
            Strength = newStrength;
            UpdateJitters();
        }
    }

    public void UpdatePullDownStrength(int newPullDownStrength)
    {
        if (PullDownStrength != newPullDownStrength)
        {
            PullDownStrength = newPullDownStrength;
            _pullDownJitter?.UpdateStrength(PullDownStrength);
        }
    }

    public void UpdateJitters()
    {
        _leftRightJitter = new LeftRightJitter(Strength);
        _smoothLeftRightJitter = new SmoothLeftRightJitter();
        _circleJitter = new CircleJitter(Strength);
        _pullDownJitter = new PullDownJitter(PullDownStrength);
    }

    public void SetDelay(int delayMs)
    {
        _delay = Math.Max(1, delayMs);
    }

    public void SetSelectedProcess(string processName)
    {
        _selectedProcessName = processName;
    }

    public void SetUseController(bool use)
    {
        try
        {
            if (use)
            {
                if (!ControllerDetector.IsAnyControllerConnected())
                {
                    UseController = false;
                    throw new InvalidOperationException("No controller connected. Please connect a controller and try again.");
                }

                _controllerHandler?.Dispose();
                _controllerHandler = (ControllerHandler)ControllerDetector.DetectController();
                _controllerHandler.StartPolling();
                UseController = true;
            }
            else
            {
                _controllerHandler?.Dispose();
                _controllerHandler = null;
                UseController = false;
            }
        }
        catch (Exception ex)
        {
            // Only reset controller-related state
            UseController = false;
            _controllerHandler?.Dispose();
            _controllerHandler = null;
            throw new InvalidOperationException("Failed to initialize controller", ex);
        }
    }

    public void HandleShakeTimerTick()
    {
        var isToggleKeyDown = (NativeMethods.GetAsyncKeyState(_toggleKey) & 0x8000) != 0;

        if (isToggleKeyDown && !_toggleKeyPressed)
        {
            _isJitterActivated = !_isJitterActivated;
            _toggleKeyPressed = true;
        }
        else if (!isToggleKeyDown)
        {
            _toggleKeyPressed = false;
        }

        if (!_isJitterActivated || !_jitterEnabled)
        {
            return;
        }

        if (!IsTargetProcessActive())
        {
            return;
        }

        bool shouldApplyJitter;
        try
        {
            if (UseController)
            {
                if (_controllerHandler == null)
                {
                    throw new InvalidOperationException("Controller handler is not initialized.");
                }

                if (UseAdsOnly)
                {
                    shouldApplyJitter = _controllerHandler.IsRightTriggerPressed && _controllerHandler.IsLeftTriggerPressed;
                }
                else
                {
                    shouldApplyJitter = _controllerHandler.IsRightTriggerPressed;
                }
            }
            else // Only check mouse input if not using controller
            {
                if (UseAdsOnly)
                {
                    bool isLeftMouseDown = (NativeMethods.GetAsyncKeyState(Win32Constants.VK_LBUTTON) & 0x8000) != 0;
                    bool isRightMouseDown = (NativeMethods.GetAsyncKeyState(Win32Constants.VK_RBUTTON) & 0x8000) != 0;
                    shouldApplyJitter = isLeftMouseDown && isRightMouseDown;
                }
                else
                {
                    shouldApplyJitter = (NativeMethods.GetAsyncKeyState(Win32Constants.VK_LBUTTON) & 0x8000) != 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error checking input state", ex);
        }

        if (shouldApplyJitter)
        {
            ApplyJitter();
        }
    }

    private void ApplyJitter()
    {
        for (int i = 0; i < 15; i++)
        {
            var inputs = new INPUT[1];
            inputs[0].Type = Win32Constants.INPUT_MOUSE;
            inputs[0].Mi.DwFlags = Win32Constants.MOUSEEVENTF_MOVE;

            _leftRightJitter?.ApplyJitter(ref inputs[0]);

            if (UseController)
            {
                _smoothLeftRightJitter?.ApplyJitter(ref inputs[0]);
            }

            if (IsCircleJitterActive)
            {
                _circleJitter?.ApplyJitter(ref inputs[0]);
            }

            _pullDownJitter?.ApplyJitter(ref inputs[0]);

            var result = NativeMethods.SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
            if (result != 1)
            {
                var error = Marshal.GetLastWin32Error();
                Debug.WriteLine($"SendInput failed with error: {error}");
            }
        }
    }

    private bool IsTargetProcessActive()
    {
        if (string.IsNullOrEmpty(_selectedProcessName))
        {
            return false;
        }

        IntPtr foregroundWindow = NativeMethods.GetForegroundWindow();
        int result = NativeMethods.GetWindowThreadProcessId(foregroundWindow, out int foregroundProcessId);

        if (result == 0)
        {
            Debug.WriteLine("Failed to get process ID of the foreground window.");
            return false;
        }

        var processes = Process.GetProcessesByName(_selectedProcessName);
        return processes.Any(p => p.Id == foregroundProcessId);
    }

    public void Dispose()
    {
        _jitterTimer?.Dispose();
        _controllerHandler?.Dispose();
    }
}