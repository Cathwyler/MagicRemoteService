# MagicRemoteService</br>
Service providing computer remote control using a LG WebOS TV.</br>
Tested with WebOS 6.0 and Windows 10.</br>

## Introduction

### About security</br>
There is no encryption data between the TV and the PC. Don't use it if you are unsure of the security of your local network. I strongly recommend to not use it on the internet without a VPN connection. Don't use it to enter password, bank card or any other sensitive information. I clear myself of any responsibility if you got data hacked.

### Before beginning</br>
There is no configuration file actually so you need to configure and compile it manually. Tools needed :</br>
- Visual Studio or MSBuild. Please refer to https://visualstudio.microsoft.com/fr/</br>
- WebOS Command line interface. Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/installation/</br>
- LG WebOS TV with developer mode activated. Please refer to https://webostv.developer.lge.com/develop/app-test/using-devmode-app/</br>

## Installation

### Step 1 : Configure TV app /TV/MagicRemoteService/main.js</br>

line 126 to 132</br>
```js
localStorage.sRightClick = "1500"; //ms, used for timing right click.
localStorage.sInactivity = "120000"; //ms, used to prevent screensaver and automatically switch on the video input.
localStorage.sInputSource = "0"; //0 : HMDI1, 1 : HDMI2, 2 : HDMI3, 3 : HDMI4, 4 : COMP1, 5 : AV1, 6 : AV2.
localStorage.sIP = "XXX.XXX.XXX.XXX"; //computer IP address, ex 192.168.1.12.
localStorage.sMask = "255.255.255.0"; //local network mask, ex 255.255.255.0, for Wake-on-LAN.
localStorage.sMac = "XX:XX:XX:XX:XX:XX"; //mac address, for Wake-on-LAN.
localStorage.sPort = "XXXXX"; //ex 41230.
```

You can compile with /TV/MagicRemoteService/build.bat</br>

### Step 2 : Configure PC app /PC/MagicRemoteService/MagicRemoteService.cs</br>

line 106</br>
```c#
private static readonly System.Net.IPEndPoint ipepIPEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, XXXXX); //same as localStorage.sPort.
```

line 229</br>
```c#
tInactivity.Interval = 7130000; //ms, if your TV is set to shutdown after 2, 4 or 6 hours
```

You can compile with /PC/MagicRemoteService/build.bat if your Visual Studio version is compatible.</br>

### Step 3 : Install the app on your WebOS TV /TV/com.cathwyler.magicremoteservice_0.0.1_all.ipk</br>
Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/Guide/testing-web-app-cli/#installing.</br>

### Step 4 : Others
Configure your firewall to grant TCP entering access from your TV to your PC with the port you set earlier.</br>
(Optionnal) Setup Wake-on-LAN on your motherboard's PC.</br>
(Optionnal) Setup Windows auto logon. Please refer to https://docs.microsoft.com/en-us/troubleshoot/windows-server/user-profiles-and-logon/turn-on-automatic-logon</br>
(Optionnal) Setup a scheduled task at opening session tu run MagicRemoteService.exe on your PC. You can check maximal authorization to grant access to your task manager for example.</br>
(Optionnal) Setup a scheduled task to keep developer mode activated with ares command on your personal server. Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/intro-cli/?wos_flag=ares-extend-dev#ares-extend-dev</br>

## Using MagicRemoteService
The red button used tu shutdown or startup your PC.
The green button used to open Windows menu.
The blue button used to popup the LG WebOS keyboard.
The return button sends an escape key to the PC.
A short middle click buton sends a left click to the PC.
A long middle click buton sends a right click to the PC.

To prevent screen saver on the TV, the app switches automatically on the video input after 2 minutes of inactivity. If the cursor reappears the app switches back foreground. While the video input is foreground all magic remote input is not caught. You need to shake the magic remote or use the scroll before getting input working if the app is in his inactivity phase.

If you are stuck at startup because Wake-on-LAN didn't work, you can do a long press on the return button to relaunch the app.
