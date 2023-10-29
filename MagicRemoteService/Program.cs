namespace MagicRemoteService {
	static public class Program {

		public static readonly System.Security.AccessControl.MutexSecurity msAll;
		public static readonly System.Security.AccessControl.EventWaitHandleSecurity ewhsAll;
		public static readonly bool bElevated;

		static Program() {
			System.Security.AccessControl.MutexAccessRule mar = new System.Security.AccessControl.MutexAccessRule(new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null), System.Security.AccessControl.MutexRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
			MagicRemoteService.Program.msAll = new System.Security.AccessControl.MutexSecurity();
			MagicRemoteService.Program.msAll.AddAccessRule(mar);
			System.Security.AccessControl.EventWaitHandleAccessRule ewhar = new System.Security.AccessControl.EventWaitHandleAccessRule(new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null), System.Security.AccessControl.EventWaitHandleRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
			MagicRemoteService.Program.ewhsAll = new System.Security.AccessControl.EventWaitHandleSecurity();
			MagicRemoteService.Program.ewhsAll.AddAccessRule(ewhar);
			MagicRemoteService.Program.bElevated = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
		}
		[System.STAThread]
		static void Main() {
			if(!System.Environment.UserInteractive) {
				System.Threading.Mutex mServer = new System.Threading.Mutex(false, "Global\\{40169E2A-C106-44BC-84FF-18AD489DB914}", out _, Program.msAll);
				if(mServer.WaitOne(System.TimeSpan.Zero)) {
					System.ServiceProcess.ServiceBase[] sb;
					sb = new System.ServiceProcess.ServiceBase[]
					{
						new MagicRemoteService.Service()
					};
					System.ServiceProcess.ServiceBase.Run(sb);
					mServer.ReleaseMutex();
				}
				mServer.Close();
				mServer.Dispose();
			} else {
				System.Threading.Mutex mClient = new System.Threading.Mutex(false, "Global\\{7CC08060-4D87-482B-9C44-FCCCBC572798}", out _, Program.msAll);
				System.Threading.EventWaitHandle ewhShow = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset, "Global\\{74D2F5A8-46FF-4AA3-B666-67F11B0DB793}", out _, Program.ewhsAll);
				if(mClient.WaitOne(System.TimeSpan.Zero)) {
					System.Windows.Forms.Application.EnableVisualStyles();
					System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
					MagicRemoteService.Application aApplication = new MagicRemoteService.Application();
					System.Threading.ManualResetEvent mreStop = new System.Threading.ManualResetEvent(false);
					System.Threading.Thread tEvent = new System.Threading.Thread(delegate () {
						System.Threading.WaitHandle[] tabEvent = new System.Threading.WaitHandle[] { mreStop, ewhShow };
						do {
							switch(System.Threading.WaitHandle.WaitAny(tabEvent)) {
								case 0:
									break;
								case 1:
									aApplication.Invoke((System.Action)delegate() {
										aApplication.Setting(null, System.EventArgs.Empty);
									});
									break;
							}
						} while(!mreStop.WaitOne(System.TimeSpan.Zero));
					});
					tEvent.Start();
					System.Windows.Forms.Application.Run(aApplication);
					mClient.ReleaseMutex();
					mreStop.Set();
					tEvent.Join();

					mreStop.Close();
					mreStop.Dispose();
				} else {
					ewhShow.Set();
				}
				ewhShow.Close();
				ewhShow.Dispose();
				mClient.Close();
				mClient.Dispose();
			}
		}
	}
}
