using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenDotNet.Scripts.UI.Themes;

public class ThemeBlock
{
	[JsonProperty("prototype", Required = Required.Default)]
	public string Prototype;

	[JsonProperty("colours", Required = Required.Default)]
	public Dictionary<string, string> Colours;

	[JsonProperty("font", Required = Required.Default)]
	public Dictionary<string, string> Font;

	[JsonProperty("misc", Required = Required.Default)]
	public Dictionary<string, dynamic> Miscellaneous;

	public static Color GetColourFromHex(string hex)
	{
		int red = int.Parse(hex[1..3], System.Globalization.NumberStyles.HexNumber);
		int blue = int.Parse(hex[3..5], System.Globalization.NumberStyles.HexNumber);
		int green = int.Parse(hex[5..7], System.Globalization.NumberStyles.HexNumber);
		int alpha = hex.Length > 7 ? int.Parse(hex[7..9], System.Globalization.NumberStyles.HexNumber) : 255;
		

		return new Color(
			red,
			blue,
			green,
			alpha
		);
	}
}
