
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

	class Application : System.Windows.Forms.ApplicationContext {
		private static readonly MagicRemoteService.Invoker iInvoker = new MagicRemoteService.Invoker();
		private static readonly MagicRemoteService.Watcher wExplorer = new MagicRemoteService.Watcher("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = \"explorer.exe\"");
		private MagicRemoteService.Service mrsService = new MagicRemoteService.Service();
		private System.Windows.Forms.NotifyIcon niIcon;
		private MagicRemoteService.Setting sSetting;
		public Application() {
			MagicRemoteService.Application.wExplorer.EventArrived += this.ExplorerStartEvent;
			Microsoft.Win32.SystemEvents.SessionEnded += this.SessionEndedEvent;
			Microsoft.Win32.SystemEvents.SessionSwitch += this.SessionSwitchEvent;

			this.mrsService.ServiceStart();

			this.Icon(this, new System.EventArgs());
			if((MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService") == null) {
				this.Setting(this, new System.EventArgs());
			}
		}
		public void BeginInvoke(System.Delegate methode) {
			MagicRemoteService.Application.iInvoker.BeginInvoke(methode);
		}
		public void ExplorerStartEvent(object sender, System.Management.EventArrivedEventArgs e) {
			this.niIcon.Dispose();
			this.Icon(this, new System.EventArgs());
		}
		public void SessionEndedEvent(object sender, Microsoft.Win32.SessionEndedEventArgs e) {
			this.Dispose();
		}
		public void SessionSwitchEvent(object sender, Microsoft.Win32.SessionSwitchEventArgs e) {
			System.Windows.Forms.Application.Exit();
		}
		protected override void Dispose(bool disposing) {
			this.mrsService.ServiceStop();
			if(disposing) {
				this.mrsService.Dispose();
				this.niIcon.Dispose();
				if(!this.sSetting.IsDisposed) {
					this.sSetting.Dispose();
				}

				Microsoft.Win32.SystemEvents.SessionSwitch -= this.SessionSwitchEvent;
				Microsoft.Win32.SystemEvents.SessionSwitch -= this.SessionSwitchEvent;
				MagicRemoteService.Application.wExplorer.EventArrived -= this.ExplorerStartEvent;
			}
			base.Dispose(disposing);
		}
		public void Icon(object sender, System.EventArgs e) {
			this.niIcon = new System.Windows.Forms.NotifyIcon();
			this.niIcon.Icon = MagicRemoteService.Properties.Resources.MagicRemoteService;
			this.niIcon.ContextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[] {
				new System.Windows.Forms.MenuItem(MagicRemoteService.Properties.Resources.ApplicationSetting, this.Setting),
				new System.Windows.Forms.MenuItem(MagicRemoteService.Properties.Resources.ApplicationExit, this.Exit)
			});
			this.niIcon.DoubleClick += this.Setting;
			this.niIcon.Visible = true;
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
