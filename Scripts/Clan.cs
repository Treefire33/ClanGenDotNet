using ClanGenDotNet.Scripts.Cats;

namespace ClanGenDotNet.Scripts;

public enum BiomeType
{
	Forest,
	Plains,
	Mountainous,
	Beach
}

public enum CatType
{
	Newborn,
	Kitten,
	Apprentice,
	Warrior,
	Medicine,
	Deputy,
	Leader,
	Elder,
	Mediator,
	General
}

public class Clan
{
	public int LeaderLives = 0;

	public List<Cat> ClanCats = [];
	public List<Cat> StarClanCats = [];
	public List<Cat> DarkForestCats = [];
	public List<Cat> UnknownCats = [];

	public readonly List<string> Seasons = [
		"Newleaf",
		"Newleaf",
		"Newleaf",
		"Greenleaf",
		"Greenleaf",
		"Greenleaf",
		"Leaf-fall",
		"Leaf-fall",
		"Leaf-fall",
		"Leaf-bare",
		"Leaf-bare",
		"Leaf-bare",
	];

	public readonly Dictionary<string, List<string>> TemperamentDict = new()
	{
		{ "low_social", ["cunning", "proud", "bloodthirsty"] },
		{ "mid_social", ["amiable", "stoic", "wary"] },
		{ "high_social", ["gracious", "mellow", "logical"] },
	};

	public int Age = 0;
	public string CurrentSeason = "Newleaf";
	public List<Clan> AllClans = [];

	public History History = new();

	public string Name = "";
	public Cat? Leader = null;
	public int LeaderPredecessors = 0;
	public Cat? Deputy = null;
	public int DeputyPredecessors = 0;
	public Cat? MedicineCat = null;
	public List<Cat> MedCatList = [];
	public int MedCatPredecessors = 0;
	public int MedCatNumber = 0;

	public BiomeType Biome = BiomeType.Forest;
	public object? CampBackground = null;
	public string StartingSeason = "";
	public Texture2D? ChosenSymbol = null;

	public Cat? Instructor = null;
	public string GameMode = "classic";

	public Clan(
		string name = "",
		Cat? leader = null,
		Cat? deputy = null,
		Cat? medicineCat = null,
		BiomeType biome = BiomeType.Forest,
		object? campBackground = null,
		Texture2D? symbol = null,
		string gameMode = "classic",
		List<Cat>? startingMembers = null,
		string startingSeason = "Newleaf",
		bool selfRunInitFunctions = true
	)
	{
		if (name == "") { return; }

		Name = name;
		Leader = leader;
		LeaderLives = 9;
		Deputy = deputy;
		MedicineCat = medicineCat;
		MedCatNumber = MedCatList.Count;

		Biome = biome;
		CurrentSeason = "Newleaf";
		StartingSeason = startingSeason;

		CampBackground = campBackground;
		ChosenSymbol = symbol;
		GameMode = gameMode;

	}
}
