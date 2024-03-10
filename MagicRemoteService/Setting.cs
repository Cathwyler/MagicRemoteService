
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

		private System.Collections.Generic.Dictionary<ushort, BindControl> dBindControl;

		private decimal dListenPort;
		private bool bInactivity;
		private decimal dTimeoutInactivity;
		private bool bStartup;

		private MagicRemoteService.WebOSCLIDeviceInput wocdiInput;
		private MagicRemoteService.Screen scrDisplay;
		private System.Net.IPAddress iaSendIP;
		private decimal dSendPort;
		private System.Net.IPAddress iaSubnetMask;
		private MagicRemoteService.PhysicalAddress paPCMac;
		private decimal dLongClick;
		private bool bInputDirect;
		private bool bOverlay;
		private bool bExtend;

		private System.Collections.Generic.Dictionary<ushort, Bind[]> dBind = new System.Collections.Generic.Dictionary<ushort, Bind[]>() {
			{ 0x0001, null },
			{ 0x0002, null },
			{ 0x0008, null },
			{ 0x000D, null },
			{ 0x0021, null },
			{ 0x0022, null },
			{ 0x0025, null },
			{ 0x0026, null },
			{ 0x0027, null },
			{ 0x0028, null },
			{ 0x0030, null },
			{ 0x0031, null },
			{ 0x0032, null },
			{ 0x0033, null },
			{ 0x0034, null },
			{ 0x0035, null },
			{ 0x0036, null },
			{ 0x0037, null },
			{ 0x0038, null },
			{ 0x0039, null },
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
			this.libVersion.Text = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			this.dBindControl = new System.Collections.Generic.Dictionary<ushort, BindControl>() {
				{ 0x0001, this.bcRemoteClick },
				{ 0x0002, this.bcRemoteLongClick },
				{ 0x0008, this.bcRemoteBackspace },
				{ 0x000D, this.bcRemoteOk },
				{ 0x0021, this.bcRemoteChannelUp },
				{ 0x0022, this.bcRemoteChannelDown },
				{ 0x0025, this.bcRemoteLeft },
				{ 0x0026, this.bcRemoteUp },
				{ 0x0027, this.bcRemoteRight },
				{ 0x0028, this.bcRemoteDown },
				{ 0x0030, this.bcRemote0 },
				{ 0x0031, this.bcRemote1 },
				{ 0x0032, this.bcRemote2 },
				{ 0x0033, this.bcRemote3 },
				{ 0x0034, this.bcRemote4 },
				{ 0x0035, this.bcRemote5 },
				{ 0x0036, this.bcRemote6 },
				{ 0x0037, this.bcRemote7 },
				{ 0x0038, this.bcRemote8 },
				{ 0x0039, this.bcRemote9 },
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
			this.cbbDisplay.DataSource = new System.Collections.Generic.List<Screen>(System.Linq.Enumerable.Concat<Screen>(new System.Collections.Generic.List<Screen>() { Screen.PrimaryDefaut }, MagicRemoteService.Screen.AllScreen.Values));
			this.PCDataRefresh();
			this.TVDataRefresh();
			this.RemoteDataRefresh();
			this.TVRefresh_Click(this, new System.EventArgs());
		}
		private new bool Enabled {
			set {
				this.UseWaitCursor = !value;
				this.tabSetting.Enabled = value;
			}
		}
		public void PCDataRefresh() {
			Microsoft.Win32.RegistryKey rkMagicRemoteService = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey(@"Software\MagicRemoteService");
			if(rkMagicRemoteService == null) {
				this.nbListenPort.Value = 41230;
				this.cbInactivity.Checked = true;
				this.nbTimeoutInactivity.Value = 7200000;
			} else {
				this.nbListenPort.Value = (int)rkMagicRemoteService.GetValue("Port", 41230);
				this.cbInactivity.Checked = (int)rkMagicRemoteService.GetValue("Inactivity", 1) != 0;
				this.nbTimeoutInactivity.Value = (int)rkMagicRemoteService.GetValue("TimeoutInactivity", 7200000);
			}
			this.dListenPort = this.nbListenPort.Value;
			this.bInactivity = this.cbInactivity.Checked;
			this.dTimeoutInactivity = this.nbTimeoutInactivity.Value;

			if(MagicRemoteService.Program.bElevated) {
				System.ServiceProcess.ServiceController scService = System.Array.Find<System.ServiceProcess.ServiceController>(System.ServiceProcess.ServiceController.GetServices(), delegate (System.ServiceProcess.ServiceController sc) {
					return sc.ServiceName == "MagicRemoteService";
				});
				if(scService == null) {
					this.cbStartup.Checked = true;
				} else {
					this.cbStartup.Checked = scService.StartType == System.ServiceProcess.ServiceStartMode.Automatic;
				}
			} else {
				TaskScheduler.ITaskService ts = (TaskScheduler.ITaskService)System.Activator.CreateInstance(System.Type.GetTypeFromProgID("Schedule.Service"));
				ts.Connect();
				TaskScheduler.IRegisteredTask rtStartup = null;
				foreach(TaskScheduler.IRegisteredTask rt in ts.GetFolder(@"\").GetTasks(0)) {
					if(rt.Name == "MagicRemoteService") {
						rtStartup = rt;
					}
				}
				if(rtStartup == null) {
					this.cbStartup.Checked = true;
				} else {
					this.cbStartup.Checked = rtStartup.Enabled;
				}
			}
			this.bStartup = this.cbStartup.Checked;
		}
		public void PCDataSave() {
			Microsoft.Win32.RegistryKey rkMagicRemoteService = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey(@"Software\MagicRemoteService");
			rkMagicRemoteService.SetValue("Port", this.nbListenPort.Value, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteService.SetValue("Inactivity", this.cbInactivity.Checked, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteService.SetValue("TimeoutInactivity", this.nbTimeoutInactivity.Value, Microsoft.Win32.RegistryValueKind.DWord);

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
				System.Configuration.Install.AssemblyInstaller ai = new System.Configuration.Install.AssemblyInstaller(System.Reflection.Assembly.GetExecutingAssembly(), this.cbStartup.Checked ? new string[] { "enable" } : new string[] { });
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
				tdStartup.Settings.Enabled = this.cbStartup.Checked;
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
				ts.GetFolder(@"\").RegisterTaskDefinition("MagicRemoteService", tdStartup, (int)TaskScheduler._TASK_CREATION.TASK_CREATE_OR_UPDATE, null, null, TaskScheduler._TASK_LOGON_TYPE.TASK_LOGON_NONE);
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
			if(this.cbbTV.SelectedItem == null) {
				this.cbbInput.Enabled = false;
				this.cbbDisplay.Enabled = false;
				this.iabSendIP.Enabled = false;
				this.nbSendPort.Enabled = false;
				this.iabSubnetMask.Enabled = false;
				this.pabPCMac.Enabled = false;
				this.nbLongClick.Enabled = false;
				this.cbInputDirect.Enabled = false;
				this.cbOverlay.Enabled = false;
				this.cbExtend.Enabled = false;
				this.btnTVInstall.Enabled = false;
				this.btnTVInspect.Enabled = false;
				this.cbbInput.SelectedItem = null;
				this.cbbDisplay.SelectedItem = null;
				this.iabSendIP.Value = MagicRemoteService.Setting.ipaSendIPDefaut;
				this.nbSendPort.Value = this.nbListenPort.Value;
				this.iabSubnetMask.Value = MagicRemoteService.Setting.ipaMaskDefaut;
				this.pabPCMac.Value = MagicRemoteService.Setting.paMacDefaut;
				this.nbLongClick.Value = 1500;
				this.cbInputDirect.Checked = true;
				this.cbOverlay.Checked = false;
				this.cbExtend.Checked = false;
			} else {
				this.cbbInput.Enabled = true;
				this.cbbDisplay.Enabled = true;
				this.iabSendIP.Enabled = true;
				this.nbSendPort.Enabled = true;
				this.iabSubnetMask.Enabled = true;
				this.pabPCMac.Enabled = true;
				this.nbLongClick.Enabled = true;
				this.cbInputDirect.Enabled = true;
				this.cbOverlay.Enabled = true;
				this.cbExtend.Enabled = true;
				this.btnTVInstall.Enabled = true;
				this.btnTVInspect.Enabled = true;
				Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey(@"Software\MagicRemoteService\Device\" + ((MagicRemoteService.WebOSCLIDevice)this.cbbTV.SelectedItem).Name);
				if(rkMagicRemoteServiceDevice == null) {
					this.cbbInput.SelectedIndex = 0;
					this.cbbDisplay.SelectedIndex = 0;
					this.iabSendIP.Value = MagicRemoteService.Setting.ipaSendIPDefaut;
					this.nbSendPort.Value = this.nbListenPort.Value;
					this.iabSubnetMask.Value = MagicRemoteService.Setting.ipaMaskDefaut;
					this.pabPCMac.Value = MagicRemoteService.Setting.paMacDefaut;
					this.nbLongClick.Value = 1500;
					this.cbInputDirect.Checked = true;
					this.cbOverlay.Checked = false;
					this.cbExtend.Checked = false;
				} else {
					string sInputId = (string)rkMagicRemoteServiceDevice.GetValue("InputId");
					if(sInputId == null) {
						this.cbbInput.SelectedIndex = 0;
					} else {
						this.cbbInput.SelectedValue = sInputId;
					}
					this.cbbDisplay.SelectedValue = (uint)(int)rkMagicRemoteServiceDevice.GetValue("Display", 0);
					string sIp = (string)rkMagicRemoteServiceDevice.GetValue("SendIp");
					if(sIp == null) {
						this.iabSendIP.Value = MagicRemoteService.Setting.ipaSendIPDefaut;
					} else {
						this.iabSendIP.FromString(sIp);
					}
					this.nbSendPort.Value = (int)rkMagicRemoteServiceDevice.GetValue("SendPort", (int)this.nbListenPort.Value);
					string sMask = (string)rkMagicRemoteServiceDevice.GetValue("Mask");
					if(sMask == null) {
						this.iabSubnetMask.Value = MagicRemoteService.Setting.ipaMaskDefaut;
					} else {
						this.iabSubnetMask.FromString(sMask);
					}
					string sMac = (string)rkMagicRemoteServiceDevice.GetValue("PCMac");
					if(sMac == null) {
						this.pabPCMac.Value = MagicRemoteService.Setting.paMacDefaut;
					} else {
						this.pabPCMac.FromString(sMac);
					}
					this.nbLongClick.Value = (int)rkMagicRemoteServiceDevice.GetValue("LongClick", 1500);
					this.cbInputDirect.Checked = ((int)rkMagicRemoteServiceDevice.GetValue("InputDirect", 1)) != 0;
					this.cbOverlay.Checked = ((int)rkMagicRemoteServiceDevice.GetValue("Overlay", 0)) != 0;
					this.cbExtend.Checked = ((int)rkMagicRemoteServiceDevice.GetValue("Extend", 0)) != 0;
				}
			}
			this.wocdiInput = (MagicRemoteService.WebOSCLIDeviceInput)this.cbbInput.SelectedItem;
			this.scrDisplay = (MagicRemoteService.Screen)this.cbbDisplay.SelectedItem;
			this.iaSendIP = this.iabSendIP.Value;
			this.dSendPort = this.nbSendPort.Value;
			this.iaSubnetMask = this.iabSubnetMask.Value;
			this.paPCMac = this.pabPCMac.Value;
			this.dLongClick = this.nbLongClick.Value;
			this.bInputDirect = this.cbInputDirect.Checked;
			this.bOverlay = this.cbOverlay.Checked;
			this.bExtend = this.cbExtend.Checked;
		}
		public void TVDataSave() {
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey(@"Software\MagicRemoteService\Device\" + ((MagicRemoteService.WebOSCLIDevice)this.cbbTV.SelectedItem).Name);
			rkMagicRemoteServiceDevice.SetValue("InputId", ((MagicRemoteService.WebOSCLIDeviceInput)this.cbbInput.SelectedItem).Id, Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("Display", ((MagicRemoteService.Screen)this.cbbDisplay.SelectedItem).Id, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteServiceDevice.SetValue("SendIp", this.iabSendIP.Value.ToString(), Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("SendPort", this.nbSendPort.Value, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteServiceDevice.SetValue("Mask", this.iabSubnetMask.Value.ToString(), Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("PCMac", this.pabPCMac.Value.ToString(), Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("LongClick", this.nbLongClick.Value, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteServiceDevice.SetValue("InputDirect", this.cbInputDirect.Checked, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteServiceDevice.SetValue("Overlay", this.cbOverlay.Checked, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteServiceDevice.SetValue("Extend", this.cbExtend.Checked, Microsoft.Win32.RegistryValueKind.DWord);
		}
		public void RemoteDataRefresh() {
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceRemoteBind = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey(@"Software\MagicRemoteService\Remote\Bind");
			if(rkMagicRemoteServiceRemoteBind == null) {
				this.dBindControl[0x0001].Value = new Bind[] { new BindMouse(BindMouseValue.Left) };
				this.dBindControl[0x0002].Value = new Bind[] { new BindMouse(BindMouseValue.Right) };
				this.dBindControl[0x0008].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Back, 0x0E, false) };
				this.dBindControl[0x000D].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Enter, 0x1C, false) };
				this.dBindControl[0x0021].Value = null;
				this.dBindControl[0x0022].Value = null;
				this.dBindControl[0x0025].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Left, 0x4B, true) };
				this.dBindControl[0x0026].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Up, 0x48, true) };
				this.dBindControl[0x0027].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Right, 0x4D, true) };
				this.dBindControl[0x0028].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Down, 0x50, true) };
				this.dBindControl[0x0030].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad0, 0x52, false) };
				this.dBindControl[0x0031].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad1, 0x4F, false) };
				this.dBindControl[0x0032].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad2, 0x50, false) };
				this.dBindControl[0x0033].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad3, 0x51, false) };
				this.dBindControl[0x0034].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad4, 0x4B, false) };
				this.dBindControl[0x0035].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad5, 0x4C, false) };
				this.dBindControl[0x0036].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad6, 0x4D, false) };
				this.dBindControl[0x0037].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad7, 0x47, false) };
				this.dBindControl[0x0038].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad8, 0x48, false) };
				this.dBindControl[0x0039].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad9, 0x49, false) };
				this.dBindControl[0x0193].Value = new Bind[] { new BindAction(BindActionValue.Shutdown) };
				this.dBindControl[0x0194].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.LWin, 0x5B, true) };
				this.dBindControl[0x0195].Value = new Bind[] { new BindMouse(BindMouseValue.Right) };
				this.dBindControl[0x0196].Value = new Bind[] { new BindAction(BindActionValue.Keyboard) };
				this.dBindControl[0x01CD].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Escape, 0x01, false) };
				this.dBindControl[0x019F].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Play, 0x00, false) };
				this.dBindControl[0x0013].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Pause, 0x00, false) };
				this.dBindControl[0x01A1].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.MediaNextTrack, 0x00, false) };
				this.dBindControl[0x019C].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.MediaPreviousTrack, 0x00, false) };
				this.dBindControl[0x019D].Value = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.MediaStop, 0x00, false) };
			} else {
				foreach(string sKey in rkMagicRemoteServiceRemoteBind.GetSubKeyNames()) {
					System.Collections.Generic.List<Bind> liBind = new System.Collections.Generic.List<Bind>();
					Microsoft.Win32.RegistryKey rkMagicRemoteServiceRemoteBindKey = rkMagicRemoteServiceRemoteBind.OpenSubKey(sKey);
					foreach(string sBind in rkMagicRemoteServiceRemoteBindKey.GetSubKeyNames()) {
						Microsoft.Win32.RegistryKey rkMagicRemoteServiceRemoteBindBind = rkMagicRemoteServiceRemoteBindKey.OpenSubKey(sBind);
						switch((int)rkMagicRemoteServiceRemoteBindBind.GetValue("Kind")) {
							case 0x00 :
								liBind.Add(new BindMouse((BindMouseValue)(int)rkMagicRemoteServiceRemoteBindBind.GetValue("Value", 0x0000)));
								break;
							case 0x01:
								liBind.Add(new BindKeyboard((byte)(int)rkMagicRemoteServiceRemoteBindBind.GetValue("VirtualKey", 0x00), (byte)(int)rkMagicRemoteServiceRemoteBindBind.GetValue("ScanCode", 0x00), (int)rkMagicRemoteServiceRemoteBindBind.GetValue("Extended", 0x00) == 0x01));
								break;
							case 0x02:
								liBind.Add(new BindAction((BindActionValue)(int)rkMagicRemoteServiceRemoteBindBind.GetValue("Value", 0x00)));
								break;
							case 0x03:
								liBind.Add(new BindCommand((string)rkMagicRemoteServiceRemoteBindBind.GetValue("Command")));
								break;
						}
					}
					this.dBindControl[ushort.Parse(sKey.Substring(2), System.Globalization.NumberStyles.HexNumber)].Value = liBind.ToArray();
				}
			}
			foreach(System.Collections.Generic.KeyValuePair<ushort, BindControl> kvp in this.dBindControl) {
				this.dBind[kvp.Key] = kvp.Value.Value;
			}
		}
		public void RemoteDataSave() {
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceRemoteBind = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey(@"Software\MagicRemoteService\Remote\Bind");
			foreach(string sKey in rkMagicRemoteServiceRemoteBind.GetSubKeyNames()) {
				Microsoft.Win32.RegistryKey rkMagicRemoteServiceRemoteBindKey = rkMagicRemoteServiceRemoteBind.CreateSubKey(sKey);
				foreach(string sBind in rkMagicRemoteServiceRemoteBindKey.GetSubKeyNames()) {
					rkMagicRemoteServiceRemoteBindKey.DeleteSubKey(sBind);
				}
				rkMagicRemoteServiceRemoteBind.DeleteSubKey(sKey);
			}
			foreach(System.Collections.Generic.KeyValuePair<ushort, BindControl> kvp in this.dBindControl) {
				if(kvp.Value.Value != null) {
					foreach(Bind b in kvp.Value.Value) {
						Microsoft.Win32.RegistryKey rkMagicRemoteServiceRemoteBindKey = rkMagicRemoteServiceRemoteBind.CreateSubKey("0x" + kvp.Key.ToString("X4"));
						Microsoft.Win32.RegistryKey rkMagicRemoteServiceRemoteBindBind = rkMagicRemoteServiceRemoteBindKey.CreateSubKey(rkMagicRemoteServiceRemoteBindKey.SubKeyCount.ToString());
						switch(b) {
							case MagicRemoteService.BindMouse bm:
								rkMagicRemoteServiceRemoteBindBind.SetValue("Kind", 0x00, Microsoft.Win32.RegistryValueKind.DWord);
								rkMagicRemoteServiceRemoteBindBind.SetValue("Value", bm.bmvValue, Microsoft.Win32.RegistryValueKind.DWord);
								break;
							case MagicRemoteService.BindKeyboard bk:
								rkMagicRemoteServiceRemoteBindBind.SetValue("Kind", 0x01, Microsoft.Win32.RegistryValueKind.DWord);
								rkMagicRemoteServiceRemoteBindBind.SetValue("VirtualKey", bk.ucVirtualKey, Microsoft.Win32.RegistryValueKind.DWord);
								rkMagicRemoteServiceRemoteBindBind.SetValue("ScanCode", bk.ucScanCode, Microsoft.Win32.RegistryValueKind.DWord);
								rkMagicRemoteServiceRemoteBindBind.SetValue("Extended", bk.bExtended, Microsoft.Win32.RegistryValueKind.DWord);
								break;
							case MagicRemoteService.BindAction ba:
								rkMagicRemoteServiceRemoteBindBind.SetValue("Kind", 0x02, Microsoft.Win32.RegistryValueKind.DWord);
								rkMagicRemoteServiceRemoteBindBind.SetValue("Value", ba.bavValue, Microsoft.Win32.RegistryValueKind.DWord);
								break;
							case MagicRemoteService.BindCommand bc:
								rkMagicRemoteServiceRemoteBindBind.SetValue("Kind", 0x03, Microsoft.Win32.RegistryValueKind.DWord);
								rkMagicRemoteServiceRemoteBindBind.SetValue("Command", bc.strCommand, Microsoft.Win32.RegistryValueKind.String);
								break;
						}
					}
				}
			}
		}
		public static void AppExtract(
			string strVersion,
			MagicRemoteService.WebOSCLIDeviceInput wocdiInput,
			System.Net.IPAddress ipaSendIP,
			decimal dSendPort,
			System.Net.IPAddress ipaMask,
			MagicRemoteService.PhysicalAddress paPCMac,
			decimal dLongClick,
			bool bInputDirect,
			bool bOverlay
		) {
			if(System.IO.Directory.Exists(@".\TV")) {
				System.IO.Directory.Delete(@".\TV", true);
			}
			System.IO.Directory.CreateDirectory(@".\TV");
			System.IO.Directory.CreateDirectory(@".\TV\MagicRemoteService");
			System.IO.Directory.CreateDirectory(@".\TV\MagicRemoteService\webOSTVjs-1.2.8");
			System.IO.Directory.CreateDirectory(@".\TV\MagicRemoteService\resources");
			System.IO.Directory.CreateDirectory(@".\TV\MagicRemoteService\resources\fr");
			System.IO.Directory.CreateDirectory(@".\TV\MagicRemoteService\resources\es");
			System.IO.Directory.CreateDirectory(@".\TV\Send");
			System.IO.File.WriteAllText(@".\TV\MagicRemoteService\main.js", MagicRemoteService.Properties.Resources.main
#if DEBUG
				.Replace(@"const bDebug = false;", @"const bDebug = true;")
#endif
				.Replace(@"const bInputDirect = true", @"const bInputDirect = " + (bInputDirect ? "true" : "false"))
				.Replace(@"const bOverlay = true", @"const bOverlay = " + (bOverlay ? "true" : "false"))
				.Replace(@"const uiLongClick = 1500", @"const uiLongClick = " + dLongClick.ToString())
				.Replace(@"const strInputId = ""HDMI""", @"const strInputId = """ + wocdiInput.Id + @"""")
				.Replace(@"const strInputAppId = ""com.webos.app.hdmi""", @"const strInputAppId = ""com.webos.app." + wocdiInput.AppIdShort + @"""")
				.Replace(@"const strInputName = ""HDMI""", @"const strInputName = """ + wocdiInput.Name + @"""")
				.Replace(@"const strInputSource = ""ext://hdmi""", @"const strInputSource = """ + wocdiInput.Source + @"""")
				.Replace(@"const strIP = ""127.0.0.1""", @"const strIP = """ + ipaSendIP.ToString() + @"""")
				.Replace(@"const uiPort = 41230", @"const uiPort = " + dSendPort.ToString())
				.Replace(@"const strMask = ""255.255.255.0""", @"const strMask = """ + ipaMask.ToString() + @"""")
				.Replace(@"const strMac = ""AA:AA:AA:AA:AA:AA""", @"const strMac = """ + paPCMac.ToString() + @"""")
				.Replace(@"const strAppId = ""com.cathwyler.magicremoteservice""", @"const strAppId = ""com.cathwyler.magicremoteservice." + wocdiInput.AppIdShort + @"""")
			);
			System.IO.File.WriteAllText(@".\TV\MagicRemoteService\index.html", MagicRemoteService.Properties.Resources.index);
			System.IO.File.WriteAllText(@".\TV\MagicRemoteService\appinfo.json", MagicRemoteService.Properties.Resources.appinfo
				.Replace(@"""id"": ""com.cathwyler.magicremoteservice""", @"""id"": ""com.cathwyler.magicremoteservice." + wocdiInput.AppIdShort + @"""")
				.Replace(@"""version"": ""1.0.0""", @"""version"": """ + strVersion + @"""")
				.Replace(@"""appDescription"": ""HDMI""", @"""appDescription"": """ + wocdiInput.Name + @"""")
				.Replace(@"""defaultWindowType"": ""floating""", @"""defaultWindowType"": """ + (bOverlay ? "floating" : "card") + @"""")
				.Replace(@"""noSplashOnLaunch"": true", @"""noSplashOnLaunch"": " + (bOverlay ? "true" : "false"))
			);
			System.IO.File.WriteAllText(@".\TV\MagicRemoteService\appstring.json", MagicRemoteService.Properties.Resources.appstring
				.Replace(@"""strAppDescription"": ""HDMI""", @"""strAppDescription"": """ + wocdiInput.Name + @"""")
			);
			System.IO.File.WriteAllBytes(@".\TV\MagicRemoteService\icon.png", MagicRemoteService.Properties.Resources.icon);
			System.IO.File.WriteAllBytes(@".\TV\MagicRemoteService\miniIcon.png", MagicRemoteService.Properties.Resources.miniIcon);
			System.IO.File.WriteAllBytes(@".\TV\MagicRemoteService\largeIcon.png", MagicRemoteService.Properties.Resources.largeIcon);
			System.IO.File.WriteAllBytes(@".\TV\MagicRemoteService\MuseoSans-Medium.ttf", MagicRemoteService.Properties.Resources.MuseoSans_Medium);
			System.IO.File.WriteAllText(@".\TV\MagicRemoteService\webOSTVjs-1.2.8\webOSTV-dev.js", MagicRemoteService.Properties.Resources.webOSTV_dev);
			System.IO.File.WriteAllText(@".\TV\MagicRemoteService\webOSTVjs-1.2.8\webOSTV.js", MagicRemoteService.Properties.Resources.webOSTV);
			System.IO.File.WriteAllText(@".\TV\MagicRemoteService\resources\fr\appinfo.json", MagicRemoteService.Properties.Resources.frappinfo);
			System.IO.File.WriteAllText(@".\TV\MagicRemoteService\resources\fr\appstring.json", MagicRemoteService.Properties.Resources.frappstring);
			System.IO.File.WriteAllText(@".\TV\MagicRemoteService\resources\es\appinfo.json", MagicRemoteService.Properties.Resources.esappinfo);
			System.IO.File.WriteAllText(@".\TV\MagicRemoteService\resources\es\appstring.json", MagicRemoteService.Properties.Resources.esappstring);
			System.IO.File.WriteAllText(@".\TV\Send\package.json", MagicRemoteService.Properties.Resources.package
				.Replace(@"""name"": ""com.cathwyler.magicremoteservice.send""", @"""name"": ""com.cathwyler.magicremoteservice." + wocdiInput.AppIdShort + @".send""")
			);
			System.IO.File.WriteAllText(@".\TV\Send\send.js", MagicRemoteService.Properties.Resources.send
				.Replace(@"const bOverlay = true", "const bOverlay = " + (bOverlay ? "true" : "false"))
				.Replace(@"const strInputAppId = ""com.webos.app.hdmi""", @"const strInputAppId = ""com.webos.app." + wocdiInput.AppIdShort + @"""")
				.Replace(@"const strAppId = ""com.cathwyler.magicremoteservice""", @"const strAppId = ""com.cathwyler.magicremoteservice." + wocdiInput.AppIdShort + @"""")
			);
			System.IO.File.WriteAllText(@".\TV\Send\services.json", MagicRemoteService.Properties.Resources.services
				.Replace(@"""id"": ""com.cathwyler.magicremoteservice.send""", @"""id"": ""com.cathwyler.magicremoteservice." + wocdiInput.AppIdShort + @".send""")
				.Replace(@"""name"": ""com.cathwyler.magicremoteservice.send""", @"""name"": ""com.cathwyler.magicremoteservice." + wocdiInput.AppIdShort + @".send""")
			);
		}
		private async void PCSave_Click(object sender, System.EventArgs e) {
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
				if(!this.IsDisposed) {
					this.PCDataRefresh();
				}
				this.Enabled = true;
				System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			} else {
				if(!this.IsDisposed) {
					this.PCDataRefresh();
				}
				this.Enabled = true;
			}
		}
		private void Close_Click(object sender, System.EventArgs e) {
			this.Close();
		}
		private async void TVRefresh_Click(object sender, System.EventArgs e) {
			this.Enabled = false;
			string sName = ((MagicRemoteService.WebOSCLIDevice)this.cbbTV.SelectedItem)?.Name;
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
				if(!this.IsDisposed) {
					this.cbbTV.DataSource = tabDevice;
					if(sName != null) {
						this.cbbTV.SelectedValue = sName;
					}
					if(this.cbbTV.SelectedItem == null) {
						this.cbbTV.SelectedIndex = 0;
					}
				}
				this.Enabled = true;
			}
		}
		private async void TVInstall_Click(object sender, System.EventArgs e) {
			if(this.cbbTV.SelectedItem == null) {
				this.ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingTVSelectErrorTitle;
				this.ttFormating.Show("", this.cbbTV);
				this.ttFormating.Show(MagicRemoteService.Properties.Resources.SettingTVSelectErrorMessage, this.cbbTV);
			} else if(this.cbbInput.SelectedItem == null) {
				this.ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingInputSelectErrorTitle;
				this.ttFormating.Show("", this.cbbInput);
				this.ttFormating.Show(MagicRemoteService.Properties.Resources.SettingInputSelectErrorMessage, this.cbbInput);
			} else if(this.cbbDisplay.SelectedItem == null) {
				this.ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingDisplaySelectErrorTitle;
				this.ttFormating.Show("", this.cbbDisplay);
				this.ttFormating.Show(MagicRemoteService.Properties.Resources.SettingDisplaySelectErrorMessage, this.cbbDisplay);
			} else if(this.iabSendIP.Value == null) {
				this.ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingSendIPErrorTitle;
				this.ttFormating.Show("", this.iabSendIP);
				this.ttFormating.Show(MagicRemoteService.Properties.Resources.SettingSendIPErrorMessage, this.iabSendIP);
			} else if(this.iabSubnetMask.Value == null) {
				this.ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingSubnetMaskErrorTitle;
				this.ttFormating.Show("", this.iabSubnetMask);
				this.ttFormating.Show(MagicRemoteService.Properties.Resources.SettingSubnetMaskErrorMessage, this.iabSubnetMask);
			} else if(this.pabPCMac.Value == null) {
				this.ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingPCMacErrorTitle;
				this.ttFormating.Show("", this.pabPCMac);
				this.ttFormating.Show(MagicRemoteService.Properties.Resources.SettingPCMacErrorMessage, this.pabPCMac);
			} else {
				this.Enabled = false;
				MagicRemoteService.WebOSCLIDevice wocdDevice = (MagicRemoteService.WebOSCLIDevice)this.cbbTV.SelectedItem;
				MagicRemoteService.WebOSCLIDeviceInput wocdiInput = (MagicRemoteService.WebOSCLIDeviceInput)this.cbbInput.SelectedItem;
				System.Net.IPAddress ipaSendIP = this.iabSendIP.Value;
				decimal dSendPort = this.nbSendPort.Value;
				System.Net.IPAddress ipMask = this.iabSubnetMask.Value;
				MagicRemoteService.PhysicalAddress macPCMac = this.pabPCMac.Value;
				decimal dLongClick = this.nbLongClick.Value;
				bool bInputDirect = this.cbInputDirect.Checked;
				bool bOverlay = this.cbOverlay.Checked;
				string strError = null;
				string strErrorInfo = null;
				if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
					try {
						System.Version vAssembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
						string strVersion = vAssembly.Major + "." + vAssembly.Minor + "." + vAssembly.Build;
						MagicRemoteService.Setting.AppExtract(strVersion, wocdiInput, ipaSendIP, dSendPort, ipMask, macPCMac, dLongClick, bInputDirect, bOverlay);
						string strTVDir = System.IO.Path.GetFullPath(@".\TV");
						MagicRemoteService.WebOSCLI.Package(strTVDir, MagicRemoteService.Application.CompleteDir(strTVDir) + "MagicRemoteService", MagicRemoteService.Application.CompleteDir(strTVDir) + "Send");
						MagicRemoteService.WebOSCLI.Install(wocdDevice.Name, @".\TV\com.cathwyler.magicremoteservice." + wocdiInput.AppIdShort + "_" + strVersion + "_all.ipk");
						MagicRemoteService.WebOSCLI.Launch(wocdDevice.Name, "com.cathwyler.magicremoteservice." + wocdiInput.AppIdShort);
						if(this.cbOverlay.Checked) {
							MagicRemoteService.WebOSCLI.NovacomRun(wocdDevice.Name, @"luna-send-pub -n 1 'luna://com.webos.service.eim/deleteDevice' '{""appId"":""com.cathwyler.magicremoteservice." + wocdiInput.AppIdShort + @"""}'");
						} else {
							MagicRemoteService.WebOSCLI.NovacomRun(wocdDevice.Name, @"luna-send-pub -n 1 'luna://com.webos.service.eim/addDevice' '{""appId"":""com.cathwyler.magicremoteservice." + wocdiInput.AppIdShort + @""", ""pigImage"": """", ""mvpdIcon"": """", ""type"": ""MVPD_IP"", ""showPopup"": true, ""label"": ""MagicRemoteService"", ""description"": """ + wocdiInput.Name + @"""}'");
						}
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
					if(!this.IsDisposed) {
						this.TVDataSave();
						this.TVDataRefresh();
					}
					this.Enabled = true;
				}
			}
		}
		private async void TV_SelectedIndexChanged(object sender, System.EventArgs e) {
			this.Enabled = false;
			string sId = ((MagicRemoteService.WebOSCLIDeviceInput)this.cbbInput.SelectedItem)?.Id;
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
			} else if(!this.IsDisposed) {
				if(!this.IsDisposed) {
					this.cbbInput.DataSource = tabDeviceInput;
					if(sId != null) {
						this.cbbInput.SelectedValue = sId;
					}
					if(this.cbbInput.SelectedItem == null) {
						this.cbbInput.SelectedIndex = 0;
					}
					this.TVDataRefresh();
				}
				this.Enabled = true;
			}
		}

		private void Setting_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			this.ttFormating.Hide(this);
		}

		private void Setting_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
			if(!e.Cancel && (
				this.dListenPort != this.nbListenPort.Value
				||
				this.bInactivity != this.cbInactivity.Checked
				||
				this.dTimeoutInactivity != this.nbTimeoutInactivity.Value
				||
				this.bStartup != this.cbStartup.Checked
			)) {
				switch(System.Windows.Forms.MessageBox.Show(MagicRemoteService.Properties.Resources.SettingPCSaveMessage, this.Text, System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1)) {
					case System.Windows.Forms.DialogResult.Yes:
						this.PCSave_Click(this, new System.EventArgs());
						break;
					case System.Windows.Forms.DialogResult.No:
						break;
					case System.Windows.Forms.DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
			if(!e.Cancel && (
				this.wocdiInput?.Id != ((MagicRemoteService.WebOSCLIDeviceInput)this.cbbInput.SelectedItem)?.Id
				||
				this.scrDisplay?.Id != ((MagicRemoteService.Screen)this.cbbDisplay.SelectedItem)?.Id
				||
				this.iaSendIP?.ToString() != this.iabSendIP.Value?.ToString()
				||
				this.dSendPort != this.nbSendPort.Value
				||
				this.iaSubnetMask?.ToString() != this.iabSubnetMask.Value?.ToString()
				||
				this.paPCMac?.ToString() != this.pabPCMac.Value?.ToString()
				||
				this.dLongClick != this.nbLongClick.Value
				||
				this.bInputDirect != this.cbInputDirect.Checked
				||
				this.bOverlay != this.cbOverlay.Checked
				||
				this.bExtend != this.cbExtend.Checked
			)) {
				switch(System.Windows.Forms.MessageBox.Show(MagicRemoteService.Properties.Resources.SettingTVSaveMessage, this.Text, System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1)) {
					case System.Windows.Forms.DialogResult.Yes:
						this.TVInstall_Click(this, new System.EventArgs());
						break;
					case System.Windows.Forms.DialogResult.No:
						break;
					case System.Windows.Forms.DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
			if(!e.Cancel && System.Linq.Enumerable.Any<System.Collections.Generic.KeyValuePair<ushort, BindControl>>(this.dBindControl, delegate (System.Collections.Generic.KeyValuePair<ushort, BindControl> kvp) {
				return this.dBind[kvp.Key] != kvp.Value.Value;
			})) {
				switch(System.Windows.Forms.MessageBox.Show(MagicRemoteService.Properties.Resources.SettingRemoteSaveMessage, this.Text, System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1)) {
					case System.Windows.Forms.DialogResult.Yes:
						this.RemoteSave_Click(this, new System.EventArgs());
						break;
					case System.Windows.Forms.DialogResult.No:
						break;
					case System.Windows.Forms.DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
		}

		private async void RemoteSave_Click(object sender, System.EventArgs e) {
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
				if(!this.IsDisposed) {
					this.RemoteDataRefresh();
				}
				this.Enabled = true;
				System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			} else {
				if(!this.IsDisposed) {
					this.RemoteDataRefresh();
				}
				this.Enabled = true;
			}
		}

		private async void TVAdd_Click(object sender, System.EventArgs e) {
			MagicRemoteService.TVAdder taDialog = new MagicRemoteService.TVAdder();
			switch(taDialog.ShowDialog()) {
				case System.Windows.Forms.DialogResult.OK:
					this.Enabled = false;
					string strError = null;
					string strErrorInfo = null;
					if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
						try {
							MagicRemoteService.WebOSCLI.SetupDeviceAdd(taDialog.Device, taDialog.Password);
							if(!taDialog.Advanced) {
								MagicRemoteService.WebOSCLI.NovacomGetKey(taDialog.Device.Name, taDialog.Device.DeviceDetail.Passphrase);
							}
							return true;
						} catch(System.Exception ex) {
							strError = MagicRemoteService.Properties.Resources.SettingTVAddErrorTitle;
							strErrorInfo = ex.Message;
							return false;
						}
					})) {
						if(!this.IsDisposed) {
							this.TVRefresh_Click(this, new System.EventArgs());
						}
						this.Enabled = true;
						System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
					} else {
						if(!this.IsDisposed) {
							this.TVRefresh_Click(this, new System.EventArgs());
						}
						this.Enabled = true;
					}
					break;
				default:
					break;
			}
		}

		private async void TVModify_Click(object sender, System.EventArgs e) {
			if(this.cbbTV.SelectedItem == null) {
				this.ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingTVSelectErrorTitle;
				this.ttFormating.Show("", this.cbbTV);
				this.ttFormating.Show(MagicRemoteService.Properties.Resources.SettingTVSelectErrorMessage, this.cbbTV);
			} else {
				MagicRemoteService.WebOSCLIDevice wocdDevice = (MagicRemoteService.WebOSCLIDevice)this.cbbTV.SelectedItem;
				MagicRemoteService.TVAdder taDialog = new MagicRemoteService.TVAdder(wocdDevice);
				switch(taDialog.ShowDialog()) {
					case System.Windows.Forms.DialogResult.OK:
						this.Enabled = false;
						string strError = null;
						string strErrorInfo = null;
						if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
							try {
								MagicRemoteService.WebOSCLI.SetupDeviceModify(wocdDevice.Name, taDialog.Device, taDialog.Password);
								if(!taDialog.Advanced) {
									MagicRemoteService.WebOSCLI.NovacomGetKey(taDialog.Device.Name, taDialog.Device.DeviceDetail.Passphrase);
								}
								return true;
							} catch(System.Exception ex) {
								strError = MagicRemoteService.Properties.Resources.SettingTVModifyErrorTitle;
								strErrorInfo = ex.Message;
								return false;
							}
						})) {
							if(!this.IsDisposed) {
								this.TVRefresh_Click(this, new System.EventArgs());
							}
							this.Enabled = true;
							System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
						} else {
							if(!this.IsDisposed) {
								this.TVRefresh_Click(this, new System.EventArgs());
							}
							this.Enabled = true;
						}
						break;
					default:
						break;
				}
			}
		}

		private async void TVRemove_Click(object sender, System.EventArgs e) {
			if(this.cbbTV.SelectedItem == null) {
				this.ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.SettingTVSelectErrorTitle;
				this.ttFormating.Show("", this.cbbTV);
				this.ttFormating.Show(MagicRemoteService.Properties.Resources.SettingTVSelectErrorMessage, this.cbbTV);
			} else {
				this.Enabled = false;
				MagicRemoteService.WebOSCLIDevice wocdDevice = (MagicRemoteService.WebOSCLIDevice)this.cbbTV.SelectedItem;
				string strError = null;
				string strErrorInfo = null;
				if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
					try {
						MagicRemoteService.WebOSCLI.SetupDeviceRemove(wocdDevice.Name);
						return true;
					} catch(System.Exception ex) {
						strError = MagicRemoteService.Properties.Resources.SettingTVRemoveErrorTitle;
						strErrorInfo = ex.Message;
						return false;
					}
				})) {
					if(!this.IsDisposed) {
						this.TVRefresh_Click(this, new System.EventArgs());
					}
					this.Enabled = true;
					System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				} else {
					if(!this.IsDisposed) {
						this.TVRefresh_Click(this, new System.EventArgs());
					}
					this.Enabled = true;
				}
			}
		}

		private void Inspect_Click(object sender, System.EventArgs e) {
			async void Inspect() {
				MagicRemoteService.WebOSCLIDevice wocdDevice = (MagicRemoteService.WebOSCLIDevice)this.cbbTV.SelectedItem;
				MagicRemoteService.WebOSCLIDeviceInput wocdiInput = (MagicRemoteService.WebOSCLIDeviceInput)this.cbbInput.SelectedItem;
				string strError = null;
				string strErrorInfo = null;
				if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
					try {
						MagicRemoteService.WebOSCLI.Inspect(wocdDevice.Name, "com.cathwyler.magicremoteservice." + wocdiInput.AppIdShort);
						return true;
					} catch(System.Exception ex) {
						strError = MagicRemoteService.Properties.Resources.SettingTVInstallErrorTitle;
						strErrorInfo = ex.Message;
						return false;
					}
				})) {
					System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
				}
			}
			if(
				this.wocdiInput?.Id != ((MagicRemoteService.WebOSCLIDeviceInput)this.cbbInput.SelectedItem)?.Id
				||
				this.scrDisplay?.Id != ((MagicRemoteService.Screen)this.cbbDisplay.SelectedItem)?.Id
				||
				this.iaSendIP?.ToString() != this.iabSendIP.Value?.ToString()
				||
				this.dSendPort != this.nbSendPort.Value
				||
				this.iaSubnetMask?.ToString() != this.iabSubnetMask.Value?.ToString()
				||
				this.paPCMac?.ToString() != this.pabPCMac.Value?.ToString()
				||
				this.dLongClick != this.nbLongClick.Value
				||
				this.bInputDirect != this.cbInputDirect.Checked
				||
				this.bOverlay != this.cbOverlay.Checked
				||
				this.bExtend != this.cbExtend.Checked
			) {
				switch(System.Windows.Forms.MessageBox.Show(MagicRemoteService.Properties.Resources.SettingTVSaveMessage, this.Text, System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button1)) {
					case System.Windows.Forms.DialogResult.Yes:
						this.TVInstall_Click(this, new System.EventArgs());
						Inspect();
						break;
					case System.Windows.Forms.DialogResult.No:
						Inspect();
						break;
					case System.Windows.Forms.DialogResult.Cancel:
						break;
				}
			} else {
				Inspect();
			}
		}
		private async void TVVersion_Click(object sender, System.EventArgs e) {
			MagicRemoteService.WebOSCLIDevice wocdDevice = (MagicRemoteService.WebOSCLIDevice)this.cbbTV.SelectedItem;
			MagicRemoteService.WebOSCLIDeviceInput wocdiInput = (MagicRemoteService.WebOSCLIDeviceInput)this.cbbInput.SelectedItem;
			string strError = null;
			string strErrorInfo = null;
			if(!await System.Threading.Tasks.Task.Run<bool>(delegate () {
				try {
					System.Windows.Forms.MessageBox.Show(MagicRemoteService.WebOSCLI.DeviceInfo(wocdDevice.Name), this.btnTVVersion.Text, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
					return true;
				} catch(System.Exception ex) {
					strError = MagicRemoteService.Properties.Resources.SettingTVVersionErrorTitle;
					strErrorInfo = ex.Message;
					return false;
				}
			})) {
				System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			}
		}
	}
}
