using ClanGenDotNet.Scripts.UI.Interfaces;
using ClanGenDotNet.Scripts.UI.Themes;

namespace ClanGenDotNet.Scripts.UI;

public class UIElement : IUIElement
{
	protected readonly UIManager Manager;
	protected readonly ThemeBlock ElementTheme;
	public ClanGenRect RelativeRect;
	public bool Hovered;
	public bool IsContained = false;
	public int Layer = 0;

	public bool Visible = true;
	public bool Active = true;

	public UIElement(ClanGenRect posScale, UIManager manager, string objectID = "defaults")
	{
		Manager = manager;
		RelativeRect = posScale;
		Manager.Elements.Add(this);
		Manager.Elements = [.. Manager.Elements.OrderBy(element => element.Layer)];
		if (objectID == null || objectID == "")
		{
			objectID = "defaults";
		}
	}

	public virtual void Update()
	{
		Hovered = CheckCollisionPointRec(GetVirutalMousePosition(), RelativeRect.RelativeRect);
	}

	public virtual void ThemeElement()
	{
		
	}

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
