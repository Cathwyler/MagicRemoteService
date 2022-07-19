
namespace MagicRemoteService {
	public partial class MacAddressBox : System.Windows.Forms.UserControl {
		public MagicRemoteService.PhysicalAddress Value {
			get {
				byte[] tabByte = new byte[6];
				if(
					byte.TryParse(this.dbbByte0.Text, System.Globalization.NumberStyles.HexNumber, null, out tabByte[0])
					&&
					byte.TryParse(this.dbbByte1.Text, System.Globalization.NumberStyles.HexNumber, null, out tabByte[1])
					&&
					byte.TryParse(this.dbbByte2.Text, System.Globalization.NumberStyles.HexNumber, null, out tabByte[2])
					&&
					byte.TryParse(this.dbbByte3.Text, System.Globalization.NumberStyles.HexNumber, null, out tabByte[3])
					&&
					byte.TryParse(this.dbbByte4.Text, System.Globalization.NumberStyles.HexNumber, null, out tabByte[4])
					&&
					byte.TryParse(this.dbbByte5.Text, System.Globalization.NumberStyles.HexNumber, null, out tabByte[5])
				) {
					MagicRemoteService.PhysicalAddress paMac = new MagicRemoteService.PhysicalAddress(tabByte);
					return paMac;
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
					this.dbbByte4.Text = "";
					this.dbbByte5.Text = "";
				} else {
					this.dbbByte0.Text = value.GetAddressBytes()[0].ToString("X2");
					this.dbbByte1.Text = value.GetAddressBytes()[1].ToString("X2");
					this.dbbByte2.Text = value.GetAddressBytes()[2].ToString("X2");
					this.dbbByte3.Text = value.GetAddressBytes()[3].ToString("X2");
					this.dbbByte4.Text = value.GetAddressBytes()[4].ToString("X2");
					this.dbbByte5.Text = value.GetAddressBytes()[5].ToString("X2");
				}
			}
		}
		public MacAddressBox() : base() {
			this.InitializeComponent();
		}
		public void FromString(string sIp) {
			MagicRemoteService.PhysicalAddress paMac;
			if(MagicRemoteService.PhysicalAddress.TryParse(sIp, out paMac)) {
				this.dbbByte0.Text = paMac.GetAddressBytes()[0].ToString("X2");
				this.dbbByte1.Text = paMac.GetAddressBytes()[1].ToString("X2");
				this.dbbByte2.Text = paMac.GetAddressBytes()[2].ToString("X2");
				this.dbbByte3.Text = paMac.GetAddressBytes()[3].ToString("X2");
				this.dbbByte4.Text = paMac.GetAddressBytes()[4].ToString("X2");
				this.dbbByte5.Text = paMac.GetAddressBytes()[5].ToString("X2");
			} else {
				this.dbbByte0.Text = "";
				this.dbbByte1.Text = "";
				this.dbbByte2.Text = "";
				this.dbbByte3.Text = "";
				this.dbbByte4.Text = "";
				this.dbbByte5.Text = "";
			}
		}
		private void dbbByte_EventPaste(object sender, System.EventArgs e) {
			MagicRemoteService.PhysicalAddress paMac;
			byte uc;
			if(byte.TryParse(System.Windows.Forms.Clipboard.GetText(), System.Globalization.NumberStyles.HexNumber, null, out uc)) {
				((MagicRemoteService.DecimalByteBox)sender).Text = uc.ToString("X2");
			} else if(MagicRemoteService.PhysicalAddress.TryParse(System.Windows.Forms.Clipboard.GetText(), out paMac)) {
				this.dbbByte0.Text = paMac.GetAddressBytes()[0].ToString("X2");
				this.dbbByte1.Text = paMac.GetAddressBytes()[1].ToString("X2");
				this.dbbByte2.Text = paMac.GetAddressBytes()[2].ToString("X2");
				this.dbbByte3.Text = paMac.GetAddressBytes()[3].ToString("X2");
				this.dbbByte4.Text = paMac.GetAddressBytes()[4].ToString("X2");
				this.dbbByte5.Text = paMac.GetAddressBytes()[5].ToString("X2");
			} else {
				ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.PhysicalAddressBoxErrorPasteTitle;
				ttFormating.Show("", (System.Windows.Forms.Control)sender);
				ttFormating.Show(MagicRemoteService.Properties.Resources.PhysicalAddressBoxErrorPasteMessage, (System.Windows.Forms.Control)sender);
			}
		}
		private void dbbByte_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			ttFormating.Hide(this);
			MagicRemoteService.DecimalByteBox ddbByte = (MagicRemoteService.DecimalByteBox)sender;
			if(char.IsControl(e.KeyChar)) {
				if(e.KeyChar == (char)System.Windows.Forms.Keys.Back) {
					if(ddbByte.TextLength == 0 && ddbByte.TextLength == ddbByte.SelectionStart) {
						this.SelectNextControl(ddbByte, false, true, false, false);
						System.Windows.Forms.SendKeys.Send(e.KeyChar.ToString());
					}
				}
			} else if(System.Uri.IsHexDigit(e.KeyChar)) {
				e.KeyChar = char.ToUpper(e.KeyChar);
				string strUnselected = ddbByte.Text.Remove(ddbByte.SelectionStart, ddbByte.SelectionLength);
				byte uc;
				if(strUnselected.Length == 2) {
					e.Handled = true;
					if(ddbByte.TextLength == ddbByte.SelectionStart) {
						this.SelectNextControl(ddbByte, true, true, false, false);
						if(!ddbByte.Focused) {
							System.Windows.Forms.SendKeys.Send("^(a)");
						}
					}
				} else if(byte.TryParse(strUnselected.Insert(ddbByte.SelectionStart, e.KeyChar.ToString()), System.Globalization.NumberStyles.HexNumber, null, out uc)) {
					if(strUnselected.Length == 1 && ddbByte.TextLength == ddbByte.SelectionStart) {
						this.SelectNextControl(ddbByte, true, true, false, false);
						if(!ddbByte.Focused) {
							System.Windows.Forms.SendKeys.Send("^(a)");
						}
					}
				} else {
					e.Handled = true;
					ttFormating.ToolTipTitle = MagicRemoteService.Properties.Resources.PhysicalAddressBoxErrorByteEntryTitle;
					ttFormating.Show("", ddbByte);
					ttFormating.Show(string.Format(MagicRemoteService.Properties.Resources.PhysicalAddressBoxErrorByteEntryMessage, ddbByte.Text + e.KeyChar), ddbByte);
				}
			} else if(e.KeyChar == '-' || e.KeyChar == ':') {
				e.Handled = true;
				string strUnselected = ddbByte.Text.Remove(ddbByte.SelectionStart, ddbByte.SelectionLength);
				byte uc;
				if(byte.TryParse(strUnselected, System.Globalization.NumberStyles.HexNumber, null, out uc)) {
					this.SelectNextControl(ddbByte, true, true, false, false);
					if(!ddbByte.Focused) {
						System.Windows.Forms.SendKeys.Send("^(a)");
					}
				}
			} else {
				e.Handled = true;
			}
		}
		private void MacAddressBox_Click(object sender, System.EventArgs e) {
			ttFormating.Hide(this);
		}
	}
}
