using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salaros.Configuration;

namespace ClanGenDotNet.Scripts.HouseKeeping;

public class Version
{
	public static string VersionName = "0.11.2.dev.net";
	public static int SaveVersionNumber = 3;

	public static string UserDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

	public static VersionInfo? Instance = null;

	public static VersionInfo GetVersionInfo()
	{
		if (Instance == null)
		{
			bool isSourceBuild = false;
			string versionNumber = VersionName;
			string releaseChannel = "";
			string upstream = "";
			bool isItch = false;
			bool isSandboxed = false;
			//bool isThonny = false; Now how are you running this on Thonny?
			bool gitInstalled = false;

			isSourceBuild = !AppDomain.CurrentDomain.IsDefaultAppDomain();

			if (File.Exists(".\\version.ini"))
			{
				var settings = new ConfigParserSettings
				{
					Encoding = Encoding.UTF8
				};
				ConfigParser versionIni = new ConfigParser(".\\version.ini", settings);
				versionNumber = versionIni.GetValue("DEFAULT", "version_number");
				releaseChannel = versionIni.GetValue("DEFAULT", "release_channel");
				upstream = versionIni.GetValue("DEFAULT", "upstream");
			}
			else
			{
				try
				{
					using Process process = Process.Start(new ProcessStartInfo()
					{
						FileName = "git",
						Arguments = "rev-parse HEAD",
						RedirectStandardOutput = true,
						UseShellExecute = false,
						CreateNoWindow = true
					})!;
					using StreamReader reader = process.StandardOutput;
					versionNumber = reader.ReadToEnd().Trim();
					gitInstalled = true;
				}
				catch
				{
					Console.WriteLine("download git please :3");
				}
			}

			if (
				Array.Exists(Environment.GetCommandLineArgs(), arg => arg == "--launched-through-itch")
				|| Environment.GetEnvironmentVariable("LAUNCHED_THROUGH_ITCH") != null
			)
			{
				isItch = true;
			}

			if (UserDataDirectory.Contains("itch-player", StringComparison.CurrentCultureIgnoreCase))
			{
				isSandboxed = true;
			}

			Instance = new VersionInfo(
				isSourceBuild,
				releaseChannel!,
				versionNumber,
				upstream,
				isItch,
				isSandboxed,
				gitInstalled
			);
		}
		return (VersionInfo)Instance!;
	}
}

public struct VersionInfo(
	bool source,
	string release,
	string version,
	string upstream,
	bool itch,
	bool sandbox,
	bool git
)
{
	public bool IsSourceBuild { get; set; } = source;
	public string ReleaseChannel { get; set; } = release;
	public string VersionNumber { get; set; } = version;
	public string Upstream { get; set; } = upstream;
	public bool IsItch { get; set; } = itch;
	public bool IsSandboxed { get; set; } = sandbox;
	public bool GitInstalled { get; set; } = git;

	public readonly bool IsDev()
	{
		return this.ReleaseChannel != "stable";
	}
}