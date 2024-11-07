using ClanGenDotNet.Scripts.Cats;
using Newtonsoft.Json;
using System.Collections.Immutable;

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

	public List<string> ClanCats = [];
	public List<string> StarClanCats = [];
	public List<string> DarkForestCats = [];
	public List<string> UnknownCats = [];

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

	public static readonly ImmutableDictionary<
		string,
		Dictionary<string, dynamic>
	> Layouts = JsonConvert.DeserializeObject<
		ImmutableDictionary<
			string,
			Dictionary<string, dynamic>
		>
	>(File.ReadAllText(".\\Resources\\placements.json"))!;

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
	public List<Cat> StartingMembers = [];

	public BiomeType Biome = BiomeType.Forest;
	public string? CampBackground = null;
	public string StartingSeason = "";
	public string? ChosenSymbol = null;

	public Cat? Instructor = null;
	public string GameMode = "classic";

	public List<string> FadedIds = [];
	private static readonly string[] _instructorChoices = [
		"apprentice",
		"mediator apprentice",
		"medicine cat apprentice",
		"warrior",
		"medicine cat",
		"leader",
		"mediator",
		"deputy",
		"elder"
	];


	public Clan(
		string name = "",
		Cat? leader = null,
		Cat? deputy = null,
		Cat? medicineCat = null,
		BiomeType biome = BiomeType.Forest,
		string? campBackground = null,
		string? symbol = null,
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

		//clan settings here

		StartingMembers = startingMembers!;
	}

	public void CreateClan()
	{
		Instructor = new(
			status: _instructorChoices.PickRandom()
		)
		{
			Dead = true,
			DeadFor = Rand.Next(20, 200)
		};
		AddCat(Instructor);
		//AddToStarClan(Instructor);
		AllClans = [];

		foreach (var id in Cat.AllCats.Keys)
		{
			bool notFound = true;
			var kitty = Cat.AllCats[id];

			foreach (var x in StartingMembers)
			{
				if (kitty == x)
				{
					AddCat(kitty);
					notFound = false;
				}
			}

			if (
				kitty != Leader
				&& kitty != MedicineCat
				&& kitty != Deputy
				&& kitty != Instructor
				&& notFound
			)
			{
				Cat.AllCats[id].Example = true;
				RemoveCat(Cat.AllCats[id].ID);
			}
		}

		foreach (string id in Cat.AllCats.Keys)
		{
			//Cat.AllCats[id].InitAllRelationships();
			Cat.AllCats[id].Backstory = "clan_founder";
			if (Cat.AllCats[id].Status == "apprentice")
			{
				//Cat.AllCats[id].StatusChange("apprentice");
			}
			//Cat.AllCats[id].Thoughts();
		}
	}

	public void AddCat(Cat meowmeow)
	{
		if (Cat.AllCats.ContainsKey(meowmeow.ID) && !ClanCats.Contains(meowmeow.ID))
		{
			ClanCats.Add(meowmeow.ID);
		}
	}

	public void RemoveCat(string id)
	{
		
	}

	public static BiomeType StringToBiome(string biome)
	{
		return biome.ToLower() switch
		{
			"forest" => BiomeType.Forest,
			"mountainous" => BiomeType.Mountainous,
			"plains" => BiomeType.Plains,
			"beach" => BiomeType.Beach,
			_ => BiomeType.Forest
		};
	}
}
