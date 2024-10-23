﻿using ClanGenDotNet.Scripts.Cats;
using System.Runtime.InteropServices;
using System.Text;

namespace ClanGenDotNet.Scripts.UI;

//This class should inherit from UIButton, however, UIButton uses Npatching for all buttons so we
//get wonky looking cats as a result. So I just put the UIButton code in here, I'll figure out a 
//better solution later.
public class UICatButton : UIElement, IUIElement, IUIClickable
{
	private readonly Cat _cat;

	public Cat GetCat()
	{
		return _cat;
	}

	private Texture2D _currentTexture;
	private Rectangle _imageRect;

	//Button Textures and Pressed State
	private Texture2D _normal;
	private Texture2D _hover;
	private Texture2D _selected;
	private Texture2D _disabled;
	private bool _pressed;

	/// <summary>
	/// Creates a UIButton with only an image.
	/// </summary>
	/// <param name="posScale">A Rectangle of position and size.</param>
	/// <param name="sprite">A Texture2D, the only sprite of the button.</param>
	/// <param name="buttonCat">A Cat object that is associated with the button.</param>
	/// <param name="manager">The UIManager, preferably game.Manager</param>
	public unsafe UICatButton(ClanGenRect posScale, Texture2D sprite, Cat buttonCat, UIManager manager) : base(posScale, manager)
	{
		_normal = sprite;
		_hover = sprite;
		_selected = sprite;
		_disabled = sprite;
		_currentTexture = _normal;
		_cat = buttonCat;
		_imageRect = new(0, 0, _currentTexture.width, _currentTexture.height);
	}

	public override unsafe void Update()
	{
		base.Update();
		DrawTexturePro(
			_currentTexture, 
			_imageRect, 
			RelativeRect, 
			new Vector2(0, 0), 
			0, 
			WHITE
		);
	}

	public void ChangeTexture()
	{
		_currentTexture = !Active ? _disabled : Hovered ? _hover : _pressed ? _selected : _normal;
	}

	public void HandleElementInteraction()
	{
		if (!Active) { return; }
		if (Hovered && IsMouseButtonDown(MOUSE_BUTTON_LEFT))
		{
			_manager.PushEvent(new(this, Events.EventType.LeftMouseDown));
			_pressed = true;
		}
		else if (Hovered && _pressed && IsMouseButtonUp(MOUSE_BUTTON_LEFT))
		{
			_manager.PushEvent(new(this, Events.EventType.LeftMouseClick));
			_pressed = false;
		}
	}
}
