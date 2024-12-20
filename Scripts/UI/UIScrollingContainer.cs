﻿using ClanGenDotNet.Scripts.UI.Interfaces;

namespace ClanGenDotNet.Scripts.UI;
public class UIScrollingContainer(ClanGenRect posScale) 
	: UIContainer(posScale), 
	IUIElement, 
	IUIContainer
{
	/*
	 Fair Warning:
		* most of this code is cobbled together and probably sucks a lot
		* be wary when treading
	 */
	private float _currentElementYOffset = 0f;
	private readonly float _scrollSpeed = 12f;
	private float _minScrollPosition = 0f;
	private float _maxScrollPosition = 0f;

	public override void Update()
	{
		if (ContainedElements.Count > 0)
		{
			_maxScrollPosition = -(ContainedElements.Last().RelativeRect.Height);
			_minScrollPosition = -(
				(ContainedElements.First().RelativeRect.Height
				 * ContainedElements.Count)
			) + (ContainedElements.First().RelativeRect.Height * 4);
			foreach (UIElement element in ContainedElements)
			{
				element.RelativeRect.Y += _currentElementYOffset;
			}
		}
		base.Update();
		foreach (UIElement element in ContainedElements)
		{
			element.RelativeRect.Y -= _currentElementYOffset;
		}
		HandleScroll();
	}

	private void HandleScroll()
	{
		_currentElementYOffset += (int)GetMouseWheelMove() * _scrollSpeed;
		_currentElementYOffset =
			_currentElementYOffset >= _maxScrollPosition
			? _maxScrollPosition
			: _currentElementYOffset;
		_currentElementYOffset =
			_currentElementYOffset <= _minScrollPosition
			? _minScrollPosition
			: _currentElementYOffset;
	}
}
