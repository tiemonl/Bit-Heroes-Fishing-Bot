using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace fisher {
	public partial class Form1 : Form {

		#region Declarations

		private static bool steam;
		private static int rodType;
		private static Point locationStartButton;
		private static Point locationCloseShellDialogBox;
		private static Point locationTradeFishButton;
		private static Point locationCloseItGotAwayButton;
		private static Point locationTimerCaughtFish;
		private static Point locationJunkItem;
		private static Point locationTopLeftWeightScreenshot;
		private static Point locationBottomRightWeightScreenshot;
		private static Point location100Position;

		Color startButtonGreen = Color.FromArgb(155, 208, 30);
		Color castButtonBlue = Color.FromArgb(030, 170, 208);
		Color colorCloseItGotAwayButton = Color.FromArgb(030, 170, 208);
		Color colorTimerCaughtFishKong = Color.FromArgb(56, 255, 56);
		Color colorTimerCaughtFishSteam = Color.FromArgb(59, 255, 59);
		Color colorJunkItem = Color.FromArgb(255, 255, 255);
		Color oneHundredCatchColor = Color.FromArgb(77, 254, 0);

		MethodHelper helper;
		#endregion


		public Form1() {

			InitializeComponent();

			backgroundThread.WorkerReportsProgress = true;
			backgroundThread.WorkerSupportsCancellation = true;
			backgroundThreadGetTimes.WorkerReportsProgress = true;
			backgroundThreadGetTimes.WorkerSupportsCancellation = true;

			this.Size = new Size(375, 350);

			rodTimerDebugToolTip.SetToolTip(rodTimerDebug, "Use this to adjust fishing timer to get a better catch result." +
				"\nAdd/subtract milliseconds (you shouldn't have to change more than 10 milliseconds)" +
				"\ndepending on if you are stopping before the max value or going past the max.");
			getTimesDedubToolTip.SetToolTip(getTimesBtn, "This function will automatically calculate the rod timer.\n" +
				"Press the button and wait for the program to time the cast bar going from max value to max value.\n" +
				"This will calculate five passes of the cast bar maximizing and return an average.\n" +
				"You can use this average as a solid rod timer to get a near 100% max cast.");

			kongButton.CheckedChanged += new EventHandler(platform_CheckedChanged);
			steamButton.CheckedChanged += new EventHandler(platform_CheckedChanged);

			woodFishingRod.CheckedChanged += new EventHandler(rodType_CheckedChanged);
			trollingRod.CheckedChanged += new EventHandler(rodType_CheckedChanged);
			spinningRod.CheckedChanged += new EventHandler(rodType_CheckedChanged);
			flyRod.CheckedChanged += new EventHandler(rodType_CheckedChanged);
			legRod.CheckedChanged += new EventHandler(rodType_CheckedChanged);

			castCatchLocationLbl.Text = "Cast/Catch Location:\nPress start button when on fishing start screen";

			rodChoiceGroupBox.Enabled = false;
			baitToUseText.Enabled = false;
			findLocationBtn.Enabled = false;
			autoBtn.Enabled = false;
			cancelAutoModeBtn.Enabled = false;
			getTimesBtn.Enabled = false;


		}
		/// <summary>
		/// This determines which radio button was clicked.
		/// This returns whether the user is using Steam or Kongregate to fish.
		/// </summary>
		private void platform_CheckedChanged(object sender, EventArgs e) {
			RadioButton radioButton = sender as RadioButton;

			if (kongButton.Checked) {
				steam = false;
			} else if (steamButton.Checked) {
				steam = true;
			}
			rodChoiceGroupBox.Enabled = true;
		}

		/// <summary>
		/// This determines which radio button was clicked.
		/// Each rod has a different timing.
		/// The timing is the time it takes for the casting bar to go from its max value, down to its lowest point and back up to its max value.
		/// I use this method of fishing as it has netted better results over clicking cast as soon as the program detects the casting bar at its max value.
		/// </summary>
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
					rodType = 1520;
				else
					rodType = 1525;
			} else if (legRod.Checked) {
				if (steam)
					rodType = 1580;
				else
					rodType = 1585;
			}
			rodTimerDebug.Value = rodType;
			helper = new MethodHelper(steam, rodType);
			baitToUseText.Enabled = true;
			findLocationBtn.Enabled = true;
		}


		private void CastCatchLocation_Click(object sender, EventArgs e) {

			locationStartButton = helper.FindColor(startButtonGreen);
			if (steam) {
				locationTradeFishButton = new Point(locationStartButton.X, locationStartButton.Y - 40);
				locationCloseShellDialogBox = new Point(locationStartButton.X + 270, locationStartButton.Y - 350);
				locationCloseItGotAwayButton = new Point(locationStartButton.X + 20, locationStartButton.Y - 130);
				locationTimerCaughtFish = new Point(locationStartButton.X - 200, locationStartButton.Y - 70);
				locationJunkItem = new Point(locationStartButton.X + 40, locationStartButton.Y - 180);
				locationTopLeftWeightScreenshot = new Point(locationStartButton.X - 25, locationStartButton.Y - 130);
				locationBottomRightWeightScreenshot = new Point(locationStartButton.X + 160, locationStartButton.Y - 50);
				location100Position = new Point(locationStartButton.X + 370, locationStartButton.Y - 81);
			} else {
				locationTradeFishButton = new Point(locationStartButton.X, locationStartButton.Y - 15);
				locationCloseShellDialogBox = new Point(locationStartButton.X + 265, locationStartButton.Y - 325);
				locationCloseItGotAwayButton = new Point(locationStartButton.X + 30, locationStartButton.Y - 100);
				locationTimerCaughtFish = new Point(locationStartButton.X - 200, locationStartButton.Y - 70);
				locationJunkItem = new Point(locationStartButton.X + 100, locationStartButton.Y - 155);
				locationTopLeftWeightScreenshot = new Point(locationStartButton.X - 25, locationStartButton.Y - 130);
				locationBottomRightWeightScreenshot = new Point(locationStartButton.X + 160, locationStartButton.Y - 50);
				location100Position = new Point(locationStartButton.X + 373, locationStartButton.Y - 81);

			}
			castCatchLocationLbl.Text = "Cast/Catch Location:\n" + locationStartButton.ToString();
			autoBtn.Enabled = true;
			cancelAutoModeBtn.Enabled = true;
			getTimesBtn.Enabled = true;
		}

		private void getTimesBtn_Click(object sender, EventArgs e) {
			getTimesBtn.Enabled = false;
			backgroundThreadGetTimes.RunWorkerAsync();
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
					printMessage(baitUsed, baitToUse, " bait used.\nPerforming cast.\nTimer: " + rodType);
					++baitUsed;
					Invoke(new Action(() => Refresh()));
					Invoke(new Action(() => helper.startCast(locationStartButton)));

					while (caughtFish) {
						printMessage(baitUsed, baitToUse, " bait used.\nWaiting for cast result.");
						Invoke(new Action(() => Refresh()));
						//performs cast
						Color color = helper.GetPixelColor(locationTimerCaughtFish.X, locationTimerCaughtFish.Y);
						if (color == colorTimerCaughtFishKong || color == colorTimerCaughtFishSteam) {
							printMessage(baitUsed, baitToUse, " bait used.\nPerforming catch.");
							Invoke(new Action(() => Refresh()));
							Invoke(new Action(() => helper.catchFish(location100Position, oneHundredCatchColor)));
							Thread.Sleep(5000);
							while (fishGetAway) {
								//fish caught
								if (helper.GetPixelColor(locationTradeFishButton.X, locationTradeFishButton.Y) == startButtonGreen) {
									printMessage(baitUsed, baitToUse, " bait used.\nCaught.");
									Invoke(new Action(() => Refresh()));
									//helper.getFishWeight(locationTopLeftWeightScreenshot);
									Invoke(new Action(() => helper.tradeItemThenCloseSpace()));
									fishGetAway = false;

								}
								//fish got away
								else if (helper.GetPixelColor(locationCloseItGotAwayButton.X, locationCloseItGotAwayButton.Y) == colorCloseItGotAwayButton) {
									printMessage(baitUsed, baitToUse, " bait used.\nFish got away. Sorry :(");
									Invoke(new Action(() => Refresh()));
									helper.fishGotAwaySpace();
									fishGetAway = false;
								}
							}
							caughtFish = false;
						}
						// caught junk
						else if (helper.GetPixelColor(locationJunkItem.X, locationJunkItem.Y) == colorJunkItem) {
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
			backgroundThread.CancelAsync();
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

		private void debugOptions_CheckedChanged(object sender, EventArgs e) {
			if (debugOptions.Checked) {
				this.Size = new Size(500, 350);

			} else {
				this.Size = new Size(375, 350);
			}
		}

		private void rodTimerDebug_ValueChanged(object sender, EventArgs e) {
			rodType = (int)rodTimerDebug.Value;
			//initiliaze helper again to reflect the rodtype change.
			helper = new MethodHelper(steam, rodType);
		}

		private void backgroundThreadGetTimes_DoWork(object sender, DoWorkEventArgs e) {
			BackgroundWorker worker = sender as BackgroundWorker;
			Invoke(new Action(() => helper.getTimesMessageBox(locationStartButton)));
			backgroundThreadGetTimes.CancelAsync();
		}
	}
}
