using ClanGenDotNet.Scripts.Cats;
using Microsoft.Toolkit.HighPerformance.Buffers;

namespace ClanGenDotNet.Scripts.Screens;

public class CampScreen (string name = "camp screen") : Screens(name)
{
	private int maxSpritesDisplayed = 400;
	private List<UICatButton> _catButtons = [];

	private Dictionary<string, dynamic> _layout;

	public override void ScreenSwitches()
	{
		base.ScreenSwitches();
		game.Switches["cat"] = null;
		if (Clan.Layouts.ContainsKey(game.Clan!.Biome.ToString() + game.Clan.CampBackground))
		{
			 _layout = Clan.Layouts[game.Clan!.Biome.ToString() + game.Clan.CampBackground];
		}
		else
		{
			_layout = Clan.Layouts["default"];
		}

		ChooseCatPositions();

		//Screens.SetDisabledMenuButtons(["camp_screen"]);
		//Screens.UpdateHeadingText($"{game.Clan.Name}Clan");
		//Screens.ShowMenuButtons();

		_catButtons.Clear();

		int spritesDisplayed = 0;
		foreach (var id in game.Clan.ClanCats)
		{
			var kitty = Cat.AllCats[id];
			if (
				!kitty.Dead
				&& kitty.InCamp
				&& !(kitty.Exiled || kitty.Outside)
				&& (
					kitty.Status != "newborn"
					|| game.Config.Fun.AllCatsAreNewborn
					|| game.Config.Fun.NewbornsCanRoam
				)
			)
			{
				spritesDisplayed++;
				if (spritesDisplayed > maxSpritesDisplayed)
				{
					break;
				}

				try
				{
					_catButtons.Add(new UICatButton(
						UIScale(new ClanGenRect(
							new Vector2(kitty.Placement!.Value.Item1, kitty.Placement!.Value.Item2), 50, 50)),
						kitty.Sprite,
						kitty,
						game.Manager
					));
				}
				catch
				{
					Console.WriteLine("man I don't know");
				}
			}
		}
	}

	private void ChooseCatPositions()
	{
		Dictionary<string, dynamic> firstChoices = _layout;

		List<string> allDens = [
			"nursery place",
			"leader place",
			"elder place",
			"medicine place",
			"apprentice place",
			"clearing place",
			"warrior place"
		];

		foreach ( string choice in allDens )
		{
			firstChoices[choice].Add(firstChoices[choice]);
		}

		foreach (var id in game.Clan!.ClanCats)
		{
			Cat kitty = Cat.AllCats[id];
			if (kitty.Dead || kitty.Outside)
			{
				continue;
			}

			switch (kitty.Status)
			{
				case "newborn":
					if (game.Config.Fun.AllCatsAreNewborn || game.Config.Fun.NewbornsCanRoam)
					{
						kitty.Placement = ChooseNonOverlappingPosition(firstChoices, allDens, [1, 100, 1, 1, 1, 100, 50]);
					}
					break;
				case "apprentice":
				case "mediator apprentice":
					kitty.Placement = ChooseNonOverlappingPosition(firstChoices, allDens, [1, 50, 1, 1, 100, 100, 1]);
					break;
				case "deputy":
					kitty.Placement = ChooseNonOverlappingPosition(firstChoices, allDens, [1, 50, 1, 1, 1, 50, 1]);
					break;
				case "elder":
					kitty.Placement = ChooseNonOverlappingPosition(firstChoices, allDens, [1, 1, 2000, 1, 1, 1, 1]);
					break;
				case "kitten":
					kitty.Placement = ChooseNonOverlappingPosition(firstChoices, allDens, [60, 8, 1, 1, 1, 1, 1]);
					break;
				case "medicine cat apprentice":
				case "medicine cat":
					kitty.Placement = ChooseNonOverlappingPosition(firstChoices, allDens, [20, 20, 20, 400, 1, 1, 1]);
					break;
				case "warrior":
				case "mediator":
					kitty.Placement = ChooseNonOverlappingPosition(firstChoices, allDens, [1, 1, 1, 1, 1, 60, 60]);
					break;
				case "leader":
					game.Clan.Leader!.Placement = ChooseNonOverlappingPosition(firstChoices, allDens, [1, 200, 1, 1, 1, 1, 1]);
					break;
				default:
					break;
			}
		}
	}
	
	private (int, int) ChooseNonOverlappingPosition(dynamic firstChoices, List<string> dens, List<int>? weights = null)
	{
		/*if (weights == null)
		{
			weights = [];
			for (int i = 0; i < weights.Count; i++) { weights.Add(1); }
		}

		int chosenIndex = Enumerable.Range(0, dens.Count).PickRandomWeighted([.. weights]);
		var firstChosenDen = dens[chosenIndex];
		while (true) //this surely isn't dangerous at all! totally not!
		{
			var chosenDen = dens[chosenIndex];
			if (firstChoices[chosenDen] != null)
			{
				(List<int>, string) position = firstChoices[chosenDen][Rand.Next(0, firstChoices[chosenDen].Count)];
				firstChoices[chosenDen].Remove(position);
				var justPosition = position.Item1;

				if (!firstChoices[chosenDen].Contains(position))
				{
					if (position.Item2.Contains('x') && !(position.Item2.Contains('y') || GetRandBits(2) == 1))
					{
						justPosition[0] += 15 * (Rand.Next(0, 1) == 1 ? 1 : -1);
					}
					if (position.Item2.Contains('y'))
					{
						justPosition[1] += 15;
					}
				}
				
				return (justPosition[0], justPosition[1]);
			}
			dens.RemoveAt(chosenIndex);
			weights.RemoveAt(chosenIndex);
			if (dens == null)
			{
				break;
			}
			chosenIndex = Enumerable.Range(0, dens.Count).PickRandomWeighted([.. weights]);
		}

		(List<int>, string) pos = _layout[firstChosenDen].PickRandom();
		var justPos = pos.Item1;
		if (pos.Item2.Contains('x') && !(pos.Item2.Contains('y') || GetRandBits(2) == 1))
		{
			justPos[0] += 15 * (Rand.Next(0, 1) == 1 ? 1 : -1);
		}
		if (pos.Item2.Contains('y'))
		{
			justPos[1] += 15;
		}

		return (justPos[0], justPos[1]);*/

		return (Rand.Next(200, 600), Rand.Next(200, 600));
	}
}
