using Newtonsoft.Json;
using System.Collections.Immutable;

namespace ClanGenDotNet.Scripts.UI;

public class Layout
{
	[JsonProperty("leader den")]
	public List<int> LeaderDen = [];

	[JsonProperty("medicine den")]
	public List<int> MedicineDen = [];

	[JsonProperty("nursery")]
	public List<int> Nursery = [];

	[JsonProperty("clearing")]
	public List<int> Clearing = [];

	[JsonProperty("apprentice den")]
	public List<int> ApprenticeDen = [];

	[JsonProperty("warrior den")]
	public List<int> WarriorDen = [];

	[JsonProperty("elder den")]
	public List<int> ElderDen = [];

	[JsonProperty("leader place")]
	public List<List<object>> LeaderPlace = [];

	[JsonProperty("medicine place")]
	public List<List<object>> MedicinePlace = [];

	[JsonProperty("nursery place")]
	public List<List<object>> NurseryPlace = [];

	[JsonProperty("clearing place")]
	public List<List<object>> ClearingPlace = [];

	[JsonProperty("apprentice place")]
	public List<List<object>> ApprenticePlace = [];

	[JsonProperty("warrior place")]
	public List<List<object>> WarriorPlace = [];

	[JsonProperty("elder place")]
	public List<List<object>> ElderPlace = [];

	public Dictionary<string, List<List<object>>> GetDict()
	{
		return new Dictionary<string, List<List<object>>>()
		{
			{ "nursery place", NurseryPlace },
			{ "leader place", LeaderPlace },
			{ "elder place", ElderPlace },
			{ "medicine place", MedicinePlace },
			{ "apprentice place", ApprenticePlace },
			{ "clearing place", ClearingPlace },
			{ "warrior place", WarriorPlace }
		};
	}
}
