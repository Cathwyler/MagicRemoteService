using System.ComponentModel;
using System.Configuration.Install;

namespace MagicRemoteService {
	[RunInstaller(true)]
	public partial class ProjectInstaller : System.Configuration.Install.Installer {
		public ProjectInstaller() {
			this.InitializeComponent();
		}

		private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e) {

		}
	}
}
