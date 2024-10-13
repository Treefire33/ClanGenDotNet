using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.Game_Structure;
using ClanGenDotNet.Scripts.HouseKeeping;
using ClanGenDotNet.Scripts.UI;
using Raylib_cs;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using static ClanGenDotNet.Scripts.Game_Structure.Game;
using static ClanGenDotNet.Scripts.Resources;
using static ClanGenDotNet.Scripts.Utility;
using static Raylib_cs.Raylib;
using Version = ClanGenDotNet.Scripts.HouseKeeping.Version;

namespace ClanGenDotNet.Scripts.Screens;

public class SettingsScreen(string name = "settings screen") : Screens(name)
{
	private string _subMenu = "general";

	private Dictionary<string, UIElement> _checkboxes = [];

	private bool _settingsChanged = false;
	private Dictionary<string, object?> _settingsAtOpen = [];

	private UIButton? _generalSettingsButton;
	private UIButton? _audioSettingsButton;
	private UIButton? _infoButton;
	private UIButton? _languageButton;

	private UIButton? _saveSettingsButton;
	private UIButton? _fullscreenToggle;
	private UIButton? _openDataDirectory;
	private UIButton? _mainMenuButton;

	public override void ScreenSwitches()
	{
		base.ScreenSwitches();
		_generalSettingsButton = new UIButton(
			UIScale(new ClanGenRect(100, 100, 150, 30)),
			ButtonStyle.MenuLeft,
			"general settings",
			18,
			game.Manager
		);
		_audioSettingsButton = new UIButton(
			UIScale(new ClanGenRect(0, 100, 150, 30)).AnchorTo(AnchorPosition.LeftTarget, _generalSettingsButton.RelativeRect),
			ButtonStyle.MenuMiddle,
			"audio settings",
			18,
			game.Manager
		);
		_infoButton = new UIButton(
			UIScale(new ClanGenRect(0, 100, 150, 30)).AnchorTo(AnchorPosition.LeftTarget, _audioSettingsButton.RelativeRect),
			ButtonStyle.MenuMiddle,
			"info",
			18,
			game.Manager
		);
		_languageButton = new UIButton(
			UIScale(new ClanGenRect(0, 100, 150, 30)).AnchorTo(AnchorPosition.LeftTarget, _infoButton.RelativeRect),
			ButtonStyle.MenuRight,
			"language",
			18,
			game.Manager
		);

		_saveSettingsButton = new UIButton(
			UIScale(new ClanGenRect(0, 550, 150, 30)).AnchorTo(AnchorPosition.CenterX),
			ButtonStyle.Squoval,
			"Save Settings",
			20,
			game.Manager
		);
		_fullscreenToggle = new UIButton(
			UIScale(new ClanGenRect(617, 25, 158, 36)),
			ButtonID.ToggleFullscreen,
			"",
			20,
			game.Manager
		);
		_openDataDirectory = new UIButton(
			UIScale(new ClanGenRect(25, 645, 178, 30)),
			ButtonStyle.Squoval,
			"Open Data Directory",
			20,
			game.Manager
		);
		if (Version.GetVersionInfo().IsSandboxed)
		{
			_openDataDirectory.SetActive(false);
		}

		_mainMenuButton = new UIButton(
			UIScale(new ClanGenRect(25, 25, 153, 30)),
			ButtonStyle.Squoval,
			"Main Menu",
			25,
			game.Manager
		);

		OpenGeneralSettings();
	}

	private UIScrollingContainer? _generalScrollView;
	private UITextBox? _instructions;
	private void OpenGeneralSettings()
	{
		EnableAllMenuButtons();
		_generalSettingsButton!.SetActive(false);
		ClearSubSettingsButtonsAndText();
		_subMenu = "general";
		_saveSettingsButton!.Show();

		_generalScrollView = new UIScrollingContainer(
			UIScale(new ClanGenRect(0, 220, 700, 300)),
			game.Manager
		);
		_instructions = new UITextBox(
			UIScale(new ClanGenRect(100, 160, 600, 100)),
			"Change the general settings of your game here.\n" +
			"More settings are available in the settings page of your Clan.",
			20,
			TextAlignment.Center,
			Color.White,
			game.Manager,
			true
		);
		RefreshCheckboxes();
	}

	private void OpenAudioSettings()
	{
		EnableAllMenuButtons();
		_audioSettingsButton!.SetActive(false);
		ClearSubSettingsButtonsAndText();
		_subMenu = "audio";
		_saveSettingsButton!.Show();
	}

	private UIScrollingContainer? _infoScrollView;
	private void OpenInfoScreen()
	{
		EnableAllMenuButtons();
		_infoButton!.SetActive(false);
		ClearSubSettingsButtonsAndText();
		_subMenu = "info";
		_saveSettingsButton!.Hide();
	}

