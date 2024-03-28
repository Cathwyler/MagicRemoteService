
var Service = require("webos-service");
var Dgram = require("dgram");

var bDebug = false;
var bOverlay = true;
var strAppId = "com.cathwyler.magicremoteservice";

var serService = new Service(strAppId + ".service"); 

/*function SendLogIfDebug() {};
if(bDebug){
	var arrLog = [];
	var dLog = {};
	var metLog = serService.register("log");
	metLog.on("request", function(mMessage) {
		try {
			if (mMessage.isSubscription) {
				dLog[mMessage.uniqueToken] = mMessage;
			}
			mMessage.respond({
				subscribed: true,
				returnValue: true
			});
		} catch(eError) {
			mMessage.respond({
				eError: eError.message,
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
	SendLogIfDebug = function() {
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
					eError: eError.message,
					returnValue: false
				});
			}
		}
	}
}*/

var socClient = Dgram.createSocket("udp4");
socClient.on("listening", function () {
	socClient.setBroadcast(true);
});

var bufWol = new Buffer(102);
bufWol.fill(0xFF, 0, 6);
var metWol = serService.register("wol");
metWol.on("request", function(mMessage) {
	try {
		bufWol.fill(new Buffer(mMessage.payload.mMac.arrMac), 6);
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

	var aKeepAlive;
	serService.activityManager.create("aKeepAlive", function(a) {
		aKeepAlive = a; 
	});

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
			name: "MagicRemoteService auto launch",
			description: "MagicRemoteService auto launch",
			type: {
				foreground: true,
				persist: true
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
			switch(mMessage.payload.event) {
				case undefined:
					break;
				case "update":
					break;
				case "start":
					serService.call("luna://com.palm.activitymanager/complete", {
						activityId: mMessage.payload.activityId,
						restart: true
					});
					break;
				default:
					break;
			}
		} catch(eError) {
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
		} catch(eError) {
		}
	});
	subInputStatus.on("cancel", function(mMessage) {
	});
}