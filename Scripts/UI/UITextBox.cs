using ClanGenDotNet.Scripts.UI.Theming;
using System.Text;
using static ClanGenDotNet.Scripts.Resources;
using static ClanGenDotNet.Scripts.Utility;

namespace ClanGenDotNet.Scripts.UI;

public enum HorizontalTextAlignment
{
	Left,
	Center,
	Right
}

public enum VerticalTextAlignment
{
	Top,
	Center,
	Bottom,
}

public class UITextBox : UIElement, IUIElement
{
	private string _text;
	private readonly bool _isMultiline;

	//Theme Attributes
	private Color _textColour;
	private Font _font;
	private float _fontSize;
	private HorizontalTextAlignment _horizontalAlignment;
	private VerticalTextAlignment _verticalAlignment;

	private Vector2 _padding = new(0, 0);

	public UITextBox(
		ClanGenRect posScale,
		string text,
		string object_id,
		UIManager manager,
		bool isMultiline = false
	) : base(posScale, manager, GetTextBoxTheme(object_id))
	{
		_text = text;
		_isMultiline = isMultiline;
		ThemeElement();
	}

	public UITextBox(
		ClanGenRect posScale,
		string text,
		ObjectID object_id,
		UIManager manager,
		bool isMultiline = false
	) : base(posScale, manager, object_id)
	{
		_text = text;
		_isMultiline = isMultiline;
		ThemeElement();
	}

	public HorizontalTextAlignment GetHorizontalAlignmentFromString(string alignment)
	{
		return alignment switch
		{
			"left" => HorizontalTextAlignment.Left,
			"center" => HorizontalTextAlignment.Center,
			"right" => HorizontalTextAlignment.Right,
			_ => HorizontalTextAlignment.Left
		};
	}

	public VerticalTextAlignment GetVerticalAlignmentFromString(string alignment)
	{
		return alignment switch
		{
			"top" => VerticalTextAlignment.Top,
			"center" => VerticalTextAlignment.Center,
			"bottom" => VerticalTextAlignment.Bottom,
			_ => VerticalTextAlignment.Top
		};
	}

	public override void ThemeElement()
	{
		base.ThemeElement();
		_font = Theme.Font.Item1; //Font resource
		_fontSize = Theme.Font.Item2 + 5; //Font size
		_textColour = Theme.Colours["text"];
		if (Theme.Miscellaneous.TryGetValue("text_horiz_alignment", out string? horizAlignment))
		{
			_horizontalAlignment = GetHorizontalAlignmentFromString(horizAlignment!);
		}
		if (Theme.Miscellaneous.TryGetValue("text_vert_alignment", out string? vertAlignment))
		{
			_verticalAlignment = GetVerticalAlignmentFromString(vertAlignment!);
		}
	}

	public void SetPadding(Vector2 padding)
	{
		_padding = padding;
	}

	private Rectangle AlignTextRec(
		ClanGenRect original,
		HorizontalTextAlignment horizontalAlignment = HorizontalTextAlignment.Left,
		VerticalTextAlignment verticalAlignment = VerticalTextAlignment.Top
	)
	{
		return AlignTextRec(original, _text, horizontalAlignment, verticalAlignment);
	}

	private Rectangle AlignTextRec(
		ClanGenRect original,
		string text,
		HorizontalTextAlignment horizontalAlignment = HorizontalTextAlignment.Left,
		VerticalTextAlignment verticalAlignment = VerticalTextAlignment.Top
	)
	{
		Vector2 textSize = MeasureTextEx(NotoSansMedium, text, _fontSize, 0);
		ClanGenRect newRect = original;
		switch (horizontalAlignment)
		{
			default:
			case HorizontalTextAlignment.Left:
				break;

			case HorizontalTextAlignment.Center:
				newRect.X = RelativeRect.Position.X
					+ (RelativeRect.Size.X / 2)
					- (textSize.X / 2);
				break;

			case HorizontalTextAlignment.Right:
				//Not implemented
				break;
		}

		switch (verticalAlignment)
		{
			default:
			case VerticalTextAlignment.Top:
				break;

			case VerticalTextAlignment.Center:
				newRect.Y = RelativeRect.Position.Y
					+ (RelativeRect.Size.Y / 2)
					- (textSize.Y / 2);
				break;

			case VerticalTextAlignment.Bottom:
				//Not implemented
				break;
		}

		return newRect;
	}

	/// <summary>
	/// Sets the text of the button.
	/// </summary>
	/// <param name="text">The text to set the button text to.</param>
	public unsafe void SetText(string text)
	{
		_text = text;
	}

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
					AlignTextRec(RelativeRect, line, _horizontalAlignment, _verticalAlignment),
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
					_textColour
				);
				positionOffset += textSize.Y;
			}
		}
		else if (RelativeRect.Height > 0)
		{
			DrawTextBoxed(
				NotoSansMedium,
				_text,
				AddRectangles(
					AlignTextRec(RelativeRect, _horizontalAlignment, _verticalAlignment),
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
				AlignTextRec(RelativeRect, _horizontalAlignment, _verticalAlignment),
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
				new Vector2(0),
				0,
				_fontSize,
				0,
				WHITE
			);
			RelativeRect.Height = textSize.Y;
		}
	}
}
