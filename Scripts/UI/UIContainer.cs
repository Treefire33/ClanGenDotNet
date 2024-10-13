using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
using static ClanGenDotNet.Scripts.Resources;
using static ClanGenDotNet.Scripts.Utility;
using ClanGenDotNet.Scripts.Game_Structure;

namespace ClanGenDotNet.Scripts.UI;

public class UIContainer(ClanGenRect posScale, UIManager manager) 
	: UIElement(posScale, manager), 
	IUIContainer, 
	IUIElement
{
	public List<UIElement> ContainedElements = [];

	public void AddElement(UIElement element)
	{
		element.Layer += Layer;
		ContainedElements.Add(element);
		if (!Active)
		{
			element.SetActive(false);
		}
		if (!Visible)
		{
			element.Visible = false;
		}
		element.IsContained = true;
	}

	public void AddElement(UIElement element, bool modPosition)
	{
		element.Layer += Layer;
		ContainedElements.Add(element);
		if (!Active)
		{
			element.SetActive(false);
		}
		if (!Visible)
		{
			element.Visible = false;
		}
		element.IsContained = true;
		if (modPosition) { element.RelativeRect.Position += RelativeRect.Position; }
	}

	public void RemoveElement(UIElement element)
	{
		ContainedElements.Remove(element);
		element.IsContained = false;
	}

	public override void Update()
	{
		base.Update();
		foreach (var element in ContainedElements)
		{
			if (!element.Visible) { continue; }

			if (!PointInArea(element.RelativeRect.Position, RelativeRect)) { continue; }

			element.Update();
			if (element is IUIClickable clickable)
			{
				clickable.ChangeTexture();
				clickable.HandleElementInteraction();
			}
		}
	}

	public void SetLayer(int newLayer)
	{
		Layer = newLayer;
		foreach(var element in ContainedElements)
		{
			element.Layer = newLayer;
		}
	}
}
