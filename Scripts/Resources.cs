using ClanGenDotNet.Scripts.UI;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace ClanGenDotNet.Scripts
{
	public static class Resources
	{
		//Fonts
		public static readonly Font Clangen = LoadFontEx(".\\Resources\\Font\\clangen.ttf", 100, null, 256);
		public static readonly Font NotoSansRegular = LoadFontEx(".\\Resources\\Font\\NotoSans-Regular.ttf", 100, null, 256);
		public static readonly Font NotoSansMedium = LoadFontEx(".\\Resources\\Font\\NotoSans-Medium.ttf", 100, null, 256);

		//Main Menu
		public static readonly Texture2D MenuImage = LoadTexture(".\\Resources\\Images\\menu.png");

		//Clan Creation
		public static readonly Texture2D ClanFrameImage = LoadTexture(".\\Resources\\PickClanScreen\\clan_name_frame.png");
		public static readonly Texture2D NameClanImage = LoadTexture(".\\Resources\\PickClanScreen\\name_clan_light.png");
		public static readonly Texture2D GameModeTextBox = LoadTexture(".\\Resources\\Images\\game_mode_text_box.png");

		//Image Buttons
		public static readonly List<Texture2D> ToggleFullscreenButtonImages = [
			LoadTexture(".\\Resources\\Buttons\\toggle_fullscreen.png"),
			LoadTexture(".\\Resources\\Buttons\\toggle_fullscreen_hover.png"),
			LoadTexture(".\\Resources\\Buttons\\toggle_fullscreen_hover.png"),
			LoadTexture(".\\Resources\\Buttons\\toggle_fullscreen_disabled.png")
		];

		//Buttons
		public static readonly List<Texture2D> MainMenuButtonImages = [
			LoadTexture(".\\Resources\\Buttons\\mainmenu_normal.png"),
			LoadTexture(".\\Resources\\Buttons\\mainmenu_hovered.png"),
			LoadTexture(".\\Resources\\Buttons\\mainmenu_hovered.png"),
			LoadTexture(".\\Resources\\Buttons\\mainmenu_disabled.png")
		];
		public static readonly List<Texture2D> SquircleButtonImages = [
			LoadTexture(".\\Resources\\Buttons\\general_normal.png"),
			LoadTexture(".\\Resources\\Buttons\\general_hovered.png"),
			LoadTexture(".\\Resources\\Buttons\\general_hovered.png"),
			LoadTexture(".\\Resources\\Buttons\\general_disabled.png")
		];
		public static readonly List<List<Texture2D>> CheckboxImages = [
			[
				LoadTexture(".\\Resources\\Buttons\\checkmark_off.png"),
				LoadTexture(".\\Resources\\Buttons\\checkmark_off_hovered.png"),
				LoadTexture(".\\Resources\\Buttons\\checkmark_off_hovered.png"),
				LoadTexture(".\\Resources\\Buttons\\checkmark_off_disabled.png")
			], 
			[
				LoadTexture(".\\Resources\\Buttons\\checkmark_on.png"),
				LoadTexture(".\\Resources\\Buttons\\checkmark_on_hovered.png"),
				LoadTexture(".\\Resources\\Buttons\\checkmark_on_hovered.png"),
				LoadTexture(".\\Resources\\Buttons\\checkmark_on_disabled.png")
			]
		];
		public static readonly List<List<Texture2D>> MenuButtonImages = [
			[
				LoadTexture(".\\Resources\\Buttons\\menu_left_normal.png"),
				LoadTexture(".\\Resources\\Buttons\\menu_left_hovered.png"),
				LoadTexture(".\\Resources\\Buttons\\menu_left_hovered.png"),
				LoadTexture(".\\Resources\\Buttons\\menu_left_disabled.png")
			],
			[
				LoadTexture(".\\Resources\\Buttons\\menu_middle_normal.png"),
				LoadTexture(".\\Resources\\Buttons\\menu_middle_hovered.png"),
				LoadTexture(".\\Resources\\Buttons\\menu_middle_hovered.png"),
				LoadTexture(".\\Resources\\Buttons\\menu_middle_disabled.png")
			],
			[
				LoadTexture(".\\Resources\\Buttons\\menu_right_normal.png"),
				LoadTexture(".\\Resources\\Buttons\\menu_right_hovered.png"),
				LoadTexture(".\\Resources\\Buttons\\menu_right_hovered.png"),
				LoadTexture(".\\Resources\\Buttons\\menu_right_disabled.png")
			]
		];

		public static NPatchInfo GenerateNPatchInfoFromButton(Texture2D button)
		{
			return new NPatchInfo
			{
				Source = new Rectangle(0, 0, new(button.Width, button.Height)),
				Left = button.Width / 3,
				Right = button.Width - (button.Width / 3)
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
				_ => MainMenuButtonImages,
			};
		}

		public static List<Texture2D> GetButtonImagesFromID(ButtonID style)
		{
			return style switch
			{
				ButtonID.ToggleFullscreen => ToggleFullscreenButtonImages,
				_ => SquircleButtonImages,
			};
		}

		public unsafe static void SetFontFilters()
		{
			fixed (Texture2D* clanGenTex = &Clangen.Texture) { GenTextureMipmaps(clanGenTex); }
			fixed (Texture2D* notoSansReg = &NotoSansRegular.Texture) { GenTextureMipmaps(notoSansReg); }
			fixed (Texture2D* notoSansMed = &NotoSansMedium.Texture) { GenTextureMipmaps(notoSansMed); }
			//SetTextureFilter(Clangen.Texture, TextureFilter.Bilinear);
			//SetTextureFilter(NotoSansRegular.Texture, TextureFilter.Bilinear);
			//SetTextureFilter(NotoSansMedium.Texture, TextureFilter.Bilinear);
		}

		public static void UnloadTextures()
		{
			UnloadTexture(MenuImage);
			UnloadTexture(ClanFrameImage);
			UnloadTexture(NameClanImage);
			UnloadTexture(GameModeTextBox);
		}

	}
}
