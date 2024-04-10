
var Service = require("webos-service");
var Dgram = require("dgram");

var bDebug = false;
var bOverlay = true;
var strAppId = "com.cathwyler.magicremoteservice";

var serService = new Service(strAppId + ".service"); 

var arrLog = [];
var dLog = {};
var metLog = serService.register("log");
metLog.on("request", function(mMessage) {
	try {
		if (mMessage.isSubscription) {
			dLog[mMessage.uniqueToken] = mMessage;
			mMessage.respond({
				subscribed: true,
				returnValue: true
			});
		} else if (!(arrLog.length > 0)) {
			mMessage.respond({
				returnValue: true
			});
		}
		while (arrLog.length > 0) {
			mMessage.respond({
				log: arrLog.shift(),
				returnValue: true
			});
		}
	} catch(eError) {
		mMessage.respond({
			errorCode: "1",
			errorText: eError.message,
			returnValue: false
		});
	}
}); 
metLog.on("cancel", function(mMessage) { 
	delete dLog[mMessage.uniqueToken]; 
});

Object.prototype.toString = function() {
	var arrAncestor = [];
	return JSON.stringify(this, function(k, o) {
		if (typeof o !== "object" || o === null) {
			return o;
		} else {
			while (arrAncestor.length > 0 && arrAncestor[arrAncestor.length - 1] !== this) {
				arrAncestor.pop();
			}
			if (arrAncestor.indexOf(o) !== -1 ) {
				return "[Circular]";
			}
			arrAncestor.push(o);
			return o;
		}
	});
};
function SendLog() {
	try {
		if(Object.keys(dLog).length > 0){
			while (arrLog.length > 0) {
				var oLog = arrLog.shift();
				for(var uniqueToken in dLog) {
					dLog[uniqueToken].respond({
						log: oLog,
						returnValue: true
					});
				}
			}
		}
	} catch(eError) {
		for(var uniqueToken in dLog) {       
			dLog[uniqueToken].respond({
				errorCode: "1",
				errorText: eError.message,
				returnValue: false
			});
		}
	}
}

function Log() {
	arrLog.push({
		bConsole: false,
		iType: 0,
		strMessage: Array.prototype.slice.call(arguments).map(function(x) {
		if (typeof o !== "object" || o === null) {
			return x;
		} else {
			return x.toString();
		}
	}).join("")});
	SendLog();
}

function LogIfDebug() {};
if(bDebug) {
	LogIfDebug = function() {
		Log.apply(this, arguments);
	}
}

function Warn() {
	arrLog.push({
		bConsole: false,
		iType: 1,
		strMessage: Array.prototype.slice.call(arguments).map(function(x) {
		if (typeof o !== "object" || o === null) {
			return x;
		} else {
			return x.toString();
		}
	}).join("")});
	SendLog();
}

function Error() {
	arrLog.push({
		bConsole: false,
		iType: 2,
		strMessage: Array.prototype.slice.call(arguments).map(function(x) {
		if (typeof o !== "object" || o === null) {
			return x;
		} else {
			return x.toString();
		}
	}).join("")});
	SendLog();
}

function ConsoleLog() {
	arrLog.push({
		bConsole: true,
		iType: 0,
		strMessage: Array.prototype.slice.call(arguments).map(function(x) {
		if (typeof o !== "object" || o === null) {
			return x;
		} else {
			return x.toString();
		}
	}).join("")});
	SendLog();
}

function ConsoleWarn() {
	arrLog.push({
		bConsole: true,
		iType: 1,
		strMessage: Array.prototype.slice.call(arguments).map(function(x) {
		if (typeof o !== "object" || o === null) {
			return x;
		} else {
			return x.toString();
		}
	}).join("")});
	SendLog();
}

function ConsoleError() {
	arrLog.push({
		bConsole: true,
		iType: 2,
		strMessage: Array.prototype.slice.call(arguments).map(function(x) {
		if (typeof o !== "object" || o === null) {
			return x;
		} else {
			return x.toString();
		}
	}).join("")});
	SendLog();
}

