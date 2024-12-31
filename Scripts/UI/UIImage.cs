namespace ClanGenDotNet.Scripts.UI;

public class UIImage(ClanGenRect posScale, Texture2D image, bool ninePatch = false, NPatchInfo info = default)
	: UIElement(posScale),
	IUIElement
{
	public Texture2D Image = image;
	private ClanGenRect _imageRect = new(0, 0, new Vector2(image.Width, image.Height));
	private readonly bool _ninePatched = ninePatch;
	private NPatchInfo _patchInfo = info.Top == default(NPatchInfo).Top ? Resources.GenerateNPatchInfoFromButton(image) : info;

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
				White
			);
		}
		else
		{
			DrawTexturePro(Image, _imageRect, RelativeRect, Vector2.Zero, 0, White);
		}
	}
}
