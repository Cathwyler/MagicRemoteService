
namespace MagicRemoteService {
	public partial class BindControl : System.Windows.Forms.UserControl {
		private MagicRemoteService.Bind b = null;
		public MagicRemoteService.Bind Value {
			get {
				return this.b;
			}
			set {
				this.b = value;
				if(value == null) {
					this.btnBind.Text = "";
				} else {
					this.btnBind.Text = value.ToString();
				}
			}
		}
		public BindControl() {
			InitializeComponent();
		}

		private void btnBind_Click(object sender, System.EventArgs e) {

			System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
			MagicRemoteService.BindCreator win = new MagicRemoteService.BindCreator(this.Value);
			switch(win.ShowDialog()) {
				case System.Windows.Forms.DialogResult.OK:
					this.Value = win.bBind;
					break;
				default:
					break;
			}
		}
	}
}

