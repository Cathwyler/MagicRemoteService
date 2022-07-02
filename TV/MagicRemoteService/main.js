
ArrayBuffer.prototype.toString = function(base) {
	return Array.from(new Uint8Array(this)).map(function(x) {
		return x.toString(base).padStart(2, "0");
	}).join("");
}

function Log() {
	document.getElementById("log-titre").innerText = "Log";
	document.getElementById("log-message").innerText = JSON.stringify(arguments);
	document.getElementById("log").style.display = "block";
	console.log.apply(console, arguments);
}
function LogIfDebug() {
	if(bDebug) {
		Log.apply(this, arguments);
	}
}
function Warn() {
	document.getElementById("log-titre").innerText = "Warn";
	document.getElementById("log-message").innerText = JSON.stringify(arguments);
	document.getElementById("log").style.display = "block";
	console.warn.apply(console, arguments);
}
function Error() {
	document.getElementById("log-titre").innerText = "Error";
	document.getElementById("log-message").innerText = JSON.stringify(arguments);
	document.getElementById("log").style.display = "block";
	console.error.apply(console, arguments);
}

document.getElementById("log").addEventListener("click", function() {
	document.getElementById("log").style.display = "none";
});

function LaunchApp(sAppId) {
	webOSDev.launch({
		id: sAppId,
		onSuccess: function (inResponse) {
			LogIfDebug("Success to launch " + sAppId);
		},
		onFailure: function (inError) {
			Error("Failed to launch " + sAppId + "[", inError.errorText, "]");
		}
	});
}

function AppVisible() {
	return document.visibilityState === 'visible';
}

function AppFocus() {
	return document.hasFocus() === true;
}

function KeyboardVisible() {
	return webOS.keyboard.isShowing() === true;
}

function CursorVisible() {
	return window.PalmSystem.cursor.visibility === true;
}

function Dialogue(sTitre, sMessage, tabButton){
	CancelDialogue();
	document.getElementById("popup-titre").innerText = sTitre;
	document.getElementById("popup-message").innerText = sMessage;
	let dePopupButton = document.getElementById("popup-button");
	dePopupButton.appendChild(document.createElement("span"));
	tabButton.forEach(function(bButton) {
		let deButton = document.createElement("button");
		deButton.innerText = bButton.sName;
		deButton.addEventListener("click", CancelDialogue);
		deButton.addEventListener("click", bButton.fAction);
		dePopupButton.appendChild(deButton);
	});
	document.getElementById("popup").style.display = "block";
}

function CancelDialogue(){
	document.getElementById("popup").style.display = "none";
	let dePopupButton = document.getElementById("popup-button");
	while(dePopupButton.firstChild) {
		dePopupButton.removeChild(dePopupButton.firstChild);
	}
}

const bDebug = false;
const tabInputSource = [{
	sId: "HDMI_1",
	sName: "HDMI 1",
	sSource: "ext://hdmi:1",
	sAppId: "com.webos.app.hdmi1"
},{
	sId: "HDMI_2",
	sName: "HDMI 2",
	sSource: "ext://hdmi:2",
	sAppId: "com.webos.app.hdmi2"
},{
	sId: "HDMI_3",
	sName: "HDMI 3",
	sSource: "ext://hdmi:3",
	sAppId: "com.webos.app.hdmi3"
},{
	sId: "HDMI_4",
	sName: "HDMI 4",
	sSource: "ext://hdmi:4",
	sAppId: "com.webos.app.hdmi4"
},{
	sId: "",
	sName: "Comp 1",
	sSource: "ext://comp:1",
	sAppId: "com.webos.app.comp1"
},{
	sId: "",
	sName: "AV 1",
	sSource: "ext://av:1",
	sAppId: "com.webos.app.av1"
},{
	sId: "",
	sName: "AV 2",
	sSource: "ext://av:2",
	sAppId: "com.webos.app.av2"
}];

