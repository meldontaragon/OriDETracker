# Ori DE: Randomizer Tracker

## Description
This is a tracker for Ori DE. This tracker is intended for use in
All Trees NMG runs with the Ori DE Randomizer (https://github.com/sigmasin/OriDERandomizer).
Currently, it features the ability to auto update skill, event, and tree pickups.

## Prerequisites
+ .NET Framework 4.5 (https://www.microsoft.com/en-us/download/details.aspx?id=40779)

## Features

### Resizing
To resize the tracker open the settings menu (right click and hit 'Settings') and then change the 
scaling (values from 50% to 400%). Please note the rest of the settings menu is currently not operational.

### Moving
To move the tracker around the screen righ click and select 'Move'. This will disable the ability 
to change any of the itmes on the tracker. Unselect it to update items on the tracker again.

### Auto Update
To start the auto updating mode, right click and select 'Auto Update'. This will override any
manual changes with the values from the game. Currently, if you start auto tracking after
you've started a run, it is impossible to track the trees correctly.

### Always On Top
Currently the tracker is always the top most window by default. To disable this, right click and
unselect the 'Always on Top' option.

## Help
For any questions or issues please post here:
https://github.com/david-c-miller/OriDERandomizerTracker/issues

There are some known issues with the tracker using a lot of CPU time. I recommend setting the
affinity to only 1 or 2 CPU cores to help alleviate this negatively affecting performance in Ori.

## Thanks
Thanks to SigmaSin for creating the Ori DE Randomizer! Additionally, thanks to DevilSquirrel
for help with setting up the auto updating component and for writing the memory related
classes. Also, thanks to ViresMajores for designing all the visual components of the tracker.
