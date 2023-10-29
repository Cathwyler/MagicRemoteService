
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
		private static System.Threading.AutoResetEvent areSession = new System.Threading.AutoResetEvent(false);
		private static System.Threading.EventWaitHandle ewhServiceStarted;
		private static System.Threading.EventWaitHandle ewhClientStarted;

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
			Service.ewhClientStarted = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset, "Global\\{9878BC83-46A0-412B-86B6-10F1C43FC0D9}", out _, Program.ewhsAll);

			if(!System.Environment.UserInteractive) {
				this.stType = ServiceType.Server;
			} else if(!Service.ewhServiceStarted.WaitOne(System.TimeSpan.Zero)) {
				this.stType = ServiceType.Both;
			} else {
				this.stType = ServiceType.Client;
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
				this.dKeyBind[0x019F] = null;                               //Play -> Play/Pause
				this.dKeyBind[0x0013] = null;                               //Pause -> Play/Pause
				this.dKeyBind[0x01A1] = null;                               //Fast-forward -> Scan Next Track
				this.dKeyBind[0x019C] = null;                               //Rewind -> Scan Previous Track
				this.dKeyBind[0x019D] = null;                               //Stop -> Stop
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
					break;
				case ServiceType.Both:
					break;
				case ServiceType.Client:
					Service.ewhClientStarted.Set();
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
					break;
				case ServiceType.Both:
					break;
				case ServiceType.Client:
					Service.ewhClientStarted.Reset();
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
					break;
				case ServiceType.Both:
				case ServiceType.Client:
					break;
			}
			Service.ewhServiceStarted.Close();
			Service.ewhServiceStarted.Dispose();
			Service.ewhClientStarted.Close();
			Service.ewhClientStarted.Dispose();
		}
		protected override void OnStart(string[] args) {
			this.ServiceStart();
		}
		protected override void OnStop() {
			this.ServiceStop();
		}
		protected override void OnSessionChange(System.ServiceProcess.SessionChangeDescription scd) {
			switch(scd.Reason) {
				case System.ServiceProcess.SessionChangeReason.SessionLock:
				case System.ServiceProcess.SessionChangeReason.SessionLogoff:
				case System.ServiceProcess.SessionChangeReason.ConsoleDisconnect:
				case System.ServiceProcess.SessionChangeReason.RemoteDisconnect:
					break;
				case System.ServiceProcess.SessionChangeReason.SessionUnlock:
				case System.ServiceProcess.SessionChangeReason.SessionLogon:
				case System.ServiceProcess.SessionChangeReason.ConsoleConnect:
				case System.ServiceProcess.SessionChangeReason.RemoteConnect:
					Service.areSession.Set();
					break;
				default:
					break;
			}
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
		private static System.Diagnostics.Process OpenUserInteractiveProcess(string strApplication, string strArgument) {

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
				throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			}

			System.IntPtr hProcessToken;
			if(!WinApi.Advapi32.OpenProcessToken(hProcess, 0x0002, out hProcessToken)) {
				WinApi.Kernel32.CloseHandle(hProcess);
				throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			}
			WinApi.Kernel32.CloseHandle(hProcess);

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
				if(!WinApi.Userenv.CreateEnvironmentBlock(out lpEnvironmentBlock, hUserToken, false)) {
					WinApi.Kernel32.CloseHandle(hUserToken);
					throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
				}
				WinApi.Kernel32.CloseHandle(hUserToken);
			}

			WinApi.StartupInfo si = new WinApi.StartupInfo();
			WinApi.ProcessInformation piProcess;
			si.cb = System.Runtime.InteropServices.Marshal.SizeOf(si);
			si.lpDesktop = "winsta0\\default";
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

			return System.Diagnostics.Process.GetProcessById((int)piProcess.dwProcessId);
		}
		private void ThreadServeur() {
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			try {
				switch(this.stType) {
					case ServiceType.Server:

						System.Net.Sockets.Socket socServer = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
						socServer.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, this.iPort));
						socServer.Listen(10);
						System.Threading.CancellationTokenSource ctsServerSocketAccept = new System.Threading.CancellationTokenSource();
						System.Threading.Tasks.Task<System.Net.Sockets.Socket> tServerSocketAccept = socServer.AcceptAsync(ctsServerSocketAccept.Token);
						
						System.IO.Pipes.NamedPipeServerStream psServer = new System.IO.Pipes.NamedPipeServerStream("{2DCF2389-4969-483D-AA13-58FD8DDDD2D5}", System.IO.Pipes.PipeDirection.Out, 1, System.IO.Pipes.PipeTransmissionMode.Message, System.IO.Pipes.PipeOptions.Asynchronous);
						
						System.Diagnostics.Process pClient = null;

						System.Threading.WaitHandle[] tabEventServer = new System.Threading.WaitHandle[] {
							Service.mreStop,
							Service.areSession,
							((System.IAsyncResult)tServerSocketAccept).AsyncWaitHandle
						};
						do {
							switch(System.Threading.WaitHandle.WaitAny(tabEventServer, -1)) {
								case 0:
									break;
								case 1:
									if(psServer.IsConnected) {
										psServer.Disconnect();
									}
									break;
								case 2:
									System.Net.Sockets.Socket socClient = tServerSocketAccept.Result;

									if(pClient != null && !pClient.HasExited) {
									} else {
										pClient = System.Array.Find<System.Diagnostics.Process>(System.Diagnostics.Process.GetProcessesByName("MagicRemoteService"), delegate (System.Diagnostics.Process p) {
											return p.Id != System.Diagnostics.Process.GetCurrentProcess().Id;
										});
									}
									if(pClient != null && !pClient.HasExited) {
										System.Threading.CancellationTokenSource ctsClientProcessExited = new System.Threading.CancellationTokenSource();
										System.Threading.Tasks.Task tClientProcessExited = pClient.WaitForExitAsync(ctsClientProcessExited.Token);

										System.Threading.WaitHandle.WaitAny(new System.Threading.WaitHandle[] {
											Service.mreStop,
											Service.ewhClientStarted,
											((System.IAsyncResult)tClientProcessExited).AsyncWaitHandle
										}, -1);

										ctsClientProcessExited.Cancel();
										try { tClientProcessExited.GetAwaiter().GetResult(); } catch(System.OperationCanceledException) { };
										ctsClientProcessExited.Dispose();
									}
									if(pClient != null && !pClient.HasExited) {
									} else {
										if(psServer.IsConnected) {
											psServer.Disconnect();
										}
										while(WinApi.Kernel32.WTSGetActiveConsoleSessionId() == 0xFFFFFFFF) {
											System.Threading.Thread.Sleep(10);
										}
										pClient = OpenUserInteractiveProcess(System.Reflection.Assembly.GetExecutingAssembly().Location, "-c");
									}
									if(!psServer.IsConnected) {
										System.Threading.CancellationTokenSource ctsServerPipeWaitForConnection = new System.Threading.CancellationTokenSource();
										System.Threading.Tasks.Task tServerPipeWaitForConnection = psServer.WaitForConnectionAsync(ctsServerPipeWaitForConnection.Token);
										
										System.Threading.CancellationTokenSource ctsClientProcessExited = new System.Threading.CancellationTokenSource();
										System.Threading.Tasks.Task tClientProcessExited = pClient.WaitForExitAsync(ctsClientProcessExited.Token);

										System.Threading.WaitHandle.WaitAny(new System.Threading.WaitHandle[] {
											Service.mreStop,
											((System.IAsyncResult)tServerPipeWaitForConnection).AsyncWaitHandle,
											((System.IAsyncResult)tClientProcessExited).AsyncWaitHandle,
										}, -1);

										ctsServerPipeWaitForConnection.Cancel();
										try { tServerPipeWaitForConnection.GetAwaiter().GetResult(); } catch(System.OperationCanceledException) { };
										ctsServerPipeWaitForConnection.Dispose();

										ctsClientProcessExited.Cancel();
										try { tClientProcessExited.GetAwaiter().GetResult(); } catch(System.OperationCanceledException) { };
										ctsClientProcessExited.Dispose();
									}
									if(!psServer.IsConnected) {
										socClient.Close();
										socClient.Dispose();
									} else {
										bf.Serialize(psServer, socClient.DuplicateAndClose(pClient.Id));
										socClient.Dispose();
									}

									tServerSocketAccept = socServer.AcceptAsync(ctsServerSocketAccept.Token);
									tabEventServer[2] = ((System.IAsyncResult)tServerSocketAccept).AsyncWaitHandle;
									break;
								default:
									throw new System.Exception("Unmanaged handle error");
							}
						} while(!mreStop.WaitOne(System.TimeSpan.Zero));

						if(psServer.IsConnected) {
							psServer.Disconnect();
						}
						psServer.Close();
						psServer.Dispose();

						ctsServerSocketAccept.Cancel();
						socServer.Close();
						try { tServerSocketAccept.GetAwaiter().GetResult(); } catch(System.OperationCanceledException) { };
						ctsServerSocketAccept.Dispose();
						socServer.Dispose();
						break;
					case ServiceType.Both:

						System.Net.Sockets.Socket socBoth = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
						socBoth.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, this.iPort));
						socBoth.Listen(10);
						System.Threading.CancellationTokenSource ctsBothSocketAccept = new System.Threading.CancellationTokenSource();
						System.Threading.Tasks.Task<System.Net.Sockets.Socket> tBothSocketAccept = socBoth.AcceptAsync(ctsBothSocketAccept.Token);

						System.Collections.Generic.List<System.Threading.Thread> liClientBoth = new System.Collections.Generic.List<System.Threading.Thread>();

						System.Threading.WaitHandle[] tabEventBoth = new System.Threading.WaitHandle[] {
							Service.mreStop,
							Service.ewhServiceStarted,
							((System.IAsyncResult)tBothSocketAccept).AsyncWaitHandle
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
									Service.mreStop.Set();
									break;
								case 2:
									System.Net.Sockets.Socket socClient = tBothSocketAccept.Result;

									System.Threading.Thread thrClient = new System.Threading.Thread(delegate () {
										this.ThreadClient(socClient);
									});
									thrClient.Start();
									liClientBoth.Add(thrClient);

									tBothSocketAccept = socBoth.AcceptAsync(ctsBothSocketAccept.Token);
									tabEventBoth[2] = ((System.IAsyncResult)tBothSocketAccept).AsyncWaitHandle;
									break;
								default:
									throw new System.Exception("Unmanaged handle error");
							}
						} while(!mreStop.WaitOne(System.TimeSpan.Zero));

						liClientBoth.RemoveAll(delegate (System.Threading.Thread thr) {
							thr.Join();
							return true;
						});

						ctsBothSocketAccept.Cancel();
						socBoth.Close();
						try { tBothSocketAccept.GetAwaiter().GetResult(); } catch(System.OperationCanceledException) { };
						ctsBothSocketAccept.Dispose();
						socBoth.Dispose();
						break;
					case ServiceType.Client:

						System.IO.Pipes.NamedPipeClientStream psClient = new System.IO.Pipes.NamedPipeClientStream(".", "{2DCF2389-4969-483D-AA13-58FD8DDDD2D5}", System.IO.Pipes.PipeDirection.In, System.IO.Pipes.PipeOptions.Asynchronous);
						psClient.Connect();
						byte[] tabData = new byte[4096];
						int iData = 0;
						System.Threading.CancellationTokenSource ctsClientPipeRead = new System.Threading.CancellationTokenSource();
						System.Threading.Tasks.Task<int> tClientPipeRead = psClient.ReadAsync(tabData, iData, tabData.Length - iData, ctsClientPipeRead.Token);

						System.Collections.Generic.List<System.Threading.Thread> liClient = new System.Collections.Generic.List<System.Threading.Thread>();

						System.Threading.WaitHandle[] tabEventClient = new System.Threading.WaitHandle[] {
							Service.mreStop,
							((System.IAsyncResult)tClientPipeRead).AsyncWaitHandle
						};
						do {
							switch(System.Threading.WaitHandle.WaitAny(tabEventClient, -1)) {
								case 0:
									break;
								case 1:
									int iRead = tClientPipeRead.Result;
									if(iRead == 0) {
										if(!(System.Array.IndexOf<string>(System.Environment.GetCommandLineArgs(), "-c") < 0) && System.Windows.Forms.Application.OpenForms.Count == 0) {
											System.Windows.Forms.Application.Exit();
										} else {
											;
											System.Threading.Tasks.Task.Run(delegate () {
												this.ServiceStop();
												this.ServiceStart();
											});
										}
									} else {
										iData += iRead;
										object ClientPipeDeserialize() {
											try {
												return bf.Deserialize(new System.IO.MemoryStream(tabData, 0, iData));
											} catch(System.Runtime.Serialization.SerializationException ex) {
												switch(ex.HResult & 0x0000FFFF) {
													case 5388:
														return null;
													default:
														throw;
												}
											}
										}
										switch(ClientPipeDeserialize()) {
											case null:
												break;
											case System.Net.Sockets.SocketInformation si:
												iData = 0;
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
									}
									tClientPipeRead = psClient.ReadAsync(tabData, iData, tabData.Length - iData, ctsClientPipeRead.Token);
									tabEventClient[1] = ((System.IAsyncResult)tClientPipeRead).AsyncWaitHandle;
									break;
								default:
									throw new System.Exception("Unmanaged handle error");
							}
						} while(!mreStop.WaitOne(System.TimeSpan.Zero));

						ctsClientPipeRead.Cancel();
						psClient.Close();
						try { tClientPipeRead.GetAwaiter().GetResult(); } catch(System.OperationCanceledException) { };
						ctsClientPipeRead.Dispose();
						psClient.Dispose();

						liClient.RemoveAll(delegate (System.Threading.Thread thr) {
							thr.Join();
							return true;
						});
						break;
				}
			} catch(System.Exception eException) {
				this.Error(eException.ToString());
				System.Threading.Tasks.Task.Run(delegate () {
					this.ServiceStop();
					this.ServiceStart();
				});
			}
		}
		private void ThreadClient(System.Net.Sockets.Socket socClient) {
			try {
				this.Log("Socket accepted [" + socClient.GetHashCode() + "]");
				byte[] tabData = new byte[4096];
				System.Threading.CancellationTokenSource ctsClientSocketReceive = new System.Threading.CancellationTokenSource();
				System.Threading.Tasks.Task<int> tClientSocketReceive = socClient.ReceiveAsync(tabData, ctsClientSocketReceive.Token);

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
											u = new WinApi.InputDummyUnionName {
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
											u = new WinApi.InputDummyUnionName {
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
											u = new WinApi.InputDummyUnionName {
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
											u = new WinApi.InputDummyUnionName {
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
											u = new WinApi.InputDummyUnionName {
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
											u = new WinApi.InputDummyUnionName {
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
										u = new WinApi.InputDummyUnionName {
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
										u = new WinApi.InputDummyUnionName {
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
										u = new WinApi.InputDummyUnionName {
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
										u = new WinApi.InputDummyUnionName {
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
						u = new WinApi.InputDummyUnionName {
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
						u = new WinApi.InputDummyUnionName {
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
						u = new WinApi.InputDummyUnionName {
							ki = new WinApi.KeybdInput {
								dwFlags = WinApi.KeybdInputFlags.KEYEVENTF_UNICODE | WinApi.KeybdInputFlags.KEYEVENTF_KEYDOWN,
								dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
							}
						}
					}, new WinApi.Input {
						type = WinApi.InputType.INPUT_KEYBOARD,
						u = new WinApi.InputDummyUnionName {
							ki = new WinApi.KeybdInput {
								dwFlags = WinApi.KeybdInputFlags.KEYEVENTF_UNICODE | WinApi.KeybdInputFlags.KEYEVENTF_KEYUP,
								dwExtraInfo = WinApi.User32.GetMessageExtraInfo()
							}
						}
					}
				};

				//Find a better way for this
				uint uiDisplay;
				MagicRemoteService.WebOSCLIDevice wocdClient = System.Array.Find<MagicRemoteService.WebOSCLIDevice>(MagicRemoteService.WebOSCLI.SetupDeviceList(), delegate (MagicRemoteService.WebOSCLIDevice wocd) {
					return wocd.DeviceInfo.IP.Equals(((System.Net.IPEndPoint)socClient.RemoteEndPoint).Address);
				});
				if(wocdClient == null) {
					uiDisplay = 0;
				} else {
					Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = (MagicRemoteService.Program.bElevated ? Microsoft.Win32.Registry.LocalMachine : Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software\\MagicRemoteService\\" + wocdClient.Name);
					if(rkMagicRemoteServiceDevice == null) {
						uiDisplay = 0;
					} else {
						uiDisplay = (uint)(int)rkMagicRemoteServiceDevice.GetValue("Display", 0);
					}
				}

				MagicRemoteService.Screen scr;
				WinApi.Input[] arrInput;
				byte[] arrByte;

				System.Threading.WaitHandle[] tabEvent = new System.Threading.WaitHandle[] {
					Service.mreStop,
					mreClientStop,
					((System.IAsyncResult)tClientSocketReceive).AsyncWaitHandle
				};
				switch(System.Threading.WaitHandle.WaitAny(tabEvent, -1, true)) {
					case 0:
					case 1:
						break;
					case 2:
						ulong ulLenMessage = (ulong)tClientSocketReceive.Result;
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
							mreClientStop.Set();
							this.Warn("Connexion refused on socket [" + socClient.GetHashCode() + "]");
						}
						tClientSocketReceive = socClient.ReceiveAsync(tabData, ctsClientSocketReceive.Token);
						tabEvent[2] = ((System.IAsyncResult)tClientSocketReceive).AsyncWaitHandle;

						break;
					default:
						throw new System.Exception("Unmanaged handle error");
				}
				while(!mreStop.WaitOne(System.TimeSpan.Zero) && !mreClientStop.WaitOne(System.TimeSpan.Zero)) {
					switch(System.Threading.WaitHandle.WaitAny(tabEvent, -1, true)) {
						case 0:
						case 1:
							socClient.Send(Service.tabClose);
							break;
						case 2:
							ulong ulLenMessage = (ulong)tClientSocketReceive.Result;
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
														if(uiDisplay != 0 && MagicRemoteService.Screen.AllScreen.TryGetValue(uiDisplay, out scr) && scr.Active && scr != MagicRemoteService.Screen.PrimaryScreen) {
															piPosition[0].u.mi.dx = ((scr.Bounds.X + ((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1) * scr.Bounds.Width) / 1920)) * 65536) / MagicRemoteService.Screen.PrimaryScreen.Bounds.Width;
															piPosition[0].u.mi.dy = ((scr.Bounds.Y + ((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 3) * scr.Bounds.Height) / 1080)) * 65536) / MagicRemoteService.Screen.PrimaryScreen.Bounds.Height;
														} else {
															piPosition[0].u.mi.dx = (System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1) * 65536) / 1920;
															piPosition[0].u.mi.dy = (System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 3) * 65536) / 1080;
														}
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
															if(dKeyBindDown.TryGetValue(usCode, out arrInput)) {
																Service.SendInputAdmin(arrInput);
																this.LogIfDebug("Processed binary message send/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															} else if(dKeyBindActionDown.TryGetValue(usCode, out arrByte)) {
																socClient.Send(arrByte);
																this.LogIfDebug("Processed binary message send/key action [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															} else {
																this.LogIfDebug("Unprocessed binary message send/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
															}
														} else {
															if(dKeyBindUp.TryGetValue(usCode, out arrInput)) {
																Service.SendInputAdmin(arrInput);
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
							tClientSocketReceive = socClient.ReceiveAsync(tabData, ctsClientSocketReceive.Token);
							tabEvent[2] = ((System.IAsyncResult)tClientSocketReceive).AsyncWaitHandle;
							break;
						default:
							throw new System.Exception("Unmanaged handle error");
					}
				}
				SystemCursor.ShowSytemCursor();
				tPing.Stop();
				tPong.Stop();
				if(this.bInactivity) {
					tInactivity.Stop();
					tPongInactivity.Stop();
				}
				ctsClientSocketReceive.Cancel();
				socClient.Close();
				try { tClientSocketReceive.GetAwaiter().GetResult(); } catch(System.OperationCanceledException) { };
				ctsClientSocketReceive.Dispose();
				socClient.Dispose();
				this.Log("Socket closed [" + socClient.GetHashCode() + "]");
			} catch(System.Exception eException) {
				SystemCursor.ShowSytemCursor();
				this.Error(eException.ToString());
			}
		}
	}
	public static class SocketExtension {
		public static System.Threading.Tasks.Task<System.Net.Sockets.Socket> AcceptAsync(this System.Net.Sockets.Socket s, System.Threading.CancellationToken ct) {
			return System.Threading.Tasks.Task<System.Net.Sockets.Socket>.Run(delegate () {
				System.Net.Sockets.Socket sAccept;

				System.Threading.ManualResetEvent mreAcceptAsyncCompleted = new System.Threading.ManualResetEvent(false);
				void AcceptAsyncCompleted(object o, System.Net.Sockets.SocketAsyncEventArgs e) {
					mreAcceptAsyncCompleted.Set();
				};

				System.Net.Sockets.SocketAsyncEventArgs eaAcceptAsync = new System.Net.Sockets.SocketAsyncEventArgs();
				eaAcceptAsync.Completed += AcceptAsyncCompleted;

				if(!s.AcceptAsync(eaAcceptAsync)) {
					AcceptAsyncCompleted(s, eaAcceptAsync);
				}

				switch(System.Threading.WaitHandle.WaitAny(new System.Threading.WaitHandle[] {
					ct.WaitHandle,
					mreAcceptAsyncCompleted
				}, -1)) {
					case 0:
						throw new System.OperationCanceledException(ct);
					case 1:
						sAccept = eaAcceptAsync.AcceptSocket;
						break;
					default:
						throw new System.Exception("Unmanaged handle error");

				}

				eaAcceptAsync.Completed -= AcceptAsyncCompleted;
				eaAcceptAsync.Dispose();

				mreAcceptAsyncCompleted.Close();
				mreAcceptAsyncCompleted.Dispose();

				return sAccept;
			});
		}
		public static System.Threading.Tasks.Task<int> ReceiveAsync(this System.Net.Sockets.Socket s, byte[] tabData, System.Threading.CancellationToken ct) {
			return System.Threading.Tasks.Task<System.Net.Sockets.Socket>.Run(delegate () {
				int iReceive;

				System.Threading.ManualResetEvent mreReceiveAsyncCompleted = new System.Threading.ManualResetEvent(false);
				void ReceiveAsyncCompleted(object o, System.Net.Sockets.SocketAsyncEventArgs e) {
					mreReceiveAsyncCompleted.Set();
				};

				System.Net.Sockets.SocketAsyncEventArgs eaReceiveAsync = new System.Net.Sockets.SocketAsyncEventArgs();
				eaReceiveAsync.SetBuffer(tabData, 0, tabData.Length);
				eaReceiveAsync.Completed += ReceiveAsyncCompleted;

				if(!s.ReceiveAsync(eaReceiveAsync)) {
					ReceiveAsyncCompleted(s, eaReceiveAsync);
				}

				switch(System.Threading.WaitHandle.WaitAny(new System.Threading.WaitHandle[] {
					ct.WaitHandle,
					mreReceiveAsyncCompleted
				}, -1)) {
					case 0:
						throw new System.OperationCanceledException(ct);
					case 1:
						iReceive = eaReceiveAsync.BytesTransferred;
						break;
					default:
						throw new System.Exception("Unmanaged handle error");

				}

				eaReceiveAsync.Completed -= ReceiveAsyncCompleted;
				eaReceiveAsync.Dispose();

				mreReceiveAsyncCompleted.Close();
				mreReceiveAsyncCompleted.Dispose();

				return iReceive;
			});
		}
	}
	public static class ProcessExtension {
		public static System.Threading.Tasks.Task WaitForExitAsync(this System.Diagnostics.Process p, System.Threading.CancellationToken ct) {
			return System.Threading.Tasks.Task<System.Net.Sockets.Socket>.Run(delegate () {
				System.Threading.ManualResetEvent mreWaitForExitExited = new System.Threading.ManualResetEvent(false);
				void WaitForExitExited(object sender, System.EventArgs e) {
					mreWaitForExitExited.Set();
				};

				p.Exited += WaitForExitExited;
				p.EnableRaisingEvents = true;

				switch(System.Threading.WaitHandle.WaitAny(new System.Threading.WaitHandle[] {
					ct.WaitHandle,
					mreWaitForExitExited
				}, -1)) {
					case 0:
						throw new System.OperationCanceledException(ct);
					case 1:
						break;
					default:
						throw new System.Exception("Unmanaged handle error");

				}

				p.EnableRaisingEvents = false;
				p.Exited -= WaitForExitExited;

				mreWaitForExitExited.Close();
				mreWaitForExitExited.Dispose();
			});
		}
	}
}