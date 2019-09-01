package dev.garlicbread;

import jdk.internal.util.xml.impl.Input;

import java.awt.*;
import java.awt.event.InputEvent;
import java.awt.event.KeyEvent;

public class Helper {
    Robot robot;

    public Helper() throws AWTException {
        robot = new Robot();
    }

    public Color GetPixelColor(Point p) {
        return robot.getPixelColor(p.x, p.y);
    }

    public boolean AreColorsSimilar(Color c1, Color c2, int tolerance) {
        return Math.abs(c1.getRed() - c2.getRed()) < tolerance &&
                Math.abs(c1.getGreen() - c2.getGreen()) < tolerance &&
                Math.abs(c1.getBlue() - c2.getBlue()) < tolerance;
    }

    public void moveCursor(Point p) {
        robot.mouseMove(p.x, p.y);
    }

    public void mouseClick(){
        int mask = InputEvent.BUTTON1_DOWN_MASK;
        robot.mousePress(mask);
        robot.mouseRelease(mask);
    }

    public void sendKey(int key){
        robot.keyPress(key);
        robot.keyRelease(key);
    }

    public void startCast(Point p) throws InterruptedException {
        checkIfUserIsOnStartScreen(p);
        Color fullRangeCastColor = new Color(23, 92, 237);
        Point rangeAreaPosition = new Point(p.x, p.y - 75);
        Thread.sleep(100);
        castRod(rangeAreaPosition, fullRangeCastColor);
    }

    public boolean checkIfUserIsOnStartScreen(Point p) {
        if (GetPixelColor(p).equals(new Color(139, 202, 24))) {
            moveCursor(p);
            mouseClick();
            return true;
        } else {
            moveCursor(p);
        }
        return false;
    }

    public void castRod(Point p, Color c) {
        sendSpaceBarEvent(p, c);
    }

    public void catchFish(Point p, Color c) {
        moveCursor(p);
        sendSpaceBarEvent(p, c);
        return;
    }

    private void sendSpaceBarEvent(Point p, Color c) {
        while (true) {
            Color locColor = GetPixelColor(p);
            if (c.getRed() == locColor.getRed() && c.getGreen() == locColor.getGreen() && c.getBlue() == locColor.getBlue()) {
                sendKey(KeyEvent.VK_SPACE);
                return;
            }
        }
    }

    public void tradeItemThenCloseClick(Point locTrade, Point locClose) throws InterruptedException {
        moveCursor(locTrade);
        mouseClick();
        Thread.sleep(500);
        moveCursor(locClose);
        mouseClick();
        Thread.sleep(500);
    }

    public void tradeItemThenCloseSpace() throws InterruptedException {
        sendKey(KeyEvent.VK_SPACE);
        Thread.sleep(500);
        sendKey(KeyEvent.VK_SPACE);
        Thread.sleep(500);
    }

    public void fishGotAwaySpace() throws InterruptedException {
        sendKey(KeyEvent.VK_SPACE);
        Thread.sleep(1000);
    }

    public void fishGotAwayClick(Point p) throws InterruptedException {
        moveCursor(p);
        mouseClick();
        Thread.sleep(1000);
    }
}
