using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;

namespace fisher {
	public partial class Form1 : Form {

		#region DLLImport's and declarations

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
		private static int rodType;
		private static Point locationCastCatchButton;
		private static Point locationCloseShellDialogBox;
		private static Point locationTradeFishButton;
		private static Point locationCloseItGotAwayButton;
		private static Point locationTimerCaughtFish;
		private static Point locationJunkItem;
		private static Point locationTopLeftWeightScreenshot;
		private static Point locationBottomRightWeightScreenshot;

		Color startButtonGreen = Color.FromArgb(155, 208, 30);
		Color castButtonBlue = Color.FromArgb(030, 170, 208);
		Color colorCloseItGotAwayButton = Color.FromArgb(030, 170, 208);
		Color colorTimerCaughtFishKong = Color.FromArgb(56, 255, 56);
		Color colorTimerCaughtFishSteam = Color.FromArgb(59, 255, 59);
		Color colorJunkItem = Color.FromArgb(255, 255, 255);

		List<Color> colorList = new List<Color>();

		FindWeight findWeight = new FindWeight();

		MethodHelper helper = new MethodHelper();
		#endregion



		public static void LeftClick(Point point) {
			NativeMethods.mouse_event(MOUSEEVENTF_LEFTDOWN, point.X, point.Y, 0, 0);
			NativeMethods.mouse_event(MOUSEEVENTF_LEFTUP, point.X, point.Y, 0, 0);
		}

		public Form1() {
			InitializeComponent();

			backgroundThread.WorkerReportsProgress = true;
			backgroundThread.WorkerSupportsCancellation = true;

			kongButton.CheckedChanged += new EventHandler(platform_CheckedChanged);
			steamButton.CheckedChanged += new EventHandler(platform_CheckedChanged);

			woodFishingRod.CheckedChanged += new EventHandler(rodType_CheckedChanged);
			trollingRod.CheckedChanged += new EventHandler(rodType_CheckedChanged);
			spinningRod.CheckedChanged += new EventHandler(rodType_CheckedChanged);
			flyRod.CheckedChanged += new EventHandler(rodType_CheckedChanged);
			legRod.CheckedChanged += new EventHandler(rodType_CheckedChanged);

			KeyPreview = true;
			castCatchLocationLbl.Text = "Cast/Catch Location:\nPress button when on fishing start screen";

			//steamButton.Enabled = false;
			rodChoiceGroupBox.Enabled = false;
			findLocationBtn.Enabled = false;
		}

		private void platform_CheckedChanged(object sender, EventArgs e) {
			RadioButton radioButton = sender as RadioButton;

			if (kongButton.Checked) {
				steam = false;
			} else if (steamButton.Checked) {
				steam = true;
			}
			rodChoiceGroupBox.Enabled = true;
		}

		private void rodType_CheckedChanged(object sender, EventArgs e) {
			RadioButton radioButton = sender as RadioButton;

			if (woodFishingRod.Checked) {
				if (steam)
					rodType = 1250;
				else
					rodType = 1260;
			} else if (trollingRod.Checked) {
				if (steam)
					rodType = 1350;
				else
					rodType = 1360;
			} else if (spinningRod.Checked) {
				if (steam)
					rodType = 1450;
				else
					rodType = 1460;
			} else if (flyRod.Checked) {
				if (steam)
					rodType = 1550;
				else
					rodType = 1560;
			} else if (legRod.Checked) {
				if (steam)
					rodType = 1650;
				else
					rodType = 1660;
			}
			findLocationBtn.Enabled = true;
		}

