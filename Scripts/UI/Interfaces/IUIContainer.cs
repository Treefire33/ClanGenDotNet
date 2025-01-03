﻿namespace ClanGenDotNet.Scripts.UI.Interfaces;

public interface IUIContainer
{
	void AddElement(UIElement element);
	void AddElement(UIElement element, bool modPosition);
	void RemoveElement(UIElement element);
}
