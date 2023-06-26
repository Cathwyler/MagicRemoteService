
ArrayBuffer.prototype.toString = function(base) {
	return Array.from(new Uint8Array(this)).map(function(x) {
		return x.toString(base).padStart(2, "0");
	}).join("");
}

function Toast(sTitre, sMessage){
	CancelToast();
	let deScreen = document.createElement("div");
	deScreen.className = "screen flex justify-center align-flex-end";
	let deLog = document.createElement("div");
	deLog.className = "window";
	deLog.id = "toast"
	deLog.addEventListener("click", CancelToast);
	let dePopupTitre = document.createElement("div");
	dePopupTitre.className = "titre";
	dePopupTitre.innerText = sTitre;
	deLog.appendChild(dePopupTitre);
	if(arguments.length) {
		let dePopupMessage = document.createElement("div");
		dePopupMessage.className = "message";
		dePopupMessage.innerText = sMessage;
		deLog.appendChild(dePopupMessage);
	}
	deScreen.appendChild(deLog);
	document.body.appendChild(deScreen);
}

function CancelToast(){
	let deLog = document.getElementById("toast")
	if(deLog !== null) {
		deLog.parentNode.remove();
	}
}

function Log() {
	Toast(oString.strLogTitle, Object.values(arguments).map(function(x) {
		if(typeof x === "object") {
			return JSON.stringify(x);
		} else {
			return x.toString();
		}
	}).join(""));
	console.log.apply(console, arguments);
}

function LogIfDebug() {
	if(bDebug) {
		Log.apply(this, arguments);
	}
}

function Warn() {
	Toast(oString.strWarnTitle, Object.values(arguments).map(function(x) {
		if(typeof x === "object") {
			return JSON.stringify(x);
		} else {
			return x.toString();
		}
	}).join(""));
	console.warn.apply(console, arguments);
}

function Error() {
	Toast(oString.strErrorTitle, Object.values(arguments).map(function(x) {
		if(typeof x === "object") {
			return JSON.stringify(x);
		} else {
			return x.toString();
		}
	}).join(""));
	console.error.apply(console, arguments);
}

function AppVisible() {
	return document.visibilityState === "visible";
}

function AppFocus() {
	return document.hasFocus() === true;
}

function KeyboardVisible() {
	return window.PalmSystem.isKeyboardVisible === true;
}

function CursorVisible() {
	return window.PalmSystem.cursor.visibility === true;
}

function Dialog(sTitre, sMessage, tabButton){
	CancelDialog();
	let deScreen = document.createElement("div");
	deScreen.className = "screen flex justify-center align-center";
	let deDialog = document.createElement("div");
	deDialog.className = "window";
	deDialog.id = "dialog"
	if(sTitre.length) {
		let dePopupTitre = document.createElement("div");
		dePopupTitre.className = "titre";
		dePopupTitre.innerText = sTitre;
		deDialog.appendChild(dePopupTitre);
	}
	if(sMessage.length) {
		let dePopupMessage = document.createElement("div");
		dePopupMessage.className = "message";
		dePopupMessage.innerText = sMessage;
		deDialog.appendChild(dePopupMessage);
	}
	if(tabButton.length) {
		let dePopupButton = document.createElement("div");
		dePopupButton.className = "button flex justify-flex-end align-center";
		tabButton.forEach(function(bButton) {
			let deButton = document.createElement("button");
			deButton.innerText = bButton.sName;
			deButton.addEventListener("click", CancelDialog);
			deButton.addEventListener("click", bButton.fAction);
			dePopupButton.appendChild(deButton);
		});
		deDialog.appendChild(dePopupButton);
	}
	deScreen.appendChild(deDialog);
	document.body.appendChild(deScreen);
}

function CancelDialog(){
	let deDialog = document.getElementById("dialog")
	if(deDialog !== null) {
		deDialog.parentNode.remove();
	}
}

const bDebug = false;

