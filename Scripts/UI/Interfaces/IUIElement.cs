namespace ClanGenDotNet.Scripts.UI.Interfaces;

public interface IUIElement
{
	void Update();
	void Revive();
	void Kill();

	void Show();
	void Hide();
	void SetActive(bool activeState);
}
