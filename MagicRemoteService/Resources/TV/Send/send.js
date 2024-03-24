
var Service = require("webos-service");
var Dgram = require("dgram");

var bOverlay = true;
var strServiceId = "com.cathwyler.magicremoteservice.send";

var serService = new Service(strServiceId); 

var socClient = Dgram.createSocket("udp4");
socClient.bind(function() {
	socClient.setBroadcast(true);
});

var bufWol = Buffer.alloc(102);
bufWol.fill(0xFF, 0, 6);
var metWol = serService.register("wol");
metWol.on("request", function(mMessage) {
	try {
		bufWol.fill(Buffer.from(mMessage.payload.mMac.arrMac), 6);
		socClient.send(bufWol, 9, mMessage.payload.strBroadcast);
		
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
	//var Service = require("webos-service");

	//var strServiceId = "com.cathwyler.magicremoteservice.auto";
	var strInputAppId = "com.webos.app.hdmi";
	var strAppId = "com.cathwyler.magicremoteservice";

	//var serService = new Service(strServiceId); 

	var dClose = {};
	var metclose = serService.register("close");
	metclose.on("request", function(mMessage) {
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
	metclose.on("cancel", function(mMessage) { 
		delete dClose[mMessage.uniqueToken]; 
	});

	var subAutoLaunch = serService.subscribe("luna://com.palm.activitymanager/create", {
		activity: {
			name: "MagicRemoteService auto launch",
			description: "MagicRemoteService auto launch",
			type: {
				foreground: true,
				probe: true,
				persist: true,
				explicit: true
			}, schedule: {
				precise: true,
				interval: "00d00h00m05s"
			}, callback: {
				method: "luna://" + strAppId + ".send/close",
				params: {}
			}
		},
		subscribe: true,
		detailedEvents: true,
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