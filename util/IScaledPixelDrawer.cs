public
interface IScaledPixelDrawer
{
	void setScale(int width, int height);
	void clear(char color);
	void drawPixel(int x, int y, char color);
}
