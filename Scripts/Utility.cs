using ClanGenDotNet.Scripts.Cats;
using ClanGenDotNet.Scripts.Game_Structure;
using System.Text;

namespace ClanGenDotNet.Scripts;

public class Utility
{
	public static Rectangle Scale(Rectangle oldRect)
	{
		float posX = oldRect.X / 1600 * Game.game.ScreenX;
		float posY = oldRect.Y / 1400 * Game.game.ScreenY;
		float scaleX = oldRect.width / 1600 * Game.game.ScreenX;
		float scaleY = oldRect.height / 1400 * Game.game.ScreenY;

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
			MathF.Floor((point.X * ScreenSettings.ScreenScale) + ScreenSettings.Offset.X),
			MathF.Floor((point.Y * ScreenSettings.ScreenScale) + ScreenSettings.Offset.Y)
		);
	}

	public static Color GetThemeColour()
	{
		return (bool)game.Settings["dark mode"]! == true
			? Resources.DarkModeColour
			: Resources.LightModeColour;
	}

	public static Rectangle AddRectangles(Rectangle rect1, Rectangle rect2)
	{
		return new Rectangle(
			rect1.X + rect2.X,
			rect1.Y + rect2.Y,
			rect1.width + rect2.width,
			rect1.height + rect2.height
		);
	}

	public readonly static Random Rand = new(10538);

	public static int GetRandBits(int size)
	{
		if (size < 4) { size = 4; }
		var tempBytes = new byte[size];
		Rand.NextBytes(tempBytes);
		return BitConverter.ToInt32(tempBytes, 0);
	}

	public static unsafe Image GenerateSprite(
		Cat cat, 
		string? lifeState = null, 
		bool hideScars = false,
		bool hideAccessories = false,
		bool alwaysLiving = false,
		bool noNotWorkingLineart = false
	)
	{
		string age;
		bool dead;
		if (lifeState is not null)
		{
			age = lifeState;
		}
		else
		{
			age = cat.Age.ToString().ToLower();
		}

		if (alwaysLiving)
		{
			dead = false;
		}
		else
		{
			dead = cat.Dead;
		}

		string catSprite;
		string[] containedAges = ["kitten", "adolescent"];
		if (
			!noNotWorkingLineart
			&& cat.NotWorking()
			&& age != "newborn"
			&& game.Config.CatSprites.SickSprites
		)
		{
			if (containedAges.Contains(age))
			{
				catSprite = "19";
			}
			else
			{
				catSprite = "18";
			}
		}
		else if (cat.Pelt.Paralyzed && age != "newborn")
		{
			if (containedAges.Contains(age))
			{
				catSprite = "17";
			}
			else
			{
				if (cat.Pelt.Length == "long")
				{
					catSprite = "16";
				}
				else
				{
					catSprite = "15";
				}
			}
		}
		else
		{
			if (age == "elder" && !game.Config.Fun.AllCatsAreNewborn)
			{
				age = "senior";
			}

			if (game.Config.Fun.AllCatsAreNewborn)
			{
				catSprite = cat.Pelt.CatSprites["newborn"].ToString();
			}
			else
			{
				catSprite = cat.Pelt.CatSprites[age].ToString();
			}
		}

		Image newSprite = ImageCopy(Sprites.CatSprites[
			cat.Pelt.GetSpritesName() + cat.Pelt.Colour + catSprite
		]);

		for (int i = 0; i < newSprite.width; i++)
		{
			for (int j = 0; j < newSprite.height; j++)
			{
				ImageDrawPixel(
					&newSprite,
					i, j,
					new Color(0, 0, 0, 0)
				);
			}
		}

		try
		{
			if (cat.Pelt.Name != "Tortie" || cat.Pelt.Name != "Calico")
			{
				Image sprite = Sprites.CatSprites[
					cat.Pelt.GetSpritesName() + cat.Pelt.Colour + catSprite
				];
				ImageDraw(
					&newSprite,
					sprite,
					new Rectangle(0, 0, sprite.width, sprite.height),
					new Rectangle(0, 0, sprite.width, sprite.height),
					WHITE
				);
				ExportImage(newSprite, ".\\example.png");
			}
			else
			{
				//Draw base coat
				Image baseCoat = Sprites.CatSprites[
					cat.Pelt.TortieBase + cat.Pelt.Colour + catSprite
				];
				ImageDraw(
					&newSprite,
					baseCoat,
					new Rectangle(0, 0, baseCoat.width, baseCoat.height),
					new Rectangle(0, 0, newSprite.width, newSprite.height),
					WHITE
				);

				string tortiePattern = "SingleColour";
				if (cat.Pelt.TortiePattern == "Single"){ tortiePattern = "SingleColour"; }
				else { tortiePattern = cat.Pelt.TortiePattern!; }

				Image patches = ImageCopy(Sprites.CatSprites[
					tortiePattern + cat.Pelt.TortieColour + catSprite
				]);
				Image mask = Sprites.CatSprites[
					"tortiemask" + cat.Pelt.Pattern + catSprite
				];
				ImageAlphaMask(
					&patches,
					mask
				);
				ImageAlphaPremultiply(&patches);

				ImageDraw(
					&newSprite,
					patches,
					new Rectangle(0, 0, baseCoat.width, baseCoat.height),
					new Rectangle(0, 0, newSprite.width, newSprite.height),
					WHITE
				);
				ExportImage(newSprite, ".\\example.png");
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error updating sprite.\n{e}\n{e.Message}");
		}

		return newSprite;
	}

	public static void UpdateSprite(Cat cat)
	{
		if (cat.Faded)
		{
			return;
		}

		cat.Sprite = GenerateSprite(cat);
		Cat.AllCats[cat.ID] = cat;
	}

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
		DrawTextBoxedSelectable(font, text, rec, fontSize, spacing, wordWrap, tint, 0, 0, WHITE, WHITE);
	}


	// Draw text using font inside rectangle limits with support for text selection
	private static unsafe void DrawTextBoxedSelectable(
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
		Color selectBackTint)
	{
		int length = TextLength(text);  // Total length in bytes of the text, scanned by codepoints in loop

		float textOffsetY = 0;          // Offset between lines (on line break '\n')
		float textOffsetX = 0.0f;       // Offset X to next character to draw

		float scaleFactor = fontSize / (float)font.baseSize;     // Character rectangle scaling factor

		// Word/character wrapping mechanism variables
		int MEASURE_STATE = 0, DRAW_STATE = 1;

		int state = wordWrap ? MEASURE_STATE : DRAW_STATE;

		int startLine = -1;         // Index where to begin drawing (where a line begins)
		int endLine = -1;           // Index where to stop drawing (where a line ends)
		int lastk = -1;             // Holds last value of the character position

		for (int i = 0, k = 0; i < length; i++, k++)
		{
			// Get next codepoint from byte string and glyph index in font
			int codepointByteCount = 0;
			int codepoint = GetCodepoint(text[i], out codepointByteCount);
			int index = GetGlyphIndex(font, codepoint);

			// NOTE: Normally we exit the decoding sequence as soon as a bad byte is found (and return 0x3f)
			// but we need to draw all of the bad bytes using the '?' symbol moving one byte
			if (codepoint == 0x3f) codepointByteCount = 1;
			i += (codepointByteCount - 1);

			float glyphWidth = 0;
			if (codepoint != '\n')
			{
				glyphWidth = (font.glyphs[index].advanceX == 0) ? font.recs[index].width * scaleFactor : font.glyphs[index].advanceX * scaleFactor;

				if (i + 1 < length) glyphWidth = glyphWidth + spacing;
			}

			// NOTE: When wordWrap is ON we first measure how much of the text we can draw before going outside of the rec container
			// We store this info in startLine and endLine, then we change states, draw the text between those two variables
			// and change states again and again recursively until the end of the text (or until we get outside of the container).
			// When wordWrap is OFF we don't need the measure state so we go to the drawing state immediately
			// and begin drawing on the next line before we can get outside the container.
			if (state == MEASURE_STATE)
			{
				// TODO: There are multiple types of spaces in UNICODE, maybe it's a good idea to add support for more
				// Ref: http://jkorpela.fi/chars/spaces.html
				if ((codepoint == ' ') || (codepoint == '\t') || (codepoint == '\n')) endLine = i;

				if ((textOffsetX + glyphWidth) > rec.width)
				{
					endLine = (endLine < 1) ? i : endLine;
					if (i == endLine) endLine -= codepointByteCount;
					if ((startLine + codepointByteCount) == endLine) endLine = (i - codepointByteCount);

					state = state == 0 ? 1 : 0;
				}
				else if ((i + 1) == length)
				{
					endLine = i;
					state = state == 0 ? 1 : 0;
				}
				else if (codepoint == '\n') state = state == 0 ? 1 : 0;

				if (state == DRAW_STATE)
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
						textOffsetY += (font.baseSize + font.baseSize / 2) * scaleFactor;
						textOffsetX = 0;
					}
				}
				else
				{
					if (!wordWrap && ((textOffsetX + glyphWidth) > rec.width))
					{
						textOffsetY += (font.baseSize + font.baseSize / 2) * scaleFactor;
						textOffsetX = 0;
					}

					// When text overflows rectangle height limit, just stop drawing
					if ((textOffsetY + font.baseSize * scaleFactor) > rec.height) break;

					// Draw selection background
					bool isGlyphSelected = false;
					if ((selectStart >= 0) && (k >= selectStart) && (k < (selectStart + selectLength)))
					{
						DrawRectangleRec(new Rectangle(rec.X + textOffsetX - 1, rec.Y + textOffsetY, glyphWidth, (float)font.baseSize * scaleFactor), selectBackTint);
						isGlyphSelected = true;
					}

					// Draw current character glyph
					if ((codepoint != ' ') && (codepoint != '\t'))
					{
						DrawTextCodepoint(font, codepoint, new Vector2(rec.X + textOffsetX, rec.Y + textOffsetY), fontSize, isGlyphSelected ? selectTint : tint);
					}
				}

				if (wordWrap && (i == endLine))
				{
					textOffsetY += (font.baseSize + font.baseSize / 2) * scaleFactor;
					textOffsetX = 0;
					startLine = endLine;
					endLine = -1;
					glyphWidth = 0;
					selectStart += lastk - k;
					k = lastk;

					state = state == 0 ? 1 : 0;
				}
			}

			textOffsetX += glyphWidth;
		}
	}

	public static Vector2 GetVirutalMousePosition()
	{
		Vector2 mousePos = GetMousePosition();
		Vector2 virtualMouse = new(0)
		{
			X = (
			mousePos.X
			- ((GetScreenWidth()
			- (ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale)) * 0.5f))
			/ ScreenSettings.ScreenScale,
			Y = (
			mousePos.Y
			- ((GetScreenHeight()
			- (ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale)) * 0.5f))
			/ ScreenSettings.ScreenScale
		};
		virtualMouse = Vector2.Clamp(virtualMouse, new Vector2(0), ScreenSettings.GameScreenSize);

		return virtualMouse;
	}

	public static bool PointInArea(Vector2 point, Vector2 topLeft, Vector2 bottomRight)
	{
		return point.X >= topLeft.X
			&& point.X <= bottomRight.X
			&& point.Y >= topLeft.Y
			&& point.Y <= bottomRight.Y;
	}

	public static bool PointInArea(Vector2 point, ClanGenRect area)
	{
		return point.X >= area.TopLeft.X
			&& point.X <= area.BottomRight.X
			&& point.Y >= area.TopLeft.Y
			&& point.Y <= area.BottomRight.Y;
	}
}
