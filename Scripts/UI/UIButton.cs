using Raylib_cs;
using System.Numerics;
using System.Text;
using static ClanGenDotNet.Scripts.Resources;
using static Raylib_cs.Raylib;

namespace ClanGenDotNet.Scripts.UI;

public enum ButtonStyle
{
	MainMenu,
	Squoval,
	MenuLeft,
	MenuMiddle,
	MenuRight,
	LadderTop,
	LadderMiddle,
	LadderBottom
}
public enum ButtonID
{
	ToggleFullscreen,
	TwitterButton,
	TumblrButton,
	DiscordButton,
	EnglishLadder //don't know, don't care to know
}
public class UIButton : UIElement, IUIClickable, IUIElement
{
	/*Why does UIButton inherit from UIImage?
		My reasoning is that all buttons in clangen are images.*/

	//Why raylib, why? Why must you make me use unsafe?
	//man, I just can't anymore, everything is making want to quit, but alas I must perservere.
	//to the above message, it was mostly just fonts and text, everything else worked first try

	//private NPatchInfo patchInfo = new();
	//private string Text = "";
	private unsafe sbyte* _text;
	private int _fontSize = 30;

	private Texture2D _currentTexture;
	private NPatchInfo _currentNPatch;
	private Rectangle _imageRect;

	//Button Textures and Pressed State
	private Texture2D _normal;
	private Texture2D _hover;
	private Texture2D _selected;
	private Texture2D _disabled;
	private bool _pressed;

	/// <summary>
	/// Creates a UIButton.
	/// </summary>
	/// <param name="posScale">A Rectangle of position and size.</param>
	/// <param name="style">A ButtonStyle, which corresponds to a series of four images.</param>
	/// <param name="text">The text the button will display.</param>
	/// <param name="manager">The UIManager, preferably game.Manager</param>
	public unsafe UIButton(ClanGenRect posScale, ButtonStyle style, string text, int fontSize, UIManager manager) : base(posScale, manager)
	{
		_text = StringToSBytes(text);
		List<Texture2D> images = GetButtonImagesFromStyle(style);
		_normal = images[0];
		_hover = images[1];
		_selected = images[2];
		_disabled = images[3];
		_currentTexture = _normal;
		_imageRect = new(0, 0, new Vector2(_currentTexture.Width, _currentTexture.Height));
		/*if(fontSize == -1)
		{
			_fontSize = CalculateFontSize();
		}
		else
		{
			_fontSize = fontSize;
		}*/
		_fontSize = fontSize;
	}

	/// <summary>
	/// Creates a UIButton.
	/// </summary>
	/// <param name="posScale">A Rectangle of position and size.</param>
	/// <param name="style">A ButtonID, which corresponds to a series of four images.</param>
	/// <param name="text">The text the button will display.</param>
	/// <param name="manager">The UIManager, preferably game.Manager</param>
	public unsafe UIButton(ClanGenRect posScale, ButtonID style, string text, int fontSize, UIManager manager) : base(posScale, manager)
	{
		_text = StringToSBytes(text);
		List<Texture2D> images = GetButtonImagesFromID(style);
		_normal = images[0];
		_hover = images[1];
		_selected = images[2];
		_disabled = images[3];
		_currentTexture = _normal;
		_imageRect = new(0, 0, new Vector2(_currentTexture.Width, _currentTexture.Height));
		_fontSize = fontSize;
	}

	/*private unsafe int CalculateFontSize()
	{
		for (int i = 0; i < 100; i++)
		{
			Vector2 textSize = MeasureTextEx(Clangen, _text, i, 2);
			if((textSize - RelativeRect.RelativeRect.Size).Length() < RelativeRect.Height)
			{
				return i;
			}
		}
		return 30;
	}*/

	/// <summary>
	/// Converts a string into a signed bytes pointer.
	/// </summary>
	/// <param name="Text">The text to convert</param>
	/// <returns>sbyte*</returns>
	private unsafe sbyte* StringToSBytes(string Text)
	{
		fixed (byte* p = Encoding.ASCII.GetBytes(Text))
		{
			return (sbyte*)p;
		}
	}

	/// <summary>
	/// Sets the text of the button.
	/// </summary>
	/// <param name="text">The text to set the button text to.</param>
	public unsafe void SetText(string text)
	{
		_text = StringToSBytes(text);
	}

	public override unsafe void Update()
	{
		Vector2 textSize = MeasureTextEx(Clangen, _text, _fontSize, 2);
		base.Update();
		//DrawTexturePro(_currentTexture, _imageRect, RelativeRect.RelativeRect, new Vector2(0, 0), 0, Color.White);
		DrawTextureNPatch(
			_currentTexture,
			_currentNPatch,
			RelativeRect.RelativeRect, 
			new Vector2(0, 0), 
			0, 
			Color.White
		);
		DrawTextPro(
			Clangen,
			_text,
			new Vector2(
				RelativeRect.RelativeRect.Position.X 
				+ (RelativeRect.RelativeRect.Size.X / 2) 
				- (textSize.X / 2), 
				RelativeRect.RelativeRect.Position.Y 
				+ (RelativeRect.RelativeRect.Size.Y / 2) 
				- (textSize.Y / 2)
			),
			new Vector2(0f, 0f),
			0,
			_fontSize,
			2,
			Color.White
		);
	}

	public void ChangeTexture()
	{
		if (!Active)
		{
			_currentTexture = _disabled;
		}
		else if (Hovered)
		{
			_currentTexture = _hover;
		}
		else if (_pressed)
		{
			_currentTexture = _selected;
		}
		else
		{
			_currentTexture = _normal;
		}
		_currentNPatch = GenerateNPatchInfoFromButton(_currentTexture);
	}

	public void HandleElementInteraction()
	{
		if (!Active) { return; }
		if (Hovered && IsMouseButtonDown(MouseButton.Left))
		{
			_manager.PushEvent(new(this, Events.EventType.LeftMouseDown));
			_pressed = true;
		}
		else if (Hovered && _pressed && IsMouseButtonUp(MouseButton.Left))
		{
			_manager.PushEvent(new(this, Events.EventType.LeftMouseClick));
			_pressed = false;
		}
	}
}
