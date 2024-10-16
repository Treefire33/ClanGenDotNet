﻿using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace ClanGenDotNet.Scripts.UI;

public class UIImage(ClanGenRect posScale, Texture2D image, UIManager manager) 
	: UIElement(posScale, manager), 
	IUIElement
{
	public Texture2D Image = image;
	private ClanGenRect _imageRect = new(0, 0, new Vector2(image.Width, image.Height));

	public override void Update()
	{
		//DrawTexture(Image, _imageRect, RelativeRect.Position, Color.White);
		DrawTexturePro(Image, _imageRect.RelativeRect, RelativeRect.RelativeRect, new Vector2(0, 0), 0, Color.White);
		base.Update();
	}
}
