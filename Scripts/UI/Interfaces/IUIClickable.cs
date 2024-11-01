namespace ClanGenDotNet.Scripts.UI.Interfaces;

public interface IUIClickable : IUIFocusable
{
	void HandleElementInteraction();
	void ChangeTexture();
}
