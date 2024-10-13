using ClanGenDotNet.Scripts.Game_Structure;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenDotNet.Scripts;

/// <summary>
/// Creates a new ClanGenRect, which is similar to pygame's Rect.
/// Has 9 coordinates that correspond to the top, center and bottom left, center, and right.
/// </summary>
/// <param name="position">The x and y of the rect.</param>
/// <param name="scale">The width and height of the rect.</param>
public struct ClanGenRect(Vector2 position, Vector2 scale)
{
	public ClanGenRect(float x, float y, float width, float height) : this(new(x, y), new(width, height)) { }
	public ClanGenRect(Vector2 position, float width, float height) : this(position, new(width, height)) { }
	public ClanGenRect(float x, float y, Vector2 scale) : this(new(x, y), scale) { }

	//RelativeRect
	public Rectangle RelativeRect { 
		get { return new Rectangle(X, Y, Width, Height); }
		set { this = new(X, Y, Width, Height); } 
	}
	public float X = position.X;
	public float Y = position.Y;
	public float Width = scale.X;
	public float Height = scale.Y;

	public Vector2 Position
	{
		get
		{
			return new Vector2(X, Y);
		}

		set
		{
			X = value.X;
			Y = value.Y;
		}
	}
	public Vector2 Size
	{
		get
		{
			return new Vector2(X, Y);
		}

		set
		{
			Width = value.X;
			Height = value.Y;
		}
	}

	//Points in RelativeRect
	public Vector2 TopLeft = position;
	public Vector2 TopCenter = new(position.X + (scale.X / 2), position.Y);
	public Vector2 TopRight = new(position.X + scale.X, position.Y);

	public Vector2 CenterLeft = new(position.X, position.Y + (scale.Y / 2));
	public Vector2 Center = position + (scale / 2);
	public Vector2 CenterRight = new(position.X + scale.X, position.Y + (scale.Y / 2));

	public Vector2 BottomLeft = new(position.X, position.Y + scale.Y);
	public Vector2 BottomCenter = new(position.X + (scale.X / 2), position.Y + scale.Y);
	public Vector2 BottomRight = position + scale;

	//Conversion Function
	public Rectangle AsRaylibRect()
	{
		return RelativeRect;
	}
	
	public override string ToString()
	{
		return $"ClanGenRect: <{RelativeRect.X}, {RelativeRect.Y}>, <{RelativeRect.Width}, {RelativeRect.Height}>";
	}

	public ClanGenRect AnchorTo(AnchorPosition anchor, ClanGenRect anchorRect = new())
	{
		ClanGenRect newRect = new();
		switch (anchor)
		{
			case AnchorPosition.TopLeft:
				newRect.X = X;
				newRect.Y = Y + anchorRect.Y + anchorRect.Height;
				break;
			case AnchorPosition.LeftTarget:
				newRect.X = X + anchorRect.X + anchorRect.Width;
				newRect.Y = Y;
				break;
			case AnchorPosition.CenterX:
				newRect.X = (ScreenSettings.GameScreenSize.X/2)-(RelativeRect.Width/2);
				newRect.Y = Y;
				break;
		}
		newRect.Width = Width; 
		newRect.Height = Height;
		return newRect;
	}

	public readonly ClanGenRect Scale(float scaleFactor)
	{
		return new(
			X * scaleFactor,
			Y * scaleFactor,
			Width * scaleFactor,
			Height * scaleFactor
		);
	}

	public readonly ClanGenRect Scale(Vector2 scaleFactor)
	{
		return new(
			X * scaleFactor.X,
			Y * scaleFactor.Y,
			Width * scaleFactor.X,
			Height * scaleFactor.Y
		);
	}

	public readonly ClanGenRect Scale(ClanGenRect scaleRect)
	{
		return new(
			X * scaleRect.X,
			Y * scaleRect.Y,
			Width * scaleRect.Width,
			Height * scaleRect.Height
		);
	}
}

public enum AnchorPosition
{ 
	TopLeft,
	LeftTarget,
	CenterX
}
