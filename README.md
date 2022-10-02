# MagicRemoteService
Service providing computer remote control using a LG WebOS TV.
Tested with WebOS 6.0 (OLED65C1, OLED48C1) and Windows 10.

- [Source](https://github.com/Cathwyler/MagicRemoteService)
- [Release](https://github.com/Cathwyler/MagicRemoteService/releases)

## Introduction

### How it works
MagicRemoteService is composed of two apps, one for TV sending magic remote inputs and one other for PC reproducing mouse and keyboard inputs. The TV app uses WebOS API and DOM events to catch magic remote inputs, WebSockets (TCP) for main data and Node.js dgram (UDP) for Wake-on-LAN functionality. The PC app uses System.Net.Sockets to receive main data and SendInput Win32 API to reproduce mouse and keyboard inputs.

### About security
There is no encryption data between the TV and the PC. Don't use it if you are unsure of the security of your local network. I strongly recommend to not use it on the internet without a VPN connection. Don't use it to enter password, bank card or any other sensitive information. I clear myself of any responsibility if you got data hacked.

### Possible Improvement
- I already tried Node.js net (TCP) for the main data exchange to get ride of the WebSockets exchange protocol, but using service on TV had really poor performance compared to WebSockets.
- Find a way to detect focus in TextBox control on Windows to automatically pop up the WebOS keyboard.
- Add possibility to choose a PC screen.
- Find a better way to get ride of TV screen saver.

## Installation

- Install WebOS Command line interface on your PC. Please refer to <https://webostv.developer.lge.com/develop/tools/cli-installation>.
- Install and activate developer mode app on your LG WebOS TV. Please refer to <https://webostv.developer.lge.com/develop/getting-started/developer-mode-app>.
- Open MagicRemoteService on PC.
- Configure PC and save.
- Select a TV then configure and install it.
- Others
  - (Optionnal) Setup Wake-on-LAN on your motherboard's PC.
  - (Optionnal) Setup Windows auto logon. Please refer to <https://docs.microsoft.com/en-us/troubleshoot/windows-server/user-profiles-and-logon/turn-on-automatic-logon>.
  - (Optionnal) Setup a scheduled task to keep developer mode activated with ares command on your personal server. Please refer to <https://webostv.developer.lge.com/develop/tools/cli-introduction#ares-extend-dev>.

## Using MagicRemoteService
MagicRemoteService need to run PC and TV app. TV and PC need properly network and video input wired as you configured in installation step.

Magic remote inputs :
- The red key shuts down or starts up your PC.
- The yellow key is actually unused.
- The green key opens Windows menu.
- The blue key pops up the WebOS keyboard.
- The return key sends an escape key to the PC.
- The navigation keys sends arrows keys to the PC.
- A short middle click sends a left click to the PC.
- A long middle click sends a right click to the PC.
- A wheel scroll is sent to the PC.

To prevent screen saver on the TV, the app switches automatically on the video input after 2 minutes of inactivity. If the cursor reappears the app switches back foreground. While the video input is foreground all magic remote inputs is not caught. You need to shake the magic remote or use the scroll before getting magic remote inputs working if the app is in his inactivity phase.

If you are stuck at startup because Wake-on-LAN didn't work, you can do a long press on the return button to relaunch the app or starts up PC manually.

Some debugs logs notifications can appear at the bottom of the screen. Short click on it to hide.

I strongly recommend adding a Windows automatic screen shutdown to prevent pixel remaining with OLED TV.
