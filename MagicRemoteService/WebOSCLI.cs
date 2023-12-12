
namespace MagicRemoteService {

	[System.Serializable]
	public class WebOSCLIException : System.Exception {
		public WebOSCLIException() {
		}
		public WebOSCLIException(string strMessage) : base(strMessage) {
		}
		public WebOSCLIException(string strMessage, System.Exception eInner) : base(strMessage, eInner) {
		}
	}
	[System.Serializable]
	public class WebOSCLINotFoundException : MagicRemoteService.WebOSCLIException {
		public WebOSCLINotFoundException() : base(MagicRemoteService.Properties.Resources.WebOSCLINotFoundExceptionMessage) {
		}
	}
	public class IPAddressJsonConverter : System.Text.Json.Serialization.JsonConverter<System.Net.IPAddress> {
		public override bool CanConvert(System.Type objectType) {
			return (objectType == typeof(System.Net.IPAddress));
		}
		public override System.Net.IPAddress Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options) {
			return System.Net.IPAddress.Parse(reader.GetString());
		}
		public override void Write(System.Text.Json.Utf8JsonWriter writer, System.Net.IPAddress value, System.Text.Json.JsonSerializerOptions serializer) {
			writer.WriteStringValue(value.ToString());
		}
	}
	public class ShortJsonConverter : System.Text.Json.Serialization.JsonConverter<short> {
		public override bool CanConvert(System.Type objectType) {
			return (objectType == typeof(short));
		}
		public override short Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options) {
			return short.Parse(reader.GetString());
		}
		public override void Write(System.Text.Json.Utf8JsonWriter writer, short value, System.Text.Json.JsonSerializerOptions options) {
			writer.WriteStringValue(value.ToString());
		}
	}
	public class WebOSCLIDeviceInput {
		public string strId;
		public string strName;
		public string strSource;
		public string strAppId;
		public string strAppIdShort;
	}
	public class WebOSCLIDeviceInfo {
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.IPAddressJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("ip")]
		public System.Net.IPAddress iaIP;
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.ShortJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("port")]
		public ushort usPort;
		[System.Text.Json.Serialization.JsonPropertyName("user")]
		public string strUser;
	}
	public class WebOSCLIDeviceDetail {
		[System.Text.Json.Serialization.JsonPropertyName("platform")]
		public string strPlatform;
		[System.Text.Json.Serialization.JsonPropertyName("privatekey")]
		public string strPrivateKey;
		[System.Text.Json.Serialization.JsonPropertyName("passphrase")]
		public string strPassphrase;
		[System.Text.Json.Serialization.JsonPropertyName("description")]
		public string strDescription;
	}
	public class WebOSCLIDevice {

		[System.Text.Json.Serialization.JsonPropertyName("profile")]
		public string strProfile;
		[System.Text.Json.Serialization.JsonPropertyName("name")]
		public string strName;
		[System.Text.Json.Serialization.JsonPropertyName("default")]
		public bool bDefault;
		[System.Text.Json.Serialization.JsonPropertyName("deviceinfo")]
		public MagicRemoteService.WebOSCLIDeviceInfo wcdiDeviceInfo;
		[System.Text.Json.Serialization.JsonPropertyName("connection")]
		public string[] arrConnection;
		[System.Text.Json.Serialization.JsonPropertyName("details")]
		public MagicRemoteService.WebOSCLIDeviceDetail wcddDeviceDetail;
	}

	internal class WebOSCLIDeviceSet {

		[System.Text.Json.Serialization.JsonPropertyName("name")]
		public string strName;
		[System.Text.Json.Serialization.JsonPropertyName("description")]
		public string strDescription;
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.IPAddressJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("host")]
		public System.Net.IPAddress iaIP;
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.ShortJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("port")]
		public ushort usPort;
		[System.Text.Json.Serialization.JsonPropertyName("username")]
		public string strUser;
		[System.Text.Json.Serialization.JsonPropertyName("password")]
		public string strPassword;
		[System.Text.Json.Serialization.JsonPropertyName("privatekey")]
		public string strPrivateKey;
		[System.Text.Json.Serialization.JsonPropertyName("passphrase")]
		public string strPassphrase;
	}

	internal static class WebOSCLI {
		private static string GetWebOSCLIDir() {
			string strWebOSCliDir = System.Environment.GetEnvironmentVariable("WEBOS_CLI_TV");
			if(strWebOSCliDir == null) {
				throw new MagicRemoteService.WebOSCLINotFoundException();
			}
			return strWebOSCliDir;
		}
		private static string ExecWebOSCLICmd(string strCommand, string strArgument, System.Collections.Generic.Dictionary<ushort, string> dInput = null, string strWorkingDirectory = null) {
			System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
			pProcess.StartInfo.FileName = Application.CompleteDir(WebOSCLI.GetWebOSCLIDir()) + strCommand + ".cmd";
			if(!string.IsNullOrEmpty(strWorkingDirectory)) {
				pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;
			}
			pProcess.StartInfo.Arguments = strArgument;
			pProcess.StartInfo.UseShellExecute = false;
			pProcess.StartInfo.CreateNoWindow = true;
			pProcess.StartInfo.RedirectStandardInput = true;
			pProcess.StartInfo.RedirectStandardError = true;
			pProcess.StartInfo.RedirectStandardOutput = true;
			pProcess.Start();
			string strErr = "";
			string strOutput = "";
			ushort usOutputLine = 0;
			pProcess.ErrorDataReceived += delegate (object sender, System.Diagnostics.DataReceivedEventArgs e) {
				if(e.Data != null) {
					strErr += e.Data;
				}
			};
			pProcess.OutputDataReceived += delegate (object sender, System.Diagnostics.DataReceivedEventArgs e) {
				if(e.Data != null) {
#if DEBUG
					System.Console.WriteLine(e.Data);
#endif
					strOutput += e.Data;
					usOutputLine++;
					if(dInput != null && dInput.ContainsKey(usOutputLine)) {
						System.Threading.Tasks.Task.Run(async delegate () {
							await System.Threading.Tasks.Task.Delay(10);
							await pProcess.StandardInput.WriteLineAsync(dInput[usOutputLine]);
						});
					}
				}
			};
			pProcess.BeginErrorReadLine();
			pProcess.BeginOutputReadLine();
			pProcess.WaitForExit();
			if(pProcess.ExitCode != 0) {
				throw new MagicRemoteService.WebOSCLIException(strErr);
			}
			return strOutput;
		}
		public static MagicRemoteService.WebOSCLIDeviceInput[] InputList() {
			return new MagicRemoteService.WebOSCLIDeviceInput[] {
				new MagicRemoteService.WebOSCLIDeviceInput() { strId = "HDMI_1", strName = "HDMI 1", strSource = "ext://hdmi:1", strAppId = "com.webos.app.hdmi1", strAppIdShort = "hdmi1" },
				new MagicRemoteService.WebOSCLIDeviceInput() { strId = "HDMI_2", strName = "HDMI 2", strSource = "ext://hdmi:2", strAppId = "com.webos.app.hdmi2", strAppIdShort = "hdmi2" },
				new MagicRemoteService.WebOSCLIDeviceInput() { strId = "HDMI_3", strName = "HDMI 3", strSource = "ext://hdmi:3", strAppId = "com.webos.app.hdmi3", strAppIdShort = "hdmi3" },
				new MagicRemoteService.WebOSCLIDeviceInput() { strId = "HDMI_4", strName = "HDMI 4", strSource = "ext://hdmi:4", strAppId = "com.webos.app.hdmi4", strAppIdShort = "hdmi4" }
			};
		}
		public static MagicRemoteService.WebOSCLIDevice[] SetupDeviceList() {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-F"
			};
			return System.Text.Json.JsonSerializer.Deserialize<MagicRemoteService.WebOSCLIDevice[]>(MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-setup-device", string.Join(" ", tabArgument)));
		}
		public static void SetupDeviceAdd(MagicRemoteService.WebOSCLIDevice wcdDevice, string strPassword) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-a \"" + wcdDevice.strName + "\"",
				"-i \"" + System.Text.Json.JsonSerializer.Serialize<MagicRemoteService.WebOSCLIDeviceSet>(new MagicRemoteService.WebOSCLIDeviceSet {
					strName = wcdDevice.strName,
					strDescription = wcdDevice.wcddDeviceDetail.strDescription,
					iaIP = wcdDevice.wcdiDeviceInfo.iaIP,
					usPort = wcdDevice.wcdiDeviceInfo.usPort,
					strUser = wcdDevice.wcdiDeviceInfo.strUser,
					strPassword = strPassword,
					strPrivateKey = wcdDevice.wcddDeviceDetail.strPrivateKey,
					strPassphrase = wcdDevice.wcddDeviceDetail.strPassphrase
				}).Replace("\"", "'") + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-setup-device", string.Join(" ", tabArgument));
		}
		public static void SetupDeviceModify(string strDevice, MagicRemoteService.WebOSCLIDevice wcdDevice, string strPassword) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-m \"" + strDevice + "\"",
				"-i \"" + System.Text.Json.JsonSerializer.Serialize<MagicRemoteService.WebOSCLIDeviceSet>(new MagicRemoteService.WebOSCLIDeviceSet {
					strName = wcdDevice.strName,
					strDescription = wcdDevice.wcddDeviceDetail.strDescription,
					iaIP = wcdDevice.wcdiDeviceInfo.iaIP,
					usPort = wcdDevice.wcdiDeviceInfo.usPort,
					strUser = wcdDevice.wcdiDeviceInfo.strUser,
					strPassword = strPassword,
					strPrivateKey = wcdDevice.wcddDeviceDetail.strPrivateKey,
					strPassphrase = wcdDevice.wcddDeviceDetail.strPassphrase
				}).Replace("\"", "'") + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-setup-device", string.Join(" ", tabArgument));
		}
		public static void SetupDeviceRemove(string strDevice) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-r \"" + strDevice + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-setup-device", string.Join(" ", tabArgument));
		}
		public static void NovacomGetKey(string strDevice, string strPassphrase) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"-k"
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-novacom", string.Join(" ", tabArgument), new System.Collections.Generic.Dictionary<ushort, string>() { { 1, strPassphrase } });
		}
		public static void Package(string strOutDirectory, string strApplication, string strService = null, string strPackage = null) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"\"" + strApplication + "\"",
				"-n"
			};
			if(!string.IsNullOrEmpty(strService)) {
				tabArgument.Add("-o \"" + strOutDirectory + "\"");
			}
			if(!string.IsNullOrEmpty(strService)) {
				tabArgument.Add("\"" + strService + "\"");
			}
			if(!string.IsNullOrEmpty(strPackage)) {
				tabArgument.Add("\"" + strPackage + "\"");
			}
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-package", string.Join(" ", tabArgument));
		}
		public static void Install(string strDevice, string strPackageFile) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"\"" + strPackageFile + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-install", string.Join(" ", tabArgument));
		}
		public static void InstallRemove(string strDevice, string strPackageFile) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"--remove \"" + strPackageFile + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-install", string.Join(" ", tabArgument));
		}
		//public static void InstallList(string strDevice, string strPackageFile) {
		//	System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
		//		"-d \"" + strDevice + "\"",
		//		"-l"
		//	};
		//	MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-install", string.Join(" ", tabArgument));
		//}
		public static void Launch(string strDevice, string strApplicationId) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"\"" + strApplicationId + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-launch", string.Join(" ", tabArgument));
		}
		public static void Inspect(string strDevice, string strApplicationId) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"-a \"" + strApplicationId + "\"",
				"-o"
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-inspect", string.Join(" ", tabArgument));
		}
		public static void Extend(string strDevice) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-extend-dev", string.Join(" ", tabArgument));
		}
	}
}
