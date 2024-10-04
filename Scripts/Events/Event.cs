using ClanGenDotNet.Scripts.UI;

namespace ClanGenDotNet.Scripts.Events
{
	public class Event(UIElement element, EventType type)
	{
		public UIElement Element = element;
		public EventType EventType = type;
	}
}
