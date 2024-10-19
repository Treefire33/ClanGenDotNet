using Microsoft.Toolkit.HighPerformance.Buffers;
using System;
using System.Text.RegularExpressions;

namespace ClanGenDotNet.Scripts.Cats;

/// <summary>
/// String[] holder, I mean, cat appearance class.
/// </summary>
public partial class Pelt
{
	public static readonly Dictionary<string, string?> SpriteNames = new() {
		{ "SingleColour", "single" },
		{ "TwoColour", "single" },
		{ "Tabby", "tabby" },
		{ "Marbled", "marbled" },
		{ "Rosette", "rosette" },
		{ "Smoke", "smoke" },
		{ "Ticked", "ticked" },
		{ "Speckled", "speckled" },
		{ "Bengal", "bengal" },
		{ "Mackerel", "mackerel" },
		{ "Classic", "classic" },
		{ "Sokoke", "sokoke" },
		{ "Agouti", "agouti" },
		{ "Singlestripe", "singlestripe" },
		{ "Masked", "masked" },
		{ "Tortie", null },
		{ "Calico", null },
	};

	public static readonly string[] PeltColours = [
		"WHITE", "PALEGREY", "SILVER", "GREY", "DARKGREY", "GHOST", "BLACK", "CREAM", "PALEGINGER",
        "GOLDEN", "GINGER", "DARKGINGER", "SIENNA", "LIGHTBROWN", "LILAC", "BROWN", "GOLDEN-BROWN", "DARKBROWN",
        "CHOCOLATE"
	];
	public static readonly string[] PeltNoWhite = [
		"PALEGREY", "SILVER", "GREY", "DARKGREY", "GHOST", "BLACK", "CREAM", "PALEGINGER",
        "GOLDEN", "GINGER", "DARKGINGER", "SIENNA", "LIGHTBROWN", "LILAC", "BROWN", "GOLDEN-BROWN", "DARKBROWN",
        "CHOCOLATE"
	];
	public static readonly string[] PeltNoBlackWhite = [
		"PALEGREY", "SILVER", "GREY", "DARKGREY", "CREAM", "PALEGINGER",
        "GOLDEN", "GINGER", "DARKGINGER", "SIENNA", "LIGHTBROWN", "LILAC", "BROWN", "GOLDEN-BROWN", "DARKBROWN",
        "CHOCOLATE"
	];

	public static readonly string[] TortiePatterns = [
		"ONE", "TWO", "THREE", "FOUR", "REDTAIL", "DELILAH", "MINIMALONE", "MINIMALTWO", "MINIMALTHREE",
        "MINIMALFOUR", "HALF",
        "OREO", "SWOOP", "MOTTLED", "SIDEMASK", "EYEDOT", "BANDANA", "PACMAN", "STREAMSTRIKE", "ORIOLE",
        "CHIMERA", "DAUB", "EMBER", "BLANKET",
        "ROBIN", "BRINDLE", "PAIGE", "ROSETAIL", "SAFI", "SMUDGED", "DAPPLENIGHT", "STREAK", "MASK",
        "CHEST", "ARMTAIL", "SMOKE", "GRUMPYFACE",
        "BRIE", "BELOVED", "BODY", "SHILOH", "FRECKLED", "HEARTBEAT"
	];
	public static readonly string[] TortieBases = [
		"single", "tabby", "bengal", "marbled", "ticked", "smoke", "rosette", "speckled", "mackerel",
        "classic", "sokoke", "agouti", "singlestripe", "masked"
	];

	public static readonly string[] PeltLength = [
		"short", "medium", "long"
	];

	public static readonly string[] EyeColours = [
		"YELLOW", "AMBER", "HAZEL", "PALEGREEN", "GREEN", "BLUE", "DARKBLUE", "GREY", "CYAN", "EMERALD",
        "PALEBLUE",
        "PALEYELLOW", "GOLD", "HEATHERBLUE", "COPPER", "SAGE", "COBALT", "SUNLITICE", "GREENYELLOW",
        "BRONZE", "SILVER"
	];

	public static readonly string[] YellowEyes = [
		"YELLOW", "AMBER", "PALEYELLOW", "GOLD", "COPPER", "GREENYELLOW", "BRONZE", "SILVER"
	];

