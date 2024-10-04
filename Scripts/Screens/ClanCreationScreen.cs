using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.Game_Structure;
using ClanGenDotNet.Scripts.UI;
using Raylib_cs;
using System.Text.RegularExpressions;
using static ClanGenDotNet.Scripts.Game_Structure.Game;
using static ClanGenDotNet.Scripts.Resources;
using static ClanGenDotNet.Scripts.Utility;
using static Raylib_cs.Raylib;

namespace ClanGenDotNet.Scripts.Screens
{
	public partial class ClanCreationScreen(string name = "clan creation screen") : Screens(name)
	{
		private readonly string _classicDetails = "This mode is Clan Generator at it's most basic.\n" +
			"The player will not be expected to manage the minutia of Clan life.\n\n" +
			"Perfect for a relaxing game session or for focusing on storytelling.\n\n" +
			"With this mode you are the eye in the sky, watching the Clan as their story unfolds.";
		private readonly string _expandedDetails = "A more hands-on experience." +
			"This mode has everything in Classic Mode as well as more management-focused features.\n\n" +
			"New features include:\n- Illnesses, Injuries, and Permanent Conditions\n" +
			"- Herb gathering and treatment\n " +
			"- Ability to choose patrol type\n\n " +
			"With this mode you'll be making the important Clan-life decisions.";
		private readonly string _cruelDetails = "This mode has all the features of Expanded mode, but is significantly more difficult. " +
			"If you'd like a challenge with a bit of brutality, then this mode is for you.\n\n" +
			"You heard the warnings... a Cruel Season is coming. Will you survive?\n\n" +
			"-COMING SOON-";

		private string _gameMode = "classic";
		private string _clanName = "";

		private object? _symbolSelected = null;
		private int _tagListDen = 0;
		private string? _biomeSelected = null;
		private int _selectedCampTag = 1;
		private string? _selectedSeason = null;
		private string _campSelected = "1";

		private string _subScreen = "gamemode";
		private Dictionary<string, UIElement> _elements = [];
		private Dictionary<string, UIButton> _tabs = [];
		private Dictionary<string, UIButton> _symbolButtons = [];

		private int _currentPage = 1;
		private int _rerollsLeft = game.Config.ClanCreation.Rerolls;

		private UIButton _mainMenuButton;
		private UITextBox _menuWarning;

		public override void ScreenSwitches()
		{
			base.ScreenSwitches();
			_currentPage = 1;
			_subScreen = "gamemode";
			_gameMode = "classic";
			_clanName = "";
			_menuWarning = new(
				UIScale(new ClanGenRect(25, 25, 600, -1)),
				"Note: going back to main menu resets the generated cats.",
				22,
				TextAlignment.Left,
				Color.White,
				game.Manager
			);
			_mainMenuButton = new(
				UIScale(new ClanGenRect(25, 50, 153, 30)), 
				ButtonStyle.Squoval, 
				"Main Menu", 
				25, 
				game.Manager
			);
			OpenGameMode();
		}

