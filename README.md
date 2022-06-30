# MagicRemoteService
Service providing computer remote control using an LG smart TV

Before begin you need
  VisualStudio or MSBuild. Please refere to https://visualstudio.microsoft.com/fr/
  WebOS Command line interface. Please refere to https://webostv.developer.lge.com/sdk/command-line-interface/installation/
  LG TV with developper mode. Please refere to https://webostv.developer.lge.com/develop/app-test/using-devmode-app/
  
There is no configuration file you need to configure and compile it manually.  

Step 1 : Configure TV App /TV/MagicRemoteService/main.js line 126 to 132
localStorage.sRightClick = "1500"; //in ms, used for timing richt click
localStorage.sInactivity = "120000"; //in second, used if your TV is set to shutdown after 2 or 4 or 6 hour
localStorage.sInputSource = "0"; //0 : HMDI1, 1 : HDMI2, 2 : HDMI3, 3 : HDMI4, 4 : COMP1, 5 : AV1, 6 : AV2
localStorage.sIP = "XXX.XXX.XXX.XXX"; //your computer IP adresse, ex 192.168.1.12
localStorage.sMask = "255.255.255.0"; //your local network mask, ex 255.255.255.0, used for Wake-on-LAN
localStorage.sMac = "XX:XX:XX:XX:XX:XX"; //your mac adresse, used for Wake-on-LAN
localStorage.sPort = "XXXXX"; //ex 41230

You can compile with /TV/MagicRemoteService/build.bat

Step 2 : Configure PC App /PC/MagicRemoteService/MagicRemoteService.cs line 106
private static readonly System.Net.IPEndPoint ipepIPEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, XXXXX); //Set same as localStorage.sPort

You can compile with /PC/MagicRemoteService/build.bat
