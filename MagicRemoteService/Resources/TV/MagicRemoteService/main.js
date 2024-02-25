
ArrayBuffer.prototype.toString = function(base) {
	return Array.prototype.slice.call(new Uint8Array(this)).map(function(x) {
		return ("00" + x.toString(base)).slice(-2);
	}).join("");
};

Window.prototype.oneEventListener = function(strType, fListener) {
	function Handler(inEvent) {
		this.removeEventListener(strType, Handler);
			if(fListener(inEvent) === false) {
			this.addEventListener(strType, Handler);
		}
	}
	this.addEventListener(strType, Handler);
};
Element.prototype.oneEventListener = Window.prototype.oneEventListener;
Document.prototype.oneEventListener = Window.prototype.oneEventListener;

Object.prototype.spread = function(o) {
	for(var strProperty in this) {
		if(strProperty in o) {
			this[strProperty] = o[strProperty];
		}
	}
};

Object.prototype.toString = function() {
	return JSON.stringify(this);
};

function startInterval(callback, ms) {
	callback();
	return setInterval(callback, ms);
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

const bInputDirect = true;
const bOverlay = true;
const uiLongClick = 1500;
const strInputId = "HDMI";
const strInputAppId = "com.webos.app.hdmi";
const strInputName = "HDMI";
const strInputSource = "ext://hdmi";
const strIP = "127.0.0.1";
const uiPort = 41230;
const strMask = "255.255.255.0";
const strMac = "AA:AA:AA:AA:AA:AA";
const strBroadcast = strIP.split(".").map(function(x, i) {
	return(x | (parseInt(strMask.split(".")[i], 10) ^ 0xFF)).toString(10);
}).join(".");
const tabMac = strMac.split(":").map(function(x) {
	return parseInt(x, 16);
});
const strAppId = "com.cathwyler.magicremoteservice";

function Toast(strTitre, strMessage) {
	var deScreenToast = document.createElement("div");
	deScreenToast.className = "screen flex justify-center align-flex-end";
	var deToast = document.createElement("div");
	deToast.className = "window toast";
	deToast.addEventListener("click", function() {
		ScreenCancel(deScreenToast);
	});
	var dePopupTitre = document.createElement("div");
	dePopupTitre.className = "titre";
	dePopupTitre.innerText = strTitre;
	deToast.appendChild(dePopupTitre);
	if(arguments.length) {
		var dePopupMessage = document.createElement("div");
		dePopupMessage.className = "message";
		dePopupMessage.innerText = strMessage;
		deToast.appendChild(dePopupMessage);
	}
	deScreenToast.appendChild(deToast);
	document.body.appendChild(deScreenToast);
	return deScreenToast;
}

function Dialog(strTitre, strMessage, tabButton) {
	var deScreenDialog = document.createElement("div");
	deScreenDialog.className = "screen flex justify-center align-center";
	var deDialog = document.createElement("div");
	deDialog.className = "window dialog";
	if(strTitre.length) {
		var dePopupTitre = document.createElement("div");
		dePopupTitre.className = "titre";
		dePopupTitre.innerText = strTitre;
		deDialog.appendChild(dePopupTitre);
	}
	if(strMessage.length) {
		var dePopupMessage = document.createElement("div");
		dePopupMessage.className = "message";
		dePopupMessage.innerText = strMessage;
		deDialog.appendChild(dePopupMessage);
	}
	if(tabButton.length) {
		var dePopupButton = document.createElement("div");
		dePopupButton.className = "button flex justify-flex-end align-center";
		tabButton.forEach(function(bButton) {
			var deButton = document.createElement("button");
			deButton.innerText = bButton.sName;
			deButton.addEventListener("click", function() {
				ScreenCancel(deScreenDialog);
			});
			deButton.addEventListener("click", bButton.fAction);
			dePopupButton.appendChild(deButton);
		});
		deDialog.appendChild(dePopupButton);
	}
	deScreenDialog.appendChild(deDialog);
	document.body.appendChild(deScreenDialog);
	return deScreenDialog;
}

function ScreenExist(deScreen) {
	return deScreen !== null && deScreen.parentNode !== null;
}

function ScreenCancel(deScreen) {
	document.body.removeChild(deScreen);
}

var deScreenToast = null;
function Log() {
	if(ScreenExist(deScreenToast)) {
		ScreenCancel(deScreenToast);
	}
	deScreenToast = Toast(oString.strLogTitle, Array.prototype.slice.call(arguments).map(function(x) {
		return x.toString();
	}).join(""));
	console.log.apply(console, arguments);
}

function LogIfDebug() {};
if(bDebug) {
	LogIfDebug = function() {
		Log.apply(this, arguments);
	}
}

function Warn() {
	if(ScreenExist(deScreenToast)) {
		ScreenCancel(deScreenToast);
	}
	deScreenToast = Toast(oString.strLogTitle, Array.prototype.slice.call(arguments).map(function(x) {
		return x.toString();
	}).join(""));
	console.warn.apply(console, arguments);
}

function Error() {
	if(ScreenExist(deScreenToast)) {
		ScreenCancel(deScreenToast);
	}
	deScreenToast = Toast(oString.strLogTitle, Array.prototype.slice.call(arguments).map(function(x) {
		return x.toString();
	}).join(""));
	console.error.apply(console, arguments);
}

function AppVisible() {};
function AppFocus() {};
function CursorVisible() {};
function KeyboardVisible() {};

var deKeyboard = document.getElementById("keyboard");
var deVideo = document.getElementById("video");

var deScreenInput = null;
var iIntervalWakeOnLan = 0;
var iTimeoutSourceStatus = 0;
var pbInputSourceStatus = null;
function InputStatus() {
	return pbInputSourceStatus === true;
}
function InputConnected() {};
if(bInputDirect){
	InputConnected = function() {
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
	};
} else {
	InputConnected = function() {
		if(ScreenExist(deScreenInput)) {
			ScreenCancel(deScreenInput);
		}
	};
}
function InputDisconnected() {};
if(bInputDirect){
	InputDisconnected = function() {
		deScreenInput = Dialog(oString.strAppTittle, oString.strInputDirectDisconnect, [
			{
				sName: oString.strInputDirectDisconnectStart,
				fAction: function() {
					iIntervalWakeOnLan = startInterval(function() {
						SendWol({
							tabMac: tabMac
						}, sBroadcast);
					}, 5000);
					iTimeoutSourceStatus = setTimeout(function() {
						iTimeoutSourceStatus = 0;
						deScreenInput = Dialog(oString.strAppTittle, oString.strInputDirectDisconnectWakeOnLanFailure, []);
					}, 5000);
				}
			}
		]);
	};
} else {
	InputDisconnected = function() {
		deScreenInput = Dialog(oString.strAppTittle, oString.strInputIndirectDisconnect, []);
	};
}
function SubscriptionInputStatus() {
	webOS.service.request("luna://com.webos.service.eim", {
		method: "getAllInputStatus",
		parameters: {
			subscribe: true
		},
		onSuccess: function(inResponse) {
			switch(inResponse.subscribed) {
				case true:
					LogIfDebug(oString.strGetAllInputStatusSuccess);
				case undefined:
					var pbLastInputSourceStatus = pbInputSourceStatus;
					inResponse.devices.forEach(function(dDevice) {
						if(strInputId === dDevice.id) {
							pbInputSourceStatus = dDevice.activate;
						}
					});
					if(pbLastInputSourceStatus === null || pbLastInputSourceStatus !== pbInputSourceStatus) {
						if(pbInputSourceStatus) {
							LogIfDebug(oString.strInputConnected);
							InputConnected();
							if(AppVisible()) {
								Open();
							}
						} else {
							LogIfDebug(oString.strInputDisconnected);
							if(pbLastInputSourceStatus !== null && AppVisible()) {
								Close();
							}
							InputDisconnected();
						}
					}
					break;
				default:
					Error(oString.strGetAllInputStatusFailure);
					break;
				}
		},
		onFailure: function(inError) {
			Error(oString.strGetAllInputStatusFailure + " [", inError.errorText, "]");
			Open();
		}
	});
}

function SubscriptionScreenSaverRequest() {
	webOS.service.request("luna://com.webos.service.tvpower", { 
		method: "power/registerScreenSaverRequest", 
		parameters: {
			clientName: strAppId,
			subscribe: true
		}, 
		onSuccess: function(inResponse) {
			switch(inResponse.subscribed) {
				case undefined:
					if((AppVisible() && AppFocus())) {
						webOS.service.request("luna://com.webos.service.tvpower", { 
							method: "power/responseScreenSaverRequest", 
							parameters: {
								clientName: strAppId,
								ack: socClient === null || socClient.readyState !== WebSocket.OPEN,
								timestamp: inResponse.timestamp
							},
							onSuccess: function(inResponse) {
								LogIfDebug(oString.strResponseScreenSaverRequestSuccess);
							},
							onFailure: function(inError) {
								switch(inError.errorCode) {
									case "-13":
										console.error(oString.strResponseScreenSaverRequestFailure + " [", inError.errorText, "]");
										break;
									default:
										Error(oString.strResponseScreenSaverRequestFailure + " [", inError.errorText, "]");
										break;
								}
							} 
						});
					}
					break;
				case true:
					LogIfDebug(oString.strRegisterScreenSaverRequestSubscribe);
					break;
				default:
					Error(oString.strRegisterScreenSaverRequestFailure);
					break;
			}
		}, 
		onFailure: function(inError) {
			switch(inError.errorCode) {
				case "-1":
					console.error(oString.strRegisterScreenSaverRequestFailure + " [", inError.errorText, "]");
					break;
				default:
					Error(oString.strRegisterScreenSaverRequestFailure + " [", inError.errorText, "]");
					break;
			}
		} 
	});
}

var pCurrent = {
	usX: 0,
	usY: 0
};
var pDown = {
	usX: 0,
	usY: 0
};
var bPositionDownSent = false;
var iTimeoutLongClick = 0;
function SubscriptionGetSensorData() {
	webOS.service.request("luna://com.webos.service.mrcu", {
		method: "sensor/getSensorData",
		parameters: oDevice.versionMajor > 1 ? {
			callbackInterval: 1,
			subscribe: true
		} : {
			callbackInterval: 1,
			subscribe: true,
			sleep: true,
			autoAlign: false
		},
		onSuccess: function(inResponse) {
			switch(inResponse.subscribed) {
				case undefined:
					if((AppVisible() && AppFocus())) {
						if(iTimeoutLongClick && ((pDown.usX - inResponse.coordinate.x) > 3 || (pDown.usX - inResponse.coordinate.x) < -3 || (pDown.usY - inResponse.coordinate.y) > 3 || (pDown.usY - inResponse.coordinate.y) < -3)) {
							clearTimeout(iTimeoutLongClick);
							iTimeoutLongClick = 0;
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
					break;
				case true:
					LogIfDebug(oString.strGetSensorDataSubscribe);
					if(oDevice.versionMajor > 2) {
					} else {
						CursorVisible = function() {
							return true;
						};
					}
					break;
				default:
					Error(oString.strGetSensorDataFailure);
					break;
				}
		},
		onFailure: function(inError) {
			switch(inError.errorCode) {
				case "1301":
					LogIfDebug(oString.strGetSensorDataFailure + " [", inError.errorText, "]");
					if(oDevice.versionMajor > 2) {
					} else {
						CursorVisible = function() {
							return false;
						};
					}
					if(oDevice.versionMajor > 2) {
						document.oneEventListener("cursorStateChange", function(inEvent) {
							if(CursorVisible()) {
								SubscriptionGetSensorData();
								return true;
							} else {
								return false;
							}
						});
					} else {
						window.oneEventListener("keydown", function(inEvent) {
							switch(inEvent.keyCode) {
								case 0x600:
									SubscriptionGetSensorData();
									return true;
								default:
									return false;
							}
						});
					}
					break;
				default:
					Error(oString.strGetSensorDataFailure + " [", inError.errorText, "]");
					break;
			}
		}
	});
}

function AddDevice() {
	webOS.service.request("luna://com.webos.service.eim", {
		method: "addDevice",
		parameters: {
			appId: strAppId,
			pigImage: "",
			mvpdIcon: "",
			type: "MVPD_IP",
			showPopup: true,
			label: oString.strAppTittle,
			description: oString.strAppDescription
		},
		onSuccess: function(inResponse) {
			LogIfDebug(oString.strAddDeviceSuccess);
		},
		onFailure: function(inError) {
			switch(inError.errorCode) {
				case "-1":
				case "EIM.105":
					console.error(oString.strAddDeviceFailure + " [", inError.errorText, "]");
					break;
				default:
					Error(oString.strAddDeviceFailure + " [", inError.errorText, "]");
					break;
			}
		} 
	});
}

function LaunchInput() {
	webOS.service.request("luna://com.webos.applicationManager", {
		method: "launch",
		parameters: {
			id: strInputAppId
		},
		onSuccess: function (inResponse) {
			LogIfDebug(oString.strCloseSuccess);
		},
		onFailure: function (inError) {
			Error(oString.strCloseFailure + " [", inError.errorText, "]");
		},
	});
}

function SubscriptionClose() {
	webOS.service.request("luna://" + strAppId + ".send", {
		method: "close",
		parameters: {
			subscribe: true
		},
		onSuccess: function (inResponse) {
			switch(inResponse.subscribed) {
				case undefined:
					window.close();
					break;
				case true:
					console.log(oString.strCloseSubscribe);
					break;
				default:
					Error(oString.strCloseFailure);
					break;
			}
		},
		onFailure: function (inError) {
			Error(oString.strCloseFailure + " [", inError.errorText, "]");
		},
	});
}

function SubscriptionDomEvent() {
	deVideo.addEventListener("mousedown", function(inEvent) {
		if(iTimeoutLongClick) {
		} else {
			iTimeoutLongClick = setTimeout(function() {
				iTimeoutLongClick = 0;
				SendKey({
					usC: 0x02,
					bS: true
				});
				SendKey({
					usC: 0x02,
					bS: false
				});
			}, uiLongClick);
			pDown.usX = pCurrent.usX
			pDown.usY = pCurrent.usY
		}
	});

	deVideo.addEventListener("mouseup", function(inEvent) {
		if(iTimeoutLongClick) {
			clearTimeout(iTimeoutLongClick);
			iTimeoutLongClick = 0;
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

	if(oDevice.versionMajor > 1) {
		deVideo.addEventListener("wheel", function(inEvent) {
			SendWheel({
				sY: inEvent.deltaY
			});
		});
	} else {
		deVideo.addEventListener("mousewheel", function(inEvent) {
			SendWheel({
				sY: inEvent.wheelDeltaY
			});
		});
	}

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

	deKeyboard.addEventListener("input", function(inEvent) {
		SendUnicode({
			usC: deKeyboard.value.charCodeAt()
		});
		deKeyboard.value = "";
	});

	if(oDevice.versionMajor > 2) {
		document.addEventListener("visibilitychange", function(inEvent) {
			if(InputStatus()) {
				if(AppVisible()) {
					Open();
				} else {
					Close();
				}
			}
		});
	} else {
		document.addEventListener("webkitvisibilitychange", function(inEvent) {
			if(InputStatus()) {
				if(AppVisible()) {
					Open();
				} else {
					Close();
				}
			}
		});
	}

	window.addEventListener("focus", function(inEvent) {
		if(CursorVisible()) {
			SendVisible({
				bV: true
			});
		}
	});

	window.addEventListener("blur", function(inEvent) {
		if(CursorVisible()) {
			SendVisible({
				bV: false
			});
		}
	});

	if(oDevice.versionMajor > 2) {
		document.addEventListener("cursorStateChange", function(inEvent) {
			SendVisible({
				bV: CursorVisible()
			});
		});
	} else {
		document.addEventListener("keydown", function(inEvent) {
			switch(inEvent.keyCode) {
				case 0x600:
					CursorVisible = function() {
						return true;
					};
					SendVisible({
						bV: CursorVisible()
					});
					break;
				case 0x601:
					CursorVisible = function() {
						return false;
					};
					SendVisible({
						bV: CursorVisible()
					});
					break;
			}
		});
	}

	if(oDevice.versionMajor > 2) {
	} else {
		deKeyboard.addEventListener("focus", function(inEvent) {
			KeyboardVisible = function() {
				return true;
			};
		});
		deKeyboard.addEventListener("blur", function(inEvent) {
			KeyboardVisible = function() {
				return false;
			};
		});
	}
}

var deScreenOpen = null;
var deScreenShutdown = null;
var iIntervalRetryOpen = 0;
var iTimeoutOpen = 0;
var socClient = null;
function SocketOpened() {};
if(bInputDirect){
	SocketOpened = function() {
		if(iTimeoutOpen) {
			clearTimeout(iTimeoutOpen);
			iTimeoutOpen = 0;
		}
		if(ScreenExist(deScreenOpen)) {
			ScreenCancel(deScreenOpen);
		}
	}
} else {
	SocketOpened = function() {
		if(iTimeoutOpen) {
			clearTimeout(iTimeoutOpen);
			iTimeoutOpen = 0;
		}
		if(iIntervalWakeOnLan) {
			clearInterval(iIntervalWakeOnLan);
			iIntervalWakeOnLan = 0;
		}
		if(ScreenExist(deScreenOpen)) {
			ScreenCancel(deScreenOpen);
		}
	}
}
function SocketClosed() {};
if(bInputDirect){
	SocketClosed = function() {
		iTimeoutOpen = setTimeout(function() {
			iTimeoutOpen = 0;
			deScreenOpen = Dialog(oString.strAppTittle, oString.strSocketOpenTimeout, []);
		}, 30000);
	};
} else {
	SocketClosed = function() {
		deScreenOpen = Dialog(oString.strAppTittle, oString.strSocketOpen, [
			{
				sName: oString.strSocketOpenStart,
				fAction: function() {
					iIntervalWakeOnLan = startInterval(function() {
						SendWol({
							tabMac: tabMac
						}, sBroadcast);
					}, 5000);
					iTimeoutOpen = setTimeout(function() {
						iTimeoutOpen = 0;
						deScreenOpen = Dialog(oString.strAppTittle, oString.strSocketOpenTimeoutWakeOnLanFailure, []);
					}, 30000);
				}
			}
		]);
	};
}
function SocketOpen() {
	socClient = new WebSocket("ws://" + strIP + ":" + uiPort);
	socClient.binaryType = "arraybuffer";
	socClient.onopen = function(e) {
		LogIfDebug(oString.strSocketOpened);
		SocketOpened();
		clearInterval(iIntervalRetryOpen);
		iIntervalRetryOpen = 0;
		if(ScreenExist(deScreenOpen)) {
			ScreenCancel(deScreenOpen);
		}
		if(CursorVisible() && AppVisible() && AppFocus()) {
			SendVisible({
				bV: true
			});
		}
	};
	socClient.onclose = function(e) {
		LogIfDebug(oString.strSocketClosed);
		if(socClient !== null && !iIntervalRetryOpen) {
			SocketClosed();
			iIntervalRetryOpen = setInterval(function() {
				if(socClient.readyState === WebSocket.CONNECTING) {
					socClient.close();
				}
				SocketOpen();
			}, 5000);
			SocketOpen();
		}
	};
	socClient.onmessage = function(e) {
		if(e.data instanceof ArrayBuffer) {
			var dwData = new DataView(e.data);
			switch(dwData.getUint8(0)) {
				case 0x01:
					if(ScreenExist(deScreenShutdown)) {
						ScreenCancel(deScreenShutdown);
					} else {
						deScreenShutdown = Dialog(oString.strAppTittle, oString.strShutdownMessage, [
							{
								sName: oString.strShutdownShutdown,
								fAction: function() {
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
					if(KeyboardVisible()) {
						deKeyboard.blur();
					} else {
						deKeyboard.focus();
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
function SocketClose() {
	var soc = socClient;
	socClient = null;
	soc.close();
}
function Open() {
	if(socClient !== null) {
		Error(oString.strSocketErrorOpen);
	} else {
		SocketClosed();
		iIntervalRetryOpen = setInterval(function() {
			if(socClient.readyState === WebSocket.CONNECTING) {
				socClient.close();
			}
			SocketOpen();
		}, 5000);
		SocketOpen();
	}
}
function Close() {
	if(socClient === null) {
		Error(oString.strSocketErrorClose);
	} else {
		SocketOpened();
		if(iIntervalRetryOpen) {
			clearInterval(iIntervalRetryOpen);
			iIntervalRetryOpen = 0;
		}
		if(CursorVisible() && AppVisible() && AppFocus()) {
			SendVisible({
				bV: false
			});
		}
		if(ScreenExist(deScreenShutdown)) {
			ScreenCancel(deScreenShutdown);
		}
		if(KeyboardVisible()) {
			deKeyboard.blur();
		}
		SocketClose();
	}
}

function SendWol(mMac, sBroadcast) {
	webOS.service.request("luna://" + strAppId + ".send", {
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
			LogIfDebug(oString.strSendPositionSuccess + " [0x" + bufPosition.toString(16) + "]@" + strIP + ":" + uiPort + " ", pPosition);
		} catch(eError) {
			Error(oString.strSendPositionFailure + " [", eError, "]@" + strIP + ":" + uiPort + " ", pPosition);
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
			LogIfDebug(oString.strSendWheelSuccess + " [0x" + bufWheel.toString(16) + "]@" + strIP + ":" + uiPort + " ", wWheel);
		} catch(eError) {
			Error(oString.strSendWheelFailure + " [", eError, "]@" + strIP + ":" + uiPort + " ", wWheel);
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
			LogIfDebug(oString.strSendVisibleSuccess + " [0x" + bufVisible.toString(16) + "]@" + strIP + ":" + uiPort + " ", vVisible);
		} catch(eError) {
			Error(oString.strSendVisibleFailure + " [", eError, "]@" + strIP + ":" + uiPort + " ", vVisible);
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
			LogIfDebug(oString.strSendKeySuccess + " [0x" + bufKey.toString(16) + "]@" + strIP + ":" + uiPort + " ", kKey);
		} catch(eError) {
			Error(oString.strSendKeyFailure + " [", eError, "]@" + strIP + ":" + uiPort + " ", kKey);
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
			LogIfDebug(oString.strSendUnicodeSuccess + " [0x" + bufUnicode.toString(16) + "]@" + strIP + ":" + uiPort + " ", kUnicode);
		} catch(eError) {
			Error(oString.strSendUnicodeFailure + " [", eError, "]@" + strIP + ":" + uiPort + " ", kUnicode);
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
			LogIfDebug(oString.strSendShutdownSuccess + " [0x" + bufShutdown.toString(16) + "]@" + strIP + ":" + uiPort);
		} catch(eError) {
			Error(oString.strSendShutdownFailure + " [", eError, "]@" + strIP + ":" + uiPort);
		}
	}
}

const strPath = webOS.fetchAppRootPath();
var oString = null;
webOS.service.request('luna://com.webos.settingsservice', {
	method: 'getSystemSettings',
	parameters: {
		keys: ['localeInfo'],
		subscribe: true
	},
	onSuccess: function(inResponse) {
		webOS.fetchAppInfo(function(oInfo) {
			oString = oInfo;
			LogIfDebug(oString.strGetSystemSettingsSuccess);
			Load();
		}, strPath + "appstring.json");
		inResponse.settings.localeInfo.locales.UI.split("-").forEach(function(x, i, arr) {
			webOS.fetchAppInfo(function(oInfo) {
				if(oInfo !== undefined) {
					oString.spread(oInfo);
					LogIfDebug(oString.strGetSystemSettingsSuccess);
					Load();
				}
			}, strPath + "resources/" + arr.slice(0, i + 1).join("/") + "/appstring.json");
		});
	},
	onFailure: function(inError) {
		webOS.fetchAppInfo(function(oInfo) {
			oString = oInfo;
			LogIfDebug(oString.strGetSystemSettingsFailure + " [", inError.errorText, "]");
			Load();
		}, strPath + "appstring.json");
	},
});

var oDevice = null;
webOS.deviceInfo(function(oInfo) {
	oDevice = oInfo;
	Load();
});

var bLoaded = false;
var iTimeoutLoad = 0;
function Load() {
	if(oDevice !== null && oString !== null && bLoaded === false) {
		if(iTimeoutLoad) {
			clearTimeout(iTimeoutLoad);
		}
		iTimeoutLoad = setTimeout(function() {
			bLoaded = true;
			
			if(oDevice.versionMajor > 2) {
				AppVisible = function() {
					return document.hidden === false;
				};
			} else {
				AppVisible = function() {
					return document.webkitHidden === false;
				};
			}
			AppFocus = function() {
				return document.hasFocus() === true;
			};
			if(oDevice.versionMajor > 4) {
				KeyboardVisible = function() {
					return window.webOSSystem.cursor.visibility === true;
				};
			} else if(oDevice.versionMajor > 2) {
				CursorVisible = function() {
					return window.PalmSystem.cursor.visibility === true;
				};
			} else {
				CursorVisible = function() {
					return false;
				};
			}
			if(oDevice.versionMajor > 4) {
				KeyboardVisible = function() {
					return window.webOSSystem.isKeyboardVisible === true;
				};
			} else if(oDevice.versionMajor > 2) {
				KeyboardVisible = function() {
					return window.PalmSystem.isKeyboardVisible === true;
				};
			} else {
				KeyboardVisible = function() {
					return false;
				};
			}
			SubscriptionInputStatus();
			SubscriptionGetSensorData();
			SubscriptionDomEvent();
			if(bOverlay) {
				/*if(oDevice.versionMajor > 4) {
					window.webOSSystem.setWindowProperty("_WEBOS_WINDOW_TYPE", "_WEBOS_WINDOW_TYPE_FLOATING");
				} else if(oDevice.versionMajor > 2) {
					window.PalmSystem.setWindowProperty("_WEBOS_WINDOW_TYPE", "_WEBOS_WINDOW_TYPE_FLOATING");
				} else {
				}*/
				SubscriptionClose();
				LaunchInput();
			} else {
				/*if(oDevice.versionMajor > 4) {
					window.webOSSystem.setWindowProperty("_WEBOS_WINDOW_TYPE", "_WEBOS_WINDOW_TYPE_CARD");
				} else if(oDevice.versionMajor > 2) {
					window.PalmSystem.setWindowProperty("_WEBOS_WINDOW_TYPE", "_WEBOS_WINDOW_TYPE_CARD");
				} else {
				}*/
				SubscriptionScreenSaverRequest();
				AddDevice(); //TODO Move this to window app and launchPoint 

				var deSource = document.createElement("source");
				deSource.setAttribute("src", strInputSource);
				deSource.setAttribute("type", "service/webos-external");
				deVideo.appendChild(deSource);
			}
		}, 1000);
	}
}
