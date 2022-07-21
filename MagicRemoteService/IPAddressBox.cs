
namespace MagicRemoteService {
	public partial class IPAddressBox : System.Windows.Forms.UserControl {
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
				if(value == null) {
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
			if(System.Net.IPAddress.TryParse(sIp, out ipaIp)) {
				this.dbbByte0.Text = ipaIp.GetAddressBytes()[0].ToString();
				this.dbbByte1.Text = ipaIp.GetAddressBytes()[1].ToString();
				this.dbbByte2.Text = ipaIp.GetAddressBytes()[2].ToString();
				this.dbbByte3.Text = ipaIp.GetAddressBytes()[3].ToString();
			} else {
				this.dbbByte0.Text = "";
				this.dbbByte1.Text = "";
				this.dbbByte2.Text = "";
				this.dbbByte3.Text = "";
			}
		}
		private void dbbByte_EventPaste(object sender, System.EventArgs e) {
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
				ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.IPAddressBoxErrorPasteTitle;
				ttFormating.Show("", (System.Windows.Forms.Control)sender);
				ttFormating.Show(MagicRemoteService.Properties.Resources.IPAddressBoxErrorPasteMessage, (System.Windows.Forms.Control)sender);
			}
		}
		private void dbbByte_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			ttFormating.Hide(this);
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
					ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.IPAddressBoxErrorByteEntryTitle;
					ttFormating.Show("", ddbByte);
					ttFormating.Show(string.Format(MagicRemoteService.Properties.Resources.IPAddressBoxErrorByteEntryMessage, ddbByte.Text + e.KeyChar), ddbByte);
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
			ttFormating.Hide(this);
		}
	}
}
