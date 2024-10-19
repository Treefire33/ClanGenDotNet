using static ClanGenDotNet.Scripts.Game_Structure.Game;

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

	public static readonly Pronouns[] DefaultPronouns = [
		Pronouns.TheyThemTheirs,
		Pronouns.SheHerHers,
		Pronouns.HeHimHis
	];

	public static Dictionary<string, Cat> AliveCats = [];
	public static Dictionary<string, Cat> OutsideCats = [];

	public static List<Cat> AliveCatsList = [];
	public static List<Cat> OrderedCatList = [];

	private static int _idIter = 0;

	//Actual Cat Class
	public string Prefix;
	public string Gender;
	public string Status;
	public string Backstory;
	public string Parent1;
	public string Parent2;
	public string Suffix;
	public bool SpecialSuffixHidden;
	public string ID;
	public int Moons;
	public bool Example;
	public bool Faded;
	public SkillDict SkillDict;
	public Pelt Pelt;
	public bool LoadingCat;

	public History? History;

	private Cat _mentor;
	private int _experience;
	private int _moons;

	public Cat(
		 string prefix,
		 string gender,
		 string status,
		 string backstory,
		 string parent1,
		 string parent2,
		 string suffix,
		 bool specialSuffixHidden,
		 string id,
		 int moons,
		 bool example,
		 bool faded,
		 SkillDict skillDict,
		 Pelt pelt,
		 bool loadingCat,
		 Dictionary<string, object> keywordArgs
	)
	{
		History = null;

		if (faded)
		{
			//InitFaded(id, status, prefix, suffix, moons, keywordArgs);
			return;
		}

		//GenerateEvents = new GenerateEvents();
		//I'll write the rest later
	}
}

//Putting it here for reasons.
public class SkillDict
{
	public string Primary = "HUNTER";
	public string? Secondary;
	public string? Hidden;
}
