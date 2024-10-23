using ClanGenDotNet.Scripts.Cats;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace ClanGenDotNet.Scripts;

public class Utility
{
	public static Rectangle Scale(Rectangle oldRect)
	{
		float posX = oldRect.X / 1600 * game.ScreenX;
		float posY = oldRect.Y / 1400 * game.ScreenY;
		float scaleX = oldRect.width / 1600 * game.ScreenX;
		float scaleY = oldRect.height / 1400 * game.ScreenY;

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
			age = cat.Age.ToCorrectString().ToLower();
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

		//catSprite = Rand.Next(1, 20).ToString();
		//Console.WriteLine(catSprite);

		Image newSprite;
		FillImage(&newSprite, BLANK);

		try
		{
			if (cat.Pelt.Name != "Tortie" && cat.Pelt.Name != "Calico")
			{
				newSprite = ImageCopy(Sprites.CatSprites[
					cat.Pelt.GetSpritesName() + cat.Pelt.Colour + catSprite
				]);
			}
			else
			{
				//Draw base coat
				newSprite = ImageCopy(Sprites.CatSprites[
					cat.Pelt.TortieBase + cat.Pelt.Colour + catSprite
				]);

				string tortiePattern = "SingleColour";
				if (cat.Pelt.TortiePattern == "Single") { tortiePattern = "SingleColour"; }
				else { tortiePattern = cat.Pelt.TortiePattern!; }

				Image patches = ImageCopy(Sprites.CatSprites[
					tortiePattern + cat.Pelt.TortieColour + catSprite
				]);
				Image mask = Sprites.CatSprites[
					"tortiemask" + cat.Pelt.Pattern + catSprite
				];
				ImageBlendMultiply(
					patches,
					mask
				);
				ImageDraw(
					&newSprite,
					patches,
					new Rectangle(0, 0, patches.width, patches.height),
					new Rectangle(0, 0, newSprite.width, newSprite.height),
					WHITE
				);
				UnloadImage(mask);
				UnloadImage(patches);
			}

			if (cat.Pelt.Tint != null && Sprites.CatTints.TintColours.TryGetValue(cat.Pelt.Tint, out List<int>? tintColour))
			{
				if (tintColour != null)
				{
					Image tint = ImageCopy(newSprite);
					List<int> colours = tintColour!;
					FillImage(
						&tint,
						new Color(
							colours[0],
							colours[1],
							colours[2],
							255
						)
					);
					ImageBlendMultiply(
					newSprite,
						tint
					);
					UnloadImage(tint);
				}
			}
			else if (cat.Pelt.Tint != null && Sprites.CatTints.DiluteTintColours.TryGetValue(cat.Pelt.Tint, out List<int>? value))
			{
				if (value != null)
				{
					Image tint = ImageCopy(newSprite);
					List<int> colours = value!;
					FillImage(
						&tint,
						new Color(
							colours[0],
							colours[1],
							colours[2],
							255
						)
					);
					ImageBlendAdditive(
						newSprite,
						tint
					);
					ExportImage(tint, ".\\tint.png");
					ExportImage(newSprite, ".\\whiteP.png");
					UnloadImage(tint);
				}
			}

			if (cat.Pelt.WhitePatches != null)
			{
				Image whitePatches = ImageCopy(Sprites.CatSprites[
					"white" + cat.Pelt.WhitePatches + catSprite
				]);

				if (
					cat.Pelt.WhitePatchesTint != null
					&& Sprites.WhitePatchesTints.TintColours.ContainsKey(cat.Pelt.WhitePatchesTint)
					&& cat.Pelt.WhitePatchesTint != "none"
				)
				{
					Image tint = ImageCopy(newSprite);
					List<int> colours = Sprites.WhitePatchesTints.TintColours[cat.Pelt.WhitePatchesTint]!;
					FillImage(
						&tint,
						new Color(
							colours[0],
							colours[1],
							colours[2],
							255
						)
					);
					ImageBlendMultiply(
						whitePatches,
						tint
					);
					UnloadImage(tint);
				}

				ImageDraw(
					&newSprite,
					whitePatches,
					new Rectangle(0, 0, whitePatches.width, whitePatches.height),
					new Rectangle(0, 0, newSprite.width, newSprite.height),
					WHITE
				);
				UnloadImage(whitePatches);
			}

			if (cat.Pelt.Points != null)
			{
				Image points = Sprites.CatSprites[
					"white" + cat.Pelt.Points + catSprite
				];

				if (
					cat.Pelt.WhitePatchesTint != null
					&& Sprites.WhitePatchesTints.TintColours.TryGetValue(
						cat.Pelt.WhitePatchesTint,
						out List<int>? color
					)
				)
				{
					if (color != null)
					{
						Image tint = ImageCopy(newSprite);
						List<int> colours = color!;
						FillImage(
							&tint,
							new Color(
								colours[0],
								colours[1],
								colours[2],
								255
							)
						);
						ImageBlendMultiply(
							points,
							tint
						);
						UnloadImage(tint);
					}
				}

				ImageDraw(
					&newSprite,
					points,
					new Rectangle(0, 0, points.width, points.height),
					new Rectangle(0, 0, newSprite.width, newSprite.height),
					WHITE
				);
			}

			if (cat.Pelt.Vitiligo != null)
			{
				Image vit = Sprites.CatSprites[
					"white" + cat.Pelt.Vitiligo + catSprite
				];
				ImageDraw(
					&newSprite,
					vit,
					new Rectangle(0, 0, vit.width, vit.height),
					new Rectangle(0, 0, newSprite.width, newSprite.height),
					WHITE
				);
			}

			Image eyes = ImageCopy(Sprites.CatSprites["eyes" + cat.Pelt.EyeColour + catSprite]);
			if (cat.Pelt.EyeColour2 != null)
			{
				Image eye2 = Sprites.CatSprites["eyes2" + cat.Pelt.EyeColour2 + catSprite];
				ImageDraw(
					&eyes,
					eye2,
					new Rectangle(0, 0, eye2.width, eye2.height),
					new Rectangle(0, 0, newSprite.width, newSprite.height),
					WHITE
				);
			}

			ImageDraw(
				&newSprite,
				eyes,
				new Rectangle(0, 0, eyes.width, eyes.height),
				new Rectangle(0, 0, newSprite.width, newSprite.height),
				WHITE
			);
			UnloadImage(eyes);

			if (!hideScars)
			{
				foreach (string scar in cat.Pelt.Scars)
				{
					if (Pelt.Scars1.Concat(Pelt.Scars3).Contains(scar))
					{
						Image scarImg = Sprites.CatSprites["scars" + scar + catSprite];
						ImageDraw(
							&newSprite,
							scarImg,
							new Rectangle(0, 0, scarImg.width, scarImg.height),
							new Rectangle(0, 0, newSprite.width, newSprite.height),
							WHITE
						);
					}
				}
			}

			if ((bool)game.Settings["shaders"]! && !dead)
			{
				Image shader = Sprites.CatSprites["shaders" + catSprite];
				ImageBlendMultiply(newSprite, shader);
				Image lighting = Sprites.CatSprites["lighting" + catSprite];
				ImageDraw(
					&newSprite,
					lighting,
					new Rectangle(0, 0, lighting.width, lighting.height),
					new Rectangle(0, 0, newSprite.width, newSprite.height),
					WHITE
				);
			}

			Image lineart;
			if (dead)
			{
				lineart = Sprites.CatSprites["lineartdead" + catSprite];
			}
			else if (cat.DarkForest)
			{
				lineart = Sprites.CatSprites["lineartdf" + catSprite];
			}
			else
			{
				lineart = Sprites.CatSprites["lines" + catSprite];
			}

			ImageDraw(
				&newSprite,
				lineart,
				new Rectangle(0, 0, lineart.width, lineart.height),
				new Rectangle(0, 0, newSprite.width, newSprite.height),
				WHITE
			);

			Image skin = Sprites.CatSprites["skin" + cat.Pelt.Skin + catSprite];
			ImageDraw(
				&newSprite,
				skin,
				new Rectangle(0, 0, skin.width, skin.height),
				new Rectangle(0, 0, newSprite.width, newSprite.height),
				WHITE
			);

			if (!hideScars)
			{
				foreach (string scar in cat.Pelt.Scars)
				{
					if (Pelt.Scars2.Contains(scar))
					{
						Image scarImg = Sprites.CatSprites["scars" + scar + catSprite];
						ImageBlendMinChannel(
							newSprite,
							scarImg
						);
					}
				}
			}

			if (!hideAccessories)
			{
				if (Pelt.PlantAccessories.Contains(cat.Pelt.Accessory))
				{
					Image accessory = Sprites.CatSprites["acc_herbs" + cat.Pelt.Accessory + catSprite];
					ImageDraw(
						&newSprite,
						accessory,
						new Rectangle(0, 0, accessory.width, accessory.height),
						new Rectangle(0, 0, newSprite.width, newSprite.height),
						WHITE
					);
				}
				else if (Pelt.WildAccessories.Contains(cat.Pelt.Accessory))
				{
					Image accessory = Sprites.CatSprites["acc_wild" + cat.Pelt.Accessory + catSprite];
					ImageDraw(
						&newSprite,
						accessory,
						new Rectangle(0, 0, accessory.width, accessory.height),
						new Rectangle(0, 0, newSprite.width, newSprite.height),
						WHITE
					);
				}
				else if (Pelt.Collars.Contains(cat.Pelt.Accessory))
				{
					Image accessory = Sprites.CatSprites["collars" + cat.Pelt.Accessory + catSprite];
					ImageDraw(
						&newSprite,
						accessory,
						new Rectangle(0, 0, accessory.width, accessory.height),
						new Rectangle(0, 0, newSprite.width, newSprite.height),
						WHITE
					);
				}
			}

			if (
				cat.Pelt.Opacity <= 97
				&& !cat.PreventFading
				&& (bool)game.Settings["fading"]!
				&& dead
			)
			{
				string stage = "0";
				if (cat.Pelt.Opacity > 45 && cat.Pelt.Opacity <= 80)
				{
					stage = "1";
				}
				else if (cat.Pelt.Opacity <= 45)
				{
					stage = "2";
				}

				Image fadeMask = Sprites.CatSprites[
					"fademask" + stage + catSprite
				];
				ImageDraw(
					&newSprite,
					fadeMask,
					new Rectangle(0, 0, fadeMask.width, fadeMask.height),
					new Rectangle(0, 0, newSprite.width, newSprite.height),
					WHITE
				);

				if (cat.DarkForest)
				{
					Image faded = Sprites.CatSprites["fadedf" + stage + catSprite];
					ImageDraw(
						&faded,
						newSprite,
						new Rectangle(0, 0, newSprite.width, newSprite.height),
						new Rectangle(0, 0, faded.width, faded.height),
						WHITE
					);
					UnloadImage(newSprite);
					newSprite = faded;
				}
				else
				{
					Image faded = Sprites.CatSprites["fadestarclan" + stage + catSprite];
					ImageDraw(
						&faded,
						newSprite,
						new Rectangle(0, 0, newSprite.width, newSprite.height),
						new Rectangle(0, 0, faded.width, faded.height),
						WHITE
					);
					UnloadImage(newSprite);
					newSprite = faded;
				}
			}

			if (cat.Pelt.Reverse)
			{
				ImageFlipHorizontal(&newSprite);
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error updating sprite.\n{e.Message}");

			newSprite = LoadImage(".\\Sprites\\error_placeholder.png");
		}

		return newSprite;
	}

	private unsafe static void FillImage(Image* src, Color pixelColour)
	{
		for (int y = 0; y < (*src).width; y++)
		{
			for (int x = 0; x < (*src).height; x++)
			{
				ImageDrawPixel(
					src,
					x,
					y,
					pixelColour
				);
			}
		}
	}

	public static unsafe void ImageBlendMultiply(Image src, Image blend)
	{
		Color* srcPixels = LoadImageColors(src);
		Color* blendPixels = LoadImageColors(blend);
		for (int i = 0; i < src.width * src.height; i++)
		{
			srcPixels[i] = new Color(
				(int)(srcPixels[i].r / 255.0f * (blendPixels[i].r / 255.0f) * 255),
				(int)(srcPixels[i].g / 255.0f * (blendPixels[i].g / 255.0f) * 255),
				(int)(srcPixels[i].b / 255.0f * (blendPixels[i].b / 255.0f) * 255),
				(int)(srcPixels[i].a / 255.0f * (blendPixels[i].a / 255.0f) * 255)
			);
		}
		for (int i = 0; i < src.width; i++)
		{
			for (int j = 0; j < src.height; j++)
			{
				int index = j * src.width + i;
				ImageDrawPixel(
					&src,
					i, j,
					srcPixels[index]
				);
			}
		}
		UnloadImageColors(srcPixels);
		UnloadImageColors(blendPixels);
	}

	private static unsafe void ImageBlendAdditive(Image src, Image blend)
	{
		Color* srcPixels = LoadImageColors(src);
		Color* blendPixels = LoadImageColors(blend);
		for (int i = 0; i < src.width * src.height; i++)
		{
			int red = Math.Min(srcPixels[i].r + blendPixels[i].r, 255);
			int green = Math.Min(srcPixels[i].g + blendPixels[i].g, 255);
			int blue = Math.Min(srcPixels[i].b + blendPixels[i].b, 255);
			srcPixels[i] = new Color(
				red,
				green,
				blue,
				(int)(srcPixels[i].a / 255.0f * (blendPixels[i].a / 255.0f) * 255)
			);
		}
		for (int i = 0; i < src.width; i++)
		{
			for (int j = 0; j < src.height; j++)
			{
				ImageDrawPixel(
					&src,
					i, j,
					srcPixels[j * src.width + i]
				);
			}
		}
		UnloadImageColors(srcPixels);
		UnloadImageColors(blendPixels);
	}

	private static unsafe void ImageBlendMinChannel(Image src, Image blend)
	{
		Color* srcPixels = LoadImageColors(src);
		Color* blendPixels = LoadImageColors(blend);
		for (int i = 0; i < src.width * src.height; i++)
		{
			srcPixels[i] = new Color(
				Math.Min(srcPixels[i].r, blendPixels[i].r),
				Math.Min(srcPixels[i].b, blendPixels[i].b),
				Math.Min(srcPixels[i].g, blendPixels[i].g),
				Math.Min(srcPixels[i].a, blendPixels[i].a)
			);
		}
		for (int i = 0; i < src.width; i++)
		{
			for (int j = 0; j < src.height; j++)
			{
				ImageDrawPixel(
					&src,
					i, j,
					srcPixels[j * src.width + i]
				);
			}
		}
		UnloadImageColors(srcPixels);
		UnloadImageColors(blendPixels);
	}

	public static void UpdateSprite(Cat cat)
	{
		if (cat.Faded)
		{
			return;
		}

		Image newImage = GenerateSprite(cat);
		if (cat.Sprite.Equals(default))
		{
			cat.Sprite = LoadTextureFromImage(ImageCopy(newImage));
		}
		else
		{
			UnloadTexture(cat.Sprite);
			cat.Sprite = LoadTextureFromImage(ImageCopy(newImage));
		}
		UnloadImage(newImage);
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
