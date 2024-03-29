﻿
namespace MagicRemoteService {
	public partial class IPAddressBox : System.Windows.Forms.UserControl {
		public event System.EventHandler ehValueChanged;
		public System.Net.IPAddress Value {
			get {
				byte[] tabByte = new byte[4];
				if(
					byte.TryParse(this.dbbByte0.Text, System.Globalization.NumberStyles.Integer, null, out tabByte[0])
					&&
					byte.TryParse(this.dbbByte1.Text, System.Globalization.NumberStyles.Integer, null, out tabByte[1])
					&&
					byte.TryParse(this.dbbByte2.Text, System.Globalization.NumberStyles.Integer, null, out tabByte[2])
					&&
					byte.TryParse(this.dbbByte3.Text, System.Globalization.NumberStyles.Integer, null, out tabByte[3])
				) {
					System.Net.IPAddress ipaIp = new System.Net.IPAddress(tabByte);
					return ipaIp;
				} else {
					return null;
				}
			}
			set {
				if(value == null || !(value.GetAddressBytes().Length == 4)) {
					this.dbbByte0.Text = "";
					this.dbbByte1.Text = "";
					this.dbbByte2.Text = "";
					this.dbbByte3.Text = "";
				} else {
					this.dbbByte0.Text = value.GetAddressBytes()[0].ToString();
					this.dbbByte1.Text = value.GetAddressBytes()[1].ToString();
					this.dbbByte2.Text = value.GetAddressBytes()[2].ToString();
					this.dbbByte3.Text = value.GetAddressBytes()[3].ToString();
				}
			}
		}
		public IPAddressBox() : base() {
			this.InitializeComponent();
		}
		public void FromString(string sIp) {
			System.Net.IPAddress ipaIp;
			if(!System.Net.IPAddress.TryParse(sIp, out ipaIp) || !(ipaIp.GetAddressBytes().Length == 4)) {
				this.dbbByte0.Text = "";
				this.dbbByte1.Text = "";
				this.dbbByte2.Text = "";
				this.dbbByte3.Text = "";
			} else {
				this.dbbByte0.Text = ipaIp.GetAddressBytes()[0].ToString();
				this.dbbByte1.Text = ipaIp.GetAddressBytes()[1].ToString();
				this.dbbByte2.Text = ipaIp.GetAddressBytes()[2].ToString();
				this.dbbByte3.Text = ipaIp.GetAddressBytes()[3].ToString();
			}
		}
		private void Byte_EventPaste(object sender, System.EventArgs e) {
			System.Net.IPAddress ipaIp;
			byte uc;
			if(byte.TryParse(System.Windows.Forms.Clipboard.GetText(), System.Globalization.NumberStyles.Integer, null, out uc)) {
				((MagicRemoteService.DecimalByteBox)sender).Text = uc.ToString();
			} else if(System.Net.IPAddress.TryParse(System.Windows.Forms.Clipboard.GetText(), out ipaIp)) {
				this.dbbByte0.Text = ipaIp.GetAddressBytes()[0].ToString();
				this.dbbByte1.Text = ipaIp.GetAddressBytes()[1].ToString();
				this.dbbByte2.Text = ipaIp.GetAddressBytes()[2].ToString();
				this.dbbByte3.Text = ipaIp.GetAddressBytes()[3].ToString();
			} else {
				this.ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.IPAddressBoxErrorPasteTitle;
				this.ttFormating.Show("", (System.Windows.Forms.Control)sender);
				this.ttFormating.Show(MagicRemoteService.Properties.Resources.IPAddressBoxErrorPasteMessage, (System.Windows.Forms.Control)sender);
			}
		}
		private void Byte_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			this.ttFormating.Hide(this);
			MagicRemoteService.DecimalByteBox ddbByte = (MagicRemoteService.DecimalByteBox)sender;
			if(char.IsControl(e.KeyChar)) {
				if(e.KeyChar == (char)System.Windows.Forms.Keys.Back) {
					if(ddbByte.TextLength == 0 && ddbByte.TextLength == ddbByte.SelectionStart) {
						if(this.SelectNextControl(ddbByte, false, true, false, false)) {
							MagicRemoteService.DecimalByteBox ddbNextByte = (MagicRemoteService.DecimalByteBox)this.ActiveControl;
							if(ddbNextByte.TextLength > 0) {
								ddbNextByte.Text = ddbNextByte.Text.Substring(0, ddbNextByte.TextLength - 1);
								ddbNextByte.SelectionStart = ddbNextByte.TextLength;
								ddbNextByte.SelectionLength = 0;
							}
						}
					}
				}
			} else if(char.IsDigit(e.KeyChar)) {
				string strUnselected = ddbByte.Text.Remove(ddbByte.SelectionStart, ddbByte.SelectionLength);
				byte uc;
				if(strUnselected.Length == 3) {
					e.Handled = true;
					if(ddbByte.TextLength == ddbByte.SelectionStart) {
						if(this.SelectNextControl(ddbByte, true, true, false, false)) {
							MagicRemoteService.DecimalByteBox ddbNextByte = (MagicRemoteService.DecimalByteBox)this.ActiveControl;
							ddbNextByte.SelectionStart = 0;
							ddbNextByte.SelectionLength = ddbByte.TextLength;
						}
					}
				} else if(byte.TryParse(strUnselected.Insert(ddbByte.SelectionStart, e.KeyChar.ToString()), System.Globalization.NumberStyles.Integer, null, out uc)) {
					if(strUnselected.Length == 2 && ddbByte.TextLength == ddbByte.SelectionStart) {
						if(this.SelectNextControl(ddbByte, true, true, false, false)) {
							MagicRemoteService.DecimalByteBox ddbNextByte = (MagicRemoteService.DecimalByteBox)this.ActiveControl;
							ddbNextByte.SelectionStart = 0;
							ddbNextByte.SelectionLength = ddbByte.TextLength;
						}
					}
				} else {
					e.Handled = true;
					this.ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.IPAddressBoxErrorByteEntryTitle;
					this.ttFormating.Show("", ddbByte);
					this.ttFormating.Show(string.Format(MagicRemoteService.Properties.Resources.IPAddressBoxErrorByteEntryMessage, ddbByte.Text + e.KeyChar), ddbByte);
				}
			} else if(e.KeyChar == '.') {
				e.Handled = true;
				string strUnselected = ddbByte.Text.Remove(ddbByte.SelectionStart, ddbByte.SelectionLength);
				byte uc;
				if(byte.TryParse(strUnselected, System.Globalization.NumberStyles.Integer, null, out uc)) {
					if(this.SelectNextControl(ddbByte, true, true, false, false)) {
						MagicRemoteService.DecimalByteBox ddbNextByte = (MagicRemoteService.DecimalByteBox)this.ActiveControl;
						ddbNextByte.SelectionStart = 0;
						ddbNextByte.SelectionLength = ddbByte.TextLength;
					}
				}
			} else {
				e.Handled = true;
			}
		}
		private void IPAddressBox_Click(object sender, System.EventArgs e) {
			this.ttFormating.Hide(this);
		}

		private void Byte0_TextChanged(object sender, System.EventArgs e) {
			if(this.ehValueChanged != null) {
				this.ehValueChanged(this, e);
			}
		}

		private void Byte1_TextChanged(object sender, System.EventArgs e) {
			if(this.ehValueChanged != null) {
				this.ehValueChanged(this, e);
			}
		}

		private void Byte2_TextChanged(object sender, System.EventArgs e) {
			if(this.ehValueChanged != null) {
				this.ehValueChanged(this, e);
			}
		}

		private void Byte3_TextChanged(object sender, System.EventArgs e) {
			if(this.ehValueChanged != null) {
				this.ehValueChanged(this, e);
			}
		}
	}
}
