package dev.garlicbread;

import com.google.gson.Gson;

import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.util.logging.Level;
import java.util.logging.Logger;

public class Main {

	public static Helper helper;
	public static Logger logger;
	public static ButtonColors buttonColors;
	public static Gson gson = new Gson();

	private static boolean steam;
	private static int rodType;
	private static int bait;

	private static Point locationStartButton;
	private static Point locationCloseShellDialogBox;
	private static Point locationTradeFishButton;
	private static Point locationCloseItGotAwayButton;
	private static Point locationTimerCaughtFish;
	private static Point locationJunkItem;
	private static Point locationTopLeftWeightScreenshot;
	private static Point locationBottomRightWeightScreenshot;
	private static Point location100Position;

	private static Color startButtonGreen;
	private static Color castButtonBlue;
	private static Color colorCloseItGotAwayButton;
	private static Color colorTimerCaughtFishKong;
	private static Color colorTimerCaughtFishSteam;
	private static Color colorJunkItem;
	private static Color oneHundredCatchColor;

	private static Robot r;

	public static void main(String[] args) throws AWTException, InterruptedException, FileNotFoundException {
		setUpColors();
		bait = Integer.parseInt(args[0]);
		r = new Robot();
		helper = new Helper(r, buttonColors);
		logger = Logger.getLogger(Logger.GLOBAL_LOGGER_NAME);

		Dimension screenDim = Toolkit.getDefaultToolkit().getScreenSize();

		BufferedImage screenCapture = r.createScreenCapture(new Rectangle(screenDim));
		setStartLocation(screenCapture);
		startFishing();

	}

	private static void setUpColors() throws FileNotFoundException {
		buttonColors = gson.fromJson(new FileReader("buttonColors.json"), ButtonColors.class);

		startButtonGreen =
				new Color(buttonColors.getStartButtonGreen().getRed(),
						buttonColors.getStartButtonGreen().getGreen(),
						buttonColors.getStartButtonGreen().getBlue());
		castButtonBlue =
				new Color(buttonColors.getCastButtonBlue().getRed(),
						buttonColors.getCastButtonBlue().getGreen(),
						buttonColors.getCastButtonBlue().getBlue());
		colorCloseItGotAwayButton =
				new Color(buttonColors.getColorCloseItGotAwayButton().getRed(),
						buttonColors.getColorCloseItGotAwayButton().getGreen(),
						buttonColors.getColorCloseItGotAwayButton().getBlue());
		colorTimerCaughtFishKong =
				new Color(buttonColors.getColorTimerCaughtFishKong().getRed(),
						buttonColors.getColorTimerCaughtFishKong().getGreen(),
						buttonColors.getColorTimerCaughtFishKong().getBlue());
		colorTimerCaughtFishSteam =
				new Color(buttonColors.getColorTimerCaughtFishSteam().getRed(),
						buttonColors.getColorTimerCaughtFishSteam().getGreen(),
						buttonColors.getColorTimerCaughtFishSteam().getBlue());
		colorJunkItem =
				new Color(buttonColors.getColorJunkItem().getRed(),
						buttonColors.getColorJunkItem().getGreen(),
						buttonColors.getColorJunkItem().getBlue());
		oneHundredCatchColor =
				new Color(buttonColors.getOneHundredCatchColor().getRed(),
						buttonColors.getOneHundredCatchColor().getGreen(),
						buttonColors.getOneHundredCatchColor().getBlue());
	}

	public static Point FindColor(Color c, BufferedImage screen) {
		for (int x = 0; x < screen.getWidth(); ++x)
			for (int y = 0; y < screen.getHeight(); ++y) {
				int screenRGB = screen.getRGB(x, y);
				Color color = new Color(screenRGB);
				if (c.equals(color)) {
					logger.log(Level.INFO, "Found starting button.");
					helper.moveCursor(new Point(x - 60, y));
					helper.mouseClick();
					return new Point(x + 30, y);
				}
			}
		logger.log(Level.SEVERE, "Start button not found. Exit code 1");
		return null;
	}

	public static void setStartLocation(BufferedImage screen) {
		Point location = FindColor(startButtonGreen, screen);
		if (location == null) {
			System.exit(1);
		} else {
			locationStartButton = location;
			setLocations();
		}
	}

	public static void setLocations() {
		locationTradeFishButton = new Point(locationStartButton.x, locationStartButton.y - 15);
		locationCloseShellDialogBox = new Point(locationStartButton.x + 265, locationStartButton.y - 325);
		locationCloseItGotAwayButton = new Point(locationStartButton.x + 30, locationStartButton.y - 100);
		locationTimerCaughtFish = new Point(locationStartButton.x - 200, locationStartButton.y - 70);
		locationJunkItem = new Point(locationStartButton.x + 100, locationStartButton.y - 155);
		locationTopLeftWeightScreenshot = new Point(locationStartButton.x - 25, locationStartButton.y - 130);
		locationBottomRightWeightScreenshot = new Point(locationStartButton.x + 160, locationStartButton.y - 50);
		location100Position = new Point(locationStartButton.x + 373, locationStartButton.y - 81);
	}

	public static void startFishing() throws InterruptedException {
		int baitToUse = bait;
		int i = 0;
		int baitUsed = 0;
		while (i < baitToUse) {
			boolean caughtFish = true;
			boolean fishGetAway = true;
			logMessage(Level.INFO, baitUsed, baitToUse, "Performing cast.");
			++baitUsed;
			Thread.sleep(1000);
			helper.startCast(locationStartButton);
			helper.moveCursor(locationTimerCaughtFish);
			while (caughtFish) {
				Color color = helper.GetPixelColor(locationTimerCaughtFish);
				if (helper.AreColorsSimilar(color, colorTimerCaughtFishKong, 20)) {
					logMessage(Level.INFO, baitUsed, baitToUse, "Performing catch.");
					helper.catchFish(location100Position, oneHundredCatchColor);
					Thread.sleep(5000);
					while (fishGetAway) {
						if (helper.GetPixelColor(locationTradeFishButton).equals(startButtonGreen)) {
							logMessage(Level.INFO, baitUsed, baitToUse, "Fish caught.");
							helper.tradeItemThenCloseClick(locationTradeFishButton, locationCloseShellDialogBox);
							fishGetAway = false;
						} else if (helper.GetPixelColor(locationCloseItGotAwayButton).equals(colorCloseItGotAwayButton)) {
							logMessage(Level.WARNING, baitUsed, baitToUse, "Fish got away. Sorry :(");
							helper.fishGotAwayClick(locationCloseItGotAwayButton);
							fishGetAway = false;
						}
					}
					caughtFish = false;
				}
				// caught junk
				else if (helper.GetPixelColor(locationJunkItem).equals(colorJunkItem)) {
					logMessage(Level.INFO, baitUsed, baitToUse, "Caught junk");
					helper.tradeItemThenCloseClick(locationTradeFishButton, locationCloseShellDialogBox);
					caughtFish = false;
				}
			}
			++i;
		}
		logMessage(Level.INFO, baitUsed, baitToUse, "Finished.");
	}

	public static void logMessage(Level level, int baitUsed, int baitToUse, String msg) {
		logger.log(level, baitUsed + "/" + baitToUse + " bait used. | " + msg);
	}
}