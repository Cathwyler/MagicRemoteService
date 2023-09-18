
const oStringDefault = {
	strAppTittle: "MagicRemoteService",
	strAppDescription: "Service providing computer remote control using a LG WebOS TV",

	strLogTitle: "Log",
	strWarnTitle: "Warn",
	strErrorTitle: "Error",

	strLaunchAppSuccess: "Success to launch {1}",
	strLaunchAppFailure: "Failed to launch {1}",
	strGetAllInputStatusSubscribe: "Start to get all input status",
	strGetAllInputStatusFailure: "Failed to get all input status",
	strSendWolSuccess: "Success to send WoL",
	strSendWolFailure: "Failed to send WoL",
	strSendPositionSuccess: "Success to send position",
	strSendPositionFailure: "Failed to send position",
	strSendWheelSuccess: "Success to send wheel",
	strSendWheelFailure: "Failed to send wheel",
	strSendVisibleSuccess: "Success to send visible",
	strSendVisibleFailure: "Failed to send visible",
	strSendKeySuccess: "Success to send key",
	strSendKeyFailure: "Failed to send key",
	strSendUnicodeSuccess: "Success to send unicode",
	strSendUnicodeFailure: "Failed to send unicode",
	strSendShutdownSuccess: "Success to send shutdown",
	strSendShutdownFailure: "Failed to send shutdown",
	strAddDeviceSuccess: "Sucess to add device",
	strAddDeviceFailure: "Failed to add device",
	strRegisterScreenSaverRequestSubscribe: "Start to register screensaver request",
	strRegisterScreenSaverRequestFailure: "Failed to register screensaver request",
	strResponseScreenSaverRequestSuccess: "Sucess to response screensaver request",
	strResponseScreenSaverRequestFailure: "Failed to response screensaver request",
	strGetSensorDataSubscribe: "Start to get sensor data",
	strGetSensorDataFailure: "Failed to get sensor data",

	strActionUnprocessed: "Unprocessed action message",

	strInputDisconect: "The remote computer appears to be turned off. Do you want to start it?",
	strInputDisconectStart: "Start",
	strInputDisconectWakeOnLanFailure: "The remote computer appears to be off and unresponsive to Wake on Lan or appears to be unplugged. Please check the proper functioning of Wake on Lan as well as the video connection",
	strSocketConnecting: "Connection to the server",
	strSocketConnectingTimeout: "The remote computer appears to be on but the service is not started or cannot be reached. Please check the proper functioning of the service on the remote computer as well as your network settings",
	strSocketErrorOpen: "Socket still open",
	strSocketErrorClosed: "Socket already closed",
	strSocketDisconnecting: "Disconnect from the server",
	strShutdownMessage: "Do you want to shutdown the remote computer?",
	strShutdownShutdown: "Shutdown",
	strShutdownAbort: "Do nothing"
};

const oStringfr = {
	strAppTittle: "MagicRemoteService",
	strAppDescription: "Service permettant de contrôler un ordinateur à l'aide d'une TV WebOS LG",

	strLogTitle: "Evenement",
	strWarnTitle: "Attention",
	strErrorTitle: "Erreur",

	strLaunchAppSuccess: "Succès du lancement de {1}",
	strLaunchAppFailure: "Echec du lancement de {1}",
	strGetAllInputStatusSubscribe: "Début de la récupération l'état des entrées",
	strGetAllInputStatusFailure: "Echec de la récupération l'état des entrées",
	strSendWolSuccess: "Succès de l'envoi du WoL",
	strSendWolFailure: "Echec de l'envoi du WoL",
	strSendPositionSuccess: "Success de l'envoi de la position",
	strSendPositionFailure: "Echec de l'envoi de la position",
	strSendWheelSuccess: "Succès de l'envoi de la roulette",
	strSendWheelFailure: "Echec de l'envoi de la roulette",
	strSendVisibleSuccess: "Succès de l'envoi de la visibilité",
	strSendVisibleFailure: "Echec de l'envoi de la visibilité",
	strSendKeySuccess: "Succès de l'envoi de la touche",
	strSendKeyFailure: "Echec de l'envoi de la touche",
	strSendUnicodeSuccess: "Succès de l'envoi du caractére unicode",
	strSendUnicodeFailure: "Echec de l'envoi du caractére unicode",
	strSendShutdownSuccess: "Succès de l'envoi de l'arret",
	strSendShutdownFailure: "Echec de l'envoi de l'arret",
	strAddDeviceSuccess: "Succès de l'ajout de l'appareil",
	strAddDeviceFailure: "Echec de l'ajout de l'appareil",
	strRegisterScreenSaverRequestSubscribe: "Début de l'abonnement à la demande d'écran de veille",
	strRegisterScreenSaverRequestFailure: "Echec de l'abonnement à la demande d'écran de veille",
	strResponseScreenSaverRequestSuccess: "Succès de la réponse à la demande d'écran de veille",
	strResponseScreenSaverRequestFailure: "Echec de la réponse à la demande d'écran de veille",
	strGetSensorDataSubscribe: "Début de la récupération des donnéees du capteur",
	strGetSensorDataFailure: "Echec de la récupération des donnéees du capteur",

	strActionUnprocessed: "Message d'action non traité",

	strInputDisconect: "L'ordinateur distant semble être éteint. Voulez-vous le demarrer ?",
	strInputDisconectStart: "Demarrer",
	strInputDisconectWakeOnLanFailure: "L'ordinateur distant semble être éteint et ne pas réagir au Wake on Lan ou semble être débranché. Veuillez vérifier le bon fonctionnement du Wake on Lan ainsi que de la connexion vidéo",
	strSocketConnecting: "Connexion au serveur",
	strSocketConnectingTimeout: "L'ordinateur distant semble être allumé mais le service n'est pas démarré ou n'est pas joignable. Veuillez vérifier le bon fonctionnement du service sur l'ordinateur distant ainsi que vos paramètres réseaux",
	strSocketErrorOpen: "Socket encore ouverte",
	strSocketErrorClosed: "Socket déjà fermée",
	strSocketDisconnecting: "Deconnexion du serveur",
	strShutdownMessage: "Voulez-vous éteindre l'ordinateur distant ?",
	strShutdownShutdown: "Éteindre",
	strShutdownAbort: "Ne rien faire"
};

function ObjectSpread(o1, o2){
	var o = o1;
	for (var strProperty in o) {
		if(strProperty in o2) {
			o[strProperty] = o2[strProperty];
		}
	}
	return o;
}

function GetString(){
	switch(window.PalmSystem.locale) {
		case "fr":
		case "fr-BE":
		case "fr-CA":
		case "fr-CH":
		case "fr-FR":
		case "fr-LU":
		case "fr-MC":
			return ObjectSpread(oStringDefault, oStringfr);
		default:
			return oStringDefault;
	}
}
