# Ori DE: Randomizer Tracker

## Description
This is a tracker for Ori DE. This tracker is intended for use with the Ori DE Randomizer (https://orirando.com). Currently, it features the ability to auto update skill, event, tree, teleporter, and relic pickups.

## Prerequisites
+ .NET Framework 4.5.2 (https://www.microsoft.com/en-us/download/details.aspx?id=40779)

## Features

### Resizing
To resize the tracker open the settings menu (right click and hit 'Settings') and then either change the initial image size. Please note the some layout options in the settings menu are not operational.

### Moving
To move the tracker around the screen right click and select 'Move'. This will disable the ability to change any of the items on the tracker except through the 'Edit' menu. Unselect the 'Move' option to update items on the tracker again.

### Auto Update
To start the auto updating mode, right click and select 'Auto Update'. This will override any manual changes with the values from the game. Currently, if you start auto tracking after you've started a run, you will need to manually update any trees you have already visited.

### Always On Top
Currently the tracker is always the top most window by default. To disable this, right click and unselect the 'Always on Top' option.

### Clear and Reset
Clear will remove any items on the tracker while reset will also change any values to the default values which can be useful for debugging any problems that occur. In general you should only use Reset if something is broken. Otherwise, only use Clear.

### Map Stones
The map stone display of the tracker is enabled by default when using an of the randomizer layouts. Otherwise, it will not be displayed.

### Shards
The shards display can be enabled manually in either the 'Settings' form or the 'Edit' form by selecting the appropriate check boxes. Additionally, if the auto-update feature is enabled and it detects shards being collected, the option will be enabled and the shards will be displayed.

### Edit Menu
The edit menu provides a set of check boxes and buttons for selecting or deselecting skills, trees, events, and shards. It also provides an easy way to both increment and decrement map stones. All options in the edit menu will be altered should the tracker be changed or through the auto update feature.

### Colors
Both the background color and the text color can be changed using the appropriate dialogs in the 'Settings' menu.

### Fonts
The tracker will attempt to load the "Amatic SC" font on startup. This is included with the tracker and is easy to install. However, if the font is not available, the tracker will present the user with a dialog box to select a different font. It is recommended to use the "Amatic SC" font as it will result in proper spacing and formatting of the tracker.

## Assets
By default the tracker comes with four sizes of assets.

## Help
For any questions or issues please post here:
https://github.com/meldontaragon/OriDETracker/issues

There are some known issues with the tracker using a lot of CPU time. I recommend setting the affinity to only 1 or 2 CPU cores to help alleviate this negatively affecting performance in Ori.

Additionally, some people have had problems with the auto update feature but have found that running the tracker as Administrator can fix this.

## Thanks
Thanks to SigmaSin for creating the Ori DE Randomizer and his assistance in getting the shards and map stone auto update features working! Additionally, thanks to DevilSquirrel for help with setting up the auto updating component and for writing the memory related classes. Thanks to ViresMajores and Hydra for their work in designing the visual components of the tracker. Thanks to Eiko for their continued work on the randomizer, the randomizer website, and assistance with coding up parts of this project.

## Related Projects 
* [OriDE Randomizer](https://github.com/sigmasin/OriDERandomizer)
* [ori_rando_server](https://github.com/turntekGodhead/ori_rando_server)
* [Autosplitter for Ori DE](https://github.com/ShootMe/LiveSplit.OriDE)
