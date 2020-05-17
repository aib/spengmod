using System;
using System.Text;
using Sandbox.ModAPI.Ingame;

public
class TextPanelTerminal
{
	private readonly IMyTextPanel display;
	private int maxLines;

	public TextPanelTerminal(IMyTextPanel display, int lines = 1)
	{
		this.display = display;
		setLines(lines);
	}

	public void setLines(int lines)
	{
		this.maxLines = lines;
		update();
	}

	public void update()
	{
		var sb = new StringBuilder();
		var lines = display.GetText().Split('\n');

		for (int i = Math.Max(lines.Length - maxLines, 0); i < lines.Length; i++) {
			sb.AppendLine(lines[i]);
		}

		display.WriteText(sb);
	}

	public void addLine(string line)
	{
		display.WriteText(line, true);
		update();
	}
}