	public static readonly string[] BlueEyes = [
		"BLUE", "DARKBLUE", "CYAN", "PALEBLUE", "HEATHERBLUE", "COBALT", "SUNLITICE", "GREY"
	];

	public static readonly string[] GreenEyes = [
		"PALEGREEN", "GREEN", "EMERALD", "SAGE", "HAZEL"
	];

	public static readonly string[] Scars1 = [
		"ONE", "TWO", "THREE", "TAILSCAR", "SNOUT", "CHEEK", "SIDE", "THROAT", "TAILBASE", "BELLY",
        "LEGBITE", "NECKBITE", "FACE", "MANLEG", "BRIGHTHEART", "MANTAIL", "BRIDGE", "RIGHTBLIND", "LEFTBLIND",
        "BOTHBLIND", "BEAKCHEEK", "BEAKLOWER", "CATBITE", "RATBITE", "QUILLCHUNK", "QUILLSCRATCH", "HINDLEG",
        "BACK", "QUILLSIDE", "SCRATCHSIDE", "BEAKSIDE", "CATBITETWO", "FOUR"
	];

	public static readonly string[] Scars2 = [
		"LEFTEAR", "RIGHTEAR", "NOTAIL", "HALFTAIL", "NOPAW", "NOLEFTEAR", "NORIGHTEAR", "NOEAR"
	];

	public static readonly string[] Scars3 = [
		"SNAKE", "TOETRAP", "BURNPAWS", "BURNTAIL", "BURNBELLY", "BURNRUMP", "FROSTFACE", "FROSTTAIL",
        "FROSTMITT", "FROSTSOCK", "TOE", "SNAKETWO"
	];

	public static readonly string[] PlantAccessories = [];
	public static readonly string[] WildAccessories = [];
	public static readonly string[] TailAccessories = [];
	public static readonly string[] Collars = [];

	public static readonly string[] Tabbies = [ "Tabby", "Ticked", "Mackerel", "Classic", "Sokoke", "Agouti" ];
	public static readonly string[] Spotted = [ "Speckled", "Rosette" ];
	public static readonly string[] Plain = [ "SingleColour", "TwoColour", "Smoke", "Singlestripe" ];
	public static readonly string[] Exotic = [ "Bengal", "Marbled", "Masked" ];
	public static readonly string[] Torties = [ "Tortie", "Calico" ];
	public static readonly List<string[]> PeltCategories = [
		Tabbies,
		Spotted,
		Plain,
		Exotic,
		Torties
	];

	public static readonly string[] SingleColours = [
		"WHITE", "PALEGREY", "SILVER", "GREY", "DARKGREY", "GHOST", "BLACK", "CREAM", "PALEGINGER",
        "GOLDEN", "GINGER", "DARKGINGER", "SIENNA", "LIGHTBROWN", "LILAC", "BROWN", "GOLDEN-BROWN", "DARKBROWN",
        "CHOCOLATE"	
	];
	public static readonly string[] GingerColours = [ "CREAM", "PALEGINGER", "GOLDEN", "GINGER", "DARKGINGER", "SIENNA" ];
	public static readonly string[] BlackColours = [ "GREY", "DARKGREY", "GHOST", "BLACK" ];
	public static readonly string[] WhiteColours = [ "WHITE", "PALEGREY", "SILVER" ];
	public static readonly string[] BrownColours = [ "LIGHTBROWN", "LILAC", "BROWN", "GOLDEN-BROWN", "DARKBROWN", "CHOCOLATE" ];
	public static readonly List<string[]> ColourCategories = [
		GingerColours,
		BlackColours,
		WhiteColours,
		BrownColours
	];

	public static readonly string[] EyeSprites = [ 
		"YELLOW", "AMBER", "HAZEL", "PALEGREEN", "GREEN", "BLUE", "DARKBLUE", "BLUEYELLOW", "BLUEGREEN",
        "GREY", "CYAN", "EMERALD", "PALEBLUE", "PALEYELLOW", "GOLD", "HEATHERBLUE", "COPPER", "SAGE", "COBALT",
        "SUNLITICE", "GREENYELLOW", "BRONZE", "SILVER" 
	];

