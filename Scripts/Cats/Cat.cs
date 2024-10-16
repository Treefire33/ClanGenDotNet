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

	public Dictionary<Age, List<int>> AgeMoons = new()
	{
		{ Age.Newborn, game.Config.CatAges.Newborn },
		{ Age.Kitten, game.Config.CatAges.Kitten },
		{ Age.Adolescent, game.Config.CatAges.Adolescent },
		{ Age.YoungAdult, game.Config.CatAges.YoungAdult },
		{ Age.Adult, game.Config.CatAges.Adult },
		{ Age.SeniorAdult, game.Config.CatAges.SeniorAdult },
		{ Age.Senior, game.Config.CatAges.Senior },
	};


}
