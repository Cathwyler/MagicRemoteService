
namespace MagicRemoteService {
	public partial class Setting : System.Windows.Forms.Form {
		private static readonly System.Net.IPAddress ipaSendIPDefaut;
		private static readonly System.Net.IPAddress ipaMaskDefaut;
		private static readonly MagicRemoteService.PhysicalAddress paMacDefaut;
		static Setting() {
			System.Net.NetworkInformation.NetworkInterface niDefaut = System.Array.Find<System.Net.NetworkInformation.NetworkInterface>(System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces(), delegate (System.Net.NetworkInformation.NetworkInterface ni) {
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

		private decimal dListenPort;
		private bool bInactivity;
		private decimal dTimeoutInactivity;
		private bool bStartup;

		private MagicRemoteService.WebOSCLIDeviceInput wcdiInput;
		private System.Net.IPAddress iaSendIP;
		private decimal dSendPort;
		private System.Net.IPAddress iaSubnetMask;
		private MagicRemoteService.PhysicalAddress paPCMac;
		private decimal	dTimeoutRightClick;
		private decimal dTimeoutScreensaver;
		private bool bExtend;

		public Setting(MagicRemoteService.Service mrs) {
			this.mrsService = mrs;
			this.InitializeComponent();
			this.PCDataRefresh();
			this.TVDataRefresh();
			this.btnTVRefresh_Click(this, new System.EventArgs());
		}
		private new bool Enabled {
			set {
				this.UseWaitCursor = !value;
				this.grpboxPC.Enabled = value;
				this.grpboxTV.Enabled = value;
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
				this.ipadrboxSendIP.Enabled = false;
				this.numboxSendPort.Enabled = false;
				this.ipadrboxSubnetMask.Enabled = false;
				this.phyadrboxPCMac.Enabled = false;
				this.numboxTimeoutRightClick.Enabled = false;
				this.numboxTimeoutScreensaver.Enabled = false;
				this.chkboxExtend.Enabled = false;
				this.btnTVInstall.Enabled = false;
				this.cmbboxInput.SelectedItem = null;
				this.ipadrboxSendIP.Value = MagicRemoteService.Setting.ipaSendIPDefaut;
				this.numboxSendPort.Value = this.numboxListenPort.Value;
				this.ipadrboxSubnetMask.Value = MagicRemoteService.Setting.ipaMaskDefaut;
				this.phyadrboxPCMac.Value = MagicRemoteService.Setting.paMacDefaut;
				this.numboxTimeoutRightClick.Value = 1500;
				this.numboxTimeoutScreensaver.Value = 120000;
				this.chkboxExtend.Checked = false;
			} else {
				this.cmbboxInput.Enabled = true;
				this.ipadrboxSendIP.Enabled = true;
				this.numboxSendPort.Enabled = true;
				this.ipadrboxSubnetMask.Enabled = true;
				this.phyadrboxPCMac.Enabled = true;
				this.numboxTimeoutRightClick.Enabled = true;
				this.numboxTimeoutScreensaver.Enabled = true;
				this.btnTVInstall.Enabled = true;
				this.chkboxExtend.Enabled = true;
				Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService\\" + ((MagicRemoteService.WebOSCLIDevice)this.cmbboxTV.SelectedItem).Name);
				if(rkMagicRemoteServiceDevice == null) {
					this.cmbboxInput.SelectedIndex = 0;
					this.ipadrboxSendIP.Value = MagicRemoteService.Setting.ipaSendIPDefaut;
					this.numboxSendPort.Value = this.numboxListenPort.Value;
					this.ipadrboxSubnetMask.Value = MagicRemoteService.Setting.ipaMaskDefaut;
					this.phyadrboxPCMac.Value = MagicRemoteService.Setting.paMacDefaut;
					this.numboxTimeoutRightClick.Value = 1500;
					this.numboxTimeoutScreensaver.Value = 120000;
					this.chkboxExtend.Checked = false;
				} else {
					string sInputId = (string)rkMagicRemoteServiceDevice.GetValue("Input");
					if(sInputId == null) {
						this.cmbboxInput.SelectedIndex = 0;
					} else {
						this.cmbboxInput.SelectedValue = sInputId;
					}
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
					this.numboxTimeoutScreensaver.Value = (int)rkMagicRemoteServiceDevice.GetValue("TimeoutScreensaver", 120000);
					this.chkboxExtend.Checked = (int)rkMagicRemoteServiceDevice.GetValue("Extend", 0) != 0;
				}
			}
			this.wcdiInput = (MagicRemoteService.WebOSCLIDeviceInput)this.cmbboxInput.SelectedItem;
			this.iaSendIP = this.ipadrboxSendIP.Value;
			this.dSendPort = this.numboxSendPort.Value;
			this.iaSubnetMask = this.ipadrboxSubnetMask.Value;
			this.paPCMac = this.phyadrboxPCMac.Value;
			this.dTimeoutRightClick = this.numboxTimeoutRightClick.Value;
			this.dTimeoutScreensaver = this.numboxTimeoutScreensaver.Value;
			this.bExtend = this.chkboxExtend.Checked;
		}
		public void TVDataSave() {
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey("Software\\MagicRemoteService\\" + ((MagicRemoteService.WebOSCLIDevice)this.cmbboxTV.SelectedItem).Name);
			rkMagicRemoteServiceDevice.SetValue("Input", ((MagicRemoteService.WebOSCLIDeviceInput)this.cmbboxInput.SelectedItem).Id, Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("SendIp", this.ipadrboxSendIP.Value.ToString(), Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("SendPort", this.numboxSendPort.Value, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteServiceDevice.SetValue("Mask", this.ipadrboxSubnetMask.Value.ToString(), Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("PCMac", this.phyadrboxPCMac.Value.ToString(), Microsoft.Win32.RegistryValueKind.String);
			rkMagicRemoteServiceDevice.SetValue("TimeoutRightClick", this.numboxTimeoutRightClick.Value, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteServiceDevice.SetValue("TimeoutScreensaver", this.numboxTimeoutScreensaver.Value, Microsoft.Win32.RegistryValueKind.DWord);
			rkMagicRemoteServiceDevice.SetValue("Extend", this.chkboxExtend.Checked, Microsoft.Win32.RegistryValueKind.DWord);
		}
		public static void AppExtract(
			string strVersion,
			MagicRemoteService.WebOSCLIDeviceInput wcdiInput,
			System.Net.IPAddress ipaSendIP,
			decimal dSendPort,
			System.Net.IPAddress ipaMask,
			MagicRemoteService.PhysicalAddress paPCMac,
			decimal dTimeoutRightClick,
			decimal dTimeoutScreensaver
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
				.Replace("const uiScreensaver = 120000;", "const uiScreensaver = " + dTimeoutScreensaver.ToString() + ";")
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
		private void btnPCSave_Click(object sender, System.EventArgs e) {
			this.Enabled = false;
			if(this.mrsService.State == MagicRemoteService.ServiceCurrentState.SERVICE_RUNNING) {
				this.mrsService.ServiceStop();
			}
			this.PCDataSave();
			this.PCDataRefresh();
			this.mrsService.ServiceStart();
			this.Enabled = true;
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
				this.Enabled = true;
				System.Windows.Forms.MessageBox.Show(strErrorInfo, strError, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
				decimal dTimeoutScreensaver = this.numboxTimeoutScreensaver.Value;
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
						MagicRemoteService.Setting.AppExtract(strVersion, wcdiInput, ipaSendIP, dSendPort, ipMask, macPCMac, dTimeoutRightClick, dTimeoutScreensaver);
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
				this.dTimeoutScreensaver != this.numboxTimeoutScreensaver.Value
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
		}
	}
}
