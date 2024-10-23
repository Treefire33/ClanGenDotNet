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
	public string Subjective = subjective;
	public string Objective = objective;
	public string Possessive = possessive;
	public string Inpossessive = inpossessive;
	public string Self = self;
	public int Conjugate = conjugate;

	public static Pronouns TheyThemTheirs = new("they", "them", "their", "theirs", "themself", 1);
	public static Pronouns HeHimHis = new("he", "him", "his", "his", "himself", 2);
	public static Pronouns SheHerHers = new("she", "her", "her", "hers", "herself", 1);
}
