
namespace MagicRemoteService {
	class Screen {
		private static Screen[] arrAllScreen = new Screen[0];
		public static Screen[] AllScreen {
			get {
				return Screen.arrAllScreen;
			}
		}
		private static System.Collections.Generic.Dictionary<string, Screen> dAllScreenByName = new System.Collections.Generic.Dictionary<string, Screen>();
		public static System.Collections.Generic.Dictionary<string, Screen> AllScreenByName {
			get {
				return Screen.dAllScreenByName;
			}
		}
		private static Screen scrPrimaryScreen;
		public static Screen PrimaryScreen {
			get {
				return Screen.scrPrimaryScreen;
			}
		}
		private string strUserFriendlyName;
		public string UserFriendlyName {
			get {
				return this.strUserFriendlyName;
			}
		}
		private string strInstanceName;
		public string InstanceName {
			get {
				return this.strInstanceName;
			}
		}
		private byte ucNumber;
		public byte Number {
			get {
				return this.ucNumber;
			}
		}
		private string strGdiDeviceName;
		public string Path {
			get {
				return this.strGdiDeviceName;
			}
		}
		private System.Drawing.Rectangle rBounds;
		public System.Drawing.Rectangle Bounds {
			get {
				return this.rBounds;
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

			System.Collections.Generic.Dictionary<uint, Screen> dPathSource = new System.Collections.Generic.Dictionary<uint, Screen>();
			foreach(WinApi.DisplayConfigPathInfo p in arrPath) {
				if (p.targetInfo.targetAvailable) {
					if(!dPathSource.ContainsKey(p.targetInfo.id)) {
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
						dPathSource.Add(p.targetInfo.id, new Screen() {
							ucNumber = (byte)(dPathSource.Count + 1),
							strInstanceName = TargetName.monitorDevicePath,
							strUserFriendlyName = TargetName.monitorFriendlyDeviceName,
							strGdiDeviceName = ""
						});
					}
					if(p.flags.HasFlag(WinApi.DisplayConfigPathInfoFlags.DISPLAYCONFIG_PATH_ACTIVE)) {
						WinApi.DisplayConfigSourceDeviceName SourceName = new WinApi.DisplayConfigSourceDeviceName() {
							header = new WinApi.DisplayConfigDeviceInfoHeader {
								type = WinApi.DisplayConfigDeviceInfoType.DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME,
								size = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinApi.DisplayConfigSourceDeviceName)),
								adapterId = p.sourceInfo.adapterId,
								id = p.sourceInfo.id
							}
						};
						if(0 != WinApi.User32.DisplayConfigGetDeviceInfo(ref SourceName)) {
							throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
						}
						dPathSource[p.targetInfo.id].strGdiDeviceName = SourceName.viewGdiDeviceName;

						
						dPathSource[p.targetInfo.id].rBounds.Location = arrMode[p.sourceInfo.u.modeInfoIdx].u.sourceMode.position;
						dPathSource[p.targetInfo.id].rBounds.Width = (int)arrMode[p.sourceInfo.u.modeInfoIdx].u.sourceMode.width;
						dPathSource[p.targetInfo.id].rBounds.Height = (int)arrMode[p.sourceInfo.u.modeInfoIdx].u.sourceMode.height;
						if(arrMode[p.sourceInfo.u.modeInfoIdx].u.sourceMode.position.X == 0 && arrMode[p.sourceInfo.u.modeInfoIdx].u.sourceMode.position.Y == 0) {
							Screen.scrPrimaryScreen = dPathSource[p.targetInfo.id];
						}

					}
				}
			}

			Screen.dAllScreenByName = new System.Collections.Generic.Dictionary<string, Screen>(); ;
			foreach(Screen scr in dPathSource.Values) {
				Screen.dAllScreenByName.Add(scr.InstanceName, scr);
			}
			Screen.arrAllScreen = new Screen[dPathSource.Count];
			foreach(Screen s in dPathSource.Values) {
				Screen.arrAllScreen[s.ucNumber - 1] = s;
			}
			//WinApi.DevMode dm = new WinApi.DevMode() {
			//	dmSize = (short)System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinApi.DevMode))
			//};
			//if(WinApi.User32.EnumDisplaySettings(SourceName.viewGdiDeviceName, WinApi.User32.ENUM_CURRENT_SETTINGS, ref dm)) {
			//	throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			//}
			//if(dd.StateFlags.HasFlag(WinApi.StateFlags.DISPLAY_DEVICE_PRIMARY_DEVICE)) {
			//	this.scrPrimaryScreen = uc;
			//}