		private void OpenGameMode()
		{
			ClearPage();
			_subScreen = "gamemode";

			_elements.Add("gamemode_background", new UIImage(
				UIScale(new ClanGenRect(325, 130, 399, 461)),
				GameModeTextBox,
				game.Manager
			));
			_elements.Add("permi_warning", new UITextBox(
				UIScale(new ClanGenRect(100, 581, 600, 40)),
				"Your Clan's game mode is permanent and cannot be changed after Clan creation.",
				20,
				TextAlignment.Center,
				Color.Black,
				game.Manager
			));

			//buttons
			_elements.Add("classic_mode_button", new UIButton(
				UIScale(new ClanGenRect(109, 240, 132, 30)),
				ButtonStyle.Squoval,
				"classic",
				20,
				game.Manager
			));
			_elements.Add("expanded_mode_button", new UIButton(
				UIScale(new ClanGenRect(94, 320, 162, 34)),
				ButtonStyle.Squoval,
				"expanded",
				20,
				game.Manager
			));
			_elements.Add("cruel_mode_button", new UIButton(
				UIScale(new ClanGenRect(100, 400, 150, 30)),
				ButtonStyle.Squoval,
				"cruel",
				20,
				game.Manager
			));

			_elements.Add("previous_step", new UIButton(
				UIScale(new ClanGenRect(253, 620, 147, 30)),
				ButtonStyle.Squoval,
				"previous",
				20,
				game.Manager
			));
			_elements["previous_step"].SetActive(false);
			_elements.Add("next_step", new UIButton(
				UIScale(new ClanGenRect(400, 620, 147, 30)),
				ButtonStyle.Squoval,
				"next",
				20,
				game.Manager
			));

			_elements.Add("random_clan_checkbox", new UICheckbox(
				UIScale(new ClanGenRect(560, -32, 34, 34)).AnchorTo(AnchorPosition.TopLeft, _elements["previous_step"].RelativeRect),
				"Quick Start",
				game.Manager
			));

			_elements.Add("mode_details", UITextBox.UITextBoxFromStyle(
				UIScale(new ClanGenRect(325, 160, 405, 461)),
				"",
				TextBoxStyle.HorizLeft20White,
				game.Manager
			));
			if(_elements["mode_details"] is UITextBox textBox) { textBox.SetPadding(new(40, 40)); }
			_elements.Add("mode_name", UITextBox.UITextBoxFromStyle(
				UIScale(new ClanGenRect(425, 135, 200, 27)),
				"classic",
				TextBoxStyle.HorizCenter20White,
				game.Manager
			));
		}

		private void OpenNameClan()
		{
			ClearPage();
			_subScreen = "name clan";

			_elements.Add("background", new UIImage(
				UIScale(new ClanGenRect(0, 0, game.ScreenX, game.ScreenY)),
				NameClanImage,
				game.Manager
			));

			_elements.Add("random", new UIButton(
				UIScale(new ClanGenRect(224, 595, 34, 34)),
				ButtonStyle.Squoval,
				"⚄",
				20,
				game.Manager
			));
			_elements.Add("error", new UITextBox(
				UIScale(new ClanGenRect(506, 1310, 596, -1)),
				"",
				20,
				TextAlignment.Left,
				Color.Black, 
				game.Manager
			));

			_elements.Add("previous_step", new UIButton(
				UIScale(new ClanGenRect(253, 635, 147, 30)),
				ButtonStyle.Squoval,
				"previous",
				20,
				game.Manager
			));
			_elements.Add("next_step", new UIButton(
				UIScale(new ClanGenRect(400, 635, 147, 30)),
				ButtonStyle.Squoval,
				"next",
				20,
				game.Manager
			));
			_elements["next_step"].SetActive(false);

			_elements.Add("name_entry", new UITextInput(
				UIScale(new ClanGenRect(265, 597, 140, 29)),
				"",
				11,
				game.Manager
			));
			_elements.Add("clan", new UITextBox(
				UIScale(new ClanGenRect(375, 600, 140, 29)),
				"-Clan",
				20,
				TextAlignment.Center,
				Color.White,
				game.Manager
			));
			_elements.Add("reset_name", new UIButton(
				UIScale(new ClanGenRect(455, 595, 134, 30)),
				ButtonStyle.Squoval,
				"Reset Name",
				20,
				game.Manager
			));
		}

		public override void HandleEvent(Event evnt)
		{
			base.HandleEvent(evnt);

			if (evnt.EventType == EventType.LeftMouseClick)
			{
				if (evnt.Element == _mainMenuButton)
				{
					ChangeScreen("start screen");
				}
				switch (_subScreen)
				{
					case "gamemode":
						HandleGameModeEvent(evnt);
						if (evnt.Element == _elements["next_step"])
						{
							game.Settings["gamemode"] = _gameMode;
							OpenNameClan();
						}
						break;
					case "name clan":
						HandleNameClanEvent(evnt);
						break;
				}
			}
		}

