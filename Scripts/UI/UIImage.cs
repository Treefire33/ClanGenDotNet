namespace ClanGenDotNet.Scripts.UI;

public class UIImage(ClanGenRect posScale, Texture2D image, UIManager manager, bool ninePatch = false, NPatchInfo info = default)
	: UIElement(posScale, manager),
	IUIElement
{
	public Texture2D Image = image;
	private ClanGenRect _imageRect = new(0, 0, new Vector2(image.width, image.height));
	private readonly bool _ninePatched = ninePatch;
	private NPatchInfo _patchInfo = info.top == default(NPatchInfo).top ? Resources.GenerateNPatchInfoFromButton(image) : info;

	public override void Update()
	{
		base.Update();

		if (_ninePatched)
		{
			DrawTextureNPatch(
				Image,
				_patchInfo,
				RelativeRect,
				Vector2.Zero,
				0,
				WHITE
			);
		}
		else
		{
			DrawTexturePro(Image, _imageRect, RelativeRect, Vector2.Zero, 0, WHITE);
		}
	}
}
