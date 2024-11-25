using ClanGenDotNet.Scripts.UI;
using static ClanGenDotNet.Scripts.Game_Structure.Game;

namespace ClanGenDotNet.Scripts;

public static class Resources
{
	//Theme Colours
	public static Color LightModeColour;
	public static Color DarkModeColour;

	//Fonts
	public unsafe static Font Clangen = LoadFont(".\\Resources\\Font\\clangen.ttf");
	public unsafe static readonly Font NotoSansRegular = LoadFont(".\\Resources\\Font\\NotoSans-Regular.ttf");
	public unsafe static readonly Font NotoSansMedium = LoadFont(".\\Resources\\Font\\NotoSans-Medium.ttf");

	//General
	public static readonly Texture2D Frame = LoadTexture(".\\Resources\\Images\\frame.png");
	public static readonly NPatchInfo FrameNPatch = new()
	{
		source = new Rectangle(0, 0, Frame.width, Frame.height),
		top = 10,
		bottom = 10,
		left = 10,
		right = 10
	};
	public static readonly Texture2D RoundedFrame = LoadTexture(".\\Resources\\Images\\rounded_frame.png");
	public static readonly NPatchInfo RoundedFrameNPatch = new()
	{
		source = new Rectangle(0, 0, Frame.width, Frame.height),
		top = 10,
		bottom = 10,
		left = 10,
		right = 10
	};

	public static readonly Texture2D ClanNameBackground = LoadTexture(".\\Resources\\Images\\clan_name_bg.png");

	//Main Menu
	public static readonly Texture2D MenuImage = LoadTexture(".\\Resources\\Images\\menu.png");

	//Clan Creation
	public static readonly Texture2D GameModeTextBox = LoadTexture(".\\Resources\\Images\\game_mode_text_box.png");

	//Image Buttons
	public static readonly List<Texture2D> ToggleFullscreenButtonImages =
		GenButtonsFromName("toggle_fullscreen");
	public static readonly List<Texture2D> TwitterButtonImages = GenButtonsFromName("twitter");
	public static readonly List<Texture2D> TumblrButtonImages = GenButtonsFromName("tumblr");
	public static readonly List<Texture2D> DiscordButtonImages = GenButtonsFromName("discord");
	public static readonly List<Texture2D> EnglishLadderImages = GenButtonsFromName("english");

	public static readonly List<Texture2D> GrantLivesButton = GenButtonsFromName("grant_lives");
	public static readonly List<Texture2D> SupportLeaderButton = GenButtonsFromName("support_leader");
	public static readonly List<Texture2D> AidClanButton = GenButtonsFromName("aid_clan");

	public static readonly List<Texture2D> ForestBiomeButton = GenButtonsFromName("forest");
	public static readonly List<Texture2D> PlainsBiomeButton = GenButtonsFromName("plains");
	public static readonly List<Texture2D> BeachBiomeButton = GenButtonsFromName("beach");
	public static readonly List<Texture2D> MountainBiomeButton = GenButtonsFromName("mountain");

	//why...?
	public static readonly List<Texture2D> ClassicTabButton = GenButtonsFromName("classic_camp");
	public static readonly List<Texture2D> GullyTabButton = GenButtonsFromName("gully_camp");
	public static readonly List<Texture2D> GrottoTabButton = GenButtonsFromName("grotto_camp");
	public static readonly List<Texture2D> LakesideTabButton = GenButtonsFromName("lakeside_camp");

	public static readonly List<Texture2D> CliffTabButton = GenButtonsFromName("cliff_camp");
	public static readonly List<Texture2D> CaveTabButton = GenButtonsFromName("cave_camp");
	public static readonly List<Texture2D> CrystalTabButton = GenButtonsFromName("crystal_camp");
	public static readonly List<Texture2D> RuinsTabButton = GenButtonsFromName("ruins_camp");

	public static readonly List<Texture2D> GrasslandsTabButton = GenButtonsFromName("grasslands_camp");
	public static readonly List<Texture2D> TunnelTabButton = GenButtonsFromName("tunnel_camp");
	public static readonly List<Texture2D> WastelandsTabButton = GenButtonsFromName("wastelands_camp");
	
	public static readonly List<Texture2D> TidepoolTabButton = GenButtonsFromName("tidepool_camp");
	public static readonly List<Texture2D> TidalCaveTabButton = GenButtonsFromName("tidal_cave_camp");
	public static readonly List<Texture2D> ShipwreckTabButton = GenButtonsFromName("shipwreck_camp");

	public static readonly List<Texture2D> FiltersTabButton = GenButtonsFromName("filters_tab");
	public static readonly List<Texture2D> SymbolSelectButton = GenButtonsFromName("symbol_select");

