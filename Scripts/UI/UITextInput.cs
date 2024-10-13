using Raylib_cs;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using static ClanGenDotNet.Scripts.Resources;
using static Raylib_cs.Raylib;

namespace ClanGenDotNet.Scripts.UI;
public class UITextInput(ClanGenRect posScale, string defaultText, int maxCharacters, UIManager manager) 
	: UIElement(posScale, manager), 
	IUIClickable, 
	IUIElement
{
	private string _text = defaultText;
	private int _maxCharacters = maxCharacters;
	private int _currentCharacters = 0;

	public bool Focused = false;

	private Regex _inputRegex = new(@"[^A-Za-z0-9 ]+", RegexOptions.Multiline);

	public override void Update()
	{
		base.Update();
		DrawRectangleRec(RelativeRect.RelativeRect, Color.White);
		DrawTextPro(
			NotoSansMedium,
			_text,
			RelativeRect.RelativeRect.Position,
			new(0),
			0,
			25,
			0,
			Color.Black
		);
		if (Hovered && IsMouseButtonPressed(MouseButton.Left))
		{
			Focused = true;
		}
		else if (IsMouseButtonPressed(MouseButton.Left))
		{
			Focused = false;
		}
		if (Focused)
		{
			SetMouseCursor(MouseCursor.IBeam);

			int key = GetCharPressed();

			while (key > 0)
			{
				if (key >= 32 && key <= 125 && _currentCharacters <= _maxCharacters)
				{
					_text += (char)key;
					_currentCharacters++;
				}

				key = GetCharPressed();
			}

			if (IsKeyPressed(KeyboardKey.Backspace))
			{
				_currentCharacters--;
				if (_currentCharacters < 0) { _currentCharacters = 0; }
				this._text = this._text.Remove(_currentCharacters);
			}

			//_text = _inputRegex.Replace(_text, "");
		}
		else
		{
			SetMouseCursor(MouseCursor.Default);
		}
	}

	public void SetText(string text)
	{
		_text = text;
		_currentCharacters = _text.Length;
	}

	public string GetText()
	{
		return _text;
	}

	public void ChangeTexture() { } //UITextInput shouldn't change texture.

	int framesCount = 0;
	public void HandleElementInteraction()
	{
		if (Focused)
		{
			framesCount++;
			if (_currentCharacters < _maxCharacters)
			{
				if ((framesCount / 20) % 2 == 0)
				{
					DrawTextPro(
						NotoSansMedium,
						"|",
						new((int)RelativeRect.X + (MeasureTextEx(NotoSansMedium, _text, 25, 0).X), (int)RelativeRect.Y),
						new(0),
						0,
						25,
						0,
						Color.Black
					);
				}
			}
		}
	}
}