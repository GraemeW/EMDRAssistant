# EMDRAssistant

EMDR Assistant is a Unity project that creates a simple moving bobble for [EMDR therapy](https://en.wikipedia.org/wiki/Eye_movement_desensitization_and_reprocessing). 

Example usage as below

![](./Assets/z_MadeAssets/Examples/EMDRSample.gif)

## Quick Start

Compile the application for the desired platform (verified to work on Mac and Windows) and run it.

If distributing the .app file to other Mac computers, take note that the application will need to be allowed via System Settings -> Privacy & Security -- i.e. since it is compiled without an Apple Developer certificate.  See [here](https://support.apple.com/en-ca/guide/mac-help/mh40616/mac) for more detail.


## Controls

* Escape Key:  to hide/show the settings menu/window
  * Mouse/Keyboard:  to make adjustment on the settings menu/window
* Enter Key:  to start/stop the bobble movement

## Features

### Settings

Cosmetic configurables include:
* Bobble 
  * Shape:  circle, square, triangle
  * Size:  controlled via slider
  * Color:  0-255 R/G/B input
* Background Color:  0-255 R/G/B input
* Window Size:
  * Adjust the resolution of the window by clicking and dragging from the the window corner(s)
  * Click `Fullscreen` to toggle the window to full screen

Motion configurables include:
* Bobble:
  * Speed:  controlled via slider
  * Range:  controlled via slider
    * (i.e. extent the bobble travels in the window)

### Save System

Settings are saved via PlayerPrefs when the Options window is closed or the application is exited.