	public static readonly string[] LittleWhite = [
		"LITTLE", "LIGHTTUXEDO", "BUZZARDFANG", "TIP", "BLAZE", "BIB", "VEE", "PAWS",
        "BELLY", "TAILTIP", "TOES", "BROKENBLAZE", "LILTWO", "SCOURGE", "TOESTAIL", "RAVENPAW", "HONEY",
        "LUNA",
        "EXTRA", "MUSTACHE", "REVERSEHEART", "SPARKLE", "RIGHTEAR", "LEFTEAR", "ESTRELLA", "REVERSEEYE",
        "BACKSPOT",
        "EYEBAGS", "LOCKET", "BLAZEMASK", "TEARS"
	];
	public static readonly string[] MidWhite = [ 
		"TUXEDO", "FANCY", "UNDERS", "DAMIEN", "SKUNK", "MITAINE", "SQUEAKS", "STAR", "WINGS",
        "DIVA", "SAVANNAH", "FADESPOTS", "BEARD", "DAPPLEPAW", "TOPCOVER", "WOODPECKER", "MISS", "BOWTIE",
        "VEST",
        "FADEBELLY", "DIGIT", "FCTWO", "FCONE", "MIA", "ROSINA", "PRINCESS", "DOUGIE"
	];
	public static readonly string[] HighWhite = [ 
		"ANY", "ANYTWO", "BROKEN", "FRECKLES", "RINGTAIL", "HALFFACE", "PANTSTWO",
		"GOATEE", "PRINCE", "FAROFA", "MISTER", "PANTS", "REVERSEPANTS", "HALFWHITE", "APPALOOSA", "PIEBALD",
        "CURVED", "GLASS", "MASKMANTLE", "MAO", "PAINTED", "SHIBAINU", "OWL", "BUB", "SPARROW", "TRIXIE",
        "SAMMY", "FRONT", "BLOSSOMSTEP", "BULLSEYE", "FINN", "SCAR", "BUSTER", "HAWKBLAZE", "CAKE"
	];
	public static readonly string[] MostlyWhite = [ 
		"VAN", "ONEEAR", "LIGHTSONG", "TAIL", "HEART", "MOORISH", "APRON", "CAPSADDLE",
        "CHESTSPECK", "BLACKSTAR", "PETAL", "HEARTTWO", "PEBBLESHINE", "BOOTS", "COW", "COWTWO", "LOVEBUG",
		"SHOOTINGSTAR", "EYESPOT", "PEBBLE", "TAILTWO", "BUDDY", "KROPKA"
	];
	public static readonly string[] PointMarkings = [ 
		"COLOURPOINT", "RAGDOLL", "SEPIAPOINT", "MINKPOINT", "SEALPOINT"
	];
	public static readonly string[] Vit = [
		"VITILIGO", "VITILIGOTWO", "MOON", "PHANTOM", "KARPATI", "POWDER", "BLEACHED", "SMOKEY"
	];
	public static readonly List<string[]> WhitePatchSprites = [
		LittleWhite,
		MidWhite,
		HighWhite,
		MostlyWhite,
		PointMarkings,
		Vit,
		[ "FULLWHITE" ]
	];

	public static readonly string[] SkinSprites = [ 
		"BLACK", "PINK", "DARKBROWN", "BROWN", "LIGHTBROWN", "DARK", "DARKGREY", "GREY", "DARKSALMON",
		"SALMON", "PEACH", "DARKMARBLED", "MARBLED", "LIGHTMARBLED", "DARKBLUE", "BLUE", "LIGHTBLUE", "RED"	
	];

	//Actual Pelt Class
	public string Name;
	public string Length;
	public string Colour;
	public string? WhitePatches;
	public string EyeColour;
	public string? EyeColour2;
	public string? TortieBase;
	public string? TortieColour;
	public string? Pattern;
	public string? TortiePattern;
	public string? Vitiligo;
	public string? Points;
	public string? Accessory;
	public bool Paralyzed;
	public int Opacity;
	public List<string> Scars;
	public string? Tint;
	public string Skin;
	public string WhitePatchesTint;
	public Dictionary<string, int> CatSprites;
	public bool Reverse;

	public bool White { get { return WhitePatches != null || Points != null; } }

