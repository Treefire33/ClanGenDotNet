namespace ClanGenDotNet.Scripts.UI;
public class TextBoxStyle(int fontSize, TextAlignment alignment, Color colour)
{
	public int FontSize = fontSize;
	public TextAlignment TextAlignment = alignment;
	public Color Colour = colour;

	public static TextBoxStyle HorizLeft20Black = new(20, TextAlignment.Left, BLACK);
	public static TextBoxStyle HorizCenter20Black = new(20, TextAlignment.Center, BLACK);
	public static TextBoxStyle HorizLeft20White = new(20, TextAlignment.Left, WHITE);
	public static TextBoxStyle HorizCenter20White = new(20, TextAlignment.Center, WHITE);

	public static TextBoxStyle HorizLeft30Black = new(30, TextAlignment.Left, BLACK);
	public static TextBoxStyle HorizCenter30Black = new(30, TextAlignment.Center, BLACK);
	public static TextBoxStyle HorizLeft30White = new(30, TextAlignment.Left, WHITE);
	public static TextBoxStyle HorizCenter30White = new(30, TextAlignment.Center, WHITE);
}
