
ArrayBuffer.prototype.toString = function(base) {
	return [].slice.call(new Uint8Array(this)).map(function(x) {
		return ("00" + x.toString(base)).slice(-2);
	}).join("");
}

function Toast(sTitre, sMessage){
	var deScreen = document.createElement("div");
	deScreen.className = "screen flex justify-center align-flex-end";
	var deToast = document.createElement("div");
	deToast.className = "window toast";
	deToast.addEventListener("click", function() {
		ScreenCancel(deScreen);
	});
	var dePopupTitre = document.createElement("div");
	dePopupTitre.className = "titre";
	dePopupTitre.innerText = sTitre;
	deToast.appendChild(dePopupTitre);
	if(arguments.length) {
		var dePopupMessage = document.createElement("div");
		dePopupMessage.className = "message";
		dePopupMessage.innerText = sMessage;
		deToast.appendChild(dePopupMessage);
	}
	deScreen.appendChild(deToast);
	document.body.appendChild(deScreen);
	return deScreen;
}

function Dialog(sTitre, sMessage, tabButton) {
	var deScreen = document.createElement("div");
	deScreen.className = "screen flex justify-center align-center";
	var deDialog = document.createElement("div");
	deDialog.className = "window dialog";
	if(sTitre.length) {
		var dePopupTitre = document.createElement("div");
		dePopupTitre.className = "titre";
		dePopupTitre.innerText = sTitre;
		deDialog.appendChild(dePopupTitre);
	}
	if(sMessage.length) {
		var dePopupMessage = document.createElement("div");
		dePopupMessage.className = "message";
		dePopupMessage.innerText = sMessage;
		deDialog.appendChild(dePopupMessage);
	}
	if(tabButton.length) {
		var dePopupButton = document.createElement("div");
		dePopupButton.className = "button flex justify-flex-end align-center";
		tabButton.forEach(function(bButton) {
			var deButton = document.createElement("button");
			deButton.innerText = bButton.sName;
			deButton.addEventListener("click", function() {
				ScreenCancel(deScreen);
			});
			deButton.addEventListener("click", bButton.fAction);
			dePopupButton.appendChild(deButton);
		});
		deDialog.appendChild(dePopupButton);
	}
	deScreen.appendChild(deDialog);
	document.body.appendChild(deScreen);
	return deScreen;
}

function ScreenExist(deScreen) {
	return deScreen !== null && deScreen.parentNode !== null;
}

function ScreenCancel(deScreen) {
	deScreen.remove();
}

function ObjectToString(o){
	var arr = [];
	for (var strProperty in o) {
		arr.push(o[strProperty])
	}
	return arr.map(function(x) {
		if(typeof x === "object") {
			return JSON.stringify(x);
		} else {
			return x.toString();
		}
	}).join("");
}

var deScreenToast = null;
function Log() {
	if(ScreenExist(deScreenToast)) {
		ScreenCancel(deScreenToast);
	}
	deScreenToast = Toast(oString.strLogTitle, ObjectToString(arguments));
	console.log.apply(console, arguments);
}

function LogIfDebug() {
	if(bDebug) {
		Log.apply(this, arguments);
	}
}

function Warn() {
	if(ScreenExist(deScreenToast)) {
		ScreenCancel(deScreenToast);
	}
	deScreenToast = Toast(oString.strWarnTitle, ObjectToString(arguments));
	console.warn.apply(console, arguments);
}

