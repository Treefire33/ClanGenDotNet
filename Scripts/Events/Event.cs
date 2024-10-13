using ClanGenDotNet.Scripts.UI;
using Raylib_cs;

namespace ClanGenDotNet.Scripts.Events
{
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

		public UIElement? Element = null;
		public KeyboardKey KeyCode = 0;
		public EventType EventType;
	}
}
