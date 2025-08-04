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
		private static readonly int iMagicRemoteServiceMouseSpeed = 10;
		private static readonly System.IntPtr hMagicRemoteServiceMouseAccel = System.Runtime.InteropServices.GCHandle.Alloc(new int[3] { 0, 0, 0 }, System.Runtime.InteropServices.GCHandleType.Pinned).AddrOfPinnedObject();
		private static readonly int iDefaultMouseSpeed = 0;
		private static readonly System.IntPtr hDefaultMouseSpeed = System.Runtime.InteropServices.GCHandle.Alloc(MagicRemoteService.SystemCursor.iDefaultMouseSpeed, System.Runtime.InteropServices.GCHandleType.Pinned).AddrOfPinnedObject();
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
				WinApi.User32.SetSystemCursor(WinApi.User32.CopyIcon(MagicRemoteService.SystemCursor.hMagicRemoteServiceCursor), ocri);
			}
		}
		public static void SetDefaultSystemCursor() {
			WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_SETCURSORS, 0, System.IntPtr.Zero, 0);
		}
		public static void SetMagicRemoteServiceMouseSpeedAccel() {
			WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_GETMOUSESPEED, 0, MagicRemoteService.SystemCursor.hDefaultMouseSpeed, 0);
			WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_SETMOUSESPEED, 0, MagicRemoteService.SystemCursor.iMagicRemoteServiceMouseSpeed, 0);

			WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_GETMOUSE, 0, MagicRemoteService.SystemCursor.hDefaultMouseAccel, 0);
			WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_SETMOUSE, 0, MagicRemoteService.SystemCursor.hMagicRemoteServiceMouseAccel, 0);
		}
		public static void SetDefaultMouseSpeedAccel() {
			if(iDefaultMouseSpeed != 0) {
				WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_SETMOUSESPEED, 0, (int)MagicRemoteService.SystemCursor.iDefaultMouseSpeed, 0);
			}
			if(arrDefaultMouseAccel[2] != -1) {
				WinApi.User32.SystemParametersInfo(WinApi.SystemParametersInfoAction.SPI_SETMOUSE, 0, MagicRemoteService.SystemCursor.hDefaultMouseAccel, 0);
			}
		}
	}
}
