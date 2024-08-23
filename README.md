# JitterGang
<img src="https://github.com/user-attachments/assets/0c6c4d63-4a4a-4271-b500-fcbbb615505a" width="200" alt="coffee-mug">

JitterGang is a customizable mouse jitter tool designed for gaming and other applications where controlled mouse movement is desired. It provides various jitter patterns and controller support.

## Features

- Multiple jitter patterns: Left-Right, Circle, and Pull-Down
- Customizable jitter strength and delay
- Process-specific targeting
- Toggleable on/off hotkey
- Controller support

## Getting Started

### Prerequisites

- Windows 10 or higher 
- .NET Framework 8.0
- DirectX runtime

### Installation

1. Download the latest release from the [releases page](https://github.com/zytkas/jittergang/releases/tag/Release).
2. Extract the zip file to your desired location.
3. Run `JitterGang.exe`.

## Usage

1. Select the target process from the dropdown list.
2. Set the desired jitter strength and delay.
3. Choose a toggle key for enabling/disabling the jitter.
4. Optionally, enable circle jitter or controller support.
5. Click "Start" to begin the jitter effect.
   
### Important Notes on Jitter Activation

- **Mouse**: The jitter effect is only active while the left mouse button is pressed.
- **Controller**: When using a controller, the jitter is only activated when the Right Trigger (RT) is pressed, not other buttons.
- **Controller Jitter**: When the controller is active, the strength parameter does not affect the jitter intensity. *will be fixed*


### Controls

| Control | Description |
|---------|-------------|
| Process | Select the target application |
| Strength | Set the intensity of the jitter effect |
| Delay | Adjust the interval between jitter movements |
| Pulldown | Set the strength of the downward pull effect |
| Turn ON/OFF | Choose a hotkey to toggle the jitter effect |
| CircleJitter | Enable circular jitter pattern |
| Controller | Enable controller support for triggering the jitter effect |

## Configuration

JitterGang automatically saves your settings to `C:\Users\[username]\Documents\JitterGang\settings.json`. This file stores your preferences for easy access in future sessions.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the [MIT License](https://opensource.org/license/mit).

## Disclaimer

Use this tool responsibly and in accordance with the terms of service of the applications you're using it with. The developers are not responsible for any consequences resulting from the use of this tool.

## Acknowledgments

- [SharpDX](https://github.com/sharpdx/SharpDX) for DirectInput and XInput support
- [Flaticon](https://www.flaticon.com/free-icons/skull) for icon
  
---

Created by zytka
