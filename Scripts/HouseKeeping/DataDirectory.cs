using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenDotNet.Scripts.HouseKeeping;

public class DataDirectory
{
	public static void SetUpDataDirectory()
	{
		Directory.CreateDirectory(GetDataDirectory());
		try
		{
			Directory.CreateDirectory(GetSaveDirectory());
			Directory.CreateDirectory(GetTempDirectory());
		}
		catch
		{
			Console.WriteLine("mac user detected, nuking pc in five seconds");
		}
		Directory.CreateDirectory(GetLogDirectory());
		Directory.CreateDirectory(GetCacheDirectory());
		Directory.CreateDirectory(GetSavedImagesDirectory());

		Console.WriteLine(Environment.OSVersion.Platform.ToString());
		if (Environment.OSVersion.Platform.ToString() != "Win32NT")
		{
			if (Directory.Exists("game_data"))
			{
				Directory.Delete("game_data");
			}
			if (!Version.GetVersionInfo().IsSourceBuild)
			{
				File.CreateSymbolicLink(GetDataDirectory(), "game_data");
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

		if (Version.GetVersionInfo().IsDev())
		{
			return
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
				"\\ClanGenNetBeta" +
				"\\ClanGen";
		}

		return
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
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