
function startInterval(fCallback, iMs) {
	fCallback();
	return setInterval(fCallback, iMs);
}

const bOverlay = true;
const strInputAppId = "com.webos.app.hdmi";
const strAppId = "com.cathwyler.magicremoteservice";

var oPackage = require("./package.json");
var Service = require("webos-service");
var Dgram = require("dgram");

var serService = new Service(oPackage.name); 

var socClient = Dgram.createSocket("udp4");
socClient.bind(function() {
	socClient.setBroadcast(true);
});

var bufWol = Buffer.alloc(102);
bufWol.fill(0xFF, 0, 6);
serService.register("wol", function(mMessage) {
	try {
		bufWol.fill(Buffer.from(mMessage.payload.mMac.tabMac), 6);
		socClient.send(bufWol, 9, mMessage.payload.strBroadcast);
		
		mMessage.respond({
			strBuffer: bufWol.toString("hex"),
			returnValue: true
		});
	} catch(eError) {
		mMessage.respond({
			eError: eError,
			returnValue: false
		});
	}
});

if(bOverlay){
	var aKeepAlive;
	serService.activityManager.create("aKeepAlive", function(a) {
		aKeepAlive = a; 
	});

	var subClose = [];
	var regClose = serService.register("close");
	regClose.on("request", function(mMessage) {
		try {
			if (mMessage.isSubscription) {
				subClose[mMessage.uniqueToken] = mMessage;
			}
			mMessage.respond({
				subscribed: true,
				returnValue: true
			});
		} catch(eError) {
			mMessage.respond({
				eError: eError,
				returnValue: false
			});
		}
	}); 
	regClose.on("cancel", function(mMessage) { 
		delete subClose[mMessage.uniqueToken]; 
	});


	var bApp = false;
	var iIntervalLaunch = 0;
	var registerInputSub = serService.subscribe("luna://com.webos.service.videooutput/getStatus", { subscribe: true });
	registerInputSub.on("response", function(mMessage) {
		try {
			if(mMessage.payload.video[0].connected === true && mMessage.payload.video[0].appId === strInputAppId) {
				if(!bApp){
					bApp = true;
					iIntervalLaunch = startInterval(function(){
						serService.call("luna://com.webos.applicationManager/launch", {
							id: strAppId
						}, function(mMessage) {
							if(mMessage.payload.returnValue){
								if(iIntervalLaunch) {
									clearInterval(iIntervalLaunch);
									iIntervalLaunch = 0;
								}
							}
						});
					}, 1000);
				}
			} else {
				if(bApp){
					bApp = false;
					if(iIntervalLaunch) {
						clearInterval(iIntervalLaunch);
						iIntervalLaunch = 0;
					}
					for(var uniqueToken in subClose) {
						subClose[uniqueToken].respond({
							returnValue: true
						});
					} 
				}
			}
		} catch(eError) {
			//SendLog(eError.message);
		}
	});
}

/*var subLog = [];
var regLog = serService.register("log");
regLog.on("request", function(mMessage) {
	try {
		if (mMessage.isSubscription) {
			subLog[mMessage.uniqueToken] = mMessage;
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
regLog.on("cancel", function(mMessage) { 
    delete subLog[mMessage.uniqueToken]; 
});

Object.prototype.toString = function() {
	return JSON.stringify(this);
};
function SendLog(){
	try {
		var log = Array.prototype.slice.call(arguments).map(function(x) {
			return x.toString();
		}).join("");
		for(var uniqueToken in subLog) {       
			subLog[uniqueToken].respond({
				log: log,
				returnValue: true
			});
		}
	} catch(eError) {
		for(var uniqueToken in subLog) {       
			subLog[uniqueToken].respond({
				eError: eError.message,
				returnValue: false
			});
		}
	}
}*/

