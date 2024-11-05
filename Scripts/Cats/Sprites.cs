using Newtonsoft.Json;
using System.Collections.Immutable;

namespace ClanGenDotNet.Scripts.Cats;

public static class Sprites
{
	public static Tint CatTints = JsonConvert
		.DeserializeObject<Tint>(File.ReadAllText(".\\Sprites\\Dicts\\tint.json"))!;
	public static Tint WhitePatchesTints = JsonConvert
		.DeserializeObject<Tint>(File.ReadAllText(".\\Sprites\\Dicts\\white_patches_tint.json"))!;
	public static Dictionary<string, Dictionary<string, object>> SymbolDict = JsonConvert
		.DeserializeObject<Dictionary<string, Dictionary<string, object>>>
		(File.ReadAllText(".\\Resources\\Dicts\\clan_symbols.json"))!;


	private static int _size = 50;
	private static readonly Dictionary<string, Image> _spritesheets = [];
	public static readonly Dictionary<string, Image> CatSprites = [];
	public static readonly Dictionary<string, Texture2D> SymbolSprites = [];
	public static readonly List<string> ClanSymbols = [];

	public static void MakeSpritesheet(string imageFile, string name)
	{
		_spritesheets.Add(name, LoadImage(imageFile));
	}

	public static void MakeGroup(
		string spritesheet,
		Vector2 position,
		string name,
		int spritesX = 3,
		int spritesY = 7,
		bool noIndex = false
	)
	{
		int groupXOffsets = (int)position.X * spritesX * _size;
		int groupYOffsets = (int)position.Y * spritesY * _size;

		string fullName;
		int i = 0;
		for (int y = 0; y < spritesY; y++)
		{
			for (int x = 0; x < spritesX; x++)
			{
				if (noIndex)
				{
					fullName = $"{name}";
				}
				else
				{
					fullName = $"{name}{i}";
				}

				Image newSprite = ImageFromImage(
					_spritesheets[spritesheet],
					new Rectangle(
						groupXOffsets + x * _size,
						groupYOffsets + y * _size,
						_size, _size
					)
				);

				CatSprites[fullName] = newSprite;
				i++;
			}
		}
	}

	public static void MakeGroupSymbol(
		string spritesheet,
		Vector2 position,
		string name,
		int spritesX = 3,
		int spritesY = 7,
		bool noIndex = false
	)
	{
		int groupXOffsets = (int)position.X * spritesX * _size;
		int groupYOffsets = (int)position.Y * spritesY * _size;

		string fullName;
		int i = 0;
		for (int y = 0; y < spritesY; y++)
		{
			for (int x = 0; x < spritesX; x++)
			{
				if (noIndex)
				{
					fullName = $"{name}";
				}
				else
				{
					fullName = $"{name}{i}";
				}

				Image newSprite = ImageFromImage(
					_spritesheets[spritesheet],
					new Rectangle(
						groupXOffsets + x * _size,
						groupYOffsets + y * _size,
						_size, _size
					)
				);

				SymbolSprites[fullName] = LoadTextureFromImage(newSprite);
				UnloadImage(newSprite);
				i++;
			}
		}
	}

