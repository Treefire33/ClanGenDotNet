using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.UI;
using Raylib_cs;
using static ClanGenDotNet.Scripts.Game_Structure.Game;
using static ClanGenDotNet.Scripts.Resources;
using static ClanGenDotNet.Scripts.Utility;
using static Raylib_cs.Raylib;

namespace ClanGenDotNet.Scripts.Screens
{
	public class StartScreen(string name = "start screen") : Screens(name)
	{
		private UIImage? _menu;
		private UIButton? _continue;
		private UIButton? _switchClan;
		private UIButton? _newClan;
		private UIButton? _settings;
		private UIButton? _quit;

		public override void ScreenSwitches()
		{
			base.ScreenSwitches();
			_menu = new(new ClanGenRect(0, 0, game.ScreenX, game.ScreenY), MenuImage, game.Manager);
			_continue = new(
				UIScale(new ClanGenRect(70, 310, 200, 30)), 
				ButtonStyle.MainMenu, 
				"continue", 
				30, 
				game.Manager
			);
			_switchClan = new(
				UIScale(new ClanGenRect(70, 15, 200, 30)).AnchorTo(AnchorPosition.TopLeft, _continue.RelativeRect), 
				ButtonStyle.MainMenu, 
				"switch clan", 
				30,
				game.Manager
			);
			_newClan = new(
				UIScale(new ClanGenRect(70, 15, 200, 30)).AnchorTo(AnchorPosition.TopLeft, _switchClan.RelativeRect), 
				ButtonStyle.MainMenu, 
				"new clan", 
				30, 
				game.Manager
			);
			_settings = new(
				UIScale(new ClanGenRect(70, 15, 200, 30)).AnchorTo(AnchorPosition.TopLeft, _newClan.RelativeRect), 
				ButtonStyle.MainMenu, 
				"settings", 
				30, 
				game.Manager
			);
			_quit = new(
				UIScale(new ClanGenRect(70, 15, 200, 30)).AnchorTo(AnchorPosition.TopLeft, _settings.RelativeRect), 
				ButtonStyle.MainMenu, 
				"quit", 
				30, 
				game.Manager
			);

			_continue.SetActive(false);
			_switchClan.SetActive(false);
		}

		public override void HandleEvent(Event evnt)
		{
			base.HandleEvent(evnt);

			if (evnt.EventType == EventType.LeftMouseClick)
			{
				if (evnt.Element == _continue)
				{

				}
				else if (evnt.Element == _newClan)
				{
					ChangeScreen("clan creation screen");
				}
				else if (evnt.Element == _settings)
				{
					ChangeScreen("settings screen");
				}
				else if (evnt.Element == _quit)
				{

				}
			}
		}

		public override void ExitScreen()
		{
			base.ExitScreen();
			_menu!.Kill();
			_menu = null;
			_continue!.Kill();
			_continue = null;
			_switchClan!.Kill();
			_switchClan = null;
			_newClan!.Kill();
			_newClan = null;
			_settings!.Kill();
			_settings = null;
			_quit!.Kill();
			_quit = null;
		}
	}
}