	public Pelt(
		string name = "SingleColour",
		string length = "short",
		string colour = "WHITE",
		string? whitePatches = null,
		string eyeColour = "BLUE",
		string? eyeColour2 = null,
		string? tortieColour = null,
		string? pattern = null,
		string? tortiePattern = null,
		string? vitiligo = null,
		string? points = null,
		string? accessory = null,
		bool paralyzed = false,
		int opacity = 100,
		List<string> scars = null,
		string? tint = null,
		string skin = "BLACK",
		string whitePatchesTint = "none",
		int kittenSprite = 0,
		int apprenticeSprite = 0,
		int adultSprite = 0,
		int seniorSprite = 0,
		int paralyzedAdultSprite = 0,
		bool reverse = false
	)
	{
		Name = name;
		Length = length;
		Colour = colour;
		WhitePatches = whitePatches;
		EyeColour = eyeColour;
		EyeColour2 = eyeColour2;
		TortieColour = tortieColour;
		Pattern = pattern;
		TortiePattern = tortiePattern;
		Vitiligo = vitiligo;
		Points = points;
		Accessory = accessory;
		Paralyzed = paralyzed;
		Opacity = opacity;
		Scars = scars;
		Tint = tint;
		Skin = skin;
		WhitePatchesTint = whitePatchesTint;
		CatSprites = new()
		{
			{ "kitten", kittenSprite },
			{ "adolescent", apprenticeSprite },
			{ "young adult", adultSprite },
			{ "adult", adultSprite },
			{ "senior adult", adultSprite },
			{ "senior", seniorSprite },
			{ "para_adult", paralyzedAdultSprite },
			{ "newborn", 20 },
			{ "para_young", 17 },
			{ "sick_adult", 18 },
			{ "sick_young", 19 },
		};
		Reverse = reverse;
	}

	

	public static Pelt GenerateNewPelt(string gender, List<Cat> parents, Age age = Age.Adult)
	{
		Pelt newPelt = new();

		// Milestones:
		bool peltWhite = newPelt.InitPatternColour(gender, parents);
		newPelt.InitWhitePatches(peltWhite, parents);
		newPelt.InitSprite();
		newPelt.InitScars(age);
		newPelt.InitAccessories(age);
		newPelt.InitEyes(parents);
		newPelt.InitPattern();
		newPelt.InitTint();

		return newPelt;
	}

	public bool RandomizePatternColour(string gender)
	{
		string chosenPelt = PeltCategories.PickRandom().PickRandom();

		int tortieChanceF = game.Config.CatGeneration.BaseFemaleTortie - 1;
		int tortieChanceM = game.Config.CatGeneration.BaseMaleTortie;

		bool torbie = false; //don't quite get the name, but sure.
		if (gender == "female") { torbie = GetRandBits(tortieChanceF) == 1; }
		else { torbie = GetRandBits(tortieChanceM) == 1; }

		string? chosenTortieBase = null;

		string[] singleColours = ["TwoColour", "SingleColour"];
		if (torbie)
		{
			chosenTortieBase = chosenPelt;
			if (singleColours.Contains(chosenTortieBase)) { chosenTortieBase = "single"; }
			chosenTortieBase = chosenTortieBase.ToLower();
			chosenPelt = Torties[Rand.Next(0, Torties.Length)];
		}

		string chosenPeltColour = ColourCategories.PickRandom().PickRandom();

		string chosenPeltLength = PeltLength.PickRandom();

		bool chosenWhite = Rand.Next(1, 100) <= 40;

		if (singleColours.Contains(chosenPelt))
		{
			if (chosenWhite) { chosenPelt = "TwoColour"; }
			else { chosenPelt = "SingleColour"; }
		}
		else if (chosenPelt == "Calico")
		{
			if (!chosenWhite) { chosenPelt = "Tortie"; }
		}

		Name = chosenPelt;
		Colour = chosenPeltColour;
		Length = chosenPeltLength;
		TortieBase = chosenTortieBase;

		return chosenWhite;
	}