	public static void LoadAll()
	{
		Image lineart = LoadImage(".\\Sprites\\lineart.png");
		int width = lineart.width; int height = lineart.height;

		if (width / 3 == height / 7)
		{
			_size = width / 3;
		}
		else
		{
			_size = 50;
		}

		string[] spritesheets = [
			"lineart", "lineartdf", "lineartdead",
			"eyes", "eyes2", "skin",
			"scars", "missingscars",
			"medcatherbs", "wild",
			"collars", "bellcollars", "bowcollars", "nyloncollars",
			"singlecolours", "speckledcolours", "tabbycolours", "bengalcolours", "marbledcolours",
			"rosettecolours", "smokecolours", "tickedcolours", "mackerelcolours", "classiccolours",
			"sokokecolours", "agouticolours", "singlestripecolours", "maskedcolours",
			"shadersnewwhite", "lightingnew",
			"whitepatches", "tortiepatchesmasks",
			"fademask", "fadestarclan", "fadedarkforest",
			"symbols"
		];
		foreach (string spritesheet in spritesheets)
		{
			if (spritesheet == "lineart" && game.Config.Fun.AprilFools)
			{
				MakeSpritesheet($".\\Sprites\\aprilfools{spritesheet}.png", spritesheet);
			}
			else
			{
				MakeSpritesheet($".\\Sprites\\{spritesheet}.png", spritesheet);
			}
		}

		MakeGroup("lineart", new Vector2(0, 0), "lines");
		MakeGroup("shadersnewwhite", new Vector2(0, 0), "shaders");
		MakeGroup("lightingnew", new Vector2(0, 0), "lighting");

		MakeGroup("lineartdead", new Vector2(0, 0), "lineartdead");
		MakeGroup("lineartdf", new Vector2(0, 0), "lineartdf");

		for (int i = 0; i < 3; i++)
		{
			MakeGroup("fademask", new(i, 0), $"fademask{i}");
			MakeGroup("fadestarclan", new(i, 0), $"fadestarclan{i}");
			MakeGroup("fadedarkforest", new(i, 0), $"fadedf{i}");
		}

		string[][] eyeColours = [
			["YELLOW", "AMBER", "HAZEL", "PALEGREEN", "GREEN", "BLUE", "DARKBLUE", "GREY", "CYAN", "EMERALD",
				"HEATHERBLUE", "SUNLITICE"],
			["COPPER", "SAGE", "COBALT", "PALEBLUE", "BRONZE", "SILVER", "PALEYELLOW", "GOLD", "GREENYELLOW"]
		];

		for (int i = 0; i < eyeColours.Length; i++)
		{
			string[] colours = eyeColours[i];
			for (int j = 0; j < colours.Length; j++)
			{
				string colour = colours[j];
				MakeGroup("eyes", new(j, i), $"eyes{colour}");
				MakeGroup("eyes2", new(j, i), $"eyes2{colour}");
			}
		}

		string[][] whitePatches = [
			["FULLWHITE", "ANY", "TUXEDO", "LITTLE", "COLOURPOINT", "VAN", "ANYTWO", "MOON", "PHANTOM", "POWDER",
				"BLEACHED", "SAVANNAH", "FADESPOTS", "PEBBLESHINE"],
			["EXTRA", "ONEEAR", "BROKEN", "LIGHTTUXEDO", "BUZZARDFANG", "RAGDOLL", "LIGHTSONG", "VITILIGO", "BLACKSTAR",
				"PIEBALD", "CURVED", "PETAL", "SHIBAINU", "OWL"],
			["TIP", "FANCY", "FRECKLES", "RINGTAIL", "HALFFACE", "PANTSTWO", "GOATEE", "VITILIGOTWO", "PAWS", "MITAINE",
				"BROKENBLAZE", "SCOURGE", "DIVA", "BEARD"],
			["TAIL", "BLAZE", "PRINCE", "BIB", "VEE", "UNDERS", "HONEY", "FAROFA", "DAMIEN", "MISTER", "BELLY",
				"TAILTIP", "TOES", "TOPCOVER"],
			["APRON", "CAPSADDLE", "MASKMANTLE", "SQUEAKS", "STAR", "TOESTAIL", "RAVENPAW", "PANTS", "REVERSEPANTS",
				"SKUNK", "KARPATI", "HALFWHITE", "APPALOOSA", "DAPPLEPAW"],
			["HEART", "LILTWO", "GLASS", "MOORISH", "SEPIAPOINT", "MINKPOINT", "SEALPOINT", "MAO", "LUNA", "CHESTSPECK",
				"WINGS", "PAINTED", "HEARTTWO", "WOODPECKER"],
			["BOOTS", "MISS", "COW", "COWTWO", "BUB", "BOWTIE", "MUSTACHE", "REVERSEHEART", "SPARROW", "VEST",
				"LOVEBUG", "TRIXIE", "SAMMY", "SPARKLE"],
			["RIGHTEAR", "LEFTEAR", "ESTRELLA", "SHOOTINGSTAR", "EYESPOT", "REVERSEEYE", "FADEBELLY", "FRONT",
				"BLOSSOMSTEP", "PEBBLE", "TAILTWO", "BUDDY", "BACKSPOT", "EYEBAGS"],
			["BULLSEYE", "FINN", "DIGIT", "KROPKA", "FCTWO", "FCONE", "MIA", "SCAR", "BUSTER", "SMOKEY", "HAWKBLAZE",
				"CAKE", "ROSINA", "PRINCESS"],
			["LOCKET", "BLAZEMASK", "TEARS", "DOUGIE"]
		];

		for (int i = 0; i < whitePatches.Length; i++)
		{
			string[] patches = whitePatches[i];
			for (int j = 0; j < patches.Length; j++)
			{
				string patch = patches[j];
				MakeGroup("whitepatches", new(j, i), $"white{patch}");
			}
		}

		string[][] colourCategories = [
			["WHITE", "PALEGREY", "SILVER", "GREY", "DARKGREY", "GHOST", "BLACK"],
			["CREAM", "PALEGINGER", "GOLDEN", "GINGER", "DARKGINGER", "SIENNA"],
			["LIGHTBROWN", "LILAC", "BROWN", "GOLDEN-BROWN", "DARKBROWN", "CHOCOLATE"]
		];

		string[] colourTypes = [
			"singlecolours", "tabbycolours", "marbledcolours", "rosettecolours",
			"smokecolours", "tickedcolours", "speckledcolours", "bengalcolours",
			"mackerelcolours", "classiccolours", "sokokecolours", "agouticolours",
			"singlestripecolours", "maskedcolours"
		];

		for (int i = 0; i < colourCategories.Length; i++)
		{
			string[] colours = colourCategories[i];
			for (int j = 0; j < colours.Length; j++)
			{
				string colour = colours[j];
				foreach (string colourType in colourTypes)
				{
					MakeGroup(colourType, new(j, i), $"{colourType[..^7]}{colour}");
				}
			}
		}

		string[][] tortiePatchesMasks = [
			["ONE", "TWO", "THREE", "FOUR", "REDTAIL", "DELILAH", "HALF", "STREAK", "MASK", "SMOKE"],
			["MINIMALONE", "MINIMALTWO", "MINIMALTHREE", "MINIMALFOUR", "OREO", "SWOOP", "CHIMERA", "CHEST", "ARMTAIL",
				"GRUMPYFACE"],
			["MOTTLED", "SIDEMASK", "EYEDOT", "BANDANA", "PACMAN", "STREAMSTRIKE", "SMUDGED", "DAUB", "EMBER", "BRIE"],
			["ORIOLE", "ROBIN", "BRINDLE", "PAIGE", "ROSETAIL", "SAFI", "DAPPLENIGHT", "BLANKET", "BELOVED", "BODY"],
			["SHILOH", "FRECKLED", "HEARTBEAT"]
		];

		for (int i = 0; i < tortiePatchesMasks.Length; i++)
		{
			string[] patches = tortiePatchesMasks[i];
			for (int j = 0; j < patches.Length; j++)
			{
				string patch = patches[j];
				MakeGroup("tortiepatchesmasks", new(j, i), $"tortiemask{patch}");
			}
		}

		string[][] skinColours = [
			["BLACK", "RED", "PINK", "DARKBROWN", "BROWN", "LIGHTBROWN"],
			["DARK", "DARKGREY", "GREY", "DARKSALMON", "SALMON", "PEACH"],
			["DARKMARBLED", "MARBLED", "LIGHTMARBLED", "DARKBLUE", "BLUE", "LIGHTBLUE"]
		];

		for (int i = 0; i < skinColours.Length; i++)
		{
			string[] colours = skinColours[i];
			for (int j = 0; j < colours.Length; j++)
			{
				string colour = colours[j];
				MakeGroup("skin", new(j, i), $"skin{colour}");
			}
		}

		LoadScars();
		LoadSymbols();

		//Prevent spritesheet from taking up like 200 MB of memory holy god
		//I'm not even kidding, before this 5 line statement, it took roughly 500 MB, now it's at
		//326 MB.
		foreach (var image in _spritesheets)
		{
			UnloadImage(image.Value);
		}
		_spritesheets.Clear(); 
	}

