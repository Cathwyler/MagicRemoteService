
function startInterval(fCallback, iMs) {
	fCallback();
	return setInterval(fCallback, iMs);
}

var bOverlay = true;
var strServiceId = "com.cathwyler.magicremoteservice.send";
var strInputAppId = "com.webos.app.hdmi";
var strAppId = "com.cathwyler.magicremoteservice";

var Service = require("webos-service");
var Dgram = require("dgram");

var serService = new Service(strServiceId); 

var socClient = Dgram.createSocket("udp4");
socClient.bind(function() {
	socClient.setBroadcast(true);
});

var bufWol = Buffer.alloc(102);
bufWol.fill(0xFF, 0, 6);
serService.register("wol", function(mMessage) {
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
	var aKeepAlive;
	serService.activityManager.create("aKeepAlive", function(a) {
		aKeepAlive = a; 
	});

	var dClose = {};
	serService.register("close", function(mMessage) {
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
	}, function(mMessage) { 
		delete dClose[mMessage.uniqueToken]; 
	});


	var bApp = false;
	var iIntervalLaunch = 0;
	var subInputStatus = serService.subscribe("luna://com.webos.service.videooutput/getStatus", { subscribe: true });
	subInputStatus.on("response", function(mMessage) {
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
}