			//if(0 != WinApi.User32.GetDisplayConfigBufferSizes(WinApi.User32.QUERY_DEVICE_CONFIG_FLAGS.QDC_ALL_PATHS, out uiPathCount, out uiModeCount)) {
			//	throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			//}

			//WinApi.User32.DISPLAYCONFIG_PATH_INFO[] arrPath = new WinApi.User32.DISPLAYCONFIG_PATH_INFO[uiPathCount];
			//WinApi.User32.DISPLAYCONFIG_MODE_INFO[] arrMode = new WinApi.User32.DISPLAYCONFIG_MODE_INFO[uiModeCount];
			//if(0 != WinApi.User32.QueryDisplayConfig(WinApi.User32.QUERY_DEVICE_CONFIG_FLAGS.QDC_ALL_PATHS, ref uiPathCount, arrPath, ref uiModeCount, arrMode, System.IntPtr.Zero)) {
			//	throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			//}

			//System.Collections.Generic.Dictionary<string, string> dPathSource = new System.Collections.Generic.Dictionary<string, string>();
			//foreach(WinApi.User32.DISPLAYCONFIG_PATH_INFO p in arrPath) {
			//	WinApi.User32.DISPLAYCONFIG_TARGET_DEVICE_NAME TargetName = new WinApi.User32.DISPLAYCONFIG_TARGET_DEVICE_NAME {
			//		header = new WinApi.User32.DISPLAYCONFIG_DEVICE_INFO_HEADER {
			//			type = WinApi.User32.DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME,
			//			size = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinApi.User32.DISPLAYCONFIG_TARGET_DEVICE_NAME)),
			//			adapterId = p.targetInfo.adapterId,
			//			id = p.targetInfo.id
			//		}
			//	};
			//	if(0 != WinApi.User32.DisplayConfigGetDeviceInfo(ref TargetName)) {
			//		throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			//	}
			//	WinApi.User32.DISPLAYCONFIG_SOURCE_DEVICE_NAME SourceName = new WinApi.User32.DISPLAYCONFIG_SOURCE_DEVICE_NAME {
			//		header = new WinApi.User32.DISPLAYCONFIG_DEVICE_INFO_HEADER {
			//			type = WinApi.User32.DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME,
			//			size = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinApi.User32.DISPLAYCONFIG_SOURCE_DEVICE_NAME)),
			//			adapterId = p.sourceInfo.adapterId,
			//			id = p.sourceInfo.id
			//		}
			//	};
			//	if(0 != WinApi.User32.DisplayConfigGetDeviceInfo(ref SourceName)) {
			//		throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
			//	}
			//	//dPathSource.Add(TargetName.monitorDevicePath, SourceName.viewGdiDeviceName);
			//}

			//System.Management.ManagementObjectCollection moc = (new System.Management.ManagementObjectSearcher("\\root\\wmi", "SELECT * FROM WMIMonitorID")).Get();
			//Screen[] arrScreen = new Screen[moc.Count];
			//byte ucNumber = 0;
			//foreach(System.Management.ManagementObject mo in moc) {
			//	byte[] UserFriendlyName = new byte[((ushort[])mo["UserFriendlyName"]).Length * sizeof(ushort)];
			//	System.Buffer.BlockCopy((ushort[])mo["UserFriendlyName"], 0, UserFriendlyName, 0, UserFriendlyName.Length);

			//	arrScreen[ucNumber].ucNumber = (byte)(ucNumber + 1);
			//	arrScreen[ucNumber].strInstanceName = (string)mo["InstanceName"];
			//	arrScreen[ucNumber].strUserFriendlyName = System.Text.Encoding.Unicode.GetString(UserFriendlyName).TrimEnd((char)0);
			//	arrScreen[ucNumber].strGdiDeviceName = 
			//	ucNumber++;
			//}

		}

	}
}
