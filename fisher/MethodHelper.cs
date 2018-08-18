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

namespace fisher {
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

		private bool steam;
		private int rodType;

		FindWeight findWeight = new FindWeight();

		public static void LeftClick(Point point) {
			NativeMethods.mouse_event(MOUSEEVENTF_LEFTDOWN, point.X, point.Y, 0, 0);
			NativeMethods.mouse_event(MOUSEEVENTF_LEFTUP, point.X, point.Y, 0, 0);
		}

		//----------------------------------------------------------------------------------------------------------//
		//-------------------------------------------Screen Pixel methods-------------------------------------------//
		//----------------------------------------------------------------------------------------------------------//


		public Color GetPixelColor(int x, int y) {
			//Cursor.Position = new Point(x, y);
			IntPtr hdc = NativeMethods.GetDC(IntPtr.Zero);
			uint pixel = NativeMethods.GetPixel(hdc, x, y);
			NativeMethods.ReleaseDC(IntPtr.Zero, hdc);
			Color color = Color.FromArgb((int)(pixel & 0x000000FF),
						 (int)(pixel & 0x0000FF00) >> 8,
						 (int)(pixel & 0x00FF0000) >> 16);
			return color;
		}

		private static Bitmap GetScreenShot() {
			//Using `SystemInformation.VirtualScreen` rather than `Screen.PrimaryScreen` 
			//ensures that the program can look at all screens for the start button.
			int screenLeft = SystemInformation.VirtualScreen.Left;
			int screenTop = SystemInformation.VirtualScreen.Top;
			int screenWidth = SystemInformation.VirtualScreen.Width;
			int screenHeight = SystemInformation.VirtualScreen.Height;

			Bitmap result = new Bitmap(screenWidth, screenHeight, PixelFormat.Format32bppRgb);
			{
				using (Graphics gfx = Graphics.FromImage(result)) {
					gfx.CopyFromScreen(screenLeft, screenTop, 0, 0, result.Size, CopyPixelOperation.SourceCopy);
				}
			}
			return result;
		}
		public Point FindColor(Color color) {

			int searchValue = color.ToArgb();
			Point result = new Point();
			using (Bitmap bmp = GetScreenShot()) {
				for (int x = 0; x < bmp.Width; x++) {
					for (int y = 0; y < bmp.Height; y++) {
						//Cursor.Position = new Point(x, y);
						if (searchValue.Equals(bmp.GetPixel(x, y).ToArgb()))
							return result = new Point(x + 30, y);
					}
				}
			}
			return result;
		}

		//---------------------------------------------------------------------------------------------------------//
		//-----------------------------------------------Find Weight-----------------------------------------------//
		//---------------------------------------------------------------------------------------------------------//


		public void getFishWeight(Point locationTopLeftWeightScreenshot) {
			Rectangle section = new Rectangle(locationTopLeftWeightScreenshot, new Size(185, 80));

			Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
			using (Graphics g = Graphics.FromImage(screenshot)) {
				g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
			}
			CropImage(screenshot, section).Save("weightOfFish.bmp", ImageFormat.Bmp);
		}

		public static Bitmap CropImage(Bitmap source, Rectangle section) {
			// An empty bitmap which will hold the cropped image
			Bitmap bmp = new Bitmap(section.Width, section.Height);

			Graphics g = Graphics.FromImage(bmp);

			// Draw the given area (section) of the source image
			// at location 0,0 on the empty bitmap (bmp)
			g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

			return bmp;
		}

		//---------------------------------------------------------------------------------------------------------//
		//------------------------------------------------Debugging------------------------------------------------//
		//---------------------------------------------------------------------------------------------------------//

		public Color getcolorlist(int x, int y) {
			List<Color> colorList = new List<Color>();
			var color = GetPixelColor(x, y);
			colorList.Add(color);
			return color;
		}

		public void getTimes(Point location, Color color) {
			StringBuilder stringBuilder = new StringBuilder();
			long sum = 0;
			Stopwatch s = new Stopwatch();
			List<long> times = new List<long>();
			bool blue = true;
			int x = location.X;
			int y = location.Y;
			while (times.Count < 5) {
				var c = GetPixelColor(x, y);
				if (c.R == color.R && c.G == color.G && c.B == color.B) {
					blue = true;
					s.Reset();
					s.Start();
					Thread.Sleep(200);
					while (blue) {
						Color c1 = GetPixelColor(x, y);
						if (c1.R == color.R && c1.G == color.G && c1.B == color.B) {
							s.Stop();
							times.Add(s.ElapsedMilliseconds);
							blue = false;
						}
					}
				}
			}
			SendKeys.Send("{ESC}");
			if (steam) {
				stringBuilder.Append("Platform: Steam\n");
			} else
				stringBuilder.Append("Platform: Kong\n");

			int i = 1;
			foreach (var item in times) {

				stringBuilder.Append(i + ": ").Append(item).Append("\n");
				++i;
				sum += item;
			}
			sum = sum / times.Count();
			stringBuilder.Append("average: " + sum);
			MessageBox.Show(stringBuilder.ToString());
		}

		public void getTimesMessageBox(Point locationStartButton) {
			Color fullRangeCastColor = Color.FromArgb(026, 118, 241);
			Point rangeAreaPosition = new Point(locationStartButton.X, locationStartButton.Y - 75);
			if (checkIfUserOnStartScreen(locationStartButton)) {
				SendKeys.Send("%{Tab}");
			}
			Thread.Sleep(100);
			getTimes(rangeAreaPosition, fullRangeCastColor);
			Thread.Sleep(500);
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
			if (GetPixelColor(locationStartButton.X, locationStartButton.Y).Equals(Color.FromArgb(155, 208, 30))) {
				Cursor.Position = locationStartButton;
				LeftClick(locationStartButton);
				return true;
			}
			return false;
		}

		public void castRod(Point location, Color color) {
			int x = location.X;
			int y = location.Y;
			//int sleeptime = rodType;
			while (true) {
				var c = GetPixelColor(x, y);
				if (c.R == color.R && c.G == color.G && c.B == color.B) {
					Thread.Sleep(rodType);
					SendKeys.Send(" ");
					return;
				}
			}
		}
		public void catchFish(Point location, Color color) {
			Cursor.Position = location;
			int x = location.X;
			int y = location.Y;
			while (true) {
				var c = GetPixelColor(x, y);
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
