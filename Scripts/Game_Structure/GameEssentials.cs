using ClanGenDotNet.Scripts.Cats;
using ClanGenDotNet.Scripts.HouseKeeping;
using ClanGenDotNet.Scripts.UI;
using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.Game_Structure;

public class Game(string currentScreen = "start screen")
{
	public int MaxNameLength = 10;

	public int ScreenX = 800, ScreenY = 700;
	public UIManager Manager = new();
	public string CurrentScreen = currentScreen;
	public string LastScreenForUpdate = "start screen";
	public string? LastScreenForProfile = "list screen";
	public bool Clicked = false;
	public bool SwitchScreens = false;
	public bool Fullscreen = false;

	public Clan? Clan = null;
	public List<Cat> ChooseCats = [];

	public Dictionary<string, Screens.Screens> AllScreens = [];
	public Dictionary<string, object?> Switches = new()
	{
		{ "cat", null },
		{ "clan_name", "" },
		{ "leader", null },
		{ "deputy", null },
		{ "medicine_cat", null },
		{ "members", new List<object>() },
		{ "re_roll", false },
		{ "roll_count", 0 },
		{ "event", null },
		{ "cur_screen", "start screen" },
		{ "naming_text", "" },
		{ "timeskip", false },
		{ "mate", null },
		{ "choosing_mate", false },
		{ "mentor", null },
		{ "setting", null },
		{ "save_settings", false },
		{ "list_page", 1 },
		{ "last_screen", "start screen" },
		{ "events_left", 0 },
		{ "save_clan", false },
		{ "saved_clan", false },
		{ "new_leader", false },
		{ "apprentice_switch", false },
		{ "deputy_switch", false },
		{ "clan_list", "" },
		{ "switch_clan", false },
		{ "read_clans", false },
		{ "kill_cat", false },
		{ "current_patrol", new List<object>() },
		{ "patrol_remove", false },
		{ "cat_remove", false },
		{ "fill_patrol", false },
		{ "patrol_done", false },
		{ "error_message", "" },
		{ "traceback", "" },
		{ "apprentice", null },
		{ "change_name", "" },
		{ "change_suffix", "" },
		{ "name_cat", null },
		{ "biome", null },
		{ "camp_bg", null },
		{ "language", "english" },
		{ "options_tab", null },
		{ "profile_tab_group", null },
		{ "sub_tab_group", null },
		{ "gender_align", null },
		{ "show_details", false },
		{ "chosen_cat", null },
		{ "game_mode", "" },
		{ "set_game_mode", false },
		{ "broke_up", false },
		{ "show_info", false },
		{ "patrol_chosen", "general" },
		{ "favorite_sub_tab", null },
		{ "root_cat", null },
		{ "window_open", false },
		{ "skip_conditions", new List<object>() },
		{ "show_history_moons", false },
		{ "fps", 30 },
		{ "war_rel_change_type", "neutral" },
		{ "disallowed_symbol_tags", new List<object>() },
		{ "audio_mute", false },
		{ "saved_scroll_positions", new Dictionary<object, object>() },
		{ "moon&season_open", false },
	};
	public Dictionary<string, object?> Settings = [];
	public Dictionary<string, List<object?>> SettingsLists = [];
	public Settings GameSettings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(".\\Resources\\gamesettings.json"))!;
	public Config Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(".\\Resources\\game_config.json"))!;

	public void SwitchSetting(string settingName)
	{
		int listIndex = SettingsLists[settingName].IndexOf(Settings[settingName]);

		Settings[settingName] = listIndex == SettingsLists[settingName].Count - 1 ? SettingsLists[settingName][0] : SettingsLists[settingName][listIndex + 1];
	}

	//the function of time.
	public void SetSettingsFromLoaded()
	{
		foreach (string key in game.GameSettings.Other.Keys)
		{
			Settings.Add(key, game.GameSettings.Other[key][0]);
			SettingsLists.Add(key, game.GameSettings.Other[key]!);
		}
		foreach (string key in game.GameSettings.General.Keys)
		{
			List<object> setting = game.GameSettings.General[key];
			Settings.Add(key, setting[2]);
			SettingsLists.Add(key, [(bool)setting[2], !(bool)setting[2]]);
		}
	}

	public void UpdateGame()
	{
		if (CurrentScreen != (string)Switches["cur_screen"]!)
		{
			CurrentScreen = (string)Switches["cur_screen"]!;
			SwitchScreens = true;
		}
		Clicked = false;
	}

	public void SaveSettings()
	{
		if (File.Exists(DataDirectory.GetSaveDirectory() + "\\settings.txt"))
		{
			File.Delete(DataDirectory.GetSaveDirectory() + "\\settings.txt");
		}

		try
		{
			SafeSave(DataDirectory.GetSaveDirectory() + "\\settings.json", Settings);
		}
		catch
		{
			Console.WriteLine("Failed to safe save settings.");
		}
	}

	public void LoadSettings()
	{
		Dictionary<string, object> loadedSettings;
		try
		{
			loadedSettings = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(DataDirectory.GetSaveDirectory() + "\\settings.json"))!;
		}
		catch
		{
			Console.WriteLine("no gamesettings found");
			return;
		}

		Settings = loadedSettings!;
	}

	public static void SafeSave(string path, object writeData, bool checkIntegrity = false, int maxAttempts = 15)
	{
		string data = writeData.GetType() != typeof(string) ? JsonConvert.SerializeObject(writeData) : (string)writeData;
		string? dirName = Path.GetDirectoryName(path), fileName = Path.GetDirectoryName(path);

		if (checkIntegrity)
		{
			if (fileName == null)
			{
				throw new Exception($"Safe Save Error: No file name was found in {path}");
			}

			string tempFilePath = DataDirectory.GetTempDirectory() + "\\" + fileName + ".tmp";

			int iter = 0;
			while (true)
			{
				using StreamWriter writeFile = new(tempFilePath);
				writeFile.Write(data);
				writeFile.Flush();
				writeFile.BaseStream.Flush();

				using StreamReader openFile = new(tempFilePath);
				string readData = openFile.ReadToEnd();

				if (data != readData)
				{
					iter++;
					if (iter > maxAttempts)
					{
						Console.WriteLine($"Safe Save Error: {fileName} was unable to properly save {iter} times. Saving failed.");
						throw new Exception($"Safe Save Error: {fileName} was unable to properly save {iter} times!");
					}
					Console.WriteLine($"Safe Save: {fileName} was incorrectly saved. Trying again.");
					continue;
				}

				Directory.Move(tempFilePath, path);
				return;
			}
		}
		else
		{
			_ = Directory.CreateDirectory(dirName!);
			using StreamWriter writeFile = new(path);
			writeFile.Write(data);
			writeFile.Flush();
			writeFile.BaseStream.Flush();
		}
	}

	public static readonly Game game = new();
}
