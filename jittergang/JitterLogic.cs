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
        private double currentPosition;
        private const double FrequencyMultiplier = 16;

        public LeftRightJitter(int strength)
        {
            this.strength = strength;
        }

        public override void ApplyJitter(ref INPUT input)
        {
            var oscillation = Math.Sin(currentPosition);
            var jitterAmount = (int)(oscillation * strength);

            input.Mi.Dx += jitterAmount;

            currentPosition += FrequencyMultiplier;

            if (currentPosition >= 2 * Math.PI)
            {
                currentPosition -= 2 * Math.PI;
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

    public class PullDownJitter : BaseJitter
    {
        private int strength;

        public PullDownJitter(int strength)
        {
            this.strength = strength;
        }

        public override void ApplyJitter(ref INPUT input)
        {
            input.Mi.Dy += strength;
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
        private Process selectedProcess;

        private BaseJitter activeJitter;
        private PullDownJitter pullDownJitter;

        public int Strength { get; private set; }
        public int PullDownStrength { get; private set; }
        public string JitterType { get; private set; }
        public bool UseController { get; private set; }

        private ControllerHandler controllerHandler;

        public int ToggleKey
        {
            set => toggleKey = value;
        }

        public Process SelectedProcess
        {
            set => selectedProcess = value ?? throw new ArgumentNullException(nameof(value));
        }

        public JitterLogic()
        {
            Strength = 0;
            PullDownStrength = 0;
            JitterType = "leftright";
            UseController = false;
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
            activeJitter = JitterType.ToLower() switch
            {
                "leftright" => new LeftRightJitter(Strength),
                "circle" => new CircleJitter(Strength),
                _ => throw new ArgumentException("Unknown jitter type", nameof(JitterType)),
            };
            pullDownJitter = new PullDownJitter(PullDownStrength);
        }

        public void SetJitterType(string type)
        {
            if (type.ToLower() is not "leftright" and not "circle")
            {
                throw new ArgumentException("Invalid jitter type. Use 'leftright' or 'circle'.", nameof(type));
            }
            JitterType = type.ToLower();
            UpdateJitters();
        }

        public void StartJitter() => jitterEnabled = true;

        public void StopJitter() => jitterEnabled = false;

        public void HandleShakeTimerTick()
        {
            var isToggleKeyDown = (NativeMethods.GetAsyncKeyState(toggleKey) & 0x8000) != 0;

            if (isToggleKeyDown && !toggleKeyPressed)
            {
                jitterEnabled = !jitterEnabled;
                toggleKeyPressed = true;
            }
            else if (!isToggleKeyDown)
            {
                toggleKeyPressed = false;
            }

            if (!jitterEnabled)
            {
                return;
            }

            bool shouldApplyJitter;
            try
            {
                shouldApplyJitter = UseController && controllerHandler != null
                    ? controllerHandler.IsButtonPressed
                    : (NativeMethods.GetAsyncKeyState(Win32Constants.VK_LBUTTON) & 0x8000) != 0;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error checking controller state", ex);
            }

            if (shouldApplyJitter)
            {
                NativeMethods.GetWindowRect(selectedProcess.MainWindowHandle, out var windowRect);
                NativeMethods.GetCursorPos(out var currentPos);

                if (IsCursorInWindow(currentPos, windowRect))
                {
                    var inputs = new INPUT[1];
                    inputs[0].Type = Win32Constants.INPUT_MOUSE;
                    inputs[0].Mi.DwFlags = Win32Constants.MOUSEEVENTF_MOVE;

                    activeJitter.ApplyJitter(ref inputs[0]);
                    pullDownJitter.ApplyJitter(ref inputs[0]);

                    var result = NativeMethods.SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
                    if (result != 1)
                    {
                        var error = Marshal.GetLastWin32Error();
                        // Handle error if needed
                    }
                }
            }
        }

        private static bool IsCursorInWindow(POINT cursorPos, RECT windowRect)
        {
            return cursorPos.X >= windowRect.Left && cursorPos.X <= windowRect.Right &&
                   cursorPos.Y >= windowRect.Top && cursorPos.Y <= windowRect.Bottom;
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