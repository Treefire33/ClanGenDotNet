using ClanGenDotNet.Scripts.UI;
using static ClanGenDotNet.Scripts.Game_Structure.Game;

namespace ClanGenDotNet.Scripts;

public static class Resources
{
	//Theme Colours
	public static Color LightModeColour;
	public static Color DarkModeColour;

	//Fonts
	public unsafe static readonly Font Clangen = LoadFont(".\\Resources\\Font\\clangen.ttf");
	public unsafe static readonly Font NotoSansRegular = LoadFont(".\\Resources\\Font\\NotoSans-Regular.ttf");
	public unsafe static readonly Font NotoSansMedium = LoadFont(".\\Resources\\Font\\NotoSans-Medium.ttf");

	//Main Menu
	public static readonly Texture2D MenuImage = LoadTexture(".\\Resources\\Images\\menu.png");

	//Clan Creation
	public static readonly Texture2D ClanFrameImage = LoadTexture(".\\Resources\\PickClanScreen\\clan_name_frame.png");
	public static readonly Texture2D NameClanImage = LoadTexture(".\\Resources\\PickClanScreen\\name_clan_light.png");
	public static readonly Texture2D GameModeTextBox = LoadTexture(".\\Resources\\Images\\game_mode_text_box.png");

	//Image Buttons
	public static readonly List<Texture2D> ToggleFullscreenButtonImages =
		GenImageButtonsFromName("toggle_fullscreen");
	public static readonly List<Texture2D> TwitterButtonImages = GenImageButtonsFromName("twitter");
	public static readonly List<Texture2D> TumblrButtonImages = GenImageButtonsFromName("tumblr");
	public static readonly List<Texture2D> DiscordButtonImages = GenImageButtonsFromName("discord");
	public static readonly List<Texture2D> EnglishLadderImages = GenImageButtonsFromName("english");

	public static readonly List<Texture2D> GrantLivesButton = GenImageButtonsFromName("grant_lives");

	public static readonly List<List<Texture2D>> CheckboxImages = [
		GenImageButtonsFromName("checkmark_off"),
		GenImageButtonsFromName("checkmark_on"),
	];

	//Buttons
	public static readonly List<Texture2D> MainMenuButtonImages = GenButtonsFromName("mainmenu");
	public static readonly List<Texture2D> SquircleButtonImages = GenButtonsFromName("general");
	public static readonly List<List<Texture2D>> MenuButtonImages = [
		GenButtonsFromName("menu_left"),
		GenButtonsFromName("menu_middle"),
		GenButtonsFromName("menu_right"),
	];
	public static readonly List<List<Texture2D>> Ladders = [
		GenButtonsFromName("ladder_top"),
		GenButtonsFromName("ladder_middle"),
		GenButtonsFromName("ladder_bottom")
	];

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
			ButtonStyle.LadderTop => Ladders[0],
			ButtonStyle.LadderMiddle => Ladders[1],
			ButtonStyle.LadderBottom => Ladders[2],
			_ => MainMenuButtonImages,
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
			_ => SquircleButtonImages,
		};
	}

	public static List<Texture2D> GenImageButtonsFromName(string name)
	{
		return [
			LoadTexture($".\\Resources\\Buttons\\{name}.png"),
			LoadTexture($".\\Resources\\Buttons\\{name}_hovered.png"),
			LoadTexture($".\\Resources\\Buttons\\{name}_hovered.png"),
			LoadTexture($".\\Resources\\Buttons\\{name}_disabled.png")
		];
	}

	public static List<Texture2D> GenButtonsFromName(string name)
	{
		return [
			LoadTexture($".\\Resources\\Buttons\\{name}_normal.png"),
			LoadTexture($".\\Resources\\Buttons\\{name}_hovered.png"),
			LoadTexture($".\\Resources\\Buttons\\{name}_hovered.png"),
			LoadTexture($".\\Resources\\Buttons\\{name}_disabled.png")
		];
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

	public static void LoadResources()
	{
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

	public static Font ConvertFontNameToFont(string fontName)
	{
		return fontName switch
		{
			"clangen" => Clangen,
			"notosans" => NotoSansRegular,
			_ => NotoSansRegular
		};
	}

	public static void UnloadTextures()
	{
		UnloadTexture(MenuImage);
		UnloadTexture(ClanFrameImage);
		UnloadTexture(NameClanImage);
		UnloadTexture(GameModeTextBox);
	}
}
