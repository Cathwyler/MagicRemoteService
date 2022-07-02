# MagicRemoteService
Service providing computer remote control using a LG WebOS TV.</br>
Tested with WebOS 6.0 and Windows 10.</br>

## Introduction

### About security
There is no encryption data between the TV and the PC. Don't use it if you are unsure of the security of your local network. I strongly recommend to not use it on the internet without a VPN connection. Don't use it to enter password, bank card or any other sensitive information. I clear myself of any responsibility if you got data hacked.

### Before beginning
There is no configuration file actually so you need to configure and compile it manually. Tools needed :</br>
- Visual Studio or MSBuild. Please refer to https://visualstudio.microsoft.com/fr/</br>
- WebOS Command line interface. Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/installation/</br>
- LG WebOS TV with developer mode activated. Please refer to https://webostv.developer.lge.com/develop/app-test/using-devmode-app/</br>

## Installation

### Step 1 : Configure TV app

https://github.com/Cathwyler/MagicRemoteService/blob/eabd7f017f8bcfc09460ff4765c3eb06676fba27/TV/MagicRemoteService/main.js#L126-L132

You can compile with [/TV/build.bat](/TV/build.bat).</br>
If the compile succeed the ouptut file should be /TV/com.cathwyler.magicremoteservice_0.0.1_all.ipk.</br>

### Step 2 : Configure PC app

https://github.com/Cathwyler/MagicRemoteService/blob/c084818723066ed1926a2081331bdef6307494f8/PC/MagicRemoteService/MagicRemoteService.cs#L106

https://github.com/Cathwyler/MagicRemoteService/blob/c084818723066ed1926a2081331bdef6307494f8/PC/MagicRemoteService/MagicRemoteService.cs#L229


You can compile with [/PC/build.bat](/PC/build.bat) if your Visual Studio version is compatible.</br>

### Step 3 : Install the app on your WebOS TV

Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/Guide/testing-web-app-cli/#installing.</br>

### Step 4 : Others
Configure your firewall to grant TCP entering access from your TV to your PC with the port you set earlier.</br>
(Optionnal) Setup Wake-on-LAN on your motherboard's PC.</br>
(Optionnal) Setup Windows auto logon. Please refer to https://docs.microsoft.com/en-us/troubleshoot/windows-server/user-profiles-and-logon/turn-on-automatic-logon</br>
(Optionnal) Setup a scheduled task at opening session tu run MagicRemoteService.exe on your PC. You can check maximal authorization to grant access to your task manager for example.</br>
(Optionnal) Setup a scheduled task to keep developer mode activated with ares command on your personal server. Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/intro-cli/?wos_flag=ares-extend-dev#ares-extend-dev</br>
(Optionnal) Setup MagicRemoteService input with uncommenting these lines :https://github.com/Cathwyler/MagicRemoteService/blob/eabd7f017f8bcfc09460ff4765c3eb06676fba27/TV/MagicRemoteService/main.js#L535-L552</br>
You can delete MagicRemoteService input with uncommenting these lines :https://github.com/Cathwyler/MagicRemoteService/blob/eabd7f017f8bcfc09460ff4765c3eb06676fba27/TV/MagicRemoteService/main.js#L522-L533</br>

## Using MagicRemoteService
The red button shuts down or starts up your PC.</br>
The green button opens Windows menu.</br>
The blue button pops up the LG WebOS keyboard.</br>
The return button sends an escape key to the PC.</br>
A short middle click buton sends a left click to the PC.</br>
A long middle click buton sends a right click to the PC.</br>

To prevent screen saver on the TV, the app switches automatically on the video input after 2 minutes of inactivity. If the cursor reappears the app switches back foreground. While the video input is foreground all magic remote input is not caught. You need to shake the magic remote or use the scroll before getting input working if the app is in his inactivity phase.</br>

If you are stuck at startup because Wake-on-LAN didn't work, you can do a long press on the return button to relaunch the app or starts up PC manually.</br>

Some debugs logs notifications can appear at the bottom of the screen. Short click on it to hide.</br>
