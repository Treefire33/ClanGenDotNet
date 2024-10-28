using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.UI.Interfaces;
using ClanGenDotNet.Scripts.UI.Theming;

namespace ClanGenDotNet.Scripts.UI;

public class UIManager
{
	public List<UIElement> Elements = [];
	public List<Event> UIEvents = [];
	public UITheme Theme;
	public bool IsFocused = false;

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

	public void LoadTheme(string themePath)
	{
		Theme = UITheme.LoadThemeFromFile(themePath);
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
