
using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.Cats;

public class Name
{
	public string Status;
	public string? Prefix;
	public string? Suffix;
	public bool SpecialSuffixHidden;

	private static NameDict _namesDict = JsonConvert.DeserializeObject<NameDict>(
		File.ReadAllText(".\\Resources\\Dicts\\names\\names.json")
	)!;

	public Name(
		string status = "warrior",
		string? prefix = null,
		string? suffix = null,
		string? colour = null,
		string? eyes = null,
		string? pelt = null,
		string? tortiePattern = null,
		string? biome = null,
		bool specialSuffixHidden = false,
		bool loadExistingName = false
	)
	{
		Status = status;
		Prefix = prefix;
		Suffix = suffix;
		SpecialSuffixHidden = specialSuffixHidden;

		bool nameFixPrefix = false;

		if (prefix == null)
		{
			GivePrefix(eyes, colour, biome);
			nameFixPrefix = true;
		}

		if (Suffix == null)
		{
			GiveSuffix(pelt, biome, tortiePattern);
			if (nameFixPrefix && Prefix == null)
			{
				nameFixPrefix = false;
			}
		}

		//finish this
		/*if (Suffix != null && !loadExistingName)
		{
			bool tripleLetter = false;

		}*/
	}

	public void GivePrefix(string? eyes, string? colour, string? biome)
	{
		bool namedAfterAppearence = false;
		if (game.Config.CatNameControls.AlwaysNameAfterAppearance)
		{
			namedAfterAppearence = true;
		}
		else
		{
			namedAfterAppearence = GetRandBits(2) == 0;
		}

		bool namedAfterBiome = GetRandBits(3) == 0;

		List<List<string>> possiblePrefixCategories = [];
		if (game.Config.CatNameControls.AllowEyeNames)
		{
			if (_namesDict.EyePrefixes.TryGetValue(eyes!, out List<string>? eyeCategory))
			{
				possiblePrefixCategories.Add(eyeCategory);
			}
		}
		if (_namesDict.ColourPrefixes.TryGetValue(colour!, out List<string>? colourCategory))
		{
			possiblePrefixCategories.Add(colourCategory);
		}
		if (biome != null && _namesDict.BiomePrefixes.TryGetValue(biome, out List<string>? biomeCategory))
		{
			possiblePrefixCategories.Add(biomeCategory);
		}

		List<string> selectedPrefixCategory;
		if (
			namedAfterAppearence
			&& possiblePrefixCategories.Count > 0
			&& !namedAfterBiome
		)
		{
			selectedPrefixCategory = possiblePrefixCategories.PickRandom();
			Prefix = selectedPrefixCategory.PickRandom();
		}
		else if (
			namedAfterBiome
			&& possiblePrefixCategories.Count > 0
		)
		{
			selectedPrefixCategory = possiblePrefixCategories.PickRandom();
			Prefix = selectedPrefixCategory.PickRandom();
		}
		else
		{
			Prefix = _namesDict.NormalPrefixes.PickRandom();
		}
	}

	public void GiveSuffix(string? pelt, string? biome, string? tortiePattern)
	{
		if (pelt == null || pelt == "SingleColour")
		{
			Suffix = _namesDict.NormalSuffixes.PickRandom();
		}
		else
		{
			bool namedAfterPelt = GetRandBits(2) == 0;
			bool namedAfterBiome = GetRandBits(3) == 0;

			if (namedAfterPelt)
			{
				if ((pelt == "Tortie" || pelt == "Calico") && _namesDict.TortiePeltSuffixes.TryGetValue(tortiePattern!, out List<string>? tortieSuffixes))
				{
					Suffix = tortieSuffixes.PickRandom();
				}
				else if (_namesDict.PeltSuffixes.TryGetValue(pelt, out List<string>? peltSuffixes))
				{
					Suffix = peltSuffixes.PickRandom();
				}
				else
				{
					Suffix = _namesDict.NormalSuffixes.PickRandom();
				}
			}
			else if (namedAfterBiome && biome != null)
			{
				if (_namesDict.BiomePrefixes.TryGetValue(biome, out List<string>? biomeSuffixes))
				{
					Suffix = biomeSuffixes.PickRandom();
				}
				else
				{
					Suffix = _namesDict.NormalSuffixes.PickRandom();
				}
			}
			else
			{
				Suffix = _namesDict.NormalSuffixes.PickRandom();
			}
		}
	}

	public override string ToString()
	{
		if (_namesDict.SpecialSuffixes.ContainsKey(Status) && !SpecialSuffixHidden)
		{
			return Prefix + _namesDict.SpecialSuffixes[Status];
		}
		if (game.Config.Fun.AprilFools)
		{
			return Prefix + "egg";
		}
		return Prefix + Suffix;
	}
}
