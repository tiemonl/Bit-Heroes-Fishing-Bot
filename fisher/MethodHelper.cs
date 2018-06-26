using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fisher {
	class MethodHelper {

		public partial class NativeMethods {

			/// Return Type: BOOL->int
			///fBlockIt: BOOL->int
			[System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "BlockInput")]
			[return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
			public static extern bool BlockInput([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool fBlockIt);


			[DllImport("user32.dll")]
			public static extern bool GetCursorPos(ref Point lpPoint);

			[DllImport("user32")]
			public static extern int SetCursorPos(int x, int y);

			[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
			public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

			[DllImport("user32.dll")]
			public static extern IntPtr GetDC(IntPtr hwnd);

			[DllImport("user32.dll")]
			public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

			[DllImport("gdi32.dll")]
			public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

			[DllImport("user32.dll")]
			public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
		}
		private const int MOUSEEVENTF_MOVE = 0x0001;
		private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
		private const int MOUSEEVENTF_LEFTUP = 0x0004;
		private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
		private const int MOUSEEVENTF_RIGHTUP = 0x0010;
		private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
		private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
		private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

		public static void LeftClick(Point point) {
			NativeMethods.mouse_event(MOUSEEVENTF_LEFTDOWN, point.X, point.Y, 0, 0);
			NativeMethods.mouse_event(MOUSEEVENTF_LEFTUP, point.X, point.Y, 0, 0);
		}


		public void fishGotAwayClick(Point location) {
			Cursor.Position = location;
			LeftClick(location);
			Thread.Sleep(1000);
		}


		public void tradeItemThenCloseClick(Point tradeLocation, Point closeLocation) {
			Cursor.Position = tradeLocation;
			LeftClick(tradeLocation);
			Thread.Sleep(500);
			Cursor.Position = closeLocation;
			LeftClick(closeLocation);
			Thread.Sleep(500);
		}

		public void fishGotAwaySpace() {
			SendKeys.SendWait(" ");
			Thread.Sleep(1000);
		}



		public void tradeItemThenCloseSpace() {
			SendKeys.Send(" ");
			Thread.Sleep(500);
			SendKeys.Send(" ");
			Thread.Sleep(500);
		}
	}
}
