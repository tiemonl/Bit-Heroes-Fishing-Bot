package dev.garlicbread;

public class ButtonColors {
	private ButtonRgb startButtonGreen;
	private ButtonRgb castButtonBlue;
	private ButtonRgb colorCloseItGotAwayButton;
	private ButtonRgb colorTimerCaughtFishKong;
	private ButtonRgb colorTimerCaughtFishSteam;
	private ButtonRgb colorJunkItem;
	private ButtonRgb oneHundredCatchColor;
	private ButtonRgb fullRangeCastColor;

	public ButtonRgb getStartButtonGreen() { return startButtonGreen; }
	public void setStartButtonGreen(ButtonRgb value) { this.startButtonGreen = value; }

	public ButtonRgb getCastButtonBlue() { return castButtonBlue; }
	public void setCastButtonBlue(ButtonRgb value) { this.castButtonBlue = value; }

	public ButtonRgb getColorCloseItGotAwayButton() { return colorCloseItGotAwayButton; }
	public void setColorCloseItGotAwayButton(ButtonRgb value) { this.colorCloseItGotAwayButton = value; }

	public ButtonRgb getColorTimerCaughtFishKong() { return colorTimerCaughtFishKong; }
	public void setColorTimerCaughtFishKong(ButtonRgb value) { this.colorTimerCaughtFishKong = value; }

	public ButtonRgb getColorTimerCaughtFishSteam() { return colorTimerCaughtFishSteam; }
	public void setColorTimerCaughtFishSteam(ButtonRgb value) { this.colorTimerCaughtFishSteam = value; }

	public ButtonRgb getColorJunkItem() { return colorJunkItem; }
	public void setColorJunkItem(ButtonRgb value) { this.colorJunkItem = value; }

	public ButtonRgb getOneHundredCatchColor() { return oneHundredCatchColor; }
	public void setOneHundredCatchColor(ButtonRgb value) { this.oneHundredCatchColor = value; }

	public ButtonRgb getFullRangeCastColor() { return fullRangeCastColor; }
	public void setFullRangeCastColor(ButtonRgb value) { this.fullRangeCastColor = value; }
}

class ButtonRgb {
	private int red;
	private int green;
	private int blue;

	public int getRed() { return red; }
	public void setRed(int value) { this.red = value; }

	public int getGreen() { return green; }
	public void setGreen(int value) { this.green = value; }

	public int getBlue() { return blue; }
	public void setBlue(int value) { this.blue = value; }
}