	public bool PatternColourInheritance(string gender, List<Cat> parents)
	{
		HashSet<string?> peltLengths = [];
		HashSet<string?> peltColours = [];
		HashSet<string?> peltNames = [];
		List<Pelt> pelts = [];
		List<bool> white = [];
		foreach (Cat p in parents)
		{
			if (p != null)
			{
				peltLengths.Add(p.Pelt.Length);
				peltColours.Add(p.Pelt.Colour);

				if (Torties.Contains(p.Pelt.Name))
				{
					peltNames.Add(Capitalize().Replace(p.Pelt.TortieBase!, m => m.Value.ToUpper()));
				}
				else
				{
					peltNames.Add(p.Pelt.Name);
				}

				pelts.Add(p.Pelt);

				white.Add(p.Pelt.White);
			}
			else
			{
				white.Add(GetRandBits(1) == 1);

				peltColours.Add(null);
				peltLengths.Add(null);
				peltNames.Add(null);
			}
		}

		if (peltColours.Count <= 0)
		{
			Console.WriteLine("WARNING - No parents: pelt randomized.");
			return RandomizePatternColour(gender);
		}

		if (Rand.Next(0, game.Config.CatGeneration.DirectInheritance) == 0)
		{
			var selected = pelts.PickRandom();
			Name = selected.Name;
			Length = selected.Length;
			Colour = selected.Colour;
			TortieBase = selected.TortieBase;
			return selected.White;
		}

		int[] weights = [0, 0, 0, 0];
		int[] addWeights = [0, 0, 0, 0];
		foreach (string p in peltNames)
		{
			if (Tabbies.Contains(p))
			{
				addWeights = [50, 10, 5, 7];
			}
			else if (Spotted.Contains(p))
			{
				addWeights = [10, 50, 5, 5];
			}
			else if (Plain.Contains(p))
			{
				addWeights = [5, 5, 50, 0];
			}
			else if (Exotic.Contains(p))
			{
				addWeights = [35, 20, 30, 15];
			}

			for (int i = 0; i < weights.Length; i++)
			{
				weights[i] += addWeights[i];
			}
		}

		if (weights.All(x => x == 0))
		{
			weights = [1, 1, 1, 1];
		}

		string chosenPelt = PeltCategories.PickRandom().PickRandomWeighted([.. weights, 0]);

		int tortieChanceF = game.Config.CatGeneration.BaseFemaleTortie - 1;
		int tortieChanceM = game.Config.CatGeneration.BaseMaleTortie;

		foreach (Pelt p in pelts)
		{
			if (Torties.Contains(p.Name))
			{
				tortieChanceF /= 2;
				tortieChanceM -= 1;
				break;
			}
		}

		bool torbie = false; //don't quite get the name, but sure.
		if (gender == "female") { torbie = GetRandBits(tortieChanceF) == 1; }
		else { torbie = GetRandBits(tortieChanceM) == 1; }

		string? chosenTortieBase = null;

		string[] singleColours = ["TwoColour", "SingleColour"];
		if (torbie)
		{
			chosenTortieBase = chosenPelt;
			if (singleColours.Contains(chosenTortieBase)) { chosenTortieBase = "single"; }
			chosenTortieBase = chosenTortieBase.ToLower();
			chosenPelt = Torties[Rand.Next(0, Torties.Length)];
		}

		weights = [0, 0, 0, 0];
		foreach (string? p in peltColours)
		{
			if (GingerColours.Contains(p))
			{
				addWeights = [40, 0, 0, 10];
			}
			else if (BlackColours.Contains(p))
			{
				addWeights = [0, 40, 2, 5];
			}
			else if (WhiteColours.Contains(p))
			{
				addWeights = [0, 5, 40, 0];
			}
			else if (BrownColours.Contains(p))
			{
				addWeights = [10, 5, 0, 35];
			}
			else if (p == null)
			{
				addWeights = [40, 40, 40, 40];
			}

			for (int i = 0; i < weights.Length; i++)
			{
				weights[i] += addWeights[i];
			}
		}

		if (weights.All(x => x == 0))
		{
			weights = [1, 1, 1, 1];
		}

		var chosenPeltColour = ColourCategories.PickRandom().PickRandomWeighted(weights);

		weights = [0, 0, 0];
		foreach (string? p in peltLengths)
		{
			if (p == "short")
			{
				addWeights = [50, 10, 2];
			}
			else if (p == "medium")
			{
				addWeights = [25, 50, 25];
			}
			else if (p == "long")
			{
				addWeights = [2, 10, 50];
			}
			else if (p == null)
			{
				addWeights = [10, 10, 10];
			}

			for (int i = 0; i < weights.Length; i++)
			{
				weights[i] += addWeights[i];
			}
		}

		var chosenPeltLength = PeltLength.PickRandomWeighted(weights);

		int percentageAddPerParent = 94 / white.Count;
		int chance = 3;
		foreach (bool p in white)
		{
			if (p)
			{
				chance += percentageAddPerParent;
			}
		}

		var chosenWhite = Rand.Next(1, 100) <= chance;

		if (singleColours.Contains(chosenPelt))
		{
			if (chosenWhite) { chosenPelt = "TwoColour"; }
			else { chosenPelt = "SingleColour"; }
		}
		else if (chosenPelt == "Calico")
		{
			if (!chosenWhite) { chosenPelt = "Tortie"; }
		}

		Name = chosenPelt;
		Colour = chosenPeltColour;
		Length = chosenPeltLength;
		TortieBase = chosenTortieBase;

		return chosenWhite;
	}

