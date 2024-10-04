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
		Right
	}

	public class UITextBox(ClanGenRect posScale, string text, int fontSize, TextAlignment alignment, Color textColour, UIManager manager) : UIElement(posScale, manager)
	{
		private readonly bool _fillRect = false;
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
			switch (align)
			{
				default:
				case TextAlignment.Left:
					return original;

				case TextAlignment.Center:
					Rectangle newRect = original;
					newRect.Position = new Vector2(
						RelativeRect.RelativeRect.Position.X 
						+ (RelativeRect.RelativeRect.Size.X / 2) 
						- (textSize.X / 2), 
						RelativeRect.RelativeRect.Position.Y 
						+ (RelativeRect.RelativeRect.Size.Y / 4));
					return newRect;

				case TextAlignment.Right:
					//Not implemented
					return original;
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
			if(RelativeRect.Height > 0)
			{
				DrawTextBoxed(
					NotoSansMedium,
					_text,
					AddRectangles(AlignTextRec(RelativeRect.RelativeRect, _text, alignment), new Rectangle(_padding / 2, -_padding)),
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
					AddRectangles(AlignTextRec(RelativeRect.RelativeRect, _text, alignment), new Rectangle(_padding / 2, -_padding)).Position,
					new(0, 0),
					0,
					_fontSize,
					0,
					Color.White
				);
				Vector2 textSize = MeasureTextEx(NotoSansMedium, _text, _fontSize, 0);
				RelativeRect.Height = textSize.Y;
			}
		}

		public static UITextBox UITextBoxFromStyle(ClanGenRect posScale, string text, TextBoxStyle style, UIManager manager)
		{
			return new UITextBox(posScale, text, style.FontSize, style.TextAlignment, style.Colour, manager);
		}
	}
}