		private void HandleGameModeEvent(Event evnt)
		{
			if (evnt.Element == _elements["classic_mode_button"])
			{
				_gameMode = "classic";
				this.RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["expanded_mode_button"])
			{
				_gameMode = "expanded";
				this.RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["cruel_mode_button"])
			{
				_gameMode = "cruel";
				this.RefreshTextAndButtons();
			}
		}

		private void HandleNameClanEvent(Event evnt)
		{
			if (evnt.Element == _elements["reset_name"] && evnt.Element is UITextInput input)
			{
				input.SetText("");
			}
			if (evnt.Element == _elements["random"] && evnt.Element is UITextInput input2)
			{
				//input2.SetText(RandomClanName());
			}
			if (evnt.Element == _elements["next_step"] && evnt.Element is UIButton next)
			{
				input = (UITextInput)_elements["name_entry"];
				string newName = ClanNamePattern().Replace(input.GetText(), "").Trim();
				if (newName == null && _elements["error"] is UITextBox textBox)
				{
					textBox.SetText("Your Clan's name cannot be empty");
					return;
				}
				//check if in clanlist here
				_clanName = newName;
				//open choose leader
			}
			else if (evnt.Element == _elements["previous_step"] && evnt.Element is UIButton prev)
			{
				_clanName = "";
				OpenGameMode();
			}
		}

		private void RefreshTextAndButtons()
		{
			if (this._subScreen == "gamemode")
			{
				if (_elements["mode_name"] is UITextBox modeName && _elements["mode_details"] is UITextBox modeDesc)
				{
					string displayMode;
					string displayText;
					switch (this._gameMode)
					{
						case "classic":
							displayMode = "Classic Mode";
							displayText = this._classicDetails;
							break;
						case "expanded":
							displayMode = "Expanded Mode";
							displayText = this._expandedDetails;
							break;
						case "cruel":
							displayMode = "Cruel Mode";
							displayText = this._cruelDetails;
							break;
						default:
							displayMode = "ERROR: Not a Valid Mode!";
							displayText = "Let me guess, tried adding in a new mode, failed to properly account for it,\nso now you're here.";
							break;
					}
					modeDesc.SetText(displayText);
					modeName.SetText(displayMode);
				}

				switch (this._gameMode)
				{
					case "classic":
						_elements["classic_mode_button"].SetActive(false);
						_elements["expanded_mode_button"].SetActive(true);
						_elements["cruel_mode_button"].SetActive(true);
						break;
					case "expanded":
						_elements["classic_mode_button"].SetActive(true);
						_elements["expanded_mode_button"].SetActive(false);
						_elements["cruel_mode_button"].SetActive(true);
						break;
					case "cruel":
						_elements["classic_mode_button"].SetActive(true);
						_elements["expanded_mode_button"].SetActive(true);
						_elements["cruel_mode_button"].SetActive(false);
						break;
					default:
						_elements["classic_mode_button"].SetActive(true);
						_elements["expanded_mode_button"].SetActive(true);
						_elements["cruel_mode_button"].SetActive(true);
						break;
				}

				_elements["next_step"].SetActive(this._gameMode != "cruel");
			}
		}

		private void ClearPage()
		{
			foreach (UIElement element in _elements.Values)
			{
				element.Kill();
			}
			_elements.Clear();
			foreach (UIElement element in _tabs.Values)
			{
				element.Kill();
			}
			_tabs.Clear();
			foreach (UIElement element in _symbolButtons.Values)
			{
				element.Kill();
			}
			_symbolButtons.Clear();
		}

		public override void ExitScreen()
		{
			base.ExitScreen();
			ClearPage();
			_mainMenuButton.Kill();
			_mainMenuButton = null;
			_menuWarning.Kill();
			_menuWarning = null;
		}

		[GeneratedRegex(@"[^A-Za-z0-9 ]+")]
		private static partial Regex ClanNamePattern();
	}
}
