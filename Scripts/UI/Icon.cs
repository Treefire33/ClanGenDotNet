namespace ClanGenDotNet.Scripts.UI;

public enum Icon
{
	Speaker = 0x1F50A,
	Mute = 0x1F507,
	Dice = 0x2684,
	CatHead = 0x1F431,
	StarClan = 0x26EA,
	DarkForest = 0x1F4A7,
	ClanPlayer = 0x2302,
	ClanOther = 0x1F3F0,
	ClanUnknown = 0x1F3DA,
	Paw = 0x1F43E,
	Mouse = 0x1F401,
	Scratches = 0x1F485,
	Herb = 0x1F33F,
	Newleaf = 0x1FAB4,
	Greenleaf = 0x2600,
	Leaffall = 0x1F342,
	Leafbare = 0x2744,
	ArrowDoubleLeft = 0x23EA,
	ArrowDoubleRight = 0x23E9,
	ArrowLeft = 0x2190,
	ArrowRight = 0x2192,
	Magnify = 0x1F50D,
	Notepad = 0x1F5C9
}

public enum ArrowSegment
{
	ArrowBody = 0x0001F89C,
	ArrowBodyHalf = 0x0001F89E,
	ArrowTailLeft = 0x2513,
	ArrowTailRight = 0x250F,
	ArrowHeadLeft = 0x2B05,
	ArrowHeadRight = 0x2B95
}

public static class IconHelperFunctions
{
	public static string GetAsUTF8(this Icon icon)
	{
		return ((char)icon).ToString();
	}

	public static string GetAsUTF8(this ArrowSegment icon)
	{
		return ((char)icon).ToString();
	}
}
