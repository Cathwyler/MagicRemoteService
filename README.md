# MagicRemoteService
Service providing computer remote control using an LG smart TV

Before begin you need
  VisualStudio or MSBuild. Please refere to https://visualstudio.microsoft.com/fr/
  WebOS Command line interface. Please refere to https://webostv.developer.lge.com/sdk/command-line-interface/installation/
  LG TV with developper mode
There is no configuration file you need to configure and compile it manually.  

Step 1 : Configure TV App /TV/MagicRemoteService/main.js line 126 to 132
localStorage.sRightClick = "1500"; //in ms, used for timing richt click
localStorage.sInactivity = "120000"; //in second, used if your TV is set to shutdown after 2 or 4 or 6 hour
localStorage.sInputSource = "0"; //0 : HMDI1, 1 : HDMI2, 2 : HDMI3, 3 : HDMI4, 4 : COMP1, 5 : AV1, 6 : AV2
localStorage.sIP = "XXX.XXX.XXX.XXX"; //ex 192.168.1.12
localStorage.sMask = "255.255.255.0"; //ex 255.255.255.0 for local network, used for Wake-on-LAN
localStorage.sMac = "XX:XX:XX:XX:XX:XX"; //ex 255.255.255.0 for local network, used for Wake-on-LAN
localStorage.sPort = "XXXXX"; //ex 41230

Step 2 : Configure 
