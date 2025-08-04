
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
	const arrAncestor = [];
	return JSON.stringify(this, function(k, o) {
		if(typeof o !== "object" || o === null) {
			return o;
		} else {
			while(arrAncestor.length > 0 && arrAncestor[arrAncestor.length - 1] !== this) {
				arrAncestor.pop();
			}
			if(arrAncestor.indexOf(o) !== -1 ) {
				return "[Circular]";
			}
			arrAncestor.push(o);
			return o;
		}
	});
};

function startInterval(callback, ms) {
	callback();
	return setInterval(callback, ms);
}

function readJson(fCallback, strPath) {
	var xhr = new XMLHttpRequest();
	xhr.onload = function(e) {
		fCallback(JSON.parse(xhr.responseText));
	};
	xhr.onerror = function(e) {
		fCallback();
	};
	xhr.open("GET", strPath, true);
	xhr.send(null);
}

const bDebug = false;

const MessageType = {
	PositionRelative: 0x00,
	PositionAbsolute: 0x01,
	Wheel: 0x02,
	Visible: 0x03,
	Key: 0x04,
	Unicode: 0x05,
	Shutdown: 0x06
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
const arrMac = strMac.split(":").map(function(x) {
	return parseInt(x, 16);
});
const aSensor = {
	dMax: 50,
	dFactor: 2,
}
const strAppId = "com.cathwyler.magicremoteservice";

const strPath = webOS.fetchAppRootPath();
var arrVersion = null;
var oString = null;

var deKeyboard = document.getElementById("keyboard");
var deVideo = document.getElementById("video");

var deScreenToast = null;
function Toast(strTitle, strMessage) {
	if(ScreenExist(deScreenToast)) {
		ScreenCancel(deScreenToast, false);
	}
	deScreenToast = document.createElement("div");
	deScreenToast.className = "screen flex justify-center align-flex-end";
	var deToast = document.createElement("div");
	deToast.className = "window toast";
	deToast.addEventListener("click", function() {
		ScreenCancel(deScreenToast, false);
	});
	if(strTitle.length) {
		var dePopupTitle = document.createElement("div");
		dePopupTitle.className = "title";
		dePopupTitle.innerText = strTitle;
		deToast.appendChild(dePopupTitle);
	}
	if(strMessage.length) {
		var dePopupMessage = document.createElement("div");
		dePopupMessage.className = "message";
		dePopupMessage.innerText = strMessage;
		deToast.appendChild(dePopupMessage);
	}
	deScreenToast.appendChild(deToast);
	document.body.appendChild(deScreenToast);
}

function Dialog(strTitle, strMessage, arrButton) {
	CursorShowCountIf0();
	var deScreenDialog = document.createElement("div");
	deScreenDialog.className = "screen flex justify-center align-center";
	var deDialog = document.createElement("div");
	deDialog.className = "window dialog";
	if(strTitle.length) {
		var dePopupTitle = document.createElement("div");
		dePopupTitle.className = "title";
		dePopupTitle.innerText = strTitle;
		deDialog.appendChild(dePopupTitle);
	}
	if(strMessage.length) {
		var dePopupMessage = document.createElement("div");
		dePopupMessage.className = "message";
		dePopupMessage.innerText = strMessage;
		deDialog.appendChild(dePopupMessage);
	}
	if(arrButton.length) {
		var dePopupButton = document.createElement("div");
		dePopupButton.className = "button flex justify-flex-end align-center";
		arrButton.forEach(function(bButton) {
			var deButton = document.createElement("button");
			deButton.innerText = bButton.strName;
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

function ScreenCancel(deScreen, bCursor) {
	document.body.removeChild(deScreen);
	if(bCursor === undefined || bCursor === true) {
		CursorHideCountIf0();
	}
}

function Log() {
	console.log.apply(console, arguments);
	Toast(oString.strLogTitle, Array.prototype.slice.call(arguments).map(function(x) {
		if(typeof o !== "object" || o === null) {
			return x;
		} else {
			return x.toString();
		}
	}).join(""));
}

function LogIfDebug() {};
if(bDebug) {
	LogIfDebug = function() {
		Log.apply(this, arguments);
	}
}

function Warn() {
	console.warn.apply(console, arguments);
	Toast(oString.strWarnTitle, Array.prototype.slice.call(arguments).map(function(x) {
		if(typeof o !== "object" || o === null) {
			return x;
		} else {
			return x.toString();
		}
	}).join(""));
}

function Error() {
	console.error.apply(console, arguments);
	Toast(oString.strErrorTitle, Array.prototype.slice.call(arguments).map(function(x) {
		if(typeof o !== "object" || o === null) {
			return x;
		} else {
			return x.toString();
		}
	}).join(""));
}

function AppVisible() {};

function CursorVisible() {};

function AppFocus() {
	return document.hasFocus() === true;
};

function KeyboardVisible() {
	return document.activeElement === deKeyboard;
};

function CursorHide() {};
function CursorShow() {};
var iCursor = 1;
function CursorHideCountIf0() {
	iCursor--;
	if (iCursor == 0) {
		//CursorHide();
	}
};
function CursorShowCountIf0() {
	if (iCursor == 0) {
		//CursorShow();
	}
	iCursor++;
};

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
				strName: oString.strInputDirectDisconnectStart,
				fAction: function() {
					iIntervalWakeOnLan = startInterval(function() {
						SendWol({
							arrMac: arrMac
						}, strBroadcast);
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
					if(AppVisible() && AppFocus() && iCursor == 0) {
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
				case "-3":
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
	dX: 960,
	dY: 540,
	sRoundX: 960,
	sRoundY: 540,
	sLastRoundX: 960,
	sLastRoundY: 540,
	dLastReset: 0
};
var pDown = {
	dX: 0,
	dY: 0,
};

function ResetQuaternion() {
	webOS.service.request("luna://com.webos.service.mrcu", {
		method: "sensor/resetQuaternion",
		onSuccess: function (inResponse) {
			console.error("Succeeded to reset the quaternion sensor");
			// To-Do something
		},
		onFailure: function (inError) {
			console.error("[" + inError.errorCode + "]: " + inError.errorText);
			// To-Do something
		},
	});
}

var bPositionDownSent = false;
var iTimeoutLongClick = 0;
function SubscriptionGetSensorData() {
	webOS.service.request("luna://com.webos.service.mrcu", {
		method: "sensor/getSensorData",
		parameters: arrVersion[0] > 1 ? {
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
					if(AppVisible() && AppFocus()) {
						if(iCursor == 0) {
							// var pitchX = -Math.atan2(2 * (inResponse.quaternion.q3 * inResponse.quaternion.q1 - inResponse.quaternion.q0 * inResponse.quaternion.q2), 1 - 2 * (qinResponse.quaternion.q1 * inResponse.quaternion.q1 - inResponse.quaternion.q0 * inResponse.quaternion.q0));
							// var pitchZ = -Math.atan2(2 * (inResponse.quaternion.q3 * inResponse.quaternion.q1 - inResponse.quaternion.q0 * inResponse.quaternion.q2), 1 - 2 * (inResponse.quaternion.q1 * inResponse.quaternion.q1 - inResponse.quaternion.q2 * inResponse.quaternion.q2));
							// var pitchY = -Math.asin(2 * (inResponse.quaternion.q3 * inResponse.quaternion.q1 - inResponse.quaternion.q0 * inResponse.quaternion.q2));
							
							var dTheta = Math.atan2(inResponse.gyroscope.z, inResponse.gyroscope.x) - Math.atan2(2 * (inResponse.quaternion.q3 * inResponse.quaternion.q1 - inResponse.quaternion.q0 * inResponse.quaternion.q2), 1 - 2 * (inResponse.quaternion.q1 * inResponse.quaternion.q1 - inResponse.quaternion.q0 * inResponse.quaternion.q0));
							var dRho = Math.sqrt(Math.pow(inResponse.gyroscope.z, 2) + Math.pow(inResponse.gyroscope.x, 2));
							
							if(dRho > 0) {
								var dRhoAcceleration = aSensor.dMax * dRho * Math.tanh(aSensor.dFactor * dRho);
								pCurrent.dX -= dRhoAcceleration * Math.sin(dTheta);
								pCurrent.dY -= dRhoAcceleration * Math.cos(dTheta);
								pCurrent.dLastReset += dRhoAcceleration;

								pCurrent.sRoundX = Math.round(pCurrent.dX);
								pCurrent.sRoundY = Math.round(pCurrent.dY);
								if(iTimeoutLongClick && ((pDown.dX - pCurrent.dX) > 3 || (pDown.dX - pCurrent.dX) < -3 || (pDown.dY - pCurrent.dY) > 3 || (pDown.dY - pCurrent.dY) < -3)) {
									clearTimeout(iTimeoutLongClick);
									iTimeoutLongClick = 0;
									SendKey({
										usC: 0x01,
										bS: true
									});
									bPositionDownSent = true;
								}
								SendPositionRelative({
									sX: pCurrent.sRoundX - pCurrent.sLastRoundX,
									sY: pCurrent.sRoundY - pCurrent.sLastRoundY
								});
								pCurrent.sLastRoundX = pCurrent.sRoundX;
								pCurrent.sLastRoundY = pCurrent.sRoundY;
								if(pCurrent.dLastReset > 1000) {
									ResetQuaternion();
									pCurrent.dLastReset = 0;
								}
							}
						} else {
							pCurrent.dX = inResponse.coordinate.x;
							pCurrent.dY = inResponse.coordinate.y;
							pCurrent.sRoundX = pCurrent.dX;
							pCurrent.sRoundY = pCurrent.dY;
							if(pCurrent.sLastRoundX != pCurrent.sRoundX || pCurrent.sLastRoundY != pCurrent.sRoundY) {
								if(iTimeoutLongClick && ((pDown.dX - pCurrent.dX) > 3 || (pDown.dX - pCurrent.dX) < -3 || (pDown.dY - pCurrent.dY) > 3 || (pDown.dY - pCurrent.dY) < -3)) {
									clearTimeout(iTimeoutLongClick);
									iTimeoutLongClick = 0;
									SendKey({
										usC: 0x01,
										bS: true
									});
									bPositionDownSent = true;
								}
								SendPositionAbsolute({
									usX: pCurrent.sRoundX,
									usY: pCurrent.sRoundY
								});
								pCurrent.sLastRoundX = pCurrent.sRoundX;
								pCurrent.sLastRoundY = pCurrent.sRoundY;
							}
						}
					}
					break;
				case true:
					LogIfDebug(oString.strGetSensorDataSubscribe);
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
					if(arrVersion[0] > 2) {
						document.oneEventListener("cursorStateChange", function(inEvent) {
							if(inEvent.detail.visibility) {
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

function LaunchInput() {
	webOS.service.request("luna://com.webos.applicationManager", {
		method: "launch",
		parameters: {
			id: strInputAppId
		},
		onSuccess: function(inResponse) {
			LogIfDebug(oString.strLaunchSuccess);
		},
		onFailure: function(inError) {
			Error(oString.strLaunchFailure + " [", inError.errorText, "]");
		},
	});
}

function SubscriptionClose() {
	webOS.service.request("luna://" + strAppId + ".service", {
		method: "close",
		parameters: {
			subscribe: true
		},
		onSuccess: function(inResponse) {
			switch(inResponse.subscribed) {
				case undefined:
					window.close();
					break;
				case true:
					LogIfDebug(oString.strCloseSubscribe);
					break;
				default:
					Error(oString.strCloseFailure);
					break;
			}
		},
		onFailure: function(inError) {
			Error(oString.strCloseFailure + " [", inError.errorText, "]");
		},
	});
}

function SubscriptionLog() {
	webOS.service.request("luna://" + strAppId + ".service", {
		method: "log",
		parameters: {
			subscribe: true
		},
		onSuccess: function(inResponse) {
			switch(inResponse.subscribed) {
				case undefined:
					switch(inResponse.log.iType){
						case 0:
							if(inResponse.log.bConsole){
								console.log(inResponse.log.strMessage);
							} else{
								Log(inResponse.log.strMessage);
							}
							break;
						case 1:
							if(inResponse.log.bConsole){
								console.warn(inResponse.log.strMessage);
							} else{
								Warn(inResponse.log.strMessage);
							}
							break;
						case 2:
							if(inResponse.log.bConsole){
								console.error(inResponse.log.strMessage);
							} else{
								Error(inResponse.log.strMessage);
							}
							break;
					}
					break;
				case true:
					LogIfDebug(oString.strLogSubscribe);
					break;
				default:
					Error(oString.strLogFailure);
					break;
			}
		},
		onFailure: function(inError) {
			Error(oString.strLogFailure + " [", inError.errorText, "]");
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
			pDown.dX = pCurrent.dX
			pDown.dY = pCurrent.dY
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

	if(arrVersion[0] > 1) {
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
		for(var i = 0; i < deKeyboard.value.length; i++){
			SendUnicode({
				usC: deKeyboard.value.charCodeAt(i)
			});
		}
		deKeyboard.value = "";
	});

	document.addEventListener(arrVersion[0] > 2 ? "visibilitychange" : "webkitvisibilitychange", function(inEvent) {
		if(InputStatus()) {
			if(AppVisible()) {
				Open();
			} else {
				Close();
			}
		}
	});

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

	if(arrVersion[0] > 2) {
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

	deKeyboard.addEventListener("focus", function(inEvent) {
		CursorShowCountIf0();
	});
	deKeyboard.addEventListener("blur", function(inEvent) {
		CursorHideCountIf0();
	});
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
				strName: oString.strSocketOpenStart,
				fAction: function() {
					iIntervalWakeOnLan = startInterval(function() {
						SendWol({
							arrMac: arrMac
						}, strBroadcast);
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
		CursorHideCountIf0();
		if(CursorVisible() && AppVisible() && AppFocus()) {
			SendVisible({
				bV: true
			});
		}
	};
	socClient.onclose = function(e) {
		LogIfDebug(oString.strSocketClosed);
		if(socClient !== null && !iIntervalRetryOpen) {
			CursorShowCountIf0();
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
								strName: oString.strShutdownShutdown,
								fAction: function() {
									SendShutdown();
								}
							}, {
								strName: oString.strShutdownAbort,
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
		if(AppVisible() && AppFocus() && CursorVisible()) {
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

function SendWol(mMac, strBroadcast) {
	webOS.service.request("luna://" + strAppId + ".service", {
		method: "wol",
		parameters: {
			mMac: mMac,
			strBroadcast: strBroadcast
		},
		onSuccess: function(inResponse) {
			LogIfDebug(oString.strSendWolSuccess + " [0x" + inResponse.strBuffer + "]@" + strBroadcast + ":9 ", mMac);
		},
		onFailure: function(inError) {
			Error(oString.strSendWolFailure + " [", inError.errorText, "]@" + strBroadcast + ":9 ", mMac);
		}
	});
}

var bufPositionRelative = new ArrayBuffer(5);
var dwPositionRelative = new DataView(bufPositionRelative);
dwPositionRelative.setUint8(0, MessageType.PositionRelative);
function SendPositionRelative(pPositionRelative) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwPositionRelative.setInt16(1, pPositionRelative.sX, true);
			dwPositionRelative.setInt16(3, pPositionRelative.sY, true);
			socClient.send(bufPositionRelative);
			//LogIfDebug(oString.strSendPositionRelativeSuccess + " [0x" + bufPositionRelative.toString(16) + "]@" + strIP + ":" + uiPort + " ", pPositionRelative);
		} catch(eError) {
			Error(oString.strSendPositionRelativeFailure + " [", eError, "]@" + strIP + ":" + uiPort + " ", pPositionRelative);
		}
	}
}

var bufPositionAbsolute = new ArrayBuffer(5);
var dwPositionAbsolute = new DataView(bufPositionAbsolute);
dwPositionAbsolute.setUint8(0, MessageType.PositionAbsolute);
function SendPositionAbsolute(pPositionAbsolute) {
	if(socClient !== null && socClient.readyState === WebSocket.OPEN) {
		try {
			dwPositionAbsolute.setUint16(1, pPositionAbsolute.usX, true);
			dwPositionAbsolute.setUint16(3, pPositionAbsolute.usY, true);
			socClient.send(bufPositionAbsolute);
			//LogIfDebug(oString.strSendPositionAbsoluteSuccess + " [0x" + bufPositionAbsolute.toString(16) + "]@" + strIP + ":" + uiPort + " ", pPositionAbsolute);
		} catch(eError) {
			Error(oString.strSendPositionAbsoluteFailure + " [", eError, "]@" + strIP + ":" + uiPort + " ", pPositionAbsolute);
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

webOS.service.request("luna://com.webos.service.tv.systemproperty", {
	method: "getSystemInfo",
	parameters: {
		keys: ["sdkVersion"]
	},
	onSuccess: function(inResponse) {
		arrVersion = inResponse.sdkVersion.split(".").map(function(x) {
			return parseInt(x);
		});
		Load();
	},
	onFailure: function(inError) {
		throw new Error(inError.errorText);
	}
});

webOS.service.request("luna://com.webos.settingsservice", {
	method: "getSystemSettings",
	parameters: {
		keys: ["localeInfo"],
		subscribe: true
	},
	onSuccess: function(inResponse) {
		readJson(function(oInfo) {
			oString = oInfo;
			function readJsonLocale(i, arr){
				if(i < arr.length){
					readJson(function(oInfo) {
						if(oInfo !== undefined){
							oString.spread(oInfo);
						}
						readJsonLocale(i + 1, arr);
					}, strPath + "resources/" + arr.slice(0, i + 1).join("/") + "/appstring.json");
				} else {
					Load();
				}
			}
			readJsonLocale(0, inResponse.settings.localeInfo.locales.UI.split("-"));
		}, strPath + "/appstring.json");
	},
	onFailure: function(inError) {
		throw new Error(inError.errorText);
	}
});

var bLoaded = false;
function Load() {
	if(arrVersion !== null && oString !== null && bLoaded === false) {
		bLoaded = true;
		if(arrVersion[0] > 2) {
			AppVisible = function() {
				return document.hidden === false;
			};
		} else {
			AppVisible = function() {
				return document.webkitHidden === false;
			};
		}
		if(arrVersion[0] > 4) {
			CursorHide = function() {
				webOSSystem.setCursor(strPath + "/cursor.png", 0, 0);
			};
			CursorShow = function() {
				webOSSystem.setCursor("default");
			};	
		} else if(arrVersion[0] > 2) {
			CursorHide = function() {
				PalmSystem.setCursor(strPath + "/cursor.png", 0, 0);
			};
			CursorShow = function() {
				PalmSystem.setCursor("default");
			};	
		} else {
			//CursorHide = function() {};
			//CursorShow = function() {};	
		}
		if(arrVersion[0] > 4) {
			CursorVisible = function() {
				return webOSSystem.cursor.visibility === true;
			};
		} else if(arrVersion[0] > 2) {
			CursorVisible = function() {
				return PalmSystem.cursor.visibility === true;
			};
		} else {
			CursorVisible = function() {
				return false;
			};
		}

		SubscriptionLog();
		SubscriptionInputStatus();
		SubscriptionGetSensorData();
		SubscriptionDomEvent();
		if(bOverlay) {
			SubscriptionClose();
			LaunchInput();
		} else {
			SubscriptionScreenSaverRequest();
			var deSource = document.createElement("source");
			deSource.setAttribute("src", strInputSource);
			deSource.setAttribute("type", "service/webos-external");
			deVideo.appendChild(deSource);
		}
	}
}
