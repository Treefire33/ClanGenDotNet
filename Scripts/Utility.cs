using Raylib_cs;
using System.Numerics;
using System.Text;
using static Raylib_cs.Raylib;
using ClanGenDotNet.Scripts.Game_Structure;

namespace ClanGenDotNet.Scripts
{
	public class Utility
	{
		public static Rectangle Scale(Rectangle oldRect)
		{
			float posX = oldRect.X / 1600 * Game.game.ScreenX;
			float posY = oldRect.Y / 1400 * Game.game.ScreenY;
			float scaleX = oldRect.Width / 1600 * Game.game.ScreenX;
			float scaleY = oldRect.Height / 1400 * Game.game.ScreenY;

			return new(posX, posY, scaleX, scaleY);
		}

		public static ClanGenRect UIScale(ClanGenRect rect)
		{
			rect.X = MathF.Floor(rect.X * ScreenSettings.ScreenScale);
			rect.Y = MathF.Floor(rect.Y * ScreenSettings.ScreenScale);
			rect.Width = rect.Width > 0 ? MathF.Floor(rect.Width * ScreenSettings.ScreenScale) : rect.Width;
			rect.Height = rect.Height > 0 ? MathF.Floor(rect.Height * ScreenSettings.ScreenScale) : rect.Height;

			return rect;
		}

		public static Vector2 UIScaleDimension(Vector2 point)
		{
			return new(
				point.X > 0 ? MathF.Floor(point.X * ScreenSettings.ScreenScale) : point.X,
				point.Y > 0 ? MathF.Floor(point.Y * ScreenSettings.ScreenScale) : point.Y
			);
		}

		public static Vector2 UIScaleOffset(Vector2 point)
		{
			return new(
				MathF.Floor(point.X * ScreenSettings.ScreenScale),
				MathF.Floor(point.Y * ScreenSettings.ScreenScale)
			);
		}

		public static float UIScaleValue(float value)
		{
			return MathF.Floor(value * ScreenSettings.ScreenScale);
		}

		//hold on, we don't blit here
		public static Vector2 UIScaleBlit(Vector2 point)
		{
			return new(
				MathF.Floor(point.X * ScreenSettings.ScreenScale + ScreenSettings.Offset.X),
				MathF.Floor(point.Y * ScreenSettings.ScreenScale + ScreenSettings.Offset.Y)
			);
		}

		public static Rectangle AddRectangles(Rectangle rect1, Rectangle rect2)
		{
			return new Rectangle(
				rect1.Position + rect2.Position,
				rect1.Size + rect2.Size
			);
		}

		//returns -1 if less than, 0 if equal, 1 if greater than
		/*public static bool CompareVector2(Vector2 vec1, Vector2 vec2, float distance)
		{

		}*/

		// stolen from: raylib-cs example: rectangle bounds
		public static void DrawTextBoxed(
			Font font,
			string text,
			Rectangle rec,
			float fontSize,
			float spacing,
			bool wordWrap,
			Color tint
		)
		{
			DrawTextBoxedSelectable(font, text, rec, fontSize, spacing, wordWrap, tint, 0, 0, Color.White, Color.White);
		}

