
namespace MagicRemoteService {
	public class Invoker : System.Windows.Forms.Control {
		public Invoker() {
			this.CreateControl();
		}
	}
	public class Watcher : System.Management.ManagementEventWatcher {
		public Watcher(string strQuery) : base(strQuery) {
			this.Start();
		}
	}

	internal class Application : System.Windows.Forms.ApplicationContext {
		private static readonly MagicRemoteService.Invoker iInvoker = new MagicRemoteService.Invoker();
		private static readonly MagicRemoteService.Watcher wExplorer = new MagicRemoteService.Watcher("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = \"explorer.exe\"");
		private MagicRemoteService.Service mrsService = new MagicRemoteService.Service();
		private System.Windows.Forms.NotifyIcon niIcon;
		private MagicRemoteService.Setting sSetting;
		public Application() {
			MagicRemoteService.Application.wExplorer.EventArrived += this.ExplorerStartEvent;

			this.VersionScript();

			this.mrsService.ServiceStart();

			this.niIcon = new System.Windows.Forms.NotifyIcon {
				Icon = MagicRemoteService.Properties.Resources.MagicRemoteService,
				ContextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[] {
					new System.Windows.Forms.MenuItem(MagicRemoteService.Properties.Resources.ApplicationSetting, this.Setting),
					new System.Windows.Forms.MenuItem(MagicRemoteService.Properties.Resources.ApplicationExit, this.Exit)
				})
			};
			this.niIcon.DoubleClick += this.Setting;
			this.niIcon.Visible = true;

