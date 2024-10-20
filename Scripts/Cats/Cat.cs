namespace ClanGenDotNet.Scripts.Cats;

public enum Age
{
	Newborn,
	Kitten,
	Adolescent,
	YoungAdult,
	Adult,
	SeniorAdult,
	Senior
}

public class Cat
{
	public static List<Cat> DeadCats = [];

	public static readonly Dictionary<Age, List<int>> AgeMoons = new() {
		{ Age.Newborn, game.Config.CatAges.Newborn },
		{ Age.Kitten, game.Config.CatAges.Kitten },
		{ Age.Adolescent, game.Config.CatAges.Adolescent },
		{ Age.YoungAdult, game.Config.CatAges.YoungAdult },
		{ Age.Adult, game.Config.CatAges.Adult },
		{ Age.SeniorAdult, game.Config.CatAges.SeniorAdult },
		{ Age.Senior, game.Config.CatAges.Senior },
	};

	public static readonly string[] RankSortOrder = [
		"newborn",
		"kitten",
		"elder",
		"apprentice",
		"warrior",
		"mediator apprentice",
		"mediator",
		"medicine cat apprentice",
		"medicine cat",
		"deputy",
		"leader"
	];

	public static readonly Dictionary<string, string> GenderTags = new() {
		{ "female", "F" },
		{ "male", "M" }
	};

	//Inclusive to both minimum and maximum.
	public static readonly Dictionary<string, (int, int)> ExperienceLevelRanges = new() {
		{ "untrained", (0, 0) },
		{ "trainee", (1, 50) },
		{ "prepared", (51, 110) },
		{ "competent", (111, 170) },
		{ "proficient", (171, 240) },
		{ "expert", (241, 320) },
		{ "master", (321, 321) }
	};

	public static readonly Pronouns[] DefaultPronouns = [
		Pronouns.TheyThemTheirs,
		Pronouns.SheHerHers,
		Pronouns.HeHimHis
	];

	public static Dictionary<string, Cat> AllCats = [];
	public static Dictionary<string, Cat> OutsideCats = [];

	public static List<Cat> AliveCatsList = [];
	public static List<Cat> OrderedCatList = [];

	private static int _idIter = 0;

	//Actual Cat Class
	public string? Prefix;
	public string? Gender;
	public string Status;
	public string Backstory;
	public Age Age;
	public CatSkills CatSkills;
	public Personality Personality;
	public string? Parent1;
	public string? Parent2;
	public List<string> AdoptiveParents;
	public List<string> FormerMentors;
	public int PatrolWithMentor;
	public List<string> Apprentices;
	public List<string> FormerApprentices;
	//public Dictionary<?, ?> Relationships;
	public List<string> Mates;
	public List<string> PreviousMates;
	public List<Pronouns> PronounsList;
	public (int, int)? Placement; //Tuple instead of Vector2 for nullification purposes.
	public string? Suffix;
	public bool SpecialSuffixHidden;
	public string ID;
	public int Moons;
	public bool Example;
	public bool Faded;
	public bool Dead;
	public bool Exiled;
	public bool Outside;
	public bool DrivenOut;
	public int DeadFor;
	public string Thought;
	public string? GenderAlignment;
	public int BirthCooldown;
	//public Dictionary<?, ?> Illnesses;
	//public Dictionary<?, ?> Injuries;
	public bool HealedCondition;
	public bool LeaderDeathHeal;
	public bool AlsoGot;
	//public Dictionary<?, ?> PermanentCondition;
	public bool DarkForest;
	public string? ExperienceLevel;
	public SkillDict SkillDict;
	public Pelt Pelt;
	public bool LoadingCat;

	public bool NoKits;
	public bool NoMates;
	public bool NoRetirement;
	public bool PreventFading;
	public bool Favourite;
	//public Inheritance Inheritance;

	public History? History;

	private Cat? _mentor;
	private int _experience;
	private int _moons;

	private Image _sprite;
	public Image Sprite
	{
		get
		{
			UpdateSprite(cat: this);
			return _sprite;
		}

		set
		{
			_sprite = value;
		}
	}