	private static void LoadScars()
	{
		string[][] scarsData = [
            ["ONE", "TWO", "THREE", "MANLEG", "BRIGHTHEART", "MANTAIL", "BRIDGE", "RIGHTBLIND", "LEFTBLIND",
             "BOTHBLIND", "BURNPAWS", "BURNTAIL"],
            ["BURNBELLY", "BEAKCHEEK", "BEAKLOWER", "BURNRUMP", "CATBITE", "RATBITE", "FROSTFACE", "FROSTTAIL",
             "FROSTMITT", "FROSTSOCK", "QUILLCHUNK", "QUILLSCRATCH"],
            ["TAILSCAR", "SNOUT", "CHEEK", "SIDE", "THROAT", "TAILBASE", "BELLY", "TOETRAP", "SNAKE", "LEGBITE",
             "NECKBITE", "FACE"],
            ["HINDLEG", "BACK", "QUILLSIDE", "SCRATCHSIDE", "TOE", "BEAKSIDE", "CATBITETWO", "SNAKETWO", "FOUR"]
        ];

		string[][] missingPartsData = [
			["LEFTEAR", "RIGHTEAR", "NOTAIL", "NOLEFTEAR", "NORIGHTEAR", "NOEAR", "HALFTAIL", "NOPAW"]
		];

		for (int i = 0; i < scarsData.Length; i++)
		{
			string[] scars = scarsData[i];
			for (int j = 0; j < scars.Length; j++)
			{
				string scar = scars[j];
				MakeGroup("scars", new(j, i), $"scars{scar}");
			}
		}

		for (int i = 0; i < missingPartsData.Length; i++)
		{
			string[] scars = missingPartsData[i];
			for (int j = 0; j < scars.Length; j++)
			{
				string scar = scars[j];
				MakeGroup("missingscars", new(j, i), $"scars{scar}");
			}
		}

		string[][] medcatHerbsData = [
			["MAPLE LEAF", "HOLLY", "BLUE BERRIES", "FORGET ME NOTS", "RYE STALK", "CATTAIL", "POPPY", "ORANGE POPPY", "CYAN POPPY", "WHITE POPPY", "PINK POPPY"],
			["BLUEBELLS", "LILY OF THE VALLEY", "SNAPDRAGON", "HERBS", "PETALS", "NETTLE", "HEATHER", "GORSE", "JUNIPER", "LAVENDER"],
			["OAK LEAVES", "CATMINT", "MAPLE SEED", "LAUREL", "BULB WHITE", "BULB YELLOW", "BULB ORANGE", "BULB PINK", "BULB BLUE", "CLOVER", "DAISY"]
		];
		string[][] dryHerbsData = [
			["DRY HERBS", "DRY CATMINT", "DRY NETTLES", "DRY LAURELS"]
		];
		string[][] wildData = [
			["RED FEATHERS", "BLUE FEATHERS", "JAY FEATHERS", "GULL FEATHERS", "SPARROW FEATHERS", "MOTH WINGS", "ROSY MOTH WINGS", "MORPHO BUTTERFLY", "MONARCH BUTTERFLY", "CICADA WINGS", "BLACK CICADA"]
		];
		string[][] collarsData = [
			["CRIMSON", "BLUE", "YELLOW", "CYAN", "RED", "LIME"],
			["GREEN", "RAINBOW", "BLACK", "SPIKES", "WHITE"],
			["PINK", "PURPLE", "MULTI", "INDIGO"]
		];
		string[][] bellCollarsData = [
			["CRIMSONBELL", "BLUEBELL", "YELLOWBELL", "CYANBELL", "REDBELL", "LIMEBELL"],
			["GREENBELL", "RAINBOWBELL", "BLACKBELL", "SPIKESBELL", "WHITEBELL"],
			["PINKBELL", "PURPLEBELL", "MULTIBELL", "INDIGOBELL"]
		];
		string[][] bowCollarsData = [
			["CRIMSONBOW", "BLUEBOW", "YELLOWBOW", "CYANBOW", "REDBOW", "LIMEBOW"],
			["GREENBOW", "RAINBOWBOW", "BLACKBOW", "SPIKESBOW", "WHITEBOW"],
			["PINKBOW", "PURPLEBOW", "MULTIBOW", "INDIGOBOW"]
		];
		string[][] nylonCollarsData = [
			["CRIMSONNYLON", "BLUENYLON", "YELLOWNYLON", "CYANNYLON", "REDNYLON", "LIMENYLON"],
			["GREENNYLON", "RAINBOWNYLON", "BLACKNYLON", "SPIKESNYLON", "WHITENYLON"],
			["PINKNYLON", "PURPLENYLON", "MULTINYLON", "INDIGONYLON"]
		];

		EnumerateAndMakeGroup(medcatHerbsData, "medcatherbs", "acc_herbs");
		for (int i = 0; i < dryHerbsData.Length; i++)
		{
			string[] group = dryHerbsData[i];
			for (int j = 0; j < group.Length; j++)
			{
				string single = group[j];
				MakeGroup("medcatherbs", new(j, 3), $"acc_herbs{single}");
			}
		}
		for (int i = 0; i < wildData.Length; i++)
		{
			string[] group = wildData[i];
			for (int j = 0; j < group.Length; j++)
			{
				string single = group[j];
				MakeGroup("wild", new(j, 0), $"acc_wild{single}");
			}
		}
		EnumerateAndMakeGroup(collarsData, "collars", "collars");
		EnumerateAndMakeGroup(bellCollarsData, "bellcollars", "collars");
		EnumerateAndMakeGroup(bowCollarsData, "bowcollars", "collars");
		EnumerateAndMakeGroup(nylonCollarsData, "nyloncollars", "collars");
	}