localStorage.sRightClick = "1500"; //ms, used for timing right click.
localStorage.sInactivity = "120000"; //ms, used to prevent screensaver and automatically switch on the video input.
localStorage.sInputSource = "0"; //0 : HMDI1, 1 : HDMI2, 2 : HDMI3, 3 : HDMI4, 4 : COMP1, 5 : AV1, 6 : AV2.
localStorage.sIP = "XXX.XXX.XXX.XXX"; //computer IP address, ex 192.168.1.12.
localStorage.sMask = "255.255.255.0"; //local network mask, ex 255.255.255.0, for Wake-on-LAN.
localStorage.sMac = "XX:XX:XX:XX:XX:XX"; //mac address, for Wake-on-LAN.
localStorage.sPort = "XXXXX"; //ex 41230.

const uiRightClick = parseInt(localStorage.sRightClick);
const uiInactivity = parseInt(localStorage.sInactivity);
const uiInputSource = parseInt(localStorage.sInputSource);
const sIP = localStorage.sIP;
const uiPort = parseInt(localStorage.sPort);
const sBroadcast = localStorage.sIP.split(".").map(function(x, i) {
	return(x | (parseInt(localStorage.sMask.split(".")[i], 10) ^ 0xFF)).toString(10);
}).join(".");
const tabMac = localStorage.sMac.split(":").map(function(x) {
    return parseInt(x, 16);
});

var options = {
	mediaTransportType: "URI",
	option: {
		mediaFormat: {
			type: "video"
		},
		transmission: {
			playTime: {
				start: 0
			}
		},
		"3dMode": "2D"
	}
};

var deSource = document.createElement("source");
deSource.setAttribute("src", tabInputSource[uiInputSource].sSource);
deSource.setAttribute("type", "service/webos-external;mediaOption=" + encodeURI(JSON.stringify(options)));

var deVideo = document.getElementById("video");
video.appendChild(deSource);

var iIntervalWakeOnLan = 0;
var iTimeoutSourceStatus = 0;
var tabInputSourceStatus = [
	false,
	false,
	false,
	false,
	false,
	false,
	false
];
webOS.service.request("luna://com.webos.service.eim", {
	method: "getAllInputStatus",
	parameters: {
		subscribe: true
	},
	onSuccess: function(inResponse){
		if(typeof(inResponse.subscribed) === "boolean") {
			LogIfDebug("Start to get all input status");
		} else {
			var bLastInputSourceStatus = tabInputSourceStatus[uiInputSource]
			tabInputSource.forEach(function(isInputSource, ui) {
				if(isInputSource.sId !== "") {
					inResponse.devices.forEach(function(dDevice) {
						if(isInputSource.sId === dDevice.id){
							tabInputSourceStatus[ui] = dDevice.activate;
						}
					});
				}
			});
			if(tabInputSourceStatus[uiInputSource]) {
				if(iIntervalWakeOnLan) {
					clearInterval(iIntervalWakeOnLan);
					iIntervalWakeOnLan = 0;
				}
				if(iTimeoutSourceStatus) {
					clearTimeout(iTimeoutSourceStatus);
					iTimeoutSourceStatus = 0;
				}
				CancelDialogue();
				if(bLastInputSourceStatus !== tabInputSourceStatus[uiInputSource]) {
					if(AppVisible()) {
						Connect();
					}
				}
			} else {
				if(bLastInputSourceStatus !== tabInputSourceStatus[uiInputSource]) {
					if(AppVisible()) {
						Close();
					}
				}
				Dialogue("Magic remote service", "L'ordinateur distant semble être éteint. Voulez-vous le demarrer ?", [
					{
						sName: "Demarrer",
						fAction: function() {
							SendWol({
								tabMac: tabMac
							}, sBroadcast);
							if(iIntervalWakeOnLan) {
								clearInterval(iIntervalWakeOnLan);
								iIntervalWakeOnLan = 0;
							}
							iIntervalWakeOnLan = setInterval(function() {
								SendWol({
									tabMac: tabMac
								}, sBroadcast);
							}, 5000);
							if(iTimeoutSourceStatus) {
								clearTimeout(iTimeoutSourceStatus);
								iTimeoutSourceStatus = 0;
							}
							iTimeoutSourceStatus = setTimeout(function() {
								iTimeoutSourceStatus = 0;
								Dialogue("Magic remote service", "L'ordinateur distant semble être éteint et ne pas réagir au Wake on Lan ou semble être débranché. Veuillez vérifier le bon fonctionnement du Wake on Lan ainsi que de la connexion vidéo", []);
							}, 5000);
						}
					}
				]);
			}
		}
	},
	onFailure: function(inError){
		Warn("Failed to get all input status [", inError.errorText, "]");
	}
});

