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
	public Rectangle RelativeRect
	{
		get => new(X, Y, Width, Height);
		set => this = new(X, Y, Width, Height);
	}
	public float X = position.X;
	public float Y = position.Y;
	public float Width = scale.X;
	public float Height = scale.Y;

	public Vector2 Position
	{
		get => new(X, Y);

		set
		{
			X = value.X;
			Y = value.Y;
		}
	}
	public Vector2 Size
	{
		get => new(Width, Height);

		set
		{
			Width = value.X;
			Height = value.Y;
		}
	}

	//Points in RelativeRect
	public Vector2 TopLeft
	{
		get { return new(X, Y); }
	}
	public Vector2 TopCenter
	{
		get { return new(X / 2 + Width / 2, Y); }
	}
	public Vector2 TopRight
	{
		get { return new(X + Width, Y); }
	}

	public Vector2 CenterLeft
	{
		get { return new(X, Y / 2 + Height / 2); }
	}
	public Vector2 Center
	{
		get { return (Position / 2) + (Size / 2); }
	}
	public Vector2 CenterRight
	{
		get { return new(X + Width, Y / 2 + Height / 2); }
	}

	public Vector2 BottomLeft
	{
		get { return new(X, Y + Height); }
		set
		{
			X = value.X;
			Y = value.Y - Height;
		}
	}
	public Vector2 BottomCenter
	{
		get { return new(X / 2 + Width / 2, Y + Height); }
	}
	public Vector2 BottomRight
	{
		get { return Position + Size; }
		set
		{
			X = value.X - Width;
			Y = value.Y - Height;
		}
	}

	/*//Conversion Function
	public Rectangle AsRaylibRect()
	{
		return RelativeRect;
	}*/
	//// Replaced by implicit conversion.

	public override string ToString()
	{
		return $"ClanGenRect: <{Position}>, <{Size}>";
	}

	public readonly ClanGenRect AnchorTo(AnchorPosition anchor, ClanGenRect anchorRect = new())
	{
		ClanGenRect newRect = new();
		switch (anchor)
		{
			case AnchorPosition.TopTarget:
				newRect.X = X;
				newRect.Y = Y + anchorRect.Y + anchorRect.Height;
				break;
			case AnchorPosition.BottomTarget:
				newRect = this;
				newRect.BottomRight = anchorRect.Position;
				break;
			case AnchorPosition.LeftTarget:
				newRect.X = X + anchorRect.X + anchorRect.Width;
				newRect.Y = Y;
				break;
			case AnchorPosition.CenterX:
				newRect.X = (ScreenSettings.GameScreenSize.X / 2) - (Width / 2);
				newRect.Y = Y;
				break;
			case AnchorPosition.Center:
				newRect.X = (ScreenSettings.GameScreenSize.X / 2) - (Width / 2) + newRect.X;
				newRect.Y = (ScreenSettings.GameScreenSize.Y / 2) - (Height / 2) + newRect.Y;
				break;
			case AnchorPosition.RightTarget:
			case AnchorPosition.Top:
			case AnchorPosition.Bottom:
			case AnchorPosition.Left:
			case AnchorPosition.Right:
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(anchor), anchor, null);
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

	public static ClanGenRect FromTopRight(float x, float y, float width, float height)
	{
		return new ClanGenRect(ScreenSettings.ScreenX + x, y, width, height);
	}

	public static ClanGenRect FromTopRight(float x, float y, Vector2 size)
	{ return FromTopRight(x - size.X, y, size.X, size.Y); }

	public static ClanGenRect FromTopRight(Vector2 pos, Vector2 size)
	{ return FromTopRight(pos.X - size.X, pos.Y, size.X, size.Y); }

	public static implicit operator Rectangle(ClanGenRect rect)
	{
		return rect.RelativeRect;
	}
}

public enum AnchorPosition
{
	TopTarget,
	BottomTarget,
	LeftTarget,
	RightTarget,
	Top,
	Bottom,
	Left,
	Right,
	CenterX,
	Center
}
