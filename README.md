# MagicRemoteService</br>
Service providing computer remote control using an LG smart TV</br>
Tested with WebOS 6.0 and Windows 10.</br>

### About security</br>
There no encription data betwen the TV and the PC. Don't use it if you are unsure of the security of your local network. I strongly recommend to not use it on the internet without a VPN connection. D'ont use it to enter password, bank card or any sensitive data. I clear myself of any responsibility if you got data hacked.

### Before begin you need :</br>
- VisualStudio or MSBuild. Please refere to https://visualstudio.microsoft.com/fr/</br>
- WebOS Command line interface. Please refere to https://webostv.developer.lge.com/sdk/command-line-interface/installation/</br>
- LG TV with developper mode. Please refere to https://webostv.developer.lge.com/develop/app-test/using-devmode-app/</br>
  
There is no configuration file actually so you need to configure and compile it manually.  

## Step 1 : Configure TV App /TV/MagicRemoteService/main.js line 126 to 132</br>
localStorage.sRightClick = "1500"; //in ms, used for timing richt click</br>
localStorage.sInactivity = "120000"; //in second, used if your TV is set to shutdown after 2 or 4 or 6 hour</br>
localStorage.sInputSource = "0"; //0 : HMDI1, 1 : HDMI2, 2 : HDMI3, 3 : HDMI4, 4 : COMP1, 5 : AV1, 6 : AV2</br>
localStorage.sIP = "XXX.XXX.XXX.XXX"; //your computer IP adresse, ex 192.168.1.12</br>
localStorage.sMask = "255.255.255.0"; //your local network mask, ex 255.255.255.0, used for Wake-on-LAN</br>
localStorage.sMac = "XX:XX:XX:XX:XX:XX"; //your mac adresse, used for Wake-on-LAN</br>
localStorage.sPort = "XXXXX"; //ex 41230</br>

You can compile with /TV/MagicRemoteService/build.bat</br>

## Step 2 : Configure PC App /PC/MagicRemoteService/MagicRemoteService.cs line 106</br>
private static readonly System.Net.IPEndPoint ipepIPEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, XXXXX); //Set same as localStorage.sPort</br>

You can compile with /PC/MagicRemoteService/build.bat if your visual studio version is compatible</br>

## Step 3 : Install on your webOS TV IPK /TV/com.cathwyler.magicremoteservice_0.0.1_all.ipk</br>
Please refer to https://webostv.developer.lge.com/sdk/command-line-interface/Guide/testing-web-app-cli/</br>

## Step 4 : Others
Setup your firewall to grant entering access TCP from your TV to your PC with the port you set earlier.
(Optionnal) Setup Wake-on-LAN on your motherboard's PC
(Optionnal) Setup auto window auto logon. Please refer to https://docs.microsoft.com/en-us/troubleshoot/windows-server/user-profiles-and-logon/turn-on-automatic-logon
(Optionnal) 

