using ClanGenDotNet.Scripts.UI.Interfaces;
using System.Text;
using static ClanGenDotNet.Scripts.Resources;
using static ClanGenDotNet.Scripts.Utility;

namespace ClanGenDotNet.Scripts.UI;
public class UITooltip : UIElement, IUIElement
{
	private string _tooltipText;
	private Vector2 _padding = new(5, 5);

	private readonly UIElement _parentElement;

	public UITooltip(string text, UIElement parentElement, UIManager manager) : base(new ClanGenRect(), manager)
	{
		if (text == null)
		{
			return;
		}
		_parentElement = parentElement;
		_tooltipText = text;
		CalculateSize();
	}

	private void CalculateSize()
	{
		Vector2 textSize = MeasureTextEx(NotoSansMedium, FormatTooltip(_tooltipText), 15, 0);
		//_padding = textSize / 8;
		RelativeRect = new ClanGenRect(
			RelativeRect.Position,
			250,
			textSize.Y * 2
		);
	}

	//Returns a string that doesn't make the tooltip bigger than the screen.
	//Max tooltip width is 200, no cap on height
	private string FormatTooltip(string text)
	{
		StringBuilder currentString = new();
		int line = 0;
		for (int i = 0; i < text.Length; i++)
		{
			_ = currentString.Append(text[i]);
			string currentLine = currentString.ToString().Split('\n')[line];
			if (MeasureTextEx(NotoSansMedium, currentLine, 20, 0).X >= 250)
			{
				_ = currentString.Append('\n');
				line++;
			}
		}
		return currentString.ToString();
	}

	public void SetTooltipText(string text)
	{
		_tooltipText = text;
	}

	public override void Update()
	{
		if (_parentElement.Hovered)
		{
			RelativeRect.Position = GetVirutalMousePosition() - new Vector2(0, RelativeRect.Height + _padding.Y);
			DrawRectangleRounded(
				RelativeRect.AsRaylibRect(),
				0.3f,
				5,
				LightModeColour
			);
			DrawRectangleRoundedLines(
				RelativeRect.AsRaylibRect(),
				0.3f,
				5,
				2,
				DarkModeColour
			);
			DrawTextBoxed(
				NotoSansMedium,
				_tooltipText,
				AddRectangles(
					RelativeRect.AsRaylibRect(),
					new Rectangle(
						_padding.X / 2, 
						_padding.Y / 2,
						-_padding.X,
						-_padding.Y
					)
				),
				18,
				0,
				true,
				BLACK
			);
		}
	}
}
