
namespace MagicRemoteService
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur de composants

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.spiServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.siServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// spiServiceProcessInstaller
			// 
			this.spiServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.spiServiceProcessInstaller.Password = null;
			this.spiServiceProcessInstaller.Username = null;
			// 
			// siServiceInstaller
			// 
			this.siServiceInstaller.Description = "Service providing computer remote control using a LG WebOS TV";
			this.siServiceInstaller.DisplayName = "Magic remote service";
			this.siServiceInstaller.ServiceName = "MagicRemoteService";
			this.siServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.spiServiceProcessInstaller,
            this.siServiceInstaller});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller spiServiceProcessInstaller;
		private System.ServiceProcess.ServiceInstaller siServiceInstaller;
	}
}