function Error() {
	if(ScreenExist(deScreenToast)) {
		ScreenCancel(deScreenToast);
	}
	deScreenToast = Toast(oString.strErrorTitle, ObjectToString(arguments));
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

const bDebug = false;

const MessageType = {
	Position: 0x00,
	Wheel: 0x01,
	Visible: 0x02,
	Key: 0x03,
	Unicode: 0x04,
	Shutdown: 0x05
}

const uiLongDown = 1500;
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
deVideo.appendChild(deSource);

var deScreenInput = null;
var iIntervalWakeOnLan = 0;
var iTimeoutSourceStatus = 0;
var pbInputSourceStatus = null;
var iIntervalSubscriptionGetAllInputStatus = setInterval(function() {
	webOS.service.request("luna://com.webos.service.eim", {
		method: "getAllInputStatus",
		parameters: {
			subscribe: true
		},
		onSuccess: function(inResponse){
			if(typeof(inResponse.subscribed) === "boolean") {
				clearInterval(iIntervalSubscriptionGetAllInputStatus);
				LogIfDebug(oString.strGetAllInputStatusSubscribe);
			} else {
				var pbLastInputSourceStatus = pbInputSourceStatus;
				inResponse.devices.forEach(function(dDevice) {
					if(sInputId === dDevice.id){
						pbInputSourceStatus = dDevice.activate;
					}
				});
				if(pbLastInputSourceStatus === null || pbLastInputSourceStatus !== pbInputSourceStatus) {
					if(pbInputSourceStatus) {
						if(iIntervalWakeOnLan) {
							clearInterval(iIntervalWakeOnLan);
							iIntervalWakeOnLan = 0;
						}
						if(iTimeoutSourceStatus) {
							clearTimeout(iTimeoutSourceStatus);
							iTimeoutSourceStatus = 0;
						}
						if(ScreenExist(deScreenInput)) {
							ScreenCancel(deScreenInput);
						}
						if(AppVisible()) {
							Connect();
						}
					} else {
						if(pbLastInputSourceStatus !== null && AppVisible()) {
							Close();
						}
						deScreenInput = Dialog(oString.strAppTittle, oString.strInputDisconect, [
							{
								sName: oString.strInputDisconectStart,
								fAction: function() {
									SendWol({
										tabMac: tabMac
									}, sBroadcast);
									iIntervalWakeOnLan = setInterval(function() {
										SendWol({
											tabMac: tabMac
										}, sBroadcast);
									}, 5000);
									iTimeoutSourceStatus = setTimeout(function() {
										iTimeoutSourceStatus = 0;
										deScreenInput = Dialog(oString.strAppTittle, oString.strInputDisconectWakeOnLanFailure, []);
									}, 5000);
								}
							}
						]);
					}
				}
			}
		},
		onFailure: function(inError){
			//Warn(oString.strGetAllInputStatusFailure + " [", inError.errorText, "]");
		}
	});
}, 1000);

var deScreenConnect = null;
var deScreenShutdown = null;
var iIntervalRetryConnect = 0;
var iTimeoutConnect = 0;
var socClient = null;
function SocketConnect(){
	socClient = new WebSocket("ws://" + sIP + ":" + uiPort);
	socClient.binaryType = "arraybuffer";
	socClient.onopen = function(e) {
		clearInterval(iIntervalRetryConnect);
		iIntervalRetryConnect = 0;
		if(iTimeoutConnect) {
			clearTimeout(iTimeoutConnect);
			iTimeoutConnect = 0;
		}
		if (ScreenExist(deScreenConnect)) {
			ScreenCancel(deScreenConnect);
		}
		if(CursorVisible() && AppVisible() && AppFocus()) {
			SendVisible({
				bV: true
			});
		}
		LogIfDebug(oString.strSocketConnecting);
	};
	socClient.onclose = function(e) {
		if(socClient !== null && !iIntervalRetryConnect) {
			iTimeoutConnect = setTimeout(function() {
				iTimeoutConnect = 0;
				deScreenConnect = Dialog(oString.strAppTittle, oString.strSocketConnectingTimeout, []);
			}, 30000);
			iIntervalRetryConnect = setInterval(function() {
				if(socClient.readyState === WebSocket.CONNECTING) {
					socClient.close();
				}
				SocketConnect();
			}, 5000);
			SocketConnect();
		}
		LogIfDebug(oString.strSocketDisconnecting);
	};
	socClient.onmessage = function(e) {
		if (e.data instanceof ArrayBuffer) {
			var dwData = new DataView(e.data);
			switch(dwData.getUint8(0)){
				case 0x01:
					if (ScreenExist(deScreenShutdown)) {
						ScreenCancel(deScreenShutdown);
					} else {
						deScreenShutdown = Dialog(oString.strAppTittle, oString.strShutdownMessage, [
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
					}
					break;
				case 0x02:
					if (KeyboardVisible()) {
						document.getElementById("keyboard").blur();
					} else {
						document.getElementById("keyboard").focus();
					}
					break;
				default:
					Error(oString.strActionUnprocessed);
			}
		} else {
			Log(e.data);
		}
	}
}
function Connect() {
	if(socClient !== null) {
		Error(oString.strSocketErrorOpen);
	} else {
		iTimeoutConnect = setTimeout(function() {
			iTimeoutConnect = 0;
			deScreenConnect = Dialog(oString.strAppTittle, oString.strSocketConnectingTimeout, []);
		}, 30000);
		iIntervalRetryConnect = setInterval(function() {
			if(socClient.readyState === WebSocket.CONNECTING) {
				socClient.close();
			}
			SocketConnect();
		}, 5000);
		SocketConnect();
	}
}
function Close() {
	if(socClient === null) {
		Error(oString.strSocketErrorClosed);
	} else {
		if(iTimeoutConnect) {
			clearTimeout(iTimeoutConnect);
			iTimeoutConnect = 0;
		}
		if(iIntervalRetryConnect) {
			clearInterval(iIntervalRetryConnect);
			iIntervalRetryConnect = 0;
		}
		if (ScreenExist(deScreenConnect)) {
			ScreenCancel(deScreenConnect);
		}
		if(CursorVisible() && AppVisible() && AppFocus()) {
			SendVisible({
				bV: false
			});
		}
		if (ScreenExist(deScreenShutdown)) {
			ScreenCancel(deScreenShutdown);
		}
		if (KeyboardVisible()) {
			document.getElementById("keyboard").blur();
		}
		var soc = socClient;
		socClient = null;
		soc.close();
	}
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
  
var bufPosition = new ArrayBuffer(5);
var dwPosition = new DataView(bufPosition);
dwPosition.setUint8(0, MessageType.Position);
function SendPosition(pPosition) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwPosition.setUint16(1, pPosition.usX, true);
			dwPosition.setUint16(3, pPosition.usY, true);
			socClient.send(bufPosition);
			//LogIfDebug(oString.strSendPositionSuccess + " [0x" + bufPosition.toString(16) + "]@" + sIP + ":" + uiPort + " ", pPosition);
		} catch(eError) {
			Error(oString.strSendPositionFailure + " [", eError, "]@" + sIP + ":" + uiPort + " ", pPosition);
		}
	}
}

var bufWheel = new ArrayBuffer(3);
var dwWheel = new DataView(bufWheel);
dwWheel.setUint8(0, MessageType.Wheel);
function SendWheel(wWheel) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwWheel.setInt16(1, wWheel.sY, true);
			socClient.send(bufWheel);
			LogIfDebug(oString.strSendWheelSuccess + " [0x" + bufWheel.toString(16) + "]@" + sIP + ":" + uiPort + " ", wWheel);
		} catch(eError) {
			Error(oString.strSendWheelFailure + " [", eError, "]@" + sIP + ":" + uiPort + " ", wWheel);
		}
	}
}