	private UITextBox? _languageInstructions;
	private void OpenLanguageSettings()
	{
		EnableAllMenuButtons();
		_languageButton!.SetActive(false);
		ClearSubSettingsButtonsAndText();
		_subMenu = "language";
		_saveSettingsButton!.Show();

		_languageInstructions = new UITextBox(
			UIScale(new ClanGenRect(100, 160, 600, 50)),
			"Change the language of the game here. This has not been implemented yet.",
			20,
			TextAlignment.Center,
			Color.White,
			game.Manager
		);

		RefreshCheckboxes();
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
			if (evnt.Element == _fullscreenToggle)
			{
				SaveSettings();
				game.SwitchSetting("fullscreen");
				game.SaveSettings();
				ScreenSettings.SetDisplayMode(sourceScreen: this);
			}
			if(evnt.Element == _openDataDirectory)
			{
				try
				{
					if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
					{
						Process.Start("open", $"-R {DataDirectory.GetDataDirectory()}");
					}
					else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
					{
						Process.Start("explorer.exe", $"{DataDirectory.GetDataDirectory()}");
					}
					else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
					{
						Process.Start("xdg-open", $"{DataDirectory.GetDataDirectory()}");
					}
				}
				catch { Console.WriteLine("Open Data Directory has failed to open the directory, blame Treefire33"); }
			}
			if (evnt.Element == _saveSettingsButton)
			{
				SaveSettings();
				game.SaveSettings();
				_settingsChanged = false;
				UpdateSaveButton();
			}
			if (evnt.Element == _generalSettingsButton)
			{
				OpenGeneralSettings();
			}
			else if(evnt.Element == _audioSettingsButton)
			{
				OpenAudioSettings();
			}
			else if(evnt.Element == _infoButton)
			{
				OpenInfoScreen();
			}
			else if(evnt.Element == _languageButton)
			{
				OpenLanguageSettings();
			}
			if (new List<string> { "general", "relation", "language" }.Contains(_subMenu))
			{
				HandleCheckbox(evnt);
			}
		}
	}

	private void HandleCheckbox(Event evnt)
	{
		if (_checkboxes.Values.Contains(evnt.Element))
		{
			foreach (KeyValuePair<string, UIElement> settingValue in _checkboxes)
			{
				if (settingValue.Value == evnt.Element)
				{
					game.SwitchSetting(settingValue.Key);
					_settingsChanged = true;
					UpdateSaveButton();
				}
			}
		}
	}

	private void SaveSettings()
	{
		//_settingsAtOpen = CloneDictionary<string, object>(game.Settings);
	}

	private void UpdateSaveButton()
	{
		_saveSettingsButton!.SetActive(_settingsChanged);
	}

	private void RefreshCheckboxes()
	{
		foreach(UICheckbox checkbox in _checkboxes.Values)
		{
			checkbox.Kill();
		}
		_checkboxes.Clear();

		if (_subMenu == "language")
		{
			_checkboxes.Add("english", new UIButton(
				UIScale(new ClanGenRect(310, 200, 180, 51)),
				ButtonID.EnglishLadder,
				"",
				0,
				game.Manager
			));
			_checkboxes.Add("spanish", new UIButton(
				UIScale(new ClanGenRect(310, 0, 180, 37)).AnchorTo(
					AnchorPosition.TopLeft, 
					_checkboxes.Last().Value.RelativeRect
				),
				ButtonStyle.LadderMiddle,
				"Spanish",
				20,
				game.Manager
			));
			_checkboxes.Add("german", new UIButton(
				UIScale(new ClanGenRect(310, 0, 180, 37)).AnchorTo(
					AnchorPosition.TopLeft,
					_checkboxes.Last().Value.RelativeRect
				),
				ButtonStyle.LadderBottom,
				"German",
				20,
				game.Manager
			));
		}
		else if (_subMenu == "general")
		{
			int i = 0;
			foreach (string setting in game.GameSettings.General.Keys)
			{
				_checkboxes.Add(setting, new UICheckbox(
					UIScale(new ClanGenRect(170, i < 0 ? 120 : 0, 34, 34))
						.AnchorTo(AnchorPosition.TopLeft, i > 0 
						? _checkboxes.Values.ToArray()[i - 1].RelativeRect 
						: UIScale(new ClanGenRect(170, i < 0 ? 34 : 0, 34, 34))),
					(string)game.GameSettings.General[setting][0],
					game.Manager
				));
				if (_checkboxes[setting] is UICheckbox box) { box.Checked = (bool)game.Settings[setting]!; }
				_generalScrollView!.AddElement(_checkboxes.Last().Value, i == 0);
				i++;
			}
		}
	}

	private void ClearSubSettingsButtonsAndText()
	{
		foreach (UIElement checkbox in _checkboxes.Values)
		{
			checkbox.Kill();
		}
		_checkboxes.Clear();

		_generalScrollView?.Kill();
		_generalScrollView = null;
		_instructions?.Kill();
		_instructions = null;

		_languageInstructions?.Kill();
		_languageInstructions = null;
	}

	private void EnableAllMenuButtons()
	{
		_generalSettingsButton!.SetActive(true);
		_infoButton!.SetActive(true);
		_languageButton!.SetActive(true);
		_audioSettingsButton!.SetActive(true);
	}

	public override void ExitScreen()
	{
		base.ExitScreen();
		ClearSubSettingsButtonsAndText();
		_generalSettingsButton!.Kill();
		_generalSettingsButton = null;
		_audioSettingsButton!.Kill();
		_audioSettingsButton = null;
		_infoButton!.Kill();
		_infoButton = null;
		_mainMenuButton!.Kill();
		_mainMenuButton = null;
		_languageButton!.Kill();
		_languageButton = null;
		_fullscreenToggle!.Kill();
		_fullscreenToggle = null;
		_openDataDirectory!.Kill();
		_openDataDirectory = null;
		_saveSettingsButton!.Kill();
		_saveSettingsButton = null;
	}
}
