using ClanGenDotNet.Scripts.UI.Interfaces;

namespace ClanGenDotNet.Scripts.UI;

public class UIImage(ClanGenRect posScale, Texture2D image, UIManager manager)
	: UIElement(posScale, manager),
	IUIElement
{
	public Texture2D Image = image;
	private ClanGenRect _imageRect = new(0, 0, new Vector2(image.width, image.height));

	public override void Update()
	{
		base.Update();

		DrawTexturePro(Image, _imageRect.RelativeRect, RelativeRect.RelativeRect, Vector2.Zero, 0, WHITE);
	}
}
