using ClanGenDotNet.Scripts.Cats;
using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.UI.Theming;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using static ClanGenDotNet.Scripts.Resources;

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
	private Cat? _leader;
	private Cat? _deputy;
	private Cat? _medicineCat;
	private List<Cat> _members = [];

	private Cat? _selectedCat;

	private string? _symbolSelected = null;
	private int _tagListDen = 0;
	private string? _biomeSelected = null;
	private int _selectedCampTag = 1;
	private string? _selectedSeason = null;
	private string _campSelected = "1";

	private string _subScreen = "gamemode";
	private Dictionary<string, UIElement> _elements = [];
	private Dictionary<string, UIElement> _tabs = [];
	private Dictionary<string, UIImage> _symbolImages = [];
	private Dictionary<string, UIButton> _symbolButtons = [];

	private Dictionary<string, UITextBox> _text = [];

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

	private List<Texture2D> _memberBackgrounds = [
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\clan_none_light.png"),
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\clan_one_light.png"),
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\clan_two_light.png"),
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\clan_three_light.png"),
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\clan_four_light.png"),
		LoadTexture(".\\Resources\\Images\\PickClanScreen\\clan_full_light.png"),
	];

	public override void ScreenSwitches()
	{
		base.ScreenSwitches();

		_gameMode = "classic";
		_clanName = "";
		_selectedCampTag = 1;
		_biomeSelected = null;
		_selectedSeason = "Newleaf";
		_symbolSelected = null;
		_leader = null;
		_deputy = null;
		_medicineCat = null;
		_members.Clear();

		_menuWarning = new(
			UIScale(new ClanGenRect(25, 25, 600, -1)),
			"Note: going back to main menu resets the generated cats.",
			"text_box_22_horizleft"
		);
		_mainMenuButton = new(
			UIScale(new ClanGenRect(25, 50, 153, 30)),
			ButtonStyle.Squoval,
			GetArrow(3) + " Main Menu"
		);

		Cat.CreateExampleCats();
		OpenGameMode();
	}

	private IEnumerable<string> _clanNames = 
		Cats.Name.NamesDict.NormalPrefixes.Concat(Cats.Name.NamesDict.ClanPrefixes);
	private string RandomClanName()
	{
		return _clanNames.PickRandom();
	}

	private void OpenGameMode()
	{
		ClearPage();
		_subScreen = "gamemode";

		_elements.Add("gamemode_background", new UIImage(
			UIScale(new ClanGenRect(325, 130, 399, 461)),
			GameModeTextBox
		));
		_elements.Add("permi_warning", new UITextBox(
			UIScale(new ClanGenRect(100, 581, 600, 40)),
			"Your Clan's game mode is permanent and cannot be changed after Clan creation.",
			"text_box_30_horizcenter"
		));

		//buttons
		_elements.Add("classic_mode_button", new UIButton(
			UIScale(new ClanGenRect(109, 240, 132, 30)),
			ButtonStyle.Squoval,
			"classic"
		));
		_elements.Add("expanded_mode_button", new UIButton(
			UIScale(new ClanGenRect(94, 320, 162, 34)),
			ButtonStyle.Squoval,
			"expanded"
		));
		_elements.Add("cruel_mode_button", new UIButton(
			UIScale(new ClanGenRect(100, 400, 150, 30)),
			ButtonStyle.Squoval,
			"cruel"
		));

		_elements.Add("previous_step", new UIButton(
			UIScale(new ClanGenRect(253, 620, 147, 30)),
			ButtonStyle.MenuLeft,
			"previous"
		));
		_elements["previous_step"].SetActive(false);
		_elements.Add("next_step", new UIButton(
			UIScale(new ClanGenRect(400, 620, 147, 30)),
			ButtonStyle.MenuRight,
			"next"
		));

		_elements.Add("random_clan_checkbox", new UICheckbox(
			UIScale(new ClanGenRect(560, -32, 34, 34)).AnchorTo(AnchorPosition.TopTarget, _elements["previous_step"].RelativeRect),
			"Quick Start"
		));

		_elements.Add("mode_details", new UITextBox(
			UIScale(new ClanGenRect(325, 160, 405, 461)),
			"",
			"text_box_30_horizleft_pad_40_40"
		));
		if (_elements["mode_details"] is UITextBox textBox) { textBox.SetPadding(new(40, 40)); }
		_elements.Add("mode_name", new UITextBox(
			UIScale(new ClanGenRect(425, 135, 200, 27)),
			"classic",
			"text_box_30_horizcenter_light"
		));
	}

	private void OpenNameClan()
	{
		ClearPage();
		_subScreen = "name clan";

		_elements.Add("under_background", new UIImage(
			UIScale(new ClanGenRect(0, 0, game.ScreenX, game.ScreenY)),
			_creationBackgrounds[1]
		));

		_elements.Add("random", new UIButton(
			UIScale(new ClanGenRect(224, 595, 34, 34)),
			ButtonStyle.Icon,
			"\u2192",
			objectID: "buttonstyles_icon"
		));

		_elements.Add("error", new UITextBox(
			UIScale(new ClanGenRect(0, 570, 800, -1)),
			"",
			"text_box_30_horizcenter_red"
		));

		_elements.Add("previous_step", new UIButton(
			UIScale(new ClanGenRect(253, 635, 147, 30)),
			ButtonStyle.MenuLeft,
			"previous"
		));
		_elements.Add("next_step", new UIButton(
			UIScale(new ClanGenRect(400, 635, 147, 30)),
			ButtonStyle.MenuRight,
			"next"
		));
		_elements["next_step"].SetActive(false);

		_elements.Add("name_entry", new UITextInput(
			UIScale(new ClanGenRect(265, 597, 140, 29)),
			"",
			11
		));
		_elements.Add("clan", new UITextBox(
			UIScale(new ClanGenRect(375, 600, 100, 25)),
			"-Clan",
			"text_box_30_horizcenter_light"
		));
		_elements.Add("reset_name", new UIButton(
			UIScale(new ClanGenRect(455, 595, 134, 30)),
			ButtonStyle.Squoval,
			"Reset Name"
		));
	}

	private void ClanNameHeader()
	{
		_elements.Add("name_backdrop", new UIImage(
			UIScale(new ClanGenRect(292, 100, 216, 50)),
			_creationBackgrounds[0]
		));
		_elements.Add("clan_name", new UITextBox(
			UIScale(new ClanGenRect(292, 100, 216, 50)),
			_clanName + "Clan",
			new ObjectID("text_box_30_horizcenter_vertcenter", "#dark")
		));
	}

	private void OpenChooseLeader()
	{
		ClearPage();
		_subScreen = "choose leader";

		_elements.Add("background", new UIImage(
			UIScale(new ClanGenRect(0, 414, 800, 286)),
			_creationBackgrounds[2]
		));

		ClanNameHeader();

		int xPos = 155;
		int yPos = 235;

		_elements.Add("roll1", new UIButton(
			UIScale(new ClanGenRect(xPos, yPos, new(34))),
			ButtonStyle.Squoval,
			"\u2684",
			objectID: "buttonstyles_icon"
		));
		yPos += 40;
		_elements.Add("roll2", new UIButton(
			UIScale(new ClanGenRect(xPos, yPos, new(34))),
			ButtonStyle.Squoval,
			"\u2684",
			objectID: "buttonstyles_icon"
		));
		yPos += 40;
		_elements.Add("roll3", new UIButton(
			UIScale(new ClanGenRect(xPos, yPos, new(34))),
			ButtonStyle.Squoval,
			"\u2684",
			objectID: "buttonstyles_icon"
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
			objectID: "buttonstyles_icon"
		));
		_elements.Add("reroll_count", new UITextBox(
			UIScale(new ClanGenRect(100, 440, 50, 25)),
			_rerollsLeft.ToString(),
			"@text_box"
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
			ButtonID.NineLivesButton
		));
		_elements.Last().Value.Hide();

		_elements.Add("error_message", new UITextBox(
			UIScale(new ClanGenRect(150, 353, 500, 55)),
			"Too young to become leader",
			"text_box_30_horizcenter_red"
		));
		_elements.Last().Value.Visible = false;

		_elements.Add("previous_step", new UIButton(
			UIScale(new ClanGenRect(253, 400, 147, 30)),
			ButtonStyle.MenuLeft,
			"previous"
		));
		_elements.Add("next_step", new UIButton(
			UIScale(new ClanGenRect(0, 400, 147, 30)).AnchorTo(AnchorPosition.LeftTarget, _elements.Last().Value.RelativeRect),
			ButtonStyle.MenuRight,
			"next"
		));
		_elements["next_step"].SetActive(false);

		RefreshCatImagesAndInfo();
	}

	private void OpenChooseDeputy()
	{
		ClearPage();
		_subScreen = "choose deputy";

		_elements.Add("background", new UIImage(
			UIScale(new ClanGenRect(0, 414, 800, 286)),
			_creationBackgrounds[3]
		));

		ClanNameHeader();
		CreateCatInfo();

		_elements.Add("select_cat", new UIButton(
			UIScale(new ClanGenRect(209, 348, 384, 52)),
			ButtonID.SupportLeaderButton
		));
		_elements.Last().Value.Hide();

		_elements.Add("error_message", new UITextBox(
			UIScale(new ClanGenRect(150, 353, 500, 55)),
			"Too young to become deputy",
			"text_box_30_horizcenter_red"
		));
		_elements.Last().Value.Visible = false;

		_elements.Add("previous_step", new UIButton(
			UIScale(new ClanGenRect(253, 400, 147, 30)),
			ButtonStyle.MenuLeft,
			"previous"
		));
		_elements.Add("next_step", new UIButton(
			UIScale(new ClanGenRect(0, 400, 147, 30)).AnchorTo(AnchorPosition.LeftTarget, _elements.Last().Value.RelativeRect),
			ButtonStyle.MenuRight,
			"next"
		));
		_elements["next_step"].SetActive(false);

		RefreshCatImagesAndInfo();
	}

	private void OpenChooseMedCat()
	{
		ClearPage();
		_subScreen = "choose med cat";

		_elements.Add("background", new UIImage(
			UIScale(new ClanGenRect(0, 414, 800, 286)),
			_creationBackgrounds[4]
		));

		ClanNameHeader();
		CreateCatInfo();

		_elements.Add("select_cat", new UIButton(
			UIScale(new ClanGenRect(260, 342, 306, 58)),
			ButtonID.AidClanButton
		));
		_elements.Last().Value.Hide();

		_elements.Add("error_message", new UITextBox(
			UIScale(new ClanGenRect(150, 353, 500, 55)),
			"Too young to become a medicine cat",
			"text_box_30_horizcenter_red"
		));
		_elements.Last().Value.Visible = false;

		_elements.Add("previous_step", new UIButton(
			UIScale(new ClanGenRect(253, 400, 147, 30)),
			ButtonStyle.MenuLeft,
			"previous"
		));
		_elements.Add("next_step", new UIButton(
			UIScale(new ClanGenRect(0, 400, 147, 30)).AnchorTo(AnchorPosition.LeftTarget, _elements.Last().Value.RelativeRect),
			ButtonStyle.MenuRight,
			"next"
		));
		_elements["next_step"].SetActive(false);

		RefreshCatImagesAndInfo();
	}

	private void OpenChooseMembers()
	{
		ClearPage();
		_subScreen = "choose members";

		_elements.Add("background", new UIImage(
			UIScale(new ClanGenRect(0, 414, 800, 286)),
			_memberBackgrounds[0]
		));

		ClanNameHeader();
		CreateCatInfo();

		_elements.Add("select_cat", new UIButton(
			UIScale(new ClanGenRect(353, 360, 95, 30)),
			ButtonStyle.Squoval,
			"Recruit"
		));
		_elements.Last().Value.Hide();

		_elements.Add("previous_step", new UIButton(
			UIScale(new ClanGenRect(253, 400, 147, 30)),
			ButtonStyle.MenuLeft,
			"previous"
		));
		_elements.Add("next_step", new UIButton(
			UIScale(new ClanGenRect(0, 400, 147, 30)).AnchorTo(AnchorPosition.LeftTarget, _elements.Last().Value.RelativeRect),
			ButtonStyle.MenuRight,
			"next"
		));
		_elements["next_step"].SetActive(false);

		RefreshCatImagesAndInfo();

		RefreshCatImagesAndInfo();
		RefreshTextAndButtons();
	}

	public void CreateCatInfo()
	{
		_elements.Add("cat_name", new UITextBox(
			UIScale(new ClanGenRect(0, 10, 250, 60)).AnchorTo(
				AnchorPosition.TopTarget,
				_elements["name_backdrop"].RelativeRect
			).AnchorTo(AnchorPosition.CenterX),
			"",
			"text_box_30_horizcenter"
		));

		_elements.Add("cat_info", new UITextBox(
			UIScale(new ClanGenRect(440, 220, 175, 125)),
			"",
			"text_box_26_horizcenter"
		));
	}

	private void OpenChooseBackground()
	{
		ClearPage();
		_subScreen = "choose camp";

		_elements.Add("previous_step", new UIButton(
			UIScale(new ClanGenRect(253, 645, 147, 30)),
			ButtonStyle.MenuLeft,
			"previous"
		));
		_elements.Add("next_step", new UIButton(
			UIScale(new ClanGenRect(0, 645, 147, 30)).AnchorTo(AnchorPosition.LeftTarget, _elements.Last().Value.RelativeRect),
			ButtonStyle.MenuRight,
			"next"
		));
		_elements["next_step"].SetActive(false);

		_elements.Add("forest_biome", new UIButton(
			UIScale(new ClanGenRect(196, 100, 100, 46)),
			ButtonID.ForestBiomeButton
		));
		_elements.Add("mountain_biome", new UIButton(
			UIScale(new ClanGenRect(304, 100, 106, 46)),
			ButtonID.MountainBiomeButton
		));
		_elements.Add("plains_biome", new UIButton(
			UIScale(new ClanGenRect(424, 100, 88, 46)),
			ButtonID.PlainsBiomeButton
		));
		_elements.Add("beach_biome", new UIButton(
			UIScale(new ClanGenRect(520, 100, 82, 46)),
			ButtonID.BeachBiomeButton
		));

		_tabs.Add("tab1", new UIElement(new ClanGenRect()));
		_tabs.Add("tab2", new UIElement(new ClanGenRect()));
		_tabs.Add("tab3", new UIElement(new ClanGenRect()));
		_tabs.Add("tab4", new UIElement(new ClanGenRect()));

		_elements.Add("newleaf_tab", new UIButton(
			UIScale(new ClanGenRect(625, 275, 39, 34)),
			ButtonStyle.IconTabLeft,
			Icon.Newleaf.GetAsUTF8(),
			objectID: "buttonstyles_icon"
		));

		_elements.Add("greenleaf_tab", new UIButton(
			UIScale(new ClanGenRect(625, 25, 39, 34))
				.AnchorTo(AnchorPosition.TopTarget, _elements.Last().Value.RelativeRect),
			ButtonStyle.IconTabLeft,
			Icon.Greenleaf.GetAsUTF8(),
			objectID: "buttonstyles_icon"
		));

		_elements.Add("leaffall_tab", new UIButton(
			UIScale(new ClanGenRect(625, 25, 39, 34))
				.AnchorTo(AnchorPosition.TopTarget, _elements.Last().Value.RelativeRect),
			ButtonStyle.IconTabLeft,
			Icon.Leafbare.GetAsUTF8(),
			objectID: "buttonstyles_icon"
		));
		_elements.Add("leafbare_tab", new UIButton(
			UIScale(new ClanGenRect(625, 25, 39, 34))
				.AnchorTo(AnchorPosition.TopTarget, _elements.Last().Value.RelativeRect),
			ButtonStyle.IconTabLeft,
			Icon.Leafbare.GetAsUTF8(),
			objectID: "buttonstyles_icon"
		));

		_elements.Add("random_background", new UIButton(
			UIScale(new ClanGenRect(255, 595, 290, 30)),
			ButtonStyle.Squoval,
			"choose a random background"
		));

		DrawArtFrame();
	}

	private void DrawArtFrame()
	{
		if (_elements.TryGetValue("art_frame", out UIElement? frame))
		{
			frame.Kill();
		}
		_elements.Remove("art_frame");
		_elements.Add("art_frame", new UIImage(
			UIScale(new ClanGenRect(0, 20, 466, 416)).AnchorTo(AnchorPosition.Center),
			Frame,
			true
		));
	}

	private void OpenChooseSymbol()
	{
		ClearPage();
		_subScreen = "choose symbol";

		_elements.Add("previous_step", new UIButton(
			UIScale(new ClanGenRect(253, 645, 147, 30)),
			ButtonStyle.MenuLeft,
			"previous"
		));
		_elements.Add("done_button", new UIButton(
			UIScale(new ClanGenRect(0, 645, 147, 30)).AnchorTo(AnchorPosition.LeftTarget, _elements.Last().Value.RelativeRect),
			ButtonStyle.MenuRight,
			"Done"
		));
		_elements["done_button"].SetActive(false);

		_elements.Add("text_container", new UIAutoResizableContainer(
			UIScale(new ClanGenRect(85, 105, 0, 0))
		));

		_text.Add("clan_name", new UITextBox(
			UIScale(new ClanGenRect(0, 0, -1, -1)),
			$"{_clanName}Clan",
			GetTextBoxTheme("text_box_40")
		));
		((UIAutoResizableContainer)_elements.Last().Value).AddElement(_text.Last().Value, true);
		_text.Add("biome", new UITextBox(
			UIScale(new ClanGenRect(0, 5, -1, -1)).AnchorTo(AnchorPosition.TopTarget, _text.Last().Value.RelativeRect),
			$"{_biomeSelected}",
			GetTextBoxTheme("text_box_30_horizleft")
		));
		((UIAutoResizableContainer)_elements.Last().Value).AddElement(_text.Last().Value);
		_text.Add("leader", new UITextBox(
			UIScale(new ClanGenRect(0, 5, -1, -1)).AnchorTo(AnchorPosition.TopTarget, _text.Last().Value.RelativeRect),
			$"Leader name: {_leader!.Name.Prefix}star",
			GetTextBoxTheme("text_box_30_horizleft")
		));
		((UIAutoResizableContainer)_elements.Last().Value).AddElement(_text.Last().Value);
		_text.Add("recommend", new UITextBox(
			UIScale(new ClanGenRect(0, 5, -1, -1)).AnchorTo(AnchorPosition.TopTarget, _text.Last().Value.RelativeRect),
			$"Recommended Symbol: None",
			GetTextBoxTheme("text_box_30_horizleft")
		));
		((UIAutoResizableContainer)_elements.Last().Value).AddElement(_text.Last().Value);
		_text.Add("selected", new UITextBox(
			UIScale(new ClanGenRect(0, 15, -1, -1)).AnchorTo(AnchorPosition.TopTarget, _text.Last().Value.RelativeRect),
			$"Selected Symbol: None",
			GetTextBoxTheme("text_box_30_horizleft")
		));
		((UIAutoResizableContainer)_elements.Last().Value).AddElement(_text.Last().Value);

		_elements.Add("random_symbol_button", new UIButton(
			UIScale(new ClanGenRect(496, 206, 34, 34)),
			ButtonStyle.Icon,
			Icon.Dice.GetAsUTF8(),
			objectID: "buttonstyles_icon"
		));

		_elements.Add("symbol_frame", new UIImage(
			UIScale(new ClanGenRect(540, 90, 169, 166)),
			Frame,
			true
		));

		_elements.Add("page_left", new UIButton(
			UIScale(new ClanGenRect(47, 414, 34, 34)),
			ButtonStyle.Icon,
			Icon.ArrowLeft.GetAsUTF8(),
			objectID: "buttonstyles_icon"
		));
		_elements.Add("page_right", new UIButton(
			UIScale(new ClanGenRect(719, 414, 34, 34)),
			ButtonStyle.Icon,
			Icon.ArrowRight.GetAsUTF8(),
			objectID: "buttonstyles_icon"
		));
		_elements.Add("filters_tab", new UIButton(
			UIScale(new ClanGenRect(100, 619, 78, 30)),
			ButtonID.FiltersTab
		));

		_elements.Add("symbol_list_frame", new UIImage(
			UIScale(new ClanGenRect(76, 250, 650, 370)),
			RoundedFrame,
			true,
			RoundedFrameNPatch
		));

		if (Sprites.ClanSymbols.Contains($"symbol{_clanName.ToUpper()}0"))
		{
			_text["recommend"].SetText(
				$"Recommended Symbol: {_clanName.ToUpper()}0"
			);
		}

		if (_symbolSelected != null)
		{
			string symbolName = _symbolSelected.Replace("symbol", "");
			_text["selected"].SetText(
				$"Selected Symbol: {symbolName}"
			);

			if (_elements.TryGetValue("selected_symbol", out UIElement? img))
			{
				img.Kill();
				_elements.Remove("selected_symbol");
			}
			_elements.Add("selected_symbol", new UIImage(
				UIScale(new ClanGenRect(573, 127, 100, 100)),
				Sprites.SymbolSprites[_symbolSelected]
			));
			RefreshSymbolList();
			while (!_symbolButtons.ContainsKey(_symbolSelected))
			{
				_currentPage++;
				RefreshSymbolList();
			}
			_elements["done_button"].Enable();
		}
		else
		{
			if (_elements.TryGetValue("selected_symbol", out UIElement? img))
			{
				img.Kill();
				_elements.Remove("selected_symbol");
			}
			_elements.Add("selected_symbol", new UIImage(
				UIScale(new ClanGenRect(573, 127, 100, 100)),
				Sprites.SymbolSprites["symbolADDER0"]
			));
			RefreshSymbolList();
		}
	}

	public void OpenClanSavedScreen()
	{
		ClearPage();
		_subScreen = "saved screen";

		_elements.Add("selected_image", new UIImage(
			UIScale(new ClanGenRect(350, 105, 100, 100)),
			Sprites.SymbolSprites[_symbolSelected!+"#dark"]
		));

		_elements.Add("leader_image", new UIImage(
			UIScale(new ClanGenRect(350, 125, 100, 100)),
			game.Clan!.Leader!.Sprite
		));

		_elements.Add("continue", new UIButton(
			UIScale(new ClanGenRect(346, 250, 102, 30)),
			ButtonStyle.Squoval,
			"continue"
		));

		_elements.Add("save_confirm", new UITextBox(
			UIScale(new ClanGenRect(100, 70, 600, 30)),
			"Your Clan has been created and saved!",
			GetTextBoxTheme("text_box_30_horizcenter")
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
			case "choose deputy":
				HandleChooseDeputyEvent(evnt);
				break;
			case "choose med cat":
				HandleChooseMedCatEvent(evnt);
				break;
			case "choose members":
				HandleChooseMembersEvent(evnt);
				break;
			case "choose camp":
				HandleChooseBackgroundEvent(evnt);
				break;
			case "choose symbol":
				HandleChooseSymbolEvent(evnt);
				break;
			case "saved screen":
				HandleSavedClanEvent(evnt);
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
				_gameMode = "cruel season";
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["next_step"])
			{
				game.Settings["game_mode"] = _gameMode;
				OpenNameClan();
			}
		}
		else if (evnt.EventType == EventType.KeyPressed)
		{
			switch (evnt.KeyCode)
			{
				case KEY_ESCAPE:
					ChangeScreen("start screen");
					break;
				case KEY_DOWN:
					if (_gameMode == "classic")
					{
						_gameMode = "expanded";
					}
					else if (_gameMode == "expanded")
					{
						_gameMode = "cruel season";
					}
					RefreshTextAndButtons();
					break;
				case KEY_UP:
					if (_gameMode == "cruel season")
					{
						_gameMode = "expanded";
					}
					else if (_gameMode == "expanded")
					{
						_gameMode = "classic";
					}
					RefreshTextAndButtons();
					break;
				case KEY_ENTER:
				case KEY_RIGHT:
					if (_elements["next_step"].Active)
					{
						game.Settings["gamemode"] = _gameMode;
					}
					OpenNameClan();
					break;
			}
		}
	}

	private void HandleNameClanEvent(Event evnt)
	{
		var nameEntry = (UITextInput)_elements["name_entry"];
		var errorText = (UITextBox)_elements["error"];
		if (nameEntry.GetText() == "")
		{
			_elements["next_step"].Disable();
		}
		else if (nameEntry.GetText()[0] == ' ')
		{
			errorText.SetText("Clan names cannot start with a space.");
			errorText.Show();
			_elements["next_step"].Disable();
		}
		else
		{
			errorText.Hide();
			_elements["next_step"].Enable();
		}
		if (evnt.EventType == EventType.LeftMouseClick)
		{
			if (evnt.Element == _elements["reset_name"])
			{
				nameEntry.SetText("");
			}

			if (evnt.Element == _elements["random"])
			{
				nameEntry.SetText(RandomClanName());
			}

			if (evnt.Element == _elements["next_step"])
			{
				string newName = ClanNamePattern().Replace(nameEntry.GetText(), "").Trim();
				if (newName == null || newName == "")
				{
					errorText.SetText("Your Clan's name cannot be empty");
					return;
				}
				//check if in clanlist here
				_clanName = newName!;
				OpenChooseLeader();
			}
			else if (evnt.Element == _elements["previous_step"])
			{
				_clanName = "";
				_gameMode = "classic";
				OpenGameMode();
			}
		}
		else if (evnt.EventType == EventType.KeyPressed)
		{
			switch (evnt.KeyCode)
			{
				case KEY_ESCAPE:
					ChangeScreen("start screen");
					break;
				case KEY_LEFT:
					if (!nameEntry.Focused)
					{
						_clanName = "";
						OpenGameMode();
					}
					break;
				case KEY_ENTER:
				case KEY_RIGHT:
					if (!nameEntry.Focused)
					{
						string newName = ClanNamePattern().Replace(nameEntry.GetText(), "").Trim();
						if (newName == null || newName == "")
						{
							errorText.SetText("Your Clan's name cannot be empty");
							return;
						}
						//check if in clanlist here
						_clanName = newName!;
						OpenChooseLeader();
					}
					break;
			}
		}
	}

	private void HandleChooseLeaderEvent(Event evnt)
	{
		if (evnt.EventType == EventType.LeftMouseClick)
		{
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
			for (int i = 0; i < 12; i++)
			{
				if (evnt.Element == _elements["cat" + i])
				{
					if (IsKeyDown(KEY_LEFT_SHIFT))
					{
						var clickedCat = ((UICatButton)evnt.Element!).GetCat();
						if (clickedCat.Age != (Age.Newborn | Age.Kitten | Age.Adolescent))
						{
							_leader = clickedCat;
							_selectedCat = null;
							OpenChooseDeputy();
							return;
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
			if (evnt.Element == _elements["select_cat"])
			{
				_leader = _selectedCat!;
				_selectedCat = null;
				OpenChooseDeputy();
			}
			else if (evnt.Element == _elements["previous_step"])
			{
				_clanName = "";
				OpenNameClan();
			}
		}
	}

	private void HandleChooseDeputyEvent(Event evnt)
	{
		if (evnt.EventType == EventType.LeftMouseClick)
		{
			for (int i = 0; i < 12; i++)
			{
				if (evnt.Element == _elements["cat" + i])
				{
					if (IsKeyDown(KEY_LEFT_SHIFT))
					{
						var clickedCat = ((UICatButton)evnt.Element!).GetCat();
						if (clickedCat.Age != (Age.Newborn | Age.Kitten | Age.Adolescent))
						{
							_deputy = clickedCat;
							_selectedCat = null;
							OpenChooseMedCat();
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
			if (evnt.Element == _elements["select_cat"])
			{
				_deputy = _selectedCat!;
				_selectedCat = null;
				OpenChooseMedCat();
			}
			else if (evnt.Element == _elements["previous_step"])
			{
				_leader = null;
				_selectedCat = null;
				OpenChooseLeader();
			}
		}
	}

	private void HandleChooseMedCatEvent(Event evnt)
	{
		if (evnt.EventType == EventType.LeftMouseClick)
		{
			for (int i = 0; i < 12; i++)
			{
				if (evnt.Element == _elements["cat" + i])
				{
					if (IsKeyDown(KEY_LEFT_SHIFT))
					{
						var clickedCat = ((UICatButton)evnt.Element!).GetCat();
						if (clickedCat.Age != (Age.Newborn | Age.Kitten | Age.Adolescent))
						{
							_medicineCat = clickedCat;
							_selectedCat = null;
							OpenChooseMembers();
						}
					}
					else if (((UICatButton)evnt.Element!).GetCat() != _leader)
					{
						_selectedCat = ((UICatButton)evnt.Element!).GetCat();
						RefreshCatImagesAndInfo(_selectedCat);
						RefreshTextAndButtons();
						return; // prevent handler from comparing to other buttons.
					}
				}
			}
			if (evnt.Element == _elements["select_cat"])
			{
				_medicineCat = _selectedCat!;
				_selectedCat = null;
				OpenChooseMembers();
			}
			else if (evnt.Element == _elements["previous_step"])
			{
				_deputy = null;
				_selectedCat = null;
				OpenChooseDeputy();
			}
		}
	}

	private void HandleChooseMembersEvent(Event evnt)
	{
		if (evnt.EventType == EventType.LeftMouseClick)
		{
			for (int i = 0; i < 12; i++)
			{
				if (evnt.Element == _elements["cat" + i])
				{
					if (IsKeyDown(KEY_LEFT_SHIFT) && _members.Count < 7)
					{
						var clickedCat = ((UICatButton)evnt.Element!).GetCat();
						_members.Add(clickedCat!);
						_selectedCat = null;
						RefreshCatImagesAndInfo(null);
						RefreshTextAndButtons();
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
			if (evnt.Element == _elements["select_cat"])
			{
				_members.Add(_selectedCat!);
				_selectedCat = null;
				RefreshCatImagesAndInfo(null);
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["previous_step"])
			{
				if (_members.Count <= 0)
				{
					_medicineCat = null;
					_selectedCat = null;
					OpenChooseMedCat();
				}
				else
				{
					_members.Remove(_members.Last());
					_selectedCat = null;
					RefreshSelectedCatInfo(null);
					RefreshTextAndButtons();
					return;
				}
			}
			else if (evnt.Element == _elements["next_step"])
			{
				_selectedCat = null;
				OpenChooseBackground();
			}
		}
	}

	private void HandleChooseBackgroundEvent(Event evnt)
	{
		if (evnt.EventType == EventType.LeftMouseClick)
		{
			if (evnt.Element == _elements["previous_step"])
			{
				OpenChooseMembers();
			}
			else if (evnt.Element == _elements["forest_biome"])
			{
				_biomeSelected = "Forest";
				_selectedCampTag = 1;
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["mountain_biome"])
			{
				_biomeSelected = "Mountainous";
				_selectedCampTag = 1;
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["plains_biome"])
			{
				_biomeSelected = "Plains";
				_selectedCampTag = 1;
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["beach_biome"])
			{
				_biomeSelected = "Beach";
				_selectedCampTag = 1;
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _tabs["tab1"])
			{
				_selectedCampTag = 1;
				RefreshSelectedCamp();
			}
			else if (evnt.Element == _tabs["tab2"])
			{
				_selectedCampTag = 2;
				RefreshSelectedCamp();
			}
			else if (evnt.Element == _tabs["tab3"])
			{
				_selectedCampTag = 3;
				RefreshSelectedCamp();
			}
			else if (_tabs.TryGetValue("tab4", out UIElement? tab4) && evnt.Element == tab4)
			{
				_selectedCampTag = 4;
				RefreshSelectedCamp();
			}
			else if (evnt.Element == _elements["newleaf_tab"])
			{
				_selectedSeason = "Newleaf";
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["greenleaf_tab"])
			{
				_selectedSeason = "Greenleaf";
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["leaffall_tab"])
			{
				_selectedSeason = "Leaf-fall";
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["leafbare_tab"])
			{
				_selectedSeason = "Leaf-bare";
				RefreshTextAndButtons();
			}
			else if (evnt.Element == _elements["random_background"])
			{
				Console.WriteLine("sorry, not implemented yet!");
			}
			else if (evnt.Element == _elements["next_step"])
			{
				OpenChooseSymbol();
			}
		}
		else if (evnt.EventType == EventType.KeyPressed)
		{
			switch (evnt.KeyCode)
			{
				case KEY_RIGHT:
					_biomeSelected =
						_biomeSelected == null
						? "Forest"
						: _biomeSelected == "Forest"
						? "Mountainous"
						: _biomeSelected == "Mountainous"
						? "Plains"
						: _biomeSelected == "Plains"
						? "Beach"
						: null;
					_selectedCampTag = 1;
					RefreshTextAndButtons();
					break;
				case KEY_LEFT:
					_biomeSelected =
						_biomeSelected == null
						? "Beach"
						: _biomeSelected == "Beach"
						? "Plains"
						: _biomeSelected == "Plains"
						? "Mountainous"
						: _biomeSelected == "Mountainous"
						? "Forest"
						: null;
					_selectedCampTag = 1;
					RefreshTextAndButtons();
					break;
				case KEY_UP:
					if (_selectedCampTag > 1 && _biomeSelected != null)
					{
						_selectedCampTag--;
					}
					RefreshSelectedCamp();
					break;
				case KEY_DOWN:
					if (_selectedCampTag < 4 && _biomeSelected != null)
					{
						_selectedCampTag++;
					}
					RefreshSelectedCamp();
					break;
				case KEY_ENTER:
					OpenChooseSymbol();
					break;
			}
		}
	}

	private void HandleChooseSymbolEvent(Event evnt)
	{
		if (evnt.EventType == EventType.LeftMouseClick)
		{
			if (evnt.Element == _elements["previous_step"])
			{
				OpenChooseBackground();
			}
			else if (evnt.Element == _elements["page_right"])
			{
				_currentPage++;
				RefreshSymbolList();
			}
			else if (evnt.Element == _elements["page_left"])
			{
				_currentPage--;
				RefreshSymbolList();
			}
			else if (evnt.Element == _elements["done_button"])
			{
				SaveClan();
				OpenClanSavedScreen();
			}
			else if (evnt.Element == _elements["random_symbol_button"])
			{
				if (_symbolSelected != null)
				{
					if (_symbolButtons.TryGetValue(_symbolSelected, out UIButton? btn))
					{
						btn.Disable();
					}
				}
			}
			else if (evnt.Element == _elements["filters_tab"])
			{
				Console.WriteLine("Can't filter yet!");
			}
			else
			{
				foreach (var button in _symbolButtons)
				{
					if (evnt.Element == button.Value)
					{
						if (_symbolSelected != null)
						{
							if (_symbolButtons.TryGetValue(_symbolSelected, out UIButton? btn))
							{
								btn.Enable();
							}
						}
						_symbolSelected = button.Key;
						RefreshTextAndButtons();
					}
				}
			}
		}
	}

	private void HandleSavedClanEvent(Event evnt)
	{
		if (evnt.EventType == EventType.LeftMouseClick && evnt.Element == _elements["continue"])
		{
			ChangeScreen("camp screen");
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
					case "cruel season":
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

			_elements["next_step"].SetActive(_gameMode != "cruel season");
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
		else if (_subScreen == "choose members")
		{
			if (_members.Count >= 4 && _members.Count <= 6)
			{
				((UIImage)_elements["background"]).Image = _memberBackgrounds[4];
				_elements["next_step"].SetActive(true);
			}
			else if (_members.Count == 7)
			{
				((UIImage)_elements["background"]).Image = _memberBackgrounds[5];
				_elements["select_cat"].SetActive(false);
				_elements["next_step"].SetActive(true);
			}
			else
			{
				((UIImage)_elements["background"]).Image = _memberBackgrounds[_members.Count];
			}

			_elements["select_cat"].Visible = (_selectedCat != null);
		}
		else if (_subScreen == "choose camp")
		{
			_elements["forest_biome"].Enable();
			_elements["mountain_biome"].Enable();
			_elements["plains_biome"].Enable();
			_elements["beach_biome"].Enable();
			switch (_biomeSelected)
			{
				case "Forest":
					_elements["forest_biome"].Disable();
					break;
				case "Mountainous":
					_elements["mountain_biome"].Disable();
					break;
				case "Plains":
					_elements["plains_biome"].Disable();
					break;
				case "Beach":
					_elements["beach_biome"].Disable();
					break;
			}

			_elements["newleaf_tab"].Enable();
			_elements["greenleaf_tab"].Enable();
			_elements["leaffall_tab"].Enable();
			_elements["leafbare_tab"].Enable();

			switch (_selectedSeason)
			{
				case "Newleaf":
					_elements["newleaf_tab"].Disable();
					break;
				case "Greenleaf":
					_elements["greenleaf_tab"].Disable();
					break;
				case "Leaf-fall":
					_elements["leaffall_tab"].Disable();
					break;
				case "Leaf-bare":
					_elements["leafbare_tab"].Disable();
					break;
			}

			if (_biomeSelected != null && _selectedCampTag > -1)
			{
				_elements["next_step"].Enable();
			}

			RefreshSelectedCamp();
		}
		else if (_subScreen == "choose symbol")
		{
			if (_symbolSelected != null)
			{
				if (_symbolButtons.TryGetValue(_symbolSelected, out UIButton? btn))
				{
					btn.Disable();
				}

				((UIImage)_elements["selected_symbol"]).Image = Sprites.SymbolSprites[_symbolSelected];

				var symbolName = _symbolSelected.Replace("symbol", "");
				_text["selected"].SetText($"Selected Symbol: {symbolName}");
				_elements["selected_symbol"].Show();
				_elements["done_button"].Enable();
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
		foreach (UITextBox element in _text.Values)
		{
			element.Kill();
		}
		foreach (UIImage element in _symbolImages.Values)
		{
			element.Kill();
		}
		_elements.Clear();
		_tabs.Clear();
		_symbolButtons.Clear();
		_text.Clear();
		_symbolImages.Clear();
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
					game.ChooseCats[i]
				);
			}
			else if (
				game.ChooseCats[i] == _leader
				|| game.ChooseCats[i] == _deputy
				|| game.ChooseCats[i] == _medicineCat
				|| _members.Contains(game.ChooseCats[i])
			)
			{
				_elements["cat" + i] = new UICatButton(
					UIScale(new ClanGenRect(650, 130 + 50 * i, 50, 50)),
					game.ChooseCats[i].Sprite,
					game.ChooseCats[i]
				);
				_elements["cat" + i].SetActive(false);
			}
			else
			{
				_elements["cat" + i] = new UICatButton(
					UIScale(new ClanGenRect(columnPos.X, 130 + 50 * i, 50, 50)),
					game.ChooseCats[i].Sprite,
					game.ChooseCats[i]
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
					game.ChooseCats[i]
				);
			}
			else if (
				game.ChooseCats[i] == _leader 
				|| game.ChooseCats[i] == _deputy 
				|| game.ChooseCats[i] == _medicineCat 
				|| _members.Contains(game.ChooseCats[i])
			)
			{
				_elements["cat" + i] = new UICatButton(
					UIScale(new ClanGenRect(700, 130 + 50 * (i - 6), 50, 50)),
					game.ChooseCats[i].Sprite,
					game.ChooseCats[i]
				);
				_elements.Last().Value.SetActive(false);
			}
			else
			{
				_elements["cat" + i] = new UICatButton(
					UIScale(new ClanGenRect(columnPos.Y, 130 + 50 * (i - 6), 50, 50)),
					game.ChooseCats[i].Sprite,
					game.ChooseCats[i]
				);
			}
		}
	}

	public void RefreshSelectedCamp()
	{
		foreach (var tab in _tabs)
		{
			tab.Value.Kill();
		}
		_tabs.Clear();

		switch (_biomeSelected)
		{
			case "Forest":
				_tabs.Add("tab1", new UIButton(
					UIScale(new ClanGenRect(95, 180, 154, 30)),
					ButtonID.ClassicTab
				));
				_tabs.Add("tab2", new UIButton(
					UIScale(new ClanGenRect(108, 215, 154, 30)),
					ButtonID.GullyTab
				));
				_tabs.Add("tab3", new UIButton(
					UIScale(new ClanGenRect(95, 250, 154, 30)),
					ButtonID.GrottoTab
				));
				_tabs.Add("tab4", new UIButton(
					UIScale(new ClanGenRect(85, 285, 154, 30)),
					ButtonID.LakesideTab
				));
				break;
			case "Mountainous":
				_tabs.Add("tab1", new UIButton(
					UIScale(new ClanGenRect(111, 180, 154, 30)),
					ButtonID.CliffTab
				));
				_tabs.Add("tab2", new UIButton(
					UIScale(new ClanGenRect(90, 215, 154, 30)),
					ButtonID.CaveTab
				));
				_tabs.Add("tab3", new UIButton(
					UIScale(new ClanGenRect(42, 250, 179, 30)),
					ButtonID.CrystalTab
				));
				_tabs.Add("tab4", new UIButton(
					UIScale(new ClanGenRect(107, 285, 154, 30)),
					ButtonID.RuinsTab
				));
				break;
			case "Plains":
				_tabs.Add("tab1", new UIButton(
					UIScale(new ClanGenRect(64, 180, 154, 30)),
					ButtonID.GrasslandsTab
				));
				_tabs.Add("tab2", new UIButton(
					UIScale(new ClanGenRect(89, 215, 154, 30)),
					ButtonID.TunnelTab
				));
				_tabs.Add("tab3", new UIButton(
					UIScale(new ClanGenRect(64, 250, 154, 30)),
					ButtonID.WastelandsTab
				));
				break;
			case "Beach":
				_tabs.Add("tab1", new UIButton(
					UIScale(new ClanGenRect(76, 180, 154, 30)),
					ButtonID.TidepoolTab
				));
				_tabs.Add("tab2", new UIButton(
					UIScale(new ClanGenRect(65, 215, 154, 30)),
					ButtonID.TidalCaveTab
				));
				_tabs.Add("tab3", new UIButton(
					UIScale(new ClanGenRect(70, 250, 154, 30)),
					ButtonID.ShipwreckTab
				));
				break;
		}

		for (int i = 0; i < _tabs.Count; i++)
		{
			if (i == _selectedCampTag)
			{
				_tabs[$"tab{_selectedCampTag}"].Disable();
			}
			_tabs[$"tab{_selectedCampTag}"].Enable();
		}

		if (_elements.TryGetValue("camp_art", out UIElement? campArt))
		{
			UnloadTexture(((UIImage)campArt).Image);
			campArt.Kill();
		}
		_elements.Remove("camp_art");

		if (_biomeSelected != null)
		{
			Texture2D campBackground = LoadTexture(GetCampArtPath(_selectedCampTag));

			_elements.Add("camp_art", new UIImage(
				UIScale(new ClanGenRect(175, 170, 450, 400))
					.AnchorTo(AnchorPosition.Center),
				campBackground
			));
		}

		DrawArtFrame();
	}

	private string GetCampArtPath(int selectedCampTag)
	{
		var leaf = _selectedSeason!.Replace("-", "");

		string campBgBaseDir = ".\\Resources\\Images\\CampBackgrounds";
		string startLeaf = leaf.ToLower();
		string lightDark = (bool)game.Settings["dark mode"]! ? "dark" : "light";

		string biome = _biomeSelected!.ToLower();

		return $"{campBgBaseDir}\\{biome}\\{startLeaf}_camp{selectedCampTag}_{lightDark}.png";
	}


	private Texture2D _emptyTex = new();
	private void RefreshSymbolList()
	{
		List<string> symbolList = [.. Sprites.ClanSymbols];
		var symbolAttributes = Sprites.SymbolDict;

		foreach (var symbol in Sprites.ClanSymbols)
		{
			int index = int.Parse(symbol[^1].ToString());
			var name = SymbolNamePattern().Replace(symbol, "");
			List<string> tags = ((JArray)symbolAttributes[name.ToLower().Captialize()][$"tags{index}"]).ToObject<List<string>>()!;
			foreach (var tag in tags)
			{
				if (((List<string>)game.Switches["disallowed_symbol_tags"]!).Contains(tag))
				{
					symbolList.Remove(symbol);
				}
			}

			var symbolChunks = Chunks(symbolList, 45);

			_currentPage = Math.Max(1, Math.Min(_currentPage, symbolChunks.Count));

			if (symbolChunks.Count <= 1)
			{
				_elements["page_left"].Disable();
				_elements["page_right"].Disable();
			}
			else if(_currentPage >= symbolChunks.Count)
			{
				_elements["page_left"].Enable();
				_elements["page_right"].Disable();
			}
			else if (_currentPage == 1 && symbolChunks.Count > 1)
			{
				_elements["page_left"].Disable();
				_elements["page_right"].Enable();
			}
			else
			{
				_elements["page_left"].Enable();
				_elements["page_right"].Enable();
			}

			List<string> displaysymbols = symbolChunks[_currentPage - 1];

			foreach (var symbolButton in _symbolButtons) { symbolButton.Value.Kill(); }
			_symbolButtons.Clear();

			foreach (var symbolImg in _symbolImages) { symbolImg.Value.Kill(); }
			_symbolImages.Clear();

			int xPos = 96;
			int yPos = 270;
			
			foreach (var sym in displaysymbols)
			{
				_symbolImages.Add($"{sym}", new UIImage(
					UIScale(new ClanGenRect(xPos, yPos, 50, 50)),
					Sprites.SymbolSprites[$"{sym}"]
				));
				_symbolButtons.Add($"{sym}", new UIButton(
					UIScale(new ClanGenRect(xPos - 12, yPos - 12, 74, 74)),
					ButtonID.SymbolsSelect
				));
				xPos += 70;
				if (xPos >= 715)
				{
					xPos = 96;
					yPos += 70;
				}
			}

			if (_symbolSelected != null && _symbolButtons.TryGetValue(_symbolSelected!, out UIButton? value))
			{
				value.Disable();
			}
		}
	}

	private List<List<string>> Chunks(List<string> list, int number)
	{
		var finalList = new List<List<string>>();
		for (int i = 0; i < list.Count; i+=number)
		{
			finalList.Add(list.GetRange(i, Math.Min(number, list.Count - i)));
		}
		return finalList;
	}

	public void SaveClan()
	{
		//game.Mediated.Clear();
		//game.Patrolled.Clear();
		//game.CatToFade.Clear();
		Cat.OutsideCats.Clear();
		//Patrol.UsedPatrols.Clear();
		Dictionary<int, string> convertCamp = new Dictionary<int, string>() {
			{ 1, "camp1" },
			{ 2, "camp2" },
			{ 3, "camp3" },
			{ 4, "camp4" }
		};
		game.Clan = new Clan(
			_clanName,
			_leader,
			_deputy,
			_medicineCat,
			biome: Clan.StringToBiome(_biomeSelected!),
			campBackground: convertCamp[_selectedCampTag],
			symbol: _symbolSelected,
			startingMembers: _members,
			gameMode: _gameMode,
			startingSeason: _selectedSeason!
		);
		game.Clan.CreateClan();
		//game.CurrentEventsList.Clear()
		//game.HerbEventsList.Clear()
		//Cat.GriefStrings.Clear()
		//Cat.SortCats();
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
	[GeneratedRegex(@"[symbol1234567890]")]
	private static partial Regex SymbolNamePattern();
}
