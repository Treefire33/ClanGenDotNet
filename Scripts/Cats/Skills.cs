namespace ClanGenDotNet.Scripts.Cats;

public enum SkillPath
{
	TEACHER = 1,
	HUNTER,
	FIGHTER,
	RUNNER,
	CLIMBER,
	SWIMMER,
	SPEAKER,
	MEDIATOR,
	CLEVER,
	INSIGHTFUL,
	SENSE,
	KIT,
	STORY,
	LORE,
	CAMP,
	HEALER,
	STAR,
	DARK,
	OMEN,
	DREAM,
	CLAIRVOYANT,
	PROPHET,
	GHOST
}

public enum HiddenSkill
{ 
	ROGUE,
	LONER,
	KITTYPET
}

[Flags]
public enum SkillType
{
	SUPERNATURAL = 1,
	STRONG = 2,
	AGILE = 3,
	SMART = 4,
	OBSERVANT = 5,	
	SOCIAL = 6
}

public static class SkillPathMethods
{
	public static string GetProficiencyString(this SkillPath skill, int proficiency)
	{
		return proficiency switch
		{
			1 => "not implemented",
			2 => "not implemented",
			3 => "not implemented",
			4 => "not implemented",
			_ => "not implemented",
		};
	}

	public static SkillPath GetRandom(List<SkillPath> exclude)
	{
		List<SkillPath> uncommonPaths = [];
		SkillPath[] paths = [ 
			SkillPath.GHOST, 
			SkillPath.PROPHET,
			SkillPath.CLAIRVOYANT,
			SkillPath.DREAM,
			SkillPath.OMEN,
			SkillPath.STAR,
			SkillPath.HEALER,
			SkillPath.DARK
		];
		foreach (SkillPath skill in paths)
		{
			if (exclude.Contains(skill)) { continue; }
			uncommonPaths.Add(skill);
		}

		if (Rand.NextDouble() * 15 == 0)
		{
			return uncommonPaths.PickRandom();
		}
		else
		{
			List<SkillPath> commonPaths = [];
			foreach (SkillPath skill in Enum.GetValues<SkillPath>().Cast<SkillPath>().ToArray())
			{
				if (exclude.Contains(skill) || uncommonPaths.Contains(skill)) { continue; }
				commonPaths.Add(skill);
			}
			return commonPaths.PickRandom();
		}
	}
}

public class Skill
{
	public static readonly (int, int)[] TierRanges = [(0, 9), (10, 19), (20, 29)];
	public static readonly (int, int) PointRange = (0, 29);

	public  static Dictionary<SkillPath, string> ShortStrings = new()
	{
		{ SkillPath.TEACHER, "teaching" },
		{ SkillPath.HUNTER, "hunting" },
		{ SkillPath.FIGHTER, "fighting" },
		{ SkillPath.RUNNER, "running" },
		{ SkillPath.CLIMBER, "climbing" },
		{ SkillPath.SWIMMER, "swimming" },
		{ SkillPath.SPEAKER, "speaking" },
		{ SkillPath.MEDIATOR, "mediating" },
		{ SkillPath.CLEVER, "clever" },
		{ SkillPath.INSIGHTFUL, "advising" },
		{ SkillPath.SENSE, "observing" },
		{ SkillPath.KIT, "caretaking" },
		{ SkillPath.STORY, "storytelling" },
		{ SkillPath.LORE, "lorekeeping" },
		{ SkillPath.CAMP, "campkeeping" },
		{ SkillPath.HEALER, "healing" },
		{ SkillPath.STAR, "StarClan" },
		{ SkillPath.OMEN, "omen" },
		{ SkillPath.DREAM, "dreaming" },
		{ SkillPath.CLAIRVOYANT, "predicting" },
		{ SkillPath.PROPHET, "prophesying" },
		{ SkillPath.GHOST, "ghosts" },
		{ SkillPath.DARK, "dark forest" }
	};

	//Actual Skill class
	public SkillPath Path;
	private int _points;
	public int Points 
	{ 
		get { return _points; }
		set 
		{
			if (value > PointRange.Item2)
			{
				_points = PointRange.Item2;
			}
			else if (value < PointRange.Item1)
			{
				_points = PointRange.Item1;
			}
			else
			{
				_points = value;
			}
		}
	}
	public bool InterestOnly;
	public int Tier
	{
		get
		{
			if (InterestOnly)
			{
				return 0;
			}
			int i = 0;
			foreach ((int, int) range in TierRanges)
			{
				if ((range.Item1 <= Points) && (Points <= range.Item2))
				{
					return i;
				}
				i++;
			}

			return 1;
		}
	}
	public string SkillProficiency
	{
		get
		{
			return Path.GetProficiencyString(Tier);
		}
	}

	public Skill(SkillPath path, int points = 0, bool interestOnly = false)
	{
		Path = path;
		InterestOnly = interestOnly;
		if (points > PointRange.Item2)
		{
			_points = PointRange.Item2;
		}
		else if (points < PointRange.Item1)
		{
			_points = PointRange.Item1;
		}
		else
		{
			_points = points;
		}
	}

	public override string ToString()
	{
		return $"<Skill: {Path}, {Points}, {Tier}, {InterestOnly} ";
	}

	public string ToSaveString()
	{
		return $"{Path},{Points},{InterestOnly}";
	}

	public string GetShortString()
	{
		return ShortStrings[Path];
	}

	public static Skill? FromSaveString(string saveString)
	{
		if (saveString is null || saveString == "") { return null; }

		string[] skillValues = saveString.Split(',');

		bool interest = skillValues[2].ToLower() == "true";

		return new Skill((SkillPath)int.Parse(skillValues[0]), int.Parse(skillValues[1]), interest);
	}

	public void SetPointsFromTier(int tier)
	{
		if (!(1 <= tier) && !(tier <= 3)) { return; }

		Points = TierRanges[tier - 1].Item1;
	}
}

public class CatSkills
{

}
