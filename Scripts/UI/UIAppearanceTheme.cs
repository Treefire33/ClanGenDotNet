using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.UI;
public class UIAppearanceTheme
{
	public Dictionary<string, Color> BaseColours;
	public Dictionary<string, Dictionary<string, Color>> UIElementColours;

	public Dictionary<string, Dictionary<string, object>> UIElementFontsInfo;

	public UIAppearanceTheme()
	{

	}

	public Dictionary<dynamic, dynamic> LoadThemeByPath(string filePath)
	{
		Dictionary<dynamic, dynamic> themeDict = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(
			File.ReadAllText(".\\Resources\\Theme\\master_screen_scale.json"))!;
		return themeDict;
	}

	public void LoadTheme(string filePath)
	{
		var themeDict = LoadThemeByPath(filePath);
		if (themeDict == null) { return; }

		ParseThemeDataFromDict(themeDict);
	}

	public void LoadDefaultColours(Dictionary<dynamic, dynamic> dict)
	{
		foreach (var dataType in dict["defaults"])
		{
			if (dataType == "colours")
			{
				var coloursDict = dict["defaults"][dataType];
				foreach (var colourKey in coloursDict)
				{
					var colour = LoadColourFromTheme((Dictionary<string, string>)coloursDict, (string)colourKey);
					BaseColours.TryAdd((string)colourKey, colour);
				}
			}
		}
	}

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

	public Color LoadColourFromTheme(Dictionary<string, string> coloursDict, string colourID)
	{
		string data = coloursDict[colourID];
		var colour = GetColourFromHex(data);
		return colour;
	}

	public void LoadElementColourDataFromTheme(
		string dataType, 
		string elementName, 
		Dictionary<string, dynamic> elementTheming
	)
	{
		if (!UIElementColours.ContainsKey(elementName))
		{
			UIElementColours[elementName] = [];
		}
		var coloursDict = elementTheming[dataType];
		foreach (string colourKey in coloursDict)
		{
			var colour = LoadColourFromTheme((Dictionary<string, string>)coloursDict, colourKey);
			UIElementColours[elementName][colourKey] = colour;
		}
	}

	public void LoadElementFontDataFromTheme(Dictionary<string, object> fileDict, string elementName)
	{
		if (!UIElementFontsInfo.ContainsKey(elementName))
		{
			UIElementFontsInfo[elementName] = new Dictionary<string, object>();
		}

		if (fileDict.ContainsKey("name"))
		{
			((Dictionary<string, object>)UIElementFontsInfo[elementName]["en"])["name"] = fileDict["name"];
		}

		if (fileDict.ContainsKey("size"))
		{
			((Dictionary<string, object>)UIElementFontsInfo[elementName]["en"])["size"] = fileDict["size"];
		}
	}

	public void LoadPrototype(string elementName, Dictionary<dynamic, dynamic> themeDict)
	{
		if (themeDict.ContainsKey("prototype"))
		{
			string prototypeID = themeDict[elementName]["prototype"];

			if (UIElementFontsInfo.TryGetValue(prototypeID, out Dictionary<string, object>? font))
			{
				var prototypeFont = font;
				if (!UIElementFontsInfo.ContainsKey(elementName))
				{
					UIElementFontsInfo[elementName] = [];
				}

				foreach (var locale in prototypeFont)
				{
					if (!UIElementFontsInfo.ContainsKey(elementName))
					{
						UIElementFontsInfo[elementName].TryAdd(locale.Key, new Dictionary<string, object>());
					}

					foreach (var dataKey in (Dictionary<string, object>)prototypeFont[locale.Key])
					{
						((Dictionary<string, object>)UIElementFontsInfo[elementName][locale.Key])[dataKey.Key] 
							= ((Dictionary<string, object>)prototypeFont[locale.Key])[dataKey.Key];
					}
				}
			}

			if (UIElementColours.TryGetValue(prototypeID, out Dictionary<string, Color>? colour))
			{
				var prototypeColours = colour;
				if (!UIElementColours.ContainsKey(elementName))
				{
					UIElementColours.TryAdd(elementName, []);
				}
				foreach (var colourKey in prototypeColours.Keys)
				{
					UIElementColours[elementName][colourKey] = prototypeColours[colourKey];
				}
			}
		}
		else
		{
			return;
		}
	}

	public void ParseSingleElementData(string elementName, Dictionary<dynamic, dynamic> elementTheming)
	{
		foreach (dynamic dataType in elementTheming)
		{
			if ((string)dataType == "font")
			{
				var fileDict = elementTheming[dataType];

			}
		}
	}

	public void ParseThemeDataFromDict(Dictionary<dynamic, dynamic> dict)
	{
		Console.WriteLine(dict.Keys.ToString());
		foreach (var elementName in dict.Keys)
		{
			//Console.WriteLine(elementName);
			if (elementName == "defaults")
			{
				LoadDefaultColours(dict);
			}
			else
			{
				LoadPrototype(elementName, dict);
				var elementThemeing = dict[elementName];
				ParseSingleElementData((string)elementName, (Dictionary<dynamic, dynamic>)elementThemeing);
			}
		}
	}
}
