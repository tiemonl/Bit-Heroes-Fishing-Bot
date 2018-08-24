# Bit Heroes Fishing Bot

[![GitHub issues](https://img.shields.io/github/issues/tiemonl/Bit-Heroes-Fishing-Bot.svg)](https://github.com/tiemonl/Bit-Heroes-Fishing-Bot/issues)
[![Github All Releases](https://img.shields.io/github/downloads/tiemonl/Bit-Heroes-Fishing-Bot/total.svg)](https://github.com/tiemonl/Bit-Heroes-Fishing-Bot/releases)
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=9F2F5CFVSHK8G)


Table of Contents
=================
* [Introduction](#introduction)
* [How to use](#how-to-use)
	* [Getting started with the program](#getting-started-with-the-program)
	* [Setting up the program](#setting-up-the-program)
	* [Fishing](#fishing)
* [Upcoming feature(s)](#upcoming-features)
* [Feature request or bug report](#feature-request-or-bug-report)
* [Current bug(s)](#current-bugs)


## Introduction

This program allows you to automatically fish for a set amount of bait. Just pick the amount of bait you want to use and press start and let the bot fish for you. This program is not 100%, however, it will always be better than a human. I do not guarantee a 100% catch rate, but I do guarantee a very high catch rate for very little effort.

Pull requests are ***welcomed*** and ***encouraged***!

## How to use
### Getting started with the program
- Download the .exe from the [releases](https://github.com/tiemonl/Bit-Heroes-Fishing-Bot/releases) tab in GitHub.
- Run the file
- you will see this screen:

![Fisher GUI](https://i.imgur.com/19MnQxW.png)

### Setting up the program
- Select Kongregate or Steam depending on which platform you are fishing in
    - If you are using steam, make sure to set the window size to 800x600
- Then select the rod you are using to fish
- Enter the amount of bait you want to go through
- Calibrate the program to know where to fish by pressing the **Find Cast/Catch Location** button on this screen:
![Calibration screen](https://i.imgur.com/8mJ0T4o.png)

### Fishing
- Once you've calibrated the program, you should have a coordinate to the right of the button resembling this: {X=100, Y=300}
    - If you have multiple monitors, make sure Bit Heroes is in the primary monitor. If Bit Heroes is in the second monitor, the program will not work and your coordinates will be {X=0, Y=0}. This indicates it did not calibrate correctly.
- Press Auto mode and let the program fish for you.
    - If you need to cancel Auto Mode for whatever reason, press cancel and the program will finish catching the current fish, before stopping.


## Upcoming feature(s)
- Stop fishing after you catch a certain weight of fish to save bait for another event. i.e. Stop fishing if you catch a fish above 12 kg.
- Minor bug fixes that few people may experience.
	- Screen resolution must be set to 100% zoom.
	- If on web browser, web browser zoom must also be set to 100%.

## Feature request or bug report
To request a feature or report a bug, open up an [issue](https://github.com/tiemonl/Bit-Heroes-Fishing-Bot/issues) in GitHub.

## Current bug(s)
- Application freezing on starting auto mode
	- Current workaround: refer to [issue #6](https://github.com/tiemonl/Bit-Heroes-Fishing-Bot/issues/6#issuecomment-414486435)
- [Issue #2](https://github.com/tiemonl/Bit-Heroes-Fishing-Bot/issues/2) Chrome lags and "de-syncs" program
	- Current workaround: restart the program. 
 	- I need to work on a fix for this, I know the fix for this I just haven't gotten around to implementing and it requires some logic change and would take more time. I promise I'll get around to it. Sorry for the people it affects.
 
