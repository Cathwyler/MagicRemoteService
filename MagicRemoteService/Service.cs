
namespace MagicRemoteService {
	public enum ServiceType {
		Server,
		Client,
		Both
	}
	public enum WebSocketOpCode : byte {
		Continuation = 0x0,
		Text = 0x1,
		Binary = 0x2,
		ConnectionClose = 0x8,
		Ping = 0x9,
		Pong = 0xA
	}
	public enum MessageType : byte {
		Position = 0x00,
		Wheel = 0x01,
		Visible = 0x02,
		Key = 0x03,
		Unicode= 0x04,
		Shutdown = 0x05
	}
	public partial class Service : System.ServiceProcess.ServiceBase {
		private volatile int iPort;
		private volatile bool bInactivity;
		private volatile int iTimeoutInactivity;
		private volatile System.Collections.Generic.Dictionary<ushort, Bind> dKeyBind = new System.Collections.Generic.Dictionary<ushort, Bind>() {
			{ 0x0001, null },
			{ 0x0002, null },
			{ 0x0008, null },
			{ 0x000D, null },
			{ 0x0025, null },
			{ 0x0026, null },
			{ 0x0027, null },
			{ 0x0028, null },
			{ 0x0193, null },
			{ 0x0194, null },
			{ 0x0195, null },
			{ 0x0196, null },
			{ 0x01CD, null },
			{ 0x019F, null },
			{ 0x0013, null },
			{ 0x01A1, null },
			{ 0x019C, null },
			{ 0x019D, null }
		};

		private System.Threading.Thread thrServeur;
		private System.Timers.Timer[] tabExtend;
		private WinApi.ServiceCurrentState scsState;
		private ServiceType stType;
		private static readonly uint SessionId = WinApi.Kernel32.WTSGetActiveConsoleSessionId();

		private static System.Threading.ManualResetEvent mreStop = new System.Threading.ManualResetEvent(true);
		private static System.Threading.Semaphore mServer;
		private static System.Threading.Semaphore mClient;
		private static System.Threading.EventWaitHandle ewhServiceStarted;
		private static System.Threading.EventWaitHandle ewhServiceStoped;
		private static System.Threading.EventWaitHandle ewhClientStarted;
		private static System.Threading.EventWaitHandle ewhClientStoped;
		private static System.Threading.EventWaitHandle ewhClientOnStart;
		private static System.Threading.EventWaitHandle ewhClientOnStop;

		public WinApi.ServiceCurrentState State {
			get {
				return this.scsState;
			}
		}
		public ServiceType Type {
			get {
				return this.stType;
			}
		}
		private static readonly byte[] tabClose = { (0b1000 << 4) | (0x8 << 0), 0x00 };
		private static readonly byte[] tabPing = { (0b1000 << 4) | (0x9 << 0), 0x00 };
		private static readonly byte[] tabPingInactivity = { (0b1000 << 4) | (0x9 << 0), 0x01, 0x01 };