	public static readonly List<List<Texture2D>> CheckboxImages = [
		GenButtonsFromName("checkmark_off"),
		GenButtonsFromName("checkmark_on"),
	];

	//Buttons
	public static readonly List<Texture2D> MainMenuButtonImages = GenButtonsFromName("mainmenu");
	public static readonly List<Texture2D> SquircleButtonImages = GenButtonsFromName("general");
	public static readonly List<Texture2D> DropdownButtonImages = GenButtonsFromName("dropdown");
	public static readonly List<Texture2D> RoundedRectButtonImages = GenButtonsFromName("rounded_rect");
	public static readonly List<Texture2D> IconButtonImages = GenButtonsFromName("icon");
	public static readonly List<List<Texture2D>> MenuButtonImages = [
		GenButtonsFromName("menu_left"),
		GenButtonsFromName("menu_middle"),
		GenButtonsFromName("menu_right"),
	];
	public static readonly List<List<Texture2D>> ProfileButtonImages = [
		GenButtonsFromName("profile_left"),
		GenButtonsFromName("profile_middle"),
		GenButtonsFromName("profile_right"),
	];
	public static readonly List<List<Texture2D>> Ladders = [
		GenButtonsFromName("ladder_top"),
		GenButtonsFromName("ladder_middle"),
		GenButtonsFromName("ladder_bottom")
	];
	public static readonly List<List<Texture2D>> IconTabs = GenIconTabs();

	public static NPatchInfo GenerateNPatchInfoFromButton(Texture2D button)
	{
		return MainMenuButtonImages.Contains(button)
			? new NPatchInfo
			{
				source = new Rectangle(0, 0, button.width, button.height),
				left = button.width / 2,
				right = button.width / 2,
				layout = (int)NPATCH_NINE_PATCH
			}
			: new NPatchInfo
			{
				source = new Rectangle(0, 0, button.width, button.height),
				left = button.width / 3,
				right = button.width / 3,
				top = button.height / 3,
				bottom = button.height / 3,
				layout = (int)NPATCH_NINE_PATCH
			};
	}

	public static List<Texture2D> GetButtonImagesFromStyle(ButtonStyle style)
	{
		return style switch
		{
			ButtonStyle.MainMenu => MainMenuButtonImages,
			ButtonStyle.Squoval => SquircleButtonImages,
			ButtonStyle.MenuLeft => MenuButtonImages[0],
			ButtonStyle.MenuMiddle => MenuButtonImages[1],
			ButtonStyle.MenuRight => MenuButtonImages[2],
			ButtonStyle.ProfileLeft => ProfileButtonImages[0],
			ButtonStyle.ProfileMiddle => ProfileButtonImages[1],
			ButtonStyle.ProfileRight => ProfileButtonImages[2],
			ButtonStyle.Dropdown => DropdownButtonImages,
			ButtonStyle.RoundedRect => RoundedRectButtonImages,
			ButtonStyle.LadderTop => Ladders[0],
			ButtonStyle.LadderMiddle => Ladders[1],
			ButtonStyle.LadderBottom => Ladders[2],
			ButtonStyle.Icon => IconButtonImages,
			ButtonStyle.IconTabTop => IconTabs[0],
			ButtonStyle.IconTabLeft => IconTabs[1],
			ButtonStyle.IconTabBottom => IconTabs[2],
			ButtonStyle.IconTabRight => IconTabs[3],
			_ => SquircleButtonImages,
		};
	}

	public static List<Texture2D> GetButtonImagesFromID(ButtonID style)
	{
		return style switch
		{
			ButtonID.ToggleFullscreen => ToggleFullscreenButtonImages,
			ButtonID.TwitterButton => TwitterButtonImages,
			ButtonID.TumblrButton => TumblrButtonImages,
			ButtonID.DiscordButton => DiscordButtonImages,
			ButtonID.EnglishLadder => EnglishLadderImages,
			ButtonID.NineLivesButton => GrantLivesButton,
			ButtonID.SupportLeaderButton => SupportLeaderButton,
			ButtonID.AidClanButton => AidClanButton,
			ButtonID.PlainsBiomeButton => PlainsBiomeButton,
			ButtonID.ForestBiomeButton => ForestBiomeButton,
			ButtonID.BeachBiomeButton => BeachBiomeButton,
			ButtonID.MountainBiomeButton => MountainBiomeButton,
			ButtonID.ClassicTab => ClassicTabButton,
			ButtonID.GullyTab => GullyTabButton,
			ButtonID.GrottoTab => GrottoTabButton,
			ButtonID.LakesideTab => LakesideTabButton,
			ButtonID.CliffTab => CliffTabButton,
			ButtonID.CaveTab => CaveTabButton,
			ButtonID.CrystalTab => CrystalTabButton,
			ButtonID.RuinsTab => RuinsTabButton,
			ButtonID.GrasslandsTab => GrasslandsTabButton,
			ButtonID.TunnelTab => TunnelTabButton,
			ButtonID.WastelandsTab => WastelandsTabButton,
			ButtonID.TidepoolTab => TidepoolTabButton,
			ButtonID.TidalCaveTab => TidalCaveTabButton,
			ButtonID.ShipwreckTab => ShipwreckTabButton,
			ButtonID.FiltersTab => FiltersTabButton,
			ButtonID.SymbolsSelect => SymbolSelectButton,
			_ => SquircleButtonImages,
		};
	}

