using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace fisherCLI {
	class MethodHelper {

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="usingSteam">Determines whether the user is using Steam or Kongregate for the program.</param>
		/// <param name="rod">Determines which fishing rod the user is using to fish</param>
		public MethodHelper(bool usingSteam, int rod) {
			steam = usingSteam;
			rodType = rod;
		}

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

		private int screenNumContainingGame;
		private bool steam;
		private int rodType;
	

		public static void LeftClick(Point point) {
			NativeMethods.mouse_event(MOUSEEVENTF_LEFTDOWN, point.X, point.Y, 0, 0);
			NativeMethods.mouse_event(MOUSEEVENTF_LEFTUP, point.X, point.Y, 0, 0);
		}

		//----------------------------------------------------------------------------------------------------------//
		//-------------------------------------------Screen Pixel methods-------------------------------------------//
		//----------------------------------------------------------------------------------------------------------//


		public Color GetPixelColor(Point location) {
			int x = location.X;
			int y = location.Y;
			IntPtr hdc = NativeMethods.GetDC(IntPtr.Zero);
			uint pixel = NativeMethods.GetPixel(hdc, x, y);
			NativeMethods.ReleaseDC(IntPtr.Zero, hdc);
			Color color = Color.FromArgb((int)(pixel & 0x000000FF),
						 (int)(pixel & 0x0000FF00) >> 8,
						 (int)(pixel & 0x00FF0000) >> 16);
			return color;
		}

		public bool AreColorsSimilar(Color c1, Color c2, int tolerance) {
			return Math.Abs(c1.R - c2.R) < tolerance &&
				   Math.Abs(c1.G - c2.G) < tolerance &&
				   Math.Abs(c1.B - c2.B) < tolerance;
		}

		public Dictionary<int, Bitmap> GetScreenShot(bool saveScreenshot) {
			//Using `SystemInformation.VirtualScreen` rather than `Screen.PrimaryScreen` 
			//ensures that the program can look at all screens for the start button.
			Dictionary<int, Bitmap> results = new Dictionary<int, Bitmap>();
			int i = 0;
			foreach (var screen in Screen.AllScreens) {

				Bitmap result = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
				{
					using (Graphics gfx = Graphics.FromImage(result)) {
						gfx.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0, 0, result.Size);
						results.Add(i, result);
					}
					if (saveScreenshot) {
						result.Save(screen.DeviceName.ToString().Trim('\\') + ".png", ImageFormat.Png);
					}
				}
				++i;
			}
			return results;
		}

		public Point FindColor(Color color) {

			int searchValue = color.ToArgb();
			Point result = new Point();
			foreach (var screen in GetScreenShot(false)) {
				using (Bitmap bmp = screen.Value) {
					for (int x = 0; x < bmp.Width; x++) {
						for (int y = 0; y < bmp.Height; y++) {
							//Cursor.Position = new Point(x, y);
							if (searchValue.Equals(bmp.GetPixel(x, y).ToArgb())) {
								screenNumContainingGame = screen.Key;
								return result = new Point(x + 30, y);
							}
						}
					}
				}
			}
			return result;
		}

		//---------------------------------------------------------------------------------------------------------//
		//------------------------------------------------Debugging------------------------------------------------//
		//---------------------------------------------------------------------------------------------------------//


		public int getTimes(Point location, Color color) {
			int sum = 0;
			Stopwatch s = new Stopwatch();
			List<long> times = new List<long>();
			bool blue = true;
			int x = location.X;
			int y = location.Y;
			while (times.Count < 5) {
				var c = GetPixelColor(location);
				if (c.R == color.R && c.G == color.G && c.B == color.B) {
					blue = true;
					s.Reset();
					s.Start();
					Thread.Sleep(200);
					while (blue) {
						Color c1 = GetPixelColor(location);
						if (c1.R == color.R && c1.G == color.G && c1.B == color.B) {
							s.Stop();
							times.Add(s.ElapsedMilliseconds);
							blue = false;
						}
					}
				}
			}
			return sum / times.Count();
		}

		public int getTimesMessageBox(Point locationStartButton) {
			Color fullRangeCastColor = Color.FromArgb(026, 118, 241);
			Point rangeAreaPosition = new Point(locationStartButton.X, locationStartButton.Y - 75);
			if (checkIfUserOnStartScreen(locationStartButton)) {
				SendKeys.Send("%{Tab}");
			}
			Thread.Sleep(100);
			return getTimes(rangeAreaPosition, fullRangeCastColor);
		}

		//---------------------------------------------------------------------------------------------------------//
		//------------------------------------------Auto clicking helpers------------------------------------------//
		//---------------------------------------------------------------------------------------------------------//
		public void startCast(Point locationStartButton) {
			checkIfUserOnStartScreen(locationStartButton);
			Color fullRangeCastColor = Color.FromArgb(026, 118, 241);
			Point rangeAreaPosition = new Point(locationStartButton.X, locationStartButton.Y - 75);
			Thread.Sleep(100);
			castRod(rangeAreaPosition, fullRangeCastColor);
		}

		public bool checkIfUserOnStartScreen(Point locationStartButton) {
			if (GetPixelColor(locationStartButton).Equals(Color.FromArgb(155, 208, 30))) {
				Cursor.Position = locationStartButton;
				LeftClick(locationStartButton);
				return true;
			} else {
				//used to view position of cursor when using two different screens
				Cursor.Position = locationStartButton;
			}
			return false;
		}

		public Point getScreenLocationPoint(Point location) {
			Screen screen = Screen.AllScreens[screenNumContainingGame];
			Point correctedLocation = new Point(screen.Bounds.Location.X + location.X, screen.Bounds.Location.Y + location.Y);
			return correctedLocation;
		}

		public void castRod(Point location, Color color) {
			while (true) {
				var c = GetPixelColor(location);
				if (c.R == color.R && c.G == color.G && c.B == color.B) {
					Thread.Sleep(rodType);
					SendKeys.Send(" ");
					return;
				}
			}
		}
		public void catchFish(Point location, Color color) {
			Cursor.Position = location;
			while (true) {
				var c = GetPixelColor(location);
				if (c.R == color.R && c.G == color.G && c.B == color.B) {
					SendKeys.Send(" ");
					return;
				}
			}
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