const uiRightClick = 1500;
const uiScreensaver = 120000;
const sInputId = "HDMI_1";
const sInputName = "HDMI 1";
const sInputSource = "ext://hdmi:1";
const sInputAppId = "com.webos.app.hdmi1";
const sIP = "127.0.0.1";
const uiPort = 41230;
const sMask = "255.255.255.0";
const sMac = "AA:AA:AA:AA:AA:AA";

const sBroadcast = sIP.split(".").map(function(x, i) {
	return(x | (parseInt(sMask.split(".")[i], 10) ^ 0xFF)).toString(10);
}).join(".");
const tabMac = sMac.split(":").map(function(x) {
	return parseInt(x, 16);
});
const sAppID = webOS.fetchAppId();

const oString = GetString();

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
deSource.setAttribute("src", sInputSource);
deSource.setAttribute("type", "service/webos-external;mediaOption=" + encodeURI(JSON.stringify(options)));

var deVideo = document.getElementById("video");
video.appendChild(deSource);

var iIntervalWakeOnLan = 0;
var iTimeoutSourceStatus = 0;
var bInputSourceStatus = false;

webOS.service.request("luna://com.webos.service.eim", {
	method: "getAllInputStatus",
	parameters: {
		subscribe: true
	},
	onSuccess: function(inResponse){
		if(typeof(inResponse.subscribed) === "boolean") {
			LogIfDebug(oString.strGetAllInputStatusSubscribe);
		} else {
			var bLastInputSourceStatus = bInputSourceStatus;
			inResponse.devices.forEach(function(dDevice) {
				if(sInputId === dDevice.id){
					bInputSourceStatus = dDevice.activate;
				}
			});
			if(bInputSourceStatus) {
				if(iIntervalWakeOnLan) {
					clearInterval(iIntervalWakeOnLan);
					iIntervalWakeOnLan = 0;
				}
				if(iTimeoutSourceStatus) {
					clearTimeout(iTimeoutSourceStatus);
					iTimeoutSourceStatus = 0;
				}
				CancelDialog();
				if(bLastInputSourceStatus !== bInputSourceStatus) {
					if(AppVisible()) {
						Connect();
					}
				}
			} else {
				if(bLastInputSourceStatus !== bInputSourceStatus) {
					if(AppVisible()) {
						Close();
					}
				}
				Dialog(oString.strAppTittle, oString.strInputDisconect, [
					{
						sName: oString.strInputDisconectStart,
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
								Dialog(oString.strAppTittle, oString.strInputDisconectWakeOnLanFailure, []);
							}, 5000);
						}
					}
				]);
			}
		}
	},
	onFailure: function(inError){
		Warn(oString.strGetAllInputStatusFailure + " [", inError.errorText, "]");
	}
});

var iTimeoutRetryConnect = 0;
var iTimeoutConnect = 0;
var socClient = null;
function Connect() {
	if(socClient !== null) {
		Error(oString.strSocketErrorOpen);
		socClient.close();
	}
	socClient = new WebSocket("ws://" + sIP + ":" + uiPort);
	socClient.onopen = function(e) {
		if(iTimeoutConnect) {
			clearTimeout(iTimeoutConnect);
			iTimeoutConnect = 0;
		}
		CancelDialog();
		if(CursorVisible() && AppVisible() && AppFocus()) {
			SendMouseVisible({
				bV: true
			});
		}
		LogIfDebug(oString.strSocketConnecting);
	};
	socClient.onclose = function(e) {
		iTimeoutRetryConnect = setTimeout(function() {
			iTimeoutRetryConnect = 0;
			Connect();
		}, 1000);
		socClient = null;
		LogIfDebug(oString.strSocketDisconnecting);
	};
	if(iTimeoutConnect) {
	} else {
		iTimeoutConnect = setTimeout(function() {
			iTimeoutConnect = 0;
			Dialog(oString.strAppTittle, oString.strSocketConnectingTimeout, []);
		}, 30000);
	}
}
function Close() {
	if(socClient === null) {
		Error(oString.strSocketErrorClosed);
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
		LogIfDebug(oString.strSocketDisconnecting);
	};
	if(CursorVisible() && AppVisible() && AppFocus()) {
		SendMouseVisible({
			bV: false
		});
	}
	socClient.close();
}