	public Cat(
		 string? prefix = null,
		 string? gender = null,
		 string status = "newborn",
		 string backstory = "clanborn",
		 string? parent1 = null,
		 string? parent2 = null,
		 string? suffix = null,
		 bool specialSuffixHidden = false,
		 string? id = null,
		 int? moons = null,
		 bool example = false,
		 bool faded = false,
		 SkillDict? skillDict = null,
		 Pelt? pelt = null,
		 bool loadingCat = false,
		 Dictionary<string, object?>? keywordArgs = null
	)
	{
		if (faded)
		{
			//InitFaded(id, status, prefix, suffix, moons, keywordArgs);
			return;
		}

		//GenerateEvents = new GenerateEvents();
		_mentor = null;
		_moons = -1;
		_experience = -1;

		Gender = gender;
		Status = status;
		Backstory = backstory;
		Age = Age.Newborn;
		Parent1 = parent1;
		Parent2 = parent2;
		AdoptiveParents = [];
		Pelt = pelt ?? new Pelt();
		FormerMentors = [];
		PatrolWithMentor = 0;
		Apprentices = [];
		FormerApprentices = [];
		//Set Relationships to empty dict
		Mates = [];
		PreviousMates = [];
		PronounsList = [ DefaultPronouns[0] ];
		Placement = null;
		Example = example;
		Dead = false;
		Exiled = false;
		Outside = false;
		DrivenOut = false;
		DeadFor = 0;
		Thought = "";
		GenderAlignment = null;
		BirthCooldown = 0;
		//set Illnesses to empty dict
		//set Injuries to empty dict
		HealedCondition = false;
		LeaderDeathHeal = false;
		AlsoGot = false;
		//set PermanentCondition to empty dict
		DarkForest = false;
		ExperienceLevel = null;

		NoKits = false;
		NoMates = false;
		NoRetirement = false;

		PreventFading = false;

		Faded = faded;

		Favourite = false;
		SpecialSuffixHidden = specialSuffixHidden;
		//Inheritance = null;

		History = null;

		if (id is null)
		{
			string potentialId = (_idIter++).ToString();

			List<string> fadedIds = [];
			if (game.Clan is not null)
			{
				fadedIds = game.Clan.FadedIds;
			}

			while (AllCats.ContainsKey(potentialId) || fadedIds.Contains(potentialId))
			{
				potentialId = (_idIter++).ToString();
			}
			ID = potentialId;
		}
		else
		{
			ID = id;
		}
	}

	public bool NotWorking()
	{
		//come back to this.
		return false;
	}

	public static Cat CreateCat(string status, int? moons = null, string? biome = null)
	{
		Cat newCat = new(status: status, keywordArgs: new() { { "biome", biome } });

		if (moons != null)
		{
			newCat.Moons = (int)moons;
		}
		else
		{
			if (moons >= 160)
			{
				newCat.Moons = Rand.Next(120, 155);
			}
			else if (moons == 0)
			{
				newCat.Moons = Rand.Next(1, 5);
			}
		}

		string[] cantGenerateWithScars = [
			"NOPAW",
			"NOTAIL",
			"HALFTAIL",
			"NOEAR",
			"BOTHBLIND",
			"RIGHTBLIND",
			"LEFTBLIND",
			"BRIGHTHEART",
			"NOLEFTEAR",
			"NORIGHTEAR",
			"MANLEG",
		];

		for (int i = 0; i < newCat.Pelt.Scars?.Count; i++)
		{
			string scar = newCat.Pelt.Scars[i];
			if (cantGenerateWithScars.Contains(scar))
			{
				newCat.Pelt.Scars.Remove(scar);
			}
		}

		return newCat;
	}

	public static void CreateExampleCats()
	{
		List<int> warriorIndices = Enumerable.Range(1, 12).Sample(3).ToList();

		for (int i = 0; i < 12; i++)
		{
			if (warriorIndices.Contains(i))
			{
				game.ChooseCats[i] = CreateCat(status: "warrior");
			}
			else
			{
				List<string> statuses = ["kitten", "apprentice", "warrior", "warrior", "elder"];
				game.ChooseCats[i] = CreateCat(status: statuses.PickRandom());
			}
		}
	}
}

//Putting it here for reasons.
public class SkillDict
{
	public string Primary = "HUNTER";
	public string? Secondary;
	public string? Hidden;
}
