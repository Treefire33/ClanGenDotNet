﻿using ClanGenDotNet.Scripts.Events;
using static ClanGenDotNet.Scripts.Game_Structure.Game;

namespace ClanGenDotNet.Scripts.Screens;

public class Screens
{
	public string Name;
	public static Dictionary<string, UIButton> MenuButtons = new()
	{
		{
			"events_screen", 
			new UIButton(
				UIScale(new ClanGenRect(246, 60, 82, 30)),
				ButtonStyle.MenuLeft,
				"Events",
				visible: false
			)
		},
		{
			"camp_screen",
			new UIButton(
				UIScale(new ClanGenRect(0, 60, 58, 30))
					.AnchorTo(AnchorPosition.LeftTarget, MenuButtons!.Last().Value.RelativeRect),
				ButtonStyle.MenuMiddle,
				"Camp", 
				visible: false
			)
		},
		{
			"catlist_screen",
			new UIButton(
				UIScale(new ClanGenRect(0, 60, 88, 30))
					.AnchorTo(AnchorPosition.LeftTarget, MenuButtons!.Last().Value.RelativeRect),
				ButtonStyle.MenuMiddle,
				"Cat List",
				visible: false
			)
		},
		{
			"patrol_screen",
			new UIButton(
				UIScale(new ClanGenRect(0, 60, 80, 30))
					.AnchorTo(AnchorPosition.LeftTarget, MenuButtons!.Last().Value.RelativeRect),
				ButtonStyle.MenuRight,
				"Patrol",
				visible: false
			)
		},
		{
			"main_menu",
			new UIButton(
				UIScale(new ClanGenRect(25, 25, 153, 30)),
				ButtonStyle.Squoval,
				GetArrow(3) + "Main Menu",
				visible: false
			)
		},
		{
			"alleginaces",
			new UIButton(
				UIScale(ClanGenRect.FromTopRight(UIScaleOffset(new(-25, 25)), new(118, 30))),
				ButtonStyle.Squoval,
				"Allegiances",
				false
			)
		},
		{
			"clan_settings",
			new UIButton(
				UIScale(ClanGenRect.FromTopRight(UIScaleOffset(new(-25, 5)), new(85, 30)))
					.AnchorTo(AnchorPosition.TopLeft, MenuButtons!.Last().Value.RelativeRect),
				ButtonStyle.Squoval,
				"Settings",
				false
			)
		}
	};

	public Screens(string name = "")
	{
		Name = name;
		if (name != "")
		{
			game.AllScreens.Add(name, this);
		}
	}

	public void ChangeScreen(string newScreen)
	{
		game.LastScreenForUpdate = Name;

		List<string> screensForProfile = ["camp screen", "list screen", "events screen"];
		List<string> screensForList = ["list screen", "profile screen", "sprite inspect screen"];
		if (screensForProfile.Contains(Name))
		{
			game.LastScreenForProfile = Name;
		}
		else if (!screensForList.Contains(Name))
		{
			//last list for profile
		}

		game.Switches["cur_screen"] = newScreen;
		game.SwitchScreens = true;
	}

	public virtual void OnUse()
	{

	}

	public virtual void HandleEvent(Event evnt)
	{

	}

	public virtual void ScreenSwitches()
	{
		SetMenuButtonsVisibility(false);
	}

	public virtual void ExitScreen()
	{

	}

	protected static void SetMenuButtonsVisibility(bool visible = false)
	{
		/*if (MenuButtons != null)
		{
			foreach (var button in MenuButtons)
			{
				button.Value.SetVisibility(visible);
			}
		}*/
	}
}
