# Ori DE: Randomizer Tracker

## Description
This is a tracker for Ori DE. This tracker is intended for use with the Ori DE 
Randomizer (https://orirando.com). Currently, it features the ability to auto 
update skill, event, tree, teleporter, and relic pickups. This code is maintained
primarily by Mel Miller (`taragon.meldon at gmail dot com`).

## Contributions
I'm thankful for the following people who have made contributions as well (in 
alphabetical order):

* DevilSquirrel: for incredible work in implementing the base for the memory hook 
which is pulled and modified from the auto splitter DevilSquirrel wrote for 
LiveSplit
* Eiko: current maintainer of the randomizer and the randomizer web server, also
was fundamental in implementing the new auto update system using the new randomizer
bitfields
* Hydra: for the majority of the graphical design of the tracker
* JHobz: for the core code for the web tracker and for helping to keep that working
through all the changes I have made that tried to break it
* SigmaSin: for creating the Randomizer and for vital assistance in implmenting 
the first version of shard and mapstone tracking with auto update
* ViresMajores: for work on graphical design of the tracker

## Prerequisites
* .NET Framework 4.5.2 (https://www.microsoft.com/en-us/download/details.aspx?id=40779)

## Features
The tracker has the ability to update either manually or through a memory hook 
on the Ori and the Blind Forest DE executable running the Randomizer DLL. 
Currently there are some limitations to the manual tracking. 

The following are the only types of pickups which can be tracked manually:

+ Skills (always on)
+ Trees (requires `Track Trees` setting)
+ Events (always on)
+ Shards (requires `Track Shards` setting)
+ Mapsontes (always on)

The following can be tracked with `Auto Update` with the game running:

* Skills (always on)
* Trees (if playing with ForceTrees or if `Track Trees` is on)
* Events (always on)
* Shards (if playing with Shards option)
* Teleporters (if `Track Teleporters` is on)
* Relics (if playing with World Tour)
* Mapstones (always on)

## Settings

### Sizes
There are four available sizes for the tracker display which can be changed
in the Settings menu:

* Small (312 px)
* Medium (437 px)
* Large (667 px)
* XL (750 px)

### Moving
To move the tracker around the screen right click and select `Draggable`. This 
will disable the ability to change any of the items on the tracker although 
if `Auto Update` is enabled, they will still be updated by that. Unselect 
the `Draggable` option to update items on the tracker again.

### Auto Update
To start the auto update mode, right click and select 'Auto Update'. This will 
override any manual changes with the values from the game. Currently, this is 
the only way to track teleporters and relics.

### Refresh Rate
This in the settings menu controls the rate at which the memory hook checks for 
changes and updates the tracker. The default is 10 Hz. The maximum allows is 
60 Hz which will check once per frame in game if something has changed (this is
very excessive and an unecessary drain on your CPU). Slower update settings should
lead to improved performance and should not result in anything being untracked
(at least after the next update cycle)

### Always On Top
Currently the tracker is always the top most window by default. To disable 
this, right click and unselect the 'Always on Top' option.

### Clear and Reset
Clear will remove any items on the tracker. If auto update is on and the game
currently being read from has anything collected which was cleared this will
be rewritten to the tracker on the next update cycle.

### Fonts and Colors
Both the background color and the text color can be changed using the appropriate
dialogs in the 'Settings' menu. The tracker will attempt to load the "Amatic SC" 
font on startup. This is included with the tracker and is easy to install. However, 
if the font is not available, the tracker will present the user with a dialog box 
to select a different font. It is recommended to use the "Amatic SC" font as it 
will result in proper spacing and formatting of the tracker. If you want to change
the font anyways, you can select a new font in 'Settings' menu.

## Help
If you run into any issues you can post them here:
https://github.com/meldontaragon/OriDETracker/issues

There are some known issues with the tracker using a lot of CPU time. I recommend 
setting the affinity to only 1 or 2 CPU cores to help alleviate this negatively 
affecting performance in Ori. Additionally, some people have had problems with the 
auto update feature but have found that running the tracker as Administrator can 
fix this.

Problems can also be reported to myself on Discord (Meldon#1653) through either the
Ori Runs Discord (invite is available at https://orirando.com/) or the Ori 
Randomizer Development Discord if you are a member (if you wish to join that Discord
you can ask in the Ori Runs Discord).

## Related Projects 
* [OriDE Randomizer - Current Fork](https://github.com/sparkle-preference/OriDERandomizer)
* [OriDE Randomizer - OLD](https://github.com/sigmasin/OriDERandomizer)
* [ori_rando_server](https://github.com/sparkle-preference/ori_rando_server)
* [Autosplitter for Ori DE](https://github.com/ShootMe/LiveSplit.OriDE)
* [OriDETAS](https://github.com/ShootMe/OriDETAS)

## License
The website code is licensed under the MIT license and is free for use.
