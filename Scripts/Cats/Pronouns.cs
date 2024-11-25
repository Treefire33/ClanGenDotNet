using Newtonsoft.Json;

namespace ClanGenDotNet.Scripts.Cats;

public struct Pronouns(
	string subjective, 
	string objective, 
	string possessive, 
	string inpossessive, 
	string self, 
	int conjugate
)
{
	[JsonProperty("subject")]
	public string Subjective = subjective;
	[JsonProperty("object")]
	public string Objective = objective;
	[JsonProperty("poss")]
	public string Possessive = possessive;
	[JsonProperty("inposs")]
	public string Inpossessive = inpossessive;
	[JsonProperty("self")]
	public string Self = self;
	[JsonProperty("conju")]
	public int Conjugate = conjugate;

	public static Pronouns TheyThemTheirs = new("they", "them", "their", "theirs", "themself", 1);
	public static Pronouns HeHimHis = new("he", "him", "his", "his", "himself", 2);
	public static Pronouns SheHerHers = new("she", "her", "her", "hers", "herself", 1);
}
