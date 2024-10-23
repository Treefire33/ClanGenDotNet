﻿using ClanGenDotNet.Scripts.Cats;
using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.UI;
using System.Text.RegularExpressions;
using static ClanGenDotNet.Scripts.Game_Structure.Game;
using static ClanGenDotNet.Scripts.Resources;
using static ClanGenDotNet.Scripts.Utility;

namespace ClanGenDotNet.Scripts.Screens;

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
	private Cat _leader;
	private Cat _deputy;
	private Cat _medicineCat;
	private List<Cat> _members;

	private Cat? _selectedCat;

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

	private UIButton? _mainMenuButton;
	private UITextBox? _menuWarning;

	private List<Texture2D> _creationBackgrounds = [
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\clan_name_frame.png"),
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\name_clan_light.png"),
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\leader_light.png"),
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\deputy_light.png"),
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\med_light.png"),
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\clan_light.png")
	];

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
			WHITE,
			game.Manager
		);
		_mainMenuButton = new(
			UIScale(new ClanGenRect(25, 50, 153, 30)),
			ButtonStyle.Squoval,
			"Main Menu",
			25,
			game.Manager
		);

		Cat.CreateExampleCats();
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
			BLACK,
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
			ButtonStyle.MenuLeft,
			"previous",
			20,
			game.Manager
		));
		_elements["previous_step"].SetActive(false);
		_elements.Add("next_step", new UIButton(
			UIScale(new ClanGenRect(400, 620, 147, 30)),
			ButtonStyle.MenuRight,
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
		if (_elements["mode_details"] is UITextBox textBox) { textBox.SetPadding(new(40, 40)); }
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

		_elements.Add("under_background", new UIImage(
			UIScale(new ClanGenRect(0, 0, game.ScreenX, game.ScreenY)),
			NameClanImage,
			game.Manager
		));

		_elements.Add("random", new UIButton(
			UIScale(new ClanGenRect(224, 595, 34, 34)),
			ButtonStyle.Squoval,
			"\u2192",
			20,
			game.Manager
		));
		_elements.Add("error", new UITextBox(
			UIScale(new ClanGenRect(506, 1310, 596, -1)),
			"",
			20,
			TextAlignment.Left,
			BLACK,
			game.Manager
		));

		_elements.Add("previous_step", new UIButton(
			UIScale(new ClanGenRect(253, 635, 147, 30)),
			ButtonStyle.MenuLeft,
			"previous",
			20,
			game.Manager
		));
		_elements.Add("next_step", new UIButton(
			UIScale(new ClanGenRect(400, 635, 147, 30)),
			ButtonStyle.MenuRight,
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
			UIScale(new ClanGenRect(375, 600, 100, 25)),
			"-Clan",
			25,
			TextAlignment.VertCenter,
			WHITE,
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

	private void ClanNameHeader()
	{
		_elements.Add("name_backdrop", new UIImage(
			UIScale(new ClanGenRect(292, 100, 216, 50)),
			_creationBackgrounds[0],
			game.Manager
		));
		_elements.Add("clan_name", new UITextBox(
			UIScale(new ClanGenRect(292, 100, 216, 50)),
			_clanName + "Clan",
			20,
			TextAlignment.VertCenter,
			WHITE,
			game.Manager
		));
	}

	private void OpenChooseLeader()
	{
		ClearPage();
		_subScreen = "choose leader";

		_elements.Add("background", new UIImage(
			UIScale(new ClanGenRect(0, 414, 800, 286)),
			_creationBackgrounds[2],
			game.Manager
		));

		ClanNameHeader();

		int xPos = 155;
		int yPos = 235;

		_elements.Add("roll1", new UIButton(
			UIScale(new ClanGenRect(xPos, yPos, new(34))),
			ButtonStyle.Squoval,
			"\u2684",
			20,
			game.Manager
		));
		yPos += 40;
		_elements.Add("roll2", new UIButton(
			UIScale(new ClanGenRect(xPos, yPos, new(34))),
			ButtonStyle.Squoval,
			"\u2684",
			20,
			game.Manager
		));
		yPos += 40;
		_elements.Add("roll3", new UIButton(
			UIScale(new ClanGenRect(xPos, yPos, new(34))),
			ButtonStyle.Squoval,
			"\u2684",
			20,
			game.Manager
		));

		int temp = 80;
		if (_rerollsLeft == -1)
		{
			temp += 5;
		}
		_elements.Add("dice", new UIButton(
			UIScale(new ClanGenRect(temp, 435, 34, 34)),
			ButtonStyle.Squoval,
			"\u2684",
			20,
			game.Manager
		));
		_elements.Add("reroll_count", new UITextBox(
			UIScale(new ClanGenRect(100, 440, 50, 25)),
			_rerollsLeft.ToString(),
			15,
			TextAlignment.Center,
			WHITE,
			game.Manager
		));

		if (game.Config.ClanCreation.Rerolls == 3)
		{
			if (_rerollsLeft <= 2)
			{
				_elements["roll1"].SetActive(false);
			}
			if (_rerollsLeft <= 1)
			{
				_elements["roll2"].SetActive(false);
			}
			if (_rerollsLeft == 0)
			{
				_elements["roll3"].SetActive(false);
			}
			_elements["dice"].Hide();
			_elements["reroll_count"].Hide();
		}
		else
		{
			if (_rerollsLeft == 0)
			{
				_elements["dice"].SetActive(false);
			}
			else if (_rerollsLeft == -1)
			{
				_elements["reroll_count"].Hide();
			}
			_elements["roll1"].Hide();
			_elements["roll2"].Hide();
			_elements["roll3"].Hide();
		}

		CreateCatInfo();

		_elements.Add("select_cat", new UIButton(
			UIScale(new ClanGenRect(234, 348, 332, 52)),
			ButtonID.NineLivesButton,
			"",
			20,
			game.Manager
		));

		_elements.Add("error_message", new UITextBox(
			UIScale(new ClanGenRect(150, 353, 500, 55)),
			"Too young to become leader",
			20,
			TextAlignment.Center,
			WHITE,
			game.Manager
		));

		_elements.Add("previous_step", new UIButton(
			UIScale(new ClanGenRect(253, 400, 147, 30)),
			ButtonStyle.MenuLeft,
			"previous",
			20,
			game.Manager
		));
		_elements.Add("next_step", new UIButton(
			UIScale(new ClanGenRect(0, 400, 147, 30)).AnchorTo(AnchorPosition.LeftTarget, _elements.Last().Value.RelativeRect),
			ButtonStyle.MenuRight,
			"next",
			20,
			game.Manager
		));
		_elements["next_step"].SetActive(false);

		RefreshCatImagesAndInfo();
	}

	public void CreateCatInfo()
	{
		_elements.Add("cat_name", new UITextBox(
			UIScale(new ClanGenRect(0, 10, 250, 60)).AnchorTo(
				AnchorPosition.TopLeft,
				_elements["name_backdrop"].RelativeRect
			).AnchorTo(AnchorPosition.CenterX),
			"",
			20,
			TextAlignment.Center,
			WHITE,
			game.Manager
		));

		_elements.Add("cat_info", new UITextBox(
			UIScale(new ClanGenRect(440, 220, 175, 125)),
			"",
			20,
			TextAlignment.Center,
			WHITE,
			game.Manager
		));
	}

	public override void HandleEvent(Event evnt)
	{
		base.HandleEvent(evnt);

		if (evnt.Element == _mainMenuButton && evnt.EventType == EventType.LeftMouseClick)
		{
			ChangeScreen("start screen");
		}
		switch (_subScreen)
		{
			case "gamemode":
				HandleGameModeEvent(evnt);
				break;
			case "name clan":
				HandleNameClanEvent(evnt);
				break;
			case "choose leader":
				HandleChooseLeaderEvent(evnt);
				break;
		}
	}

	private void HandleGameModeEvent(Event evnt)
	{
		if (evnt.EventType == EventType.LeftMouseClick)
		{
			if (evnt.Element == _elements["classic_mode_button"])
			{
				_gameMode = "classic";
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["expanded_mode_button"])
			{
				_gameMode = "expanded";
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["cruel_mode_button"])
			{
				_gameMode = "cruel";
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["next_step"])
			{
				game.Settings["game_mode"] = _gameMode;
				OpenNameClan();
			}
		}
	}

	private void HandleNameClanEvent(Event evnt)
	{
		var nameEntry = (UITextInput)_elements["name_entry"];
		_elements["next_step"].SetActive(nameEntry.GetText().Length > 0);
		if (evnt.EventType == EventType.LeftMouseClick)
		{
			if (evnt.Element == _elements["reset_name"] && evnt.Element is UITextInput input)
			{
				input.SetText("");
			}

			if (evnt.Element == _elements["random"] && evnt.Element is UITextInput)
			{
				//input2.SetText(RandomClanName());
			}

			if (evnt.Element == _elements["next_step"] && evnt.Element is UIButton)
			{
				input = (UITextInput)_elements["name_entry"];
				string newName = ClanNamePattern().Replace(input.GetText(), "").Trim();
				if (newName == null && _elements["error"] is UITextBox textBox)
				{
					textBox.SetText("Your Clan's name cannot be empty");
					return;
				}
				//check if in clanlist here
				_clanName = newName!;
				//open choose leader
				OpenChooseLeader();
			}
			else if (evnt.Element == _elements["previous_step"] && evnt.Element is UIButton)
			{
				_clanName = "";
				_gameMode = "classic";
				OpenGameMode();
			}
		}
	}

	private void HandleChooseLeaderEvent(Event evnt)
	{
		for (int i = 0; i < 12; i++)
		{
			if (evnt.Element == _elements["cat" + i])
			{
				if (IsKeyDown(KEY_LEFT_SHIFT | KEY_RIGHT_SHIFT))
				{
					var clickedCat = ((UICatButton)evnt.Element!).GetCat();
					if (clickedCat.Age == (Age.Newborn | Age.Kitten | Age.Adolescent))
					{
						_leader = clickedCat;
						_selectedCat = null;
						//OpenChooseDeputy();
					}
				}
				else
				{
					_selectedCat = ((UICatButton)evnt.Element!).GetCat();
					RefreshCatImagesAndInfo(_selectedCat);
					RefreshTextAndButtons();
					return; // prevent handler from comparing to other buttons.
				}
			}
		}
		if (
			evnt.Element == _elements["roll1"] 
			|| evnt.Element == _elements["roll2"]
			|| evnt.Element == _elements["roll3"]
			|| evnt.Element == _elements["dice"]
		)
		{
			_elements["select_cat"].Hide();
			Cat.CreateExampleCats();
			_selectedCat = null;
			if (_elements.TryGetValue("error_message", out UIElement? errorMessage))
			{
				errorMessage.Hide();
			}	
			RefreshCatImagesAndInfo();
			_rerollsLeft -= 1;
			if (game.Config.ClanCreation.Rerolls == 3)
			{
				evnt.Element.SetActive(false);
			}
			else
			{
				((UITextBox)_elements["reroll_count"]).SetText(_rerollsLeft.ToString());
				if (_rerollsLeft == 0)
				{
					evnt.Element.SetActive(false);
				}
			}
			return;
		}
		else if (evnt.Element == _elements["select_cat"])
		{
			_leader = _selectedCat!;
			_selectedCat = null;

		}
		else if (evnt.Element == _elements["previous_step"])
		{
			_clanName = "";
			OpenNameClan();
		}
	}

	private void RefreshTextAndButtons()
	{
		if (_subScreen == "gamemode")
		{
			if (_elements["mode_name"] is UITextBox modeName && _elements["mode_details"] is UITextBox modeDesc)
			{
				string displayMode;
				string displayText;
				switch (_gameMode)
				{
					case "classic":
						displayMode = "Classic Mode";
						displayText = _classicDetails;
						break;
					case "expanded":
						displayMode = "Expanded Mode";
						displayText = _expandedDetails;
						break;
					case "cruel":
						displayMode = "Cruel Mode";
						displayText = _cruelDetails;
						break;
					default:
						displayMode = "ERROR: Not a Valid Mode!";
						displayText = "";
						break;
				}
				modeDesc.SetText(displayText);
				modeName.SetText(displayMode);
			}

			switch (_gameMode)
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

			_elements["next_step"].SetActive(_gameMode != "cruel");
		}
		else if (_subScreen == "choose leader" || _subScreen == "choose deputy" || _subScreen == "choose med cat")
		{
			if (
				_selectedCat != null 
				&& (
					_selectedCat.Age == Age.Newborn 
					|| _selectedCat.Age == Age.Kitten 
					|| _selectedCat.Age == Age.Adolescent)
				)
			{
				_elements["select_cat"].Hide();
				_elements["error_message"].Show();
			}
			else
			{
				_elements["select_cat"].Show();
				_elements["error_message"].Hide();
			}
		}
	}

	private void ClearPage()
	{
		foreach (UIElement element in _elements.Values)
		{
			element.Kill();
		}
		foreach (UIElement element in _tabs.Values)
		{
			element.Kill();
		}
		foreach (UIElement element in _symbolButtons.Values)
		{
			element.Kill();
		}
		_elements.Clear();
		_tabs.Clear();
		_symbolButtons.Clear();
	}

	public void RefreshSelectedCatInfo(Cat? selected = null)
	{
		if (selected != null)
		{
			UITextBox name = (UITextBox)_elements["cat_name"];
			if (_subScreen == "choose leader")
			{
				name.SetText(selected.Name.ToString()! + " --> " + selected.Name.Prefix + "star");
			}
			else
			{
				name.SetText(selected.Name.ToString()!);
			}

			name.Show();
			UITextBox info = (UITextBox)_elements["cat_info"];
			info.SetText(
				selected.Gender+"\n"+selected.Age.ToCorrectString()+"\nnot implemented\nnot implemented"
			);
			info.Show();
		}
		else
		{
			_elements["next_step"].SetActive(false);
			_elements["cat_name"].Hide();
			_elements["cat_info"].Hide();
		}	
	}

	public void RefreshCatImagesAndInfo(Cat? selected = null)
	{
		Vector2 columnPos = new(50, 100);

		RefreshSelectedCatInfo(selected);

		for (int i = 0; i < 6; i++)
		{
			if (_elements.ContainsKey("cat" + i))
			{
				_elements["cat" + i].Kill();
			}
			else
			{
				_elements.Add("cat" + i, null);
			}
			if (game.ChooseCats[i] == selected)
			{
				_elements["cat" + i] = new UICatButton(
					UIScale(new ClanGenRect(270, 200, 150, 150)),
					game.ChooseCats[i].Sprite,
					game.ChooseCats[i],
					game.Manager
				);
			}
			else if (game.ChooseCats[i] == _leader || game.ChooseCats[i] == _deputy || game.ChooseCats[i] == _medicineCat)
			{
				_elements["cat" + i] = new UICatButton(
					UIScale(new ClanGenRect(650, 130 + 50 * i, 50, 50)),
					game.ChooseCats[i].Sprite,
					game.ChooseCats[i],
					game.Manager
				);
				_elements["cat" + i].SetActive(false);
			}
			else
			{
				_elements["cat" + i] = new UICatButton(
					UIScale(new ClanGenRect(columnPos.X, 130 + 50 * i, 50, 50)),
					game.ChooseCats[i].Sprite,
					game.ChooseCats[i],
					game.Manager
				);
			}
		}

		for (int i = 6; i < 12; i++)
		{
			if (_elements.ContainsKey("cat" + i))
			{
				_elements["cat" + i].Kill();
			}
			else
			{
				_elements.Add("cat" + i, null);
			}
			if (game.ChooseCats[i] == selected)
			{
				_elements["cat" + i] = new UICatButton(
					UIScale(new ClanGenRect(270, 200, 150, 150)),
					game.ChooseCats[i].Sprite,
					game.ChooseCats[i],
					game.Manager
				);
			}
			else if (game.ChooseCats[i] == _leader || game.ChooseCats[i] == _deputy || game.ChooseCats[i] == _medicineCat)
			{
				_elements["cat" + i] = new UICatButton(
					UIScale(new ClanGenRect(700, 130 + 50 * (i - 6), 50, 50)),
					game.ChooseCats[i].Sprite,
					game.ChooseCats[i],
					game.Manager
				);
				_elements.Last().Value.SetActive(false);
			}
			else
			{
				_elements["cat" + i] = new UICatButton(
					UIScale(new ClanGenRect(columnPos.Y, 130 + 50 * (i - 6), 50, 50)),
					game.ChooseCats[i].Sprite,
					game.ChooseCats[i],
					game.Manager
				);
			}
		}
	}

	public override void ExitScreen()
	{
		_mainMenuButton?.Kill();
		_mainMenuButton = null;
		ClearPage();
		_menuWarning?.Kill();
		_menuWarning = null;
		_rerollsLeft = game.Config.ClanCreation.Rerolls;
		base.ExitScreen();
	}

	[GeneratedRegex(@"[^A-Za-z0-9 ]+")]
	private static partial Regex ClanNamePattern();
}