		public Service() {
			this.InitializeComponent();
			if(!System.Diagnostics.EventLog.SourceExists(this.ServiceName)) {
				System.Diagnostics.EventLog.CreateEventSource(this.ServiceName, "Application");
			}
			this.elEventLog.Source = this.ServiceName;
			this.elEventLog.Log = "Application";
		}
		public void ServiceStart() {
			Service.ewhServiceStarted = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "Global\\{FFB31601-E362-48A5-B9A2-5DF29A3B06C1}", out _, Program.ewhsAll);
			Service.ewhServiceStoped = new System.Threading.EventWaitHandle(true, System.Threading.EventResetMode.ManualReset, "Global\\{A99BF327-CEF0-4B97-979B-A7BC9FC007C0}", out _, Program.ewhsAll);
			Service.ewhClientStarted = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "Global\\{9878BC83-46A0-412B-86B6-10F1C43FC0D9}", out _, Program.ewhsAll);
			Service.ewhClientStoped = new System.Threading.EventWaitHandle(true, System.Threading.EventResetMode.ManualReset, "Global\\{D68680EE-C0DC-4E48-8BED-4142DDD32B51}", out _, Program.ewhsAll);
			Service.ewhClientOnStart = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset, "Global\\{DE5E5FB8-970A-4305-869E-5AD3BC1CD7B7}", out _, Program.ewhsAll);
			Service.ewhClientOnStop = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset, "Global\\{2D55D3E0-9951-49D5-A578-41D27CE72EE5}", out _, Program.ewhsAll);

			if(!System.Environment.UserInteractive) {
				Service.mServer = new System.Threading.Semaphore(1, 1, "Global\\{4D2D4202-C4FC-41B5-BE9D-547D98A8140C}", out _, MagicRemoteService.Program.ssAll);
				if(!Service.mServer.WaitOne(System.TimeSpan.Zero, true)) {
					throw new System.Exception("Server already running");
				}
				this.stType = ServiceType.Server;
			} else {
				Service.mClient = new System.Threading.Semaphore(1, 1, "Global\\{C4CCA362-510C-4E66-A316-29F9ADE7F664}", out _, MagicRemoteService.Program.ssAll);
				if(!Service.mClient.WaitOne(System.TimeSpan.Zero, true)) {
					throw new System.Exception("Client already running");
				}
				if(!(System.Array.IndexOf<string>(System.Environment.GetCommandLineArgs(), "-c") < 0) && System.Windows.Forms.Application.OpenForms.Count == 0) {
					this.stType = ServiceType.Client;
				} else  if(!Service.ewhServiceStoped.WaitOne(System.TimeSpan.Zero, true)) {
					this.stType = ServiceType.Client;
				} else {
					this.stType = ServiceType.Both;
				}
			}

			WinApi.ServiceStatus ssServiceStatus = new WinApi.ServiceStatus();
			switch(this.stType) {
				case ServiceType.Server:
					this.Log("Service server start");
					break;
				case ServiceType.Both:
					this.Log("Service both start");
					break;
				case ServiceType.Client:
					this.Log("Service client start");
					break;
			}
			this.scsState = WinApi.ServiceCurrentState.SERVICE_START_PENDING;
			switch(this.stType) {
				case ServiceType.Server:
					ssServiceStatus.dwCurrentState = WinApi.ServiceCurrentState.SERVICE_START_PENDING;
					ssServiceStatus.dwWaitHint = 100000;
					bool truc = WinApi.Advapi32.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);
					break;
				case ServiceType.Both:
				case ServiceType.Client:
					break;
			}
			Microsoft.Win32.RegistryKey rkMagicRemoteService = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService");
			if(rkMagicRemoteService == null) {
				this.iPort = 41230;
				this.bInactivity = true;
				this.iTimeoutInactivity = 7200000;
			} else {
				this.iPort = (int)rkMagicRemoteService.GetValue("Port", 41230);
				this.bInactivity = (int)rkMagicRemoteService.GetValue("Inactivity", 1) != 0;
				this.iTimeoutInactivity = (int)rkMagicRemoteService.GetValue("TimeoutInactivity", 7200000);
			}
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceKeyBindMouse = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService\\KeyBindMouse");
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceKeyBindKeyboard = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService\\KeyBindKeyboard");
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceKeyBindAction = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService\\KeyBindAction");
			if(rkMagicRemoteServiceKeyBindMouse == null && rkMagicRemoteServiceKeyBindKeyboard == null && rkMagicRemoteServiceKeyBindAction == null) {
				this.dKeyBind[0x0001] = new BindMouse(BindMouseValue.Left);       //Click -> Left click
				this.dKeyBind[0x0002] = new BindMouse(BindMouseValue.Right);      //Long click -> Right click
				this.dKeyBind[0x0008] = new BindKeyboard(0x000E);           //BACKSPACE -> Keyboard Delete
				this.dKeyBind[0x000D] = new BindKeyboard(0x001C);           //OK -> Keyboard Return Enter
				this.dKeyBind[0x0025] = new BindKeyboard(0xE04B);           //Left -> Keyboard LeftArrow
				this.dKeyBind[0x0026] = new BindKeyboard(0xE048);           //Up -> Keyboard UpArrow
				this.dKeyBind[0x0027] = new BindKeyboard(0xE04D);           //Right -> Keyboard RightArrow
				this.dKeyBind[0x0028] = new BindKeyboard(0xE050);           //Down -> Keyboard DownArrow
				this.dKeyBind[0x0193] = new BindAction(BindActionValue.Shutdown); //Red -> Show shutdown
				this.dKeyBind[0x0194] = new BindKeyboard(0xE05B);           //Green -> Keyboard Left GUI
				this.dKeyBind[0x0195] = new BindMouse(BindMouseValue.Right);      //Yellow -> Right click
				this.dKeyBind[0x0196] = new BindAction(BindActionValue.Keyboard); //Blue -> Show keyboard
				this.dKeyBind[0x01CD] = new BindKeyboard(0x0001);           //Back -> Keyboard Escape
				this.dKeyBind[0x019F] = null;								//Play -> Play/Pause
				this.dKeyBind[0x0013] = null;								//Pause -> Play/Pause
				this.dKeyBind[0x01A1] = null;								//Fast-forward -> Scan Next Track
				this.dKeyBind[0x019C] = null;								//Rewind -> Scan Previous Track
				this.dKeyBind[0x019D] = null;								//Stop -> Stop
			} else {
				this.dKeyBind = new System.Collections.Generic.Dictionary<ushort, Bind>();
				foreach(string sKey in rkMagicRemoteServiceKeyBindMouse.GetValueNames()) {
					this.dKeyBind[System.UInt16.Parse(sKey.Substring(2), System.Globalization.NumberStyles.HexNumber)] = new BindMouse((BindMouseValue)(int)rkMagicRemoteServiceKeyBindMouse.GetValue(sKey, 0x0000));
				}
				foreach(string sKey in rkMagicRemoteServiceKeyBindKeyboard.GetValueNames()) {
					this.dKeyBind[System.UInt16.Parse(sKey.Substring(2), System.Globalization.NumberStyles.HexNumber)] = new BindKeyboard((ushort)(int)rkMagicRemoteServiceKeyBindKeyboard.GetValue(sKey, 0x0000));
				}
				foreach(string sKey in rkMagicRemoteServiceKeyBindAction.GetValueNames()) {
					this.dKeyBind[System.UInt16.Parse(sKey.Substring(2), System.Globalization.NumberStyles.HexNumber)] = new BindAction((BindActionValue)(int)rkMagicRemoteServiceKeyBindAction.GetValue(sKey, 0x0000));
				}
			}
			switch(this.stType) {
				case ServiceType.Server:
				case ServiceType.Both:
					if(rkMagicRemoteService != null) {
						this.tabExtend = System.Array.ConvertAll(System.Array.FindAll(rkMagicRemoteService.GetSubKeyNames(), delegate (string str) {
							Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService\\" + str);
							return (int)rkMagicRemoteServiceDevice.GetValue("Extend", 0) != 0;
						}), new System.Converter<string, System.Timers.Timer>(delegate (string str) {

							Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).CreateSubKey("Software\\MagicRemoteService\\" + str);

							System.Timers.Timer tExtend = new System.Timers.Timer();
							tExtend.AutoReset = false;
							tExtend.Interval = System.Math.Max(100, (new System.DateTime((long)rkMagicRemoteServiceDevice.GetValue("LastExtend", System.DateTime.Now.AddDays(-1).Ticks)).AddHours(-4).Date.AddDays(1).AddHours(4) - System.DateTime.Now).TotalMilliseconds);
							tExtend.Elapsed += async delegate (System.Object oSource, System.Timers.ElapsedEventArgs eElapsed) {
								if(await System.Threading.Tasks.Task.Run<bool>(delegate () {
									try {
										WebOSCLI.Extend(str);
										return true;
									} catch(System.Exception) {
										return false;
									}
								})) {
									rkMagicRemoteServiceDevice.SetValue("LastExtend", System.DateTime.Now.Ticks, Microsoft.Win32.RegistryValueKind.QWord);
								}
								tExtend.Interval = System.Math.Max(60000, (new System.DateTime((long)rkMagicRemoteServiceDevice.GetValue("LastExtend", System.DateTime.Now.AddDays(-1).Ticks)).AddHours(-4).Date.AddDays(1).AddHours(4) - System.DateTime.Now).TotalMilliseconds);
								tExtend.Start();
							};
							tExtend.Start();
							return tExtend;
						}));
					} else {
						this.tabExtend = new System.Timers.Timer[] { };
					}
					break;
				case ServiceType.Client:
					break;
			}
			switch(this.stType) {
				case ServiceType.Server:
					Service.ewhServiceStarted.Set();
					Service.ewhServiceStoped.Reset();
					break;
				case ServiceType.Both:
				case ServiceType.Client:
					Service.ewhClientStarted.Set();
					Service.ewhClientStoped.Reset();
					Service.ewhClientOnStart.Set();
					Service.ewhClientOnStop.Reset();
					break;
			}
			Service.mreStop.Reset();
			this.thrServeur = new System.Threading.Thread(delegate () {
				this.ThreadServeur();
			});
			this.thrServeur.Start();
			switch(this.stType) {
				case ServiceType.Server:
					this.Log("Service server started");
					break;
				case ServiceType.Both:
					this.Log("Service both started");
					break;
				case ServiceType.Client:
					this.Log("Service client started");
					break;
			}
			this.scsState = WinApi.ServiceCurrentState.SERVICE_RUNNING;
			switch(this.stType) {
				case ServiceType.Server:
					ssServiceStatus.dwCurrentState = WinApi.ServiceCurrentState.SERVICE_RUNNING;
					WinApi.Advapi32.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);
					break;
				case ServiceType.Both:
				case ServiceType.Client:
					break;
			}
		}
		public void ServiceStop() {

			WinApi.ServiceStatus ssServiceStatus = new WinApi.ServiceStatus();
			switch(this.stType) {
				case ServiceType.Server:
					this.Log("Service server stop");
					break;
				case ServiceType.Both:
					this.Log("Service both stop");
					break;
				case ServiceType.Client:
					this.Log("Service client stop");
					break;
			}
			this.scsState = WinApi.ServiceCurrentState.SERVICE_STOP_PENDING;
			switch(this.stType) {
				case ServiceType.Server:
					ssServiceStatus.dwCurrentState = WinApi.ServiceCurrentState.SERVICE_STOP_PENDING;
					ssServiceStatus.dwWaitHint = 100000;
					WinApi.Advapi32.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);
					break;
				case ServiceType.Both:
				case ServiceType.Client:
					break;
			}
			switch(this.stType) {
				case ServiceType.Server:
				case ServiceType.Both:
					foreach(System.Timers.Timer tExtend in this.tabExtend) {
						tExtend.Stop();
						tExtend.Dispose();
					}
					this.tabExtend = null;
					break;
				case ServiceType.Client:
					break;
			}
			switch(this.stType) {
				case ServiceType.Server:
					Service.ewhServiceStarted.Reset();
					Service.ewhServiceStoped.Set();
					break;
				case ServiceType.Both:
				case ServiceType.Client:
					Service.ewhClientStarted.Reset();
					Service.ewhClientStoped.Set();
					Service.ewhClientOnStart.Reset();
					Service.ewhClientOnStop.Set();
					break;
			}
			Service.mreStop.Set();
			this.thrServeur.Join();
			this.thrServeur = null;
			switch(this.stType) {
				case ServiceType.Server:
					this.Log("Service server stoped");
					break;
				case ServiceType.Both:
					this.Log("Service both stoped");
					break;
				case ServiceType.Client:
					this.Log("Service client stoped");
					break;
			}
			this.scsState = WinApi.ServiceCurrentState.SERVICE_STOPPED;
			switch(this.stType) {
				case ServiceType.Server:
					ssServiceStatus.dwCurrentState = WinApi.ServiceCurrentState.SERVICE_STOPPED;
					WinApi.Advapi32.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);
					break;
				case ServiceType.Both:
				case ServiceType.Client:
					break;
			}
			switch(this.stType) {
				case ServiceType.Server:
					Service.mServer.Release();
					Service.mServer.Close();
					Service.mServer.Dispose();
					break;
				case ServiceType.Both:
				case ServiceType.Client:
					Service.mClient.Release();
					Service.mClient.Close();
					Service.mClient.Dispose();
					break;
			}
			Service.ewhServiceStarted.Close();
			Service.ewhServiceStarted.Dispose();
			Service.ewhServiceStoped.Close();
			Service.ewhServiceStoped.Dispose();
			Service.ewhClientStarted.Close();
			Service.ewhClientStarted.Dispose();
			Service.ewhClientStoped.Close();
			Service.ewhClientStoped.Dispose();
			Service.ewhClientOnStart.Close();
			Service.ewhClientOnStart.Dispose();
			Service.ewhClientOnStop.Close();
			Service.ewhClientOnStop.Dispose();
		}
		protected override void OnStart(string[] args) {
			this.ServiceStart();
		}
		protected override void OnStop() {
			this.ServiceStop();
		}
		public void Log(string sLog) {
			this.elEventLog.WriteEntry(sLog, System.Diagnostics.EventLogEntryType.Information);
		}
		public void LogIfDebug(string sLog) {
#if DEBUG
			Log(sLog);
#endif
		}
		public void Warn(string sWarn) {
			this.elEventLog.WriteEntry(sWarn, System.Diagnostics.EventLogEntryType.Warning);
		}
		public void Error(string sError) {
			this.elEventLog.WriteEntry(sError, System.Diagnostics.EventLogEntryType.Error);
		}

		private static uint SendInputAdmin(WinApi.Input[] pInputs) {
			if(Service.SessionId == WinApi.Kernel32.WTSGetActiveConsoleSessionId()) {
				uint uiInput = WinApi.User32.SendInput((uint)pInputs.Length, pInputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinApi.Input)));
				if(0 == uiInput) {
					System.IntPtr hLastInputDesktop = WinApi.User32.GetThreadDesktop(WinApi.Kernel32.GetCurrentThreadId());
					System.IntPtr hInputDesktop = WinApi.User32.OpenInputDesktop(0, true, 0x10000000);
					if(((int)hInputDesktop) == 0) {
					} else if(!WinApi.User32.SetThreadDesktop(hInputDesktop)) {
					} else {
						if(((int)hLastInputDesktop) == 0) {
						} else if(!WinApi.User32.CloseDesktop(hLastInputDesktop)) {
						} else {
						}
						uiInput = WinApi.User32.SendInput((uint)pInputs.Length, pInputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinApi.Input)));
						if(0 == uiInput) {
						}
					}
				}
				return uiInput;
			} else {
				System.Windows.Forms.Application.Exit();
				return 0;
			}
		}
		private static void OpenUserInteractiveProcess(string strApplication, string strArgument, out WinApi.ProcessInformation piProcess) {

			uint uiSessionId = WinApi.Kernel32.WTSGetActiveConsoleSessionId();
			if(uiSessionId == 0xFFFFFFFF) {
				throw new System.Exception("Unable to get active console session id");
			}

			uint uiWinlogonPid = (uint)System.Array.Find<System.Diagnostics.Process>(System.Diagnostics.Process.GetProcessesByName("winlogon"), delegate (System.Diagnostics.Process p) {
				return (uint)p.SessionId == uiSessionId;
			}).Id;
			if(uiWinlogonPid == 0) {
				throw new System.Exception("Unable to get winlogon pid");
			}

			System.IntPtr hProcess = WinApi.Kernel32.OpenProcess(0x2000000, false, uiWinlogonPid);
			if(hProcess == System.IntPtr.Zero) {
				throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			}

			System.IntPtr hProcessToken;
			if(!WinApi.Advapi32.OpenProcessToken(hProcess, 0x0002, out hProcessToken)) {
				WinApi.Kernel32.CloseHandle(hProcess);
				throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			}
			WinApi.Kernel32.CloseHandle(hProcess);

			System.IntPtr hProcessTokenDupplicate;
			WinApi.SecurityAttributes sa = new WinApi.SecurityAttributes();
			sa.Length = System.Runtime.InteropServices.Marshal.SizeOf(sa);
			if(!WinApi.Advapi32.DuplicateTokenEx(hProcessToken, WinApi.Advapi32.MAXIMUM_ALLOWED, ref sa, WinApi.SecurityImpersonationLevel.SecurityImpersonation, WinApi.TokenType.TokenPrimary, out hProcessTokenDupplicate)) {
				WinApi.Kernel32.CloseHandle(hProcessToken);
				throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			}
			WinApi.Kernel32.CloseHandle(hProcessToken);

			System.IntPtr lpEnvironmentBlock;
			System.IntPtr hUserToken;
			if(!WinApi.Wtsapi32.WTSQueryUserToken(uiSessionId, out hUserToken)) {
				lpEnvironmentBlock = System.IntPtr.Zero;
				//throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			} else {
				if(!WinApi.Userenv.CreateEnvironmentBlock(out lpEnvironmentBlock, hUserToken, false)) {
					WinApi.Kernel32.CloseHandle(hUserToken);
					throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
				}
				WinApi.Kernel32.CloseHandle(hUserToken);
			}

			WinApi.StartupInfo si = new WinApi.StartupInfo();
			si.cb = System.Runtime.InteropServices.Marshal.SizeOf(si);
			si.lpDesktop = "winsta0\\default";
			if(!WinApi.Advapi32.CreateProcessAsUser(hProcessTokenDupplicate, strApplication, strArgument, ref sa, ref sa, false, 0x00000400, lpEnvironmentBlock, System.IO.Path.GetDirectoryName(strApplication), ref si, out piProcess)) {
				WinApi.Kernel32.CloseHandle(hProcessTokenDupplicate);
				if(lpEnvironmentBlock != System.IntPtr.Zero) {
					WinApi.Userenv.DestroyEnvironmentBlock(lpEnvironmentBlock);
				}
				throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			}

			WinApi.Kernel32.CloseHandle(hProcessTokenDupplicate);
			if(lpEnvironmentBlock != System.IntPtr.Zero) {
				WinApi.Userenv.DestroyEnvironmentBlock(lpEnvironmentBlock);
			}
		}
		private static bool WaitProcess(WinApi.ProcessInformation piProcess, uint dwMilliseconds = 0xFFFFFFFF) {
			switch((WinApi.ObjectSate)WinApi.Kernel32.WaitForSingleObject(piProcess.hProcess, dwMilliseconds)) {
				case WinApi.ObjectSate.WAIT_TIMEOUT:
					return false;
				case WinApi.ObjectSate.WAIT_OBJECT_0:
					return true;
				default:
					throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			}
		}

		private void ThreadServeur() {
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			try {
				switch(this.stType) {
					case ServiceType.Server:
						
						System.Net.Sockets.Socket socServer = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
						socServer.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, this.iPort));
						socServer.Listen(10);
						System.Func<System.Net.Sockets.Socket> fServer = delegate () {
							try {
								return socServer.Accept();
							} catch(System.Net.Sockets.SocketException ex) {
								switch(ex.ErrorCode) {
									case 10004:
										return null;
									default:
										throw;
								}
							}
						};
						System.Threading.Tasks.Task<System.Net.Sockets.Socket> tServer = System.Threading.Tasks.Task<System.Net.Sockets.Socket>.Run(fServer);

						System.IO.Pipes.NamedPipeServerStream psServer = new System.IO.Pipes.NamedPipeServerStream("{2DCF2389-4969-483D-AA13-58FD8DDDD2D5}", System.IO.Pipes.PipeDirection.Out, 1, System.IO.Pipes.PipeTransmissionMode.Message, System.IO.Pipes.PipeOptions.Asynchronous);
						WinApi.ProcessInformation piProcess = new WinApi.ProcessInformation() { hProcess = System.IntPtr.Zero };

						System.Threading.WaitHandle[] tabEventServer = new System.Threading.WaitHandle[] { Service.mreStop, Service.ewhClientOnStart, Service.ewhClientOnStop, ((System.IAsyncResult)tServer).AsyncWaitHandle };
						bool bAliveServer = true;
						do {
							switch(System.Threading.WaitHandle.WaitAny(tabEventServer, -1, true)) {
								case 0:
									bAliveServer = false;
									break;
								case 1:
									if(!psServer.IsConnected) {
										psServer.WaitForConnection();
									}
									break;
								case 2:
									if(psServer.IsConnected) {
										psServer.Disconnect();
									}
									break;
								case 3:
									if(!Service.ewhClientStarted.WaitOne(System.TimeSpan.Zero, true)) {
										Service.mClient = new System.Threading.Semaphore(1, 1, "Global\\{C4CCA362-510C-4E66-A316-29F9ADE7F664}", out _, MagicRemoteService.Program.ssAll);
										Service.mClient.WaitOne(-1, true);
										Service.mClient.Release();
										Service.mClient.Close();
										Service.mClient.Dispose();
										if(piProcess.hProcess != System.IntPtr.Zero && !WaitProcess(piProcess, 0)) {
											WaitProcess(piProcess);
										}
										OpenUserInteractiveProcess(System.Reflection.Assembly.GetExecutingAssembly().Location, "-c", out piProcess);
									}
									if(!psServer.IsConnected) {
										psServer.WaitForConnection();
									}
									bf.Serialize(psServer, tServer.Result.DuplicateAndClose(System.Array.Find<System.Diagnostics.Process>(System.Diagnostics.Process.GetProcessesByName("MagicRemoteService"), delegate (System.Diagnostics.Process p) {
										return p.Id != System.Diagnostics.Process.GetCurrentProcess().Id;
									}).Id));
									tServer = System.Threading.Tasks.Task<System.Net.Sockets.Socket>.Run(fServer);
									tabEventServer[3] = ((System.IAsyncResult)tServer).AsyncWaitHandle;
									break;
							}
						} while(bAliveServer);
						if(psServer.IsConnected) {
							psServer.Disconnect();
						}
						psServer.Close();
						psServer.Dispose();

						WinApi.Kernel32.CloseHandle(piProcess.hProcess);
						socServer.Close();
						socServer.Dispose();
						//tServer.Wait();
						//tServer.Dispose();

						break;
					case ServiceType.Both:

						System.Net.Sockets.Socket socBoth = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
						socBoth.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, this.iPort));
						socBoth.Listen(10);
						System.Func<System.Net.Sockets.Socket> fBoth = delegate () {
							try {
								return socBoth.Accept();
							} catch(System.Net.Sockets.SocketException ex) {
								switch(ex.ErrorCode) {
									case 10004:
										return null;
									default:
										throw;
								}
							}
						};
						System.Threading.Tasks.Task<System.Net.Sockets.Socket> tBoth = System.Threading.Tasks.Task<System.Net.Sockets.Socket>.Run(fBoth);

						System.Collections.Generic.List<System.Threading.Thread> liClientBoth = new System.Collections.Generic.List<System.Threading.Thread>();

						System.Threading.WaitHandle[] tabEventBoth = new System.Threading.WaitHandle[] { Service.mreStop, Service.ewhServiceStarted, ((System.IAsyncResult)tBoth).AsyncWaitHandle };
						bool bAliveBoth = true;
						do {
							switch(System.Threading.WaitHandle.WaitAny(tabEventBoth, -1, true)) {
								case 0:
									bAliveBoth = false;
									break;
								case 1:
									System.Threading.Tasks.Task.Run(delegate () {
										this.ServiceStop();
										this.ServiceStart();
									});
									bAliveBoth = false;
									break;
								case 2:
									System.Net.Sockets.Socket socClient = tBoth.Result;
									System.Threading.Thread thrClient = new System.Threading.Thread(delegate () {
										this.ThreadClient(socClient);
									});
									thrClient.Start();
									liClientBoth.Add(thrClient);
									tBoth = System.Threading.Tasks.Task<System.Net.Sockets.Socket>.Run(fBoth);
									tabEventBoth[2] = ((System.IAsyncResult)tBoth).AsyncWaitHandle;
									break;
							}
						} while(bAliveBoth);

						liClientBoth.RemoveAll(delegate (System.Threading.Thread thr) {
							thr.Join();
							return true;
						});
						socBoth.Close();
						socBoth.Dispose();
						//tBoth.Wait();
						//tBoth.Dispose();
						break;
					case ServiceType.Client:
						System.Collections.Generic.List<System.Threading.Thread> liClient = new System.Collections.Generic.List<System.Threading.Thread>();

						System.IO.Pipes.NamedPipeClientStream psClient = new System.IO.Pipes.NamedPipeClientStream(".", "{2DCF2389-4969-483D-AA13-58FD8DDDD2D5}", System.IO.Pipes.PipeDirection.In, System.IO.Pipes.PipeOptions.Asynchronous);

						psClient.Connect();

						System.Func<object> fClient = new System.Func<object>(delegate() {
							byte[] tabData = new byte[4096];
							int iData = 0;
							int iRead;
							object o = null;
							do {
								iRead = psClient.Read(tabData, iData, 4096 - iData);
								if(iRead != 0) {
									iData += iRead;
									try {
										o = bf.Deserialize(new System.IO.MemoryStream(tabData, 0, iData));
									} catch(System.Runtime.Serialization.SerializationException ex) {
										switch(ex.HResult & 0x0000FFFF) {
											case 5388:
												break;
											default:
												throw;
										}
									}
								}
							} while(iRead != 0 && o == null);
							return o;
						});
						System.Threading.Tasks.Task<object> tClient = System.Threading.Tasks.Task<object>.Run(fClient);

						System.Threading.WaitHandle[] tabEventClient = new System.Threading.WaitHandle[] { Service.mreStop, ((System.IAsyncResult)tClient).AsyncWaitHandle };
						bool bAliveClient = true;
						do {
							switch(System.Threading.WaitHandle.WaitAny(tabEventClient, -1, true)) {
								case 0:
									bAliveClient = false;
									break;
								case 1:
									if(tClient.Result == null) {
										if(!(System.Array.IndexOf<string>(System.Environment.GetCommandLineArgs(), "-c") < 0) && System.Windows.Forms.Application.OpenForms.Count == 0) {
											System.Windows.Forms.Application.Exit();
										} else {
											System.Threading.Tasks.Task.Run(delegate () {
												this.ServiceStop();
												this.ServiceStart();
											});
											bAliveClient = false;
										}
									} else if(tClient.Result is System.Net.Sockets.SocketInformation) {
										System.Net.Sockets.Socket socClient = new System.Net.Sockets.Socket((System.Net.Sockets.SocketInformation)tClient.Result);
										System.Threading.Thread thrClient = new System.Threading.Thread(delegate () {
											this.ThreadClient(socClient);
										});
										thrClient.Start();
										liClient.Add(thrClient);
										tClient = System.Threading.Tasks.Task<object>.Run(fClient);
										tabEventClient[1] = ((System.IAsyncResult)tClient).AsyncWaitHandle;
									} else {
										throw new System.Exception("erreur de communication");
									}
									break;
							}
						} while(bAliveClient);
						tClient.Wait();
						tClient.Dispose();
						psClient.Close();
						psClient.Dispose();

						liClient.RemoveAll(delegate (System.Threading.Thread thr) {
							thr.Join();
							return true;
						});
						break;
				}
			} catch(System.Exception eException) {
				this.Error(eException.ToString());
			}
		}
		private void ThreadClient(System.Net.Sockets.Socket socClient) {
			try {
				this.Log("Socket accepted [" + socClient.GetHashCode() + "]");
				byte[] tabData = new byte[4096];
				System.Threading.ManualResetEvent mreClientStop = new System.Threading.ManualResetEvent(false);
				System.Timers.Timer tUserInput = new System.Timers.Timer();
				tUserInput.Interval = 10;
				tUserInput.AutoReset = true;
				tUserInput.Elapsed += delegate (System.Object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					WinApi.LastInputInfo lii = new WinApi.LastInputInfo();
					lii.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lii);
					if(!WinApi.User32.GetLastInputInfo(ref lii)) {
					} else if(((uint)System.Environment.TickCount - lii.dwTime) < 10) {
						System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
						pProcess.StartInfo.FileName = "shutdown";
						pProcess.StartInfo.Arguments = "/a";
						pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
						pProcess.Start();
						tUserInput.Stop();
						this.Log("Client user input activity on socket [" + socClient.GetHashCode() + "]");
					}
				};
				System.Timers.Timer tPongInactivity = new System.Timers.Timer();
				tPongInactivity.Interval = 5000;
				tPongInactivity.AutoReset = false;
				tPongInactivity.Elapsed += delegate (System.Object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					socClient.Send(Service.tabClose);
					mreClientStop.Set();
					this.Warn("Client timeout pong inactivity on socket [" + socClient.GetHashCode() + "]");
				};
				System.Timers.Timer tInactivity = new System.Timers.Timer();
				tInactivity.Interval = this.iTimeoutInactivity - 300000;
				tInactivity.AutoReset = false;
				tInactivity.Elapsed += delegate (System.Object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					socClient.Send(Service.tabPingInactivity);
					tPongInactivity.Start();
					this.Log("Client timeout inactivity on socket [" + socClient.GetHashCode() + "]");
				};

				System.Timers.Timer tPong = new System.Timers.Timer();
				tPong.Interval = 5000;
				tPong.AutoReset = false;
				tPong.Elapsed += delegate (System.Object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					socClient.Send(Service.tabClose);
					mreClientStop.Set();
					this.Warn("Client timeout pong on socket [" + socClient.GetHashCode() + "]");
				};
				System.Timers.Timer tPing = new System.Timers.Timer();
				tPing.Interval = 30000;
				tPing.AutoReset = true;
				tPing.Elapsed += delegate (System.Object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					socClient.Send(Service.tabPing);
					tPong.Start();
				};

				System.Collections.Generic.Dictionary<ushort, WinApi.Input[]> dKeyBindDown = new System.Collections.Generic.Dictionary<ushort, WinApi.Input[]>();
				System.Collections.Generic.Dictionary<ushort, WinApi.Input[]> dKeyBindUp = new System.Collections.Generic.Dictionary<ushort, WinApi.Input[]>();
				System.Collections.Generic.Dictionary<ushort, byte[]> dKeyBindActionDown = new System.Collections.Generic.Dictionary<ushort, byte[]>();
				foreach(System.Collections.Generic.KeyValuePair<ushort, Bind> kvp in this.dKeyBind) {
					switch(kvp.Value) {
						case BindMouse bm:
							switch(bm.bmvValue) {
								case BindMouseValue.Left:
									dKeyBindDown.Add(kvp.Key, new WinApi.Input[] {
										new WinApi.Input {
											type = WinApi.InputType.INPUT_MOUSE,
											u = new WinApi.DummyUnionName {
												mi = new WinApi.MouseInput {
													dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_LEFTDOWN,
													dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
												}
											}
										}
									});
									dKeyBindUp.Add(kvp.Key, new WinApi.Input[] {
										new WinApi.Input {
											type = WinApi.InputType.INPUT_MOUSE,
											u = new WinApi.DummyUnionName {
												mi = new WinApi.MouseInput {
													dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_LEFTUP,
													dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
												}
											}
										}
									});
									break;
								case BindMouseValue.Right:
									dKeyBindDown.Add(kvp.Key, new WinApi.Input[] {
										new WinApi.Input {
											type = WinApi.InputType.INPUT_MOUSE,
											u = new WinApi.DummyUnionName {
												mi = new WinApi.MouseInput {
													dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_RIGHTDOWN,
													dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
												}
											}
										}
									});
									dKeyBindUp.Add(kvp.Key, new WinApi.Input[] {
										new WinApi.Input {
											type = WinApi.InputType.INPUT_MOUSE,
											u = new WinApi.DummyUnionName {
												mi = new WinApi.MouseInput {
													dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_RIGHTUP,
													dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
												}
											}
										}
									});
									break;
								case BindMouseValue.Middle:
									dKeyBindDown.Add(kvp.Key, new WinApi.Input[] {
										new WinApi.Input {
											type = WinApi.InputType.INPUT_MOUSE,
											u = new WinApi.DummyUnionName {
												mi = new WinApi.MouseInput {
													dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_MIDDLEDOWN,
													dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
												}
											}
										}
									});
									dKeyBindUp.Add(kvp.Key, new WinApi.Input[] {
										new WinApi.Input {
											type = WinApi.InputType.INPUT_MOUSE,
											u = new WinApi.DummyUnionName {
												mi = new WinApi.MouseInput {
													dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_MIDDLEUP,
													dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
												}
											}
										}
									});
									break;
							}
							break;
						case BindKeyboard bk:
							if((bk.usScanCode & 0xFF00) == 0xE000) {
								dKeyBindDown.Add(kvp.Key, new WinApi.Input[] {
									new WinApi.Input {
										type = WinApi.InputType.INPUT_KEYBOARD,
										u = new WinApi.DummyUnionName {
											ki = new WinApi.KeybdInput {
												wScan = bk.usScanCode,
												dwFlags = WinApi.KeybdInputFlags.KEYEVENTF_KEYDOWN | WinApi.KeybdInputFlags.KEYEVENTF_SCANCODE | WinApi.KeybdInputFlags.KEYEVENTF_EXTENDEDKEY,
												dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
											}
										}
									}
								});
								dKeyBindUp.Add(kvp.Key, new WinApi.Input[] {
									new WinApi.Input {
										type = WinApi.InputType.INPUT_KEYBOARD,
										u = new WinApi.DummyUnionName {
											ki = new WinApi.KeybdInput {
												wScan = bk.usScanCode,
												dwFlags = WinApi.KeybdInputFlags.KEYEVENTF_KEYUP | WinApi.KeybdInputFlags.KEYEVENTF_SCANCODE | WinApi.KeybdInputFlags.KEYEVENTF_EXTENDEDKEY,
												dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
											}
										}
									}
								});
							} else {
								dKeyBindDown.Add(kvp.Key, new WinApi.Input[] {
									new WinApi.Input {
										type = WinApi.InputType.INPUT_KEYBOARD,
										u = new WinApi.DummyUnionName {
											ki = new WinApi.KeybdInput {
												wScan = bk.usScanCode,
												dwFlags = WinApi.KeybdInputFlags.KEYEVENTF_KEYDOWN | WinApi.KeybdInputFlags.KEYEVENTF_SCANCODE,
												dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
											}
										}
									}
								});
								dKeyBindUp.Add(kvp.Key, new WinApi.Input[] {
									new WinApi.Input {
										type = WinApi.InputType.INPUT_KEYBOARD,
										u = new WinApi.DummyUnionName {
											ki = new WinApi.KeybdInput {
												wScan = bk.usScanCode,
												dwFlags = WinApi.KeybdInputFlags.KEYEVENTF_KEYUP | WinApi.KeybdInputFlags.KEYEVENTF_SCANCODE,
												dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
											}
										}
									}
								});
							}
							break;
						case BindAction ba:
							dKeyBindActionDown.Add(kvp.Key, new byte[] {
								(0b1000 << 4) | (0x2 << 0),
								0x01,
								(byte)ba.bavValue
							});
							break;
					}
				};

				WinApi.Input[] piPosition = new WinApi.Input[] {
					new WinApi.Input {
						type = WinApi.InputType.INPUT_MOUSE,
						u = new WinApi.DummyUnionName {
							mi = new WinApi.MouseInput {
								dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_ABSOLUTE | WinApi.MouseInputFlags.MOUSEEVENTF_MOVE,
								dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
							}
						}
					}
				};
				WinApi.Input[] piWheel = new WinApi.Input[] {
					new WinApi.Input {
						type = WinApi.InputType.INPUT_MOUSE,
						u = new WinApi.DummyUnionName {
							mi = new WinApi.MouseInput {
								dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_WHEEL,
								dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
							}
						}
					}
				};
				WinApi.Input[] piUnicode = new WinApi.Input[] {
					new WinApi.Input {
						type = WinApi.InputType.INPUT_KEYBOARD,
						u = new WinApi.DummyUnionName {
							ki = new WinApi.KeybdInput {
								dwFlags = WinApi.KeybdInputFlags.KEYEVENTF_UNICODE | WinApi.KeybdInputFlags.KEYEVENTF_KEYDOWN,
								dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
							}
						}
					}, new WinApi.Input {
						type = WinApi.InputType.INPUT_KEYBOARD,
						u = new WinApi.DummyUnionName {
							ki = new WinApi.KeybdInput {
								dwFlags = WinApi.KeybdInputFlags.KEYEVENTF_UNICODE | WinApi.KeybdInputFlags.KEYEVENTF_KEYUP,
								dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
							}
						}
					}
				};

				System.Func<ulong> fClient = delegate () {
					return (ulong)socClient.Receive(tabData);
				};
				System.Threading.Tasks.Task<ulong> tClient = System.Threading.Tasks.Task<ulong>.Run(fClient);

				System.Threading.WaitHandle[] tabEvent = new System.Threading.WaitHandle[] { Service.mreStop, mreClientStop, ((System.IAsyncResult)tClient).AsyncWaitHandle };
				bool bAlive = true;
				switch(System.Threading.WaitHandle.WaitAny(tabEvent, -1, true)) {
					case 0:
					case 1:
						bAlive = false;
						break;
					case 2:
						ulong ulLenMessage = tClient.Result;
						if(tabData[0] == 'G' && tabData[1] == 'E' && tabData[2] == 'T') {
							socClient.Send(System.Text.Encoding.UTF8.GetBytes(
								"HTTP/1.1 101 Switching Protocols\r\n" +
								"Connection: Upgrade\r\n" +
								"Upgrade: websocket\r\n" +
								"Sec-WebSocket-Accept: " + System.Convert.ToBase64String(System.Security.Cryptography.SHA1.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(System.Text.RegularExpressions.Regex.Match(System.Text.Encoding.UTF8.GetString(tabData), "Sec-WebSocket-Key: (.*)\r\n").Groups[1].Value + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"))) + "\r\n\r\n"));

							//TODO Something to ask TV if cursor visible
							this.Log("Client connected on socket [" + socClient.GetHashCode() + "]");
							tPing.Start();
							if(this.bInactivity) {
								tInactivity.Start();
							}
						} else {
							bAlive = false;
							this.Warn("Connexion refused on socket [" + socClient.GetHashCode() + "]");
						}
						break;
				}
				while(bAlive) {
					tClient = System.Threading.Tasks.Task<ulong>.Run(fClient);
					tabEvent[2] = ((System.IAsyncResult)tClient).AsyncWaitHandle;
					switch(System.Threading.WaitHandle.WaitAny(tabEvent, -1, true)) {
						case 0:
							socClient.Send(Service.tabClose);
							bAlive = false;
							break;
						case 1:
							bAlive = false;
							break;
						case 2:
							ulong ulLenMessage = tClient.Result;
							ulong ulOffsetFrame = 0;
							while(!(ulOffsetFrame == ulLenMessage)) {
								bool bFin = (tabData[ulOffsetFrame] & 0b10000000) == 0b10000000;
								bool bRsv1 = (tabData[ulOffsetFrame] & 0b01000000) == 0b01000000;
								bool bRsv2 = (tabData[ulOffsetFrame] & 0b00100000) == 0b00100000;
								bool bRsv3 = (tabData[ulOffsetFrame] & 0b00010000) == 0b00010000;
								byte ucOpcode = (byte)(tabData[ulOffsetFrame] & 0b00001111);

								bool bMask = (tabData[ulOffsetFrame + 1] & 0b10000000) == 0b10000000;
								ulong ulLenData;
								ulong ulOffsetMask;
								if((tabData[ulOffsetFrame + 1] & 0b01111111) == 0b01111110) {
									ulLenData = System.BitConverter.ToUInt16(new byte[] { tabData[ulOffsetFrame + 3], tabData[ulOffsetFrame + 2] }, 0);
									ulOffsetMask = ulOffsetFrame + 4;
								} else if((tabData[ulOffsetFrame + 1] & 0b01111111) == 0b01111111) {
									ulLenData = System.BitConverter.ToUInt64(new byte[] { tabData[ulOffsetFrame + 9], tabData[ulOffsetFrame + 8], tabData[ulOffsetFrame + 7], tabData[ulOffsetFrame + 6], tabData[ulOffsetFrame + 5], tabData[ulOffsetFrame + 4], tabData[ulOffsetFrame + 3], tabData[ulOffsetFrame + 2] }, 0);
									ulOffsetMask = ulOffsetFrame + 10;
								} else {
									ulLenData = (byte)(tabData[ulOffsetFrame + 1] & 0b01111111);
									ulOffsetMask = ulOffsetFrame + 2;
								}

								ulong ulOffsetData;
								if(bMask) {
									ulOffsetData = ulOffsetMask + 4;
									for(ulong ul = 0; ul < ulLenData; ul++) {
										tabData[ulOffsetData + ul] ^= tabData[ulOffsetMask + (ul % 4)];
									}
								} else {
									ulOffsetData = ulOffsetMask;
								}
								if(!bFin) {
									this.Warn("Unable to process split frame on socket [" + socClient.GetHashCode() + "]");
								} else {
									switch(ucOpcode) {
										case (byte)MagicRemoteService.WebSocketOpCode.Continuation:
											this.Warn("Unable to process split frame on socket [" + socClient.GetHashCode() + "]");
											break;
										case (byte)MagicRemoteService.WebSocketOpCode.Text:
											if(ulLenData != 0) {
												this.Warn("Unprocessed text message [" + System.Text.Encoding.UTF8.GetString(tabData, (int)ulOffsetData, (int)ulLenData) + "]");
											}
											break;
										case (byte)MagicRemoteService.WebSocketOpCode.Binary:
											tPing.Stop();
											tPing.Start();
											if(this.bInactivity) {
												tInactivity.Stop();
												tInactivity.Start();
											}
											if(ulLenData != 0) {
												switch(tabData[ulOffsetData + 0]) {
													case (byte)MagicRemoteService.MessageType.Position:
														piPosition[0].u.mi.dx = (int)((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1) * 65535) / 1920);
														piPosition[0].u.mi.dy = (int)((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 3) * 65535) / 1080);
														Service.SendInputAdmin(piPosition);
														//this.LogIfDebug("Processed binary message send/position [0x" + System.BitConverter.ToString(tabData, ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usX: " + System.BitConverter.ToUInt16(tabData, ulOffsetData + 1).ToString() + ", usY: " + System.BitConverter.ToUInt16(tabData, ulOffsetData + 3).ToString());
														break;
													case (byte)MagicRemoteService.MessageType.Wheel:
														piWheel[0].u.mi.mouseData = (uint)(-System.BitConverter.ToInt16(tabData, (int)ulOffsetData + 1) * 3);
														Service.SendInputAdmin(piWheel);
														this.LogIfDebug("Processed binary message send/wheel [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], sY: " + (-System.BitConverter.ToInt16(tabData, (int)ulOffsetData + 1)).ToString());
														break;
													case (byte)MagicRemoteService.MessageType.Visible:
														if(System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 1)) {
															MagicRemoteService.SystemCursor.HideSytemCursor();
														} else {
															MagicRemoteService.SystemCursor.ShowSytemCursor();
														}
														this.LogIfDebug("Processed binary message send/visible [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], bV: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 1).ToString());
														break;
													case (byte)MagicRemoteService.MessageType.Key:
														ushort usCode = System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1);
														if((tabData[ulOffsetData + 3] & 0x01) == 0x01) {
															if(dKeyBindDown.ContainsKey(usCode)) {
																Service.SendInputAdmin(dKeyBindDown[usCode]);
																this.LogIfDebug("Processed binary message send/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															} else if(dKeyBindActionDown.ContainsKey(usCode)) {
																socClient.Send(dKeyBindActionDown[usCode]);
																this.LogIfDebug("Processed binary message send/key action [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															} else {
																this.LogIfDebug("Unprocessed binary message send/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															}
														} else {
															if(dKeyBindUp.ContainsKey(usCode)) {
																Service.SendInputAdmin(dKeyBindUp[usCode]);
																this.LogIfDebug("Processed binary message send/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															} else {
																this.LogIfDebug("Unprocessed binary message send/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															}
														}
														break;
													case (byte)MagicRemoteService.MessageType.Unicode:
														piUnicode[0].u.ki.wScan = System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1);
														piUnicode[1].u.ki.wScan = System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1);
														Service.SendInputAdmin(piUnicode);
														this.LogIfDebug("Processed binary message send/unicode [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.Text.Encoding.UTF8.GetString(tabData, (int)ulOffsetData + 1, 2));
														break;
													case (byte)MagicRemoteService.MessageType.Shutdown:
														System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
														pProcess.StartInfo.FileName = "shutdown";
														pProcess.StartInfo.Arguments = "/s /t 0";
														pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
														pProcess.Start();
														this.LogIfDebug("Processed binary message send/shutdown [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "]");
														break;
													default:
														this.Warn("Uprocessed binary message [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "]");
														break;
												}
											}
											break;
										case (byte)MagicRemoteService.WebSocketOpCode.ConnectionClose:
											if(bMask) {
												tabData[ulOffsetFrame + 1] = (byte)((0b10000000 & 0b10000000) | (tabData[ulOffsetFrame] & 0b01111111));
												for(ulong ul = 0; ul < ulLenData; ul++) {
													tabData[ulOffsetMask + ul] = tabData[ulOffsetData + ul];
												}
											}
											socClient.Send(tabData, (int)ulOffsetFrame, (int)(ulOffsetMask - ulOffsetFrame + ulLenData), System.Net.Sockets.SocketFlags.None);
											mreClientStop.Set();
											this.Log("Client disconnected on socket [" + socClient.GetHashCode() + "]");
											break;
										case (byte)MagicRemoteService.WebSocketOpCode.Ping:
											tabData[ulOffsetFrame] = (byte)((tabData[ulOffsetFrame] & 0xF0) | (0x0A & 0x0F));
											if(bMask) {
												tabData[ulOffsetFrame + 1] = (byte)((0b10000000 & 0b10000000) | (tabData[ulOffsetFrame] & 0b01111111));
												for(ulong ul = 0; ul < ulLenData; ul++) {
													tabData[ulOffsetMask + ul] = tabData[ulOffsetData + ul];
												}
											}
											socClient.Send(tabData, (int)ulOffsetFrame, (int)(ulOffsetMask - ulOffsetFrame + ulLenData), System.Net.Sockets.SocketFlags.None);
											this.LogIfDebug("Ping received on socket [" + socClient.GetHashCode() + "]");
											break;
										case (byte)MagicRemoteService.WebSocketOpCode.Pong:
											if(ulLenData != 0) {
												switch(tabData[ulOffsetData + 0]) {
													case 0x01:
														tPongInactivity.Stop();
														System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
														pProcess.StartInfo.FileName = "shutdown";
														pProcess.StartInfo.Arguments = "/s /t 300";
														pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
														pProcess.Start();
														tUserInput.Start();
														this.LogIfDebug("Pong incativity received on socket [" + socClient.GetHashCode() + "]");
														break;
													default:
														this.Warn("Uprocessed pong message [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "]");
														break;
												}
											} else {
												tPong.Stop();
												this.LogIfDebug("Pong received on socket [" + socClient.GetHashCode() + "]");
											}
											break;
										default:
											this.Warn("Unprocessed message [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], " + System.Text.Encoding.Default.GetString(tabData, (int)ulOffsetData, (int)ulLenData));
											break;
									}
								}
								ulOffsetFrame = ulOffsetData + ulLenData;
							}
							break;
					}
				}
				SystemCursor.ShowSytemCursor();
				tPing.Stop();
				tPong.Stop();
				if(this.bInactivity) {
					tInactivity.Stop();
					tPongInactivity.Stop();
				}
				socClient.Close();
				socClient.Dispose();
				this.Log("Socket closed [" + socClient.GetHashCode() + "]");
			} catch(System.Exception eException) {
				SystemCursor.ShowSytemCursor();
				this.Error(eException.ToString());
			}
		}
	}
}