var iTimeoutRetryConnect = 0;
var iTimeoutConnect = 0;
var socClient = null;
function Connect() {
	if(socClient !== null) {
		Error("Socket encore ouverte");
		socClient.close();
	}
	socClient = new WebSocket("ws://" + sIP + ":" + uiPort);
	socClient.onopen = function(e) {
		if(iTimeoutConnect) {
			clearTimeout(iTimeoutConnect);
			iTimeoutConnect = 0;
		}
		CancelDialogue();
		if(CursorVisible()) {
			if((AppVisible() && AppFocus()) || bInputOpened) {
				SendMouseVisible({
					bV: true
				});
			}
		}
		LogIfDebug("Connexion au serveur");
	};
	socClient.onclose = function(e) {
		iTimeoutRetryConnect = setTimeout(function() {
			iTimeoutRetryConnect = 0;
			Connect();
		}, 1000);
		socClient = null;
		LogIfDebug("Deconnexion du serveur");
	};
	if(iTimeoutConnect) {
	} else {
		iTimeoutConnect = setTimeout(function() {
			iTimeoutConnect = 0;
			Dialogue("Magic remote service", "L'ordinateur distant semble être allumé mais le service n'est pas démarré ou n'est pas joignable. Veuillez vérifier le bon fonctionnement du service sur l'ordinateur distant ainsi que vos paramètres réseaux", []);
		}, 30000);
	}
}
function Close() {
	if(socClient === null) {
		Error("Socket déjà fermée");
		socClient = new WebSocket("ws://" + sIP + ":" + uiPort);
	}
	if(iTimeoutRetryConnect) {
		clearTimeout(iTimeoutRetryConnect);
		iTimeoutRetryConnect = 0;
	}
	if(iTimeoutConnect) {
		clearTimeout(iTimeoutConnect);
		iTimeoutConnect = 0;
	}
	socClient.onclose = function(e) {
		socClient = null;
		LogIfDebug("Deconnexion du serveur");
	};
	if(CursorVisible()) {
		if((AppVisible() && AppFocus()) || bInputOpened) {
			SendMouseVisible({
				bV: false
			});
		}
	}
	socClient.close();
}

function SendWol(mMac, sBroadcast) {
	webOS.service.request("luna://com.cathwyler.magicremoteservice.send", {
		method: "wol",
		parameters: {
			mMac: mMac,
			sBroadcast: sBroadcast
		},
		onSuccess: function(inResponse) {
			console.log("Success to send/wol [0x" + inResponse.sBuffer + "]@" + sBroadcast + ":9", mMac);
		},
		onFailure: function(inError) {
			Error("Failed to send/wol [", inError.eError, "]@" + sBroadcast + ":9", mMac);
		}
	});
}
  
var bufMousePosition = new ArrayBuffer(5);
var dwMousePosition = new DataView(bufMousePosition);
dwMousePosition.setUint8(0, 0x00);
function SendMousePosition(pPosition) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwMousePosition.setUint16(1, pPosition.usX, true);
			dwMousePosition.setUint16(3, pPosition.usY, true);
			socClient.send(bufMousePosition);
			//LogIfDebug("Success to send/mouse/position [0x" + bufMousePosition.toString(16) + "]@" + sIP + ":" + uiPort, pPosition);
		} catch(eError) {
			Error("Failed to send/mouse/position [", eError, "]@" + sIP + ":" + uiPort, pPosition);
		}
	}
}