		private void Timer(Point location, Color color) {
			Stopwatch s = new Stopwatch();
			s.Start();
			int x = location.X;
			int y = location.Y;
			//int sleeptime = rodType;
			while (true) {
				var c = GetPixelColor(x, y);
				if (c.R == color.R && c.G == color.G && c.B == color.B) {

					s.Stop();
					MessageBox.Show(s.ElapsedMilliseconds.ToString());
					return;
				}
			}
		}
		private void castRod(Point location, Color color) {
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
		private void catchFish(Point location, Color color) {
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
		public void getTimes(Point location, Color color) {
			StringBuilder stringBuilder = new StringBuilder();
			long sum = 0;
			Stopwatch s = new Stopwatch();
			List<long> times = new List<long>();
			bool blue = true;
			int x = location.X;
			int y = location.Y;
			while (times.Count < 10) {
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

			if (steam) {
				stringBuilder.Append("Platform: Steam");
			} else
				stringBuilder.Append("Platform: Kong");

			int i = 1;
			foreach (var item in times) {

				stringBuilder.Append(i + ": ").Append(item).Append("\n");
				++i;
				sum += item;
			}
			sum = sum / times.Count();
			stringBuilder.Append("average: " + sum);
			rodType = (int)sum;
			MessageBox.Show(stringBuilder.ToString());

		}
		public Color getcolorlist(int x, int y) {
			var color = GetPixelColor(x, y);
			colorList.Add(color);
			return color;
		}
		static public Color GetPixelColor(int x, int y) {
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
			Bitmap result = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
			{
				using (Graphics gfx = Graphics.FromImage(result)) {
					gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
				}
			}
			return result;
		}
		private static Point FindColor(Color color) {

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
		private static void GetScreenie() {
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

		private void CastCatchLocation_Click(object sender, EventArgs e) {

			locationCastCatchButton = FindColor(startButtonGreen);
			if (steam) {
				locationTradeFishButton = new Point(locationCastCatchButton.X, locationCastCatchButton.Y - 40);
				locationCloseShellDialogBox = new Point(locationCastCatchButton.X + 270, locationCastCatchButton.Y - 350);
				locationCloseItGotAwayButton = new Point(locationCastCatchButton.X + 20, locationCastCatchButton.Y - 130);
				locationTimerCaughtFish = new Point(locationCastCatchButton.X - 200, locationCastCatchButton.Y - 70);
				locationJunkItem = new Point(locationCastCatchButton.X + 40, locationCastCatchButton.Y - 180);
				locationTopLeftWeightScreenshot = new Point(locationCastCatchButton.X - 25, locationCastCatchButton.Y - 130);
				locationBottomRightWeightScreenshot = new Point(locationCastCatchButton.X + 160, locationCastCatchButton.Y - 50);
			} else {
				locationTradeFishButton = new Point(locationCastCatchButton.X, locationCastCatchButton.Y - 15);
				locationCloseShellDialogBox = new Point(locationCastCatchButton.X + 265, locationCastCatchButton.Y - 325);
				locationCloseItGotAwayButton = new Point(locationCastCatchButton.X + 30, locationCastCatchButton.Y - 100);
				locationTimerCaughtFish = new Point(locationCastCatchButton.X - 200, locationCastCatchButton.Y - 70);
				locationJunkItem = new Point(locationCastCatchButton.X + 100, locationCastCatchButton.Y - 155);
				locationTopLeftWeightScreenshot = new Point(locationCastCatchButton.X - 25, locationCastCatchButton.Y - 130);
				locationBottomRightWeightScreenshot = new Point(locationCastCatchButton.X + 160, locationCastCatchButton.Y - 50);
				
			}
			castCatchLocationLbl.Text = "Cast/Catch Location:\n" + locationCastCatchButton.ToString();
			//getTimes(new Point(castCatchLocation.X, castCatchLocation.Y - 75), Color.FromArgb(026, 118, 241));
		}

		private void castMethod() {
			if (GetPixelColor(locationCastCatchButton.X, locationCastCatchButton.Y).Equals(Color.FromArgb(155, 208, 30))) {
				Cursor.Position = locationCastCatchButton;
				LeftClick(locationCastCatchButton);
			}
			Color fullRangeCastColor = Color.FromArgb(026, 118, 241);
			Point rangeAreaPosition = new Point(locationCastCatchButton.X, locationCastCatchButton.Y - 75);
			Thread.Sleep(100);
			castRod(rangeAreaPosition, fullRangeCastColor);
		}

		private void catchFishMethod() {
			Color oneHundredCatchColor = Color.FromArgb(77, 254, 0);
			Point oneHundredPosition;
			if (steam) {
				oneHundredPosition = new Point(locationCastCatchButton.X + 370, locationCastCatchButton.Y - 81);
			} else {
				oneHundredPosition = new Point(locationCastCatchButton.X + 373, locationCastCatchButton.Y - 81);
			}
			//Cursor.Position = oneHundredPosition;
			catchFish(oneHundredPosition, oneHundredCatchColor);

		}

		private void getTimesBtn_Click(object sender, EventArgs e) {
			Color fullRangeCastColor = Color.FromArgb(026, 118, 241);
			Point rangeAreaPosition = new Point(locationCastCatchButton.X, locationCastCatchButton.Y - 75);
			getTimesBtn.Enabled = false;
			Thread.Sleep(100);
			getTimes(rangeAreaPosition, fullRangeCastColor);
			Thread.Sleep(500);
			getTimesBtn.Enabled = true;
		}
		private void autoBtn_Click(object sender, EventArgs e) {

			backgroundThread.RunWorkerAsync();

		}

		private void backgroundThread_DoWork(object sender, DoWorkEventArgs e) {
			BackgroundWorker worker = sender as BackgroundWorker;
			int baitToUse = int.Parse(baitToUseText.Text);
			int i = 0;
			int baitUsed = 0;
			while (i < baitToUse) {
				if (worker.CancellationPending == true) {
					e.Cancel = true;
					break;
				} else {
					//Performs cast
					bool caughtFish = true;
					bool fishGetAway = true;
					printMessage(baitUsed, baitToUse, " bait used.\nPerforming cast.");
					++baitUsed;
					Invoke(new Action(() => Refresh()));
					Invoke(new Action(() => castMethod()));

					while (caughtFish) {
						printMessage(baitUsed, baitToUse, " bait used.\nWaiting for cast result.");
						Invoke(new Action(() => Refresh()));
						//performs cast
						Color color = GetPixelColor(locationTimerCaughtFish.X, locationTimerCaughtFish.Y);
						if (color == colorTimerCaughtFishKong || color == colorTimerCaughtFishSteam) {
							printMessage(baitUsed, baitToUse, " bait used.\nPerforming catch.");
							Invoke(new Action(() => Refresh()));
							Invoke(new Action(() => catchFishMethod()));
							Thread.Sleep(5000);
							while (fishGetAway) {
								//fish caught
								if (GetPixelColor(locationTradeFishButton.X, locationTradeFishButton.Y) == startButtonGreen) {
									printMessage(baitUsed, baitToUse, " bait used.\nCaught.");
									Invoke(new Action(() => Refresh()));
									//GetScreenie();
									Invoke(new Action(() => helper.tradeItemThenCloseSpace()));
									fishGetAway = false;

								}
								//fish got away
								else if (GetPixelColor(locationCloseItGotAwayButton.X, locationCloseItGotAwayButton.Y) == colorCloseItGotAwayButton) {
									printMessage(baitUsed, baitToUse, " bait used.\nFish got away. Sorry :(");
									Invoke(new Action(() => Refresh()));
									helper.fishGotAwaySpace();
									fishGetAway = false;
								}
							}
							caughtFish = false;
						}
						// caught junk
						else if (GetPixelColor(locationJunkItem.X, locationJunkItem.Y) == colorJunkItem) {
							printMessage(baitUsed, baitToUse, " bait used.\nCaught junk");
							Invoke(new Action(() => Refresh()));
							Invoke(new Action(() => helper.tradeItemThenCloseSpace()));
							caughtFish = false;
						}
					}
					++i;
				}
			}
			printMessage(baitUsed, baitToUse, " bait used.\nFinished.");
		}



		private void cancelAutoModeBtn_Click(object sender, EventArgs e) {
			if (backgroundThread.WorkerSupportsCancellation == true) {
				// Cancel the asynchronous operation.
				backgroundThread.CancelAsync();
			}
		}

		private void printMessage(int baitUsed, int baitToUse, string msg) {
			debugAutoStepLbl.Invoke((MethodInvoker)delegate {
				debugAutoStepLbl.Text = baitUsed + "/" + baitToUse + msg;
			});
		}
	}
}
