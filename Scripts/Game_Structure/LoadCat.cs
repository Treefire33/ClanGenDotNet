using ClanGenDotNet.Scripts.Cats;
using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.Game_Structure;

public class LoadCat
{
	public static void LoadCats()
	{
		var clanName = game.Switches["clan_list"]![0];
		var jsonPath = $"{GetSaveDirectory()}\\{clanName}\\clan_cats.json";

		_ = JsonConvert.DeserializeObject<List<Cat>>(File.ReadAllText(jsonPath))!;
	}
}