var bufMouseKey = new ArrayBuffer(4);
var dwMouseKey = new DataView(bufMouseKey);
dwMouseKey.setUint8(0, 0x01);
function SendMouseKey(kKey) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwMouseKey.setUint16(1, kKey.usC, true);
			dwMouseKey.setUint8(3, kKey.bS);
			socClient.send(bufMouseKey);
			LogIfDebug("Success to send/mouse/key [0x" + bufMouseKey.toString(16) + "]@" + sIP + ":" + uiPort, kKey);
		} catch(eError) {
			Error("Failed to send/mouse/key [", eError, "]@" + sIP + ":" + uiPort, kKey);
		}
	}
}

var bufMouseWheel = new ArrayBuffer(3);
var dwMouseWheel = new DataView(bufMouseWheel);
dwMouseWheel.setUint8(0, 0x02);
function SendMouseWheel(wWheel) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwMouseWheel.setInt16(1, wWheel.sY, true);
			socClient.send(bufMouseWheel);
			LogIfDebug("Success to send/mouse/wheel [0x" + bufMouseWheel.toString(16) + "]@" + sIP + ":" + uiPort, wWheel);
		} catch(eError) {
			Error("Failed to send/mouse/wheel [", eError, "]@" + sIP + ":" + uiPort, wWheel);
		}
	}
}

var bufMouseVisible = new ArrayBuffer(2);
var dwMouseVisible = new DataView(bufMouseVisible);
dwMouseVisible.setUint8(0, 0x03);
function SendMouseVisible(vVisible) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwMouseVisible.setUint8(1, vVisible.bV);
			socClient.send(bufMouseVisible);
			LogIfDebug("Success to send/mouse/Visible [0x" + bufMouseVisible.toString(16) + "]@" + sIP + ":" + uiPort, vVisible);
		} catch(eError) {
			Error("Failed to send/mouse/Visible [", eError, "]@" + sIP + ":" + uiPort, vVisible);
		}
	}
}

var bufKeyboardKey = new ArrayBuffer(4);
var dwKeyboardKey = new DataView(bufKeyboardKey);
dwKeyboardKey.setUint8(0, 0x04);
function SendKeyboardKey(kKey) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwKeyboardKey.setUint16(1, kKey.usC, true);
			dwKeyboardKey.setUint8(3, kKey.bS);
			socClient.send(bufKeyboardKey);
			LogIfDebug("Success to send/keyboard/key [0x" + bufKeyboardKey.toString(16) + "]@" + sIP + ":" + uiPort, kKey);
		} catch(eError) {
			Error("Failed to send/keyboard/key [", eError, "]@" + sIP + ":" + uiPort, kKey);
		}
	}
}

var bufKeyboardUnicode = new ArrayBuffer(3);
var dwKeyboardUnicode = new DataView(bufKeyboardUnicode);
dwKeyboardUnicode.setUint8(0, 0x05);
function SendKeyboardUnicode(kUnicode) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwKeyboardUnicode.setUint16(1, kUnicode.usC, true);
			socClient.send(bufKeyboardUnicode);
			LogIfDebug("Success to send/keyboard/unicode [0x" + bufKeyboardUnicode.toString(16) + "]@" + sIP + ":" + uiPort, kUnicode);
		} catch(eError) {
			Error("Failed to send/keyboard/unicode [", eError, "]@" + sIP + ":" + uiPort, kUnicode);
		}
	}
}

var bufShutdown = new ArrayBuffer(1);
var dwShutdown = new DataView(bufShutdown);
dwShutdown.setUint8(0, 0x06);
function SendShutdown() {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			socClient.send(bufShutdown);
			LogIfDebug("Success to send/shutdown [0x" + bufShutdown.toString(16) + "]@" + sIP + ":" + uiPort);
		} catch(eError) {
			Error("Failed to send/shutdown [", eError, "]@" + sIP + ":" + uiPort);
		}
	}
}

var pMouse = {
	usX: 0,
	usY: 0
};
var pMouseDown = {
	usX: 0,
	usY: 0
};
var bMouseDownSent = false;
var iTimeoutRightClick = 0;
var bInputOpened = false;
var iTimeoutInactivity = 0;

