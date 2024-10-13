using ClanGenDotNet.Scripts;
using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.Game_Structure;
using ClanGenDotNet.Scripts.Screens;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static ClanGenDotNet.Scripts.Game_Structure.Game;
using ClanGenDotNet.Scripts.HouseKeeping;
using System.Numerics;

namespace ClanGenDotNet
{
	public class ClanGenMain
	{
		public static void Main(string[] args)
		{
			SetTraceLogLevel(TraceLogLevel.Error);
			SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.VSyncHint);
			InitWindow(game.ScreenX, game.ScreenY, "ClanGen.Net");
			SetWindowMinSize(800, 700);

			DataDirectory.SetUpDataDirectory();
			game.SetSettingsFromLoaded();
			game.LoadSettings();
			ScreenSettings.ToggleFullscreen(showConfirmDialog: false, ingameSwitch: false);

			Resources.SetFontFilters();

			AllScreens.InstanceScreens();
            SetTargetFPS((int)game.Switches["fps"]!);
            game.AllScreens[game.CurrentScreen].ScreenSwitches();

			NPatchInfo frameNPatch = new();
			Texture2D frame = LoadTexture(".\\Resources\\frame.png");
			frameNPatch.Source = new Rectangle(0, 0, frame.Width, frame.Height);
			frameNPatch.Top = 10;
			frameNPatch.Bottom = 10;
			frameNPatch.Left = 10;
			frameNPatch.Right = 10;

			Texture2D menuLogoless = LoadTexture(".\\Resources\\menu_logoless.png");

			while (!WindowShouldClose())
			{
				BeginTextureMode(ScreenSettings.Screen);
                ClearBackground(new(game.Config.Theme.LightModeBackground[0], game.Config.Theme.LightModeBackground[1], game.Config.Theme.LightModeBackground[2], 255));

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
				}

                game.Manager.ResetEvents();

				EndTextureMode();

				BeginDrawing();
				ClearBackground(new(0, 0, 0, 255));
				DrawTexturePro(
					menuLogoless,
					new Rectangle(0, 0, GetScreenWidth(), GetScreenHeight()),
					new Rectangle(
						0, 0,
						GetScreenWidth(),  GetScreenHeight()
					),
					new(0),
					0,
					Color.White
				);
				DrawTextureNPatch(
					frame,
					frameNPatch,
					new Rectangle(
						(GetScreenWidth() - (ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale)) * 0.5f - (frame.Width / 2),
						(GetScreenHeight() - (ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale)) * 0.5f - (frame.Width / 2),
						(ScreenSettings.GameScreenSize.X + frame.Width) * ScreenSettings.ScreenScale,
						(ScreenSettings.GameScreenSize.Y + frame.Height) * ScreenSettings.ScreenScale
					),
					new(0),
					0,
					Color.White
				);
				DrawTexturePro(
					ScreenSettings.Screen.Texture, 
					new Rectangle(0, 0, ScreenSettings.Screen.Texture.Width, -ScreenSettings.Screen.Texture.Height),
					new Rectangle(
						(GetScreenWidth() - ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale) * 0.5f,
						(GetScreenHeight() - ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale) * 0.5f,
						ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale,
						ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale
					),
					new(0),
					0,
					Color.White
				);
				EndDrawing();
			}

			UnloadRenderTexture(ScreenSettings.Screen);

			CloseWindow();
		}
	}
}
