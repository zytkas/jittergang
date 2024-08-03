using jittergang.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace jittergang
{
    public abstract class BaseJitter
    {
        public abstract void ApplyJitter(ref INPUT input);
    }

    public class LeftRightJitter : BaseJitter
    {
        private int strength;
        private double currentPosition = 0;
        private const double FrequencyMultiplier = 16; 
        public LeftRightJitter(int strength)
        {
            this.strength = strength;
        }

        public override void ApplyJitter(ref INPUT input)
        {
            double oscillation = Math.Sin(currentPosition);
            int jitterAmount = (int)(oscillation * strength);

            input.mi.dx += jitterAmount;

            currentPosition += FrequencyMultiplier;

            if (currentPosition >= 2 * Math.PI)
            {
                currentPosition -= 2 * Math.PI;
            }
        }
    }


    public class CircleJitter : BaseJitter
    {
        private int radius;
        private double angle;
        private const double AngleIncrement = Math.PI / 2;

        public CircleJitter(int radius)
        {
            this.radius = radius;
            this.angle = 0;
        }

        public override void ApplyJitter(ref INPUT input)
        {
            input.mi.dx += (int)(radius * Math.Cos(angle));
            input.mi.dy += (int)(radius * Math.Sin(angle));

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
            input.mi.dy += strength;
        }

        public void UpdateStrength(int newStrength)
        {
            strength = newStrength;
        }
    }



    public class JitterLogic
    {
        private bool jitterEnabled = false;
        private int toggleKey;
        private bool toggleKeyPressed = false;
        private Process? selectedProcess;

        private BaseJitter activeJitter;
        private PullDownJitter pullDownJitter;



        public int Strength { get; private set; }
        public int PullDownStrength { get; private set; }


        public string JitterType { get; private set; }

        public bool UseController { get; private set; }

        private ControllerHandler controllerHandler;


        public int ToggleKey
        {
            set { toggleKey = value; }
        }

        public Process SelectedProcess
        {
            set { selectedProcess = value; }
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
                if (pullDownJitter != null)
                {
                    pullDownJitter.UpdateStrength(PullDownStrength);
                }
            }
        }

        public void UpdateJitters()
        {
            switch (JitterType.ToLower())
            {
                case "leftright":
                    activeJitter = new LeftRightJitter(Strength);
                    break;
                case "circle":
                    activeJitter = new CircleJitter(Strength);
                    break;
                default:
                    throw new ArgumentException("Unknown jitter type");
            }
            pullDownJitter = new PullDownJitter(PullDownStrength);
        }

        public void SetJitterType(string type)
        {
            if (type.ToLower() != "leftright" && type.ToLower() != "circle")
            {
                throw new ArgumentException("Invalid jitter type. Use 'leftright' or 'circle'.");
            }
            JitterType = type.ToLower();
            UpdateJitters();
        }

        public void StartJitter()
        {
            jitterEnabled = true;
        }

        public void StopJitter()
        {
            jitterEnabled = false;
        }


        public void HandleShakeTimerTick()
        {


            bool isToggleKeyDown = (NativeMethods.GetAsyncKeyState(toggleKey) & 0x8000) != 0;

            if (isToggleKeyDown && !toggleKeyPressed)
            {
                jitterEnabled = !jitterEnabled;
                toggleKeyPressed = true;
            }
            else if (!isToggleKeyDown)
            {
                toggleKeyPressed = false;
            }


            bool shouldApplyJitter = false;
            try
            {
                shouldApplyJitter = UseController && controllerHandler != null ?
                controllerHandler.IsButtonPressed :
                (NativeMethods.GetAsyncKeyState(Win32Constants.VK_LBUTTON) & 0x8000) != 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking controller state", ex);
            }

            if (shouldApplyJitter)
            {
                RECT windowRect;
                NativeMethods.GetWindowRect(selectedProcess.MainWindowHandle, out windowRect);
                POINT currentPos;
                NativeMethods.GetCursorPos(out currentPos);

                if (IsCursorInWindow(currentPos, windowRect))
                {
                    INPUT[] inputs = new INPUT[1];
                    inputs[0].type = Win32Constants.INPUT_MOUSE;
                    inputs[0].mi.dwFlags = Win32Constants.MOUSEEVENTF_MOVE;

                    activeJitter.ApplyJitter(ref inputs[0]);
                    pullDownJitter.ApplyJitter(ref inputs[0]);


                    uint result = NativeMethods.SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
                    if (result != 1)
                    {
                        int error = Marshal.GetLastWin32Error();
                    }
                }
            }

        }

        private bool IsCursorInWindow(POINT cursorPos, RECT windowRect)
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
                    throw new Exception("Failed to initialize controller", ex);
                }
            }
            else
            {
                if (controllerHandler != null)
                {
                    controllerHandler.StopPolling();
                    controllerHandler.Dispose();
                    controllerHandler = null;
                }
                UseController = false;
            }
        }
    }
}