	public bool InitPatternColour(string gender, List<Cat> parents)
	{
		if (parents.Count > 0)
		{
			return PatternColourInheritance(gender, parents);
		}
		else
		{
			return RandomizePatternColour(gender);
		}
	}

	public void WhitePatchesInheritance(List<Cat> parents)
	{
		HashSet<string> parentWhitePatches = new HashSet<string>();
		List<string?> parentPoints = [];
		foreach (var p in parents)
		{
			if (p != null)
			{
				if (p.Pelt.WhitePatches != null)
				{
					parentWhitePatches.Add(p.Pelt.WhitePatches);
				}
				if(p.Pelt.Points != null)
				{
					parentPoints.Add(p.Pelt.Points);
				}
			}
		}

		if (parents.Count <= 0)
		{
			RandomizeWhitePatches();
			return;
		}

		if (parentWhitePatches.Count > 0 && Rand.Next(0, game.Config.CatGeneration.DirectInheritance) == 0)
		{
			HashSet<string> temp = new(parentWhitePatches);

			if (Name == "Tortie")
			{
				for (int i = 0; i < temp.Count; i++)
				{
					if (HighWhite.Concat(MostlyWhite).Concat(["FULLWHITE"]).Contains(temp.ElementAt(i)))
					{
						temp.Remove(temp.ElementAt(i));
					}
				}
			}
			else if (Name == "Calico")
			{
				for (int i = 0; i < temp.Count; i++)
				{
					if (LittleWhite.Concat(MidWhite).Contains(temp.ElementAt(i)))
					{
						temp.Remove(temp.ElementAt(i));
					}
				}
			}
			
			if (temp.Count > 0)
			{
				WhitePatches = temp.PickRandom();

				if (parentPoints.Count > 0 && Name != "Tortie")
				{
					Points = parentPoints.PickRandom();
				}
				else { Points = null; }

				return;
			}	
		}

		int chance;
		if (parentPoints.Count > 0)
		{
			chance = 10 - parentPoints.Count;
		}
		else
		{
			chance = 40;
		}

		if (Name != "Tortie" && Rand.Next() * chance == 0)
		{
			Points = PointMarkings.PickRandom();
		}
		else
		{
			Points = null;
		}

		var whiteList = LittleWhite.Concat(MidWhite).Concat(HighWhite).Concat(MostlyWhite).Concat(["FULLWHITE"]);

		int[] weights = [0, 0, 0, 0, 0];
		int[] addWeights = [0, 0, 0, 0, 0];
		foreach (var p in parentWhitePatches)
		{
			if (LittleWhite.Contains(p))
			{
				addWeights = [40, 20, 15, 5, 0];
			}
			else if (MidWhite.Contains(p))
			{
				addWeights = [10, 40, 15, 10, 0];
			}
			else if (HighWhite.Contains(p))
			{
				addWeights = [15, 20, 40, 10, 1];
			}
			else if (MostlyWhite.Contains(p))
			{
				addWeights = [5, 15, 20, 40, 5];
			}
			else if (p == "FULLWHITE")
			{
				addWeights = [0, 5, 15, 40, 10];
			}
			else
			{
				addWeights = [0, 0, 0, 0, 0];
			}

			for (int i = 0; i < weights.Length; i++)
			{
				weights[i] += addWeights[i];
			}
		}

		if (weights.All(x => x == 0))
		{
			if (!parents.All(p => p != null))
			{
				weights = [20, 10, 10, 5, 0];
			}
			else
			{
				weights = [50, 5, 0, 0, 0];
			}
		}

		if (Name == "Tortie")
		{
			weights = [.. weights[..2], .. new int[0, 0, 0]];

			if (weights.All(x => x == 0))
			{
				weights = [2, 1, 0, 0, 0];
			}
		}
		else if (Name == "Calico")
		{
			weights = [.. new int[0, 0, 0], .. weights[3..]];

			if (weights.All(x => x == 0))
			{
				weights = [2, 1, 0, 0, 0];
			}
		}

		var chosenWhitePatches = whiteList.PickRandomWeighted(weights);

		WhitePatches = chosenWhitePatches;
		if (
			Points != null
			&& (
				HighWhite.Contains(WhitePatches)
				|| MostlyWhite.Contains(WhitePatches)
				|| WhitePatches == "FULLWHITE"
			)
		)
		{
			Points = null;
		}
	}