			if((MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService") == null) {
				this.Setting(this, System.EventArgs.Empty);
			}
		}
		private void VersionScript() {
			System.Version vCurrent = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			Microsoft.Win32.RegistryKey rkMagicRemoteService = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey("Software\\MagicRemoteService");
			System.Version vRegistry = new System.Version((string)rkMagicRemoteService.GetValue("Version", "0.0.0.0"));
			if(vRegistry != vCurrent) {
				if(vRegistry < new System.Version("1.2.2.10")) {
					rkMagicRemoteService.DeleteSubKey("KeyBindMouse", false);
					rkMagicRemoteService.DeleteSubKey("KeyBindKeyboard", false);
					rkMagicRemoteService.DeleteSubKey("KeyBindAction", false);
				}
				if(vRegistry < new System.Version("1.2.3.0")) {
					foreach(string strSubKey in rkMagicRemoteService.GetSubKeyNames()) {
						string strNewSubKey;
						switch(strSubKey) {
							case "KeyBindMouse":
								strNewSubKey = "Remote\\Mouse";
								break;
							case "KeyBindKeyboard":
								strNewSubKey = "Remote\\Keyboard";
								break;
							case "KeyBindAction":
								strNewSubKey = "Remote\\Action";
								break;
							default:
								strNewSubKey = "Device\\" + strSubKey;
								break;
						}
						Microsoft.Win32.RegistryKey rkMagicRemoteServiceSubKey = rkMagicRemoteService.OpenSubKey(strSubKey);
						Microsoft.Win32.RegistryKey rkMagicRemoteServiceNewSubKey = rkMagicRemoteService.CreateSubKey(strNewSubKey);

						foreach(string strValue in rkMagicRemoteServiceSubKey.GetValueNames()) {
							object oValue = rkMagicRemoteServiceSubKey.GetValue(strValue);
							rkMagicRemoteServiceNewSubKey.SetValue(strValue, oValue, rkMagicRemoteServiceSubKey.GetValueKind(strValue));
						}
						rkMagicRemoteServiceSubKey.Close();
						rkMagicRemoteService.DeleteSubKey(strSubKey);
					}
					Microsoft.Win32.RegistryKey rkMagicRemoteServiceDeviceList = rkMagicRemoteService.OpenSubKey("Device", true);
					if(rkMagicRemoteServiceDeviceList != null) {
						foreach(string sDevice in rkMagicRemoteServiceDeviceList.GetSubKeyNames()) {
							Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = rkMagicRemoteServiceDeviceList.OpenSubKey(sDevice, true);
							if(rkMagicRemoteServiceDevice != null) {
								rkMagicRemoteServiceDevice.SetValue("InputId", rkMagicRemoteServiceDevice.GetValue("Input"), rkMagicRemoteServiceDevice.GetValueKind("Input"));
								rkMagicRemoteServiceDevice.DeleteValue("Input");
								rkMagicRemoteServiceDevice.SetValue("LongClick", rkMagicRemoteServiceDevice.GetValue("TimeoutRightClick"), rkMagicRemoteServiceDevice.GetValueKind("TimeoutRightClick"));
								rkMagicRemoteServiceDevice.DeleteValue("TimeoutRightClick");
								rkMagicRemoteServiceDevice.DeleteValue("TimeoutScreensaver", false);
							}
						}
					}
					Microsoft.Win32.RegistryKey rkMagicRemoteServiceMouse = rkMagicRemoteService.OpenSubKey("Remote\\Mouse", true);
					if(rkMagicRemoteServiceMouse != null) {
						foreach(string sKey in rkMagicRemoteServiceMouse.GetValueNames()) {
							Microsoft.Win32.RegistryKey rkMagicRemoteServiceMouseKey = rkMagicRemoteServiceMouse.CreateSubKey(sKey);
							rkMagicRemoteServiceMouseKey.SetValue("1", System.BitConverter.GetBytes((ushort)(int)rkMagicRemoteServiceMouse.GetValue(sKey)), Microsoft.Win32.RegistryValueKind.Binary);
							rkMagicRemoteServiceMouse.DeleteValue(sKey);
						}
					}
					Microsoft.Win32.RegistryKey rkMagicRemoteServiceKeyboard = rkMagicRemoteService.OpenSubKey("Remote\\Keyboard", true);
					if(rkMagicRemoteServiceKeyboard != null) {
						foreach(string sKey in rkMagicRemoteServiceKeyboard.GetValueNames()) {
							Microsoft.Win32.RegistryKey rkMagicRemoteServiceKeyboardKey = rkMagicRemoteServiceKeyboard.CreateSubKey(sKey);
							rkMagicRemoteServiceKeyboardKey.SetValue("1", rkMagicRemoteServiceKeyboard.GetValue(sKey), Microsoft.Win32.RegistryValueKind.Binary);
							rkMagicRemoteServiceKeyboard.DeleteValue(sKey);
						}
					}
					Microsoft.Win32.RegistryKey rkMagicRemoteServiceAction = rkMagicRemoteService.OpenSubKey("Remote\\Action", true);
					if(rkMagicRemoteServiceAction != null) {
						foreach(string sKey in rkMagicRemoteServiceAction.GetValueNames()) {
							Microsoft.Win32.RegistryKey rkMagicRemoteServiceActionKey = rkMagicRemoteServiceAction.CreateSubKey(sKey);
							rkMagicRemoteServiceActionKey.SetValue("1", new byte[] { (byte)(int)rkMagicRemoteServiceAction.GetValue(sKey) }, Microsoft.Win32.RegistryValueKind.Binary);
							rkMagicRemoteServiceAction.DeleteValue(sKey);
						}
					}
				}
				rkMagicRemoteService.SetValue("Version", vCurrent.ToString(), Microsoft.Win32.RegistryValueKind.String);
			}
		}
		public void Invoke(System.Delegate methode) {
			MagicRemoteService.Application.iInvoker.Invoke(methode);
		}
		public void ExplorerStartEvent(object sender, System.Management.EventArrivedEventArgs e) {
			this.niIcon.Visible = false;
			this.niIcon.Visible = true;
		}
		protected override void Dispose(bool disposing) {
			if(disposing) {
				this.mrsService.Dispose();
				this.niIcon.Dispose();
				if(this.sSetting != null && !this.sSetting.IsDisposed) {
					this.sSetting.Dispose();
				}
				MagicRemoteService.Application.wExplorer.EventArrived -= this.ExplorerStartEvent;
			}
			base.Dispose(disposing);
		}
		public void Setting(object sender, System.EventArgs e) {
			if(this.sSetting == null || this.sSetting.IsDisposed) {
				this.sSetting = new MagicRemoteService.Setting(this.mrsService);
			}
			this.sSetting.Show();
			this.sSetting.Activate();
			this.sSetting.WindowState = System.Windows.Forms.FormWindowState.Normal;
		}
		public void Exit(object sender, System.EventArgs e) {
			this.mrsService.ServiceStop();
			System.Windows.Forms.Application.Exit();
		}
		public static string CompleteDir(string strPath) {
			if(!strPath.Contains(System.IO.Path.AltDirectorySeparatorChar.ToString())) {
				return strPath.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) ? strPath : (strPath + System.IO.Path.DirectorySeparatorChar.ToString());
			} else if(!strPath.Contains(System.IO.Path.DirectorySeparatorChar.ToString())) {
				return strPath.EndsWith(System.IO.Path.AltDirectorySeparatorChar.ToString()) ? strPath : (strPath + System.IO.Path.AltDirectorySeparatorChar.ToString());
			} else {
				throw new System.Exception();
			}
		}
	}
}