var iIntervalSubscription = setInterval(function() {
	webOS.service.request("luna://com.webos.service.mrcu", {
		method: "sensor/getSensorData",
		parameters: {
			callbackInterval: 1,
			subscribe: true
		},
		onSuccess: function(inResponse) {
			clearInterval(iIntervalSubscription);
			if(typeof(inResponse.subscribed) === "boolean") {
				LogIfDebug("Start to send/mouse/position");
	 		} else {
				if(bInputOpened) {
					LaunchApp(webOS.fetchAppId());
				}
				if((AppVisible() && AppFocus()) || bInputOpened) {
					if(iTimeoutInactivity) {
						clearTimeout(iTimeoutInactivity);
						iTimeoutInactivity = 0;
					}
					iTimeoutInactivity = setTimeout(function() {
						iTimeoutInactivity = 0;
						if((AppVisible() && AppFocus()) || bInputOpened) {
							bInputOpened = true;
							LaunchApp(tabInputSource[uiInputSource].sAppId);
						}
					}, uiInactivity);
					if(iTimeoutRightClick && ((pMouseDown.usX - inResponse.coordinate.x) > 3 || (pMouseDown.usX - inResponse.coordinate.x) < -3 || (pMouseDown.usY - inResponse.coordinate.y) > 3 || (pMouseDown.usY - inResponse.coordinate.y) < -3)) {
						clearTimeout(iTimeoutRightClick);
						iTimeoutRightClick = 0;
						SendMouseKey({
							usC: 0,
							bS: true
						});
						bMouseDownSent = true;
					}
					pMouse.usX = inResponse.coordinate.x;
					pMouse.usY = inResponse.coordinate.y;
					SendMousePosition({
						usX: inResponse.coordinate.x,
						usY: inResponse.coordinate.y
					});
				}
	 		}
			return true;
		},
		onFailure: function(inError) {
			Warn("Failed to get sensor data [", inError.errorText, "]");
			return;
		}
	});
}, 1000);

/*var iIntervalKeepalive = setInterval(function() {
	webOS.service.request("luna://com.cathwyler.magicremoteservice.send", {
		method: "keepalive",
		onSuccess: function(inResponse) {
			LogIfDebug("Success to keep alive");
		},
		onFailure: function(inError) {
			Error("Failed to keep alive [", inError, "]");
		}
	});
}, 1000);*/

/*webOS.service.request("luna://com.webos.service.eim", {
	method: "deleteDevice",
	parameters: {
		appId: "com.cathwyler.magicremoteservice"
	},
	onSuccess: function(inResponse){
		LogIfDebug("Sucess to delete device");
	},
	onFailure: function(inError){
		Error("Failed to delete device [", inError.errorText, "]");
	}
});*/

/*webOS.service.request("luna://com.webos.service.eim", {
	method: "addDevice",
	parameters: {
		appId: "com.cathwyler.magicremoteservice",
		pigImage: "",
		mvpdIcon: "",
		type: "MVPD_IP",
		showPopup: true,
		label: "Magic remote service",
		description: "Service permettant de contrôler un ordinateur à l'aide d'une smart TV LG",
	},
	onSuccess: function(inResponse){
		LogIfDebug("Sucess to add device");
	},
	onFailure: function(inError){
		Error("Failed to add device [", inError.errorText, "]");
	}
});*/

/*document.getElementById("fullscreen").addEventListener("click", function(){
	if (!document.fullscreenElement){
		if(video.requestFullscreen){
			video.requestFullscreen();
		}
	} else {
		if (document.exitFullscreen){
			document.exitFullscreen();
		}
	}
});*/

/*webOS.service.request("luna://com.webos.applicationManager", {
	method: "getForegroundAppInfo",
	parameters: {},
	onSuccess: function(inResponse) {
 		LogIfDebug("Success get foreground app info [", inResponse, "]");
	},
	onFailure: function(inError) {
		Error("Failed to get foreground app info [", inError, "]");
	}
})*/

