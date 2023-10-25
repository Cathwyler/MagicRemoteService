
namespace MagicRemoteService {

	public delegate void WndProcEventHandler(ref System.Windows.Forms.Message m);
	public class Message : System.Windows.Forms.IMessageFilter {
		public static event WndProcEventHandler WndProcEvent;

		public Message() {
			System.Windows.Forms.Application.AddMessageFilter(this);
		}
		public bool PreFilterMessage(ref System.Windows.Forms.Message m) {
			Message.WndProcEvent(ref m);
			return false;
		}
	}
	public class Invoker : System.Windows.Forms.Control {
		public Invoker() {
			this.CreateControl();
		}
	}

	class Application : System.Windows.Forms.ApplicationContext {

		private static readonly uint uiTaskbarCreated = WinApi.User32.RegisterWindowMessage("TaskbarCreated");
		private static readonly MagicRemoteService.Message mMessage = new MagicRemoteService.Message();
		private static readonly MagicRemoteService.Invoker cInvoker = new MagicRemoteService.Invoker();

		private MagicRemoteService.Service mrsService = new MagicRemoteService.Service();
		private System.Windows.Forms.NotifyIcon niIcon = null;
		private MagicRemoteService.Setting sSetting = null;
		public Application() {
			Microsoft.Win32.SystemEvents.SessionEnded += this.SessionEndedEvent;
			Microsoft.Win32.SystemEvents.SessionSwitch += this.SessionSwitchEvent;
			MagicRemoteService.Message.WndProcEvent += this.WndProc;

			this.mrsService.ServiceStart();

			this.Icon(this, new System.EventArgs());
			if((MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService") == null) {
				this.Setting(this, new System.EventArgs());
			}
		}
		public void BeginInvoke(System.Delegate methode) {
			MagicRemoteService.Application.cInvoker.BeginInvoke(methode);
		}

		void WndProc(ref System.Windows.Forms.Message m) {
			System.Console.WriteLine(m.ToString());
			switch(m.Msg) {
				case 0x0001:
					
					break;
				default:
					if(m.Msg == MagicRemoteService.Application.uiTaskbarCreated) {
						this.niIcon.Dispose();
						this.Icon(this, new System.EventArgs());
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
				if(this.sSetting == null || this.sSetting.IsDisposed) {
					this.sSetting.Dispose();
				}
				Microsoft.Win32.SystemEvents.SessionEnded -= this.SessionEndedEvent;
				Microsoft.Win32.SystemEvents.SessionSwitch -= this.SessionSwitchEvent;
				MagicRemoteService.Message.WndProcEvent -= this.WndProc;
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