var socClient = Dgram.createSocket("udp4");
socClient.on("listening", function () {
	socClient.setBroadcast(true);
});

var bufWol = new Buffer(102);
bufWol.fill(0xFF, 0, 6);
var metWol = serService.register("wol");
metWol.on("request", function(mMessage) {
	try {
		for (var iWol = 6, iMac = 0; iWol < bufWol.length; iWol++, iMac++) {
			bufWol[iWol] = mMessage.payload.mMac.arrMac[iMac % mMessage.payload.mMac.arrMac.length];
		}
		socClient.send(bufWol, 0, bufWol.length, 9, mMessage.payload.strBroadcast);
		
		mMessage.respond({
			strBuffer: bufWol.toString("hex"),
			returnValue: true
		});
	} catch(eError) {
		mMessage.respond({
			errorCode: "1",
			errorText: eError.message,
			returnValue: false
		});
	}
});

if(bOverlay){
	var strInputAppId = "com.webos.app.hdmi";
	
	serService.activityManager.create("MagicRemoteServiceKeepAlive");

	var dClose = {};
	var metClose = serService.register("close");
	metClose.on("request", function(mMessage) {
		try {
			if (mMessage.isSubscription) {
				dClose[mMessage.uniqueToken] = mMessage;
				mMessage.respond({
					subscribed: true,
					returnValue: true
				});
			} else {
				LogIfDebug("Subscribe auto launch callback");
				mMessage.respond({
					returnValue: true
				});
			}
		} catch(eError) {
			mMessage.respond({
				errorCode: "1",
				errorText: eError.message,
				returnValue: false
			});
		}
	}); 
	metClose.on("cancel", function(mMessage) { 
		delete dClose[mMessage.uniqueToken]; 
	});

	var subAutoLaunch = serService.subscribe("luna://com.palm.activitymanager/create", {
		activity: {
			name: "MagicRemoteServiceAutoLaunch",
			description: "MagicRemoteService auto launch",
			type: {
				foreground: true,
				persist: true,
				explicit: true
			}, schedule: {
				precise: true,
				interval: "00d00h00m05s"
			}, callback: {
				method: "luna://" + strAppId + ".service/close",
				params: {}
			}
		},
		subscribe: true,
		detailedEvents: true, //WebOS 3
		start: true,
		replace: true
	});
	subAutoLaunch.on("response", function(mMessage) {
		try {
			if(mMessage.payload.returnValue) {
				switch(mMessage.payload.event) {
					case undefined:
						LogIfDebug("Subscribe auto launch");
						break;
					case "start":
						LogIfDebug("Subscribe auto launch start");
						serService.call("luna://com.palm.activitymanager/complete", {
							activityId: mMessage.payload.activityId,
							restart: true
						});
						break;
					default:
						LogIfDebug("Subscribe auto launch ", mMessage.payload.event);
						break;
				}
			} else {
				Error("Subscribe auto launch response error [", mMessage.payload.errorText, "]");
			}
		} catch(eError) {
			Error("Subscribe auto launch response error [", eError, "]");
		}
	});

	var Http = require("http");
	var Crypto = require("crypto");
	var Fs = require("fs");

	var bApp = false;
	var strSsapClientKey;
	Fs.readFile("./SsapClientKey", { encoding: "utf8" }, function(eError, strData){
		if(eError) {
			ConsoleError("readFile error [", eError, "]");
		}
		strSsapClientKey = strData;
		SsapLaunch();
	});

	function SsapLaunch() {
		var strSsapWebSocketClientKey = new Buffer("13-" + Date.now()).toString("base64");
		var haSsapWebSocketShaSum = Crypto.createHash("sha1");
		haSsapWebSocketShaSum.update(strSsapWebSocketClientKey + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11");

		var hrSsap = Http.request({
			host: "127.0.0.1",
			port: 3000,
			headers: {
				Connection: "Upgrade",
				Upgrade: "websocket",
				"Sec-WebSocket-Version": 13,
				"Sec-WebSocket-Key": strSsapWebSocketClientKey
			}
		});
		hrSsap.end();
		hrSsap.on("upgrade", function(hsrSsap, socSsap, hSsap) {
			if (hsrSsap.headers["sec-websocket-accept"] !== haSsapWebSocketShaSum.digest("base64")) {
				Error("Ssap invalid server key");
				socSsap.end();
			} else {
				socSsap.setTimeout(0);
				socSsap.setNoDelay(true);
				socSsap.on("close", function() {
					LogIfDebug("Ssap close");
					setTimeout(function() {
						SsapLaunch();
					}, 5000);
				});
				socSsap.on("data", function(bufStream) {
					try{
						UnframeData(bufStream, function(bufFrame, bFin, bRsv1, bRsv2, bRsv3, ucOpcode, bufMask, bufData) {
							if(!bFin) {
								Error("Ssap unable to process split frame", bufFrame);
							} else {
								switch(ucOpcode) {
									case 0x0: //Continuation
										Error("Ssap unable to process split frame", bufFrame);
										break;
									case 0x1: //Text
										if(bufMask !== undefined) {
											Unmask(bufMask, bufData);
										}
										var mMessage = JSON.parse(bufData.toString("utf8"));
										LogIfDebug("Ssap data text", mMessage);
										switch(mMessage.id) {
											case "0":
												switch(mMessage.type) {
													case "registered":
														strSsapClientKey = mMessage.payload["client-key"];
														Fs.writeFile("./SsapClientKey", strSsapClientKey, { encoding: "utf8" }, function(eError) {
															if(eError) {
																ConsoleError("writeFile error [", eError, "]");
															}
														});
														socSsap.write(FrameData(JSON.stringify({
															type: "subscribe",
															uri: "ssap://com.webos.applicationManager/getForegroundAppInfo",
															id: "1",
															payload: {}
														}), true, false, false, false, 0x1, 0), "binary");
														break;
												}
												break;
											case "1":
												switch(mMessage.type) {
													case "response":
														switch(mMessage.payload.appId) {
															case strInputAppId:
																LogIfDebug("Get foreground app info launch");
																bApp = true;
																serService.call("luna://com.webos.applicationManager/launch", {
																	id: strAppId
																});
																break;
															default:
																if(bApp) {
																	LogIfDebug("Get foreground app info close");
																	bApp = false;
																	for(var uniqueToken in dClose) {
																		dClose[uniqueToken].respond({
																			returnValue: true
																		});
																	} 
																}
																break;
														}
														break;
												}
												break;
										}
										break;
									case 0x2: //Binary
										Error("Ssap unable to process binary frame", bufFrame);
										break;
									case 0x8: //Close
										LogIfDebug("Ssap close frame", bufFrame);
										strSsapClientKey = "";
										break;
									case 0x9: //Ping
										LogIfDebug("Ssap data Ping", bufFrame);
										bufFrame[0] = (bufFrame[0] & 0xF0) | (0x0A & 0x0F);
										socSsap.write(bufFrame, "binary");
										break;
									case 0xA: //Pong
										Error("Ssap unable to process pong frame", bufFrame);
										break;
									default:
										Error("Ssap unprocessed message", bufFrame);
										break;
								}
							}
						});
					} catch(eError) {
						Error("data error [", eError, "]");
					}
				});
				LogIfDebug("Ssap open");
				try{
					socSsap.write(FrameData(JSON.stringify({
						type: "register",
						id: "0",
						payload: {
							forcePairing: false,
							pairingType: "PROMPT",
							manifest: {
								permissions: [
									"READ_RUNNING_APPS"
								]
							},
							"client-key": strSsapClientKey
						}
					}), true, false, false, false, 0x1, 0), "binary");
				} catch(eError) {
					Error("Ssap open error [", eError, "]");
				}
			}
		});
	}

	function UnframeData(bufStream, fCallback) {
		var ulLenStream = bufStream.length;
		var ulOffsetFrame = 0;
		while(!(ulOffsetFrame == ulLenStream)) {
			var bFin = (bufStream[ulOffsetFrame] & 0x80) == 0x80;
			var bRsv1 = (bufStream[ulOffsetFrame] & 0x40) == 0x40;
			var bRsv2 = (bufStream[ulOffsetFrame] & 0x20) == 0x20;
			var bRsv3 = (bufStream[ulOffsetFrame] & 0x10) == 0x10;
			var ucOpcode = bufStream[ulOffsetFrame] & 0x0F;

			var bMask = (bufStream[ulOffsetFrame + 1] & 0x80) == 0x80;
			var ulLenData;
			var ulOffsetMask;
			if((bufStream[ulOffsetFrame + 1] & 0x7F) == 0x7F) {
				ulLenData = bufStream.readUInt32BE(ulOffsetFrame + 2); //Truncated no 64bit
				ulOffsetMask = ulOffsetFrame + 10;
			} else if((bufStream[ulOffsetFrame + 1] & 0x7F) == 0x7E) {
				ulLenData = bufStream.readUInt16BE(ulOffsetFrame + 2);
				ulOffsetMask = ulOffsetFrame + 4;
			} else {
				ulLenData = bufStream[ulOffsetFrame + 1] & 0x7F;
				ulOffsetMask = ulOffsetFrame + 2;
			}
			var bufMask;
			var ulOffsetData;
			if(bMask) {
				bufMask = bufStream.slice(ulOffsetMask, ulOffsetMask + 4);
				ulOffsetData = ulOffsetMask + 4;
			} else {
				bufMask = undefined;
				ulOffsetData = ulOffsetMask;
			}
			var bufData = bufStream.slice(ulOffsetData, ulOffsetData + ulLenData);
			var bufFrame = bufStream.slice(ulOffsetFrame, ulOffsetData + ulLenData);
			fCallback(bufFrame, bFin, bRsv1, bRsv2, bRsv3, ucOpcode, bufMask, bufData);
			ulOffsetFrame = bufFrame.length;
		}
	}

	function FrameData(strData, bFin, bRsv1, bRsv2, bRsv3, ucOpcode, ulMask) {
		var ulLenData = Buffer.byteLength(strData);
		var ulOffsetMask;
		if(ulLenData > 0xFFFF) {
			ulOffsetMask = 10;
		} else if(ulLenData > 0x7D) {
			ulOffsetMask = 4;
		} else {
			ulOffsetMask = 2;
		}
		var ulOffsetData;
		if(ulMask) {
			ulOffsetData = ulOffsetMask + 4;
		} else {
			ulOffsetData = ulOffsetMask;
		}
		bufData = new Buffer(ulOffsetData + ulLenData);
		bufData[0] = (bFin ? 0x80 : 0x00);
		bufData[0] |= (bRsv1 ? 0x40 : 0x00);
		bufData[0] |= (bRsv2 ? 0x20 : 0x00);
		bufData[0] |= (bRsv3 ? 0x10 : 0x00);
		bufData[0] |= (ucOpcode & 0x0F);

		bufData[1] = (ulMask > 0 ? 0x80 : 0x00);
		if(ulLenData > 0xFFFF) {
			bufData[1] |= 0x7F;
			bufData.writeUInt32BE(ulLenData, 2);
			bufData.fill(0x00, 6, 10);
		} else if(ulLenData > 0x7D) {
			bufData[1] |= 0x7E;
			bufData.writeUInt16BE(ulLenData, 2);
		} else {
			bufData[1] |= (ulLenData & 0x7F);
		}
		if(ulMask > 0) {
			bufData.writeUInt32BE(ulMask, ulOffsetMask);
		}
		bufData.write(strData, ulOffsetData, ulLenData, "utf8");
		return bufData;
	}

	function Unmask(bufMask, bufData) {
		for(var ul = 0; ul < bufData.length; ul++) {
			bufData[ul] ^= bufMask[ul % 4];
		}
		return bufData;
	}
}