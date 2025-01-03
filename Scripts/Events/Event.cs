﻿using ClanGenDotNet.Scripts.UI;

namespace ClanGenDotNet.Scripts.Events;

public class Event
{
	public Event(UIElement element, EventType type)
	{
		Element = element;
		EventType = type;
	}

	public Event(int keyCode, EventType type)
	{
		KeyCode = (KeyboardKey)keyCode;
		EventType = type;
	}

	public override string ToString()
	{
		if (Element == null)
		{
			return $"UIEvent of key: {KeyCode}";
		}
		return $"UIEvent of type: {EventType}";
	}

	public UIElement? Element = null;
	public KeyboardKey KeyCode = 0;
	public EventType EventType;
}
