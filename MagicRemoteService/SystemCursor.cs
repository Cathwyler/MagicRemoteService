namespace MagicRemoteService {
	internal static class SystemCursor {

		private static readonly System.IntPtr hInvisibleCursor = WinApi.User32.CreateCursor(System.IntPtr.Zero, 0, 0, 32, 32, new byte[]
		{
			0xFF, 0xFF, 0xFF, 0xFF,   // line 1 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 2 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 3 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 4 
 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 5 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 6 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 7 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 8 
 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 9 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 10 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 11 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 12 
 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 13 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 14 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 15 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 16 
 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 17 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 18 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 19 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 20 
 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 21 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 22 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 23 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 24 
 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 25 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 26 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 27 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 28 
 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 29 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 30 
			0xFF, 0xFF, 0xFF, 0xFF,   // line 31 
			0xFF, 0xFF, 0xFF, 0xFF	// line 32 
		}, new byte[]
		{
			0x00, 0x00, 0x00, 0x00,   // line 1 
			0x00, 0x00, 0x00, 0x00,   // line 2 
			0x00, 0x00, 0x00, 0x00,   // line 3 
			0x00, 0x00, 0x00, 0x00,   // line 4 
 
			0x00, 0x00, 0x00, 0x00,   // line 5 
			0x00, 0x00, 0x00, 0x00,   // line 6 
			0x00, 0x00, 0x00, 0x00,   // line 7 
			0x00, 0x00, 0x00, 0x00,   // line 8 
 
			0x00, 0x00, 0x00, 0x00,   // line 9 
			0x00, 0x00, 0x00, 0x00,   // line 10 
			0x00, 0x00, 0x00, 0x00,   // line 11 
			0x00, 0x00, 0x00, 0x00,   // line 12 
 
			0x00, 0x00, 0x00, 0x00,   // line 13 
			0x00, 0x00, 0x00, 0x00,   // line 14 
			0x00, 0x00, 0x00, 0x00,   // line 15 
			0x00, 0x00, 0x00, 0x00,   // line 16 
 
			0x00, 0x00, 0x00, 0x00,   // line 17 
			0x00, 0x00, 0x00, 0x00,   // line 18 
			0x00, 0x00, 0x00, 0x00,   // line 19 
			0x00, 0x00, 0x00, 0x00,   // line 20 
 
			0x00, 0x00, 0x00, 0x00,   // line 21 
			0x00, 0x00, 0x00, 0x00,   // line 22 
			0x00, 0x00, 0x00, 0x00,   // line 23 
			0x00, 0x00, 0x00, 0x00,   // line 24 
 
			0x00, 0x00, 0x00, 0x00,   // line 25 
			0x00, 0x00, 0x00, 0x00,   // line 26 
			0x00, 0x00, 0x00, 0x00,   // line 27 
			0x00, 0x00, 0x00, 0x00,   // line 28 
 
			0x00, 0x00, 0x00, 0x00,   // line 29 
			0x00, 0x00, 0x00, 0x00,   // line 30 
			0x00, 0x00, 0x00, 0x00,   // line 31 
			0x00, 0x00, 0x00, 0x00	// line 32 
		});
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

		private static System.Collections.Generic.IDictionary<WinApi.OemCursorRessourceId, System.IntPtr> dSystemCursor = new System.Collections.Generic.Dictionary<WinApi.OemCursorRessourceId, System.IntPtr>();

		private static readonly int iMouseNoSpeed = 10;
		private static readonly System.IntPtr hMouseNoAccel = System.Runtime.InteropServices.GCHandle.Alloc(new int[3] { 0, 0, 0 }, System.Runtime.InteropServices.GCHandleType.Pinned).AddrOfPinnedObject();
		private static int iMouseSpeed = 0;
		private static readonly System.IntPtr hMouseSpeed = System.Runtime.InteropServices.GCHandle.Alloc(MagicRemoteService.SystemCursor.iMouseSpeed, System.Runtime.InteropServices.GCHandleType.Pinned).AddrOfPinnedObject();
		private static System.IntPtr hMouseAccel = System.IntPtr.Zero;
		
		public static void HideSytemCursor() {
#if !DEBUG
			foreach(WinApi.OemCursorRessourceId ocri in MagicRemoteService.SystemCursor.arrCursor) {
				if(!MagicRemoteService.SystemCursor.dSystemCursor.ContainsKey(ocri)) {
					MagicRemoteService.SystemCursor.dSystemCursor.Add(ocri, WinApi.User32.CopyIcon(WinApi.User32.LoadCursor(System.IntPtr.Zero, (int)ocri)));
				}
				WinApi.User32.SetSystemCursor(WinApi.User32.CopyIcon(MagicRemoteService.SystemCursor.hInvisibleCursor), (uint)ocri);
			}
#endif
		}
		public static void ShowSytemCursor() {
#if !DEBUG
			foreach(WinApi.OemCursorRessourceId ocri in MagicRemoteService.SystemCursor.arrCursor) {
				if(MagicRemoteService.SystemCursor.dSystemCursor.ContainsKey(ocri)) {
					WinApi.User32.SetSystemCursor(MagicRemoteService.SystemCursor.dSystemCursor[ocri], (uint)ocri);
					MagicRemoteService.SystemCursor.dSystemCursor.Remove(ocri);
				}
			}
#endif
		}
		public static void DisableMouseSpeedAccel() {
			WinApi.User32.SystemParametersInfo((uint)WinApi.SystemParametersInfoAction.SPI_GETMOUSESPEED, 0, MagicRemoteService.SystemCursor.hMouseSpeed, 0);
			WinApi.User32.SystemParametersInfo((uint)WinApi.SystemParametersInfoAction.SPI_SETMOUSESPEED, 0, MagicRemoteService.SystemCursor.iMouseNoSpeed, 0);
		
			if(MagicRemoteService.SystemCursor.hMouseAccel == System.IntPtr.Zero) {
				MagicRemoteService.SystemCursor.hMouseAccel = System.Runtime.InteropServices.GCHandle.Alloc(new int[3], System.Runtime.InteropServices.GCHandleType.Pinned).AddrOfPinnedObject();
			}
			WinApi.User32.SystemParametersInfo((uint)WinApi.SystemParametersInfoAction.SPI_GETMOUSE, 0, MagicRemoteService.SystemCursor.hMouseAccel, 0);
			WinApi.User32.SystemParametersInfo((uint)WinApi.SystemParametersInfoAction.SPI_SETMOUSE, 0, MagicRemoteService.SystemCursor.hMouseNoAccel, 0);
		}
		public static void EnableMouseSpeedAccel() {
			if(MagicRemoteService.SystemCursor.iMouseSpeed != 0) {
				WinApi.User32.SystemParametersInfo((uint)WinApi.SystemParametersInfoAction.SPI_SETMOUSESPEED, 0, MagicRemoteService.SystemCursor.iMouseSpeed, 0);
			}
			if(MagicRemoteService.SystemCursor.hMouseAccel != System.IntPtr.Zero) {
				WinApi.User32.SystemParametersInfo((uint)WinApi.SystemParametersInfoAction.SPI_SETMOUSE, 0, MagicRemoteService.SystemCursor.hMouseAccel, 0);
			}
		}
	}
}
