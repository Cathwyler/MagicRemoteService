
var pkgInfo = require("./package.json");
var Service = require("webos-service");
var Dgram = require("dgram");

var serService = new Service(pkgInfo.name);

/*serService.register("keepalive", function(mMessage) {
	try {
		mMessage.respond({
			returnValue: true
		});
	} catch(eError) {
		mMessage.respond({
			eError: eError,
			returnValue: false
		});
	}
});*/

var socClient = Dgram.createSocket("udp4");
socClient.bind(function() {
	socClient.setBroadcast(true);
});

var bufWol = Buffer.alloc(102);
bufWol.fill(0xFF, 0, 6);

serService.register("wol", function(mMessage) {
	try {
		bufWol.fill(Buffer.from(mMessage.payload.mMac.tabMac), 6);
		socClient.send(bufWol, 9, mMessage.payload.sBroadcast);
		
		mMessage.respond({
			sBuffer: bufWol.toString("hex"),
			returnValue: true
		});
	} catch(eError) {
		mMessage.respond({
			eError: eError,
			returnValue: false
		});
	}
});
