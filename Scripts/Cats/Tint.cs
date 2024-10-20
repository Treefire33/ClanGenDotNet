using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.Cats;

public partial class Tint
{
	[JsonProperty("colour_groups", Required = Required.Always)]
	public ColourGroups ColourGroups { get; set; }

	[JsonProperty("possible_tints", Required = Required.Always)]
	public PossibleTints PossibleTints { get; set; }

	[JsonProperty("tint_colours", Required = Required.Always)]
	public TintColours TintColours { get; set; }

	[JsonProperty("dilute_tint_colours", Required = Required.Always)]
	public DiluteTintColours DiluteTintColours { get; set; }
}

public partial class ColourGroups
{
	[JsonProperty("WHITE", Required = Required.Always)]
	public string White { get; set; }

	[JsonProperty("PALEGREY", Required = Required.Always)]
	public string Palegrey { get; set; }

	[JsonProperty("SILVER", Required = Required.Always)]
	public string Silver { get; set; }

	[JsonProperty("GREY", Required = Required.Always)]
	public string Grey { get; set; }

	[JsonProperty("DARKGREY", Required = Required.Always)]
	public string Darkgrey { get; set; }

	[JsonProperty("GHOST", Required = Required.Always)]
	public string Ghost { get; set; }

	[JsonProperty("BLACK", Required = Required.Always)]
	public string Black { get; set; }

	[JsonProperty("CREAM", Required = Required.Always)]
	public string Cream { get; set; }

	[JsonProperty("PALEGINGER", Required = Required.Always)]
	public string Paleginger { get; set; }

	[JsonProperty("GOLDEN", Required = Required.Always)]
	public string Golden { get; set; }

	[JsonProperty("GINGER", Required = Required.Always)]
	public string Ginger { get; set; }

	[JsonProperty("DARKGINGER", Required = Required.Always)]
	public string Darkginger { get; set; }

	[JsonProperty("SIENNA", Required = Required.Always)]
	public string Sienna { get; set; }

	[JsonProperty("LIGHTBROWN", Required = Required.Always)]
	public string Lightbrown { get; set; }

	[JsonProperty("LILAC", Required = Required.Always)]
	public string Lilac { get; set; }

	[JsonProperty("BROWN", Required = Required.Always)]
	public string Brown { get; set; }

	[JsonProperty("GOLDEN-BROWN", Required = Required.Always)]
	public string GoldenBrown { get; set; }

	[JsonProperty("DARKBROWN", Required = Required.Always)]
	public string Darkbrown { get; set; }

	[JsonProperty("CHOCOLATE", Required = Required.Always)]
	public string Chocolate { get; set; }
}

public partial class DiluteTintColours
{
	[JsonProperty("dilute", Required = Required.Always)]
	public List<long> Dilute { get; set; }

	[JsonProperty("warmdilute", Required = Required.Always)]
	public List<long> Warmdilute { get; set; }

	[JsonProperty("cooldilute", Required = Required.Always)]
	public List<long> Cooldilute { get; set; }

	[JsonProperty("none", Required = Required.AllowNull)]
	public object None { get; set; }
}

public partial class PossibleTints
{
	[JsonProperty("basic", Required = Required.Always)]
	public List<string> Basic { get; set; }

	[JsonProperty("warm", Required = Required.Always)]
	public List<string> Warm { get; set; }

	[JsonProperty("cool", Required = Required.Always)]
	public List<string> Cool { get; set; }

	[JsonProperty("white", Required = Required.Always)]
	public List<string> White { get; set; }

	[JsonProperty("monochrome", Required = Required.Always)]
	public List<string> Monochrome { get; set; }

	[JsonProperty("brown", Required = Required.Always)]
	public List<string> Brown { get; set; }
}

public partial class TintColours
{
	[JsonProperty("pink", Required = Required.Always)]
	public List<long> Pink { get; set; }

	[JsonProperty("gray", Required = Required.Always)]
	public List<long> Gray { get; set; }

	[JsonProperty("red", Required = Required.Always)]
	public List<long> Red { get; set; }

	[JsonProperty("black", Required = Required.Always)]
	public List<long> Black { get; set; }

	[JsonProperty("orange", Required = Required.Always)]
	public List<long> Orange { get; set; }

	[JsonProperty("yellow", Required = Required.Always)]
	public List<long> Yellow { get; set; }

	[JsonProperty("purple", Required = Required.Always)]
	public List<long> Purple { get; set; }

	[JsonProperty("blue", Required = Required.Always)]
	public List<long> Blue { get; set; }

	[JsonProperty("none", Required = Required.AllowNull)]
	public object None { get; set; }
}
