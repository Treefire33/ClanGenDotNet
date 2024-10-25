using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.UI.Interfaces;
using ClanGenDotNet.Scripts.UI.Themes;
using Microsoft.Toolkit.HighPerformance;
using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.UI;

public class UIManager
{
	public List<UIElement> Elements = [];
	public List<Event> UIEvents = [];
	public bool IsFocused = false;

	public Dictionary<string, ThemeBlock> LightTheme
		= JsonConvert.DeserializeObject<Dictionary<string, ThemeBlock>>(
			File.ReadAllText(".\\Resources\\Theme\\master_screen_scale.json")
		)!;
	public Dictionary<string, ThemeBlock> DarkTheme
			= JsonConvert.DeserializeObject<Dictionary<string, ThemeBlock>>(
				File.ReadAllText(".\\Resources\\Theme\\themes\\dark.json")
			)!;

	public UIManager()
	{
		foreach (KeyValuePair<string, ThemeBlock> nameTheme in LightTheme)
		{
			if (nameTheme.Value.Prototype != null)
			{
				ThemeBlock prototype = LightTheme[nameTheme.Value.Prototype];
				nameTheme.Value.Colours =
					nameTheme.Value.Colours 
					?? prototype.Colours 
					?? LightTheme["defaults"].Colours;
				nameTheme.Value.Font =
					nameTheme.Value.Font 
					?? prototype.Font 
					?? LightTheme["defaults"].Font;
				nameTheme.Value.Miscellaneous =
					nameTheme.Value.Miscellaneous 
					?? prototype.Miscellaneous 
					?? LightTheme["defaults"].Miscellaneous;
				/*if (!nameTheme.Value.Colours.ContainsKey("normal_text"))
				{
					var toSelectFrom = prototype.Colours ?? LightTheme["defaults"].Colours;
					nameTheme.Value.Colours.Add("normal_text", toSelectFrom["normal_text"]);
				}*/
				nameTheme.Value.Colours.TryAdd("normal_text", "#000000");
			}
		}
	}

	public void DrawUI()
	{
		foreach (UIElement element in Elements)
		{
			if (element.IsContained
				|| !element.Visible
			) { continue; }

			element.Update();
			if (element is IUIClickable clickable)
			{
				clickable.ChangeTexture();
				clickable.HandleElementInteraction();
			}
		}
	}

	public void PushEvent(Event newEvent)
	{
		UIEvents.Add(newEvent);
	}

	public void ResetEvents()
	{
		UIEvents.Clear();
	}
}
