using ClanGenDotNet.Scripts.UI.Interfaces;

namespace ClanGenDotNet.Scripts.UI;

public class UIElement : IUIElement
{
	protected readonly UIManager _manager;
	public ClanGenRect RelativeRect;
	public bool Hovered;
	public bool IsContained = false;
	public int Layer = 0;

	public bool Visible = true;
	public bool Active = true;

	public UIElement(ClanGenRect posScale, UIManager manager)
	{
		_manager = manager;
		RelativeRect = posScale;
		_manager.Elements.Add(this);
		_manager.Elements = [.. _manager.Elements.OrderBy(element => element.Layer)];
	}

	public virtual void Update()
	{
		Hovered = CheckCollisionPointRec(Utility.GetVirutalMousePosition(), RelativeRect.RelativeRect);
	}

	public void Revive()
	{
		_manager.Elements.Add(this);
	}

	public void Kill()
	{
		_ = _manager.Elements.Remove(this);
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
