﻿namespace ClanGenDotNet.Scripts.Screens;

public static class AllScreens
{
	public static void InstanceScreens()
	{
		_ = new StartScreen();
		_ = new ClanCreationScreen();
		_ = new SettingsScreen();
		_ = new CampScreen();
	}
}
