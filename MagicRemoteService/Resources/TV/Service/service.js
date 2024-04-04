
var Service = require("webos-service");
var Dgram = require("dgram");

var bDebug = false;
var bOverlay = true;
var strAppId = "com.cathwyler.magicremoteservice";

var serService = new Service(strAppId + ".service"); 

function LogIfDebug() {};
if(bDebug){
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
	LogIfDebug = function() {
		try {
			arrLog.push(Array.prototype.slice.call(arguments).map(function(x) {
				return x.toString();
			}).join(""));
			if(Object.keys(dLog).length > 0){
				while (arrLog.length > 0) {
					var strLog = arrLog.shift();
					for(var uniqueToken in dLog) {
						dLog[uniqueToken].respond({
							log: strLog,
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
				LogIfDebug("Subscribe auto launch response error [", mMessage.payload.errorText, "]");
			}
		} catch(eError) {
			LogIfDebug("Subscribe auto launch response error [", eError, "]");
		}
	});
	subAutoLaunch.on("cancel", function(mMessage) {
	});

	var bApp = false;
	var subInputStatus = serService.subscribe("luna://com.webos.service.videooutput/getStatus", {
		subscribe: true
	});
	subInputStatus.on("response", function(mMessage) {
		try {
			if(mMessage.payload.returnValue){
				if(mMessage.payload.video[0].connected === true && mMessage.payload.video[0].appId === strInputAppId) {
					if(!bApp){
						bApp = true;
						serService.call("luna://com.webos.applicationManager/launch", {
							id: strAppId
						});
					}
				} else {
					if(bApp){
						bApp = false;
						for(var uniqueToken in dClose) {
							dClose[uniqueToken].respond({
								returnValue: true
							});
						} 
					}
				}
			} else {
				LogIfDebug("Subscribe input status response error [", mMessage.payload.errorText, "]");
			}
		} catch(eError) {
			LogIfDebug("Subscribe input status response error [", eError, "]");
		}
	});
	subInputStatus.on("cancel", function(mMessage) {
	});
}