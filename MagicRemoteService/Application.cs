
namespace MagicRemoteService {

	class MessageFilter : System.Windows.Forms.IMessageFilter {

		public delegate void WndProc(ref System.Windows.Forms.Message m);
		private WndProc wp;
		public MessageFilter(WndProc wp) {
			this.wp = wp;
		}
		public bool PreFilterMessage(ref System.Windows.Forms.Message m) {
			this.wp(ref m);
			return true;
		}
	}
	class Application : System.Windows.Forms.ApplicationContext {
		private static readonly string strDirectorySeparatorChar = System.IO.Path.DirectorySeparatorChar.ToString();
		private static readonly string strAltDirectorySeparatorChar = System.IO.Path.DirectorySeparatorChar.ToString();
		private MagicRemoteService.Service mrsService = new MagicRemoteService.Service();
		private System.Windows.Forms.NotifyIcon niIcon = new System.Windows.Forms.NotifyIcon();
		private MagicRemoteService.Setting sSetting = null;
		private uint uiTaskbarCreated;
		public Application() {
			Microsoft.Win32.SystemEvents.SessionEnded += this.SessionEndedEvent;
			Microsoft.Win32.SystemEvents.SessionSwitch += this.SessionSwitchEvent;
			this.niIcon.Icon = MagicRemoteService.Properties.Resources.MagicRemoteService;
			this.niIcon.ContextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[] {
				new System.Windows.Forms.MenuItem(MagicRemoteService.Properties.Resources.ApplicationSetting, this.Setting),
				new System.Windows.Forms.MenuItem(MagicRemoteService.Properties.Resources.ApplicationExit, this.Exit)
			});
			this.niIcon.DoubleClick += this.Setting;
			this.niIcon.Visible = true;
			System.Windows.Forms.Application.AddMessageFilter(new MessageFilter(this.WndProc));
			this.uiTaskbarCreated = WinApi.User32.RegisterWindowMessage("TaskbarCreated");

			this.mrsService.ServiceStart();
			Microsoft.Win32.RegistryKey rkMagicRemoteService = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService");
			if(rkMagicRemoteService == null) {
				this.Setting(this, new System.EventArgs());
			}
		}
		void WndProc(ref System.Windows.Forms.Message m) {
			switch(m.Msg) {
				default:
					if(m.Msg == this.uiTaskbarCreated) {
						this.niIcon.Visible = false;
						this.niIcon.Visible = true;
					}
					break;
			}
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
				Microsoft.Win32.SystemEvents.SessionEnded -= this.SessionEndedEvent;
				Microsoft.Win32.SystemEvents.SessionSwitch -= this.SessionSwitchEvent;
			}
			base.Dispose(disposing);
		}
		public void Setting(object sender, System.EventArgs e) {
			if(this.sSetting == null || this.sSetting.IsDisposed) {
				this.sSetting = new MagicRemoteService.Setting(this.mrsService);
				this.sSetting.Show();
			}
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