	public static List<Texture2D> GenButtonsFromName(string name)
	{
		Texture2D loadedNormal = LoadTexture($".\\Resources\\Buttons\\{name}.png");
		Texture2D loadedHover = LoadTexture($".\\Resources\\Buttons\\{name}_hovered.png");
		Texture2D loadedDisabled = LoadTexture($".\\Resources\\Buttons\\{name}_disabled.png");
		return [
			loadedNormal,
			loadedHover,
			loadedHover,
			loadedDisabled
		];
	}

	private static List<List<Texture2D>> GenIconTabs()
	{
		Image allTabs = LoadImage(".\\Resources\\Buttons\\icon_tab.png");

		Dictionary<string, List<Image>> nTabs = [];

		int tabSize = allTabs.width / 3;
		string[] tabs = ["icon_tab_top", "icon_tab_left", "icon_tab_bottom", "icon_tab_right"];
		string[] states = ["normal", "hovered", "disabled"];
		for (int y = 0; y < tabs.Length; y++)
		{
			nTabs.Add(tabs[y], []);
			for (int x = 0; x < states.Length; x++)
			{
				Image tabOfState = ImageFromImage(allTabs, new Rectangle(x*tabSize, y*tabSize, tabSize, tabSize));
				nTabs[tabs[y]].Add(tabOfState);
			}
		}

		List<List<Texture2D>> finalTabs = [];

		foreach (var tibby in nTabs)
		{
			List<Texture2D> textures = [];
			foreach (var t in tibby.Value)
			{
				textures.Add(LoadTextureFromImage(t));
				UnloadImage(t);
			}
			textures.Add(textures.Last());
			finalTabs.Add(textures);
		}

		return finalTabs;
	}

	private static unsafe void SetFontFilters()
	{
		fixed (Texture2D* clanGenTex = &Clangen.texture) { GenTextureMipmaps(clanGenTex); }
		/*fixed (Texture2D* notoSansReg = &NotoSansRegular.texture) { GenTextureMipmaps(notoSansReg); }
		fixed (Texture2D* notoSansMed = &NotoSansMedium.texture) { GenTextureMipmaps(notoSansMed); }*/
		//SetTextureFilter(Clangen.texture, TEXTURE_FILTER_POINT);
		SetTextureFilter(NotoSansRegular.texture, TEXTURE_FILTER_BILINEAR);
		SetTextureFilter(NotoSansMedium.texture, TEXTURE_FILTER_BILINEAR);
	}

	public unsafe static void LoadResources()
	{
		int[] codepointsInt = Enumerable.Range(0x0020, 0x00A0)
			.Concat([
				0x2026, 0x2190, 0x2192, 0x2302,
				0x23E9, 0x23EA, 0x250F, 0x2600,
				0x2684, 0x26EA, 0x2744, 0x2B05, 
				0x2B95, 0x2513,
				0x1F33F, 0x1F342, 0x1F3DA, 0x1F3F0,
				0x1F401, 0x1F431, 0x1F43E, 0x1F485,
				0x1F4A7, 0x1F507, 0x1F50A, 0x1F50D,
				0x1F5C9, 0x1F89C, 0x1F89E, 0x1FAB4
			])
			.ToArray();
		fixed (int* codepoints = codepointsInt)
		{
			//we don't have 256 characters, but alas, it wouldn't load with any number lower.
			Clangen = LoadFontEx(".\\Resources\\Font\\clangen.ttf", 32, codepoints, 256); 
			//Sadly, I can't do anything about... whatever is happening with unicode characters.
		}
		SetFontFilters();
		LightModeColour = new(
			game.Config.Theme.LightModeBackground[0],
			game.Config.Theme.LightModeBackground[1],
			game.Config.Theme.LightModeBackground[2],
			255
		);
		DarkModeColour = new(
			game.Config.Theme.DarkModeBackground[0],
			game.Config.Theme.DarkModeBackground[1],
			game.Config.Theme.DarkModeBackground[2],
			255
		);
	}

	public static void UnloadTextures()
	{
		UnloadTexture(MenuImage);
		UnloadTexture(GameModeTextBox);
	}
}
