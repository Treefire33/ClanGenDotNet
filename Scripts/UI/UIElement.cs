using Raylib_cs;
using static Raylib_cs.Raylib;

namespace ClanGenDotNet.Scripts.UI
{
	public class UIElement
	{
		protected readonly UIManager _manager;
		public ClanGenRect RelativeRect;
		public bool Hovered;

		public UIElement(ClanGenRect posScale, UIManager manager)
		{
			_manager = manager;
			RelativeRect = posScale;
			_manager.Elements.Add(this);
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
			_manager.Elements.Remove(this);
		}

		public virtual void SetActive(bool activeState) { }
	}
}
