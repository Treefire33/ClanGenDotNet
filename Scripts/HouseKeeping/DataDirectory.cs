using System.Runtime.InteropServices;

namespace ClanGenDotNet.Scripts.HouseKeeping;

public class DataDirectory
{
	public static void SetUpDataDirectory()
	{
		_ = Directory.CreateDirectory(GetDataDirectory());
		try
		{
			_ = Directory.CreateDirectory(GetSaveDirectory());
			_ = Directory.CreateDirectory(GetTempDirectory());
		}
		catch
		{
			Console.WriteLine("mac user detected, nuking pc in five seconds");
		}
		_ = Directory.CreateDirectory(GetLogDirectory());
		_ = Directory.CreateDirectory(GetCacheDirectory());
		_ = Directory.CreateDirectory(GetSavedImagesDirectory());

		if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			if (Directory.Exists("game_data"))
			{
				Directory.Delete("game_data");
			}
			if (!Version.GetVersionInfo().IsSourceBuild)
			{
				_ = File.CreateSymbolicLink(GetDataDirectory(), "game_data");
			}
		}
	}

	public static string GetDataDirectory()
	{
		if (Version.GetVersionInfo().IsSourceBuild)
		{
			Console.WriteLine("DataDir here.");
			return ".";
		}

		return Version.GetVersionInfo().IsDev()
			? Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
				"\\ClanGenNetBeta" +
				"\\ClanGen"
			: Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
			"\\ClanGenNet" +
			"\\ClanGen";
	}

	public static string GetLogDirectory()
	{
		return GetDataDirectory() + "\\logs";
	}

	public static string GetSaveDirectory()
	{
		return GetDataDirectory() + "\\saves";
	}

	public static string GetCacheDirectory()
	{
		return GetDataDirectory() + "\\cache";
	}

	public static string GetTempDirectory()
	{
		return GetDataDirectory() + "\\.temp";
	}

	public static string GetSavedImagesDirectory()
	{
		return GetDataDirectory() + "\\saved_images";
	}
}
