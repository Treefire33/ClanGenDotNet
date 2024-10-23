﻿using ClanGenDotNet.Scripts;
using ClanGenDotNet.Scripts.Cats;
using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.Game_Structure;
using ClanGenDotNet.Scripts.HouseKeeping;
using ClanGenDotNet.Scripts.Screens;
using static ClanGenDotNet.Scripts.Game_Structure.Game;
using static ClanGenDotNet.Scripts.Utility;

namespace ClanGenDotNet;

public class ClanGenMain
{
	public unsafe static void Main(string[] args)
	{
		SetTraceLogLevel((int)LOG_ERROR);
		SetConfigFlags(FLAG_WINDOW_RESIZABLE | FLAG_VSYNC_HINT);
		InitWindow(game.ScreenX, game.ScreenY, "ClanGen.Net");
		SetWindowMinSize(800, 700);

		DataDirectory.SetUpDataDirectory();
		game.SetSettingsFromLoaded();
		game.LoadSettings();
		ScreenSettings.ToggleFullscreen(showConfirmDialog: false, ingameSwitch: false);

		Resources.LoadResources();
		Sprites.LoadAll();

		AllScreens.InstanceScreens();
		SetTargetFPS((int)game.Switches["fps"]!);
		game.AllScreens[game.CurrentScreen].ScreenSwitches();

		NPatchInfo frameNPatch = new();
		Texture2D frame = LoadTexture(".\\Resources\\frame.png");
		frameNPatch.source = new Rectangle(0, 0, frame.width, frame.height);
		frameNPatch.top = 10;
		frameNPatch.bottom = 10;
		frameNPatch.left = 10;
		frameNPatch.right = 10;

		Texture2D menuLogoless = LoadTexture(".\\Resources\\menu_logoless.png");

		DiscordRPC discordRPC = null;
		try
		{
			discordRPC = new();
			discordRPC.UpdateActivity("start screen");
		}
		catch { Console.WriteLine("DiscordRPC unable to start."); }

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
					GetScreenWidth(), GetScreenHeight()
				),
				new Vector2(0),
				0,
				WHITE
			);
			DrawTextureNPatch(
				frame,
				frameNPatch,
				new Rectangle(
					((GetScreenWidth() - (ScreenSettings.GameScreenSize.X * ScreenSettings.ScreenScale)) * 0.5f) - (frame.width / 2),
					((GetScreenHeight() - (ScreenSettings.GameScreenSize.Y * ScreenSettings.ScreenScale)) * 0.5f) - (frame.width / 2),
					(ScreenSettings.GameScreenSize.X + frame.width) * ScreenSettings.ScreenScale,
					(ScreenSettings.GameScreenSize.Y + frame.height) * ScreenSettings.ScreenScale
				),
				new Vector2(0),
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
				new Vector2(0),
				0,
				WHITE
			);
			EndDrawing();
		}

		UnloadRenderTexture(ScreenSettings.Screen);

		CloseWindow();
	}
}
