package dev.garlicbread;

import java.awt.*;
import java.awt.event.InputEvent;
import java.awt.event.KeyEvent;

public class Helper {
	Robot robot;
	ButtonColors buttonColors;
	private Color fullRangeCastColor;
	private Color startButtonGreen;

	public Helper(Robot r, ButtonColors buttonColors) throws AWTException {
		robot = r;
		this.buttonColors = buttonColors;
		setUpColors();
	}

	private void setUpColors() {
		fullRangeCastColor = new Color(
				buttonColors.getFullRangeCastColor().getGreen(),
				buttonColors.getFullRangeCastColor().getRed(),
				buttonColors.getFullRangeCastColor().getBlue());
		startButtonGreen = new Color(
				buttonColors.getStartButtonGreen().getGreen(),
				buttonColors.getStartButtonGreen().getRed(),
				buttonColors.getStartButtonGreen().getBlue());
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

	public void mouseClick() {
		int mask = InputEvent.BUTTON1_DOWN_MASK;
		robot.mousePress(mask);
		robot.mouseRelease(mask);
	}

	public void sendKey(int key) {
		robot.keyPress(key);
		robot.keyRelease(key);
	}

	public void startCast(Point p) throws InterruptedException {
		checkIfUserIsOnStartScreen(p);
		Point rangeAreaPosition = new Point(p.x, p.y - 75);
		Thread.sleep(100);
		castRod(rangeAreaPosition, fullRangeCastColor);
	}

	public boolean checkIfUserIsOnStartScreen(Point p) {
		if (GetPixelColor(p).equals(startButtonGreen)) {
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
