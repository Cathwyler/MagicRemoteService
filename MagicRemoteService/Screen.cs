
namespace MagicRemoteService {
	class Screen {
		private static readonly object oAllScreenLock = new object();
		private static Screen scrPrimaryScreen;
		public static Screen PrimaryScreen {
			get {
				lock(Screen.oAllScreenLock) {
					return Screen.scrPrimaryScreen;
				}
			}
		}
		private static System.Collections.Generic.Dictionary<uint, Screen> dAllScreen;
		public static System.Collections.Generic.Dictionary<uint, Screen> AllScreen {
			get {
				lock(Screen.oAllScreenLock) {
					return Screen.dAllScreen;
				}
			}
		}
		private static Screen scrPrimaryDefaut = new Screen() {
			ucNumber = 0,
			bActive = false,
			bPrimary = false,
			uiId = 0,
			strUserFriendlyName = MagicRemoteService.Properties.Resources.ScreenPrimaryDefaultUserFriendlyName,
			rBounds = new System.Drawing.Rectangle {
				X = 0,
				Y = 0,
				Width = 0,
				Height = 0
			},
		};
		public static Screen PrimaryDefaut {
			get {
				return Screen.scrPrimaryDefaut;
			}
		}
		private uint uiId;
		public uint Id {
			get {
				return this.uiId;
			}
		}
		private byte ucNumber;
		public byte Number {
			get {
				return this.ucNumber;
			}
		}
		private bool bActive;
		public bool Active {
			get {
				return this.bActive;
			}
		}
		private bool bPrimary;
		public bool Primary {
			get {
				return this.bPrimary;
			}
		}
		private string strUserFriendlyName;
		public string UserFriendlyName {
			get {
				return this.strUserFriendlyName;
			}
		}
		private System.Drawing.Rectangle rBounds;
		public System.Drawing.Rectangle Bounds {
			get {
				return this.rBounds;
			}
		}
		public string NumberUserFriendlyName {
			get {
				return this.Number == 0 ? "<" + this.strUserFriendlyName + ">" : (MagicRemoteService.Properties.Resources.Screen + " " + this.Number.ToString() + " : " + this.strUserFriendlyName);
			}
		}
		static Screen() {
			Microsoft.Win32.SystemEvents.DisplaySettingsChanged += Screen.DisplaySettingsChangedEvent;
			Screen.UpdateScreen();
		}
		static public void DisplaySettingsChangedEvent(object sender, System.EventArgs e) {
			Screen.UpdateScreen();
		}

		private static void UpdateScreen() {

			uint uiPathCount;
			uint uiModeCount;
			if(0 != WinApi.User32.GetDisplayConfigBufferSizes(WinApi.QueryDisplayConfigFlags.QDC_ALL_PATHS, out uiPathCount, out uiModeCount)) {
				throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			}

			WinApi.DisplayConfigPathInfo[] arrPath = new WinApi.DisplayConfigPathInfo[uiPathCount];
			WinApi.DisplayConfigModeInfo[] arrMode = new WinApi.DisplayConfigModeInfo[uiModeCount];
			if(0 != WinApi.User32.QueryDisplayConfig(WinApi.QueryDisplayConfigFlags.QDC_ALL_PATHS, ref uiPathCount, arrPath, ref uiModeCount, arrMode, System.IntPtr.Zero)) {
				throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			}

			System.Collections.Generic.Dictionary<uint, Screen> dAllScreen = new System.Collections.Generic.Dictionary<uint, Screen>();
			foreach(WinApi.DisplayConfigPathInfo p in arrPath) {
				if (p.targetInfo.targetAvailable) {
					if(!dAllScreen.ContainsKey(p.targetInfo.id)) {
						WinApi.DisplayConfigTargetDeviceName TargetName = new WinApi.DisplayConfigTargetDeviceName() {
							header = new WinApi.DisplayConfigDeviceInfoHeader {
								type = WinApi.DisplayConfigDeviceInfoType.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME,
								size = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinApi.DisplayConfigTargetDeviceName)),
								adapterId = p.targetInfo.adapterId,
								id = p.targetInfo.id
							}
						};
						if(0 != WinApi.User32.DisplayConfigGetDeviceInfo(ref TargetName)) {
							throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
						}
						dAllScreen.Add(p.targetInfo.id, new Screen() {
							ucNumber = (byte)(dAllScreen.Count + 1),
							bActive = false,
							bPrimary = false,
							uiId = p.targetInfo.id,
							strUserFriendlyName = TargetName.monitorFriendlyDeviceName,
							rBounds = new System.Drawing.Rectangle {
								X = 0,
								Y = 0,
								Width = 0,
								Height = 0
							},
						}); ;
					}
					if(p.flags.HasFlag(WinApi.DisplayConfigPathInfoFlags.DISPLAYCONFIG_PATH_ACTIVE)) {
						if(!p.sourceInfo.u.modeInfoIdx.HasFlag(WinApi.DisplayConfigPathInfoIdx.DISPLAYCONFIG_PATH_MODE_IDX_INVALID)) {
							dAllScreen[p.targetInfo.id].bActive = true;
							dAllScreen[p.targetInfo.id].bPrimary = arrMode[(uint)p.sourceInfo.u.modeInfoIdx].u.sourceMode.position.x == 0 && arrMode[(uint)p.sourceInfo.u.modeInfoIdx].u.sourceMode.position.y == 0;
							dAllScreen[p.targetInfo.id].rBounds.X = arrMode[(uint)p.sourceInfo.u.modeInfoIdx].u.sourceMode.position.x;
							dAllScreen[p.targetInfo.id].rBounds.Y = arrMode[(uint)p.sourceInfo.u.modeInfoIdx].u.sourceMode.position.y;
							dAllScreen[p.targetInfo.id].rBounds.Width = (int)arrMode[(uint)p.sourceInfo.u.modeInfoIdx].u.sourceMode.width;
							dAllScreen[p.targetInfo.id].rBounds.Height = (int)arrMode[(uint)p.sourceInfo.u.modeInfoIdx].u.sourceMode.height;
						}
					}
				}
			}

			lock(Screen.oAllScreenLock) {
				Screen.scrPrimaryScreen = null;
				Screen.dAllScreen = dAllScreen;
				foreach(Screen scr in dAllScreen.Values) {
					if(scr.Primary) {
						Screen.scrPrimaryScreen = scr;
					}
				}
			}
		}
	}
}
