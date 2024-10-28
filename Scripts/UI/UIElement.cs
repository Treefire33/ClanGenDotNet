using ClanGenDotNet.Scripts.UI.Interfaces;
using ClanGenDotNet.Scripts.UI.Theming;

namespace ClanGenDotNet.Scripts.UI;

public class UIElement : IUIElement
{
	protected readonly UIManager Manager;
	protected UIElementAppearance Theme;
	public ClanGenRect RelativeRect;
	public bool Hovered;
	public bool IsContained = false;
	public int Layer = 0;

	public bool Visible = true;
	public bool Active = true;

	public UIElement(ClanGenRect posScale, UIManager manager, string objectID = "default")
	{
		Manager = manager;
		RelativeRect = posScale;
		Manager.Elements.Add(this);
		Manager.Elements = [.. Manager.Elements.OrderBy(element => element.Layer)];
		if (objectID == null || objectID == "")
		{
			objectID = "default";
		}
		Theme = Manager.Theme.GetThemeFromID(objectID);
	}

	public virtual void Update()
	{
		Hovered = CheckCollisionPointRec(Utility.GetVirutalMousePosition(), RelativeRect.RelativeRect);
	}

	public virtual void ThemeElement() { }

	public void Revive()
	{
		Manager.Elements.Add(this);
	}

	public void Kill()
	{
		_ = Manager.Elements.Remove(this);
	}

	public void Show()
	{
		Visible = true;
	}

	public void Hide()
	{
		Visible = false;
	}

	public void SetActive(bool activeState)
	{
		Active = activeState;
	}
}
