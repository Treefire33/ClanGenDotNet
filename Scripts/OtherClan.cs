using ClanGenDotNet.Scripts.Cats;
using Newtonsoft.Json;
using System.Collections.Immutable;

namespace ClanGenDotNet.Scripts;

public class OtherClan
{
	private readonly List<string> TemperamentList = [
		"cunning",
		"wary",
		"logical",
		"proud",
		"stoic",
		"mellow",
		"bloodthirsty",
		"amiable",
		"gracious"
	];

	public string Name;
	public int Relationship;
	public string Temperament;
	public string ChosenSymbol;
	
	[JsonConstructor]
	public OtherClan(string? name, int relation = 0, string? temperament = null, string? chosen_symbol = null)
	{
		string[] clanNames = new string[Cats.Name.NamesDict.NormalPrefixes.Count];
		Cats.Name.NamesDict.NormalPrefixes.CopyTo(clanNames);
		clanNames = clanNames.Concat(Cats.Name.NamesDict.ClanPrefixes).ToArray();

		Name = name ?? clanNames.PickRandom();
		Relationship = relation == 0 ? Rand.Next(8, 12) : relation;
		Temperament = temperament ?? TemperamentList.PickRandom();
		if (!TemperamentList.Contains(Temperament))
		{
			Temperament = TemperamentList.PickRandom();
		}
	}
}
