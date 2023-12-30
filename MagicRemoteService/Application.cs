
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
