
namespace MagicRemoteService {
	public partial class Setting : System.Windows.Forms.Form {

		private static readonly System.Net.IPAddress ipaSendIPDefaut;
		private static readonly System.Net.IPAddress ipaMaskDefaut;
		private static readonly MagicRemoteService.PhysicalAddress paMacDefaut;
		static Setting() {
			System.Net.NetworkInformation.NetworkInterface niDefaut = System.Linq.Enumerable.First<System.Net.NetworkInformation.NetworkInterface>(System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(), delegate (System.Net.NetworkInformation.NetworkInterface ni) {
				return ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up && ni.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback;
			});

			System.Net.NetworkInformation.UnicastIPAddressInformation uipaiDefaut = System.Linq.Enumerable.First<System.Net.NetworkInformation.UnicastIPAddressInformation>(niDefaut.GetIPProperties().UnicastAddresses, delegate (System.Net.NetworkInformation.UnicastIPAddressInformation ipa) {
				return ipa.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
			});

			MagicRemoteService.Setting.ipaSendIPDefaut = uipaiDefaut.Address;
			MagicRemoteService.Setting.ipaMaskDefaut = uipaiDefaut.IPv4Mask;
			MagicRemoteService.Setting.paMacDefaut = new MagicRemoteService.PhysicalAddress(niDefaut.GetPhysicalAddress());
		}

		private MagicRemoteService.Service mrsService;

		private System.Collections.Generic.Dictionary<ushort, BindControl> dKeyBindControl;
		//private System.Collections.Generic.Dictionary<BindControl, ushort> dBindControlKey;

		private decimal dListenPort;
		private bool bInactivity;
		private decimal dTimeoutInactivity;
		private bool bStartup;

		private MagicRemoteService.WebOSCLIDeviceInput wcdiInput;
		private MagicRemoteService.Screen scrDisplay;
		private System.Net.IPAddress iaSendIP;
		private decimal dSendPort;
		private System.Net.IPAddress iaSubnetMask;
		private MagicRemoteService.PhysicalAddress paPCMac;
		private decimal	dTimeoutRightClick;
		private bool bExtend;

		private System.Collections.Generic.Dictionary<ushort, Bind> dKeyBind = new System.Collections.Generic.Dictionary<ushort, Bind>() {
			{ 0x0001, null },
			{ 0x0002, null },
			{ 0x0008, null },
			{ 0x000D, null },
			{ 0x0025, null },
			{ 0x0026, null },
			{ 0x0027, null },
			{ 0x0028, null },
			{ 0x0193, null },
			{ 0x0194, null },
			{ 0x0195, null },
			{ 0x0196, null },
			{ 0x01CD, null },
			{ 0x019F, null },
			{ 0x0013, null },
			{ 0x01A1, null },
			{ 0x019C, null },
			{ 0x019D, null }
		};

