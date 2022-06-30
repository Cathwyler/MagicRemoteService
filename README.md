# MagicRemoteService</br>
Service providing computer remote control using an LG smart TV</br>
Tested with WebOS 6.0 and Windows 10.</br>

## Introduction

### About security</br>
There no encription data betwen the TV and the PC. Don't use it if you are unsure of the security of your local network. I strongly recommend to not use it on the internet without a VPN connection. Don't use it to enter password, bank card or any other sensitive information. I clear myself of any responsibility if you got data hacked.

### Before begin you need :</br>
- VisualStudio or MSBuild. Please refere to https://visualstudio.microsoft.com/fr/</br>
- WebOS Command line interface. Please refere to https://webostv.developer.lge.com/sdk/command-line-interface/installation/</br>
- LG TV with developper mode. Please refere to https://webostv.developer.lge.com/develop/app-test/using-devmode-app/</br>
  
There is no configuration file actually so you need to configure and compile it manually.  

## Installation

### Step 1 : Configure TV App /TV/MagicRemoteService/main.js line 126 to 132</br>
localStorage.sRightClick = "1500"; //in ms, used for timing richt click.</br>
localStorage.sInactivity = "120000"; //in ms, used to prevent screensaver and automatically switch on the input.</br>
localStorage.sInputSource = "0"; //0 : HMDI1, 1 : HDMI2, 2 : HDMI3, 3 : HDMI4, 4 : COMP1, 5 : AV1, 6 : AV2.</br>
localStorage.sIP = "XXX.XXX.XXX.XXX"; //your computer IP adresse, ex 192.168.1.12.</br>
localStorage.sMask = "255.255.255.0"; //your local network mask, ex 255.255.255.0, used for Wake-on-LAN.</br>
localStorage.sMac = "XX:XX:XX:XX:XX:XX"; //your mac adresse, used for Wake-on-LAN.</br>
localStorage.sPort = "XXXXX"; //ex 41230.</br>

You can compile with /TV/MagicRemoteService/build.bat</br>

### Step 2 : Configure PC App /PC/MagicRemoteService/MagicRemoteService.cs line 106 and 229</br>
private static readonly System.Net.IPEndPoint ipepIPEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, XXXXX); //Set same as localStorage.sPort.</br>
tInactivity.Interval = 7130000; //in ms, used if your TV is set to shutdown after 2 or 4 or 6 hour</br>

You can compile with /PC/MagicRemoteService/build.bat if your visual studio version is compatible.</br>

### Step 3 : Install on your webOS TV IPK /TV/com.cathwyler.magicremoteservice_0.0.1_all.ipk</br>
Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/Guide/testing-web-app-cli/#installing.</br>


### Step 4 : Others
Setup your firewall to grant entering access TCP from your TV to your PC with the port you set earlier.</br>
(Optionnal) Setup Wake-on-LAN on your motherboard's PC</br>
(Optionnal) Setup Windows auto logon. Please refer to https://docs.microsoft.com/en-us/troubleshoot/windows-server/user-profiles-and-logon/turn-on-automatic-logon</br>
(Optionnal) Setup a sheduled task at opening session tu run MagicRemoteService.exe on your PC. You can check maximal authorization to grant access for exemple to your task manager.</br>
(Optionnal) Setup a sheduled task to keep developer mode activated with ares command on your personal server. Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/intro-cli/?wos_flag=ares-extend-dev#ares-extend-dev</br>

## Using MagicRemoteService
The red buton used tu shutdown or startup your PC.
The green buton used to open Windows menu.
The blue buton used to popup the LG WebOS keyboard.
The return buton used to send escape key to the PC.

To prevent screen saver on the tv, the app switch automatically on the video input after 2 minutes of inactivity. If the cursor reapere the app is reswitch foreground. While the video input is foreground all magic remote input is not catch. You need to shake magic remote or use scroll before get input working if the app is in his inactivity phase.

If your are stuck at startup beacause Wake-on-LAN didn't worked, you can do a long press on return buton to relaunch the app.
