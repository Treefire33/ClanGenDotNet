using Raylib_cs;
using System.Numerics;
using System.Text;
using static ClanGenDotNet.Scripts.Resources;
using static ClanGenDotNet.Scripts.Utility;
using static Raylib_cs.Raylib;

namespace ClanGenDotNet.Scripts.UI
{
	public enum TextAlignment
	{
		Left,
		Center,
		Right,
		VertCenter
	}

	public class UITextBox(ClanGenRect posScale, string text, int fontSize, TextAlignment alignment, Color textColour, UIManager manager, bool isMultiline = false) : UIElement(posScale, manager)
	{
		private readonly bool _fillRect = false;
		private readonly bool _isMultiline = isMultiline;
		//private unsafe sbyte* _text;
		private string _text = text;
		private Color _textColour = textColour;

		private Vector2 _padding = new(0, 0);

        /// <summary>
        /// Converts a string into a signed bytes pointer.
        /// </summary>
        /// <param name="Text">The text to convert</param>
        /// <returns>sbyte*</returns>
        private unsafe sbyte* StringToSBytes(string Text)
		{
			fixed (byte* p = Encoding.ASCII.GetBytes(Text))
			{
				return (sbyte*)p;
			}
		}

		public void SetPadding(Vector2 padding)
		{
			_padding = padding;
		}

		private unsafe Rectangle AlignTextRec(Rectangle original, string text, TextAlignment align)
		{
			Vector2 textSize = MeasureTextEx(NotoSansMedium, text, _fontSize, 0);
			Rectangle newRect = new();
			switch (align)
			{
				default:
				case TextAlignment.Left:
					return original;

				case TextAlignment.Center:
					newRect = original;
					newRect.Position = new Vector2(
						RelativeRect.RelativeRect.Position.X 
						+ (RelativeRect.RelativeRect.Size.X / 2) 
						- (textSize.X / 2), 
						RelativeRect.RelativeRect.Position.Y 
						+ (RelativeRect.RelativeRect.Size.Y / 4)
					);
					return newRect;

				case TextAlignment.Right:
					//Not implemented
					return original;

				case TextAlignment.VertCenter:
					newRect = original;
					newRect.Position = new Vector2(
						RelativeRect.RelativeRect.Position.X
						+ (RelativeRect.RelativeRect.Size.X / 2)
						- (textSize.X / 2),
						RelativeRect.RelativeRect.Position.Y
						+ (RelativeRect.RelativeRect.Size.Y / 2)
						- (textSize.Y / 2)
					);
					return newRect;
			}
		}

		/*/// <summary>
		/// Sets the text of the button.
		/// </summary>
		/// <param name="text">The text to set the button text to.</param>
		public unsafe void SetText(string text)
		{
			_text = StringToSBytes(text);
		}*/

		/// <summary>
		/// Sets the text of the button.
		/// </summary>
		/// <param name="text">The text to set the button text to.</param>
		public unsafe void SetText(string text)
		{
			_text = text;
		}

		private int _fontSize = fontSize;

		public unsafe override void Update()
		{
			base.Update();
			Vector2 textSize = MeasureTextEx(NotoSansMedium, _text, _fontSize, 0);
			if (_isMultiline)
			{
				float positionOffset = 0f;
				foreach (string line in _text.Split('\n'))
				{
					textSize = MeasureTextEx(NotoSansMedium, line, _fontSize, 0);
					DrawTextPro(
						NotoSansMedium,
						line,
						AddRectangles(
							AlignTextRec(RelativeRect.RelativeRect, line, alignment),
							new Rectangle(_padding / 2, -_padding)
						).Position + new Vector2(0, positionOffset),
						Vector2.Zero,
						0,
						_fontSize,
						0,
						Color.White
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
						AlignTextRec(RelativeRect.RelativeRect, _text, alignment),
						new Rectangle(_padding / 2, -_padding)
					),
					_fontSize,
					0,
					true,
					_textColour
				);
			}
			else
			{
				DrawTextPro(
					NotoSansMedium,
					_text,
					AddRectangles(
						AlignTextRec(RelativeRect.RelativeRect, _text, alignment),
						new Rectangle(_padding / 2, -_padding)
					).Position,
					new(0, 0),
					0,
					_fontSize,
					0,
					Color.White
				);
				RelativeRect.Height = textSize.Y;
			}
		}

		public static UITextBox UITextBoxFromStyle(ClanGenRect posScale, string text, TextBoxStyle style, UIManager manager)
		{
			return new UITextBox(posScale, text, style.FontSize, style.TextAlignment, style.Colour, manager);
		}
	}
}
