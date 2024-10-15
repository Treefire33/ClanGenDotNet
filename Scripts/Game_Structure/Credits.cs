using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ClanGenDotNet.Scripts.Game_Structure;

public class Credits
{
	[JsonProperty("text", Required = Required.Always)]
	public List<string> Text { get; set; }

	[JsonProperty("contrib", Required = Required.Always)]
	public Dictionary<string, string> Contrib { get; set; }
}
