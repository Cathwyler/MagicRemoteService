
namespace MagicRemoteService {
	public partial class TV : System.Windows.Forms.Form {
		private MagicRemoteService.WebOSCLIDevice _wcdTV;
		private string _strPassword;
		private bool _bAdvanced;
		public MagicRemoteService.WebOSCLIDevice wcdTV {
			get { return this._wcdTV; }
		}
		public string strPassword {
			get { return _strPassword; }
		}
		public bool bAdvanced {
			get { return _bAdvanced; }
		}
		public TV(MagicRemoteService.WebOSCLIDevice wcdTV = null) {
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			InitializeComponent();
			this._wcdTV = new MagicRemoteService.WebOSCLIDevice() {
				DeviceInfo = new WebOSCLIDeviceInfo(),
				DeviceDetail = new WebOSCLIDeviceDetail()
			};
			if(wcdTV == null) {
				this.cbAdvanced.Checked = false;
				this.tbName.Text = "";
				this.tbDescription.Text = "";
				this.ipadrboxIP.Value = null;
				this.numboxSendPort.Value = 9922;
				this.tbUser.Text = "prisoner";
				this.tbPrivateKey.Text = "";
				this.tbPassphrase.Text = "";
			} else {
				this.cbAdvanced.Checked = true;
				this.tbName.Text = wcdTV.Name;
				this.tbDescription.Text = wcdTV.DeviceDetail.Description;
				this.ipadrboxIP.Value = wcdTV.DeviceInfo.IP;
				this.numboxSendPort.Value = wcdTV.DeviceInfo.Port;
				this.tbUser.Text = wcdTV.DeviceInfo.User;
				this.tbPrivateKey.Text = wcdTV.DeviceDetail.PrivateKey;
				this.tbPassphrase.Text = wcdTV.DeviceDetail.Passphrase;
			}
			this.tbPassword.Text = "";
		}
		private void btnConfirm_Click(object sender, System.EventArgs e) {
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}
		private void btnCancel_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void tbName_TextChanged(object sender, System.EventArgs e) {
			this._wcdTV.Name = this.tbName.Text;
		}

		private void tbDescription_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this._wcdTV.DeviceDetail.Description = this.tbDescription.Text;
			}
		}

		private void ipadrboxIP_ValueChanged(object sender, System.EventArgs e) {
			this._wcdTV.DeviceInfo.IP = this.ipadrboxIP.Value;
		}

		private void numboxSendPort_ValueChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this._wcdTV.DeviceInfo.Port = (short)this.numboxSendPort.Value;
			}
		}

		private void tbUser_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this._wcdTV.DeviceInfo.User = this.tbUser.Text;
			}
		}

		private void tbPassword_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this._strPassword = this.tbPassword.Text;
			}
		}

		private void tbPassphrase_TextChanged(object sender, System.EventArgs e) {
			this._wcdTV.DeviceDetail.Passphrase = this.tbPassphrase.Text;
		}

		private void tbPrivateKey_TextChanged(object sender, System.EventArgs e) {
			if(this.cbAdvanced.Checked) {
				this._wcdTV.DeviceDetail.PrivateKey = this.tbPrivateKey.Text;
			}
		}

		private void cbAdvanced_CheckedChanged(object sender, System.EventArgs e) {
			this._bAdvanced = this.cbAdvanced.Checked;
			this.pnlDescription.Visible = this.cbAdvanced.Checked;
			this.pnlPort.Visible = this.cbAdvanced.Checked;
			this.pnlUser.Visible = this.cbAdvanced.Checked;
			this.pnlPassword.Visible = this.cbAdvanced.Checked;
			this.pnlPrivateKey.Visible = this.cbAdvanced.Checked;
			this.labKeyServer.Visible = !this.cbAdvanced.Checked;
			if(!this.cbAdvanced.Checked) {
				this._wcdTV.DeviceDetail.Description = "";
				this._wcdTV.DeviceInfo.Port = 0;
				this._wcdTV.DeviceInfo.User = "";
				this._wcdTV.DeviceDetail.PrivateKey = "";
			} else {
				this._wcdTV.DeviceDetail.Description = this.tbDescription.Text;
				this._wcdTV.DeviceInfo.Port = (short)this.numboxSendPort.Value;
				this._wcdTV.DeviceInfo.User = this.tbUser.Text;
				this._wcdTV.DeviceDetail.PrivateKey = this.tbPrivateKey.Text;
			}
		}
	}
}
