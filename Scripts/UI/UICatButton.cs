using ClanGenDotNet.Scripts.Cats;

namespace ClanGenDotNet.Scripts.UI;

public class UICatButton(
	ClanGenRect posScale,
	Texture2D sprite,
	Cat buttonCat,
	UIManager manager
) : UIButton(posScale, sprite, manager)
{
	private readonly Cat _cat = buttonCat;

	public Cat GetCat()
	{
		return _cat;
	}
}
