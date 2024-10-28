using Newtonsoft.Json;
using System.Text;

namespace ClanGenDotNet.Scripts.UI.Theming;

public class UITheme
{
	private Dictionary<string, UIElementTheme> _elementThemes = [];
	public Dictionary<string, UIElementAppearance> ElementThemes = [];

	public static UITheme LoadThemeFromFile(string filePath)
	{
		UITheme newTheme = new()
		{
			//Loaded theme *should* never be null
			_elementThemes = JsonConvert.DeserializeObject<
				Dictionary<string, UIElementTheme>
			>(File.ReadAllText(filePath))!
		};
		newTheme.LoadPrototypes();
		newTheme.LoadThemes();
		return newTheme;
	}

	public void LoadThemes()
	{
		foreach (var theme in _elementThemes)
		{
			UIElementAppearance newElementAppearance = new()
			{
				Font = GetElementFontEntry(theme.Key),
				Colours = GetElementColours(theme.Key),
				Miscellaneous = GetElementMiscEntries(theme.Key),
			};
			ElementThemes.Add(
				theme.Key,
				newElementAppearance
			);
		}
	}

	public (Font, int) GetElementFontEntry(string element)
	{
		return (
			GetFontFromName(_elementThemes[element].Font!["name"]), 
			int.Parse(_elementThemes[element].Font!["size"])
		);
	}

	public Dictionary<string, Color> GetElementColours(string element)
	{
		Dictionary<string, Color> elementColours = [];
		foreach (var entry in _elementThemes[element].Colour!)
		{
			elementColours.Add(entry.Key, GetColourFromHex(entry.Value));
		}
		return elementColours;
	}

	public Dictionary<string, string> GetElementMiscEntries(string element)
	{
		Dictionary<string, string> entries = [];
		foreach (var entry in _elementThemes[element].Misc!)
		{
			entries.Add(entry.Key, entry.Value);
		}
		return entries;
	}

	public Font GetFontFromName(string fontName)
	{
		return fontName switch
		{
			"clangen" => Resources.Clangen,
			"notosans" => Resources.NotoSansRegular,
			_ => Resources.NotoSansRegular
		};
	}

	public Color GetColourFromHex(string hex)
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

	public void LoadPrototypes()
	{
		//we should have a default theme, if not, then add one!
		UIElementTheme defaultTheme = _elementThemes["default"];
		foreach (KeyValuePair<string, UIElementTheme> theme in _elementThemes)
		{
			string themeName = theme.Key;
			UIElementTheme elementTheme = theme.Value;
			elementTheme.Colour ??= [];
			elementTheme.Font ??= [];
			elementTheme.Misc ??= [];
			if (theme.Value.Prototype != null && _elementThemes.TryGetValue(theme.Value.Prototype, out UIElementTheme? prototypeTheme))
			{
				foreach (KeyValuePair<string, string> colourEntry in prototypeTheme.Colour!)
				{
					if (!elementTheme.Colour.ContainsKey(colourEntry.Key))
					{
						elementTheme.Colour[colourEntry.Key] = colourEntry.Value;
					}
				}
				foreach (KeyValuePair<string, string> fontEntry in prototypeTheme.Font!)
				{
					if (!elementTheme.Font.ContainsKey(fontEntry.Key))
					{
						elementTheme.Font[fontEntry.Key] = fontEntry.Value;
					}
				}
				foreach (KeyValuePair<string, string> miscEntry in prototypeTheme.Misc!)
				{
					if (!elementTheme.Misc.ContainsKey(miscEntry.Key))
					{
						elementTheme.Misc[miscEntry.Key] = miscEntry.Value;
					}
				}
			}
			else
			{
				foreach (KeyValuePair<string, string> colourEntry in defaultTheme.Colour!)
				{
					if (!elementTheme.Colour.ContainsKey(colourEntry.Key))
					{
						elementTheme.Colour[colourEntry.Key] = colourEntry.Value;
					}
				}
				foreach (KeyValuePair<string, string> fontEntry in defaultTheme.Font!)
				{
					if (!elementTheme.Font.ContainsKey(fontEntry.Key))
					{
						elementTheme.Font[fontEntry.Key] = fontEntry.Value;
					}
				}
				foreach (KeyValuePair<string, string> miscEntry in defaultTheme.Misc!)
				{
					if (!elementTheme.Misc.ContainsKey(miscEntry.Key))
					{
						elementTheme.Misc[miscEntry.Key] = miscEntry.Value;
					}
				}
			}
		}
	}

	public override string ToString()
	{
		StringBuilder representation = new();
		foreach (var theme in _elementThemes)
		{
			representation.Append(theme.Key + "\n\t");
			representation.AppendLine(theme.Value.ToString());
		}
		return representation.ToString();
	}

	public UIElementAppearance GetThemeFromID(string objectID)
	{
		return ElementThemes[objectID];
	}
}
