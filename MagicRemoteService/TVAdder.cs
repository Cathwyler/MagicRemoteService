
namespace MagicRemoteService {
	public partial class TVAdder : System.Windows.Forms.Form {
		private readonly MagicRemoteService.WebOSCLIDevice wocdDevice;
		private string strPassword;
		private bool bAdvanced;
		public MagicRemoteService.WebOSCLIDevice Device {
			get {
				return this.wocdDevice;
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
		public TVAdder(MagicRemoteService.WebOSCLIDevice wocdDevice = null) {
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.InitializeComponent();
			this.wocdDevice = new MagicRemoteService.WebOSCLIDevice() {
				Name = "",
				DeviceInfo = new WebOSCLIDeviceInfo() {
					IP = null,
					Port = 9922,
					User = "prisoner"
				},
				DeviceDetail = new WebOSCLIDeviceDetail() {
					Description = "",
					PrivateKey = "",
					Passphrase = ""
				}
			};
			if(wocdDevice == null) {
				this.cbAdvanced.Checked = false;
				this.tbName.Text = this.wocdDevice.Name;
				this.tbDescription.Text = this.wocdDevice.DeviceDetail.Description;
				this.iabIP.Value = this.wocdDevice.DeviceInfo.IP;
				this.nbSendPort.Value = this.wocdDevice.DeviceInfo.Port;
				this.tbUser.Text = this.wocdDevice.DeviceInfo.User;
				this.tbPrivateKey.Text = this.wocdDevice.DeviceDetail.PrivateKey;
				this.tbPassphrase.Text = this.wocdDevice.DeviceDetail.Passphrase;
			} else {
				this.cbAdvanced.Checked = true;
				this.tbName.Text = wocdDevice.Name;
				this.tbDescription.Text = wocdDevice.DeviceDetail.Description;
				this.iabIP.Value = wocdDevice.DeviceInfo.IP;
				this.nbSendPort.Value = wocdDevice.DeviceInfo.Port;
				this.tbUser.Text = wocdDevice.DeviceInfo.User;
				this.tbPrivateKey.Text = wocdDevice.DeviceDetail.PrivateKey;
				this.tbPassphrase.Text = wocdDevice.DeviceDetail.Passphrase;
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
			this.wocdDevice.Name = this.tbName.Text;
		}

		private void Description_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this.wocdDevice.DeviceDetail.Description = this.tbDescription.Text;
			}
		}

		private void IP_ValueChanged(object sender, System.EventArgs e) {
			this.wocdDevice.DeviceInfo.IP = this.iabIP.Value;
		}

		private void SendPort_ValueChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this.wocdDevice.DeviceInfo.Port = (ushort)this.nbSendPort.Value;
			}
		}

		private void User_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this.wocdDevice.DeviceInfo.User = this.tbUser.Text;
			}
		}

		private void Password_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this.strPassword = this.tbPassword.Text;
			}
		}

		private void Passphrase_TextChanged(object sender, System.EventArgs e) {
			this.wocdDevice.DeviceDetail.Passphrase = this.tbPassphrase.Text;
		}

		private void PrivateKey_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this.wocdDevice.DeviceDetail.PrivateKey = this.tbPrivateKey.Text;
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
				this.wocdDevice.DeviceDetail.Description = "";
				this.wocdDevice.DeviceInfo.Port = 0;
				this.wocdDevice.DeviceInfo.User = "";
				this.wocdDevice.DeviceDetail.PrivateKey = "";
			} else {
				this.wocdDevice.DeviceDetail.Description = this.tbDescription.Text;
				this.wocdDevice.DeviceInfo.Port = (ushort)this.nbSendPort.Value;
				this.wocdDevice.DeviceInfo.User = this.tbUser.Text;
				this.wocdDevice.DeviceDetail.PrivateKey = this.tbPrivateKey.Text;
			}
		}
	}
}
