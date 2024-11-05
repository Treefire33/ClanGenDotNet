using System.Text;
using static ClanGenDotNet.Scripts.Resources;
using static ClanGenDotNet.Scripts.Utility;

namespace ClanGenDotNet.Scripts.UI;

public enum TextAlignment
{
	Left,
	Center,
	Right,
	VertCenter
}

public class UITextBox(
	ClanGenRect posScale, 
	string text, 
	int fontSize, 
	TextAlignment alignment, 
	Color textColour, 
	UIManager manager, 
	bool isMultiline = false
) : UIElement(posScale, manager)
{
	private readonly bool _fillRect = false;
	private readonly bool _isMultiline = isMultiline;
	private string _text = text;
	private Color _textColour = textColour;

	private Vector2 _padding = new(0, 0);

	public void SetPadding(Vector2 padding)
	{
		_padding = padding;
	}

	private unsafe Rectangle AlignTextRec(ClanGenRect original, string text, TextAlignment align)
	{
		Vector2 textSize = MeasureTextEx(NotoSansMedium, text, _fontSize, 0);
		_ = new Rectangle();
		ClanGenRect newRect;
		switch (align)
		{
			default:
			case TextAlignment.Left:
				return original;

			case TextAlignment.Center:
				newRect = original;
				newRect.Position = new Vector2(
					RelativeRect.Position.X
					+ (RelativeRect.Size.X / 2)
					- (textSize.X / 2),
					RelativeRect.Position.Y
					+ (RelativeRect.Size.Y / 4)
				);
				return newRect;

			case TextAlignment.Right:
				//Not implemented
				return original;

			case TextAlignment.VertCenter:
				newRect = original;
				newRect.Position = new Vector2(
					RelativeRect.Position.X
					+ (RelativeRect.Size.X / 2)
					- (textSize.X / 2),
					RelativeRect.Position.Y
					+ (RelativeRect.Size.Y / 2)
					- (textSize.Y / 2)
				);
				return newRect;
		}
	}

	/// <summary>
	/// Sets the text of the button.
	/// </summary>
	/// <param name="text">The text to set the button text to.</param>
	public unsafe void SetText(string text)
	{
		_text = text;
	}

	private readonly int _fontSize = fontSize;

	public override unsafe void Update()
	{
		base.Update();
		Vector2 textSize = MeasureTextEx(NotoSansMedium, _text, _fontSize, 0);
		if (_isMultiline)
		{
			float positionOffset = 0f;
			foreach (string line in _text.Split('\n'))
			{
				textSize = MeasureTextEx(NotoSansMedium, line, _fontSize, 0);
				var addedRectangles = AddRectangles(
					AlignTextRec(RelativeRect, line, alignment),
					new Rectangle(
						_padding.X / 2,
						_padding.Y / 2,
						-_padding.X,
						-_padding.Y
					)
				);
				DrawTextPro(
					NotoSansMedium,
					line,
					new Vector2(addedRectangles.x, addedRectangles.y) + new Vector2(0, positionOffset),
					Vector2.Zero,
					0,
					_fontSize,
					0,
					WHITE
				);
				positionOffset += textSize.Y;
			}
		}
		else if (RelativeRect.Height > 0 && RelativeRect.Width > 0)
		{
			DrawTextBoxed(
				NotoSansMedium,
				_text,
				AddRectangles(
					AlignTextRec(RelativeRect, _text, alignment),
					new Rectangle(
						_padding.X / 2,
						_padding.Y / 2,
						-_padding.X,
						-_padding.Y
					)
				),
				_fontSize,
				0,
				true,
				_textColour
			);
		}
		else
		{
			var addedRectangles = AddRectangles(
				AlignTextRec(RelativeRect, _text, alignment),
				new Rectangle(
					_padding.X / 2,
					_padding.Y / 2,
					-_padding.X,
					-_padding.Y
				)
			);
			DrawTextPro(
				NotoSansMedium,
				_text,
				new Vector2(addedRectangles.x, addedRectangles.y),
				Vector2.Zero,
				0,
				_fontSize,
				0,
				WHITE
			);
			RelativeRect.Height = textSize.Y;
		}
	}

	public static UITextBox UITextBoxFromStyle(ClanGenRect posScale, string text, TextBoxStyle style, UIManager manager)
	{
		return new UITextBox(posScale, text, style.FontSize, style.TextAlignment, style.Colour, manager);
	}
}
