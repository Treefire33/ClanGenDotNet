using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.Cats;

public partial class Tint
{
	[JsonProperty("colour_groups")]
	public Dictionary<string, string> ColourGroups = [];

	[JsonProperty("possible_tints")]
	public Dictionary<string, List<string>> PossibleTints = [];

	[JsonProperty("tint_colours")]
	public Dictionary<string, List<int>?> TintColours = [];

	[JsonProperty("dilute_tint_colours")]
	public Dictionary<string, List<int>?> DiluteTintColours = [];
}