	public void RandomizeWhitePatches()
	{
		if (
			Name != "Tortie"
			&& GetRandBits(game.Config.CatGeneration.RandomPointChance) == 0
		)
		{
			Points = PointMarkings.PickRandom();
		}
		else { Points = null; }

		int[] weights = [0, 0, 0, 0, 0];
		if (Name == "Tortie")
		{
			weights = [2, 1, 0, 0, 0];
		}
		else if(Name == "Calico")
		{
			weights = [0, 0, 25, 15, 1];
		}
		else
		{
			weights = [10, 10, 10, 10, 1];
		}

		var chosenWhitePatches = WhitePatchSprites.PickRandom().PickRandomWeighted(weights);

		WhitePatches = chosenWhitePatches;
		if (
			Points != null 
			&& (
				HighWhite.Contains(WhitePatches) 
				|| MostlyWhite.Contains(WhitePatches) 
				|| WhitePatches == "FULLWHITE"
			)
		)
		{
			Points = null;
		}
	}

	public void InitWhitePatches(bool peltWhite, List<Cat> parents)
	{
		List<string> parentVit = [];
		foreach (var p in parents)
		{
			if (p != null && p.Pelt.Vitiligo != null)
			{
				parentVit.Add(p.Pelt.Vitiligo);
			}
		}

		int vitChance = Math.Max(game.Config.CatGeneration.VitChance - parentVit.Count, 0);
		if (GetRandBits(vitChance) == 0)
		{
			Vitiligo = Vit.PickRandom();
		}

		if (peltWhite)
		{
			if (parents.Count > 0)
			{
				WhitePatchesInheritance(parents);
			}
			else
			{
				RandomizeWhitePatches();
			}
		}
		else
		{
			WhitePatches = null;
			Points = null;
		}
	}

	public void InitSprite()
	{
		CatSprites = new Dictionary<string, int>{
			{ "newborn", 20 },
			{ "kitten", Rand.Next(0, 2) },
			{ "adolescent", Rand.Next(3, 5) },
			{ "senior", Rand.Next(12, 14) },
			{ "sick_young", 19 },
			{ "sick_adult", 18 },
		};

		Reverse = GetRandBits(1) == 1;

		Skin = SkinSprites.PickRandom();

		if (Length != "long")
		{
			CatSprites.Add("adult", Rand.Next(6, 8));
			CatSprites.Add("para_adult", 16);
		}
		else
		{
			CatSprites.Add("adult", Rand.Next(9, 11));
			CatSprites.Add("para_adult", 15);
		}
		CatSprites.Add("young adult", CatSprites["adult"]);
		CatSprites.Add("senior adult", CatSprites["adult"]);
	}

	public void InitScars(Age age)
	{
		if (age == Age.Newborn) { return; }

		int scarChoice;
		if (age == (Age.Kitten | Age.Adolescent))
		{
			scarChoice = Rand.Next(0, 50);
		}
		else if (age == (Age.YoungAdult | Age.Adult))
		{
			scarChoice = Rand.Next(0, 20);
		}
		else
		{
			scarChoice = Rand.Next(0, 15);
		}

		if (scarChoice == 1)
		{
			Scars.Add(new string[] { Scars1.PickRandom(), Scars3.PickRandom() }.PickRandom());
		}

		if (Scars.Contains("NOTAIL") && Scars.Contains("HALFTAIL"))
		{
			Scars.Remove("HALFTAIL");
		}
	}

