namespace MagicRemoteService {

	static public class Program {
		static private System.Windows.Forms.Control cInvoker;
		static private MagicRemoteService.Application aApplication;
		static private System.Threading.Mutex mProgram = new System.Threading.Mutex(true, "{7CC08060-4D87-482B-9C44-FCCCBC572798}");
		static private System.Threading.EventWaitHandle ewhEventShow = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset, "Global\\{74D2F5A8-46FF-4AA3-B666-67F11B0DB793}");
		static private System.Threading.AutoResetEvent areEndentStop = new System.Threading.AutoResetEvent(false);
		[System.STAThread]
		static void Main() {
			if(MagicRemoteService.Program.mProgram.WaitOne(System.TimeSpan.Zero, true)) {
				System.Windows.Forms.Application.EnableVisualStyles();
				System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
				MagicRemoteService.Program.aApplication = new MagicRemoteService.Application();

				MagicRemoteService.Program.cInvoker = new System.Windows.Forms.Control();
				MagicRemoteService.Program.cInvoker.CreateControl();
				System.Threading.Thread tEvent = new System.Threading.Thread(delegate() {
					System.Threading.WaitHandle[] tabEvent = new System.Threading.WaitHandle[] { MagicRemoteService.Program.areEndentStop, MagicRemoteService.Program.ewhEventShow };
					bool bAlive = true;
					do {
						switch(System.Threading.WaitHandle.WaitAny(tabEvent)) {
							case 0:
								bAlive = false;
								break;
							case 1:
								MagicRemoteService.Program.cInvoker.BeginInvoke((System.Action)delegate() {
									MagicRemoteService.Program.aApplication.Setting(MagicRemoteService.Program.ewhEventShow, new System.EventArgs());
								});
								break;
						}
					} while(bAlive);
				});
				tEvent.Start();

				System.Windows.Forms.Application.Run(MagicRemoteService.Program.aApplication);
				MagicRemoteService.Program.mProgram.ReleaseMutex();

				MagicRemoteService.Program.areEndentStop.Set();
				tEvent.Join();
				MagicRemoteService.Program.cInvoker.Dispose();

			} else {
				MagicRemoteService.Program.ewhEventShow.Set();
			}
		}
	}
}
