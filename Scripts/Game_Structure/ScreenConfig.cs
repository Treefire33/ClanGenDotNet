using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.Game_Structure;

public class ScreenConfig
{
	[JsonProperty("fullscreen_display", Required = Required.Always)]
	public int FullscreenDisplay { get; set; }
}