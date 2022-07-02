
namespace MagicRemoteService {
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


	public partial class MagicRemoteService : System.ServiceProcess.ServiceBase {
		private static readonly System.Net.IPEndPoint ipepIPEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, XXXXX); //same as localStorage.sPort.

		private System.Threading.Thread thrServeur;
		private bool bActive;

		public MagicRemoteService() {
			InitializeComponent();
			if(!System.Diagnostics.EventLog.SourceExists(this.ServiceName)) {
				System.Diagnostics.EventLog.CreateEventSource(this.ServiceName, "Application");
			}
			this.elEventLog.Source = this.ServiceName;
			this.elEventLog.Log = "Application";

			System.Net.Sockets.Socket socServeur = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
			this.thrServeur = new System.Threading.Thread(delegate() {
				this.ThreadServeur(socServeur);
			});
		}

		[System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

		public void Run() {
			this.Log("Démarrage du service");

			ServiceStatus ssServiceStatus = new ServiceStatus();
			ssServiceStatus.dwCurrentState = ServiceCurrentState.SERVICE_START_PENDING;
			ssServiceStatus.dwWaitHint = 100000;
			MagicRemoteService.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);

			this.bActive = true;
			this.thrServeur.Start();

			ssServiceStatus.dwCurrentState = ServiceCurrentState.SERVICE_RUNNING;
			MagicRemoteService.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);
		}

		public void Exit() {
			this.Log("Arrêt du service");

			ServiceStatus ssServiceStatus = new ServiceStatus();
			ssServiceStatus.dwCurrentState = ServiceCurrentState.SERVICE_STOP_PENDING;
			ssServiceStatus.dwWaitHint = 100000;
			MagicRemoteService.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);

			this.bActive = false;
			this.thrServeur.Join();

			ssServiceStatus.dwCurrentState = ServiceCurrentState.SERVICE_STOPPED;
			MagicRemoteService.SetServiceStatus(this.ServiceHandle, ref ssServiceStatus);
		}

		protected override void OnStart(string[] args) {
			this.Run();
		}

		protected override void OnStop() {
			this.Exit();
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

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		private static extern uint MapVirtualKeyA(uint uCode, MapTypeFlags uMapType);

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern System.IntPtr GetMessageExtraInfo();

		private void ThreadServeur(System.Net.Sockets.Socket socServeur) {
			try {
				System.Collections.Generic.List<System.Threading.Thread> liClient = new System.Collections.Generic.List<System.Threading.Thread>();
				socServeur.Bind(MagicRemoteService.ipepIPEndPoint);
				socServeur.Listen(10);
				while(this.bActive) {
					if(!socServeur.Poll(10, System.Net.Sockets.SelectMode.SelectRead)) {
					} else {
						System.Net.Sockets.Socket socClient = socServeur.Accept();
						System.Threading.Thread thrClient = new System.Threading.Thread(delegate() {
							this.ThreadClient(socClient);
						});
						thrClient.Start();
						liClient.Add(thrClient);
					}
					liClient.RemoveAll(delegate(System.Threading.Thread thr) {
						return !thr.IsAlive;
					});
				}
				liClient.RemoveAll(delegate(System.Threading.Thread thr) {
					thr.Join();
					return true;
				});
				socServeur.Close();
			} catch(System.Exception eException) {
				this.Error(eException.ToString());
			}
		}


		private void ThreadClient(System.Net.Sockets.Socket socClient) {
			try {
				this.Log("Socket accepted [" + socClient.GetHashCode() + "]");
				byte[] tabData = new System.Byte[4096];
				bool bAlive = true;
				System.Timers.Timer tInactivity = new System.Timers.Timer();
				tInactivity.Interval = XXXXXXX; //ms, ex 7200000, if your TV is set to shutdown after 2, 4 or 6 hours
				tInactivity.AutoReset = false;
				tInactivity.Elapsed += delegate (System.Object oSource, System.Timers.ElapsedEventArgs eElapsed) {
					System.Diagnostics.Process.Start("shutdown", "/s /t 0");
				};
				tInactivity.Start();
				while(this.bActive && bAlive) {
					if(!socClient.Poll(10, System.Net.Sockets.SelectMode.SelectRead)) {
					} else {
						tInactivity.Stop();
						tInactivity.Start();
						ulong ulLenMessage = (ulong)socClient.Receive(tabData);
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
													MagicRemoteService.SendInput((uint)piInput.Length, piInput, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
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
															MagicRemoteService.SendInput((uint)piInput.Length, piInput, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
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
															MagicRemoteService.SendInput((uint)piInput.Length, piInput, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
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
													MagicRemoteService.SendInput((uint)piInput.Length, piInput, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
													this.LogIfDebug("Processed binary message send/mouse/wheel [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], sY: " + (-System.BitConverter.ToInt16(tabData, (int)ulOffsetData + 1)).ToString());
													break;
												case 0x03:
													if(System.BitConverter.ToBoolean(tabData, (int)ulOffsetData + 1)) {
														SystemCursor.HideSytemCursor();
													} else {
														SystemCursor.ShowSytemCursor();
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
															MagicRemoteService.SendInput((uint)piInput.Length, piInput, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
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
															MagicRemoteService.SendInput((uint)piInput.Length, piInput, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
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
													MagicRemoteService.SendInput((uint)piInput.Length, piInput, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Input)));
													this.LogIfDebug("Processed binary message send/keyboard/Unicode [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], usC: " + System.Text.Encoding.UTF8.GetString(tabData, (int)ulOffsetData + 1, 2));
													break;
												case 0x06:
													System.Diagnostics.Process.Start("shutdown", "/s /t 0");
													this.LogIfDebug("Processed binary message send/shutdown [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "]");
													break;

												default:
													this.Warn("Uprocessed binary message [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "]");
													break;
											}
										}
										break;
									case 0x8: //Connection close
										bAlive = false;
										this.Log("Client disconnected on socket [" + socClient.GetHashCode() + "]");
										break;
									default:
										this.Warn("Unprocessed message [0x" + System.BitConverter.ToString(tabData, (int)ulOffsetData, (int)ulLenData).Replace("-", System.String.Empty) + "], " + System.Text.Encoding.Default.GetString(tabData, (int)ulOffsetData, (int)ulLenData));
										break;
								}
								ulOffsetFrame = ulOffsetData + ulLenData;
							}
						}
					}
				}
				SystemCursor.ShowSytemCursor();
				tInactivity.Stop();
				socClient.Close();
				this.Log("Socket closed [" + socClient.GetHashCode() + "]");
			} catch(System.Exception eException) {
				SystemCursor.ShowSytemCursor();
				this.Error(eException.ToString());
			}
		}
	}
}
