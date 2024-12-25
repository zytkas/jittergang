using jittergang.Services.Input.Controllers;
using SharpDX.DirectInput;
using SharpDX.XInput;
using DirectInputDeviceType = SharpDX.DirectInput.DeviceType;

namespace JitterGang.Services.Input.Controllers;

public static class ControllerDetector
{
    public static bool IsAnyControllerConnected()
    {
        // Проверяем XInput контроллеры
        for (int i = 0; i < 4; i++)
        {
            var controller = new Controller((UserIndex)i);
            if (controller.IsConnected)
            {
                return true;
            }
        }

        // Проверяем DirectInput контроллеры
        try
        {
            var directInput = new DirectInput();
            var gamepads = directInput.GetDevices(DirectInputDeviceType.Gamepad, DeviceEnumerationFlags.AllDevices);
            var joysticks = directInput.GetDevices(DirectInputDeviceType.Joystick, DeviceEnumerationFlags.AllDevices);

            return gamepads.Count > 0 || joysticks.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    public static IControllerHandler DetectController()
    {
        if (!IsAnyControllerConnected())
        {
            throw new InvalidOperationException("No controller connected. Please connect a controller first.");
        }

        // Проверяем XInput контроллеры
        for (int i = 0; i < 4; i++)
        {
            var controller = new Controller((UserIndex)i);
            if (controller.IsConnected)
            {
                return new XInputHandler((UserIndex)i);
            }
        }

        // Проверяем DirectInput контроллеры
        var directInput = new DirectInput();
        foreach (var deviceInstance in directInput.GetDevices(DirectInputDeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
        {
            return new DirectInputHandler(deviceInstance.InstanceGuid);
        }
        foreach (var deviceInstance in directInput.GetDevices(DirectInputDeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
        {
            return new DirectInputHandler(deviceInstance.InstanceGuid);
        }

        throw new InvalidOperationException("No compatible controller found.");
    }
}