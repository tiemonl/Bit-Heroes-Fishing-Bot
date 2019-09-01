using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace fisherCLI {
	public partial class Form1 : Form {

		#region Declarations

		private int bait;
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


		public Form1(int bait) {

			InitializeComponent();
			this.bait = bait;
			steam = false;

			backgroundThread.WorkerReportsProgress = true;
			backgroundThread.WorkerSupportsCancellation = true;
			backgroundThreadGetTimes.WorkerReportsProgress = true;
			backgroundThreadGetTimes.WorkerSupportsCancellation = true;

			this.Size = new Size(375, 350);

			helper = new MethodHelper(steam);
			findCastCatchLocation();
			startFishing();
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

		private void findCastCatchLocation() {
			locationStartButton = helper.FindColor(startButtonGreen);
					locationTradeFishButton = new Point(locationStartButton.X, locationStartButton.Y - 15);
					locationCloseShellDialogBox = new Point(locationStartButton.X + 265, locationStartButton.Y - 325);
					locationCloseItGotAwayButton = new Point(locationStartButton.X + 30, locationStartButton.Y - 100);
					locationTimerCaughtFish = new Point(locationStartButton.X - 200, locationStartButton.Y - 70);
					locationJunkItem = new Point(locationStartButton.X + 100, locationStartButton.Y - 155);
					locationTopLeftWeightScreenshot = new Point(locationStartButton.X - 25, locationStartButton.Y - 130);
					locationBottomRightWeightScreenshot = new Point(locationStartButton.X + 160, locationStartButton.Y - 50);
					location100Position = new Point(locationStartButton.X + 373, locationStartButton.Y - 81);
		}

		private void getTimesBtn_Click(object sender, EventArgs e) {
			getTimesBtn.Enabled = false;
			backgroundThreadGetTimes.RunWorkerAsync();
			getTimesBtn.Enabled = true;
		}



		private void startFishing() {
			backgroundThread.RunWorkerAsync();
		}

		private void backgroundThread_DoWork(object sender, DoWorkEventArgs e) {
			BackgroundWorker worker = sender as BackgroundWorker;
			int baitToUse = bait;
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
					Invoke(new Action(() => Cursor.Position = locationTimerCaughtFish));
					while (caughtFish) {
						if (worker.CancellationPending == true) {
							e.Cancel = true;
							break;
						}
						printMessage(baitUsed, baitToUse, " bait used.\nWaiting for cast result.");
						Invoke(new Action(() => Refresh()));
						//performs cast
						Color color = helper.GetPixelColor(locationTimerCaughtFish);
						//if (color == colorTimerCaughtFishKong || color == colorTimerCaughtFishSteam) {
						if (helper.AreColorsSimilar(color, colorTimerCaughtFishKong, 20)) {
							if (worker.CancellationPending == true) {
								e.Cancel = true;
								break;
							}
							printMessage(baitUsed, baitToUse, " bait used.\nPerforming catch.");
							Invoke(new Action(() => Refresh()));
							Invoke(new Action(() => helper.catchFish(location100Position, oneHundredCatchColor)));
							Thread.Sleep(5000);
							while (fishGetAway) {
							
								//fish caught
								if (worker.CancellationPending == true) {
									e.Cancel = true;
									break;
								}
								if (helper.GetPixelColor(locationTradeFishButton) == startButtonGreen) {
									printMessage(baitUsed, baitToUse, " bait used.\nCaught.");
									Invoke(new Action(() => Refresh()));
									//helper.getFishWeight(locationTopLeftWeightScreenshot);
									Invoke(new Action(() => helper.tradeItemThenCloseClick(locationTradeFishButton, locationCloseShellDialogBox)));
									fishGetAway = false;

								}
								//fish got away
								else if (helper.GetPixelColor(locationCloseItGotAwayButton) == colorCloseItGotAwayButton) {
									printMessage(baitUsed, baitToUse, " bait used.\nFish got away. Sorry :(");
									Invoke(new Action(() => Refresh()));
									helper.fishGotAwayClick(locationCloseItGotAwayButton);
									fishGetAway = false;
								}
							}
							caughtFish = false;
						}
						// caught junk
						else if (helper.GetPixelColor(locationJunkItem) == colorJunkItem) {
							printMessage(baitUsed, baitToUse, " bait used.\nCaught junk");
							Invoke(new Action(() => Refresh()));
							Invoke(new Action(() => helper.tradeItemThenCloseClick(locationTradeFishButton, locationCloseShellDialogBox)));
							caughtFish = false;
						}
					}
					++i;
				}
			}
			printMessage(baitUsed, baitToUse, " bait used.\nFinished.");
			backgroundThread.CancelAsync();
			Program.ExitApplication(0);
		}



		private void cancelAutoModeBtn_Click(object sender, EventArgs e) {
			backgroundThread.CancelAsync();
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

		private void backgroundThreadGetTimes_DoWork(object sender, DoWorkEventArgs e) {
			BackgroundWorker worker = sender as BackgroundWorker;
			Invoke(new Action(() => helper.getTimesMessageBox(locationStartButton)));
			backgroundThreadGetTimes.CancelAsync();
		}

		private void saveScreenshotButton_Click(object sender, EventArgs e) {
			helper.GetScreenShot(true);
		}

		private void Form1_Load(object sender, EventArgs e) {
			Thread.Sleep(500);
			this.Hide();
		}
	}
}
