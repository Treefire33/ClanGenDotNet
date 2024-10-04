using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace ClanGenDotNet.Scripts.UI;
public class TextBoxStyle(int fontSize, TextAlignment alignment, Color colour)
{
	public int FontSize = fontSize;
	public TextAlignment TextAlignment = alignment;
	public Color Colour = colour;

	public static TextBoxStyle HorizLeft20Black = new(20, TextAlignment.Left, Color.Black);
	public static TextBoxStyle HorizCenter20Black = new(20, TextAlignment.Center, Color.Black);
	public static TextBoxStyle HorizLeft20White = new(20, TextAlignment.Left, Color.White);
	public static TextBoxStyle HorizCenter20White = new(20, TextAlignment.Center, Color.White);

	public static TextBoxStyle HorizLeft30Black = new(30, TextAlignment.Left, Color.Black);
	public static TextBoxStyle HorizCenter30Black = new(30, TextAlignment.Center, Color.Black);
	public static TextBoxStyle HorizLeft30White = new(30, TextAlignment.Left, Color.White);
	public static TextBoxStyle HorizCenter30White = new(30, TextAlignment.Center, Color.White);
}