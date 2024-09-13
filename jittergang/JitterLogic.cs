using JitterGang.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace JitterGang
{
    public abstract class BaseJitter
    {
        public abstract void ApplyJitter(ref INPUT input);
    }

    public class LeftRightJitter : BaseJitter
    {
        private readonly int strength;
        private int currentDirection = 1;
        private int moveCount = 0;
        private const int MovesPerDirection = 10;
        private const int MicroStrength = 1;

        public LeftRightJitter(int strength)
        {
            this.strength = strength;
        }

        public override void ApplyJitter(ref INPUT input)
        {
            int microMove = Math.Min(MicroStrength, strength);
            input.Mi.Dx += microMove * currentDirection;

            moveCount++;
            if (moveCount >= MovesPerDirection)
            {
                currentDirection *= -1;
                moveCount = 0;
            }
        }
    }

    public class CircleJitter : BaseJitter
    {
        private readonly int radius;
        private double angle;
        private const double AngleIncrement = Math.PI / 2;

        public CircleJitter(int radius)
        {
            this.radius = radius;
        }

        public override void ApplyJitter(ref INPUT input)
        {
            input.Mi.Dx += (int)(radius * Math.Cos(angle));
            input.Mi.Dy += (int)(radius * Math.Sin(angle));

            angle += AngleIncrement;
            if (angle >= 2 * Math.PI)
            {
                angle -= 2 * Math.PI;
            }
        }
    }

    public class SmoothLeftRightJitter : BaseJitter
    {
        private const int Radius = 2;
        private const double AngleIncrement = 0.09;
        private double angle;

        public SmoothLeftRightJitter()
        {
            this.angle = 0;
        }

        public override void ApplyJitter(ref INPUT input)
        {

            double deltaX = Radius * Math.Cos(angle);
            double deltaY = Radius * Math.Sin(angle);

            input.Mi.Dx += (int)Math.Round(deltaX);
            input.Mi.Dy += (int)Math.Round(deltaY);

            angle += AngleIncrement;
            if (angle >= 2 * Math.PI)
            {
                angle -= 2 * Math.PI;
            }
        }
    }

    public class PullDownJitter : BaseJitter
    {
        private int strength;
        private int accumulatedStrength;

        public PullDownJitter(int strength)
        {
            this.strength = strength;
            this.accumulatedStrength = 0;
        }

        public override void ApplyJitter(ref INPUT input)
        {
            float weakenedStrength = strength / 60.0f;

            accumulatedStrength += (int)(weakenedStrength * 100);

            if (accumulatedStrength >= 100)
            {
                int pixelsToMove = accumulatedStrength / 100;
                input.Mi.Dy += pixelsToMove;
                accumulatedStrength %= 100;
            }
        }

        public void UpdateStrength(int newStrength)
        {
            strength = newStrength;
        }
    }

    public class JitterLogic
    {
        private bool jitterEnabled;
        private int toggleKey;
        private bool toggleKeyPressed;
        private string selectedProcessName;
        private bool isJitterActivated = false;

        private LeftRightJitter leftRightJitter;
        private SmoothLeftRightJitter smoothLeftRightJitter;
        private CircleJitter circleJitter;
        private PullDownJitter pullDownJitter;

        public int Strength { get; private set; }
        public int PullDownStrength { get; private set; }
        public bool UseController { get; private set; }
        public bool IsCircleJitterActive { get; set; }
        public bool UseAdsOnly { get; set; }

        private ControllerHandler controllerHandler;

        public int ToggleKey
        {
            set => toggleKey = value;
        }

        public JitterLogic()
        {
            Strength = 0;
            PullDownStrength = 0;
            UseController = false;
            IsCircleJitterActive = false;
            UpdateJitters();
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
                pullDownJitter?.UpdateStrength(PullDownStrength);
            }
        }

        public void UpdateJitters()
        {
            leftRightJitter = new LeftRightJitter(Strength);
            smoothLeftRightJitter = new SmoothLeftRightJitter();
            circleJitter = new CircleJitter(Strength);
            pullDownJitter = new PullDownJitter(PullDownStrength);
        }

        public void StartJitter() => jitterEnabled = true;

        public void StopJitter() => jitterEnabled = false;

        public void HandleShakeTimerTick()
        {
            var isToggleKeyDown = (NativeMethods.GetAsyncKeyState(toggleKey) & 0x8000) != 0;

            if (isToggleKeyDown && !toggleKeyPressed)
            {
                isJitterActivated = !isJitterActivated;
                toggleKeyPressed = true;
            }
            else if (!isToggleKeyDown)
            {
                toggleKeyPressed = false;
            }

            if (!isJitterActivated)
            {
                return;
            }

            if (!IsTargetProcessActive())
            {
                return; // Pause jitter when target process is not active
            }

            bool shouldApplyJitter;
            try
            {
                if (UseController && controllerHandler != null)
                {
                    if (UseAdsOnly)
                    {
                        shouldApplyJitter = controllerHandler.IsRightTriggerPressed && controllerHandler.IsLeftTriggerPressed;
                    }
                    else
                    {
                        shouldApplyJitter = controllerHandler.IsRightTriggerPressed;
                       
                    }
                }
                else
                {
                    shouldApplyJitter = (NativeMethods.GetAsyncKeyState(Win32Constants.VK_LBUTTON) & 0x8000) != 0;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error checking controller state", ex);
            }

            if (shouldApplyJitter)
            {
                for (int i = 0; i < 5; i++)
                {
                    var inputs = new INPUT[1];
                    inputs[0].Type = Win32Constants.INPUT_MOUSE;
                    inputs[0].Mi.DwFlags = Win32Constants.MOUSEEVENTF_MOVE;


                    leftRightJitter.ApplyJitter(ref inputs[0]);

                    if (UseController)
                    {
                        smoothLeftRightJitter.ApplyJitter(ref inputs[0]);
                    }


                    if (IsCircleJitterActive)
                    {
                        circleJitter.ApplyJitter(ref inputs[0]);
                    }


                    pullDownJitter.ApplyJitter(ref inputs[0]);

                    var result = NativeMethods.SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
                    if (result != 1)
                    {
                        var error = Marshal.GetLastWin32Error();
                    }
                }
            }
        }

        public void SetSelectedProcess(string processName)
        {
            selectedProcessName = processName;
        }

        private bool IsTargetProcessActive()
        {
            if (string.IsNullOrEmpty(selectedProcessName))
            {
                return false;
            }

            IntPtr foregroundWindow = NativeMethods.GetForegroundWindow();
            NativeMethods.GetWindowThreadProcessId(foregroundWindow, out int foregroundProcessId);

            var processes = Process.GetProcessesByName(selectedProcessName);
            return processes.Any(p => p.Id == foregroundProcessId);
        }

        public void SetUseController(bool use)
        {
            if (use)
            {
                try
                {
                    controllerHandler = ControllerDetector.DetectController();
                    controllerHandler.StartPolling();
                    UseController = true;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to initialize controller", ex);
                }
            }
            else
            {
                controllerHandler?.Dispose();
                controllerHandler = null;
                UseController = false;
            }
        }
    }
}