		public Setting(MagicRemoteService.Service mrs) {
			this.mrsService = mrs;
			this.InitializeComponent();
			this.dKeyBindControl = new System.Collections.Generic.Dictionary<ushort, BindControl>() {
				{ 0x0001, this.bcRemoteClick },
				{ 0x0002, this.bcRemoteLongClick },
				{ 0x0008, this.bcRemoteBackspace },
				{ 0x000D, this.bcRemoteOk },
				{ 0x0025, this.bcRemoteLeft },
				{ 0x0026, this.bcRemoteUp },
				{ 0x0027, this.bcRemoteRight },
				{ 0x0028, this.bcRemoteDown },
				{ 0x0193, this.bcRemoteRed },
				{ 0x0194, this.bcRemoteGreen },
				{ 0x0195, this.bcRemoteYellow },
				{ 0x0196, this.bcRemoteBlue },
				{ 0x01CD, this.bcRemoteBack },
				{ 0x019F, this.bcRemotePlay },
				{ 0x0013, this.bcRemotePause },
				{ 0x01A1, this.bcRemoteFastForward },
				{ 0x019C, this.bcRemoteRewind },
				{ 0x019D, this.bcRemoteStop }
			};
			this.cmbboxDisplay.DataSource = new System.Collections.Generic.List<Screen>(System.Linq.Enumerable.Concat<Screen>(new System.Collections.Generic.List<Screen>() { Screen.PrimaryDefaut }, MagicRemoteService.Screen.AllScreen.Values));
			this.PCDataRefresh();
			this.TVDataRefresh();
			this.RemoteDataRefresh();
			this.btnTVRefresh_Click(this, new System.EventArgs());
		}
		private new bool Enabled {
			set {
				this.UseWaitCursor = !value;
				this.tabSetting.Enabled = value;
			}
		}
		public void PCDataRefresh() {
			Microsoft.Win32.RegistryKey rkMagicRemoteService = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService");
			if(rkMagicRemoteService == null) {
				this.numboxListenPort.Value = 41230;
				this.chkboxInactivity.Checked = true;
				this.numboxTimeoutInactivity.Value = 7200000;
			} else {
				this.numboxListenPort.Value = (int)rkMagicRemoteService.GetValue("Port", 41230);
				this.chkboxInactivity.Checked = (int)rkMagicRemoteService.GetValue("Inactivity", 1) != 0;
				this.numboxTimeoutInactivity.Value = (int)rkMagicRemoteService.GetValue("TimeoutInactivity", 7200000);
			}
			this.dListenPort = this.numboxListenPort.Value;
			this.bInactivity = this.chkboxInactivity.Checked;
			this.dTimeoutInactivity = this.numboxTimeoutInactivity.Value;

			if(MagicRemoteService.Program.bElevated) {
				System.ServiceProcess.ServiceController scService = System.Array.Find<System.ServiceProcess.ServiceController>(System.ServiceProcess.ServiceController.GetServices(), delegate (System.ServiceProcess.ServiceController sc) {
					return sc.ServiceName == "MagicRemoteService";
				});
				if(scService == null) {
					this.chkboxStartup.Checked = true;
				} else {
					this.chkboxStartup.Checked = scService.StartType == System.ServiceProcess.ServiceStartMode.Automatic;
				}
			} else {
				TaskScheduler.ITaskService ts = (TaskScheduler.ITaskService)System.Activator.CreateInstance(System.Type.GetTypeFromProgID("Schedule.Service"));
				ts.Connect();
				TaskScheduler.IRegisteredTask rtStartup = null;
				foreach(TaskScheduler.IRegisteredTask rt in ts.GetFolder("\\").GetTasks(0)) {
					if(rt.Name == "MagicRemoteService") {
						rtStartup = rt;
					}
				}
				if(rtStartup == null) {
					this.chkboxStartup.Checked = true;
				} else {
					this.chkboxStartup.Checked = rtStartup.Enabled;
				}
			}
			this.bStartup = this.chkboxStartup.Checked;
		}
		public void PCDataSave() {
			Microsoft.Win32.RegistryKey rkMagicRemoteService = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey("Software\\MagicRemoteService");
			rkMagicRemoteService.SetValue("Port", this.numboxListenPort.Value, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteService.SetValue("Inactivity", this.chkboxInactivity.Checked, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteService.SetValue("TimeoutInactivity", this.numboxTimeoutInactivity.Value, Microsoft.Win32.RegistryValueKind.DWord);

			if(MagicRemoteService.Program.bElevated) {
				System.ServiceProcess.ServiceController scService = System.Array.Find<System.ServiceProcess.ServiceController>(System.ServiceProcess.ServiceController.GetServices(), delegate (System.ServiceProcess.ServiceController sc) {
					return sc.ServiceName == "MagicRemoteService";
				});
				if(scService != null) {
					if(scService.CanStop) {
						scService.Stop();
						scService.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped);
					}
				}
				//Find a way to set exception handling
				System.Configuration.Install.AssemblyInstaller ai = new System.Configuration.Install.AssemblyInstaller(System.Reflection.Assembly.GetExecutingAssembly(), this.chkboxStartup.Checked ? new string[] {"enable"} : new string[] { });
				System.Collections.Hashtable h = new System.Collections.Hashtable();
				ai.UseNewContext = true;
				try {
					if(scService != null) {
						ai.Uninstall(h);
					}
					ai.Install(h);
					ai.Commit(h);
				} catch {
					try {
						ai.Rollback(h);
					} catch { }
					throw;
				}
				ai.Dispose();
				scService = System.Array.Find<System.ServiceProcess.ServiceController>(System.ServiceProcess.ServiceController.GetServices(), delegate (System.ServiceProcess.ServiceController sc) {
					return sc.ServiceName == "MagicRemoteService";
				});
				if(scService != null) {
					scService.Start();
				}
			} else {
				TaskScheduler.ITaskService ts = (TaskScheduler.ITaskService)System.Activator.CreateInstance(System.Type.GetTypeFromProgID("Schedule.Service"));
				ts.Connect();
				TaskScheduler.ITaskDefinition tdStartup = ts.NewTask(0);
				//tdStartup.Principal.RunLevel = TaskScheduler._TASK_RUNLEVEL.TASK_RUNLEVEL_HIGHEST;
				tdStartup.Settings.Enabled = this.chkboxStartup.Checked;
				tdStartup.Settings.IdleSettings.StopOnIdleEnd = false;
				tdStartup.Settings.DisallowStartIfOnBatteries = false;
				tdStartup.Settings.StopIfGoingOnBatteries = false;
				tdStartup.Settings.ExecutionTimeLimit = "PT0S";
				tdStartup.Settings.MultipleInstances = TaskScheduler._TASK_INSTANCES_POLICY.TASK_INSTANCES_STOP_EXISTING;
				TaskScheduler.ILogonTrigger ltStartup = (TaskScheduler.ILogonTrigger)tdStartup.Triggers.Create(TaskScheduler._TASK_TRIGGER_TYPE2.TASK_TRIGGER_LOGON);
				ltStartup.UserId = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
				TaskScheduler.IExecAction eaStartup = (TaskScheduler.IExecAction)tdStartup.Actions.Create(TaskScheduler._TASK_ACTION_TYPE.TASK_ACTION_EXEC);
				eaStartup.Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
				eaStartup.WorkingDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
				ts.GetFolder("\\").RegisterTaskDefinition("MagicRemoteService", tdStartup, (int)TaskScheduler._TASK_CREATION.TASK_CREATE_OR_UPDATE, null, null, TaskScheduler._TASK_LOGON_TYPE.TASK_LOGON_NONE);
			}

			if(MagicRemoteService.Program.bElevated) {
				NetFwTypeLib.INetFwRule nfrMagicRemoteService = (NetFwTypeLib.INetFwRule)System.Activator.CreateInstance(System.Type.GetTypeFromProgID("HNetCfg.FWRule"));
				nfrMagicRemoteService.Direction = NetFwTypeLib.NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
				nfrMagicRemoteService.Name = "MagicRemoteService";
				nfrMagicRemoteService.Enabled = true;
				nfrMagicRemoteService.Action = NetFwTypeLib.NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
				nfrMagicRemoteService.ApplicationName = System.Reflection.Assembly.GetExecutingAssembly().Location;
				nfrMagicRemoteService.Protocol = (int)NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
				NetFwTypeLib.INetFwPolicy2 nfp = (NetFwTypeLib.INetFwPolicy2)System.Activator.CreateInstance(System.Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
				nfp.Rules.Remove("MagicRemoteService");
				nfp.Rules.Add(nfrMagicRemoteService);
			}
		}
		public void TVDataRefresh() {
			if(this.cmbboxTV.SelectedItem == null) {
				this.cmbboxInput.Enabled = false;
				this.cmbboxDisplay.Enabled = false;
				this.ipadrboxSendIP.Enabled = false;
				this.numboxSendPort.Enabled = false;
				this.ipadrboxSubnetMask.Enabled = false;
				this.phyadrboxPCMac.Enabled = false;
				this.numboxTimeoutRightClick.Enabled = false;
				this.chkboxExtend.Enabled = false;
				this.btnTVInstall.Enabled = false;
				this.cmbboxInput.SelectedItem = null;
				this.cmbboxDisplay.SelectedItem = null;
				this.ipadrboxSendIP.Value = MagicRemoteService.Setting.ipaSendIPDefaut;
				this.numboxSendPort.Value = this.numboxListenPort.Value;
				this.ipadrboxSubnetMask.Value = MagicRemoteService.Setting.ipaMaskDefaut;
				this.phyadrboxPCMac.Value = MagicRemoteService.Setting.paMacDefaut;
				this.numboxTimeoutRightClick.Value = 1500;
				this.chkboxExtend.Checked = false;
			} else {
				this.cmbboxInput.Enabled = true;
				this.cmbboxDisplay.Enabled = true;
				this.ipadrboxSendIP.Enabled = true;
				this.numboxSendPort.Enabled = true;
				this.ipadrboxSubnetMask.Enabled = true;
				this.phyadrboxPCMac.Enabled = true;
				this.numboxTimeoutRightClick.Enabled = true;
				this.btnTVInstall.Enabled = true;
				this.chkboxExtend.Enabled = true;
				Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService\\" + ((MagicRemoteService.WebOSCLIDevice)this.cmbboxTV.SelectedItem).Name);
				if(rkMagicRemoteServiceDevice == null) {
					this.cmbboxInput.SelectedIndex = 0;
					this.cmbboxDisplay.SelectedIndex = 0;
					this.ipadrboxSendIP.Value = MagicRemoteService.Setting.ipaSendIPDefaut;
					this.numboxSendPort.Value = this.numboxListenPort.Value;
					this.ipadrboxSubnetMask.Value = MagicRemoteService.Setting.ipaMaskDefaut;
					this.phyadrboxPCMac.Value = MagicRemoteService.Setting.paMacDefaut;
					this.numboxTimeoutRightClick.Value = 1500;
					this.chkboxExtend.Checked = false;
				} else {
					string sInputId = (string)rkMagicRemoteServiceDevice.GetValue("Input");
					if(sInputId == null) {
						this.cmbboxInput.SelectedIndex = 0;
					} else {
						this.cmbboxInput.SelectedValue = sInputId;
					}
					this.cmbboxDisplay.SelectedValue = (uint)(int)rkMagicRemoteServiceDevice.GetValue("Display", (int)(uint)this.cmbboxDisplay.SelectedValue);
					string sIp = (string)rkMagicRemoteServiceDevice.GetValue("SendIp");
					if(sIp == null) {
						this.ipadrboxSendIP.Value = MagicRemoteService.Setting.ipaSendIPDefaut;
					} else {
						this.ipadrboxSendIP.FromString(sIp);
					}
					this.numboxSendPort.Value = (int)rkMagicRemoteServiceDevice.GetValue("SendPort", (int)this.numboxListenPort.Value);
					string sMask = (string)rkMagicRemoteServiceDevice.GetValue("Mask");
					if(sMask == null) {
						this.ipadrboxSubnetMask.Value = MagicRemoteService.Setting.ipaMaskDefaut;
					} else {
						this.ipadrboxSubnetMask.FromString(sMask);
					}
					string sMac = (string)rkMagicRemoteServiceDevice.GetValue("PCMac");
					if(sMac == null) {
						this.phyadrboxPCMac.Value = MagicRemoteService.Setting.paMacDefaut;
					} else {
						this.phyadrboxPCMac.FromString(sMac);
					}
					this.numboxTimeoutRightClick.Value = (int)rkMagicRemoteServiceDevice.GetValue("TimeoutRightClick", 1500);
					this.chkboxExtend.Checked = (int)rkMagicRemoteServiceDevice.GetValue("Extend", 0) != 0;
				}
			}
			this.wcdiInput = (MagicRemoteService.WebOSCLIDeviceInput)this.cmbboxInput.SelectedItem;
			this.scrDisplay = (MagicRemoteService.Screen)this.cmbboxDisplay.SelectedItem;
			this.iaSendIP = this.ipadrboxSendIP.Value;
			this.dSendPort = this.numboxSendPort.Value;
			this.iaSubnetMask = this.ipadrboxSubnetMask.Value;
			this.paPCMac = this.phyadrboxPCMac.Value;
			this.dTimeoutRightClick = this.numboxTimeoutRightClick.Value;
			this.bExtend = this.chkboxExtend.Checked;
		}
		public void TVDataSave() {
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey("Software\\MagicRemoteService\\" + ((MagicRemoteService.WebOSCLIDevice)this.cmbboxTV.SelectedItem).Name);
			rkMagicRemoteServiceDevice.SetValue("Input", ((MagicRemoteService.WebOSCLIDeviceInput)this.cmbboxInput.SelectedItem).Id, Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("Display", ((MagicRemoteService.Screen)this.cmbboxDisplay.SelectedItem).Id, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteServiceDevice.SetValue("SendIp", this.ipadrboxSendIP.Value.ToString(), Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("SendPort", this.numboxSendPort.Value, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteServiceDevice.SetValue("Mask", this.ipadrboxSubnetMask.Value.ToString(), Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("PCMac", this.phyadrboxPCMac.Value.ToString(), Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("TimeoutRightClick", this.numboxTimeoutRightClick.Value, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteServiceDevice.SetValue("Extend", this.chkboxExtend.Checked, Microsoft.Win32.RegistryValueKind.DWord);
		}
		public void RemoteDataRefresh() {
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceKeyBindMouse = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService\\KeyBindMouse");
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceKeyBindKeyboard = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService\\KeyBindKeyboard");
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceKeyBindAction = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService\\KeyBindAction");
			if(rkMagicRemoteServiceKeyBindMouse == null && rkMagicRemoteServiceKeyBindKeyboard == null && rkMagicRemoteServiceKeyBindAction == null) {
				this.bcRemoteClick.Value = new BindMouse(BindMouseValue.Left);		//Click -> Left click
				this.bcRemoteLongClick.Value = new BindMouse(BindMouseValue.Right);	//Long click -> Right click
				this.bcRemoteBackspace.Value = new BindKeyboard(0x000E);		//BACKSPACE -> Keyboard Delete
				this.bcRemoteOk.Value = new BindKeyboard(0x001C);				//OK -> Keyboard Return Enter
				this.bcRemoteLeft.Value = new BindKeyboard(0xE04B);				//Left -> Keyboard LeftArrow
				this.bcRemoteUp.Value = new BindKeyboard(0xE048);				//Up -> Keyboard UpArrow
				this.bcRemoteRight.Value = new BindKeyboard(0xE04D);			//Right -> Keyboard RightArrow
				this.bcRemoteDown.Value = new BindKeyboard(0xE050);				//Down -> Keyboard DownArrow
				this.bcRemoteRed.Value = new BindAction(BindActionValue.Shutdown);	//Red -> Show shutdown
				this.bcRemoteGreen.Value = new BindKeyboard(0xE05B);			//Green -> Keyboard Left GUI
				this.bcRemoteYellow.Value = new BindMouse(BindMouseValue.Right);		//Yellow -> Right click
				this.bcRemoteBlue.Value = new BindAction(BindActionValue.Keyboard);	//Blue -> Show keyboard
				this.bcRemoteBack.Value = new BindKeyboard(0x0001);				//Back -> Keyboard Escape
				this.bcRemotePlay.Value = null;									//Play
				this.bcRemotePause.Value = null;								//Pause
				this.bcRemoteFastForward.Value = null;							//Fast-forward
				this.bcRemoteRewind.Value = null;								//Rewind
				this.bcRemoteStop.Value = null;									//Stop
			} else {
				foreach(string sKey in rkMagicRemoteServiceKeyBindMouse.GetValueNames()) {
					this.dKeyBindControl[System.UInt16.Parse(sKey.Substring(2), System.Globalization.NumberStyles.HexNumber)].Value = new BindMouse((BindMouseValue)(int)rkMagicRemoteServiceKeyBindMouse.GetValue(sKey, 0x0000));
				}
				foreach(string sKey in rkMagicRemoteServiceKeyBindKeyboard.GetValueNames()) {
					this.dKeyBindControl[System.UInt16.Parse(sKey.Substring(2), System.Globalization.NumberStyles.HexNumber)].Value = new BindKeyboard((ushort)(int)rkMagicRemoteServiceKeyBindKeyboard.GetValue(sKey, 0x0000));
				}
				foreach(string sKey in rkMagicRemoteServiceKeyBindAction.GetValueNames()) {
					this.dKeyBindControl[System.UInt16.Parse(sKey.Substring(2), System.Globalization.NumberStyles.HexNumber)].Value = new BindAction((BindActionValue)(int)rkMagicRemoteServiceKeyBindAction.GetValue(sKey, 0x0000));
				}
			}
			foreach(System.Collections.Generic.KeyValuePair<ushort, BindControl> kvp in this.dKeyBindControl) {
				this.dKeyBind[kvp.Key] = kvp.Value.Value;
			}
		}
		public void RemoteDataSave() {
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceKeyBindMouse = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey("Software\\MagicRemoteService\\KeyBindMouse");
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceKeyBindKeyboard = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey("Software\\MagicRemoteService\\KeyBindKeyboard");
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceKeyBindAction = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey("Software\\MagicRemoteService\\KeyBindAction");
			foreach(string sKey in rkMagicRemoteServiceKeyBindMouse.GetValueNames()) {
				//if(!this.dKeyBind.ContainsKey(System.UInt16.Parse(sKey.Substring(2), System.Globalization.NumberStyles.HexNumber))) {
					rkMagicRemoteServiceKeyBindMouse.DeleteValue(sKey);
				//}
			}
			foreach(string sKey in rkMagicRemoteServiceKeyBindKeyboard.GetValueNames()) {
				//if(!this.dKeyBind.ContainsKey(System.UInt16.Parse(sKey.Substring(2), System.Globalization.NumberStyles.HexNumber))) {
					rkMagicRemoteServiceKeyBindKeyboard.DeleteValue(sKey);
				//}
			}
			foreach(string sKey in rkMagicRemoteServiceKeyBindAction.GetValueNames()) {
				//if(!this.dKeyBind.ContainsKey(System.UInt16.Parse(sKey.Substring(2), System.Globalization.NumberStyles.HexNumber))) {
					rkMagicRemoteServiceKeyBindAction.DeleteValue(sKey);
				//}
			}
			foreach(System.Collections.Generic.KeyValuePair<ushort, BindControl> kvp in this.dKeyBindControl) {
				switch(kvp.Value.Value) {
					case BindMouse bm:
						rkMagicRemoteServiceKeyBindMouse.SetValue("0x" + kvp.Key.ToString("X4"), bm.bmvValue, Microsoft.Win32.RegistryValueKind.DWord);
						break;
					case BindKeyboard bk:
						rkMagicRemoteServiceKeyBindKeyboard.SetValue("0x" + kvp.Key.ToString("X4"), bk.usScanCode, Microsoft.Win32.RegistryValueKind.DWord);
						break;
					case BindAction ba:
						rkMagicRemoteServiceKeyBindAction.SetValue("0x" + kvp.Key.ToString("X4"), ba.bavValue, Microsoft.Win32.RegistryValueKind.DWord);
						break;
				}
			}

		}
		public static void AppExtract(
			string strVersion,
			MagicRemoteService.WebOSCLIDeviceInput wcdiInput,
			System.Net.IPAddress ipaSendIP,
			decimal dSendPort,
			System.Net.IPAddress ipaMask,
			MagicRemoteService.PhysicalAddress paPCMac,
			decimal dTimeoutRightClick
		) {
			if(System.IO.Directory.Exists(".\\TV")) {
				System.IO.Directory.Delete(".\\TV", true);
			}
			System.IO.Directory.CreateDirectory(".\\TV");
			System.IO.Directory.CreateDirectory(".\\TV\\MagicRemoteService");
			System.IO.Directory.CreateDirectory(".\\TV\\MagicRemoteService\\webOSTVjs-1.2.4");
			System.IO.Directory.CreateDirectory(".\\TV\\MagicRemoteService\\ressources");
			System.IO.Directory.CreateDirectory(".\\TV\\MagicRemoteService\\ressources\\fr");
			System.IO.Directory.CreateDirectory(".\\TV\\Send");
			System.IO.File.WriteAllText(".\\TV\\MagicRemoteService\\main.js", MagicRemoteService.Properties.Resources.main
				.Replace("const uiRightClick = 1500;", "const uiRightClick = " + dTimeoutRightClick.ToString() + ";")
				.Replace("const sInputId = \"HDMI_1\";", "const sInputId = \"" + wcdiInput.Id + "\";")
				.Replace("const sInputName = \"HDMI 1\";", "const sInputName = \"" + wcdiInput.Name + "\";")
				.Replace("const sInputSource = \"ext://hdmi:1\";", "const sInputSource = \"" + wcdiInput.Source + "\";")
				.Replace("const sInputAppId = \"com.webos.app.hdmi1\";", "const sInputAppId = \"" + wcdiInput.AppId + "\";")
				.Replace("const sIP = \"127.0.0.1\";", "const sIP = \"" + ipaSendIP.ToString() + "\";")
				.Replace("const uiPort = 41230;", "const uiPort = " + dSendPort.ToString() + ";")
				.Replace("const sMask = \"255.255.255.0\";", "const sMask = \"" + ipaMask.ToString() + "\";")
				.Replace("const sMac = \"AA:AA:AA:AA:AA:AA\";", "const sMac = \"" + paPCMac.ToString() + "\";")
			);
			System.IO.File.WriteAllText(".\\TV/MagicRemoteService\\index.html", MagicRemoteService.Properties.Resources.index);
			System.IO.File.WriteAllText(".\\TV/MagicRemoteService\\appinfo.json", MagicRemoteService.Properties.Resources.appinfo
				.Replace("\"id\": \"com.cathwyler.magicremoteservice\"", "\"id\": \"com.cathwyler.magicremoteservice." + wcdiInput.AppIdShort + "\"")
				.Replace("\"version\": \"1.0.0\"", "\"version\": \"" + strVersion + "\"")
			);
			System.IO.File.WriteAllText(".\\TV/MagicRemoteService\\ressource.js", MagicRemoteService.Properties.Resources.ressource);
			System.IO.File.WriteAllBytes(".\\TV/MagicRemoteService\\icon.png", MagicRemoteService.Properties.Resources.icon);
			System.IO.File.WriteAllBytes(".\\TV/MagicRemoteService\\largeIcon.png", MagicRemoteService.Properties.Resources.largeIcon);
			System.IO.File.WriteAllBytes(".\\TV/MagicRemoteService\\MuseoSans-Medium.ttf", MagicRemoteService.Properties.Resources.MuseoSans_Medium);
			System.IO.File.WriteAllText(".\\TV\\MagicRemoteService\\webOSTVjs-1.2.4\\webOSTV-dev.js", MagicRemoteService.Properties.Resources.webOSTV_dev);
			System.IO.File.WriteAllText(".\\TV\\MagicRemoteService\\webOSTVjs-1.2.4\\webOSTV.js", MagicRemoteService.Properties.Resources.webOSTV);
			System.IO.File.WriteAllText(".\\TV\\MagicRemoteService\\ressources\\fr\\appinfo.json", MagicRemoteService.Properties.Resources.frappinfo
				.Replace("\"version\": \"1.0.0\"", "\"version\": \"" + strVersion + "\"")
			);
			System.IO.File.WriteAllText(".\\TV\\Send\\package.json", MagicRemoteService.Properties.Resources.package
				.Replace("\"name\": \"com.cathwyler.magicremoteservice.send\"", "\"name\": \"com.cathwyler.magicremoteservice." + wcdiInput.AppIdShort + ".send\"")
				.Replace("\"version\": \"1.0.0\"", "\"version\": \"" + strVersion + "\"")
			);
			System.IO.File.WriteAllText(".\\TV\\Send\\send.js", MagicRemoteService.Properties.Resources.send);
			System.IO.File.WriteAllText(".\\TV\\Send\\services.json", MagicRemoteService.Properties.Resources.services
				.Replace("\"id\": \"com.cathwyler.magicremoteservice.send\"", "\"id\": \"com.cathwyler.magicremoteservice." + wcdiInput.AppIdShort + ".send\"")
				.Replace("\"name\": \"com.cathwyler.magicremoteservice.send\"", "\"name\": \"com.cathwyler.magicremoteservice." + wcdiInput.AppIdShort + ".send\"")
			);
		}
		private async void btnPCSave_Click(object sender, System.EventArgs e) {
			this.Enabled = false;
			string strError = null;
			string strErrorInfo = null;
			if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
				try {
					if(this.mrsService.Type == MagicRemoteService.ServiceType.Both) {
						this.mrsService.ServiceStop();
					}
					this.PCDataSave();
					if(this.mrsService.Type == MagicRemoteService.ServiceType.Both) {
						this.mrsService.ServiceStart();
					}
					return true;
				} catch(System.Exception ex) {
					strError = MagicRemoteService.Properties.Resources.SettingPCSaveErrorTittle;
					strErrorInfo = ex.Message;
					return false;
				}
			})) {
				this.PCDataRefresh();
				this.Enabled = true;
				System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			} else {
				this.PCDataRefresh();
				this.Enabled = true;
			}
		}
		private void btnClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}
		private async void btnTVRefresh_Click(object sender, System.EventArgs e) {
			this.Enabled = false;
			string sName = ((MagicRemoteService.WebOSCLIDevice)this.cmbboxTV.SelectedItem)?.Name;
			MagicRemoteService.WebOSCLIDevice[] tabDevice = null;
			string strError = null;
			string strErrorInfo = null;
			if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
				try {
					tabDevice = MagicRemoteService.WebOSCLI.SetupDeviceList();
					return true;
				} catch(System.Exception ex) {
					strError = MagicRemoteService.Properties.Resources.SettingTVRefreshlErrorTitle;
					strErrorInfo = ex.Message;
					return false;
				}
			})) {
				System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				this.Enabled = true;
			} else {
				this.cmbboxTV.DataSource = tabDevice;
				if(sName != null) {
					this.cmbboxTV.SelectedValue = sName;
				}
				if(this.cmbboxTV.SelectedItem == null) {
					this.cmbboxTV.SelectedIndex = 0;
				}
				this.Enabled = true;
			}
		}
		private async void btnTVInstall_Click(object sender, System.EventArgs e) {
			if(this.cmbboxTV.SelectedItem == null) {
				ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingTVSelectErrorTitle;
				ttFormating.Show("", this.cmbboxTV);
				ttFormating.Show(MagicRemoteService.Properties.Resources.SettingTVSelectErrorMessage, this.cmbboxTV);
			} else if(this.cmbboxInput.SelectedItem == null) {
				ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingInputSelectErrorTitle;
				ttFormating.Show("", this.cmbboxInput);
				ttFormating.Show(MagicRemoteService.Properties.Resources.SettingInputSelectErrorMessage, this.cmbboxInput);
			} else if(this.ipadrboxSendIP.Value == null) {
				ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingSendIPErrorTitle;
				ttFormating.Show("", this.ipadrboxSendIP);
				ttFormating.Show(MagicRemoteService.Properties.Resources.SettingSendIPErrorMessage, this.ipadrboxSendIP);
			} else if(this.ipadrboxSubnetMask.Value == null) {
				ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingSubnetMaskErrorTitle;
				ttFormating.Show("", this.ipadrboxSubnetMask);
				ttFormating.Show(MagicRemoteService.Properties.Resources.SettingSubnetMaskErrorMessage, this.ipadrboxSubnetMask);
			} else if(this.phyadrboxPCMac.Value == null) {
				ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingPCMacErrorTitle;
				ttFormating.Show("", this.phyadrboxPCMac);
				ttFormating.Show(MagicRemoteService.Properties.Resources.SettingPCMacErrorMessage, this.phyadrboxPCMac);
			} else {
				this.Enabled = false;
				MagicRemoteService.WebOSCLIDevice wcdDevice = (MagicRemoteService.WebOSCLIDevice)this.cmbboxTV.SelectedItem;
				decimal dTimeoutRightClick = this.numboxTimeoutRightClick.Value;
				MagicRemoteService.WebOSCLIDeviceInput wcdiInput = (MagicRemoteService.WebOSCLIDeviceInput)this.cmbboxInput.SelectedItem;
				System.Net.IPAddress ipaSendIP = this.ipadrboxSendIP.Value;
				decimal dSendPort = this.numboxSendPort.Value;
				System.Net.IPAddress ipMask = this.ipadrboxSubnetMask.Value;
				MagicRemoteService.PhysicalAddress macPCMac = this.phyadrboxPCMac.Value;
				string strError = null;
				string strErrorInfo = null;
				if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
					try {
						System.Version vAssembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
						string strVersion = vAssembly.Major + "." + vAssembly.Minor + "." + vAssembly.Build;
						MagicRemoteService.Setting.AppExtract(strVersion, wcdiInput, ipaSendIP, dSendPort, ipMask, macPCMac, dTimeoutRightClick);
						string strTVDir = System.IO.Path.GetFullPath(".\\TV");
						MagicRemoteService.WebOSCLI.Package(strTVDir, MagicRemoteService.Application.CompleteDir(strTVDir) + "MagicRemoteService", MagicRemoteService.Application.CompleteDir(strTVDir) + "Send");
						MagicRemoteService.WebOSCLI.Install(wcdDevice.Name, ".\\TV\\com.cathwyler.magicremoteservice." + wcdiInput.AppIdShort + "_" + strVersion + "_all.ipk");
						MagicRemoteService.WebOSCLI.Launch(wcdDevice.Name, "com.cathwyler.magicremoteservice." + wcdiInput.AppIdShort);
						return true;
					} catch(System.Exception ex) {
						strError = MagicRemoteService.Properties.Resources.SettingTVInstallErrorTitle;
						strErrorInfo = ex.Message;
						return false;
					}
				})) {
					this.Enabled = true;
					System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				} else {
					this.TVDataSave();
					this.TVDataRefresh();
					this.Enabled = true;
				}
			}
		}
		private async void cmbboxTV_SelectedIndexChanged(object sender, System.EventArgs e) {
			this.Enabled = false;
			string sId = ((MagicRemoteService.WebOSCLIDeviceInput)this.cmbboxInput.SelectedItem)?.Id;
			MagicRemoteService.WebOSCLIDeviceInput[] tabDeviceInput = null;
			string strError = null;
			string strErrorInfo = null;
			if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
				try {
					tabDeviceInput = MagicRemoteService.WebOSCLI.InputList();
					return true;
				} catch(System.Exception ex) {
					strError = MagicRemoteService.Properties.Resources.SettingInputRefreshlErrorTitle;
					strErrorInfo = ex.Message;
					return false;
				}
			})) {
				this.Enabled = true;
				System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			} else {
				this.cmbboxInput.DataSource = tabDeviceInput;
				if(sId != null) {
					this.cmbboxInput.SelectedValue = sId;
				}
				if(this.cmbboxInput.SelectedItem == null) {
					this.cmbboxInput.SelectedIndex = 0;
				}
				this.TVDataRefresh();
				this.Enabled = true;
			}
		}

		private void Setting_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			this.ttFormating.Hide(this);
		}

		private void Setting_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
			if(!e.Cancel && (
				this.dListenPort != this.numboxListenPort.Value
				||
				this.bInactivity != this.chkboxInactivity.Checked
				||
				this.dTimeoutInactivity != this.numboxTimeoutInactivity.Value
				||
				this.bStartup != this.chkboxStartup.Checked
			)) {
				switch(System.Windows.Forms.MessageBox.Show(MagicRemoteService.Properties.Resources.SettingPCSaveMessage, this.Text, System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1)) {
					case System.Windows.Forms.DialogResult.Yes:
						this.btnPCSave_Click(this, new System.EventArgs());
						break;
					case System.Windows.Forms.DialogResult.No:
						break;
					case System.Windows.Forms.DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
			if(!e.Cancel && (
				this.wcdiInput?.Id != ((MagicRemoteService.WebOSCLIDeviceInput)this.cmbboxInput.SelectedItem)?.Id
				||
				this.scrDisplay?.Id != ((MagicRemoteService.Screen)this.cmbboxDisplay.SelectedItem)?.Id
				||
				this.iaSendIP?.ToString() != this.ipadrboxSendIP.Value?.ToString()
				||
				this.dSendPort != this.numboxSendPort.Value
				||
				this.iaSubnetMask?.ToString() != this.ipadrboxSubnetMask.Value?.ToString()
				||
				this.paPCMac?.ToString() != this.phyadrboxPCMac.Value?.ToString()
				||
				this.dTimeoutRightClick != this.numboxTimeoutRightClick.Value
				||
				this.bExtend != this.chkboxExtend.Checked
			)) {
				switch(System.Windows.Forms.MessageBox.Show(MagicRemoteService.Properties.Resources.SettingTVSaveMessage, this.Text, System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1)) {
					case System.Windows.Forms.DialogResult.Yes:
						this.btnTVInstall_Click(this, new System.EventArgs());
						break;
					case System.Windows.Forms.DialogResult.No:
						break;
					case System.Windows.Forms.DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
			if(!e.Cancel && System.Linq.Enumerable.Any<System.Collections.Generic.KeyValuePair<ushort, BindControl>>(this.dKeyBindControl, delegate(System.Collections.Generic.KeyValuePair<ushort, BindControl> kvp){
				return this.dKeyBind[kvp.Key] != kvp.Value.Value;
			})) {
				switch(System.Windows.Forms.MessageBox.Show(MagicRemoteService.Properties.Resources.SettingRemoteSaveMessage, this.Text, System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1)) {
					case System.Windows.Forms.DialogResult.Yes:
						this.btnRemoteSave_Click(this, new System.EventArgs());
						break;
					case System.Windows.Forms.DialogResult.No:
						break;
					case System.Windows.Forms.DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
		}

		private async void btnRemoteSave_Click(object sender, System.EventArgs e) {
			this.Enabled = false;
			string strError = null;
			string strErrorInfo = null;
			if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
				try {
					if(this.mrsService.Type == MagicRemoteService.ServiceType.Both) {
						this.mrsService.ServiceStop();
					}
					this.RemoteDataSave();
					if(this.mrsService.Type == MagicRemoteService.ServiceType.Both) {
						this.mrsService.ServiceStart();
					}
					return true;
				} catch(System.Exception ex) {
					strError = MagicRemoteService.Properties.Resources.SettingRemoteSaveErrorTittle;
					strErrorInfo = ex.Message;
					return false;
				}
			})) {
				this.RemoteDataRefresh();
				this.Enabled = true;
				System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			} else {
				this.RemoteDataRefresh();
				this.Enabled = true;
			}
		}

		private async void btnTVAdd_Click(object sender, System.EventArgs e) {
			MagicRemoteService.TV win = new MagicRemoteService.TV();
			switch(win.ShowDialog()) {
				case System.Windows.Forms.DialogResult.OK:
					this.Enabled = false;
					string strError = null;
					string strErrorInfo = null;
					if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
						try {
							MagicRemoteService.WebOSCLI.SetupDeviceAdd(win.wcdTV, win.strPassword);
							if(!win.bAdvanced) {
								MagicRemoteService.WebOSCLI.NovacomGetKey(win.wcdTV.Name, win.wcdTV.DeviceDetail.Passphrase);
							}
							return true;
						} catch(System.Exception ex) {
							strError = MagicRemoteService.Properties.Resources.SettingTVAddErrorTitle;
							strErrorInfo = ex.Message;
							return false;
						}
					})) {
						this.Enabled = true;
						System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
					} else {
						this.Enabled = true;
						this.btnTVRefresh_Click(this, new System.EventArgs());
					}
					break;
				default:
					break;
			}
		}

		private async void btnTVModify_Click(object sender, System.EventArgs e) {
			if(this.cmbboxTV.SelectedItem == null) {
				ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingTVSelectErrorTitle;
				ttFormating.Show("", this.cmbboxTV);
				ttFormating.Show(MagicRemoteService.Properties.Resources.SettingTVSelectErrorMessage, this.cmbboxTV);
			} else {
				MagicRemoteService.WebOSCLIDevice wcdDevice = (MagicRemoteService.WebOSCLIDevice)this.cmbboxTV.SelectedItem;
				MagicRemoteService.TV win = new MagicRemoteService.TV(wcdDevice);
				switch(win.ShowDialog()) {
					case System.Windows.Forms.DialogResult.OK:
						this.Enabled = false;
						string strError = null;
						string strErrorInfo = null;
						if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
							try {
								MagicRemoteService.WebOSCLI.SetupDeviceModify(wcdDevice.Name, win.wcdTV, win.strPassword);
								if (!win.bAdvanced) {
									MagicRemoteService.WebOSCLI.NovacomGetKey(win.wcdTV.Name, win.wcdTV.DeviceDetail.Passphrase);
								}
								return true;
							} catch(System.Exception ex) {
								strError = MagicRemoteService.Properties.Resources.SettingTVModifyErrorTitle;
								strErrorInfo = ex.Message;
								return false;
							}
						})) {
							this.Enabled = true;
							System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
						} else {
							this.Enabled = true;
							this.btnTVRefresh_Click(this, new System.EventArgs());
						}
						break;
					default:
						break;
				}
			}
		}

		private async void btnTVRemove_Click(object sender, System.EventArgs e) {
			if(this.cmbboxTV.SelectedItem == null) {
				ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingTVSelectErrorTitle;
				ttFormating.Show("", this.cmbboxTV);
				ttFormating.Show(MagicRemoteService.Properties.Resources.SettingTVSelectErrorMessage, this.cmbboxTV);
			} else {
				this.Enabled = false;
				MagicRemoteService.WebOSCLIDevice wcdDevice = (MagicRemoteService.WebOSCLIDevice)this.cmbboxTV.SelectedItem;
				string strError = null;
				string strErrorInfo = null;
				if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
					try {
						MagicRemoteService.WebOSCLI.SetupDeviceRemove(wcdDevice.Name);
						return true;
					} catch(System.Exception ex) {
						strError = MagicRemoteService.Properties.Resources.SettingTVRemoveErrorTitle;
						strErrorInfo = ex.Message;
						return false;
					}
				})) {
					this.Enabled = true;
					System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				} else {
					this.Enabled = true;
					this.btnTVRefresh_Click(this, new System.EventArgs());
				}
			}
		}
	}
}