		// Draw text using font inside rectangle limits with support for text selection
		static unsafe void DrawTextBoxedSelectable(
			Font font,
			string text,
			Rectangle rec,
			float fontSize,
			float spacing,
			bool wordWrap,
			Color tint,
			int selectStart,
			int selectLength,
			Color selectTint,
			Color selectBackTint
		)
		{
			int length = text.Length;

			// Offset between lines (on line break '\n')
			float textOffsetY = 0;

			// Offset X to next character to draw
			float textOffsetX = 0.0f;

			// Character rectangle scaling factor
			float scaleFactor = fontSize / (float)font.BaseSize;

			// Word/character wrapping mechanism variables
			bool shouldMeasure = wordWrap;

			// Index where to begin drawing (where a line begins)
			int startLine = -1;

			// Index where to stop drawing (where a line ends)
			int endLine = -1;

			// Holds last value of the character position
			int lastk = -1;

			using var textNative = new Utf8Buffer(text);

			for (int i = 0, k = 0; i < length; i++, k++)
			{
				// Get next codepoint from byte string and glyph index in font
				int codepointByteCount = 0;
				int codepoint = GetCodepoint(&textNative.AsPointer()[i], &codepointByteCount);
				int index = GetGlyphIndex(font, codepoint);

				// NOTE: Normally we exit the decoding sequence as soon as a bad byte is found (and return 0x3f)
				// but we need to draw all of the bad bytes using the '?' symbol moving one byte
				if (codepoint == 0x3f)
				{
					codepointByteCount = 1;
				}

				i += (codepointByteCount - 1);

				float glyphWidth = 0;
				if (codepoint != '\n')
				{
					glyphWidth = (font.Glyphs[index].AdvanceX == 0) ?
						font.Recs[index].Width * scaleFactor :
						font.Glyphs[index].AdvanceX * scaleFactor;

					if (i + 1 < length)
					{
						glyphWidth = glyphWidth + spacing;
					}
				}

				// NOTE: When wordWrap is ON we first measure how much of the text we can draw before going outside of
				// the rec container. We store this info in startLine and endLine, then we change states, draw the text
				// between those two variables and change states again and again recursively until the end of the text
				// (or until we get outside of the container). When wordWrap is OFF we don't need the measure state so
				// we go to the drawing state immediately and begin drawing on the next line before we can get outside
				// the container.
				if (shouldMeasure)
				{
					// TODO: There are multiple types of spaces in UNICODE, maybe it's a good idea to add support for
					// more. Ref: http://jkorpela.fi/chars/spaces.html
					if ((codepoint == ' ') || (codepoint == '\t') || (codepoint == '\n'))
					{
						endLine = i;
					}

					if ((textOffsetX + glyphWidth) > rec.Width)
					{
						endLine = (endLine < 1) ? i : endLine;
						if (i == endLine)
						{
							endLine -= codepointByteCount;
						}
						if ((startLine + codepointByteCount) == endLine)
						{
							endLine = (i - codepointByteCount);
						}

						shouldMeasure = !shouldMeasure;
					}
					else if ((i + 1) == length)
					{
						endLine = i;
						shouldMeasure = !shouldMeasure;
					}
					else if (codepoint == '\n')
					{
						shouldMeasure = !shouldMeasure;
					}

					if (!shouldMeasure)
					{
						textOffsetX = 0;
						i = startLine;
						glyphWidth = 0;

						// Save character position when we switch states
						int tmp = lastk;
						lastk = k - 1;
						k = tmp;
					}
				}
				else
				{
					if (codepoint == '\n')
					{
						if (!wordWrap)
						{
							textOffsetY += (font.BaseSize + font.BaseSize / 2) * scaleFactor;
							textOffsetX = 0;
						}
					}
					else
					{
						if (!wordWrap && ((textOffsetX + glyphWidth) > rec.Width))
						{
							textOffsetY += (font.BaseSize + font.BaseSize / 2) * scaleFactor;
							textOffsetX = 0;
						}

						// When text overflows rectangle height limit, just stop drawing
						if ((textOffsetY + font.BaseSize * scaleFactor) > rec.Height)
						{
							break;
						}

						// Draw selection background
						bool isGlyphSelected = false;
						if ((selectStart >= 0) && (k >= selectStart) && (k < (selectStart + selectLength)))
						{
							DrawRectangleRec(
								new Rectangle(
									rec.X + textOffsetX - 1,
									rec.Y + textOffsetY,
									glyphWidth,
									(float)font.BaseSize * scaleFactor
								),
								selectBackTint
							);
							isGlyphSelected = true;
						}

						// Draw current character glyph
						if ((codepoint != ' ') && (codepoint != '\t'))
						{
							DrawTextCodepoint(
								font,
								codepoint,
								new Vector2(rec.X + textOffsetX, rec.Y + textOffsetY),
								fontSize,
								isGlyphSelected ? selectTint : tint
							);
						}
					}

					if (wordWrap && (i == endLine))
					{
						textOffsetY += (font.BaseSize + font.BaseSize / 2) * scaleFactor;
						textOffsetX = 0;
						startLine = endLine;
						endLine = -1;
						glyphWidth = 0;
						selectStart += lastk - k;
						k = lastk;

						shouldMeasure = !shouldMeasure;
					}
				}

				if ((textOffsetX != 0) || (codepoint != ' '))
				{
					// avoid leading spaces
					textOffsetX += glyphWidth;
				}
			}
		}

		public static Vector2 GetVirutalMousePosition()
		{
			Vector2 mousePos = GetMousePosition();
			Vector2 virtualMouse = new(0);
			virtualMouse.X = (
				mousePos.X 
				- (GetScreenWidth() 
				- (ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale)) * 0.5f) 
				/ ScreenSettings.ScreenScale;
			virtualMouse.Y = (
				mousePos.Y
				- (GetScreenHeight()
				- (ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale)) * 0.5f)
				/ ScreenSettings.ScreenScale;
			virtualMouse = Vector2.Clamp(virtualMouse, new Vector2(0), ScreenSettings.GameScreenSize);

			return virtualMouse;
		}
		
		public static bool PointInArea(Vector2 point, Vector2 topLeft, Vector2 bottomRight)
		{
			if (
				point.X >= topLeft.X 
				&& point.X <= bottomRight.X 
				&& point.Y >= topLeft.Y 
				&& point.Y <= bottomRight.Y
			)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool PointInArea(Vector2 point, ClanGenRect area)
		{
			if (
				point.X >= area.TopLeft.X
				&& point.X <= area.BottomRight.X
				&& point.Y >= area.TopLeft.Y
				&& point.Y <= area.BottomRight.Y
			)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}