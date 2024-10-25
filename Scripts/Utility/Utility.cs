using ClanGenDotNet.Scripts.Cats;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace ClanGenDotNet.Scripts;

public partial class Utility
{
	public static Rectangle AddRectangles(Rectangle rect1, Rectangle rect2)
	{
		return new Rectangle(
			rect1.X + rect2.X,
			rect1.Y + rect2.Y,
			rect1.width + rect2.width,
			rect1.height + rect2.height
		);
	}

	public readonly static Random Rand = new(10538);

	public static int GetRandBits(int size)
	{
		if (size < 4) { size = 4; }
		var tempBytes = new byte[size];
		Rand.NextBytes(tempBytes);
		return BitConverter.ToInt32(tempBytes, 0);
	}

	public static bool PointInArea(Vector2 point, Vector2 topLeft, Vector2 bottomRight)
	{
		return point.X >= topLeft.X
			&& point.X <= bottomRight.X
			&& point.Y >= topLeft.Y
			&& point.Y <= bottomRight.Y;
	}

	public static bool PointInArea(Vector2 point, ClanGenRect area)
	{
		return point.X >= area.TopLeft.X
			&& point.X <= area.BottomRight.X
			&& point.Y >= area.TopLeft.Y
			&& point.Y <= area.BottomRight.Y;
	}
}
