using ClanGenDotNet.Scripts.UI;
using Newtonsoft.Json;
using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace ClanGenDotNet.Scripts.Game_Structure;

public class ScreenSettings
{
	public static Vector2 Offset;
	public static int ScreenX;
	public static int ScreenY;
	public static int ScreenScale;
	public static Vector2 GameScreenSize = new(800, 700);
	public static Dictionary<object, object> CurrentVariableDictionary = [];

	public static RenderTexture2D Screen;
	public static ScreenConfig ScreenConfig = JsonConvert.DeserializeObject<ScreenConfig>(
		File.ReadAllText(".\\Resources\\screen_config.json")
	)!;

	private static bool _displayChangeInProgress = false;

	public static void SetDisplayMode(
		Screens.Screens? sourceScreen = null, 
		bool showConfirmDialog = true,
		bool ingameSwitch = true
	)
	{
		if (_displayChangeInProgress) { return; }
		_displayChangeInProgress = true;

		Vector2 oldOffset = Offset;
		int oldScale = ScreenScale;
		Vector2 mousePosition = GetMousePosition();

		bool fullscreen = (bool)Game.game.Settings["fullscreen"]!;

		if (sourceScreen != null)
		{
			//reconstruct the screen or something I give up
		}

		if (fullscreen)
		{
			List<Vector2> displaySizes = [];
			for (int i = 0; i < GetMonitorCount(); i++)
			{
				displaySizes.Add(new Vector2(GetMonitorWidth(i), GetMonitorHeight(i)));
			} //honestly thought it would be harder
			ScreenConfig.FullscreenDisplay = ScreenConfig.FullscreenDisplay < displaySizes.Count 
				? ScreenConfig.FullscreenDisplay 
				: 0;
			Vector2 displaySize = displaySizes[ScreenConfig.FullscreenDisplay];

			DetermineScreenScale((int)displaySize.X, (int)displaySize.Y);

			Screen = LoadRenderTexture(ScreenX, ScreenY);
			SetTextureFilter(Screen.Texture, TextureFilter.Bilinear);
			SetWindowMonitor(ScreenConfig.FullscreenDisplay);

			ToggleBorderlessWindowed();

			Offset = new Vector2(
				MathF.Floor((displaySize.X - ScreenX) / 2),
				MathF.Floor((displaySize.Y - ScreenY) / 2)
			);
		}
		else
		{
			Offset = new Vector2(0, 0);
			ScreenX = 800;
			ScreenY = 700;
			ScreenScale = 1;
			Screen = LoadRenderTexture(ScreenX, ScreenY);
			SetTextureFilter(Screen.Texture, TextureFilter.Bilinear);
		}
		GameScreenSize = new Vector2(ScreenX, ScreenY);

		//I'll add the rest later.
		_displayChangeInProgress = false;
	}

	public static void ToggleFullscreen(
		Screens.Screens? sourceScreen = null,
		bool showConfirmDialog = true,
		bool ingameSwitch = true
	)
	{
		while (_displayChangeInProgress)
		{
			continue;
		}

		bool fullscreen = (bool)Game.game.Settings["fullscreen"]!;

		Game.game.Settings["fullscreen"] = fullscreen;
		Game.game.SaveSettings();

		SetDisplayMode(
			sourceScreen,
			showConfirmDialog,
			ingameSwitch
		);
	}

	private static void DetermineScreenScale(int x, int y)
	{
		int scaleX = x / 200;
		int scaleY = y / 175;
		ScreenScale = Math.Min(scaleX, scaleY) / 4;
		ScreenX = 800 * ScreenScale;
		ScreenY = 700 * ScreenScale;

		Offset = new Vector2(
			MathF.Floor((x - ScreenX) / 2), 
			MathF.Floor((y - ScreenY) / 2)
		);
		GameScreenSize = new Vector2(ScreenX, ScreenY);
	}
}