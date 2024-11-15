using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.Cats;

public class NameDict
{
	[JsonProperty("special_suffixes", Required = Required.Always)]
	public Dictionary<string, string> SpecialSuffixes { get; set; } = [];

	[JsonProperty("normal_suffixes", Required = Required.Always)]
	public List<string> NormalSuffixes { get; set; } = [];

	[JsonProperty("pelt_suffixes", Required = Required.Always)]
	public Dictionary<string, List<string>> PeltSuffixes { get; set; } = [];

	[JsonProperty("tortie_pelt_suffixes", Required = Required.Always)]
	public Dictionary<string, List<string>> TortiePeltSuffixes { get; set; } = [];

	[JsonProperty("normal_prefixes", Required = Required.Always)]
	public List<string> NormalPrefixes { get; set; } = [];

	[JsonProperty("clan_prefixes", Required = Required.Always)]
	public List<string> ClanPrefixes { get; set; } = [];

	[JsonProperty("colour_prefixes", Required = Required.Always)]
	public Dictionary<string, List<string>> ColourPrefixes { get; set; } = [];

	[JsonProperty("biome_prefixes", Required = Required.Always)]
	public Dictionary<string, List<string>> BiomePrefixes { get; set; } = [];

	[JsonProperty("biome_suffixes", Required = Required.Always)]
	public Dictionary<string, List<string>> BiomeSuffixes { get; set; } = [];

	[JsonProperty("eye_prefixes", Required = Required.Always)]
	public Dictionary<string, List<string>> EyePrefixes { get; set; } = [];

	[JsonProperty("loner_names", Required = Required.Always)]
	public List<string> LonerNames { get; set; } = [];

	[JsonProperty("animal_suffixes", Required = Required.Always)]
	public List<string> AnimalSuffixes { get; set; } = [];

	[JsonProperty("animal_prefixes", Required = Required.Always)]
	public List<string> AnimalPrefixes { get; set; } = [];

	[JsonProperty("inappropriate_names", Required = Required.Always)]
	public List<string> InappropriateNames { get; set; } = [];
}
