using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ClanGenDotNet.Scripts.Game_Structure;

public class ScreenConfig
{
	[JsonProperty("fullscreen_display", Required = Required.Always)]
	public int FullscreenDisplay { get; set; }
}