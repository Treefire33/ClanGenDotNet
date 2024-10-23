using ClanGenDotNet.Scripts.Events;
using static ClanGenDotNet.Scripts.Game_Structure.Game;

namespace ClanGenDotNet.Scripts.Screens;

public class Screens
{
	public string Name;

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

	}

	public virtual void ExitScreen()
	{

	}
}
