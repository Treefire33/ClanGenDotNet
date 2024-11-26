using ClanGenDotNet.Scripts.Cats;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
	private static readonly List<string> _seasons = [
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
		Layout
	> Layouts = JsonConvert.DeserializeObject<
		ImmutableDictionary<
			string,
			Layout
		>
	>(File.ReadAllText(".\\Resources\\placements.json"))!;

	[JsonIgnore]
	public List<OtherClan> AllClans = [];
	public History History = new();

	public string Name = "";
	public int Age = 0;

	[JsonIgnore]
	public BiomeType Biome = BiomeType.Forest;

	public string StartingSeason = "Newleaf";
	public string CurrentSeason = "Newleaf";
	public string? CampBackground = null;
	public string GameMode = "classic";

	public int Reputation;
	public string Temperament;

	[JsonIgnore]
	public Cat? Instructor = null;

	[JsonIgnore]
	public Cat? Leader = null;
	public int LeaderPredecessors = 0;
	public int LeaderLives = 9;

	[JsonIgnore]
	public Cat? Deputy = null;
	public int DeputyPredecessors = 0;

	[JsonIgnore]
	public Cat? MedicineCat = null;
	public List<Cat> MedCatList = [];
	public int MedCatPredecessors = 0;
	public int MedCatNumber = 0;

	[JsonIgnore]
	public List<Cat> StartingMembers = [];

	public string? ChosenSymbol = null;

	public List<string> FadedIds = [];
	public List<string> ClanCats = [];
	public List<string> StarClanCats = [];
	public List<string> DarkForestCats = [];
	public List<string> UnknownCats = [];

	public int VersionName;
	public string VersionCommit;
	public bool SourceBuild;
	
	public List<Pronouns> CustomPronouns = [];
	public War War;

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

	[JsonConstructor]
	public Clan(
		string clanname,
		int clanage,
		string biome,
		string camp_bg,
		string clan_symbol,
		string gamemode,
		string last_focus_change,
		List<string> clans_in_focus,
		string instructor,
		int reputation,
		List<string> mediated,
		string? starting_season,
		string temperament,
		int version_name,
		string version_commit,
		bool source_build,
		List<Pronouns> custom_pronouns,
		string leader,
		int leader_lives,
		int leader_predecessors,
		string deputy,
		int deputy_predecessors,
		string med_cat,
		int med_cat_number,
		int med_cat_predecessors,
		string clan_cats,
		string faded_cats,
		List<string> patrolled_cats,
		List<OtherClan> other_clans,
		War war
	)
	{
		Name = clanname;
		Age = clanage;
		_ = Enum.TryParse(biome, out Biome);
		CampBackground = camp_bg;
		ChosenSymbol = clan_symbol;
		GameMode = gamemode;
		VersionName = version_name;
		VersionCommit = version_commit;
		SourceBuild = source_build;

		AllClans = other_clans;

		Leader ??= Cat.AllCats[leader];
		LeaderLives = leader_lives;
		LeaderPredecessors = leader_predecessors;
		
		Deputy ??= Cat.AllCats[leader];
		DeputyPredecessors = deputy_predecessors;
		
		MedicineCat ??= Cat.AllCats[med_cat];
		MedCatPredecessors = med_cat_predecessors;
		MedCatNumber = med_cat_number;
		
		Reputation = reputation;
		Age = clanage;
		StartingSeason = starting_season ?? "Newleaf";
		GetCurrentSeason();
		
		CustomPronouns = custom_pronouns;
		
		Instructor = Cat.AllCats[instructor];
		AddCat(Instructor);
		
		ChosenSymbol = clan_symbol;

		foreach (string id in clan_cats.Split(','))
		{
			if (Cat.AllCats.TryGetValue(id, out Cat? cat))
			{
				AddCat(cat);
			}
		}

		War = war;

		if (faded_cats != "")
		{
			FadedIds.AddRange(faded_cats.Split(','));
		}
		
		//implement rest when needed
	}

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
		LeaderPredecessors = 0;
		Deputy = deputy;
		DeputyPredecessors = 0;
		MedicineCat = medicineCat;
		MedCatPredecessors = 0;
		MedCatNumber = MedCatList.Count;
		
		//Herbs = new();

		Age = 0;
		CurrentSeason = "Newleaf";
		StartingSeason = startingSeason;
		Instructor = null;

		Biome = biome;
		CampBackground = campBackground;
		ChosenSymbol = symbol;
		GameMode = gameMode;
		
		CustomPronouns = [];

		//clan settings here

		StartingMembers = startingMembers!;
		War = new();

		FadedIds = [];
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

	private void GetCurrentSeason()
	{
		if (game.Config.LockSeason)
		{
			CurrentSeason = StartingSeason;
			return;
		}
		
		Dictionary<string, int> modifiers = new Dictionary<string, int>()
		{
			{ "Newleaf", 0 },
			{ "Greenleaf", 3 },
			{ "Leaf-fall", 6 },
			{ "Leaf-bare", 9 }
		};
		var index = Age % 12 + modifiers[StartingSeason];
		
		if (index > 11) { index -= 12; }
		
		CurrentSeason = _seasons[index];
	}

	public static string LoadClan()
	{
		string versionInfo = "0.null.0";

		try
		{
			if (File.Exists($"{GetSaveDirectory()}\\{game.Switches["clan_list"]![0]}clan.json"))
			{
				game.Clan = JsonConvert.DeserializeObject<Clan>(
					File.ReadAllText($"{GetSaveDirectory()}\\{game.Switches["clan_list"]![0]}clan.json")
				);
			}
			else
			{
				Console.WriteLine($"Error: Failed to find {game.Switches["clan_list"]![0]}clan.json");
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"Error: Failed to load {game.Switches["clan_list"]![0]}clan.json");
			Console.WriteLine(e);
		}

		versionInfo = $"{game.Clan!.VersionName}.{game.Clan.VersionCommit}.{game.Clan.SourceBuild}";

		return versionInfo;
	}
}

public class War
{
	[JsonProperty("at_war")] public bool AtWar;
	
	[JsonProperty("enemy")] public string Enemy = "";
	
	[JsonProperty("duration")] public int Duration;
}
