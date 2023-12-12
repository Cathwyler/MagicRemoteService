
namespace MagicRemoteService {
	[System.ComponentModel.RunInstaller(true)]
	public partial class ProjectInstaller : System.Configuration.Install.Installer {
		public ProjectInstaller() {
			this.InitializeComponent();
		}
		public override void Install(System.Collections.IDictionary stateSaver) {
			this.siServiceInstaller.StartType = this.Context.Parameters.ContainsKey("enable") ? System.ServiceProcess.ServiceStartMode.Automatic : System.ServiceProcess.ServiceStartMode.Manual;
			base.Install(stateSaver);
		}
	}
}
