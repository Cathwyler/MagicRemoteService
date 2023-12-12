
namespace MagicRemoteService {
	public partial class DecimalByteBox : System.Windows.Forms.TextBox {
		public DecimalByteBox() : base() {
		}

		public event System.EventHandler ehEventPaste;

		private const int WM_PASTE = 0x0302;

		protected override void WndProc(ref System.Windows.Forms.Message m) {
			switch(m.Msg) {
				case WM_PASTE:
					ehEventPaste.Invoke(this, new System.EventArgs());
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}
	}
}
