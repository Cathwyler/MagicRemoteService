namespace MagicRemoteService {
	static class SystemCursor {
		private static readonly System.IntPtr hMagicRemoteServiceCursor = SystemCursor.GetCursor(MagicRemoteService.Properties.Resources.MagicRemoteServiceCursor);
		private static readonly WinApi.OemCursorRessourceId[] arrCursor = new WinApi.OemCursorRessourceId[] {
			WinApi.OemCursorRessourceId.OCR_NORMAL,
			WinApi.OemCursorRessourceId.OCR_IBEAM,
			WinApi.OemCursorRessourceId.OCR_WAIT,
			WinApi.OemCursorRessourceId.OCR_CROSS,
			WinApi.OemCursorRessourceId.OCR_UP,
			WinApi.OemCursorRessourceId.OCR_HAND,
			WinApi.OemCursorRessourceId.OCR_NO,
			WinApi.OemCursorRessourceId.OCR_APPSTARTING
		};
		private static readonly System.Collections.Generic.IDictionary<WinApi.OemCursorRessourceId, System.IntPtr> dSystemCursor = new System.Collections.Generic.Dictionary<WinApi.OemCursorRessourceId, System.IntPtr>();
		private static readonly int iMagicRemoteServiceMouseSpeed = 10;
		private static readonly System.IntPtr hMagicRemoteServiceMouseAccel = System.Runtime.InteropServices.GCHandle.Alloc(new int[3] { 0, 0, 0 }, System.Runtime.InteropServices.GCHandleType.Pinned).AddrOfPinnedObject();
		private static int iDefaultMouseSpeed = 0;
		private static readonly int[] arrDefaultMouseAccel = new int[3] { 0, 0, -1 };
		private static readonly System.IntPtr hDefaultMouseAccel = System.Runtime.InteropServices.GCHandle.Alloc(arrDefaultMouseAccel, System.Runtime.InteropServices.GCHandleType.Pinned).AddrOfPinnedObject();
		private static System.IntPtr GetCursor(byte[] arrCursor) {
			string strCursor = System.IO.Path.GetTempFileName();
			System.IO.File.WriteAllBytes(strCursor, arrCursor);
			System.IntPtr hCursor = WinApi.User32.LoadCursorFromFile(strCursor);
			System.IO.File.Delete(strCursor);
			return hCursor;
		}
		public static void SetMagicRemoteServiceSystemCursor() {
			foreach(WinApi.OemCursorRessourceId ocri in MagicRemoteService.SystemCursor.arrCursor) {
				if(!MagicRemoteService.SystemCursor.dSystemCursor.ContainsKey(ocri)) {
					MagicRemoteService.SystemCursor.dSystemCursor.Add(ocri, WinApi.User32.CopyIcon(WinApi.User32.LoadCursor(System.IntPtr.Zero, ocri)));
				}
				WinApi.User32.SetSystemCursor(WinApi.User32.CopyIcon(MagicRemoteService.SystemCursor.hMagicRemoteServiceCursor), ocri);
			}
		}
		public static void SetDefaultSystemCursor() {
			if(MagicRemoteService.SystemCursor.dSystemCursor.Count == 0) {
				return;
			}
			foreach(WinApi.OemCursorRessourceId ocri in MagicRemoteService.SystemCursor.arrCursor) {
				if(MagicRemoteService.SystemCursor.dSystemCursor.TryGetValue(ocri, out System.IntPtr hSystemCursor)) {
					WinApi.User32.SetSystemCursor(hSystemCursor, ocri);
					MagicRemoteService.SystemCursor.dSystemCursor.Remove(ocri);
				}
			}
			// Reload the user's cursor theme so Windows restores its configured size
			// and DPI variants, rather than keeping only the copied cursor images.
			// The copies above remain a fallback if reloading the theme fails.
			WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_SETCURSORS, 0, System.IntPtr.Zero, 0);
		}
		public static void SetMagicRemoteServiceMouseSpeedAccel() {
			if(iDefaultMouseSpeed == 0 && WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_GETMOUSESPEED, 0, ref iDefaultMouseSpeed, 0)) {
				WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_SETMOUSESPEED, 0, MagicRemoteService.SystemCursor.iMagicRemoteServiceMouseSpeed, 0);
			}

			if(arrDefaultMouseAccel[2] == -1 && WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_GETMOUSE, 0, MagicRemoteService.SystemCursor.hDefaultMouseAccel, 0)) {
				WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_SETMOUSE, 0, MagicRemoteService.SystemCursor.hMagicRemoteServiceMouseAccel, 0);
			}
		}
		public static void SetDefaultMouseSpeedAccel() {
			if(iDefaultMouseSpeed != 0 && WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_SETMOUSESPEED, 0, iDefaultMouseSpeed, 0)) {
				iDefaultMouseSpeed = 0;
			}
			if(arrDefaultMouseAccel[2] != -1 && WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_SETMOUSE, 0, MagicRemoteService.SystemCursor.hDefaultMouseAccel, 0)) {
				arrDefaultMouseAccel[2] = -1;
			}
		}
	}
}
