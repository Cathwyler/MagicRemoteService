
namespace MagicRemoteService {
	public partial class TVAdder : System.Windows.Forms.Form {
		private readonly MagicRemoteService.WebOSCLIDevice wcdDevice;
		private string strPassword;
		private bool bAdvanced;
		public MagicRemoteService.WebOSCLIDevice Device {
			get {
				return this.wcdDevice;
			}
		}
		public string Password {
			get {
				return this.strPassword;
			}
		}
		public bool Advanced {
			get {
				return this.bAdvanced;
			}
		}
		public TVAdder(MagicRemoteService.WebOSCLIDevice wcdDevice = null) {
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.InitializeComponent();
			this.wcdDevice = new MagicRemoteService.WebOSCLIDevice() {
				strName = "",
				wcdiDeviceInfo = new WebOSCLIDeviceInfo() {
					iaIP = null,
					usPort = 9922,
					strUser = "prisoner"
				},
				wcddDeviceDetail = new WebOSCLIDeviceDetail() {
					strDescription = "",
					strPrivateKey = "",
					strPassphrase = ""
				}
			};
			if(wcdDevice == null) {
				this.cbAdvanced.Checked = false;
				this.tbName.Text = this.wcdDevice.strName;
				this.tbDescription.Text = this.wcdDevice.wcddDeviceDetail.strDescription;
				this.ipadrboxIP.Value = this.wcdDevice.wcdiDeviceInfo.iaIP;
				this.numboxSendPort.Value = this.wcdDevice.wcdiDeviceInfo.usPort;
				this.tbUser.Text = this.wcdDevice.wcdiDeviceInfo.strUser;
				this.tbPrivateKey.Text = this.wcdDevice.wcddDeviceDetail.strPrivateKey;
				this.tbPassphrase.Text = this.wcdDevice.wcddDeviceDetail.strPassphrase;
			} else {
				this.cbAdvanced.Checked = true;
				this.tbName.Text = wcdDevice.strName;
				this.tbDescription.Text = wcdDevice.wcddDeviceDetail.strDescription;
				this.ipadrboxIP.Value = wcdDevice.wcdiDeviceInfo.iaIP;
				this.numboxSendPort.Value = wcdDevice.wcdiDeviceInfo.usPort;
				this.tbUser.Text = wcdDevice.wcdiDeviceInfo.strUser;
				this.tbPrivateKey.Text = wcdDevice.wcddDeviceDetail.strPrivateKey;
				this.tbPassphrase.Text = wcdDevice.wcddDeviceDetail.strPassphrase;
			}
			this.tbPassword.Text = "";
		}
		private void Confirm_Click(object sender, System.EventArgs e) {
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}
		private void Cancel_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void Name_TextChanged(object sender, System.EventArgs e) {
			this.wcdDevice.strName = this.tbName.Text;
		}

		private void Description_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this.wcdDevice.wcddDeviceDetail.strDescription = this.tbDescription.Text;
			}
		}

		private void IP_ValueChanged(object sender, System.EventArgs e) {
			this.wcdDevice.wcdiDeviceInfo.iaIP = this.ipadrboxIP.Value;
		}

		private void SendPort_ValueChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this.wcdDevice.wcdiDeviceInfo.usPort = (ushort)this.numboxSendPort.Value;
			}
		}

		private void User_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this.wcdDevice.wcdiDeviceInfo.strUser = this.tbUser.Text;
			}
		}

		private void Password_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this.strPassword = this.tbPassword.Text;
			}
		}

		private void Passphrase_TextChanged(object sender, System.EventArgs e) {
			this.wcdDevice.wcddDeviceDetail.strPassphrase = this.tbPassphrase.Text;
		}

		private void PrivateKey_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this.wcdDevice.wcddDeviceDetail.strPrivateKey = this.tbPrivateKey.Text;
			}
		}

		private void Advanced_CheckedChanged(object sender, System.EventArgs e) {
			this.bAdvanced = this.cbAdvanced.Checked;
			this.pnlDescription.Visible = this.cbAdvanced.Checked;
			this.pnlPort.Visible = this.cbAdvanced.Checked;
			this.pnlUser.Visible = this.cbAdvanced.Checked;
			this.pnlPassword.Visible = this.cbAdvanced.Checked;
			this.pnlPrivateKey.Visible = this.cbAdvanced.Checked;
			this.labKeyServer.Visible = !this.cbAdvanced.Checked;
			if(!this.cbAdvanced.Checked) {
				this.wcdDevice.wcddDeviceDetail.strDescription = "";
				this.wcdDevice.wcdiDeviceInfo.usPort = 0;
				this.wcdDevice.wcdiDeviceInfo.strUser = "";
				this.wcdDevice.wcddDeviceDetail.strPrivateKey = "";
			} else {
				this.wcdDevice.wcddDeviceDetail.strDescription = this.tbDescription.Text;
				this.wcdDevice.wcdiDeviceInfo.usPort = (ushort)this.numboxSendPort.Value;
				this.wcdDevice.wcdiDeviceInfo.strUser = this.tbUser.Text;
				this.wcdDevice.wcddDeviceDetail.strPrivateKey = this.tbPrivateKey.Text;
			}
		}
	}
}
