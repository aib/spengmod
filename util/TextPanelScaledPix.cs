using System;
using System.Text;
using Sandbox.ModAPI.Ingame;

public
class TextPanelScaledPix: IScaledPixelDrawer
{
	private readonly IMyTextPanel display;
	private readonly int forceScale;
	private readonly int width;
	private readonly int height;
	private readonly char[,] pix;

	private int scale;
	private int offsetX, offsetY;

	public TextPanelScaledPix(IMyTextPanel display, int forceScale = 0, int width = 171, int height = 171)
	{
		this.display = display;
		this.forceScale = forceScale;
		this.width = width;
		this.height = height;
		pix = new char[height, width];
		setScale(width, height);
	}

	public void setScale(int imageWidth, int imageHeight)
	{
		if (forceScale != 0) {
			scale = forceScale;
		} else {
			scale = Math.Min(width / imageWidth, height / imageHeight);
		}
		offsetX = (width - (imageWidth * scale)) / 2;
		offsetY = (height - (imageHeight * scale)) / 2;
	}

	public void clear(char color)
	{
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				pix[y, x] = color;
			}
		}
	}

	public void drawPixel(int x, int y, char color)
	{
		for (int dy = 0; dy < scale; dy++) {
			for (int dx = 0; dx < scale; dx++) {
				pix[offsetY + scale*y + dy, offsetX + scale*x + dx] = color;
			}
		}
	}

	public void update()
	{
		var sb = new StringBuilder();
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				sb.Append(pix[y, x]);
			}
			sb.Append("\n");
		}

		display.WriteText(sb.ToString());
	}
}
