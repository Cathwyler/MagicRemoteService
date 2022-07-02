# MagicRemoteService
Service providing computer remote control using a LG WebOS TV.</br>
Tested with WebOS 6.0 (OLED65C1, OLED48C1) and Windows 10.

## Introduction

### How it works
MagicRemoteService is composed of two apps, one for TV sending magic remote inputs and one other for PC reproducing mouse and keyboard inputs. The TV app uses WebOS API and DOM events to catch magic remote inputs, WebSockets (TCP) for main data and Node.js dgram (UDP) for Wake-on-LAN functionality. The PC app uses System.Net.Sockets to receive main data and SendInput Win32 API to reproduce mouse and keyboard inputs.

### About security
There is no encryption data between the TV and the PC. Don't use it if you are unsure of the security of your local network. I strongly recommend to not use it on the internet without a VPN connection. Don't use it to enter password, bank card or any other sensitive information. I clear myself of any responsibility if you got data hacked.

### Before beginning
There is no configuration file actually so you need to configure and compile it manually. Tools needed :
- Visual Studio or MSBuild. Please refer to https://visualstudio.microsoft.com/fr/.
- WebOS Command line interface. Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/installation/.
- LG WebOS TV with developer mode activated. Please refer to https://webostv.developer.lge.com/develop/app-test/using-devmode-app/.

### Possible Improvement
- I already tried Node.js net (TCP) for the main data exchange to get ride of the WebSockets exchange protocol, but using service on TV had really poor performance compared to WebSockets.
- I didn't try to use a dedicated mouse and keyboard driver to reproduce mouse and keyboard inputs.
- I already tried to use system and user service but due to Windows security the mouse and keyboard inputs could not be properly reproduced. A dedicated driver should probably fix it.
- Find a way to detect focus in TextBox control on Windows to automatically pop up the WebOS keyboard.
- Add HMI for configuring TV and PC app.
- Add possibility to install or uninstall MagicRemoteService input and manage multiple remote video inputs.
- Add possibility to choose a PC screen.
- Find a better wey to get ride of TV screen saver.

## Installation

### Step 1 : Configure TV app
https://github.com/Cathwyler/MagicRemoteService/blob/eabd7f017f8bcfc09460ff4765c3eb06676fba27/TV/MagicRemoteService/main.js#L126-L132
You can compile with [/TV/build.bat](/TV/build.bat).</br>
If the compile succeed the ouptut file should be /TV/com.cathwyler.magicremoteservice_0.0.1_all.ipk.

### Step 2 : Configure PC app
https://github.com/Cathwyler/MagicRemoteService/blob/c084818723066ed1926a2081331bdef6307494f8/PC/MagicRemoteService/MagicRemoteService.cs#L106
https://github.com/Cathwyler/MagicRemoteService/blob/c084818723066ed1926a2081331bdef6307494f8/PC/MagicRemoteService/MagicRemoteService.cs#L229
You can compile with [/PC/build.bat](/PC/build.bat) if your Visual Studio version is compatible.

### Step 3 : Install the app on your WebOS TV
Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/Guide/testing-web-app-cli/#installing.

### Step 4 : Others
- Configure your firewall to grant TCP entering access from your TV to your PC with the port you set earlier.
- (Optionnal) Setup Wake-on-LAN on your motherboard's PC.
- (Optionnal) Setup Windows auto logon. Please refer to https://docs.microsoft.com/en-us/troubleshoot/windows-server/user-profiles-and-logon/turn-on-automatic-logon.
- (Optionnal) Setup a scheduled task at opening session tu run MagicRemoteService.exe on your PC. You can check maximal authorization to grant access to your task manager for example.
- (Optionnal) Setup a scheduled task to keep developer mode activated with ares command on your personal server. Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/intro-cli/?wos_flag=ares-extend-dev#ares-extend-dev.
- (Optionnal) Setup MagicRemoteService input with uncommenting these lines :https://github.com/Cathwyler/MagicRemoteService/blob/eabd7f017f8bcfc09460ff4765c3eb06676fba27/TV/MagicRemoteService/main.js#L535-L552 You can delete MagicRemoteService input with uncommenting these lines :https://github.com/Cathwyler/MagicRemoteService/blob/eabd7f017f8bcfc09460ff4765c3eb06676fba27/TV/MagicRemoteService/main.js#L522-L533

## Using MagicRemoteService
MagicRemoteService need to run PC and TV app. TV and PC need properly network and video input wired as you configured in step 1 and 2.

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