	public void InitAccessories(Age age)
	{
		if (age == Age.Newborn) { Accessory = null; return; }

		int accDisplayChoice;
		if (age == (Age.Kitten | Age.Adolescent))
		{
			accDisplayChoice = Rand.Next(0, 80);
		}
		else if (age == (Age.YoungAdult | Age.Adult))
		{
			accDisplayChoice = Rand.Next(0, 180);
		}
		else
		{
			accDisplayChoice = Rand.Next(0, 100);
		}

		if (accDisplayChoice == 1)
		{
			Accessory = new string[] { PlantAccessories.PickRandom(), WildAccessories.PickRandom() }.PickRandom();
		}
		else
		{
			Accessory = null;
		}
	}

	private void InitEyes(List<Cat> parents)
	{
		if (parents.Count <= 0)
		{
			EyeColour = EyeColours.PickRandom();
		}
		else
		{
			List<string> tempList = [.. EyeColours];
			parents.ForEach(p => tempList.Add(p.Pelt.EyeColour));
			EyeColour = tempList.PickRandom();
		}

		int num = game.Config.CatGeneration.BaseHeterochromia;
		if (HighWhite.Concat(MostlyWhite).Concat(["FULLWHITE"]).Contains(WhitePatches) || Colour == "WHITE")
		{
			num -= 90;
		}
		if (WhitePatches == "FULLWHITE" || Colour == "WHITE")
		{
			num -= 10;
		}
		foreach (var p in parents)
		{
			if (p.Pelt.EyeColour2 != null)
			{
				num -= 10;
			}
		}

		if (num < 0) { num = 1; }

		if (Rand.Next(0, num) == 0)
		{
			List<string[]> colourWheel = [YellowEyes, BlueEyes, GreenEyes];
			foreach (var colour in colourWheel)
			{
				if (colour.Contains(EyeColour))
				{
					colourWheel.Remove(colour);
					EyeColour2 = colourWheel.PickRandom().PickRandom();
					break;
				}
			}
		}
	}

	public void InitPattern()
	{
		if (Torties.Contains(Name))
		{
			TortieBase ??= TortieBases.PickRandom();
			Pattern ??= TortiePatterns.PickRandom();


			int wildcardChance = game.Config.CatGeneration.WildcardTortie;

			if (Colour != null)
			{
				if (wildcardChance == 0 || GetRandBits(wildcardChance) == 1)
				{
					Console.WriteLine("Wildcard tortie!"); //might as well keep it.

					TortiePattern = TortieBases.PickRandom();

					List<string> possibleColours = [.. PeltColours];
					possibleColours.Remove(Colour);
					TortieColour = possibleColours.PickRandom();
				}
				else
				{
					string[] baseInclusions = ["singlestripe", "smoke", "single"];
					string[] patternChoices = [
						"tabby",
						"mackerel",
						"classic",
						"single",
						"smoke",
						"agouti",
						"ticked"
					];
					if (baseInclusions.Contains(TortieBase))
					{
						TortiePattern = patternChoices.PickRandom();
					}
					else
					{
						TortiePattern = new string[] { TortieBase!, "single" }.PickRandomWeighted([97, 3]);
					}

					if (Colour == "WHITE")
					{
						List<string> possibleColours = [.. PeltColours];
						possibleColours.Remove(Colour);
						Colour = possibleColours.PickRandom();
					}

					if (BlackColours.Contains(Colour) || WhiteColours.Contains(Colour))
					{
						TortieColour = GingerColours.Concat(GingerColours).Concat(BrownColours).PickRandom();
					}
					else if (GingerColours.Contains(Colour))
					{
						TortieColour = BrownColours.Concat(BlackColours).Concat(BlackColours).PickRandom();
					}
					else if (BrownColours.Contains(Colour))
					{
						List<string> possibleColours = [.. BrownColours];
						possibleColours.Remove(Colour);
						possibleColours = [.. possibleColours, .. BlackColours, .. GingerColours, .. GingerColours];
						Colour = possibleColours.PickRandom();
					}
					else
					{
						TortieColour = "GOLDEN";
					}
				}
			}
			else
			{
				TortieColour = "GOLDEN";
			}
		}
		else
		{
			TortieBase = null;
			TortiePattern = null;
			TortieColour = null;
			Pattern = null;
		}
	}

	public void InitTint()
	{
		/*To be implemented
		 Reason: Requires Sprite class.
		 */
	}

	[GeneratedRegex(@"\b([a-z])")]
	private static partial Regex Capitalize();
}
