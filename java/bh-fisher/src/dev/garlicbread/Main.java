package dev.garlicbread;

import java.awt.*;
import java.awt.image.BufferedImage;

public class Main {

    public static Helper helper;

    private static boolean steam;
    private static int rodType;
    private static int bait = 10;
    private static Point locationStartButton;
    private static Point locationCloseShellDialogBox;
    private static Point locationTradeFishButton;
    private static Point locationCloseItGotAwayButton;
    private static Point locationTimerCaughtFish;
    private static Point locationJunkItem;
    private static Point locationTopLeftWeightScreenshot;
    private static Point locationBottomRightWeightScreenshot;
    private static Point location100Position;

    private static Color startButtonGreen = new Color(139, 202, 24);
    private static Color castButtonBlue = new Color(31, 153, 197);
    private static Color colorCloseItGotAwayButton = new Color(31, 153, 197);
    private static Color colorTimerCaughtFishKong = new Color(56, 255, 56);
    private static Color colorTimerCaughtFishSteam = new Color(59, 255, 59);
    private static Color colorJunkItem = new Color(255, 255, 255);
    private static Color oneHundredCatchColor = new Color(71, 255, 3);

    private static Robot r;

    public static void main(String[] args) throws AWTException, InterruptedException {
        r = new Robot();
        helper = new Helper();
        Dimension screenDim = Toolkit.getDefaultToolkit().getScreenSize();

        BufferedImage screenCapture = r.createScreenCapture(new Rectangle(screenDim));
        setLocations(screenCapture);
        startFishing();

    }

    public static Point FindColor(Color c, BufferedImage screen) {
        for (int x = 0; x < screen.getWidth(); ++x)
            for (int y = 0; y < screen.getHeight(); ++y) {
                int screenRGB = screen.getRGB(x, y);
                Color color = new Color(screenRGB);
                if (c.equals(color)) {
                    System.out.println("found color!");
                    helper.moveCursor(new Point(x + 30, y));
                    helper.mouseClick();
                    return new Point(x + 30, y);
                }
            }
        System.out.println("did not find start button");
        System.exit(1);
        return null;
    }

    public static void setLocations(BufferedImage screen) {
        locationStartButton = FindColor(startButtonGreen, screen);
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
            ++baitUsed;
            helper.startCast(locationStartButton);
            helper.moveCursor(locationTimerCaughtFish);
            while (caughtFish) {
                //performs cast
                Color color = helper.GetPixelColor(locationTimerCaughtFish);
                //if (color == colorTimerCaughtFishKong || color == colorTimerCaughtFishSteam) {
                if (helper.AreColorsSimilar(color, colorTimerCaughtFishKong, 20)) {
                    helper.catchFish(location100Position, oneHundredCatchColor);
                    Thread.sleep(5000);
                    while (fishGetAway) {
                        if (helper.GetPixelColor(locationTradeFishButton).equals(startButtonGreen)) {
                            //helper.getFishWeight(locationTopLeftWeightScreenshot);
                            helper.tradeItemThenCloseClick(locationTradeFishButton, locationCloseShellDialogBox);
                            fishGetAway = false;

                        }
                        //fish got away
                        else if (helper.GetPixelColor(locationCloseItGotAwayButton).equals(colorCloseItGotAwayButton)) {
                            helper.fishGotAwayClick(locationCloseItGotAwayButton);
                            fishGetAway = false;
                        }
                    }
                    caughtFish = false;
                }
                // caught junk
                else if (helper.GetPixelColor(locationJunkItem).equals(colorJunkItem)) {
                    helper.tradeItemThenCloseClick(locationTradeFishButton, locationCloseShellDialogBox);
                    caughtFish = false;
                }
            }
            ++i;

        }
    }
}
