using ClanGenDotNet.Scripts.Cats;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace ClanGenDotNet.Scripts;

public partial class Utility
{
	public static Rectangle Scale(Rectangle oldRect)
	{
		float posX = oldRect.X / 1600 * game.ScreenX;
		float posY = oldRect.Y / 1400 * game.ScreenY;
		float scaleX = oldRect.width / 1600 * game.ScreenX;
		float scaleY = oldRect.height / 1400 * game.ScreenY;

		return new(posX, posY, scaleX, scaleY);
	}

	public static ClanGenRect UIScale(ClanGenRect rect)
	{
		rect.X = MathF.Floor(rect.X * ScreenSettings.ScreenScale);
		rect.Y = MathF.Floor(rect.Y * ScreenSettings.ScreenScale);
		rect.Width = rect.Width > 0 ? MathF.Floor(rect.Width * ScreenSettings.ScreenScale) : rect.Width;
		rect.Height = rect.Height > 0 ? MathF.Floor(rect.Height * ScreenSettings.ScreenScale) : rect.Height;

		return rect;
	}

	public static Vector2 UIScaleDimension(Vector2 point)
	{
		return new(
			point.X > 0 ? MathF.Floor(point.X * ScreenSettings.ScreenScale) : point.X,
			point.Y > 0 ? MathF.Floor(point.Y * ScreenSettings.ScreenScale) : point.Y
		);
	}

	public static Vector2 UIScaleOffset(Vector2 point)
	{
		return new(
			MathF.Floor(point.X * ScreenSettings.ScreenScale),
			MathF.Floor(point.Y * ScreenSettings.ScreenScale)
		);
	}

	public static float UIScaleValue(float value)
	{
		return MathF.Floor(value * ScreenSettings.ScreenScale);
	}

	//hold on, we don't blit here
	public static Vector2 UIScaleBlit(Vector2 point)
	{
		return new(
			MathF.Floor((point.X * ScreenSettings.ScreenScale) + ScreenSettings.Offset.X),
			MathF.Floor((point.Y * ScreenSettings.ScreenScale) + ScreenSettings.Offset.Y)
		);
	}

	public static Color GetThemeColour()
	{
		return (bool)game.Settings["dark mode"]! == true
			? Resources.DarkModeColour
			: Resources.LightModeColour;
	}

	public static Vector2 GetVirutalMousePosition()
	{
		Vector2 mousePos = GetMousePosition();
		Vector2 virtualMouse = new(0)
		{
			X = (
			mousePos.X
			- ((GetScreenWidth()
			- (ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale)) * 0.5f))
			/ ScreenSettings.ScreenScale,
			Y = (
			mousePos.Y
			- ((GetScreenHeight()
			- (ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale)) * 0.5f))
			/ ScreenSettings.ScreenScale
		};
		virtualMouse = Vector2.Clamp(virtualMouse, new Vector2(0), ScreenSettings.GameScreenSize);

		return virtualMouse;
	}
}
