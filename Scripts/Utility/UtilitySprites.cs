using ClanGenDotNet.Scripts.Cats;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace ClanGenDotNet.Scripts;

public partial class Utility
{
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
}
