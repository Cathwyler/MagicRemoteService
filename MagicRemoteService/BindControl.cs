
namespace MagicRemoteService {
	public partial class BindControl : System.Windows.Forms.UserControl {
		private MagicRemoteService.Bind bBind = null;
		public MagicRemoteService.Bind Value {
			get {
				return this.bBind;
			}
			set {
				this.bBind = value;
				if(value == null) {
					this.btnBind.Text = "";
				} else {
					this.btnBind.Text = value.ToString();
				}
			}
		}
		public BindControl() {
			this.InitializeComponent();
		}

		private void Bind_Click(object sender, System.EventArgs e) {
			System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
			MagicRemoteService.BindCreator bcDialog = new MagicRemoteService.BindCreator(this.Value);
			switch(bcDialog.ShowDialog()) {
				case System.Windows.Forms.DialogResult.OK:
					this.Value = bcDialog.Bind;
					break;
				default:
					break;
			}
		}
	}
}

