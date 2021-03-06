
const oStringDefault = {
	strAppTittle: "Magic remote service",
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
	strSendMousePositionSuccess: "Success to send mouse position",
	strSendMousePositionFailure: "Failed to send mouse position",
	strSendMouseKeySuccess: "Success to send mouse key",
	strSendMouseKeyFailure: "Failed to send mouse key",
	strSendMouseWheelSuccess: "Success to send mouse wheel",
	strSendMouseWheelFailure: "Failed to send mouse wheel",
	strSendMouseVisibleSuccess: "Success to send mouse visible",
	strSendMouseVisibleFailure: "Failed to send mouse visible",
	strSendKeyboardKeySuccess: "Success to send keyboard key",
	strSendKeyboardKeyFailure: "Failed to send keyboard key",
	strSendKeyboardUnicodeSuccess: "Success to send keyboard unicode",
	strSendKeyboardUnicodeFailure: "Failed to send keyboard unicode",
	strSendShutdownSuccess: "Success to send shutdown",
	strSendShutdownFailure: "Failed to send shutdown",
	strGetSensorDataSubscribe: "Start to get sensor data",
	strGetSensorDataFailure: "Failed to get sensor data",
	strAddDeviceSuccess: "Sucess to add device",
	strAddDeviceFailure: "Failed to add device",

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
	strShutdownAbort: "Do nothing",
	strStartMessage: "Do you want to start the remote computer?",
	strStartStart: "Start",
	strStartAbort: "Do nothing"
};

const oStringfr = {
	strAppTittle: "Magic remote service",
	strAppDescription: "Service permettant de contr??ler un ordinateur ?? l'aide d'une TV WebOS LG",

	strLogTitle: "Evenement",
	strWarnTitle: "Attention",
	strErrorTitle: "Erreur",

	strLaunchAppSuccess: "Succ??s du lancement de {1}",
	strLaunchAppFailure: "Echec du lancement de {1}",
	strGetAllInputStatusSubscribe: "D??but de la r??cup??ration l'??tat des entr??es",
	strGetAllInputStatusFailure: "Echec de la r??cup??ration l'??tat des entr??es",
	strSendWolSuccess: "Succ??s de l'envoi du WoL",
	strSendWolFailure: "Echec de l'envoi du WoL",
	strSendMousePositionSuccess: "Success de l'envoi de la position de la souris",
	strSendMousePositionFailure: "Echec de l'envoi de la position de la souris",
	strSendMouseKeySuccess: "Succ??s de l'envoi de la touche de la souris",
	strSendMouseKeyFailure: "Echec de l'envoi de la touche de la souris",
	strSendMouseWheelSuccess: "Succ??s de l'envoi de la roulette de la sourie",
	strSendMouseWheelFailure: "Echec de l'envoi de la roulette de la sourie",
	strSendMouseVisibleSuccess: "Succ??s de l'envoi de la visibilit?? de la sourie",
	strSendMouseVisibleFailure: "Echec de l'envoi de la visibilit?? de la sourie",
	strSendKeyboardKeySuccess: "Succ??s de l'envoi de la touche du clavier",
	strSendKeyboardKeyFailure: "Echec de l'envoi de la touche du clavier",
	strSendKeyboardUnicodeSuccess: "Succ??s de l'envoi du caract??re unicode du clavier",
	strSendKeyboardUnicodeFailure: "Echec de l'envoi du caract??re unicode du clavier",
	strSendShutdownSuccess: "Succ??s de l'envoi de l'arret",
	strSendShutdownFailure: "Echec de l'envoi de l'arret",
	strGetSensorDataSubscribe: "D??but de la r??cup??ration des donn??ees du capteur",
	strGetSensorDataFailure: "Echec de la r??cup??ration des donn??ees du capteur",
	strAddDeviceSuccess: "Succ??s de l'ajout de l'appareil",
	strAddDeviceFailure: "Echec de l'ajout de l'appareil",

	strInputDisconect: "L'ordinateur distant semble ??tre ??teint. Voulez-vous le demarrer ?",
	strInputDisconectStart: "Demarrer",
	strInputDisconectWakeOnLanFailure: "L'ordinateur distant semble ??tre ??teint et ne pas r??agir au Wake on Lan ou semble ??tre d??branch??. Veuillez v??rifier le bon fonctionnement du Wake on Lan ainsi que de la connexion vid??o",
	strSocketConnecting: "Connexion au serveur",
	strSocketConnectingTimeout: "L'ordinateur distant semble ??tre allum?? mais le service n'est pas d??marr?? ou n'est pas joignable. Veuillez v??rifier le bon fonctionnement du service sur l'ordinateur distant ainsi que vos param??tres r??seaux",
	strSocketErrorOpen: "Socket encore ouverte",
	strSocketErrorClosed: "Socket d??j?? ferm??e",
	strSocketDisconnecting: "Deconnexion du serveur",
	strShutdownMessage: "Voulez-vous ??teindre l'ordinateur distant ?",
	strShutdownShutdown: "Eteindre",
	strShutdownAbort: "Ne rien faire",
	strStartMessage: "Voulez-vous demarrer l'ordinateur distant ?",
	strStartStart: "Demarrer",
	strStartAbort: "Ne rien faire"
};

function GetString(){
	switch(window.PalmSystem.locale) {
		case "fr":
		case "fr-BE":
		case "fr-CA":
		case "fr-CH":
		case "fr-FR":
		case "fr-LU":
		case "fr-MC":
			return {
				...oStringDefault,
				...oStringfr
			};
			break;
		default:
			return oStringDefault;
			break;
	}
}
