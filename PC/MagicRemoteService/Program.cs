namespace MagicRemoteService {

	enum CtrlType {
		CTRL_C_EVENT = 0,
		CTRL_BREAK_EVENT = 1,
		CTRL_CLOSE_EVENT = 2,
		CTRL_LOGOFF_EVENT = 5,
		CTRL_SHUTDOWN_EVENT = 6
	}

	enum CmdShow {
		SW_HIDE = 0,
		SW_SHOWNORMAL = 1,
		SW_SHOWMINIMIZED = 2,
		SW_SHOWMAXIMIZED = 3,
		SW_SHOWNOACTIVATE = 4,
		SW_SHOW = 5,
		SW_MINIMIZE = 6,
		SW_SHOWMINNOACTIVE = 7,
		SW_SHOWNA = 8,
		SW_RESTORE = 9,
		SW_SHOWDEFAULT = 10,
		SW_FORCEMINIMIZE = 11
	}

	delegate bool CtrlHandler(CtrlType sig);

	static class Program {

		private static MagicRemoteService mrsService = new MagicRemoteService();

		private static CtrlHandler chHandler;

		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
		static extern bool AllocConsole();

		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		static extern System.IntPtr GetConsoleWindow();

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		static extern bool ShowWindow(System.IntPtr hWnd, CmdShow nCmdShow);

		[System.Runtime.InteropServices.DllImport("Kernel32")]
		private static extern bool SetConsoleCtrlHandler(CtrlHandler chHandler, bool add);

		private static bool CtrlHandler(CtrlType ctCtrlType) {
			Program.areExit.Set();
			System.Threading.Thread.Sleep(100);
			return false;
		}
		private static System.Threading.AutoResetEvent areExit = new System.Threading.AutoResetEvent(false);


		static void Main() {
#if DEBUG
			AllocConsole();
			ShowWindow(GetConsoleWindow(), CmdShow.SW_HIDE);
			Program.chHandler += new CtrlHandler(CtrlHandler);
			SetConsoleCtrlHandler(Program.chHandler, true);

			Program.mrsService.Run();
			Program.areExit.WaitOne();
			Program.mrsService.Exit();

			System.Environment.Exit(0);
#else
            AllocConsole();
            ShowWindow(GetConsoleWindow(), CmdShow.SW_HIDE);
            Program.chHandler += new CtrlHandler(CtrlHandler);
            SetConsoleCtrlHandler(Program.chHandler, true);

            Program.mrsService.Run();
            Program.areExit.WaitOne();
            Program.mrsService.Exit();

            System.Environment.Exit(0);

            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new MagicRemoteService()
            //};
            //ServiceBase.Run(ServicesToRun);
#endif
		}
	}
}
