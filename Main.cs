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
		//Sprites.LoadAll();
		//LoadResources();

		var clanList = game.ReadClans();
		if (clanList != null)
		{
			game.Switches["clan_list"] = clanList;
			try
			{
				LoadCat.LoadCats();
				var versionInfo = Clan.LoadClan();
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to load");
				Console.WriteLine(e);
			}
		}

		Thread.Sleep(1000);

		_finishedLoading = true;
	}

	private static void LoadingAnimation(float scale = 1)
	{
		List<Texture2D> frames = [];
		for (int index = 1; index < 11; index++)
		{
			frames.Add(LoadTexture($".\\Resources\\Images\\LoadingAnimation\\Startup\\{index}.png"));
		}

		int x = ScreenSettings.Screen.Texture.Width / 2;
		int y = ScreenSettings.Screen.Texture.Height / 2;

		int i = 0;
		while (!_finishedLoading)
		{
			SetTargetFPS(8);

			BeginTextureMode(ScreenSettings.Screen);
			ClearBackground(GetThemeColour());
			DrawTexturePro(
				frames[i],
				new Rectangle(0, 0, frames[i].Width, frames[i].Height),
				new Rectangle(
					x - frames[i].Width / 2,
					y - frames[i].Height / 2,
					frames[i].Width, frames[i].Height
				),
				Vector2.Zero,
				0,
				White
			);
			EndTextureMode();

			BeginDrawing();
			ClearBackground(White);
			DrawTexturePro(
				ScreenSettings.Screen.Texture,
				new Rectangle(0, 0, ScreenSettings.Screen.Texture.Width, -ScreenSettings.Screen.Texture.Height),
				new Rectangle(
					(GetScreenWidth() - (ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale)) * 0.5f,
					(GetScreenHeight() - (ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale)) * 0.5f,
					ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale,
					ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale
				),
				Vector2.Zero,
				0,
				White
			);
			EndDrawing();

		i++;

			if (i >= frames.Count)
			{
				i = 0;
			}
		}

		foreach (var frame in frames)
		{
			UnloadTexture(frame);
		}
	}

	public static void Main(string[] args)
	{
		SetTraceLogLevel(Error);
		SetConfigFlags(ResizableWindow | VSyncHint);
		InitWindow(game.ScreenX, game.ScreenY, "ClanGen.Net");
		SetWindowMinSize(800, 700);
		SetWindowIcon(_windowIcon);

		UnloadImage(_windowIcon);

		SetExitKey(Null); //So that way we can use KEY_ESCAPE

		SetUpDataDirectory();
		game.Manager.LoadTheme(
			".\\Resources\\Theme\\default_theme.json"
		);

		game.SetSettingsFromLoaded();
		game.LoadSettings();
		ScreenSettings.ToggleFullscreen(showConfirmDialog: false, ingameSwitch: false);

		Sprites.LoadAll();
		LoadResources();

		Thread loadingThread = new(new ThreadStart(LoadData));
		loadingThread.Start();
		LoadingAnimation();
		loadingThread.Join();

		AllScreens.InstanceScreens();
		game.AllScreens[game.CurrentScreen].ScreenSwitches();

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

		SetTargetFPS(game.Switches["fps"]);

		while (!WindowShouldClose() && _finishedLoading)
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
			ClearBackground(White);
			DrawTexturePro(
				MenuLogolessImage,
				new Rectangle(0, 0, GetScreenWidth(), GetScreenHeight()),
				new Rectangle(
					0, 0,
					GetScreenWidth(), GetScreenHeight()
				),
				Vector2.Zero,
				0,
				White
			);
			DrawTextureNPatch(
				Frame,
				FrameNPatch,
				new Rectangle(
					((GetScreenWidth() - (ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale)) * 0.5f) - (Frame.Width / 2),
					((GetScreenHeight() - (ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale)) * 0.5f) - (Frame.Height / 2),
					(ScreenSettings.GameScreenSize.X + Frame.Width) * ScreenSettings.ScreenScale,
					(ScreenSettings.GameScreenSize.Y + Frame.Height) * ScreenSettings.ScreenScale
				),
				Vector2.Zero,
				0,
				White
			);
			DrawTexturePro(
				ScreenSettings.Screen.Texture,
				new Rectangle(0, 0, ScreenSettings.Screen.Texture.Width, -ScreenSettings.Screen.Texture.Height),
				new Rectangle(
					(GetScreenWidth() - (ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale)) * 0.5f,
					(GetScreenHeight() - (ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale)) * 0.5f,
					ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale,
					ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale
				),
				Vector2.Zero,
				0,
				White
			);
			EndDrawing();
		}

		UnloadRenderTexture(ScreenSettings.Screen);

		CloseWindow();
	}
}