function SendWol(mMac, sBroadcast) {
	webOS.service.request("luna://" + sAppID + ".send", {
		method: "wol",
		parameters: {
			mMac: mMac,
			sBroadcast: sBroadcast
		},
		onSuccess: function(inResponse) {
			LogIfDebug(oString.strSendWolSuccess + " [0x" + inResponse.sBuffer + "]@" + sBroadcast + ":9 ", mMac);
		},
		onFailure: function(inError) {
			Error(oString.strSendWolFailure + " [", inError.eError, "]@" + sBroadcast + ":9 ", mMac);
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
			//LogIfDebug(oString.strSendMousePositionSuccess + " [0x" + bufMousePosition.toString(16) + "]@" + sIP + ":" + uiPort + " ", pPosition);
		} catch(eError) {
			Error(oString.strSendMousePositionFailure + " [", eError, "]@" + sIP + ":" + uiPort + " ", pPosition);
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
			LogIfDebug(oString.strSendMouseKeySuccess + " [0x" + bufMouseKey.toString(16) + "]@" + sIP + ":" + uiPort + " ", kKey);
		} catch(eError) {
			Error(oString.strSendMouseKeyFailure + " [", eError, "]@" + sIP + ":" + uiPort + " ", kKey);
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
			LogIfDebug(oString.strSendMouseWheelSuccess + " [0x" + bufMouseWheel.toString(16) + "]@" + sIP + ":" + uiPort + " ", wWheel);
		} catch(eError) {
			Error(oString.strSendMouseWheelFailure + " [", eError, "]@" + sIP + ":" + uiPort + " ", wWheel);
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
			LogIfDebug(oString.strSendMouseVisibleSuccess + " [0x" + bufMouseVisible.toString(16) + "]@" + sIP + ":" + uiPort + " ", vVisible);
		} catch(eError) {
			Error(oString.strSendMouseVisibleFailure + " [", eError, "]@" + sIP + ":" + uiPort + " ", vVisible);
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
			dwKeyboardKey.setUint8(3, (kKey.bS << 0) | (kKey.bE << 1));
			socClient.send(bufKeyboardKey);
			LogIfDebug(oString.strSendKeyboardKeySuccess + " [0x" + bufKeyboardKey.toString(16) + "]@" + sIP + ":" + uiPort + " ", kKey);
		} catch(eError) {
			Error(oString.strSendKeyboardKeyFailure + " [", eError, "]@" + sIP + ":" + uiPort + " ", kKey);
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
			LogIfDebug(oString.strSendKeyboardUnicodeSuccess + " [0x" + bufKeyboardUnicode.toString(16) + "]@" + sIP + ":" + uiPort + " ", kUnicode);
		} catch(eError) {
			Error(oString.strSendKeyboardUnicodeFailure + " [", eError, "]@" + sIP + ":" + uiPort + " ", kUnicode);
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
			LogIfDebug(oString.strSendShutdownSuccess + " [0x" + bufShutdown.toString(16) + "]@" + sIP + ":" + uiPort);
		} catch(eError) {
			Error(oString.strSendShutdownFailure + " [", eError, "]@" + sIP + ":" + uiPort);
		}
	}
}

webOS.service.request("luna://com.webos.service.eim", { 
	method: "addDevice", 
	parameters: { 
		appId: sAppID, 
		pigImage: "", 
		mvpdIcon: "", 
		type: "MVPD_IP", 
		showPopup: true, 
		label: oString.strAppTittle + " " + sInputName, 
		description: oString.strAppDescription, 
	}, 
	onSuccess: function(inResponse){ 
		LogIfDebug(oString.strAddDeviceSuccess); 
	}, 
	onFailure: function(inError){ 
		Error(oString.strAddDeviceFailure + " [", inError.errorText, "]"); 
	} 
});

var iIntervalSubscriptionRegisterScreenSaverRequest = setInterval(function() {
	webOS.service.request("luna://com.webos.service.tvpower", { 
		method: "power/registerScreenSaverRequest", 
		parameters: {
			clientName: sAppID,
			subscribe: true
		}, 
		onSuccess: function(inResponse){
			clearInterval(iIntervalSubscriptionRegisterScreenSaverRequest);
			if(typeof(inResponse.subscribed) === "boolean") {
				LogIfDebug(oString.strRegisterScreenSaverRequestSubscribe);
			} else {
				if((AppVisible() && AppFocus())){
					webOS.service.request("luna://com.webos.service.tvpower", { 
						method: "power/responseScreenSaverRequest", 
						parameters: {
							clientName: sAppID,
							ack: !bInputSourceStatus,
							timestamp: inResponse.timestamp
						}, 
						onSuccess: function(inResponse){ 
							LogIfDebug(oString.strResponseScreenSaverRequestSuccess); 
						}, 
						onFailure: function(inError){ 
							Error(oString.strResponseScreenSaverRequestFailure + " [", inError.errorText, "]"); 
						} 
					});
				}
			} 
		}, 
		onFailure: function(inError){ 
			Warn(oString.strRegisterScreenSaverRequestFailure + " [", inError.errorText, "]"); 
		} 
	});
}, 1000);

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

var iIntervalSubscriptionGetSensorData = setInterval(function() {
	webOS.service.request("luna://com.webos.service.mrcu", {
		method: "sensor/getSensorData",
		parameters: {
			callbackInterval: 1,
			subscribe: true
		},
		onSuccess: function(inResponse) {
			clearInterval(iIntervalSubscriptionGetSensorData);
			if(typeof(inResponse.subscribed) === "boolean") {
				LogIfDebug(oString.strGetSensorDataSubscribe);
	 		} else {
				if((AppVisible() && AppFocus())){
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
			Warn(oString.strGetSensorDataFailure + " [", inError.errorText, "]");
			return;
		}
	});
}, 1000);

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
			SendKeyboardKey({
				usC: inEvent.keyCode,
				bS: true,
				bE: false
			});
			break;
		case 0x25:
		case 0x26:
		case 0x27:
		case 0x28:
			SendKeyboardKey({
				usC: inEvent.keyCode,
				bS: true,
				bE: true
			});
			break;
		case 0x194:
			SendKeyboardKey({
				usC: 0x5B,
				bS: true,
				bE: true
			});
			break;
		case 0x195:
			SendMouseKey({
				usC: 1,
				bS: true
			});
			break;
		case 0x1CD:
			SendKeyboardKey({
				usC: 0x1B,
				bS: true,
				bE: false
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
			SendKeyboardKey({
				usC: inEvent.keyCode,
				bS: false,
				bE: false
			});
			break;
		case 0x25:
		case 0x26:
		case 0x27:
		case 0x28:
			SendKeyboardKey({
				usC: inEvent.keyCode,
				bS: false,
				bE: true
			});
			break;
		case 0x193:
			if(bInputSourceStatus){
				Dialog(oString.strAppTittle, oString.strShutdownMessage, [
					{
						sName: oString.strShutdownShutdown,
						fAction: function(){
							SendShutdown();
						}
					}, {
						sName: oString.strShutdownAbort,
						fAction: null
					}
				]);
			} else {
				Dialog(oString.strAppTittle, oString.strStartMessage, [
					{
						sName: oString.strStartStart,
						fAction: function(){
							SendWol({
								tabMac: tabMac
							}, sBroadcast);
						}
					}, {
						sName: oString.strStartAbort,
						fAction: null
					}
				]);
			}
			break;
		case 0x194:
			SendKeyboardKey({
				usC: 0x5B,
				bS: false,
				bE: true
			});
			break;
		case 0x195:
			SendMouseKey({
				usC: 1,
				bS: false,
				bE: false
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
				bS: false,
				bE: false
			});
			break;
		default:
			break;
	}
});

document.getElementById("keyboard").addEventListener("input", function(inEvent) {
	SendKeyboardUnicode({
		usC: inEvent.data.charCodeAt()
	});
});

document.addEventListener("visibilitychange", function() {
	if(!bInputSourceStatus) {
	} else {
		if(AppVisible()) {
			Connect();
		} else {
			Close();
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
