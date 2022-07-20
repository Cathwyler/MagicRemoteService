
namespace MagicRemoteService {
	class Application : System.Windows.Forms.ApplicationContext {
		private static readonly string strDirectorySeparatorChar = System.IO.Path.DirectorySeparatorChar.ToString();
		private static readonly string strAltDirectorySeparatorChar = System.IO.Path.DirectorySeparatorChar.ToString();
		private MagicRemoteService.Service mrsService = new MagicRemoteService.Service();
		private System.Windows.Forms.NotifyIcon niIcon = new System.Windows.Forms.NotifyIcon();
		private MagicRemoteService.Setting sSetting = null;
		public Application() {
			this.mrsService.ServiceStart();

			this.niIcon.Icon = MagicRemoteService.Properties.Resources.MagicRemoteService;
			this.niIcon.ContextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[] {
				new System.Windows.Forms.MenuItem(MagicRemoteService.Properties.Resources.ApplicationSetting, this.Setting),
				new System.Windows.Forms.MenuItem(MagicRemoteService.Properties.Resources.ApplicationExit, this.Exit)
			});
			this.niIcon.DoubleClick += this.Setting;
			this.niIcon.Visible = true;


			Microsoft.Win32.RegistryKey rkMagicRemoteService = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\MagicRemoteService");
			if(rkMagicRemoteService == null) {
				this.Setting(this, new System.EventArgs());
			}
		}
		protected override void Dispose(bool disposing) {
			if(disposing) {
				this.mrsService.ServiceStop();
				this.mrsService.Dispose();
				this.niIcon.Dispose();
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