var bufVisible = new ArrayBuffer(2);
var dwVisible = new DataView(bufVisible);
dwVisible.setUint8(0, MessageType.Visible);
function SendVisible(vVisible) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwVisible.setUint8(1, vVisible.bV);
			socClient.send(bufVisible);
			LogIfDebug(oString.strSendVisibleSuccess + " [0x" + bufVisible.toString(16) + "]@" + sIP + ":" + uiPort + " ", vVisible);
		} catch(eError) {
			Error(oString.strSendVisibleFailure + " [", eError, "]@" + sIP + ":" + uiPort + " ", vVisible);
		}
	}
}

var bufKey = new ArrayBuffer(4);
var dwKey = new DataView(bufKey);
dwKey.setUint8(0, MessageType.Key);
function SendKey(kKey) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwKey.setUint16(1, kKey.usC, true);
			dwKey.setUint8(3, kKey.bS);
			socClient.send(bufKey);
			LogIfDebug(oString.strSendKeySuccess + " [0x" + bufKey.toString(16) + "]@" + sIP + ":" + uiPort + " ", kKey);
		} catch(eError) {
			Error(oString.strSendKeyFailure + " [", eError, "]@" + sIP + ":" + uiPort + " ", kKey);
		}
	}
}

var bufUnicode = new ArrayBuffer(3);
var dwUnicode = new DataView(bufUnicode);
dwUnicode.setUint8(0, MessageType.Unicode);
function SendUnicode(kUnicode) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwUnicode.setUint16(1, kUnicode.usC, true);
			socClient.send(bufUnicode);
			LogIfDebug(oString.strSendUnicodeSuccess + " [0x" + bufUnicode.toString(16) + "]@" + sIP + ":" + uiPort + " ", kUnicode);
		} catch(eError) {
			Error(oString.strSendUnicodeFailure + " [", eError, "]@" + sIP + ":" + uiPort + " ", kUnicode);
		}
	}
}

