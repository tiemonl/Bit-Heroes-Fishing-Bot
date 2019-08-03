using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fisherCLI {
	static class Program {

		static int exitCode = 0;

		public static void ExitApplication(int exitCode) {
			Program.exitCode = exitCode;
			Application.Exit();
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1(Convert.ToInt32(args[0])));
			return 0;
		}
	}
}