document.addEventListener("mousedown", function(inEvent) {
	if(iTimeoutRightClick) {
	} else {
		iTimeoutRightClick = setTimeout(function() {
			iTimeoutRightClick = 0;
			SendMouseKey({
				usC: 1,
				bS: true
			});
			SendMouseKey({
				usC: 1,
				bS: false
			});
		}, uiRightClick);
		pMouseDown.usX = pMouse.usX
		pMouseDown.usY = pMouse.usY
	}
});

document.addEventListener("mouseup", function(inEvent) {
	if(iTimeoutRightClick) {
		clearTimeout(iTimeoutRightClick);
		iTimeoutRightClick = 0;
		SendMouseKey({
			usC: 0,
			bS: true
		});
		bMouseDownSent = true;
	}
	if(bMouseDownSent) {
		SendMouseKey({
			usC: 0,
			bS: false
		});
		bMouseDownSent = false;
	}
});

document.addEventListener("wheel", function(inEvent) {
	SendMouseWheel({
		sY: inEvent.deltaY
	});
});

document.addEventListener("keydown", function(inEvent) {
	switch(inEvent.keyCode) {
		case 0x08:
		case 0x0D:
		case 0x25:
		case 0x26:
		case 0x27:
		case 0x28:
			SendKeyboardKey({
				usC: inEvent.keyCode,
				bS: true
			});
			break;
		case 0x194:
			SendKeyboardKey({
				usC: 0x5B,
				bS: true
			});
			break;
		case 0x1CD:
			SendKeyboardKey({
				usC: 0x1B,
				bS: true
			});
			break;
		default:
			break;
	}
});

document.addEventListener("keyup", function(inEvent) {
	switch(inEvent.keyCode) {
		case 0x08:
		case 0x0D:
		case 0x25:
		case 0x26:
		case 0x27:
		case 0x28:
			SendKeyboardKey({
				usC: inEvent.keyCode,
				bS: false
			});
			break;
		case 0x193:
			if(tabInputSourceStatus[uiInputSource]){
				Dialogue("Magic remote service", "Voulez-vous éteindre l'ordinateur distant ?", [
					{
						sName: "Eteindre",
						fAction: function(){
							SendShutdown();
						}
					}, {
						sName: "Ne rien faire",
						fAction: null
					}
				]);
			} else {
				Dialogue("Magic remote service", "Voulez-vous demarrer l'ordinateur distant ?", [
					{
						sName: "Demarrer",
						fAction: function(){
							SendWol({
								tabMac: tabMac
							}, sBroadcast);
						}
					}, {
						sName: "Ne rien faire",
						fAction: null
					}
				]);
			}
			break;
		case 0x194:
			SendKeyboardKey({
				usC: 0x5B,
				bS: false
			});
			break;
		case 0x196:
			if (KeyboardVisible() === true) {
				document.getElementById("keyboard").blur();
			} else {
				document.getElementById("keyboard").focus();
			}
			break;
		case 0x1CD:
			SendKeyboardKey({
				usC: 0x1B,
				bS: false
			});
			break;
		default:
			break;
	}
});

document.getElementById("keyboard").addEventListener('input', function(inEvent) {
	SendKeyboardUnicode({
		usC: inEvent.data.charCodeAt()
	});
});

document.addEventListener("visibilitychange", function() {
	if(bInputOpened) {
		if(AppVisible()) {
			bInputOpened = false;
		}
	} else {
		if(!tabInputSourceStatus[uiInputSource]) {

		} else {
			if(AppVisible()) {
				Connect();
			} else {
				Close();
			}
		}
	}
});

window.addEventListener("focus", function() {
	if(CursorVisible()) {
		SendMouseVisible({
			bV: true
		});
	}
});

window.addEventListener("blur", function() {
	if(CursorVisible()) {
		SendMouseVisible({
			bV: false
		});
	}
});

document.addEventListener("cursorStateChange", function(inEvent) {
	SendMouseVisible({
		bV: CursorVisible()
	});
});
