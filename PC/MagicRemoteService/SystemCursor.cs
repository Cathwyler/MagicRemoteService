﻿namespace MagicRemoteService {

	public enum CursorName {
		OCR_APPSTARTING = 32650,
		OCR_NORMAL = 32512,
		OCR_CROSS = 32515,
		OCR_HAND = 32649,
		OCR_HELP = 32651,
		OCR_IBEAM = 32513,
		OCR_NO = 32648,
		OCR_SIZEALL = 32646,
		OCR_SIZENESW = 32643,
		OCR_SIZENS = 32645,
		OCR_SIZENWSE = 32642,
		OCR_SIZEWE = 32644,
		OCR_UP = 32516,
		OCR_WAIT = 32514
	}

	static class SystemCursor {
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern bool SetSystemCursor(System.IntPtr hcur, uint id);

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern System.IntPtr LoadCursor(System.IntPtr hInstance, int lpCursorName);

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern System.IntPtr CreateCursor(System.IntPtr hInst, int xHotSpot, int yHotSpot, int nWidth, int nHeight, byte[] pvANDPlane, byte[] pvXORPlane);

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern System.IntPtr CopyIcon(System.IntPtr pcur);

		private static readonly System.IntPtr hInvisibleCursor = CreateCursor(System.IntPtr.Zero, 0, 0, 32, 32, new byte[]
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
            0xFF, 0xFF, 0xFF, 0xFF    // line 32 
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
            0x00, 0x00, 0x00, 0x00    // line 32 
        });

		private static System.Collections.Generic.IDictionary<CursorName, System.IntPtr> dSystemCursor = new System.Collections.Generic.Dictionary<CursorName, System.IntPtr>();

		public static void HideSytemCursor() {
			foreach(CursorName cnCursorName in (CursorName[])System.Enum.GetValues(typeof(CursorName))) {
				if(!SystemCursor.dSystemCursor.ContainsKey(cnCursorName)) {
					SystemCursor.dSystemCursor.Add(cnCursorName, CopyIcon(LoadCursor(System.IntPtr.Zero, (int)cnCursorName)));
				}
				SetSystemCursor(CopyIcon(SystemCursor.hInvisibleCursor), (uint)cnCursorName);
			}
		}
		public static void ShowSytemCursor() {
			foreach(CursorName cnCursorName in (CursorName[])System.Enum.GetValues(typeof(CursorName))) {
				if(SystemCursor.dSystemCursor.ContainsKey(cnCursorName)) {
					SetSystemCursor(SystemCursor.dSystemCursor[cnCursorName], (uint)cnCursorName);
					SystemCursor.dSystemCursor.Remove(cnCursorName);
				}
			}
		}
	}
}
