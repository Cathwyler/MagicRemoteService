
namespace MagicRemoteService {
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct PROCESS_INFORMATION {
		public System.IntPtr hProcess;
		public System.IntPtr hThread;
		public uint dwProcessId;
		public uint dwThreadId;
	}
	public enum ObjectSate : uint {
		WAIT_ABANDONED = 0x00000080,
		WAIT_OBJECT_0 = 0x00000000,
		WAIT_TIMEOUT = 0x00000102,
		WAIT_FAILED = 0xFFFFFFFF,
	}
	public enum WTS_CONNECTSTATE_CLASS {
		WTSActive,
		WTSConnected,
		WTSConnectQuery,
		WTSShadow,
		WTSDisconnected,
		WTSIdle,
		WTSListen,
		WTSReset,
		WTSDown,
		WTSInit
	}
	public enum WTSInfoClass {
		WTSInitialProgram,
		WTSApplicationName,
		WTSWorkingDirectory,
		WTSOEMId,
		WTSSessionId,
		WTSUserName,
		WTSWinStationName,
		WTSDomainName,
		WTSConnectState,
		WTSClientBuildNumber,
		WTSClientName,
		WTSClientDirectory,
		WTSClientProductId,
		WTSClientHardwareId,
		WTSClientAddress,
		WTSClientDisplay,
		WTSClientProtocolType,
		WTSIdleTime,
		WTSLogonTime,
		WTSIncomingBytes,
		WTSOutgoingBytes,
		WTSIncomingFrames,
		WTSOutgoingFrames,
		WTSClientInfo,
		WTSSessionInfo
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	struct WTS_SESSION_INFO {
		public uint SessionID;
		[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
		public string pWinStationName;
		public WTS_CONNECTSTATE_CLASS State;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct SECURITY_ATTRIBUTES {
		public int Length;
		public System.IntPtr lpSecurityDescriptor;
		public bool bInheritHandle;
	}
	public enum SECURITY_IMPERSONATION_LEVEL : int {
		SecurityAnonymous = 0,
		SecurityIdentification = 1,
		SecurityImpersonation = 2,
		SecurityDelegation = 3,
	}
	public enum TOKEN_TYPE : int {
		TokenPrimary = 1,
		TokenImpersonation = 2
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct STARTUPINFO {
		public int cb;
		public string lpReserved;
		public string lpDesktop;
		public string lpTitle;
		public uint dwX;
		public uint dwY;
		public uint dwXSize;
		public uint dwYSize;
		public uint dwXCountChars;
		public uint dwYCountChars;
		public uint dwFillAttribute;
		public uint dwFlags;
		public ushort wShowWindow;
		public short cbReserved2;
		public System.IntPtr lpReserved2;
		public System.IntPtr hStdInput;
		public System.IntPtr hStdOutput;
		public System.IntPtr hStdError;
	}
	[System.Flags]
	public enum AccessMaskStandard : uint {
		DELETE = 0x00010000,
		READ_CONTROL = 0x00020000,
		SYNCHRONIZE = 0x00100000,
		WRITE_DAC = 0x00040000,
		WRITE_OWNER = 0x00080000
	}
	[System.Flags]
	public enum AccessMaskWinsta : uint {
		WINSTA_ACCESSCLIPBOARD = 0x0004,
		WINSTA_ACCESSGLOBALATOMS = 0x0020,
		WINSTA_CREATEDESKTOP = 0x0008,
		WINSTA_ENUMDESKTOPS = 0x0001,
		WINSTA_ENUMERATE = 0x0100,
		WINSTA_EXITWINDOWS = 0x0040,
		WINSTA_READATTRIBUTES = 0x0002,
		WINSTA_READSCREEN = 0x0200,
		WINSTA_WRITEATTRIBUTES = 0x0010
	}
	[System.Flags]
	public enum AccessMaskToken : uint {
		TOKEN_ASSIGN_PRIMARY = 0x0001,
		TOKEN_DUPLICATE = 0x0002,
		TOKEN_IMPERSONATE = 0x0004,
		TOKEN_QUERY = 0x0008,
		TOKEN_QUERY_SOURCE = 0x0010,
		TOKEN_ADJUST_PRIVILEGES = 0x0020,
		TOKEN_ADJUST_GROUPS = 0x0040,
		TOKEN_ADJUST_DEFAULT = 0x0080,
		TOKEN_ADJUST_SESSIONID = 0x0100
	}
	[System.Flags]
	public enum AccessMaskDesktop : uint {
		DESKTOP_CREATEMENU = 0x0004,
		DESKTOP_CREATEWINDOW = 0x0002,
		DESKTOP_ENUMERATE = 0x0040,
		DESKTOP_HOOKCONTROL = 0x0008,
		DESKTOP_JOURNALPLAYBACK = 0x0020,
		DESKTOP_JOURNALRECORD = 0x0010,
		DESKTOP_READOBJECTS = 0x0001,
		DESKTOP_SWITCHDESKTOP = 0x0100,
		DESKTOP_WRITEOBJECTS = 0x0080
	}
	public enum InputType : uint {
		INPUT_MOUSE = 0,
		INPUT_KEYBOARD = 1,
		INPUT_HARDWARE = 2
	}
	[System.Flags]
	public enum KeybdInputFlags : uint {
		KEYEVENTF_KEYDOWN = 0x0000,
		KEYEVENTF_EXTENDEDKEY = 0x0001,
		KEYEVENTF_KEYUP = 0x0002,
		KEYEVENTF_UNICODE = 0x0004,
		KEYEVENTF_SCANCODE = 0x0008
	}
	[System.Flags]
	public enum MouseInputFlags : uint {
		MOUSEEVENTF_MOVE = 0x0001,
		MOUSEEVENTF_LEFTDOWN = 0x0002,
		MOUSEEVENTF_LEFTUP = 0x0004,
		MOUSEEVENTF_RIGHTDOWN = 0x0008,
		MOUSEEVENTF_RIGHTUP = 0x0010,
		MOUSEEVENTF_MIDDLEDOWN = 0x0020,
		MOUSEEVENTF_MIDDLEUP = 0x0040,
		MOUSEEVENTF_XDOWN = 0x0080,
		MOUSEEVENTF_XUP = 0x0100,
		MOUSEEVENTF_WHEEL = 0x0800,
		MOUSEEVENTF_HWHEEL = 0x01000,
		MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000,
		MOUSEEVENTF_VIRTUALDESK = 0x4000,
		MOUSEEVENTF_ABSOLUTE = 0x8000
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct MouseInput {
		public int dx;
		public int dy;
		public uint mouseData;
		public MouseInputFlags dwFlags;
		public uint time;
		public System.IntPtr dwExtraInfo;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct KeybdInput {
		public ushort wVk;
		public ushort wScan;
		public KeybdInputFlags dwFlags;
		public uint time;
		public System.IntPtr dwExtraInfo;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct HardwareInput {
		public uint uMsg;
		public ushort wParamL;
		public ushort wParamH;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
	public struct DummyUnionName {
		[System.Runtime.InteropServices.FieldOffset(0)] public MouseInput mi;
		[System.Runtime.InteropServices.FieldOffset(0)] public KeybdInput ki;
		[System.Runtime.InteropServices.FieldOffset(0)] public HardwareInput hi;
	}
	public struct Input {
		public InputType type;
		public DummyUnionName u;
	}
	[System.Flags]
	public enum MapTypeFlags : uint {
		MAPVK_VK_TO_VSC = 0,
		MAPVK_VSC_TO_VK = 1,
		MAPVK_VK_TO_CHAR = 2,
		MAPVK_VSC_TO_VK_EX = 3,
		MAPVK_VK_TO_VSC_EX = 04
	}
	public enum ServiceCurrentState : uint {
		SERVICE_STOPPED = 0x00000001,
		SERVICE_START_PENDING = 0x00000002,
		SERVICE_STOP_PENDING = 0x00000003,
		SERVICE_RUNNING = 0x00000004,
		SERVICE_CONTINUE_PENDING = 0x00000005,
		SERVICE_PAUSE_PENDING = 0x00000006,
		SERVICE_PAUSED = 0x00000007,
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct ServiceStatus {
		public uint dwServiceType;
		public ServiceCurrentState dwCurrentState;
		public uint dwControlsAccepted;
		public uint dwWin32ExitCode;
		public uint dwServiceSpecificExitCode;
		public uint dwCheckPoint;
		public uint dwWaitHint;
	};
	public enum ServiceType {
		Server,
		Client,
		Both
	}
	public partial class Service : System.ServiceProcess.ServiceBase {
		private static readonly System.IntPtr WTS_CURRENT_SERVER_HANDLE = System.IntPtr.Zero;
		
		public const uint STANDARD_RIGHTS_ALL = (uint)(AccessMaskStandard.DELETE | AccessMaskStandard.READ_CONTROL | AccessMaskStandard.WRITE_DAC | AccessMaskStandard.WRITE_OWNER | AccessMaskStandard.SYNCHRONIZE);
		public const uint STANDARD_RIGHTS_READ = (uint)(AccessMaskStandard.READ_CONTROL);
		public const uint STANDARD_RIGHTS_WRITE = (uint)(AccessMaskStandard.READ_CONTROL);
		public const uint STANDARD_RIGHTS_EXECUTE = (uint)(AccessMaskStandard.READ_CONTROL);
		public const uint STANDARD_RIGHTS_REQUIRED = (uint)(AccessMaskStandard.DELETE | AccessMaskStandard.READ_CONTROL | AccessMaskStandard.WRITE_DAC | AccessMaskStandard.WRITE_OWNER);

		public const uint WINSTA_GENERIC_READ = STANDARD_RIGHTS_READ | (uint)(AccessMaskWinsta.WINSTA_ENUMDESKTOPS | AccessMaskWinsta.WINSTA_ENUMERATE | AccessMaskWinsta.WINSTA_READATTRIBUTES | AccessMaskWinsta.WINSTA_READSCREEN);
		public const uint WINSTA_GENERIC_WRITE = STANDARD_RIGHTS_WRITE | (uint)(AccessMaskWinsta.WINSTA_ACCESSCLIPBOARD | AccessMaskWinsta.WINSTA_CREATEDESKTOP | AccessMaskWinsta.WINSTA_WRITEATTRIBUTES);
		public const uint WINSTA_GENERIC_EXECUTE = STANDARD_RIGHTS_EXECUTE | (uint)(AccessMaskWinsta.WINSTA_ACCESSGLOBALATOMS | AccessMaskWinsta.WINSTA_EXITWINDOWS);
		public const uint WINSTA_GENERIC_ALL = STANDARD_RIGHTS_REQUIRED | (uint)(AccessMaskWinsta.WINSTA_ACCESSCLIPBOARD | AccessMaskWinsta.WINSTA_ACCESSGLOBALATOMS | AccessMaskWinsta.WINSTA_CREATEDESKTOP | AccessMaskWinsta.WINSTA_ENUMDESKTOPS | AccessMaskWinsta.WINSTA_ENUMERATE | AccessMaskWinsta.WINSTA_EXITWINDOWS | AccessMaskWinsta.WINSTA_READATTRIBUTES | AccessMaskWinsta.WINSTA_READSCREEN | AccessMaskWinsta.WINSTA_WRITEATTRIBUTES);

		public const uint DESKTOP_GENERIC_READ = STANDARD_RIGHTS_READ | (uint)(AccessMaskDesktop.DESKTOP_ENUMERATE | AccessMaskDesktop.DESKTOP_READOBJECTS);
		public const uint DESKTOP_GENERIC_WRITE = STANDARD_RIGHTS_WRITE | (uint)(AccessMaskDesktop.DESKTOP_CREATEMENU | AccessMaskDesktop.DESKTOP_CREATEWINDOW | AccessMaskDesktop.DESKTOP_HOOKCONTROL | AccessMaskDesktop.DESKTOP_JOURNALPLAYBACK | AccessMaskDesktop.DESKTOP_JOURNALRECORD | AccessMaskDesktop.DESKTOP_WRITEOBJECTS);
		public const uint DESKTOP_GENERIC_EXECUTE = STANDARD_RIGHTS_EXECUTE | (uint)(AccessMaskDesktop.DESKTOP_SWITCHDESKTOP);
		public const uint DESKTOP_GENERIC_ALL = STANDARD_RIGHTS_REQUIRED | (uint)(AccessMaskDesktop.DESKTOP_CREATEMENU | AccessMaskDesktop.DESKTOP_CREATEWINDOW | AccessMaskDesktop.DESKTOP_ENUMERATE | AccessMaskDesktop.DESKTOP_HOOKCONTROL | AccessMaskDesktop.DESKTOP_JOURNALPLAYBACK | AccessMaskDesktop.DESKTOP_JOURNALRECORD | AccessMaskDesktop.DESKTOP_READOBJECTS | AccessMaskDesktop.DESKTOP_SWITCHDESKTOP | AccessMaskDesktop.DESKTOP_WRITEOBJECTS);
		
		public const uint TOKEN_READ = STANDARD_RIGHTS_READ | (uint)(AccessMaskToken.TOKEN_QUERY);
		public const uint TOKEN_WRITE = STANDARD_RIGHTS_WRITE | (uint)(AccessMaskToken.TOKEN_ADJUST_PRIVILEGES | AccessMaskToken.TOKEN_ADJUST_GROUPS | AccessMaskToken.TOKEN_ADJUST_DEFAULT);
		public const uint TOKEN_EXECUTE = STANDARD_RIGHTS_EXECUTE;
		public const uint TOKEN_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | (uint)(AccessMaskToken.TOKEN_ADJUST_DEFAULT | AccessMaskToken.TOKEN_ADJUST_GROUPS | AccessMaskToken.TOKEN_ADJUST_PRIVILEGES | AccessMaskToken.TOKEN_ADJUST_SESSIONID | AccessMaskToken.TOKEN_ASSIGN_PRIMARY | AccessMaskToken.TOKEN_DUPLICATE | AccessMaskToken.TOKEN_IMPERSONATE | AccessMaskToken.TOKEN_QUERY | AccessMaskToken.TOKEN_QUERY_SOURCE);

		public const uint MAXIMUM_ALLOWED = 0x02000000;

		public const uint GENERIC_READ = 0x80000000;
		public const uint GENERIC_WRITE = 0x20000000;
		public const uint GENERIC_EXECUTE = 0x40000000;
		public const uint GENERIC_ALL = 0x10000000;

		private volatile int iPort;
		private volatile bool bInactivity;
		private volatile int iTimeoutInactivity;
		private System.Threading.Thread thrServeur;
		private System.Timers.Timer[] tabExtend;
		//private volatile bool bActive;
		private ServiceCurrentState scsState;
		private ServiceType stType;
		private static readonly uint SessionId = Service.WTSGetActiveConsoleSessionId();

		private static System.Threading.ManualResetEvent mreStop = new System.Threading.ManualResetEvent(true);
		private static System.Threading.Semaphore mServer;
		private static System.Threading.Semaphore mClient;
		private static System.Threading.EventWaitHandle ewhServiceStarted;
		private static System.Threading.EventWaitHandle ewhServiceStoped;
		private static System.Threading.EventWaitHandle ewhClientStarted;
		private static System.Threading.EventWaitHandle ewhClientStoped;
		private static System.Threading.EventWaitHandle ewhClientOnStart;
		private static System.Threading.EventWaitHandle ewhClientOnStop;

		public ServiceCurrentState State {
			get {
				return scsState;
			}
		}
		public Service() {
			this.InitializeComponent();
			if(!System.Diagnostics.EventLog.SourceExists(this.ServiceName)) {
				System.Diagnostics.EventLog.CreateEventSource(this.ServiceName, "Application");
			}
			this.elEventLog.Source = this.ServiceName;
			this.elEventLog.Log = "Application";
		}
		[System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
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
				if(!(System.Array.IndexOf<string>(System.Environment.GetCommandLineArgs(), "-c") < 0)) {
					this.stType = ServiceType.Client;
				} else  if(!Service.ewhServiceStoped.WaitOne(System.TimeSpan.Zero, true)) {
					this.stType = ServiceType.Client;
				} else {
					this.stType = ServiceType.Both;
				}
			}

			ServiceStatus ssServiceStatus = new ServiceStatus();
			switch(this.stType) {
				case ServiceType.Server:
				case ServiceType.Both:
					this.Log("Service start");

					this.scsState = ServiceCurrentState.SERVICE_START_PENDING;
					break;
				case ServiceType.Client:
					this.Log("Service client start");
					break;
			}
			switch(this.stType) {
				case ServiceType.Server:
					ssServiceStatus.dwCurrentState = ServiceCurrentState.SERVICE_START_PENDING;
					ssServiceStatus.dwWaitHint = 100000;
					bool truc = Service.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);
					break;
				case ServiceType.Both:
				case ServiceType.Client:
					break;
			}
			Microsoft.Win32.RegistryKey rkMagicRemoteService = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\MagicRemoteService");
			if(rkMagicRemoteService == null) {
				this.iPort = 41230;
				this.bInactivity = true;
				this.iTimeoutInactivity = 7200000;
			} else {
				this.iPort = (int)rkMagicRemoteService.GetValue("Port", 41230);
				this.bInactivity = (int)rkMagicRemoteService.GetValue("Inactivity", 1) != 0;
				this.iTimeoutInactivity = (int)rkMagicRemoteService.GetValue("TimeoutInactivity", 7200000);
			}
			switch(this.stType) {
				case ServiceType.Server:
				case ServiceType.Both:
					if(rkMagicRemoteService != null) {
						this.tabExtend = System.Array.ConvertAll(System.Array.FindAll(rkMagicRemoteService.GetSubKeyNames(), delegate (string str) {
							Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\MagicRemoteService\\" + str);
							return (int)rkMagicRemoteServiceDevice.GetValue("Extend", 0) != 0;
						}), new System.Converter<string, System.Timers.Timer>(delegate (string str) {

							Microsoft.Win32.RegistryKey rkMagicRemoteServiceDevice = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("Software\\MagicRemoteService\\" + str);

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
				case ServiceType.Both:
					this.Log("Service started");

					this.scsState = ServiceCurrentState.SERVICE_RUNNING;
					break;
				case ServiceType.Client:
					this.Log("Service client started");
					break;
			}
			switch(this.stType) {
				case ServiceType.Server:
					ssServiceStatus.dwCurrentState = ServiceCurrentState.SERVICE_RUNNING;
					Service.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);
					break;
				case ServiceType.Both:
				case ServiceType.Client:
					break;
			}
		}
		public void ServiceStop() {

			ServiceStatus ssServiceStatus = new ServiceStatus();
			switch(this.stType) {
				case ServiceType.Server:
				case ServiceType.Both:
					this.Log("Service stop");

					this.scsState = ServiceCurrentState.SERVICE_STOP_PENDING;
					break;
				case ServiceType.Client:
					this.Log("Service client stop");
					break;
			}
			switch(this.stType) {
				case ServiceType.Server:
					ssServiceStatus.dwCurrentState = ServiceCurrentState.SERVICE_STOP_PENDING;
					ssServiceStatus.dwWaitHint = 100000;
					Service.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);
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
				case ServiceType.Both:
					this.Log("Service stoped");

					this.scsState = ServiceCurrentState.SERVICE_STOPPED;
					break;
				case ServiceType.Client:
					this.Log("Service client stoped");
					break;
			}
			switch(this.stType) {
				case ServiceType.Server:
					ssServiceStatus.dwCurrentState = ServiceCurrentState.SERVICE_STOPPED;
					Service.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);
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
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		private static extern uint WTSGetActiveConsoleSessionId();
		[System.Runtime.InteropServices.DllImport("wtsapi32.dll", SetLastError = true)]
		private static extern bool WTSQuerySessionInformation(System.IntPtr hServer, uint sessionId, WTSInfoClass wtsInfoClass, out string ppBuffer, out uint pBytesReturned);
		[System.Runtime.InteropServices.DllImport("wtsapi32.dll", SetLastError = true)]
		private static extern int WTSEnumerateSessions(System.IntPtr hServer, int Reserved, int Version, ref System.IntPtr ppSessionInfo, ref int pCount);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern System.IntPtr GetProcessWindowStation();
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern System.IntPtr OpenWindowStation(string lpszWinSta, bool fInherit, uint dwDesiredAccess);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern bool CloseWindowStation(System.IntPtr hWinsta);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern bool SetProcessWindowStation(System.IntPtr hWinSta);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern System.IntPtr OpenInputDesktop(uint dwFlags, bool fInherit, uint dwDesiredAccess);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern bool CloseDesktop(System.IntPtr hDesktop);
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		private static extern uint GetCurrentThreadId();
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern System.IntPtr GetThreadDesktop(uint dwThreadId);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern bool SetThreadDesktop(System.IntPtr hDesktop);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern uint MapVirtualKeyA(uint uCode, MapTypeFlags uMapType);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern System.IntPtr GetMessageExtraInfo();
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		private static extern System.IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);
		[System.Runtime.InteropServices.DllImport("advapi32", SetLastError = true)]
		private static extern bool OpenProcessToken(System.IntPtr hProcessHandle, uint dwDesiredAccess, out System.IntPtr hTokenHandle);
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(System.IntPtr hObject);
		[System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool DuplicateTokenEx(System.IntPtr hExistingToken, uint dwDesiredAccess, ref SECURITY_ATTRIBUTES lpTokenAttributes, SECURITY_IMPERSONATION_LEVEL ImpersonationLevel, TOKEN_TYPE TokenType, out System.IntPtr phNewToken);
		[System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool CreateProcessAsUser(System.IntPtr hToken, string lpApplicationName, string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes, ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, System.IntPtr lpEnvironment, string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		private static extern uint WaitForSingleObject(System.IntPtr hHandle, uint dwMilliseconds);
		[System.Runtime.InteropServices.DllImport("wtsapi32.dll", SetLastError = true)]
		private static extern bool WTSQueryUserToken(uint sessionId, out System.IntPtr hToken);
		[System.Runtime.InteropServices.DllImport("userenv.dll", SetLastError = true)]
		private static extern bool CreateEnvironmentBlock(out System.IntPtr lpEnvironment, System.IntPtr hToken, bool bInherit);
		[System.Runtime.InteropServices.DllImport("userenv.dll", SetLastError = true)]
		private static extern bool DestroyEnvironmentBlock(System.IntPtr lpEnvironment);
		
		private static uint SendInputAdmin(Input[] pInputs) {
			if(Service.SessionId == WTSGetActiveConsoleSessionId()) {
				uint uiInput = Service.SendInput((uint)pInputs.Length, pInputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
				if(0 == uiInput) {
					System.IntPtr hLastInputDesktop = GetThreadDesktop(GetCurrentThreadId());
					System.IntPtr hInputDesktop = OpenInputDesktop(0, true, 0x10000000);
					if(((int)hInputDesktop) == 0) {
					} else if(!SetThreadDesktop(hInputDesktop)) {
					} else {
						if(((int)hLastInputDesktop) == 0) {
						} else if(!CloseDesktop(hLastInputDesktop)) {
						} else {
						}
						uiInput = Service.SendInput((uint)pInputs.Length, pInputs, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
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
		private static void OpenUserInteractiveProcess(string strApplication, string strArgument, out PROCESS_INFORMATION piProcess) {

			uint uiSessionId = WTSGetActiveConsoleSessionId();
			if(uiSessionId == 0xFFFFFFFF) {
				throw new System.Exception("Unable to get active console session id");
			}

			uint uiWinlogonPid = (uint)System.Array.Find<System.Diagnostics.Process>(System.Diagnostics.Process.GetProcessesByName("winlogon"), delegate (System.Diagnostics.Process p) {
				return (uint)p.SessionId == uiSessionId;
			}).Id;
			if(uiWinlogonPid == 0) {
				throw new System.Exception("Unable to get winlogon pid");
			}

			System.IntPtr hProcess = OpenProcess(0x2000000, false, uiWinlogonPid);
			if(hProcess == System.IntPtr.Zero) {
				throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			}

			System.IntPtr hProcessToken;
			if(!OpenProcessToken(hProcess, 0x0002, out hProcessToken)) {
				CloseHandle(hProcess);
				throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			}

			System.IntPtr hProcessTokenDupplicate;
			SECURITY_ATTRIBUTES sa = new SECURITY_ATTRIBUTES();
			sa.Length = System.Runtime.InteropServices.Marshal.SizeOf(sa);
			if(!DuplicateTokenEx(hProcessToken, MAXIMUM_ALLOWED, ref sa, SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, TOKEN_TYPE.TokenPrimary, out hProcessTokenDupplicate)) {
				CloseHandle(hProcess);
				CloseHandle(hProcessToken);
				throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			}

			System.IntPtr hUserToken;
			if(!WTSQueryUserToken(uiSessionId, out hUserToken)) {
				CloseHandle(hProcess);
				CloseHandle(hProcessToken);
				CloseHandle(hProcessTokenDupplicate);
				throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			}

			System.IntPtr lpEnvironmentBlock;
			if(!CreateEnvironmentBlock(out lpEnvironmentBlock, hUserToken, false)) {
				CloseHandle(hProcess);
				CloseHandle(hProcessToken);
				CloseHandle(hProcessTokenDupplicate);
				CloseHandle(hUserToken);
				throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			}

			STARTUPINFO si = new STARTUPINFO();
			si.cb = System.Runtime.InteropServices.Marshal.SizeOf(si);
			si.lpDesktop = "winsta0\\default";
			if(!CreateProcessAsUser(hProcessTokenDupplicate, strApplication, strArgument, ref sa, ref sa, false, 0x00000400, lpEnvironmentBlock, System.IO.Path.GetDirectoryName(strApplication), ref si, out piProcess)) {
				CloseHandle(hProcess);
				CloseHandle(hProcessToken);
				CloseHandle(hProcessTokenDupplicate);
				CloseHandle(hUserToken);
				DestroyEnvironmentBlock(lpEnvironmentBlock);
				throw new System.Exception(new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message);
			}

			CloseHandle(hProcess);
			CloseHandle(hProcessToken);
			CloseHandle(hProcessTokenDupplicate);
			CloseHandle(hUserToken);
			DestroyEnvironmentBlock(lpEnvironmentBlock);
		}
		private static bool WaitProcess(PROCESS_INFORMATION piProcess, uint dwMilliseconds = 0xFFFFFFFF) {
			switch((ObjectSate)WaitForSingleObject(piProcess.hProcess, dwMilliseconds)) {
				case ObjectSate.WAIT_TIMEOUT:
					return false;
				case ObjectSate.WAIT_OBJECT_0:
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
						PROCESS_INFORMATION piProcess = new PROCESS_INFORMATION() { hProcess = System.IntPtr.Zero };

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

						if(piProcess.hProcess != System.IntPtr.Zero && !WaitProcess(piProcess, 0)) {
							WaitProcess(piProcess);
						}
						CloseHandle(piProcess.hProcess);
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
										if(!(System.Array.IndexOf<string>(System.Environment.GetCommandLineArgs(), "-c") < 0)) {
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
				System.Timers.Timer tInactivity = new System.Timers.Timer();
				tInactivity.Interval = this.iTimeoutInactivity;
				tInactivity.AutoReset = false;
				tInactivity.Elapsed += delegate (System.Object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
					pProcess.StartInfo.FileName = "shutdown";
					pProcess.StartInfo.Arguments = "/s /t 0";
					pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
					pProcess.Start();
				};
				if(this.bInactivity) {
					tInactivity.Start();
				}
				System.Func<ulong> fClient = delegate () {
					return (ulong)socClient.Receive(tabData);
				};
				System.Threading.Tasks.Task<ulong> tClient = System.Threading.Tasks.Task<ulong>.Run(fClient);

				System.Threading.WaitHandle[] tabEvent = new System.Threading.WaitHandle[] { Service.mreStop, mreClientStop, ((System.IAsyncResult)tClient).AsyncWaitHandle };
				bool bAlive = true;
				do {
					switch(System.Threading.WaitHandle.WaitAny(tabEvent, -1, true)) {
						case 0:
						case 1:
							bAlive = false;
							break;
						case 2:
							if(this.bInactivity) {
								tInactivity.Stop();
								tInactivity.Start();
							}
							ulong ulLenMessage = tClient.Result;
							if(tabData[0] == 'G' && tabData[1] == 'E' && tabData[2] == 'T') {
								socClient.Send(System.Text.Encoding.UTF8.GetBytes(
									"HTTP/1.1 101 Switching Protocols\r\n" +
									"Connection: Upgrade\r\n" +
									"Upgrade: websocket\r\n" +
									"Sec-WebSocket-Accept: " + System.Convert.ToBase64String(System.Security.Cryptography.SHA1.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(System.Text.RegularExpressions.Regex.Match(System.Text.Encoding.UTF8.GetString(tabData), "Sec-WebSocket-Key: (.*)\r\n").Groups[1].Value + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"))) + "\r\n\r\n"));

								//TODO Something to ask TV if cursor visible
								this.Log("Client connected on socket [" + socClient.GetHashCode() + "]");
							} else {
								ulong ulOffsetFrame = 0;
								while(!(ulOffsetFrame == ulLenMessage)) {
									bool bFin = 0 != (tabData[ulOffsetFrame] & 0b10000000);
									bool bRsv1 = 0 != (tabData[ulOffsetFrame] & 0b01000000);
									bool bRsv2 = 0 != (tabData[ulOffsetFrame] & 0b00100000);
									bool bRsv3 = 0 != (tabData[ulOffsetFrame] & 0b00010000);
									byte ucOpcode = (byte)(tabData[ulOffsetFrame] & 0b00001111);

									bool bMask = 0 != (tabData[ulOffsetFrame + 1] & 0b10000000);
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
									switch(ucOpcode) {
										case 0x1: //Text frame
											if(ulLenData != 0) {
												this.Warn("Unprocessed text message [" + System.Text.Encoding.UTF8.GetString(tabData, (int)ulOffsetData, (int)ulLenData) + "]");
											}
											break;
										case 0x2: //Binary frame
											if(ulLenData != 0) {
												Input[] piInput;
												switch(tabData[ulOffsetData + 0]) {
													case 0x00:
														piInput = new Input[]
														{
															new Input
															{
																type = InputType.INPUT_MOUSE,
																u = new DummyUnionName
																{
																	mi = new MouseInput
																	{
																		dx = (int)((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1) * 65535) / 1920),
																		dy = (int)((System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 3) * 65535) / 1080),
																		dwFlags = MouseInputFlags.MOUSEEVENTF_MOVE | MouseInputFlags.MOUSEEVENTF_ABSOLUTE,
																		dwExtraInfo = GetMessageExtraInfo()
																	}
																}
															}
														};
														Service.SendInputAdmin(piInput);
														//this.LogIfDebug("Processed binary message send/mouse/position [0x" + System.BitConverter.ToString(tabData, ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usX: " + System.BitConverter.ToUInt16(tabData, ulOffsetData + 1).ToString() + ", usY: " + System.BitConverter.ToUInt16(tabData, ulOffsetData + 3).ToString());
														break;
													case 0x01:
														switch(System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1)) {
															case 0x00:
																piInput = new Input[]
																{
																	new Input
																	{
																		type = InputType.INPUT_MOUSE,
																		u = new DummyUnionName
																		{
																			mi = new MouseInput
																			{
																				dwFlags = (tabData[ulOffsetData + 3] == 1) ? MouseInputFlags.MOUSEEVENTF_LEFTDOWN : MouseInputFlags.MOUSEEVENTF_LEFTUP,
																				dwExtraInfo = GetMessageExtraInfo()
																			}
																		}
																	}
																};
																Service.SendInputAdmin(piInput);
																this.LogIfDebug("Processed binary message send/mouse/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
																break;
															case 0x01:
																piInput = new Input[]
																{
																	new Input
																	{
																		type = InputType.INPUT_MOUSE,
																		u = new DummyUnionName
																		{
																			mi = new MouseInput
																			{
																				dwFlags = (tabData[ulOffsetData + 3] == 1) ? MouseInputFlags.MOUSEEVENTF_RIGHTDOWN : MouseInputFlags.MOUSEEVENTF_RIGHTUP,
																				dwExtraInfo = GetMessageExtraInfo()
																			}
																		}
																	}
																};
																Service.SendInputAdmin(piInput);
																this.LogIfDebug("Processed binary message send/mouse/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
																break;
															default:
																this.Warn("Unprocessed binary message send/mouse/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
																break;
														}
														break;
													case 0x02:
														piInput = new Input[]
														{
															new Input
															{
																type = InputType.INPUT_MOUSE,
																u = new DummyUnionName
																{
																	mi = new MouseInput
																	{
																		mouseData = (uint)(-System.BitConverter.ToInt16(tabData, (int)ulOffsetData + 1) * 3),
																		dwFlags = MouseInputFlags.MOUSEEVENTF_WHEEL,
																		dwExtraInfo = GetMessageExtraInfo()
																	}
																}
															}
														};
														Service.SendInputAdmin(piInput);
														this.LogIfDebug("Processed binary message send/mouse/wheel [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], sY: " + (-System.BitConverter.ToInt16(tabData, (int)ulOffsetData + 1)).ToString());
														break;
													case 0x03:
														if(System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 1)) {
															MagicRemoteService.SystemCursor.HideSytemCursor();
														} else {
															MagicRemoteService.SystemCursor.ShowSytemCursor();
														}
														this.LogIfDebug("Processed binary message send/mouse/Visible [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], bV: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 1).ToString());
														break;
													case 0x04:
														switch(System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1)) {
															case 0x08:
															case 0x1B:
																piInput = new Input[]
																{
																	new Input
																	{
																		type = InputType.INPUT_KEYBOARD,
																		u = new DummyUnionName
																		{
																			ki = new KeybdInput
																			{
																				wScan = (ushort)MapVirtualKeyA((uint)System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1), MapTypeFlags.MAPVK_VK_TO_VSC),
																				dwFlags = ((tabData[ulOffsetData + 3] == 1) ? KeybdInputFlags.KEYEVENTF_KEYDOWN : KeybdInputFlags.KEYEVENTF_KEYUP) | KeybdInputFlags.KEYEVENTF_SCANCODE,
																				dwExtraInfo = GetMessageExtraInfo()
																			}
																		}
																	}
																};
																Service.SendInputAdmin(piInput);
																this.LogIfDebug("Processed binary message send/keyboard/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString() + ", bS: " + (ushort)MapVirtualKeyA((uint)System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1), MapTypeFlags.MAPVK_VK_TO_VSC));
																break;
															case 0x0D:
															case 0x25:
															case 0x26:
															case 0x27:
															case 0x28:
															case 0x5B:
																piInput = new Input[]
																{
																	new Input
																	{
																		type = InputType.INPUT_KEYBOARD,
																		u = new DummyUnionName
																		{
																			ki = new KeybdInput
																			{
																				wScan = (ushort)MapVirtualKeyA((uint)System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1), MapTypeFlags.MAPVK_VK_TO_VSC),
																				dwFlags = ((tabData[ulOffsetData + 3] == 1) ? KeybdInputFlags.KEYEVENTF_KEYDOWN : KeybdInputFlags.KEYEVENTF_KEYUP) | KeybdInputFlags.KEYEVENTF_EXTENDEDKEY | KeybdInputFlags.KEYEVENTF_SCANCODE,
																				dwExtraInfo = GetMessageExtraInfo()
																			}
																		}
																	}
																};
																Service.SendInputAdmin(piInput);
																this.LogIfDebug("Processed binary message send/keyboard/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString() + ", bS: " + (ushort)MapVirtualKeyA((uint)System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1), MapTypeFlags.MAPVK_VK_TO_VSC));
																break;
																//piInput = new Input[]
																//{
																//	new Input
																//	{
																//		type = InputType.INPUT_KEYBOARD,
																//		u = new DummyUnionName
																//		{
																//			ki = new KeybdInput
																//			{
																//				wVk = System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1),
																//				dwFlags = (tabData[ulOffsetData + 3] == 1) ? KeybdInputFlags.KEYEVENTF_KEYDOWN : KeybdInputFlags.KEYEVENTF_KEYUP,
																//				dwExtraInfo = GetMessageExtraInfo()
																//			}
																//		}
																//	}
																//};
																//MagicRemoteService.SendInput((uint)piInput.Length, piInput, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
																//this.LogIfDebug("Processed binary message send/keyboard/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
																//break;
															default:
																this.Warn("Unprocessed binary message send/keyboard/key [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1).ToString() + ", bS: " + System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 3).ToString());
																break;
														}
														break;
													case 0x05:
														piInput = new Input[]
														{
														new Input
														{
															type = InputType.INPUT_KEYBOARD,
															u = new DummyUnionName
															{
																ki = new KeybdInput
																{
																	wScan = System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1),
																	dwFlags = KeybdInputFlags.KEYEVENTF_UNICODE | KeybdInputFlags.KEYEVENTF_KEYDOWN,
																	dwExtraInfo = GetMessageExtraInfo()
																}
															}
														},
														new Input
														{
															type = InputType.INPUT_KEYBOARD,
															u = new DummyUnionName
															{
																ki = new KeybdInput
																{
																	wScan = System.BitConverter.ToUInt16(tabData, (int)ulOffsetData + 1),
																	dwFlags = KeybdInputFlags.KEYEVENTF_UNICODE | KeybdInputFlags.KEYEVENTF_KEYUP,
																	dwExtraInfo = GetMessageExtraInfo()
																}
															}
														}
														};
														Service.SendInputAdmin(piInput);
														this.LogIfDebug("Processed binary message send/keyboard/Unicode [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.Text.Encoding.UTF8.GetString(tabData, (int)ulOffsetData + 1, 2));
														break;
													case 0x06:
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
										case 0x8: //Connection close
											mreClientStop.Set();
											this.Log("Client disconnected on socket [" + socClient.GetHashCode() + "]");
											break;
										default:
											this.Warn("Unprocessed message [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], " + System.Text.Encoding.Default.GetString(tabData, (int)ulOffsetData, (int)ulLenData));
											break;
									}
									ulOffsetFrame = ulOffsetData + ulLenData;
								}
							}
							tClient = System.Threading.Tasks.Task<ulong>.Run(fClient);
							tabEvent[2] = ((System.IAsyncResult)tClient).AsyncWaitHandle;
							break;
					}
				} while(bAlive);
				SystemCursor.ShowSytemCursor();
				if(this.bInactivity) {
					tInactivity.Stop();
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
