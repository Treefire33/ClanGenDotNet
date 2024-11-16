using ClanGenDotNet.Scripts;
using ClanGenDotNet.Scripts.Cats;
using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.Screens;

namespace ClanGenDotNet;

public class ClanGenMain
{
	private static Image _windowIcon = LoadImage(".\\Resources\\Images\\icon.png");
	private static bool _finishedLoading = false;

	private static void LoadData()
	{
		Sprites.LoadAll();
		Resources.LoadResources();

		var clanList = game.ReadClans();
		try
		{
			if (clanList != null)
			{
				game.Switches["clan_list"] = clanList;
			}
			//LoadCats();
		}
		catch
		{
			Console.WriteLine("Failed to load");
		}

		SetUpDataDirectory();
		game.Manager.LoadTheme(
			".\\Resources\\Theme\\default_theme.json"
		);

		_finishedLoading = true;
	}

	public static void Main(string[] args)
	{
		SetTraceLogLevel((int)LOG_ERROR);
		SetConfigFlags(FLAG_WINDOW_RESIZABLE | FLAG_VSYNC_HINT);
		InitWindow(game.ScreenX, game.ScreenY, "ClanGen.Net");
		SetWindowMinSize(800, 700);
		SetWindowIcon(_windowIcon);

		UnloadImage(_windowIcon);

		SetExitKey(KEY_NULL); //So that way we can use KEY_ESCAPE

		LoadData();

		game.SetSettingsFromLoaded();
		game.LoadSettings();
		ScreenSettings.ToggleFullscreen(showConfirmDialog: false, ingameSwitch: false);
		AllScreens.InstanceScreens();
		game.AllScreens[game.CurrentScreen].ScreenSwitches();

		Texture2D menuLogoless = LoadTexture(".\\Resources\\Images\\menu_logoless.png");

		DiscordRPC? discordRPC = null;
		if ((bool)game.Settings["discord"]!)
		{ 
			try
			{
				discordRPC = new();
				discordRPC.UpdateActivity();
			}
			catch { Console.WriteLine("DiscordRPC unable to start."); }
		}

		//Console.WriteLine(game.Manager.Theme.ToString());

		while (!WindowShouldClose())
		{
			discordRPC?.Discord.RunCallbacks();
			BeginTextureMode(ScreenSettings.Screen);
			ClearBackground(GetThemeColour());

			game.UpdateGame();

			game.Manager.DrawUI();

			int keyPressed = GetKeyPressed();
			if (keyPressed != 0)
			{
				game.Manager.PushEvent(new Event(keyPressed, EventType.KeyPressed));
			}
			foreach (Event evnt in game.Manager.UIEvents)
			{
				game.AllScreens[game.CurrentScreen].HandleEvent(evnt);
			}

			if (game.SwitchScreens)
			{
				game.AllScreens[game.LastScreenForUpdate].ExitScreen();
				game.AllScreens[game.CurrentScreen].ScreenSwitches();
				game.SwitchScreens = false;
				discordRPC?.UpdateActivity();
			}

			game.Manager.ResetEvents();
			EndTextureMode();

			BeginDrawing();
			ClearBackground(WHITE);
			DrawTexturePro(
				menuLogoless,
				new Rectangle(0, 0, GetScreenWidth(), GetScreenHeight()),
				new Rectangle(
					0, 0,
					GetScreenWidth(), GetScreenHeight()
				),
				Vector2.Zero,
				0,
				WHITE
			);
			DrawTextureNPatch(
				Resources.Frame,
				Resources.FrameNPatch,
				new Rectangle(
					((GetScreenWidth() - (ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale)) * 0.5f) - (Resources.Frame.width / 2),
					((GetScreenHeight() - (ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale)) * 0.5f) - (Resources.Frame.height / 2),
					(ScreenSettings.GameScreenSize.X + Resources.Frame.width) * ScreenSettings.ScreenScale,
					(ScreenSettings.GameScreenSize.Y + Resources.Frame.height) * ScreenSettings.ScreenScale
				),
				Vector2.Zero,
				0,
				WHITE
			);
			DrawTexturePro(
				ScreenSettings.Screen.texture,
				new Rectangle(0, 0, ScreenSettings.Screen.texture.width, -ScreenSettings.Screen.texture.height),
				new Rectangle(
					(GetScreenWidth() - (ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale)) * 0.5f,
					(GetScreenHeight() - (ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale)) * 0.5f,
					ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale,
					ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale
				),
				Vector2.Zero,
				0,
				WHITE
			);
			EndDrawing();
		}

		UnloadRenderTexture(ScreenSettings.Screen);

		CloseWindow();
	}
}
