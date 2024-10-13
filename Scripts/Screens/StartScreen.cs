using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.HouseKeeping;
using ClanGenDotNet.Scripts.UI;
using Raylib_cs;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;
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

		private UITextBox? _warning;
		private Dictionary<string, UIButton> _socialButtons = new Dictionary<string, UIButton>();

		public override void ScreenSwitches()
		{
			base.ScreenSwitches();
			_menu = new(new ClanGenRect(0, 0, game.ScreenX, game.ScreenY), MenuImage, game.Manager);
			_continue = new(
				UIScale(new ClanGenRect(70, 310, 200, 30)), 
				ButtonStyle.MainMenu, 
				"continue", 
				20, 
				game.Manager
			);
			_switchClan = new(
				UIScale(new ClanGenRect(70, 15, 200, 30)).AnchorTo(AnchorPosition.TopLeft, _continue.RelativeRect), 
				ButtonStyle.MainMenu, 
				"switch clan", 
				20,
				game.Manager
			);
			_newClan = new(
				UIScale(new ClanGenRect(70, 15, 200, 30)).AnchorTo(AnchorPosition.TopLeft, _switchClan.RelativeRect), 
				ButtonStyle.MainMenu, 
				"new clan", 
				20, 
				game.Manager
			);
			_settings = new(
				UIScale(new ClanGenRect(70, 15, 200, 30)).AnchorTo(AnchorPosition.TopLeft, _newClan.RelativeRect), 
				ButtonStyle.MainMenu, 
				"settings and info", 
				20, 
				game.Manager
			);
			_quit = new(
				UIScale(new ClanGenRect(70, 15, 200, 30)).AnchorTo(AnchorPosition.TopLeft, _settings.RelativeRect), 
				ButtonStyle.MainMenu, 
				"quit", 
				20, 
				game.Manager
			);
			_warning = new(
				UIScale(new ClanGenRect(0, 600, 800, 40)),
				"Warning: This game contains mild depictions of gore, canon-typical violence and animal abuse.",
				20,
				TextAlignment.Center,
				Color.White,
				game.Manager
			);

			_socialButtons.Add("twitter_button", new UIButton(
				UIScale(new ClanGenRect(12, 647, 40, 40)),
				ButtonID.TwitterButton,
				"",
				0,
				game.Manager
			));
			_socialButtons.Add("tumblr_button", new UIButton(
				UIScale(new ClanGenRect(5, 647, 40, 40))
					.AnchorTo(AnchorPosition.LeftTarget, _socialButtons.Last().Value.RelativeRect),
				ButtonID.TumblrButton,
				"",
				0,
				game.Manager
			));
			_socialButtons.Add("discord_button", new UIButton(
				UIScale(new ClanGenRect(7, 647, 40, 40))
					.AnchorTo(AnchorPosition.LeftTarget, _socialButtons.Last().Value.RelativeRect),
				ButtonID.DiscordButton,
				"",
				0,
				game.Manager
			));

			_continue.SetActive(false);
			_switchClan.SetActive(false);
		}

		public override void HandleEvent(Event evnt)
		{
			base.HandleEvent(evnt);

			if (evnt.EventType == EventType.LeftMouseClick && evnt.Element is UIButton element)
			{
				Dictionary<UIButton, string> screens = new Dictionary<UIButton, string>{
					{ _continue!, "camp screen" },
					{ _switchClan!, "switch clan screen" },
					{ _newClan!, "clan creation screen" },
					{ _settings!, "settings screen" },
				};
				if (screens.TryGetValue(element, out string? value))
				{
					ChangeScreen(value);
				}
				if (element == _quit)
				{
					Environment.Exit(0);
				}
				else if (_socialButtons.ContainsValue(element))
				{
					string link = "https://github.com/ClanGenOfficial/clangen";
					if (element == _socialButtons["twitter_button"]) { link = "https://twitter.com/OfficialClangen"; }
					else if (element == _socialButtons["tumblr_button"]) { link = "https://officialclangen.tumblr.com/"; }
					else if (element == _socialButtons["discord_button"]) { link = "https://discord.gg/clangen"; }
					try
					{
						if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
						{
							Process.Start("open", $"-u {link}");
						}
						else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
						{
							Process.Start("cmd.exe", $"/C start \"\" {link}");
						}
						else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
						{
							Process.Start("xdg-open", $"{link}");
						}
					}
					catch { Console.WriteLine("Couldn't open link, blame Treefire33"); }
				}
			}
			else if (evnt.EventType == EventType.KeyPressed && (bool)game.Settings["keybinds"]! == true)
			{
				if (
					evnt.KeyCode == (KeyboardKey.Enter | KeyboardKey.Space | KeyboardKey.One)
					&& _continue!.Active
				)
				{
					ChangeScreen("camp screen");
				}
				if (evnt.KeyCode >= KeyboardKey.Two && evnt.KeyCode <= KeyboardKey.Five)
				{
					switch (evnt.KeyCode)
					{
						case KeyboardKey.Three:
							ChangeScreen("clan creation screen");
						break;
						case KeyboardKey.Four:
							ChangeScreen("settings screen");
						break;
						case KeyboardKey.Five:
							Environment.Exit(0);
						break;
					}
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
			foreach(KeyValuePair<string, UIButton> socialButton in _socialButtons)
			{
				socialButton.Value.Kill();
			}
			_socialButtons.Clear();
			_warning!.Kill();
			_warning = null;
		}
	}
}
