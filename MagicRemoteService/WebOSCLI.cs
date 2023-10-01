
namespace MagicRemoteService {

	[System.Serializable]
	public class WebOSCLIException : System.Exception {
		public WebOSCLIException() { }
		public WebOSCLIException(string strMessage) :
			base(strMessage) { }
		public WebOSCLIException(string strMessage, System.Exception eInner) :
			base(strMessage, eInner) { }

	}
	[System.Serializable]
	public class WebOSCLINotFoundException : MagicRemoteService.WebOSCLIException {
		public WebOSCLINotFoundException() :
			base(MagicRemoteService.Properties.Resources.WebOSCLINotFoundExceptionMessage) {
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
		public string Id { get; set; }
		public string Name { get; set; }
		public string Source { get; set; }
		public string AppId { get; set; }
		public string AppIdShort { get; set; }
	}
	//public class WebOSCLIDeviceInstall {
	//	public string Name { get; set; }
	//}
	public class WebOSCLIDeviceInfo {
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.IPAddressJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("ip")]
		public System.Net.IPAddress IP { get; set; }
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.ShortJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("port")]
		public short Port { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("user")]
		public string User { get; set; }
	}
	public class WebOSCLIDeviceDetail {
		[System.Text.Json.Serialization.JsonPropertyName("platform")]
		public string Platform { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("privatekey")]
		public string PrivateKey { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("passphrase")]
		public string Passphrase { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("description")]
		public string Description { get; set; }
	}
	public class WebOSCLIDevice {

		[System.Text.Json.Serialization.JsonPropertyName("profile")]
		public string Profile { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("name")]
		public string Name { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("default")]
		public bool Default { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("deviceinfo")]
		public MagicRemoteService.WebOSCLIDeviceInfo DeviceInfo { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("connection")]
		public string[] Connection { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("details")]
		public MagicRemoteService.WebOSCLIDeviceDetail DeviceDetail { get; set; }

	}
	class WebOSCLIDeviceSet {

		[System.Text.Json.Serialization.JsonPropertyName("name")]
		public string Name { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("description")]
		public string Description { get; set; }
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.IPAddressJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("host")]
		public System.Net.IPAddress IP { get; set; }
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.ShortJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("port")]
		public short Port { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("username")]
		public string User { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("password")]
		public string Password { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("privatekey")]
		public string PrivateKey { get; set; }
		[System.Text.Json.Serialization.JsonPropertyName("passphrase")]
		public string Passphrase { get; set; }
	}
	static class WebOSCLI {
		static private string GetWebOSCLIDir() {
			string strWebOSCliDir = System.Environment.GetEnvironmentVariable("WEBOS_CLI_TV");
			if(strWebOSCliDir == null) {
				throw new MagicRemoteService.WebOSCLINotFoundException();
			}
			return strWebOSCliDir;
		}
		static private string ExecWebOSCLICmd(string strCommand, string strArgument, System.Collections.Generic.Dictionary<ushort, string> dInput = null, string strWorkingDirectory = null) {
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
			pProcess.ErrorDataReceived += delegate(object sender, System.Diagnostics.DataReceivedEventArgs e) {
				if(e.Data != null) {
					strErr += e.Data;
				}
			};
			pProcess.OutputDataReceived += delegate(object sender, System.Diagnostics.DataReceivedEventArgs e) {
				if(e.Data != null) {
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
		static public MagicRemoteService.WebOSCLIDeviceInput[] InputList() {
			return new MagicRemoteService.WebOSCLIDeviceInput[] {
				new MagicRemoteService.WebOSCLIDeviceInput() { Id = "HDMI_1", Name = "HDMI 1", Source = "ext://hdmi:1", AppId = "com.webos.app.hdmi1", AppIdShort = "hdmi1" },
				new MagicRemoteService.WebOSCLIDeviceInput() { Id = "HDMI_2", Name = "HDMI 2", Source = "ext://hdmi:2", AppId = "com.webos.app.hdmi2", AppIdShort = "hdmi2" },
				new MagicRemoteService.WebOSCLIDeviceInput() { Id = "HDMI_3", Name = "HDMI 3", Source = "ext://hdmi:3", AppId = "com.webos.app.hdmi3", AppIdShort = "hdmi3" },
				new MagicRemoteService.WebOSCLIDeviceInput() { Id = "HDMI_4", Name = "HDMI 4", Source = "ext://hdmi:4", AppId = "com.webos.app.hdmi4", AppIdShort = "hdmi4" }
			};
		}
		static public MagicRemoteService.WebOSCLIDevice[] SetupDeviceList() {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string>();
			tabArgument.Add("-F");
			return System.Text.Json.JsonSerializer.Deserialize<MagicRemoteService.WebOSCLIDevice[]>(MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-setup-device", string.Join(" ", tabArgument)));
		}
		static public void SetupDeviceAdd(MagicRemoteService.WebOSCLIDevice wcdDevice, string strPassword) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string>();
			tabArgument.Add("-a \"" + wcdDevice.Name + "\"");
			tabArgument.Add("-i \"" + System.Text.Json.JsonSerializer.Serialize<MagicRemoteService.WebOSCLIDeviceSet>(new MagicRemoteService.WebOSCLIDeviceSet {
				Name = wcdDevice.Name,
				Description = wcdDevice.DeviceDetail.Description,
				IP = wcdDevice.DeviceInfo.IP,
				Port = wcdDevice.DeviceInfo.Port,
				User = wcdDevice.DeviceInfo.User,
				Password = strPassword,
				PrivateKey = wcdDevice.DeviceDetail.PrivateKey,
				Passphrase = wcdDevice.DeviceDetail.Passphrase
			}).Replace("\"", "'") + "\"");
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-setup-device", string.Join(" ", tabArgument));
		}
		static public void SetupDeviceModify(string strDevice, MagicRemoteService.WebOSCLIDevice wcdDevice, string strPassword) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string>();
			tabArgument.Add("-m \"" + strDevice + "\"");
			tabArgument.Add("-i \"" + System.Text.Json.JsonSerializer.Serialize<MagicRemoteService.WebOSCLIDeviceSet>(new MagicRemoteService.WebOSCLIDeviceSet {
				Name = wcdDevice.Name,
				Description = wcdDevice.DeviceDetail.Description,
				IP = wcdDevice.DeviceInfo.IP,
				Port = wcdDevice.DeviceInfo.Port,
				User = wcdDevice.DeviceInfo.User,
				Password = strPassword,
				PrivateKey = wcdDevice.DeviceDetail.PrivateKey,
				Passphrase = wcdDevice.DeviceDetail.Passphrase
			}).Replace("\"", "'") + "\"");
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-setup-device", string.Join(" ", tabArgument));
		}
		static public void SetupDeviceRemove(string strDevice) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string>();
			tabArgument.Add("-r \"" + strDevice + "\"");
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-setup-device", string.Join(" ", tabArgument));
		}
		static public void NovacomGetKey(string strDevice, string strPassphrase) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string>();
			tabArgument.Add("-d \"" + strDevice + "\"");
			tabArgument.Add("-k");
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-novacom", string.Join(" ", tabArgument), new System.Collections.Generic.Dictionary<ushort, string>(){ { 1, strPassphrase } });
		}
		static public void Package(string strOutDirectory, string strApplication, string strService = null, string strPackage = null) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string>();
			tabArgument.Add("\"" + strApplication + "\"");
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
		static public void Install(string strDevice, string strPackageFile) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string>();
			tabArgument.Add("-d \"" + strDevice + "\"");
			tabArgument.Add("\"" + strPackageFile + "\"");
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-install", string.Join(" ", tabArgument));
		}
		static public void InstallRemove(string strDevice, string strPackageFile) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string>();
			tabArgument.Add("-d \"" + strDevice + "\"");
			tabArgument.Add("--remove \"" + strPackageFile + "\"");
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-install", string.Join(" ", tabArgument));
		}
		//static public void InstallList(string strDevice, string strPackageFile) {
		//	System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string>();
		//	tabArgument.Add("-d \"" + strDevice + "\"");
		//	tabArgument.Add("-l");
		//	MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-install", string.Join(" ", tabArgument));
		//}
		static public void Launch(string strDevice, string strApplicationId) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string>();
			tabArgument.Add("-d \"" + strDevice + "\"");
			tabArgument.Add("\"" + strApplicationId + "\"");
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-launch", string.Join(" ", tabArgument));
		}
		static public void Extend(string strDevice) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string>();
			tabArgument.Add("-d \"" + strDevice + "\"");
			MagicRemoteService.WebOSCLI.ExecWebOSCLICmd("ares-extend-dev", string.Join(" ", tabArgument));
		}
	}
}