var bufShutdown = new ArrayBuffer(1);
var dwShutdown = new DataView(bufShutdown);
dwShutdown.setUint8(0, MessageType.Shutdown);
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
			if(typeof(inResponse.subscribed) === "boolean") {
				clearInterval(iIntervalSubscriptionRegisterScreenSaverRequest);
				LogIfDebug(oString.strRegisterScreenSaverRequestSubscribe);
			} else {
				if((AppVisible() && AppFocus())){
					webOS.service.request("luna://com.webos.service.tvpower", { 
						method: "power/responseScreenSaverRequest", 
						parameters: {
							clientName: sAppID,
							ack: socClient === null || socClient.readyState !== WebSocket.OPEN,
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
			//Warn(oString.strRegisterScreenSaverRequestFailure + " [", inError.errorText, "]"); 
		} 
	});
}, 1000);

var pCurrent = {
	usX: 0,
	usY: 0
};
var pDown = {
	usX: 0,
	usY: 0
};
var bPositionDownSent = false;
var iTimeoutLongDown = 0;
var iIntervalSubscriptionGetSensorData = setInterval(function() {
	webOS.service.request("luna://com.webos.service.mrcu", {
		method: "sensor/getSensorData",
		parameters: {
			callbackInterval: 1,
			subscribe: true
		},
		onSuccess: function(inResponse) {
			if(typeof(inResponse.subscribed) === "boolean") {
				clearInterval(iIntervalSubscriptionGetSensorData);
				LogIfDebug(oString.strGetSensorDataSubscribe);
	 		} else {
				if((AppVisible() && AppFocus())){
					if(iTimeoutLongDown && ((pDown.usX - inResponse.coordinate.x) > 3 || (pDown.usX - inResponse.coordinate.x) < -3 || (pDown.usY - inResponse.coordinate.y) > 3 || (pDown.usY - inResponse.coordinate.y) < -3)) {
						clearTimeout(iTimeoutLongDown);
						iTimeoutLongDown = 0;
						SendKey({
							usC: 0x01,
							bS: true
						});
						bPositionDownSent = true;
					}
					pCurrent.usX = inResponse.coordinate.x;
					pCurrent.usY = inResponse.coordinate.y;
					SendPosition({
						usX: inResponse.coordinate.x,
						usY: inResponse.coordinate.y
					});
				}
	 		}
			return true;
		},
		onFailure: function(inError) {
			//Warn(oString.strGetSensorDataFailure + " [", inError.errorText, "]");
			return;
		}
	});
}, 1000);

document.getElementById("video").addEventListener("mousedown", function(inEvent) {
	if(iTimeoutLongDown) {
	} else {
		iTimeoutLongDown = setTimeout(function() {
			iTimeoutLongDown = 0;
			SendKey({
				usC: 0x02,
				bS: true
			});
			SendKey({
				usC: 0x02,
				bS: false
			});
		}, uiLongDown);
		pDown.usX = pCurrent.usX
		pDown.usY = pCurrent.usY
	}
});

document.getElementById("video").addEventListener("mouseup", function(inEvent) {
	if(iTimeoutLongDown) {
		clearTimeout(iTimeoutLongDown);
		iTimeoutLongDown = 0;
		SendKey({
			usC: 0x01,
			bS: true
		});
		bPositionDownSent = true;
	}
	if(bPositionDownSent) {
		SendKey({
			usC: 0x01,
			bS: false
		});
		bPositionDownSent = false;
	}
});

document.getElementById("video").addEventListener("wheel", function(inEvent) {
	SendWheel({
		sY: inEvent.deltaY
	});
});

document.addEventListener("keydown", function(inEvent) {
	SendKey({
		usC: inEvent.keyCode,
		bS: true
	});
});

document.addEventListener("keyup", function(inEvent) {
	SendKey({
		usC: inEvent.keyCode,
		bS: false
	});
});

document.getElementById("keyboard").addEventListener("input", function(inEvent) {
	SendUnicode({
		usC: inEvent.data.charCodeAt()
	});
});

document.addEventListener("visibilitychange", function() {
	if(pbInputSourceStatus === null || !pbInputSourceStatus) {
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
		SendVisible({
			bV: true
		});
	}
});

window.addEventListener("blur", function() {
	if(CursorVisible()) {
		SendVisible({
			bV: false
		});
	}
});

document.addEventListener("cursorStateChange", function(inEvent) {
	SendVisible({
		bV: CursorVisible()
	});
});
