namespace ClanGenDotNet.Scripts.Screens;

public class CatListScreen(string name = "cat list screen") : Screens(name)
{
	private UIContainer? _listScreenContainer;
	private UIContainer? _catListBar;
	public override void ScreenSwitches()
	{
		base.ScreenSwitches();
		SetMenuButtonsVisibility(true);
		SetDisabledMenuButtons(["catlist_screen"]);

		_listScreenContainer = new(
			UIScale(new ClanGenRect(0, 0, 800, 700))
		);

		_catListBar = new(
			UIScale(new ClanGenRect(104, 134, 700, 400))
		);

		_listScreenContainer.AddElement(_catListBar);
	}

	public override void ExitScreen()
	{
		base.ExitScreen();
		_listScreenContainer?.Kill();
		_catListBar?.Kill();
		_listScreenContainer = null;
		_catListBar = null;
	}
}
