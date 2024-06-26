﻿
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
		Unicode = 0x04,
		Shutdown = 0x05
	}
	public partial class Service : System.ServiceProcess.ServiceBase {
		private volatile int iPort;
		private volatile bool bInactivity;
		private volatile int iTimeoutInactivity;
		private volatile bool bVideoInput;
		private volatile int iTimeoutVideoInput;
		private volatile System.Collections.Generic.Dictionary<ushort, Bind[]> dBind = new System.Collections.Generic.Dictionary<ushort, Bind[]>() {
			{ 0x0001, null },
			{ 0x0002, null },
			{ 0x0008, null },
			{ 0x000D, null },
			{ 0x0021, null },
			{ 0x0022, null },
			{ 0x0025, null },
			{ 0x0026, null },
			{ 0x0027, null },
			{ 0x0028, null },
			{ 0x0030, null },
			{ 0x0031, null },
			{ 0x0032, null },
			{ 0x0033, null },
			{ 0x0034, null },
			{ 0x0035, null },
			{ 0x0036, null },
			{ 0x0037, null },
			{ 0x0038, null },
			{ 0x0039, null },
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

		private static System.Diagnostics.EventLog elEventLog = new System.Diagnostics.EventLog("Application", ".", "MagicRemoteService");

		private System.Threading.Thread thrServer;
		private WinApi.ServiceCurrentState scsState;
		private ServiceType stType;

		private static System.Threading.ManualResetEvent mreStop = new System.Threading.ManualResetEvent(true);
		private static System.Threading.AutoResetEvent areSessionChanged = new System.Threading.AutoResetEvent(false);
		private static System.Threading.EventWaitHandle ewhServerStarted;
		private static System.Threading.EventWaitHandle ewhClientStarted;
		private static System.Threading.EventWaitHandle ewhSessionChanged;
		private static System.Threading.EventWaitHandle ewhClientConnecting;
		private static System.Threading.EventWaitHandle ewhServerMessage;
		private static System.Threading.EventWaitHandle ewhServerDisconnecting;

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
		private static readonly byte[] tabPingUserInput = { (0b1000 << 4) | (0x9 << 0), 0x01, 0x01 };
		static Service() {
			if(!System.Diagnostics.EventLog.SourceExists("MagicRemoteService")) {
				System.Diagnostics.EventLog.CreateEventSource("MagicRemoteService", "Application");
			}
		}
		public Service() {
			this.InitializeComponent();
		}
		public void ServiceStart() {
			Service.ewhServerStarted = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, @"Global\{FFB31601-E362-48A5-B9A2-5DF29A3B06C1}", out _, Program.ewhsAll);
			Service.ewhClientStarted = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, @"Global\{9878BC83-46A0-412B-86B6-10F1C43FC0D9}", out _, Program.ewhsAll);
			Service.ewhSessionChanged = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset, @"Global\{996C2D37-8FAC-4C89-8A00-CE30CBE66B87}", out _, Program.ewhsAll);
			Service.ewhClientConnecting = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset, @"Global\{06031D31-621A-4288-850E-8FEE0ED3F054}", out _, Program.ewhsAll);
			Service.ewhServerMessage = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset, @"Global\{78968501-8AE0-424D-B82E-EA0A0BEA3414}", out _, Program.ewhsAll);
			Service.ewhServerDisconnecting = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset, @"Global\{C45127E0-B626-46D0-8610-C5DE20E4F790}", out _, Program.ewhsAll);

			if(!System.Environment.UserInteractive) {
				this.stType = ServiceType.Server;
			} else if(!Service.ewhServerStarted.WaitOne(System.TimeSpan.Zero)) {
				this.stType = ServiceType.Both;
			} else {
				this.stType = ServiceType.Client;
			}

			WinApi.ServiceStatus ssServiceStatus = new WinApi.ServiceStatus();
			switch(this.stType) {
				case ServiceType.Server:
					Service.Log("Service server start");
					break;
				case ServiceType.Both:
					Service.Log("Service both start");
					break;
				case ServiceType.Client:
					Service.Log("Service client start");
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
			Microsoft.Win32.RegistryKey rkMagicRemoteService = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey(@"Software\MagicRemoteService");
			if(rkMagicRemoteService == null) {
				this.iPort = 41230;
				this.bInactivity = true;
				this.iTimeoutInactivity = 7200000;
				this.bVideoInput = true;
				this.iTimeoutVideoInput = 900000;
			} else {
				this.iPort = (int)rkMagicRemoteService.GetValue("Port", 41230);
				this.bInactivity = (int)rkMagicRemoteService.GetValue("Inactivity", 1) != 0;
				this.iTimeoutInactivity = (int)rkMagicRemoteService.GetValue("TimeoutInactivity", 7200000);
				this.bVideoInput = (int)rkMagicRemoteService.GetValue("VideoInput", 1) != 0;
				this.iTimeoutVideoInput = (int)rkMagicRemoteService.GetValue("TimeoutVideoInput", 900000);
			}
			Microsoft.Win32.RegistryKey rkMagicRemoteServiceRemoteBind = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey(@"Software\MagicRemoteService\Remote\Bind");
			if(rkMagicRemoteServiceRemoteBind == null) {
				this.dBind[0x0001] = new Bind[] { new BindMouse(BindMouseValue.Left) };
				this.dBind[0x0002] = new Bind[] { new BindMouse(BindMouseValue.Right) };
				this.dBind[0x0008] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Back, 0x0E, false) };
				this.dBind[0x000D] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Enter, 0x1C, false) };
				this.dBind[0x0021] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.ControlKey, 0x1D, false), new BindKeyboard((byte)System.Windows.Forms.Keys.C, 0x2E, false) };
				this.dBind[0x0022] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.ControlKey, 0x1D, false), new BindKeyboard((byte)System.Windows.Forms.Keys.V, 0x2F, false) };
				this.dBind[0x0025] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Left, 0x4B, true) };
				this.dBind[0x0026] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Up, 0x48, true) };
				this.dBind[0x0027] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Right, 0x4D, true) };
				this.dBind[0x0028] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Down, 0x50, true) };
				this.dBind[0x0030] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad0, 0x52, false) };
				this.dBind[0x0031] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad1, 0x4F, false) };
				this.dBind[0x0032] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad2, 0x50, false) };
				this.dBind[0x0033] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad3, 0x51, false) };
				this.dBind[0x0034] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad4, 0x4B, false) };
				this.dBind[0x0035] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad5, 0x4C, false) };
				this.dBind[0x0036] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad6, 0x4D, false) };
				this.dBind[0x0037] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad7, 0x47, false) };
				this.dBind[0x0038] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad8, 0x48, false) };
				this.dBind[0x0039] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.NumPad9, 0x49, false) };
				this.dBind[0x0193] = new Bind[] { new BindAction(BindActionValue.Shutdown) };
				this.dBind[0x0194] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.LWin, 0x5B, true) };
				this.dBind[0x0195] = new Bind[] { new BindMouse(BindMouseValue.Right) };
				this.dBind[0x0196] = new Bind[] { new BindAction(BindActionValue.Keyboard) };
				this.dBind[0x01CD] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Escape, 0x01, false) };
				this.dBind[0x019F] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Play, 0x00, false) };
				this.dBind[0x0013] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.Pause, 0x00, false) };
				this.dBind[0x01A1] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.MediaNextTrack, 0x00, false) };
				this.dBind[0x019C] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.MediaPreviousTrack, 0x00, false) };
				this.dBind[0x019D] = new Bind[] { new BindKeyboard((byte)System.Windows.Forms.Keys.MediaStop, 0x00, false) };
			} else {
				foreach(string sKey in rkMagicRemoteServiceRemoteBind.GetSubKeyNames()) {
					System.Collections.Generic.List<Bind> liBind = new System.Collections.Generic.List<Bind>();
					Microsoft.Win32.RegistryKey rkMagicRemoteServiceRemoteBindKey = rkMagicRemoteServiceRemoteBind.OpenSubKey(sKey);
					foreach(string sBind in rkMagicRemoteServiceRemoteBindKey.GetSubKeyNames()) {
						Microsoft.Win32.RegistryKey rkMagicRemoteServiceRemoteBindBind = rkMagicRemoteServiceRemoteBindKey.OpenSubKey(sBind);
						switch((int)rkMagicRemoteServiceRemoteBindBind.GetValue("Kind")) {
							case 0x00:
								liBind.Add(new BindMouse((BindMouseValue)(int)rkMagicRemoteServiceRemoteBindBind.GetValue("Value", 0x0000)));
								break;
							case 0x01:
								liBind.Add(new BindKeyboard((byte)(int)rkMagicRemoteServiceRemoteBindBind.GetValue("VirtualKey", 0x00), (byte)(int)rkMagicRemoteServiceRemoteBindBind.GetValue("ScanCode", 0x00), (int)rkMagicRemoteServiceRemoteBindBind.GetValue("Extended", 0x00) == 0x01));
								break;
							case 0x02:
								liBind.Add(new BindAction((BindActionValue)(int)rkMagicRemoteServiceRemoteBindBind.GetValue("Value", 0x00)));
								break;
							case 0x03:
								liBind.Add(new BindCommand((string)rkMagicRemoteServiceRemoteBindBind.GetValue("Command")));
								break;
						}
					}
					this.dBind[ushort.Parse(sKey.Substring(2), System.Globalization.NumberStyles.HexNumber)] = liBind.ToArray();
				}
			}
			switch(this.stType) {
				case ServiceType.Server:
					Service.ewhServerStarted.Set();
					break;
				case ServiceType.Both:
					break;
				case ServiceType.Client:
					Service.ewhClientStarted.Set();
					break;
			}
			Service.mreStop.Reset();
			this.thrServer = new System.Threading.Thread(delegate () {
				this.ThreadServer();
			});
			this.thrServer.Start();
			switch(this.stType) {
				case ServiceType.Server:
					Service.Log("Service server started");
					break;
				case ServiceType.Both:
					Service.Log("Service both started");
					break;
				case ServiceType.Client:
					Service.Log("Service client started");
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
					Service.Log("Service server stop");
					break;
				case ServiceType.Both:
					Service.Log("Service both stop");
					break;
				case ServiceType.Client:
					Service.Log("Service client stop");
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
					Service.ewhServerStarted.Reset();
					break;
				case ServiceType.Both:
					break;
				case ServiceType.Client:
					Service.ewhClientStarted.Reset();
					break;
			}
			Service.mreStop.Set();
			this.thrServer.Join();
			this.thrServer = null;
			switch(this.stType) {
				case ServiceType.Server:
					Service.Log("Service server stoped");
					break;
				case ServiceType.Both:
					Service.Log("Service both stoped");
					break;
				case ServiceType.Client:
					Service.Log("Service client stoped");
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
					break;
				case ServiceType.Both:
				case ServiceType.Client:
					break;
			}
			Service.ewhServerStarted.Close();
			Service.ewhServerStarted.Dispose();
			Service.ewhClientStarted.Close();
			Service.ewhClientStarted.Dispose();
			Service.ewhSessionChanged.Close();
			Service.ewhSessionChanged.Dispose();
			Service.ewhClientConnecting.Close();
			Service.ewhClientConnecting.Dispose();
			Service.ewhServerMessage.Close();
			Service.ewhServerMessage.Dispose();
			Service.ewhServerDisconnecting.Close();
			Service.ewhServerDisconnecting.Dispose();
		}
		protected override void OnStart(string[] args) {
			this.ServiceStart();
		}
		protected override void OnStop() {
			this.ServiceStop();
		}
		protected override void OnSessionChange(System.ServiceProcess.SessionChangeDescription scd) {
			switch(scd.Reason) {
				case System.ServiceProcess.SessionChangeReason.ConsoleConnect:
					Service.areSessionChanged.Set();
					break;
				default:
					break;
			}
		}
		public static void Log(string sLog) {
			Service.elEventLog.WriteEntry(sLog, System.Diagnostics.EventLogEntryType.Information);
		}
		public static void LogIfDebug(string sLog) {
#if DEBUG
			Service.Log(sLog);
#endif
		}
		public static void Warn(string sWarn) {
			Service.elEventLog.WriteEntry(sWarn, System.Diagnostics.EventLogEntryType.Warning);
		}
		public static void Error(string sError) {
			Service.elEventLog.WriteEntry(sError, System.Diagnostics.EventLogEntryType.Error);
		}
		private static bool SetThreadInputDesktop() {
			System.IntPtr hInputDesktop = WinApi.User32.OpenInputDesktop(0, true, 0x10000000);
			if(System.IntPtr.Zero == hInputDesktop) {
				return false;
			} else if(!WinApi.User32.SetThreadDesktop(hInputDesktop)) {
				WinApi.User32.CloseDesktop(hInputDesktop);
				return false;
			} else {
				WinApi.User32.CloseDesktop(hInputDesktop);
				return true;
			}
		}
		private static uint SendInputAdmin(WinApi.Input[] pInputs) {
			uint uiInput = WinApi.User32.SendInput((uint)pInputs.Length, pInputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinApi.Input)));
			if(0 == uiInput && SetThreadInputDesktop()) {
				return WinApi.User32.SendInput((uint)pInputs.Length, pInputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinApi.Input)));
			} else {
				return uiInput;
			}
		}
		private static uint OpenUserInteractiveProcess(string strApplication, string strArgument) {
			uint uiSessionId = WinApi.Kernel32.WTSGetActiveConsoleSessionId();
			if(uiSessionId == 0xFFFFFFFF) {
				return 0;
			} else {
				System.Diagnostics.Process pWinlogon = System.Array.Find<System.Diagnostics.Process>(System.Diagnostics.Process.GetProcessesByName("winlogon"), delegate (System.Diagnostics.Process p) {
					return (uint)p.SessionId == uiSessionId;
				});
				if(pWinlogon == null) {
					throw new System.Exception("Unable to get winlogon process");
				}

				System.IntPtr hProcessToken;
				if(!WinApi.Advapi32.OpenProcessToken(pWinlogon.Handle, 0x0002, out hProcessToken)) {
					throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
				}

				System.IntPtr hProcessTokenDupplicate;
				WinApi.SecurityAttributes sa = new WinApi.SecurityAttributes();
				sa.Length = System.Runtime.InteropServices.Marshal.SizeOf(sa);
				if(!WinApi.Advapi32.DuplicateTokenEx(hProcessToken, WinApi.Advapi32.MAXIMUM_ALLOWED, ref sa, WinApi.SecurityImpersonationLevel.SecurityImpersonation, WinApi.TokenType.TokenPrimary, out hProcessTokenDupplicate)) {
					WinApi.Kernel32.CloseHandle(hProcessToken);
					throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
				}
				WinApi.Kernel32.CloseHandle(hProcessToken);

				System.IntPtr lpEnvironmentBlock;
				System.IntPtr hUserToken;
				if(!WinApi.Wtsapi32.WTSQueryUserToken(uiSessionId, out hUserToken)) {
					lpEnvironmentBlock = System.IntPtr.Zero;
					//throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
				} else {
					if(!WinApi.Userenv.CreateEnvironmentBlock(out lpEnvironmentBlock, hUserToken, true)) {
						WinApi.Kernel32.CloseHandle(hUserToken);
						throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
					}
					WinApi.Kernel32.CloseHandle(hUserToken);
				}

				WinApi.StartupInfo si = new WinApi.StartupInfo();
				WinApi.ProcessInformation piProcess;
				si.cb = System.Runtime.InteropServices.Marshal.SizeOf(si);
				si.lpDesktop = @"winsta0\default";
				if(!WinApi.Advapi32.CreateProcessAsUser(hProcessTokenDupplicate, strApplication, strArgument, ref sa, ref sa, false, 0x00000400, lpEnvironmentBlock, System.IO.Path.GetDirectoryName(strApplication), ref si, out piProcess)) {
					WinApi.Kernel32.CloseHandle(hProcessTokenDupplicate);
					if(lpEnvironmentBlock != System.IntPtr.Zero) {
						WinApi.Userenv.DestroyEnvironmentBlock(lpEnvironmentBlock);
					}
					throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
				}

				WinApi.Kernel32.CloseHandle(hProcessTokenDupplicate);
				if(lpEnvironmentBlock != System.IntPtr.Zero) {
					WinApi.Userenv.DestroyEnvironmentBlock(lpEnvironmentBlock);
				}

				return piProcess.dwProcessId;
			}
		}
		private void ThreadServer() {
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			try {
				switch(this.stType) {
					case ServiceType.Server:

						System.Net.Sockets.Socket socServer = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
						socServer.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, this.iPort));
						socServer.Listen(10);
						System.Threading.AutoResetEvent areServerAcceptAsyncCompleted = new System.Threading.AutoResetEvent(false);
						void ServerAcceptAsyncCompleted(object o, System.Net.Sockets.SocketAsyncEventArgs e) {
							areServerAcceptAsyncCompleted.Set();
						};
						System.Net.Sockets.SocketAsyncEventArgs eaServerAcceptAsync = new System.Net.Sockets.SocketAsyncEventArgs();
						eaServerAcceptAsync.Completed += ServerAcceptAsyncCompleted;
						if(!socServer.AcceptAsync(eaServerAcceptAsync)) {
							ServerAcceptAsyncCompleted(socServer, eaServerAcceptAsync);
						}
						System.Net.Sockets.Socket socClientToSend = null;

						System.IO.Pipes.NamedPipeServerStream psServer = new System.IO.Pipes.NamedPipeServerStream("{2DCF2389-4969-483D-AA13-58FD8DDDD2D5}", System.IO.Pipes.PipeDirection.Out, 1, System.IO.Pipes.PipeTransmissionMode.Message, System.IO.Pipes.PipeOptions.Asynchronous, 4096, 4096);

						System.Diagnostics.Process pClient = null;
						System.Threading.AutoResetEvent areWaitForExitExited = new System.Threading.AutoResetEvent(false);
						void ClientWaitForExitExited(object o, System.EventArgs e) {
							areWaitForExitExited.Set();
						};

						System.Threading.WaitHandle[] tabEventServer = new System.Threading.WaitHandle[] {
							Service.mreStop,
							Service.areSessionChanged,
							Service.ewhClientConnecting,
							areServerAcceptAsyncCompleted,
							areWaitForExitExited
						};
						do {
							switch(System.Threading.WaitHandle.WaitAny(tabEventServer, -1)) {
								case 0:
									break;
								case 1:
									if(psServer.IsConnected && Service.ewhClientStarted.WaitOne(System.TimeSpan.Zero) && !pClient.HasExited) {
										Service.ewhSessionChanged.Set();
									} else if(socClientToSend != null) {
										if(!psServer.IsConnected || pClient.HasExited) {
											areWaitForExitExited.Reset();
											OpenUserInteractiveProcess(System.Reflection.Assembly.GetExecutingAssembly().Location, "-c");
										}
									}
									break;
								case 2:
									if(pClient != null) {
										pClient.EnableRaisingEvents = false;
										pClient.Exited -= ClientWaitForExitExited;
										pClient.Close();
										pClient.Dispose();
									}
									if(psServer.IsConnected) {
										psServer.Disconnect();
									}
									psServer.WaitForConnection();
									pClient = psServer.GetClientProcess();
									pClient.Exited += ClientWaitForExitExited;
									pClient.EnableRaisingEvents = true;
									if(socClientToSend != null) {
										bf.Serialize(psServer, socClientToSend.DuplicateAndClose(pClient.Id));
										Service.ewhServerMessage.Set();
										socClientToSend.Dispose();
										socClientToSend = null;
									}
									break;
								case 3:
									if(socClientToSend != null) {
										socClientToSend.Close();
										socClientToSend.Dispose();
										socClientToSend = null;
									}
									if(psServer.IsConnected && Service.ewhClientStarted.WaitOne(System.TimeSpan.Zero) && !pClient.HasExited) {
										bf.Serialize(psServer, eaServerAcceptAsync.AcceptSocket.DuplicateAndClose(pClient.Id));
										Service.ewhServerMessage.Set();
										eaServerAcceptAsync.AcceptSocket.Dispose();
									} else {
										socClientToSend = eaServerAcceptAsync.AcceptSocket;
										if(!psServer.IsConnected || pClient.HasExited) {
											areWaitForExitExited.Reset();
											OpenUserInteractiveProcess(System.Reflection.Assembly.GetExecutingAssembly().Location, "-c");
										}
									}
									eaServerAcceptAsync.AcceptSocket = null;
									if(!socServer.AcceptAsync(eaServerAcceptAsync)) {
										ServerAcceptAsyncCompleted(socServer, eaServerAcceptAsync);
									}
									break;
								case 4:
									if(socClientToSend != null) {
										OpenUserInteractiveProcess(System.Reflection.Assembly.GetExecutingAssembly().Location, "-c");
									}
									break;
								default:
									throw new System.Exception("Unmanaged handle error");
							}
						} while(!Service.mreStop.WaitOne(System.TimeSpan.Zero));

						Service.ewhServerDisconnecting.Set();

						if(pClient != null) {
							pClient.EnableRaisingEvents = false;
							pClient.Exited -= ClientWaitForExitExited;
							pClient.Close();
							pClient.Dispose();
						}
						areWaitForExitExited.Close();
						areWaitForExitExited.Dispose();

						if(psServer.IsConnected) {
							psServer.Disconnect();
						}
						psServer.Close();
						psServer.Dispose();

						eaServerAcceptAsync.Completed -= ServerAcceptAsyncCompleted;
						eaServerAcceptAsync.Dispose();
						areServerAcceptAsyncCompleted.Close();
						areServerAcceptAsyncCompleted.Dispose();
						socServer.Close();
						socServer.Dispose();

						break;
					case ServiceType.Both:

						System.Net.Sockets.Socket socBoth = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
						socBoth.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, this.iPort));
						socBoth.Listen(10);
						System.Threading.AutoResetEvent areBothAcceptAsyncCompleted = new System.Threading.AutoResetEvent(false);
						void BothAcceptAsyncCompleted(object o, System.Net.Sockets.SocketAsyncEventArgs e) {
							areBothAcceptAsyncCompleted.Set();
						};
						System.Net.Sockets.SocketAsyncEventArgs eaBothAcceptAsync = new System.Net.Sockets.SocketAsyncEventArgs();
						eaBothAcceptAsync.Completed += BothAcceptAsyncCompleted;
						if(!socBoth.AcceptAsync(eaBothAcceptAsync)) {
							BothAcceptAsyncCompleted(socBoth, eaBothAcceptAsync);
						}

						System.Collections.Generic.List<System.Threading.Thread> liClientBoth = new System.Collections.Generic.List<System.Threading.Thread>();

						System.Threading.WaitHandle[] tabEventBoth = new System.Threading.WaitHandle[] {
							Service.mreStop,
							Service.ewhServerStarted,
							areBothAcceptAsyncCompleted
						};
						do {
							switch(System.Threading.WaitHandle.WaitAny(tabEventBoth, -1)) {
								case 0:
									break;
								case 1:
									System.Threading.Tasks.Task.Run(delegate () {
										this.ServiceStop();
										this.ServiceStart();
									});
									break;
								case 2:
									System.Net.Sockets.Socket socClient = eaBothAcceptAsync.AcceptSocket;

									System.Threading.Thread thrClient = new System.Threading.Thread(delegate () {
										this.ThreadClient(socClient);
									});
									thrClient.Start();
									liClientBoth.Add(thrClient);

									eaBothAcceptAsync.AcceptSocket = null;
									if(!socBoth.AcceptAsync(eaBothAcceptAsync)) {
										BothAcceptAsyncCompleted(socBoth, eaBothAcceptAsync);
									}
									break;
								default:
									throw new System.Exception("Unmanaged handle error");
							}
						} while(!Service.mreStop.WaitOne(System.TimeSpan.Zero));

						liClientBoth.RemoveAll(delegate (System.Threading.Thread thr) {
							thr.Join();
							return true;
						});

						eaBothAcceptAsync.Completed -= BothAcceptAsyncCompleted;
						eaBothAcceptAsync.Dispose();
						areBothAcceptAsyncCompleted.Close();
						areBothAcceptAsyncCompleted.Dispose();
						socBoth.Close();
						socBoth.Dispose();
						break;
					case ServiceType.Client:

						System.IO.Pipes.NamedPipeClientStream psClient = new System.IO.Pipes.NamedPipeClientStream(".", "{2DCF2389-4969-483D-AA13-58FD8DDDD2D5}", System.IO.Pipes.PipeDirection.In, System.IO.Pipes.PipeOptions.Asynchronous);
						Service.ewhClientConnecting.Set();
						psClient.Connect();
						System.Diagnostics.Process pServer = psClient.GetServerProcess();
						System.Collections.Generic.List<System.Threading.Thread> liClient = new System.Collections.Generic.List<System.Threading.Thread>();

						System.Threading.WaitHandle[] tabEventClient = new System.Threading.WaitHandle[] {
							Service.mreStop,
							Service.ewhSessionChanged,
							Service.ewhServerDisconnecting,
							Service.ewhServerMessage
						};
						do {
							switch(System.Threading.WaitHandle.WaitAny(tabEventClient, -1)) {
								case 0:
									break;
								case 1:
									System.Threading.Tasks.Task.Run(delegate () {
										this.ServiceStop();
										System.Windows.Forms.Application.Exit();
									});
									break;
								case 2:
									if(!(System.Array.IndexOf<string>(System.Environment.GetCommandLineArgs(), "-c") < 0) && System.Windows.Forms.Application.OpenForms.Count == 0) {
										System.Threading.Tasks.Task.Run(delegate () {
											this.ServiceStop();
											System.Windows.Forms.Application.Exit();
										});
									} else {
										System.Threading.Tasks.Task.Run(delegate () {
											this.ServiceStop();
											this.ServiceStart();
										});
									}
									break;
								case 3:
									switch(bf.Deserialize(psClient)) {
										case System.Net.Sockets.SocketInformation si:
											System.Net.Sockets.Socket socClient = new System.Net.Sockets.Socket(si);

											System.Threading.Thread thrClient = new System.Threading.Thread(delegate () {
												this.ThreadClient(socClient);
											});
											thrClient.Start();
											liClient.Add(thrClient);
											break;
										default:
											throw new System.Exception("Communication error");
									}
									break;
								default:
									throw new System.Exception("Unmanaged handle error");
							}
						} while(!Service.mreStop.WaitOne(System.TimeSpan.Zero));

						psClient.Close();
						psClient.Dispose();

						liClient.RemoveAll(delegate (System.Threading.Thread thr) {
							thr.Join();
							return true;
						});
						break;
				}
			} catch(System.Exception eException) {
				Service.Error(eException.ToString());
				System.Threading.Tasks.Task.Run(delegate () {
					this.ServiceStop();
					this.ServiceStart();
				});
			}
		}
		private void ThreadClient(System.Net.Sockets.Socket socClient) {
			try {
				Service.Log("Socket accepted [" + socClient.GetHashCode() + "]");
				byte[] tabData = new byte[4096];
				System.Threading.AutoResetEvent areClientReceiveAsyncCompleted = new System.Threading.AutoResetEvent(false);
				void ClientReceiveAsyncCompleted(object o, System.Net.Sockets.SocketAsyncEventArgs e) {
					areClientReceiveAsyncCompleted.Set();
				};
				System.Net.Sockets.SocketAsyncEventArgs eaClientReceiveAsync = new System.Net.Sockets.SocketAsyncEventArgs();
				eaClientReceiveAsync.SetBuffer(tabData, 0, tabData.Length);
				eaClientReceiveAsync.Completed += ClientReceiveAsyncCompleted;
				if(!socClient.ReceiveAsync(eaClientReceiveAsync)) {
					ClientReceiveAsyncCompleted(socClient, eaClientReceiveAsync);
				}

				System.Threading.ManualResetEvent mreClientStop = new System.Threading.ManualResetEvent(false);

				System.Timers.Timer tUserInput = new System.Timers.Timer {
					Interval = 10,
					AutoReset = true
				};
				tUserInput.Elapsed += delegate (object oSource, System.Timers.ElapsedEventArgs eElapsed) {
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
						Service.Log("Client user input activity on socket [" + socClient.GetHashCode() + "]");
					}
				};
				System.Timers.Timer tPongUserInput = new System.Timers.Timer {
					Interval = 5000,
					AutoReset = false
				};
				tPongUserInput.Elapsed += delegate (object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					socClient.Send(Service.tabClose);
					mreClientStop.Set();
					Service.Warn("Client timeout pong inactivity on socket [" + socClient.GetHashCode() + "]");
				};
				System.Timers.Timer tInactivity = new System.Timers.Timer {
					Interval = this.iTimeoutInactivity - 300000,
					AutoReset = false
				};
				tInactivity.Elapsed += delegate (object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					socClient.Send(Service.tabPingUserInput);
					tPongUserInput.Start();
					Service.Log("Client timeout inactivity on socket [" + socClient.GetHashCode() + "]");
				};

				System.Timers.Timer tVideoInput = new System.Timers.Timer {
					Interval = this.iTimeoutVideoInput - 300000,
					AutoReset = false
				};
				tVideoInput.Elapsed += delegate (object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					socClient.Send(Service.tabPingUserInput);
					tPongUserInput.Start();
					Service.Log("Client timeout video input on socket [" + socClient.GetHashCode() + "]");
				};

				System.Timers.Timer tPong = new System.Timers.Timer {
					Interval = 5000,
					AutoReset = false
				};
				tPong.Elapsed += delegate (object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					socClient.Send(Service.tabClose);
					mreClientStop.Set();
					Service.Warn("Client timeout pong on socket [" + socClient.GetHashCode() + "]");
				};
				System.Timers.Timer tPing = new System.Timers.Timer {
					Interval = 30000,
					AutoReset = true
				};
				tPing.Elapsed += delegate (object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					socClient.Send(Service.tabPing);
					tPong.Start();
				};

				void PowerSettingNotificationArrived(WinApi.PowerBroadcastSetting pbs) {
					if(pbs.PowerSetting == WinApi.User32.GUID_MONITOR_POWER_ON) {
						switch(pbs.Data) {
							case 0:
								if(this.bVideoInput) {
									tVideoInput.Start();
								}
								break;
							case 1:
								if(this.bVideoInput) {
									tVideoInput.Stop();
								}
								break;
						}
					}
				};
				MagicRemoteService.Application.PowerSettingNotificationArrived += PowerSettingNotificationArrived;

				System.Collections.Generic.Dictionary<ushort, WinApi.Input[]> dBindDown = new System.Collections.Generic.Dictionary<ushort, WinApi.Input[]>();
				System.Collections.Generic.Dictionary<ushort, WinApi.Input[]> dBindUp = new System.Collections.Generic.Dictionary<ushort, WinApi.Input[]>();
				System.Collections.Generic.Dictionary<ushort, byte[][]> dBindActionDown = new System.Collections.Generic.Dictionary<ushort, byte[][]>();
				System.Collections.Generic.Dictionary<ushort, string[]> dBindCommandDown = new System.Collections.Generic.Dictionary<ushort, string[]>();
				foreach(System.Collections.Generic.KeyValuePair<ushort, Bind[]> kvp in this.dBind) {
					if(kvp.Value != null) {
						System.Collections.Generic.List<WinApi.Input> liBindDown = new System.Collections.Generic.List<WinApi.Input>();
						System.Collections.Generic.List<WinApi.Input> liBindUp = new System.Collections.Generic.List<WinApi.Input>();
						System.Collections.Generic.List<byte[]> liBindActionDown = new System.Collections.Generic.List<byte[]>();
						System.Collections.Generic.List<string> liBindCommandDown = new System.Collections.Generic.List<string>();
						foreach(Bind b in kvp.Value) {
							switch(b) {
								case MagicRemoteService.BindMouse bm:
									switch(bm.bmvValue) {
										case BindMouseValue.Left:
											liBindDown.Add(new WinApi.Input {
												type = WinApi.InputType.INPUT_MOUSE,
												u = new WinApi.InputDummyUnionName {
													mi = new WinApi.MouseInput {
														dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_LEFTDOWN,
														dwExtraInfo = System.IntPtr.Zero
													}
												}
											});
											liBindUp.Add(new WinApi.Input {
												type = WinApi.InputType.INPUT_MOUSE,
												u = new WinApi.InputDummyUnionName {
													mi = new WinApi.MouseInput {
														dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_LEFTUP,
														dwExtraInfo = System.IntPtr.Zero
													}
												}
											});
											break;
										case BindMouseValue.Right:
											liBindDown.Add(new WinApi.Input {
												type = WinApi.InputType.INPUT_MOUSE,
												u = new WinApi.InputDummyUnionName {
													mi = new WinApi.MouseInput {
														dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_RIGHTDOWN,
														dwExtraInfo = System.IntPtr.Zero
													}
												}
											});
											liBindUp.Add(new WinApi.Input {
												type = WinApi.InputType.INPUT_MOUSE,
												u = new WinApi.InputDummyUnionName {
													mi = new WinApi.MouseInput {
														dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_RIGHTUP,
														dwExtraInfo = System.IntPtr.Zero
													}
												}
											});
											break;
										case BindMouseValue.Middle:
											liBindDown.Add(new WinApi.Input {
												type = WinApi.InputType.INPUT_MOUSE,
												u = new WinApi.InputDummyUnionName {
													mi = new WinApi.MouseInput {
														dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_MIDDLEDOWN,
														dwExtraInfo = System.IntPtr.Zero
													}
												}
											});
											liBindUp.Add(new WinApi.Input {
												type = WinApi.InputType.INPUT_MOUSE,
												u = new WinApi.InputDummyUnionName {
													mi = new WinApi.MouseInput {
														dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_MIDDLEUP,
														dwExtraInfo = System.IntPtr.Zero
													}
												}
											});
											break;
									}
									break;
								case MagicRemoteService.BindKeyboard bk:
									liBindDown.Add(new WinApi.Input {
										type = WinApi.InputType.INPUT_KEYBOARD,
										u = new WinApi.InputDummyUnionName {
											ki = new WinApi.KeybdInput {
												wVk = bk.ucVirtualKey,
												wScan = bk.ucScanCode,
												dwFlags = bk.bExtended ? (WinApi.KeybdInputFlags.KEYEVENTF_EXTENDEDKEY | WinApi.KeybdInputFlags.KEYEVENTF_KEYDOWN) : WinApi.KeybdInputFlags.KEYEVENTF_KEYDOWN,
												dwExtraInfo = System.IntPtr.Zero
											}
										}
									});
									liBindUp.Add(new WinApi.Input {
										type = WinApi.InputType.INPUT_KEYBOARD,
										u = new WinApi.InputDummyUnionName {
											ki = new WinApi.KeybdInput {
												wVk = bk.ucVirtualKey,
												wScan = bk.ucScanCode,
												dwFlags = bk.bExtended ? (WinApi.KeybdInputFlags.KEYEVENTF_EXTENDEDKEY | WinApi.KeybdInputFlags.KEYEVENTF_KEYUP) : WinApi.KeybdInputFlags.KEYEVENTF_KEYUP,
												dwExtraInfo = System.IntPtr.Zero
											}
										}
									});
									break;
								case MagicRemoteService.BindAction ba:
									liBindActionDown.Add(new byte[] {
										(0b1000 << 4) | (0x2 << 0),
										0x01,
										(byte)ba.bavValue
									});
									break;
								case MagicRemoteService.BindCommand bc:
									liBindCommandDown.Add(bc.strCommand);
									break;
							}
						}
						if(liBindDown.Count > 0) {
							dBindDown.Add(kvp.Key, liBindDown.ToArray());
						}
						if(liBindUp.Count > 0) {
							dBindUp.Add(kvp.Key, liBindUp.ToArray());
						}
						if(liBindActionDown.Count > 0) {
							dBindActionDown.Add(kvp.Key, liBindActionDown.ToArray());
						}
						if(liBindCommandDown.Count > 0) {
							dBindCommandDown.Add(kvp.Key, liBindCommandDown.ToArray());
						}
					}
				};

				WinApi.Input[] piPosition = new WinApi.Input[] {
					new WinApi.Input {
						type = WinApi.InputType.INPUT_MOUSE,
						u = new WinApi.InputDummyUnionName {
							mi = new WinApi.MouseInput {
								dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_MOVE,
								dwExtraInfo = System.IntPtr.Zero
							}
						}
					}
				};
				WinApi.Input[] piWheel = new WinApi.Input[] {
					new WinApi.Input {
						type = WinApi.InputType.INPUT_MOUSE,
						u = new WinApi.InputDummyUnionName {
							mi = new WinApi.MouseInput {
								dwFlags = WinApi.MouseInputFlags.MOUSEEVENTF_WHEEL,
								dwExtraInfo = System.IntPtr.Zero
							}
						}
					}
				};
				WinApi.Input[] piUnicode = new WinApi.Input[] {
					new WinApi.Input {
						type = WinApi.InputType.INPUT_KEYBOARD,
						u = new WinApi.InputDummyUnionName {
							ki = new WinApi.KeybdInput {
								dwFlags = WinApi.KeybdInputFlags.KEYEVENTF_UNICODE | WinApi.KeybdInputFlags.KEYEVENTF_KEYDOWN,
								dwExtraInfo = System.IntPtr.Zero
							}
						}
					}, new WinApi.Input {
						type = WinApi.InputType.INPUT_KEYBOARD,
						u = new WinApi.InputDummyUnionName {
							ki = new WinApi.KeybdInput {
								dwFlags = WinApi.KeybdInputFlags.KEYEVENTF_UNICODE | WinApi.KeybdInputFlags.KEYEVENTF_KEYUP,
								dwExtraInfo = System.IntPtr.Zero
							}
						}
					}
				};

				//Find a better way for this
				uint uiDisplay = 0;
				System.Threading.Tasks.Task.Run(delegate () {
					System.Net.IPAddress iaClient = ((System.Net.IPEndPoint)socClient.RemoteEndPoint).Address;
					MagicRemoteService.WebOSCLIDevice wocdClient = System.Array.Find<MagicRemoteService.WebOSCLIDevice>(MagicRemoteService.WebOSCLI.SetupDeviceList(), delegate (MagicRemoteService.WebOSCLIDevice wocd) {
						return wocd.DeviceInfo.IP.Equals(iaClient);
					});
					if(wocdClient == null) {
						uiDisplay = 0;
					} else {
						Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey(@"Software\MagicRemoteService\Device\" + wocdClient.Name);
						if(rkMagicRemoteServiceDevice == null) {
							uiDisplay = 0;
						} else {
							uiDisplay = (uint)(int)rkMagicRemoteServiceDevice.GetValue("Display", 0);
						}
					}
				});

				MagicRemoteService.Screen scr;
				WinApi.Input[] arrInput;
				byte[][] arr2Byte;
				string[] arrString;
				WinApi.PointL pMouse = new WinApi.PointL();

				System.Threading.WaitHandle[] tabEvent = new System.Threading.WaitHandle[] {
					Service.mreStop,
					mreClientStop,
					areClientReceiveAsyncCompleted
				};
				switch(System.Threading.WaitHandle.WaitAny(tabEvent, -1, true)) {
					case 0:
						break;
					case 1:
						break;
					case 2:
						ulong ulLenMessage = (ulong)eaClientReceiveAsync.BytesTransferred;
						if(tabData[0] == 'G' && tabData[1] == 'E' && tabData[2] == 'T') {
							socClient.Send(System.Text.Encoding.UTF8.GetBytes(
								"HTTP/1.1 101 Switching Protocols\r\n" +
								"Connection: Upgrade\r\n" +
								"Upgrade: websocket\r\n" +
								"Sec-WebSocket-Accept: " + System.Convert.ToBase64String(System.Security.Cryptography.SHA1.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(System.Text.RegularExpressions.Regex.Match(System.Text.Encoding.UTF8.GetString(tabData), "Sec-WebSocket-Key: (.*)\r\n").Groups[1].Value + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"))) + "\r\n\r\n"));

							//TODO Something to ask TV if cursor visible
							Service.Log("Client connected on socket [" + socClient.GetHashCode() + "]");
							tPing.Start();
							if(this.bInactivity) {
								tInactivity.Start();
							}
						} else {
							mreClientStop.Set();
							Service.Warn("Connexion refused on socket [" + socClient.GetHashCode() + "]");
						}

						if(!socClient.ReceiveAsync(eaClientReceiveAsync)) {
							ClientReceiveAsyncCompleted(socClient, eaClientReceiveAsync);
						}
						break;
					default:
						throw new System.Exception("Unmanaged handle error");
				}
				while(!Service.mreStop.WaitOne(System.TimeSpan.Zero) && !mreClientStop.WaitOne(System.TimeSpan.Zero)) {
					switch(System.Threading.WaitHandle.WaitAny(tabEvent, -1, true)) {
						case 0:
							socClient.Send(Service.tabClose);
							break;
						case 1:
							break;
						case 2:
							ulong ulLenMessage = (ulong)eaClientReceiveAsync.BytesTransferred;
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
								if((tabData[ulOffsetFrame + 1] & 0b01111111) == 0b01111111) {
									ulLenData = System.BitConverter.ToUInt64(new byte[] { tabData[ulOffsetFrame + 9], tabData[ulOffsetFrame + 8], tabData[ulOffsetFrame + 7], tabData[ulOffsetFrame + 6], tabData[ulOffsetFrame + 5], tabData[ulOffsetFrame + 4], tabData[ulOffsetFrame + 3], tabData[ulOffsetFrame + 2] }, 0);
									ulOffsetMask = ulOffsetFrame + 10;
								} else if((tabData[ulOffsetFrame + 1] & 0b01111111) == 0b01111110) {
									ulLenData = System.BitConverter.ToUInt16(new byte[] { tabData[ulOffsetFrame + 3], tabData[ulOffsetFrame + 2] }, 0);
									ulOffsetMask = ulOffsetFrame + 4;
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
									Service.Warn("Unable to process split frame on socket [" + socClient.GetHashCode() + "]");
								} else {
									switch(ucOpcode) {
										case (byte)MagicRemoteService.WebSocketOpCode.Continuation:
											Service.Warn("Unable to process split frame on socket [" + socClient.GetHashCode() + "]");
											break;
										case (byte)MagicRemoteService.WebSocketOpCode.Text:
											if(ulLenData != 0) {
												Service.Warn("Unprocessed text message [" + System.Text.Encoding.UTF8.GetString(tabData, (int)ulOffsetData, (int)ulLenData) + "]");
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
														WinApi.User32.GetCursorPos(out pMouse);
														if(uiDisplay != 0 && MagicRemoteService.Screen.AllScreen.TryGetValue(uiDisplay, out scr) && scr.Active && scr != MagicRemoteService.Screen.PrimaryScreen) {
															piPosition[0].u.mi.dx = scr.Bounds.X + ((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1) * scr.Bounds.Width) / 1920) - pMouse.x;
															piPosition[0].u.mi.dy = scr.Bounds.Y + ((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 3) * scr.Bounds.Height) / 1080) - pMouse.y;
														} else {
															piPosition[0].u.mi.dx = ((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1) * MagicRemoteService.Screen.PrimaryScreen.Bounds.Width) / 1920) - pMouse.x;
															piPosition[0].u.mi.dy = ((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 3) * MagicRemoteService.Screen.PrimaryScreen.Bounds.Height) / 1080) - pMouse.y;
														}
														Service.SendInputAdmin(piPosition);

														//if(uiDisplay != 0 && MagicRemoteService.Screen.AllScreen.TryGetValue(uiDisplay, out scr) && scr.Active && scr != MagicRemoteService.Screen.PrimaryScreen) {
														//	piPosition[0].u.mi.dx = ((scr.Bounds.X + ((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1) * scr.Bounds.Width) / 1920)) * 65536) / MagicRemoteService.Screen.PrimaryScreen.Bounds.Width;
														//	piPosition[0].u.mi.dy = ((scr.Bounds.Y + ((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 3) * scr.Bounds.Height) / 1080)) * 65536) / MagicRemoteService.Screen.PrimaryScreen.Bounds.Height;
														//} else {
														//	piPosition[0].u.mi.dx = (System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1) * 65536) / 1920;
														//	piPosition[0].u.mi.dy = (System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 3) * 65536) / 1080;
														//}
														//Service.SendInputAdmin(piPosition);
														break;
													case (byte)MagicRemoteService.MessageType.Wheel:
														piWheel[0].u.mi.mouseData = (uint)(-System.BitConverter.ToInt16(tabData, (int)ulOffsetData + 1) * 3);
														Service.SendInputAdmin(piWheel);
														Service.LogIfDebug("Processed binary message send/wheel [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "], sY: " + (-System.BitConverter.ToInt16(tabData, (int)ulOffsetData + 1)).ToString());
														break;
													case (byte)MagicRemoteService.MessageType.Visible:
														if(System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 1)) {
															MagicRemoteService.SystemCursor.HideSytemCursor();
															MagicRemoteService.SystemCursor.DisableMouseSpeedAccel();
														} else {
															MagicRemoteService.SystemCursor.ShowSytemCursor();
															MagicRemoteService.SystemCursor.EnableMouseSpeedAccel();
														}
														Service.LogIfDebug("Processed binary message send/visible [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "], bV: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 1).ToString());
														break;
													case (byte)MagicRemoteService.MessageType.Key:
														ushort usCode = System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1);
														if((tabData[ulOffsetData + 3] & 0x01) == 0x01) {
															if(dBindDown.TryGetValue(usCode, out arrInput)) {
																Service.SendInputAdmin(arrInput);
																Service.LogIfDebug("Processed binary message send/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															} else if(dBindActionDown.TryGetValue(usCode, out arr2Byte)) {
																foreach(byte[] arrByte in arr2Byte) {
																	socClient.Send(arrByte);
																}
																Service.LogIfDebug("Processed binary message send/key action [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															} else if(dBindCommandDown.TryGetValue(usCode, out arrString)) {
																foreach(string strCommand in arrString) {
																	System.Diagnostics.Process pCommand = new System.Diagnostics.Process();
																	pCommand.StartInfo.FileName = "cmd";
																	pCommand.StartInfo.Arguments = "/c " + strCommand;
																	pCommand.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
																	pCommand.Start();
																}
																Service.LogIfDebug("Processed binary message send/key command [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															} else {
																Service.LogIfDebug("Unprocessed binary message send/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															}
														} else {
															if(dBindUp.TryGetValue(usCode, out arrInput)) {
																Service.SendInputAdmin(arrInput);
																Service.LogIfDebug("Processed binary message send/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															} else {
																Service.LogIfDebug("Unprocessed binary message send/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															}
														}
														break;
													case (byte)MagicRemoteService.MessageType.Unicode:
														piUnicode[0].u.ki.wScan = System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1);
														piUnicode[1].u.ki.wScan = System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1);
														Service.SendInputAdmin(piUnicode);
														Service.LogIfDebug("Processed binary message send/unicode [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "], usC: " + System.Text.Encoding.UTF8.GetString(tabData, (int)ulOffsetData + 1, 2));
														break;
													case (byte)MagicRemoteService.MessageType.Shutdown:
														System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
														pProcess.StartInfo.FileName = "shutdown";
														pProcess.StartInfo.Arguments = "/s /t 0";
														pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
														pProcess.Start();
														Service.LogIfDebug("Processed binary message send/shutdown [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "]");
														break;
													default:
														Service.Warn("Uprocessed binary message [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "]");
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
											Service.Log("Client disconnected on socket [" + socClient.GetHashCode() + "]");
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
											Service.LogIfDebug("Ping received on socket [" + socClient.GetHashCode() + "]");
											break;
										case (byte)MagicRemoteService.WebSocketOpCode.Pong:
											if(ulLenData != 0) {
												switch(tabData[ulOffsetData + 0]) {
													case 0x01:
														tPongUserInput.Stop();
														System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
														pProcess.StartInfo.FileName = "shutdown";
														pProcess.StartInfo.Arguments = "/s /t 300";
														pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
														pProcess.Start();
														tUserInput.Start();
														Service.LogIfDebug("Pong incativity received on socket [" + socClient.GetHashCode() + "]");
														break;
													default:
														Service.Warn("Unprocessed pong message [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "]");
														break;
												}
											} else {
												tPong.Stop();
												Service.LogIfDebug("Pong received on socket [" + socClient.GetHashCode() + "]");
											}
											break;
										default:
											Service.Warn("Unprocessed message [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", string.Empty) + "], " + System.Text.Encoding.Default.GetString(tabData, (int)ulOffsetData, (int)ulLenData));
											break;
									}
								}
								ulOffsetFrame = ulOffsetData + ulLenData;
							}

							if(!socClient.ReceiveAsync(eaClientReceiveAsync)) {
								ClientReceiveAsyncCompleted(socClient, eaClientReceiveAsync);
							}
							break;
						default:
							throw new System.Exception("Unmanaged handle error");
					}
				}
				MagicRemoteService.SystemCursor.ShowSytemCursor();
				MagicRemoteService.SystemCursor.EnableMouseSpeedAccel();
				tPing.Stop();
				tPong.Stop();
				if(this.bInactivity) {
					tInactivity.Stop();
					tVideoInput.Stop();
					tPongUserInput.Stop();
				}

				eaClientReceiveAsync.Completed -= ClientReceiveAsyncCompleted;
				eaClientReceiveAsync.Dispose();
				areClientReceiveAsyncCompleted.Close();
				areClientReceiveAsyncCompleted.Dispose();
				socClient.Close();
				socClient.Dispose();
				Service.Log("Socket closed [" + socClient.GetHashCode() + "]");
			} catch(System.Exception eException) {
				MagicRemoteService.SystemCursor.ShowSytemCursor();
				MagicRemoteService.SystemCursor.EnableMouseSpeedAccel();
				Service.Error(eException.ToString());
			}
		}
	}
	public static class PipeExtension {
		public static System.Diagnostics.Process GetServerProcess(this System.IO.Pipes.NamedPipeClientStream psClient) {
			WinApi.Kernel32.GetNamedPipeServerProcessId(psClient.SafePipeHandle.DangerousGetHandle(), out uint uiProcessId);
			return System.Diagnostics.Process.GetProcessById((int)uiProcessId);
		}
		public static System.Diagnostics.Process GetClientProcess(this System.IO.Pipes.NamedPipeServerStream psServer) {
			WinApi.Kernel32.GetNamedPipeClientProcessId(psServer.SafePipeHandle.DangerousGetHandle(), out uint uiProcessId);
			return System.Diagnostics.Process.GetProcessById((int)uiProcessId);
		}
	}
}
