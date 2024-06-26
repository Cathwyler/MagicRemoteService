﻿
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
	public class UShortJsonConverter : System.Text.Json.Serialization.JsonConverter<ushort> {
		public override bool CanConvert(System.Type objectType) {
			return (objectType == typeof(ushort));
		}
		public override ushort Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options) {
			return ushort.Parse(reader.GetString());
		}
		public override void Write(System.Text.Json.Utf8JsonWriter writer, ushort value, System.Text.Json.JsonSerializerOptions options) {
			writer.WriteStringValue(value.ToString());
		}
	}
	public class WebOSCLIDeviceInput {
		public string Id {
			get; set;
		}
		public string Name {
			get; set;
		}
		public string Source {
			get; set;
		}
		public string AppIdShort {
			get; set;
		}
	}
	public class WebOSCLIDeviceInfo {
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.IPAddressJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("ip")]
		public System.Net.IPAddress IP {
			get; set;
		}
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.UShortJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("port")]
		public ushort Port {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("user")]
		public string User {
			get; set;
		}
	}
	public class WebOSCLIDeviceDetail {
		[System.Text.Json.Serialization.JsonPropertyName("platform")]
		public string Platform {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("privatekey")]
		public string PrivateKey {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("passphrase")]
		public string Passphrase {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("description")]
		public string Description {
			get; set;
		}
	}
	public class WebOSCLIDevice {

		[System.Text.Json.Serialization.JsonPropertyName("profile")]
		public string Profile {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("name")]
		public string Name {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("default")]
		public bool Default {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("deviceinfo")]
		public MagicRemoteService.WebOSCLIDeviceInfo DeviceInfo {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("connection")]
		public string[] Connection {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("details")]
		public MagicRemoteService.WebOSCLIDeviceDetail DeviceDetail {
			get; set;
		}
	}

	internal class WebOSCLIDeviceSet {

		[System.Text.Json.Serialization.JsonPropertyName("name")]
		public string Name {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("description")]
		public string Description {
			get; set;
		}
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.IPAddressJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("host")]
		public System.Net.IPAddress IP {
			get; set;
		}
		[System.Text.Json.Serialization.JsonConverter(typeof(MagicRemoteService.UShortJsonConverter))]
		[System.Text.Json.Serialization.JsonPropertyName("port")]
		public ushort Port {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("username")]
		public string User {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("password")]
		public string Password {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("privatekey")]
		public string PrivateKey {
			get; set;
		}
		[System.Text.Json.Serialization.JsonPropertyName("passphrase")]
		public string Passphrase {
			get; set;
		}
	}
	internal static class WebOSCLI {
		private static string ExecWebOSCLICommand(string strCommand, string strArgument, System.Collections.Generic.Dictionary<ushort, string> dInput = null, string strWorkingDirectory = null) {
			System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
			pProcess.StartInfo.FileName = "cmd";
			if(!string.IsNullOrEmpty(strWorkingDirectory)) {
				pProcess.StartInfo.WorkingDirectory = strWorkingDirectory;
			}
			pProcess.StartInfo.Arguments = "/c " + strCommand + " " + strArgument;
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
					strOutput += e.Data + System.Environment.NewLine;
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
				new MagicRemoteService.WebOSCLIDeviceInput() { Id = "HDMI_1", Name = "HDMI 1", Source = "ext://hdmi:1", AppIdShort = "hdmi1" },
				new MagicRemoteService.WebOSCLIDeviceInput() { Id = "HDMI_2", Name = "HDMI 2", Source = "ext://hdmi:2", AppIdShort = "hdmi2" },
				new MagicRemoteService.WebOSCLIDeviceInput() { Id = "HDMI_3", Name = "HDMI 3", Source = "ext://hdmi:3", AppIdShort = "hdmi3" },
				new MagicRemoteService.WebOSCLIDeviceInput() { Id = "HDMI_4", Name = "HDMI 4", Source = "ext://hdmi:4", AppIdShort = "hdmi4" }
			};
		}
		public static MagicRemoteService.WebOSCLIDevice[] SetupDeviceList() {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-F"
			};
			return System.Text.Json.JsonSerializer.Deserialize<MagicRemoteService.WebOSCLIDevice[]>(MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-setup-device", string.Join(" ", tabArgument)));
		}
		public static void SetupDeviceAdd(MagicRemoteService.WebOSCLIDevice wocdDevice, string strPassword) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-a \"" + wocdDevice.Name + "\"",
				"-i \"" + System.Text.Json.JsonSerializer.Serialize<MagicRemoteService.WebOSCLIDeviceSet>(new MagicRemoteService.WebOSCLIDeviceSet {
					Name = wocdDevice.Name,
					Description = wocdDevice.DeviceDetail.Description,
					IP = wocdDevice.DeviceInfo.IP,
					Port = wocdDevice.DeviceInfo.Port,
					User = wocdDevice.DeviceInfo.User,
					Password = strPassword,
					PrivateKey = wocdDevice.DeviceDetail.PrivateKey,
					Passphrase = wocdDevice.DeviceDetail.Passphrase
				}).Replace("\"", "'") + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-setup-device", string.Join(" ", tabArgument));
		}
		public static void SetupDeviceModify(string strDevice, MagicRemoteService.WebOSCLIDevice wocdDevice, string strPassword) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-m \"" + strDevice + "\"",
				"-i \"" + System.Text.Json.JsonSerializer.Serialize<MagicRemoteService.WebOSCLIDeviceSet>(new MagicRemoteService.WebOSCLIDeviceSet {
					Name = wocdDevice.Name,
					Description = wocdDevice.DeviceDetail.Description,
					IP = wocdDevice.DeviceInfo.IP,
					Port = wocdDevice.DeviceInfo.Port,
					User = wocdDevice.DeviceInfo.User,
					Password = strPassword,
					PrivateKey = wocdDevice.DeviceDetail.PrivateKey,
					Passphrase = wocdDevice.DeviceDetail.Passphrase
				}).Replace("\"", "'") + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-setup-device", string.Join(" ", tabArgument));
		}
		public static void SetupDeviceRemove(string strDevice) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-r \"" + strDevice + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-setup-device", string.Join(" ", tabArgument));
		}
		public static void NovacomGetKey(string strDevice, string strPassphrase) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"-k"
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-novacom", string.Join(" ", tabArgument), new System.Collections.Generic.Dictionary<ushort, string>() { { 1, strPassphrase } });
		}
		public static void NovacomRun(string strDevice, string strCommand) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"-r \"" + strCommand.Replace("\"", "\\\"") + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-novacom", string.Join(" ", tabArgument));
		}
		public static void Package(string strOutDirectory, string strApplication, string strService = null, string strPackage = null) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"\"" + strApplication + "\"",
				"-n"
			};
			if(!string.IsNullOrEmpty(strOutDirectory)) {
				tabArgument.Add("-o \"" + strOutDirectory + "\"");
			}
			if(!string.IsNullOrEmpty(strService)) {
				tabArgument.Add("\"" + strService + "\"");
			}
			if(!string.IsNullOrEmpty(strPackage)) {
				tabArgument.Add("\"" + strPackage + "\"");
			}
			MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-package", string.Join(" ", tabArgument));
		}
		public static void Install(string strDevice, string strPackageFile) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"\"" + strPackageFile + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-install", string.Join(" ", tabArgument));
		}
		public static void InstallRemove(string strDevice, string strPackageFile) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"--remove \"" + strPackageFile + "\""
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-install", string.Join(" ", tabArgument));
		}
		public static void Launch(string strDevice, string strApplicationId, string strParameter = null) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"\"" + strApplicationId + "\""
			};
			if(!string.IsNullOrEmpty(strParameter)) {
				tabArgument.Add("-p \"" + strParameter + "\"");
			}
			MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-launch", string.Join(" ", tabArgument));
		}
		public static void InspectApplication(string strDevice, string strApplicationId) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"-a \"" + strApplicationId + "\"",
				"-o"
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-inspect", string.Join(" ", tabArgument));
		}
		public static void InspectService(string strDevice, string strServiceId) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"-s \"" + strServiceId + "\"",
				"-o"
			};
			MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-inspect", string.Join(" ", tabArgument));
		}
		public static string DeviceInfo(string strDevice) {
			System.Collections.Generic.List<string> tabArgument = new System.Collections.Generic.List<string> {
				"-d \"" + strDevice + "\"",
				"-i"
			};
			return MagicRemoteService.WebOSCLI.ExecWebOSCLICommand("ares-device", string.Join(" ", tabArgument));
		}
	}
}
