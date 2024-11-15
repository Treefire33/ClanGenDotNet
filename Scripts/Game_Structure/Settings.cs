using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.Game_Structure;

public class Settings
{
	[JsonProperty("general", Required = Required.Always)]
	/*public Dictionary<string, List<General>> General { get; set; }*/
	public Dictionary<string, List<object>> General { get; set; } = [];

	[JsonProperty("__other", Required = Required.Always)]
	public Dictionary<string, List<object>> Other { get; set; } = [];
}
