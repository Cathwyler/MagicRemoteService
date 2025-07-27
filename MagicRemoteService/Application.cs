
namespace MagicRemoteService {
	public class Invoker : System.Windows.Forms.Control {
		public Invoker() : base() {
			this.CreateControl();
		}
	}
	public class Watcher : System.Management.ManagementEventWatcher {
		public Watcher(string strQuery) : base(strQuery) {
			this.Start();
		}
	}
	public class PowerSettingNotification : System.Windows.Forms.Control {
		private readonly System.IntPtr hNotification;
		public event NotificationArrivedEventHandler NotificationArrived;

		public delegate void NotificationArrivedEventHandler(WinApi.PowerBroadcastSetting pbs);
		public PowerSettingNotification() : base() {
			this.hNotification = WinApi.User32.RegisterPowerSettingNotification(this.Handle, ref WinApi.User32.GUID_MONITOR_POWER_ON, WinApi.User32.DEVICE_NOTIFY_WINDOW_HANDLE);
			if(this.hNotification == System.IntPtr.Zero) {
				throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			}
		}
		protected override void Dispose(bool disposing) {
			WinApi.User32.UnregisterPowerSettingNotification(this.hNotification);
			base.Dispose(disposing);
		}
		protected override void WndProc(ref System.Windows.Forms.Message m) {
			switch(m.Msg) {
				case 0x218:
					switch(m.WParam.ToInt32()) {
						case 0x8013:
							this.NotificationArrived?.Invoke(System.Runtime.InteropServices.Marshal.PtrToStructure<WinApi.PowerBroadcastSetting>(m.LParam));
							break;
					}
					break;
			}
			base.WndProc(ref m);
		}
	}
	internal class Application : System.Windows.Forms.ApplicationContext {
		private static readonly MagicRemoteService.Invoker iInvoker = new MagicRemoteService.Invoker();
		private static readonly MagicRemoteService.Watcher wExplorer = new MagicRemoteService.Watcher("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = \"explorer.exe\"");
		private static readonly MagicRemoteService.PowerSettingNotification psnPowerSettingNotification = new MagicRemoteService.PowerSettingNotification();
		public static event MagicRemoteService.PowerSettingNotification.NotificationArrivedEventHandler PowerSettingNotificationArrived;
		
		private readonly MagicRemoteService.Service mrsService = new MagicRemoteService.Service();
		private readonly System.Windows.Forms.NotifyIcon niIcon;
		private MagicRemoteService.Setting sSetting;
		static Application() {
			MagicRemoteService.Application.psnPowerSettingNotification.NotificationArrived += MagicRemoteService.Application.OnPowerSettingNotification;
		}
		public Application() {
			MagicRemoteService.Application.wExplorer.EventArrived += this.OnExplorerStart;
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

			if((MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey(@"Software\MagicRemoteService") == null) {
				this.Setting(this, System.EventArgs.Empty);
			}
		}

		private void VersionScript() {
			System.Version vCurrent = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			Microsoft.Win32.RegistryKey rkMagicRemoteService = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey(@"Software\MagicRemoteService");
			System.Version vRegistry = new System.Version((string)rkMagicRemoteService.GetValue("Version", "0.0.0.0"));
			if(vRegistry != vCurrent) {
				if(vRegistry < new System.Version("1.2.3.0")) {
					rkMagicRemoteService.DeleteSubKey("KeyBindMouse", false);
					rkMagicRemoteService.DeleteSubKey("KeyBindKeyboard", false);
					rkMagicRemoteService.DeleteSubKey("KeyBindAction", false);
					foreach(string strSubKey in rkMagicRemoteService.GetSubKeyNames()) {
						string strNewSubKey = @"Device\" + strSubKey;
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
				}
				rkMagicRemoteService.SetValue("Version", vCurrent.ToString(), Microsoft.Win32.RegistryValueKind.String);
			}
		}
		public void Invoke(System.Delegate methode) {
			MagicRemoteService.Application.iInvoker.Invoke(methode);
		}
		private void OnExplorerStart(object sender, System.Management.EventArrivedEventArgs e) {
			this.niIcon.Visible = false;
			this.niIcon.Visible = true;
		}
		static private void OnPowerSettingNotification(WinApi.PowerBroadcastSetting pbs) {
			MagicRemoteService.Application.PowerSettingNotificationArrived?.Invoke(pbs);
		}
		protected override void Dispose(bool disposing) {
			if(disposing) {
				this.mrsService.Dispose();
				this.niIcon.Dispose();
				if(this.sSetting != null && !this.sSetting.IsDisposed) {
					this.sSetting.Dispose();
				}
				MagicRemoteService.Application.wExplorer.EventArrived -= this.OnExplorerStart;
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
