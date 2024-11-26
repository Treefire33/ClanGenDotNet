using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.UI.Theming;
using static ClanGenDotNet.Scripts.Game_Structure.Game;

namespace ClanGenDotNet.Scripts.Screens;

public class Screens
{
	public string Name;
	public static Dictionary<string, UIElement>? MenuButtons = null;

	public Screens(string name = "")
	{
		Name = name;
		if (name != "")
		{
			game.AllScreens.Add(name, this);
		}
		if (MenuButtons == null)
		{
			MenuButtons = [];
			MenuButtons.Add("events_screen", new UIButton(
				UIScale(new ClanGenRect(246, 60, 82, 30)),
				ButtonStyle.MenuLeft,
				"Events",
				visible: false
			));
			MenuButtons.Add("camp_screen", new UIButton(
				UIScale(new ClanGenRect(0, 60, 58, 30))
					.AnchorTo(AnchorPosition.LeftTarget, MenuButtons!.Last().Value.RelativeRect),
				ButtonStyle.MenuMiddle,
				"Camp",
				visible: false
			));
			MenuButtons.Add("catlist_screen", new UIButton(
				UIScale(new ClanGenRect(0, 60, 88, 30))
					.AnchorTo(AnchorPosition.LeftTarget, MenuButtons!.Last().Value.RelativeRect),
				ButtonStyle.MenuMiddle,
				"Cat List",
				visible: false
			));
			MenuButtons.Add("patrol_screen", new UIButton(
				UIScale(new ClanGenRect(0, 60, 80, 30))
					.AnchorTo(AnchorPosition.LeftTarget, MenuButtons!.Last().Value.RelativeRect),
				ButtonStyle.MenuRight,
				"Patrol",
				visible: false
			));
			MenuButtons.Add("main_menu", new UIButton(
				UIScale(new ClanGenRect(25, 25, 153, 30)),
				ButtonStyle.Squoval,
				GetArrow(3) + "Main Menu",
				visible: false
			));
			var scaleRect = UIScale(new(Vector2.Zero, new(118, 30)));
			scaleRect = ClanGenRect.FromTopRight(UIScaleOffset(new(-25, 25)), scaleRect.Size);
			MenuButtons.Add("alleginaces", new UIButton(
				scaleRect,
				ButtonStyle.Squoval,
				"Allegiances",
				false
			));
			scaleRect = UIScale(new(Vector2.Zero, new(85, 30)));
			scaleRect = ClanGenRect.FromTopRight(UIScaleOffset(new(-25, 5)), scaleRect.Size);
			MenuButtons.Add("clan_settings", new UIButton(
				scaleRect
					.AnchorTo(AnchorPosition.TopTarget, MenuButtons!.Last().Value.RelativeRect),
				ButtonStyle.Squoval,
				"Settings",
				false
			));

			scaleRect = UIScale(new(Vector2.Zero, new(190, 35)));
			scaleRect.BottomLeft = UIScaleDimension(Vector2.Zero);
			MenuButtons.Add("name_background", new UIImage(
				scaleRect.AnchorTo(AnchorPosition.BottomTarget, MenuButtons["camp_screen"].RelativeRect)
					.AnchorTo(AnchorPosition.CenterX),
				ClanNameBackground
			));
			MenuButtons.Last().Value.Hide();

			scaleRect = UIScale(new(Vector2.Zero, new(193, 35)));
			scaleRect.BottomLeft = UIScaleOffset(new(0, 1));
			MenuButtons.Add("heading", new UITextBox(
				scaleRect.AnchorTo(AnchorPosition.BottomTarget, MenuButtons["camp_screen"].RelativeRect)
					.AnchorTo(AnchorPosition.CenterX),
				"",
				new ObjectID("text_box_34_horizcenter_vertcenter", "#dark")
			));
			MenuButtons.Last().Value.Hide();
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
		if (evnt.EventType == EventType.LeftMouseClick)
		{
			if (evnt.Element == MenuButtons!["main_menu"])
			{
				ChangeScreen("start screen");
			}
			else if (evnt.Element == MenuButtons!["camp_screen"])
			{
				ChangeScreen("camp screen");
			}
			else if (evnt.Element == MenuButtons!["catlist_screen"])
			{
				ChangeScreen("cat list screen");
			}
		}
	}

	public virtual void ScreenSwitches()
	{
		SetMenuButtonsVisibility(false);
		UpdateHeadingText(game.Clan!.Name + "Clan");
	}

	public virtual void ExitScreen()
	{

	}

	protected static void UpdateHeadingText(string text)
	{
		((UITextBox)MenuButtons?["heading"]!).SetText(text);
	}

	protected static void SetMenuButtonsVisibility(bool visible = false)
	{
		foreach (var button in MenuButtons!)
		{
			button.Value.SetVisibility(visible);
		}
	}
	
	public static void SetDisabledMenuButtons(List<string> buttons)
	{
		foreach (var button in MenuButtons!)
		{
			if (buttons.Contains(button.Key))
			{
				button.Value.SetActive(false);
			}
			else
			{
				button.Value.SetActive(true);
			}
		}
	}
}
