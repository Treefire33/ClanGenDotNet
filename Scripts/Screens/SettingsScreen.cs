using ClanGenDotNet.Scripts.Events;
using ClanGenDotNet.Scripts.Game_Structure;
using ClanGenDotNet.Scripts.HouseKeeping;
using ClanGenDotNet.Scripts.UI;
using Raylib_cs;
using System.Diagnostics;
using static ClanGenDotNet.Scripts.Game_Structure.Game;
using static ClanGenDotNet.Scripts.Resources;
using static ClanGenDotNet.Scripts.Utility;
using static Raylib_cs.Raylib;
using Version = ClanGenDotNet.Scripts.HouseKeeping.Version;

namespace ClanGenDotNet.Scripts.Screens;

public class SettingsScreen(string name = "settings screen") : Screens(name)
{
	private string _subMenu = "general";

	private Dictionary<string, UICheckbox> checkboxes = [];

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
	}

	private void OpenGeneralSettings()
	{
		EnableAllMenuButtons();
		_generalSettingsButton!.SetActive(false);
		ClearSubSettingsButtonsAndText();
		_subMenu = "general";

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
					ProcessStartInfo startInfo = new ProcessStartInfo
					{
						Arguments = DataDirectory.GetDataDirectory(),
						FileName = "explorer.exe"
					};

					if(Environment.OSVersion.Platform.ToString() == "Unix")
					{
						Process.Start("open", $"-R {DataDirectory.GetDataDirectory()}");
					}
					else if(Environment.OSVersion.Platform.ToString() == "Win32NT")
					{
						Process.Start(startInfo);
					}
				}
				catch
				{
					Console.WriteLine("Open Data Directory has failed to open the directory, blame Treefire33");
				}
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

			}
			else if(evnt.Element == _infoButton)
			{

			}
			else if(evnt.Element == _languageButton)
			{

			}
			if (new List<string> { "general", "relation", "language" }.Contains(_subMenu))
			{
				HandleCheckbox(evnt);
			}
		}
	}

	private void HandleCheckbox(Event evnt)
	{
		if (checkboxes.Values.Contains(evnt.Element))
		{
			foreach (KeyValuePair<string, UICheckbox> settingValue in checkboxes)
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
		foreach(UICheckbox checkbox in checkboxes.Values)
		{
			checkbox.Kill();
		}
		checkboxes.Clear();

		int i = 0;
		foreach(string setting in game.GameSettings.General.Keys)
		{
			checkboxes.Add(setting, new UICheckbox(
				UIScale(new ClanGenRect(170, i < 0 ? 120 : 0, 34, 34))
					.AnchorTo(AnchorPosition.TopLeft, i > 0 ? checkboxes.Values.ToArray()[i-1].RelativeRect : UIScale(new ClanGenRect(170, i < 0 ? 34 : 0, 34, 34))),
				(string)game.GameSettings.General[setting][0],
				game.Manager
			));
			checkboxes[setting].Checked = (bool)game.Settings[setting];
			i++;
		}
	}

	private void ClearSubSettingsButtonsAndText()
	{
		foreach (UICheckbox checkbox in checkboxes.Values)
		{
			checkbox.Kill();
		}
		checkboxes.Clear();
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