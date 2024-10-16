using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.Game_Structure;

public class Credits
{
	[JsonProperty("text", Required = Required.Always)]
	public required List<string> Text { get; set; }

	[JsonProperty("contrib", Required = Required.Always)]
	public required Dictionary<string, string> Contrib { get; set; }
}
