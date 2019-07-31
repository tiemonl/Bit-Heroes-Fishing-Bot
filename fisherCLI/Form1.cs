using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fisherCLI {
	public partial class Form1 : Form {
		#region Declarations
		
		private int bait;
		private static bool steam;
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

			setRod();
			findCastCatchLocation();
			startFishing();
		}

		/// <summary>
		/// This determines which radio button was clicked.
		/// Each rod has a different timing.
		/// The timing is the time it takes for the casting bar to go from its max value, down to its lowest point and back up to its max value.
		/// I use this method of fishing as it has netted better results over clicking cast as soon as the program detects the casting bar at its max value.
		/// </summary>
		private void setRod() {
			helper = new MethodHelper(steam, 1000); //TODO: change ctor to not use rodType
		}


		private void findCastCatchLocation() {
			locationStartButton = helper.FindColor(startButtonGreen);
			locationStartButton = helper.getScreenLocationPoint(locationStartButton);
			locationTradeFishButton = new Point(locationStartButton.X, locationStartButton.Y - 15);
			locationCloseShellDialogBox = new Point(locationStartButton.X + 265, locationStartButton.Y - 325);
			locationCloseItGotAwayButton = new Point(locationStartButton.X + 30, locationStartButton.Y - 100);
			locationTimerCaughtFish = new Point(locationStartButton.X - 200, locationStartButton.Y - 70);
			locationJunkItem = new Point(locationStartButton.X + 100, locationStartButton.Y - 155);
			locationTopLeftWeightScreenshot = new Point(locationStartButton.X - 25, locationStartButton.Y - 130);
			locationBottomRightWeightScreenshot = new Point(locationStartButton.X + 160, locationStartButton.Y - 50);
			location100Position = new Point(locationStartButton.X + 373, locationStartButton.Y - 81);
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
					++baitUsed;
					Invoke(new Action(() => Refresh()));
					Invoke(new Action(() => helper.startCast(locationStartButton)));
					Invoke(new Action(() => Cursor.Position = locationTimerCaughtFish));
					while (caughtFish) {
						if (worker.CancellationPending == true) {
							e.Cancel = true;
							break;
						}
						Invoke(new Action(() => Refresh()));
						//performs cast
						Color color = helper.GetPixelColor(locationTimerCaughtFish);
						//if (color == colorTimerCaughtFishKong || color == colorTimerCaughtFishSteam) {
						if (helper.AreColorsSimilar(color, colorTimerCaughtFishKong, 20)) {
							if (worker.CancellationPending == true) {
								e.Cancel = true;
								break;
							}
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
									Invoke(new Action(() => Refresh()));
									//helper.getFishWeight(locationTopLeftWeightScreenshot);
									Invoke(new Action(() => helper.tradeItemThenCloseClick(locationTradeFishButton, locationCloseShellDialogBox)));
									fishGetAway = false;

								}
								//fish got away
								else if (helper.GetPixelColor(locationCloseItGotAwayButton) == colorCloseItGotAwayButton) {
									Invoke(new Action(() => Refresh()));
									helper.fishGotAwayClick(locationCloseItGotAwayButton);
									fishGetAway = false;
								}
							}
							caughtFish = false;
						}
						// caught junk
						else if (helper.GetPixelColor(locationJunkItem) == colorJunkItem) {
							Invoke(new Action(() => Refresh()));
							Invoke(new Action(() => helper.tradeItemThenCloseClick(locationTradeFishButton, locationCloseShellDialogBox)));
							caughtFish = false;
						}
					}
					++i;
				}
			}
			backgroundThread.CancelAsync();
		}
	}
}

