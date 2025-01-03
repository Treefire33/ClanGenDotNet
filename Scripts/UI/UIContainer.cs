﻿namespace ClanGenDotNet.Scripts.UI;

public class UIContainer(ClanGenRect posScale)
	: UIElement(posScale),
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
		AddElement(element);
		if (modPosition) { element.RelativeRect.Position += RelativeRect.Position; }
	}

	public void RemoveElement(UIElement element)
	{
		_ = ContainedElements.Remove(element);
		element.IsContained = false;
	}

	public override void Update()
	{
		base.Update();
		BeginScissorMode((int)RelativeRect.X, (int)RelativeRect.Y, (int)RelativeRect.Width, (int)RelativeRect.Height);
		foreach (UIElement element in ContainedElements.Where(element => element.Visible))
		{
			element.Update();
			if (element is IUIClickable clickable)
			{
				clickable.ChangeTexture();
				clickable.HandleElementInteraction();
			}
		}
		EndScissorMode();
	}

	public void SetLayer(int newLayer)
	{
		Layer = newLayer;
		foreach (UIElement element in ContainedElements)
		{
			element.Layer = newLayer;
		}
	}
}
