<img src="https://github.com/Swifter1243/Enlighten/assets/61858676/b882b8c0-4a5a-4014-aa0f-e8dae30c1739" width="200">

# Welcome to Enlighten!

Enlighten is a [Chromapper](https://github.com/Caeden117/ChroMapper) plugin focused on generating advanced lighting patterns.

Ever wanted to represent the vibration of strings or the receding abrasion of a sound? Or maybe you want to simply dim a section that's too bright. It's all possible in Enlighten along with extensive control and quality of life features!

Grab the latest version [here](https://github.com/Swifter1243/Enlighten/releases/latest) and throw it into ChroMapper plugin's folder.

# How to Use

Press `Tab` and click on the Enlighten icon on this sidebar to open the Enlighten panel.

![image](https://github.com/Swifter1243/Enlighten/assets/61858676/39a976f6-62df-4709-bb1f-46a357e1a1f3)
![image](https://github.com/Swifter1243/Enlighten/assets/61858676/092b5970-c629-4d75-bc6f-12f208a4d648)

Here you will find an assortment of options to apply to light events. How Enlighten works is that it effects all events in your ChroMapper selection when you press the green `Run` button in the top right.

![3](https://github.com/Swifter1243/Enlighten/assets/61858676/e8ba5538-a63a-4a43-abc0-fd9a3fc69863)

You also have the option to open `Gradient` mode, which will allow you to shift the parameters of the options through the start and end of the selection.

The "Start" tab is how all of the options will behave at the start of the selection, and the "End" tab is how all of the options will behave at the end of the selection. You also have the option to select an [easing](https://easings.net/) to change how quickly the options shift.

![4](https://github.com/Swifter1243/Enlighten/assets/61858676/240e92e6-6b23-4c53-8e2e-03c93f20fbf5)

Click on one of the options on the sidebar to enable it. You can either drag the slider or manually enter a number in the input field to change a parameter's value.

![5](https://github.com/Swifter1243/Enlighten/assets/61858676/f427c4bc-5389-4d3a-909d-8dd8b9595d10)

You're also able to `Disable` the options with the blue eye, and given that you've adjusted it from default values, you can re-enable it later and keep the same values.

![6](https://github.com/Swifter1243/Enlighten/assets/61858676/5bacbf80-fb70-405d-9607-85c9b6ed63b7)

You can also `Delete` the option and it's values with the red garbage can.
If you'd like to reset to default values, you can click the circular gray arrow.

Those 2 options have also exist in the top left of the Enlighten panel to apply to every option.

![7](https://github.com/Swifter1243/Enlighten/assets/61858676/fd24ce0d-8334-4698-aba1-3eb1fddeaa0d)

`Disabling`, `Enabling`, and `Deleting` can also be applied to both ends of a gradient if you shift click it as well.

Additionally, in gradient mode you can send the same values to the other end of the gradient if you want that particular option to remain static. This option is also available to be applied to every option in the top right.

![8](https://github.com/Swifter1243/Enlighten/assets/61858676/3765aced-5bcd-45fe-b4ab-3761348323fe)
![9](https://github.com/Swifter1243/Enlighten/assets/61858676/4e00828a-ecd5-498f-a39b-1d856a6389b7)

You also have the option to swap the start and end options of the gradient with this pink button in the top right.

![10](https://github.com/Swifter1243/Enlighten/assets/61858676/3cc00188-01f7-46ea-96b6-ca87b1632717)

To the right of that button is a toggle to process each event group individually.

![11](https://github.com/Swifter1243/Enlighten/assets/61858676/37767912-e302-49ba-8f8b-052cc18c2be9)

What this means is that each group (light lane) will have it's own starting point in the gradient, instead of every selected event being considered one big group.

Here it is without the option enabled, and then with it on.

<img src="https://github.com/Swifter1243/Enlighten/assets/61858676/147a2000-136c-4712-bbab-1e5ef33a66e3" height="400">
<img src="https://github.com/Swifter1243/Enlighten/assets/61858676/1b2e0234-8e38-486d-a049-4907c4e22e9d" height="400">

Now let's go through each option and what it does!

### Brightness

![14](https://github.com/Swifter1243/Enlighten/assets/61858676/79122f85-b1fd-4ef0-98cf-50bb32a22cfc)

Simply multiplies the brightness (RGB) of every light by `Multiplier`

### Alpha

![15](https://github.com/Swifter1243/Enlighten/assets/61858676/8d4adfda-a476-4025-86df-856bade1069d)

Also really simple, multiplies the alpha of every light by `Multiplier`

### Hue

![16](https://github.com/Swifter1243/Enlighten/assets/61858676/839f03b6-31ea-4639-95f3-0e1f08ca395c)

Shifts the hue of every event by `Offset`. Can be extended past 1 in either direction to create "cycles"

### Saturation

![17](https://github.com/Swifter1243/Enlighten/assets/61858676/cdab5ada-d265-4fb7-a2b1-6210f700142b)

Adds the `Offset` value to the saturation of every event.

### Flutter

![18](https://github.com/Swifter1243/Enlighten/assets/61858676/56a299cc-f723-4275-8cc8-dc434ddac88a)

Adds new events halfway in between every selected event. The in-between events' brightness is multiplied by `Multiplier` and `Turbulence` controls the amount of random brightness.

### Pulse

![19](https://github.com/Swifter1243/Enlighten/assets/61858676/63c01d5b-2815-4cb0-993f-6525e32f81ae)

Pulse adds a brightness swell `Cycles` times throughout the selection. `Intensity` controls the brightness of the pulse. 

`Cycles` is not effected by gradient easings as the period of the cycles is already changed throughout the selection, so you already have enough control over the behavior of the pulses.
