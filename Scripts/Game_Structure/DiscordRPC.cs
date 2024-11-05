using Discord;

namespace ClanGenDotNet.Scripts.Game_Structure;

public class DiscordRPC
{
	public Discord.Discord Discord;
	private ActivityManager _activityManager;
	private Activity _clanGenActivity;

	public static double ConvertToUnixTimestamp(DateTime date)
	{
		DateTime origin = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		TimeSpan diff = date.ToUniversalTime() - origin;
		return Math.Floor(diff.TotalMilliseconds);
	}

	public DiscordRPC()
	{
		var clientID = Environment.GetEnvironmentVariable("DISCORD_CLIENT_ID");
		clientID ??= "1297043565515505686";
		Discord = new Discord.Discord(long.Parse(clientID!), (ulong)CreateFlags.NoRequireDiscord);

		_activityManager = Discord.GetActivityManager();
		_clanGenActivity = new()
		{
			ApplicationId = 1297043565515505686,
			Name = "ClanGen.NET",
			State = "Currently on {screen}",
			Details = "Testing treefire33's C# port of ClanGen.",
			Timestamps = new()
			{
				Start = (long)ConvertToUnixTimestamp(DateTime.Now)
			},
			Assets = new()
			{
				SmallImage = "small_image_thing",
				SmallText = "hi squidward",
				LargeImage = "menu",
				LargeText = "ClanGen.Net made by treefire33."
			},
			Type = ActivityType.Playing
		};	
	}

	public void UpdateActivity()
	{
		_clanGenActivity.Details = $"Managing a clan in ClanGen.Net.";
		_clanGenActivity.Assets.SmallText = "Currently testing ClanGen.Net.";
		_clanGenActivity.State = $"{game.CurrentScreen}";
		_activityManager.UpdateActivity(
			_clanGenActivity,
			(result) => { /*Console.WriteLine(result.ToString());*/ }
		);
	}
}
