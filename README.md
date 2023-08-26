<img src="https://github.com/Swifter1243/Enlighten/assets/61858676/2e37e897-cf93-410c-af10-f96aa203a655" width="200">

# Welcome to Enlighten!

Enlighten is a [Chromapper](https://github.com/Caeden117/ChroMapper) plugin focused on generating advanced lighting patterns.

Ever wanted to represent the vibration of strings or the receding abrasion of a sound? Or maybe you want to simply dim a section that's too bright. It's all possible in Enlighten along with extensive control and quality of life features!

Grab the latest version [here](https://github.com/Swifter1243/Enlighten/releases/latest) and throw it into ChroMapper plugin's folder.

# How to Use

Press `Tab` and click on the Enlighten icon on this sidebar to open the Enlighten panel.

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/f8960556-4bf6-4225-96d1-a72764c6da72)
![image](https://github.com/Swifter1243/Enlighten/assets/61858676/bb8e0043-c3ca-435b-baf4-095358ef26c2)

Here you will find an assortment of options to apply to light events. How Enlighten works is that it effects all events in your ChroMapper selection when you press the green `Run` button in the top right.

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/7d3aa8e0-9361-452e-8cc7-d862a11d3b08)

You also have the option to open `Gradient` mode, which will allow you to shift the parameters of the options through the start and end of the selection.

The "Start" tab is how all of the options will behave at the start of the selection, and the "End" tab is how all of the options will behave at the end of the selection. You also have the option to select an [easing](https://easings.net/) to change how quickly the options shift.

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/ccc543f7-a154-4360-94ae-1c843007485f)

Click on one of the options on the sidebar to enable it. You can either drag the slider or manually enter a number in the input field to change a parameter's value.

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/3cc064f5-7a7a-43c7-ad35-f18941aebc9b)

You're also able to `Disable` the options with the blue eye, and given that you've adjusted it from default values, you can re-enable it later and keep the same values.

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/77d5c5c6-e247-4895-b680-0f5d5308cb21)

You can also `Delete` the option and it's values with the red garbage can.
If you'd like to reset to default values, you can click the circular gray arrow.

Those 2 options have also exist in the top left of the Enlighten panel to apply to every option.

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/365a39ad-a322-43af-99c5-637b45a6d837)

`Disabling`, `Enabling`, and `Deleting` can also be applied to both ends of a gradient if you shift click it as well.

Additionally, in gradient mode you can send the same values to the other end of the gradient if you want that particular option to remain static. This option is also available to be applied to every option in the top right.

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/916c7b6e-9358-427f-aa3d-f5f276717812)
![image](https://github.com/Swifter1243/Enlighten/assets/61858676/4a960579-4f3c-4f2d-9eac-18daeb3c0de7)

You also have the option to swap the start and end options of the gradient with this pink button in the top right.

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/5638328d-5bc3-4f88-9cb1-8e6ebaddda5c)

To the right of that button is a toggle to process each event group individually.

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/562e0dc5-5956-440f-b4c6-f061876fb006)

What this means is that each group (light lane) will have it's own starting point in the gradient, instead of every selected event being considered one big group.

Here it is without the option enabled, and then with it on.

<img src="https://github.com/Swifter1243/Enlighten/assets/61858676/d1a66b74-8703-4cd0-9992-0d16c5341769" height="400">
<img src="https://github.com/Swifter1243/Enlighten/assets/61858676/2e0b6557-167f-47eb-8af8-db4199e8be42" height="400">

Now let's go through each option and what it does!

### Brightness

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/4775ec3b-b2a8-4e0c-9b54-012c64cb4ec7)

Simply multiplies the brightness (RGB) of every light by `Multiplier`

### Alpha

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/5682fe31-34b8-49ba-b4a9-47721fdf65b1)

Also really simple, multiplies the alpha of every light by `Multiplier`

### Hue

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/1c8db6e4-0b4a-4027-86e9-2c65cab629d8)

Shifts the hue of every event by `Offset`. Can be extended past 1 in either direction to create "cycles"

### Saturation

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/3c6efd77-079f-4c90-a2a1-431a25885a39)

Adds the `Offset` value to the saturation of every event.

### Flutter

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/d74b89a6-9505-4c3e-9911-f96bf154457e)

Adds new events halfway in between every selected event. The in-between events' brightness is multiplied by `Multiplier` and `Turbulence` controls the amount of random brightness.

### Pulse

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/95f124d1-90f1-43e6-8472-baafe9fe95a7)

Pulse adds a brightness swell `Cycles` times throughout the selection. `Intensity` controls the brightness of the pulse. 

`Cycles` is not effected by gradient easings as the period of the cycles is already changed throughout the selection, so you already have enough control over the behavior of the pulses.
