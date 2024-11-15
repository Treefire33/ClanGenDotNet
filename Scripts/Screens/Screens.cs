using ClanGenDotNet.Scripts.Events;
using static ClanGenDotNet.Scripts.Game_Structure.Game;

namespace ClanGenDotNet.Scripts.Screens;

public class Screens
{
	public string Name;
	public static Dictionary<string, UIButton> MenuButtons = new Dictionary<string, UIButton>()
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
		SetMenuButtonsVisibility();
	}

	public virtual void ExitScreen()
	{

	}

	protected void SetMenuButtonsVisibility(bool visible = false)
	{

	}
}
