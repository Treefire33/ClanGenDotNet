using ClanGenDotNet.Scripts.Events;
using System.Runtime.InteropServices;
using System.Text;
using static ClanGenDotNet.Scripts.Resources;

namespace ClanGenDotNet.Scripts.UI;

public partial class UIButton : UIElement, IUIClickable, IUIElement
{
	[MarshalAs(UnmanagedType.LPUTF8Str)] private string _text;
	private readonly int _fontSize = 30;

	private Texture2D _currentTexture;
	private NPatchInfo _currentNPatch;

	//Button Textures and Pressed State
	private Texture2D _normal;
	private Texture2D _hover;
	private Texture2D _selected;
	private Texture2D _disabled;
	private bool _pressed;

	private Vector2 _textSize = new(0);
	private Vector2 _textPosition = new(0);

	/// <summary>
	/// Creates a UIButton.
	/// </summary>
	/// <param name="posScale">A Rectangle of position and size.</param>
	/// <param name="style">A ButtonStyle, which corresponds to a series of four images.</param>
	/// <param name="text">The text the button will display.</param>
	/// <param name="manager">The UIManager, preferably game.Manager</param>
	public unsafe UIButton(ClanGenRect posScale, ButtonStyle style, string text, int fontSize, UIManager manager) 
		: base(posScale, manager)
	{
		_text = text;
		List<Texture2D> images = GetButtonImagesFromStyle(style);
		_normal = images[0];
		_hover = images[1];
		_selected = images[2];
		_disabled = images[3];
		_currentTexture = _normal;
		_fontSize = fontSize;
		SetText(text);
	}

	/// <summary>
	/// Creates a UIButton.
	/// </summary>
	/// <param name="posScale">A Rectangle of position and size.</param>
	/// <param name="style">A ButtonID, which corresponds to a series of four images.</param>
	/// <param name="text">The text the button will display.</param>
	/// <param name="manager">The UIManager, preferably game.Manager</param>
	public unsafe UIButton(ClanGenRect posScale, ButtonID style, UIManager manager) 
		: base(posScale, manager)
	{
		_text = "";
		List<Texture2D> images = GetButtonImagesFromID(style);
		_normal = images[0];
		_hover = images[1];
		_selected = images[2];
		_disabled = images[3];
		_currentTexture = _normal;
		_fontSize = 0;
		SetText(_text);
	}

	/// <summary>
	/// Creates a UIButton with only an image.
	/// </summary>
	/// <param name="posScale">A Rectangle of position and size.</param>
	/// <param name="sprite">A Texture2D, the only sprite of the button.</param>
	/// <param name="manager">The UIManager, preferably game.Manager</param>
	public unsafe UIButton(ClanGenRect posScale, Texture2D sprite, UIManager manager) : base(posScale, manager)
	{
		_text = "";
		_normal = sprite;
		_hover = sprite;
		_selected = sprite;
		_disabled = sprite;
		_currentTexture = _normal;
		_fontSize = 0;
		SetText("");
	}

	/// <summary>
	/// Sets the text of the button.
	/// </summary>
	/// <param name="text">The text to set the button text to.</param>
	public unsafe void SetText(string text)
	{
		_text = text;
		_textSize = MeasureTextEx(Clangen, _text, _fontSize, 0);
		_textPosition = new Vector2(
			RelativeRect.Position.X
			+ (RelativeRect.Size.X / 2)
			- (_textSize.X / 2),
			RelativeRect.Position.Y
			+ (RelativeRect.Size.Y / 2)
			- (_textSize.Y / 2)
		);
	}

	[DllImport("raylib", CallingConvention = CallingConvention.Cdecl)]
	public unsafe static extern void DrawTextEx(
		Font font,
		[MarshalAs(UnmanagedType.LPUTF8Str)]string text,
		Vector2 position,
		float fontSize,
		float spacing,
		Color tint
	);

	public override unsafe void Update()
	{
		base.Update();

		DrawTextureNPatch(
			_currentTexture,
			_currentNPatch,
			RelativeRect,
			Vector2.Zero,
			0,
			WHITE
		);
		DrawTextEx(
			Clangen,
			_text,
			_textPosition,
			_fontSize,
			0,
			WHITE
		);
	}

	public void ChangeTexture()
	{
		_currentTexture = !Active ? _disabled : Hovered ? _hover : _pressed ? _selected : _normal;
		_currentNPatch = GenerateNPatchInfoFromButton(_currentTexture);
	}

	public void HandleElementInteraction()
	{
		if (!Active) { return; }

		if (Hovered)
		{
			Event newEvent = new(this, EventType.None);

			if (IsMouseButtonDown(MOUSE_BUTTON_LEFT))
			{
				newEvent.EventType = EventType.LeftMouseDown;
				_pressed = true;
			}
			else if (IsMouseButtonReleased(MOUSE_BUTTON_LEFT))
			{
				newEvent.EventType = _pressed ? EventType.LeftMouseClick : EventType.LeftMouseUp;
				_pressed = false;
			}
			else if (IsMouseButtonDown(MOUSE_BUTTON_RIGHT))
			{
				newEvent.EventType = EventType.RightMouseDown;
				_pressed = true;
			}
			else if (IsMouseButtonReleased(MOUSE_BUTTON_RIGHT))
			{
				newEvent.EventType = _pressed ? EventType.RightMouseClick : EventType.RightMouseUp;
				_pressed = false;
			}
			else
			{
				_pressed = false;
			}
			
			if (newEvent.EventType != EventType.None)
			{
				Manager.PushEvent(newEvent);
			}
		}
	}
}
