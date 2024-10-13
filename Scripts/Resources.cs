using ClanGenDotNet.Scripts.UI;
using Raylib_cs;
using System.Numerics;
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
		public static readonly List<Texture2D> ToggleFullscreenButtonImages = 
			GenImageButtonsFromName("toggle_fullscreen");
		public static readonly List<Texture2D> TwitterButtonImages = GenImageButtonsFromName("twitter");
		public static readonly List<Texture2D> TumblrButtonImages = GenImageButtonsFromName("tumblr");
		public static readonly List<Texture2D> DiscordButtonImages = GenImageButtonsFromName("discord");
		public static readonly List<Texture2D> EnglishLadderImages = GenImageButtonsFromName("english");
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
			if (MainMenuButtonImages.Contains(button))
			{
				return new NPatchInfo
				{
					Source = new Rectangle(0, 0, new(button.Width, button.Height)),
					Left = button.Width / 2,
					Right = button.Width / 2,
					Layout = NPatchLayout.NinePatch
				};
			}
			return new NPatchInfo
			{
				Source = new Rectangle(0, 0, new(button.Width, button.Height)),
				Left = button.Width / 3,
				Right = button.Width /3,
				Top = button.Height / 3,
				Bottom = button.Height /3,
				Layout = NPatchLayout.NinePatch
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

		public unsafe static void SetFontFilters()
		{
			fixed (Texture2D* clanGenTex = &Clangen.Texture) { GenTextureMipmaps(clanGenTex); }
			fixed (Texture2D* notoSansReg = &NotoSansRegular.Texture) { GenTextureMipmaps(notoSansReg); }
			fixed (Texture2D* notoSansMed = &NotoSansMedium.Texture) { GenTextureMipmaps(notoSansMed); }
			//SetTextureFilter(Clangen.Texture, TextureFilter.Bilinear);
			//SetTextureFilter(NotoSansRegular.Texture, TextureFilter.Point);
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