	public static void LoadSymbols()
	{
		char[] letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
				   'V', 'W', 'Y', 'Z'];

		int yPos = 1;
		foreach (char letter in letters)
		{
			int xMod = 0;

			int i = 0;
			foreach (var symbolKV in SymbolDict
				.Where(symbol => symbol.Key.Contains(letter) && SymbolDict[symbol.Key]["variants"] != null)
			)
			{
				string symbol = symbolKV.Key;
				if (Convert.ToInt32(SymbolDict[symbol]["variants"]) > 1 && xMod > 0)
				{
					xMod++;
				}
				foreach (int varIndex in Enumerable.Range(0, Convert.ToInt32(SymbolDict[symbol]["variants"])))
				{
					int xPos = i + xMod;

					if (Convert.ToInt32(SymbolDict[symbol]["variants"]) > 1)
					{
						xPos++;
					}
					else if (xPos > 0)
					{
						xPos--;
					}

					ClanSymbols.Add($"symbol{symbol.ToUpper()}{varIndex}");
					MakeGroupSymbol("symbols", new Vector2(xPos, yPos), ClanSymbols.Last(), 1, 1, true);
				}

				i++;
			}

			yPos++;
		}

		DarkModeSymbol();
	}

	private unsafe static void DarkModeSymbol()
	{
		Dictionary<string, Texture> darkSymbols = [];
		foreach (var symbolSprite in SymbolSprites)
		{
			Image darkSymbol = LoadImageFromTexture(symbolSprite.Value);
			Color* pixels = LoadImageColors(darkSymbol);
			Color original = new(87, 76, 45, 255);
			Color replacement = new(87, 76, 45, 255);
			for (int i = 0; i < darkSymbol.width; i++)
			{
				for (int j = 0; j < darkSymbol.height; j++)
				{
					if (pixels[i * darkSymbol.width + j].Equals(original))
					{
						pixels[i * darkSymbol.width + j] = replacement;
					}
				}
			}
			darkSymbols.Add(symbolSprite.Key + "#dark", LoadTextureFromImage(darkSymbol));
			UnloadImageColors(pixels);
			UnloadImage(darkSymbol);
		}
		foreach (var darkSprite in darkSymbols)
		{
			SymbolSprites.Add(darkSprite.Key, darkSprite.Value);
		}
		darkSymbols.Clear();
	}

	public static void EnumerateAndMakeGroup(string[][] data, string name, string groupName)
	{
		for (int i = 0; i < data.Length; i++)
		{
			string[] group = data[i];
			for (int j = 0; j < group.Length; j++)
			{
				string single = group[j];
				MakeGroup(name, new(j, i), $"{groupName}{single}");
			}
		}
	}
}
