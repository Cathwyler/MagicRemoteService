
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
	strSendMousePositionSuccess: "Success de l'envoi de la position de la souris",
	strSendMousePositionFailure: "Echec de l'envoi de la position de la souris",
	strSendMouseKeySuccess: "Succès de l'envoi de la touche de la souris",
	strSendMouseKeyFailure: "Echec de l'envoi de la touche de la souris",
	strSendMouseWheelSuccess: "Succès de l'envoi de la roulette de la sourie",
	strSendMouseWheelFailure: "Echec de l'envoi de la roulette de la sourie",
	strSendMouseVisibleSuccess: "Succès de l'envoi de la visibilité de la sourie",
	strSendMouseVisibleFailure: "Echec de l'envoi de la visibilité de la sourie",
	strSendKeyboardKeySuccess: "Succès de l'envoi de la touche du clavier",
	strSendKeyboardKeyFailure: "Echec de l'envoi de la touche du clavier",
	strSendKeyboardUnicodeSuccess: "Succès de l'envoi du caractére unicode du clavier",
	strSendKeyboardUnicodeFailure: "Echec de l'envoi du caractére unicode du clavier",
	strSendShutdownSuccess: "Succès de l'envoi de l'arret",
	strSendShutdownFailure: "Echec de l'envoi de l'arret",
	strGetSensorDataSubscribe: "Début de la récupération des donnéees du capteur",
	strGetSensorDataFailure: "Echec de la récupération des donnéees du capteur",
	strAddDeviceSuccess: "Succès de l'ajout de l'appareil",
	strAddDeviceFailure: "Echec de l'ajout de l'appareil",

	strInputDisconect: "L'ordinateur distant semble être éteint. Voulez-vous le demarrer ?",
	strInputDisconectStart: "Demarrer",
	strInputDisconectWakeOnLanFailure: "L'ordinateur distant semble être éteint et ne pas réagir au Wake on Lan ou semble être débranché. Veuillez vérifier le bon fonctionnement du Wake on Lan ainsi que de la connexion vidéo",
	strSocketConnecting: "Connexion au serveur",
	strSocketConnectingTimeout: "L'ordinateur distant semble être allumé mais le service n'est pas démarré ou n'est pas joignable. Veuillez vérifier le bon fonctionnement du service sur l'ordinateur distant ainsi que vos paramètres réseaux",
	strSocketErrorOpen: "Socket encore ouverte",
	strSocketErrorClosed: "Socket déjà fermée",
	strSocketDisconnecting: "Deconnexion du serveur",
	strShutdownMessage: "Voulez-vous éteindre l'ordinateur distant ?",
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
