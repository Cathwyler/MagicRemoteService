
namespace MagicRemoteService.WinApi {
	public enum WTSConnectStateClass {
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
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	struct WTSSessionInfo {
		public uint SessionID;
		[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
		public string pWinStationName;
		public WTSConnectStateClass State;
	}
	static class Wtsapi32 {
		[System.Runtime.InteropServices.DllImport("wtsapi32.dll", SetLastError = true)]
		public static extern int WTSEnumerateSessions(System.IntPtr hServer, int Reserved, int Version, ref WTSSessionInfo ppSessionInfo, ref int pCount);
		[System.Runtime.InteropServices.DllImport("wtsapi32.dll", SetLastError = true)]
		public static extern bool WTSQueryUserToken(uint sessionId, out System.IntPtr hToken);
	}
}
