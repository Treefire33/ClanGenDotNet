using ClanGenDotNet.Scripts.UI.Theming;

namespace ClanGenDotNet.Scripts.UI;

public class UIElement : IUIElement
{
	protected readonly UIManager Manager;
	protected UIElementAppearance Theme;
	public ClanGenRect RelativeRect;
	public int Layer = 0;
	public bool IsContained = false;

	public bool Visible = true;
	public bool Active = true;
	public bool Hovered = false;

	public UIElement(ClanGenRect posScale, bool visible = true, ObjectID objectID = default)
	{
		Manager = game.Manager;
		RelativeRect = posScale;
		Manager.Elements.Add(this);
		Manager.Elements = [.. Manager.Elements.OrderBy(element => element.Layer)];
		if (objectID == default)
		{
			objectID.ID = "default";
			objectID.Class = "";
		}
		Theme = Manager.Theme.GetFromObjectID(objectID);
		Visible = visible;
	}

	public virtual void Update()
	{
		Hovered = CheckCollisionPointRec(GetVirutalMousePosition(), RelativeRect);
	}

	public virtual void ThemeElement() { }

	public void Revive()
	{
		Manager.Elements.Add(this);
	}

	public void Kill()
	{
		if (!Manager.Elements.Remove(this))
		{
			Console.WriteLine("Element has already been killed, set to null and reinstance element.");
		}
	}

	public void SetVisibility(bool visibilityState)
	{
		Visible = visibilityState;
	}

	public void Show() => SetVisibility(true);
	public void Hide() => SetVisibility(false);

	public void SetActive(bool activeState)
	{
		Active = activeState;
	}

	public void Enable() => SetActive(true);
	public void Disable() => SetActive(false);
}
