using ClanGenDotNet.Scripts.UI.Interfaces;
using System.Text.RegularExpressions;
using static ClanGenDotNet.Scripts.Resources;

namespace ClanGenDotNet.Scripts.UI;
public partial class UITextInput(ClanGenRect posScale, string defaultText, int maxCharacters)
	: UIElement(posScale),
	IUIClickable,
	IUIElement
{
	private string _text = defaultText;
	private readonly int _maxCharacters = maxCharacters;
	private int _currentCharacters = 0;

	public bool Focused = false;

	private readonly Regex _inputRegex = DefaultInputRegex();

	public override void Update()
	{
		base.Update();
		DrawRectangleRec(RelativeRect.RelativeRect, White);
		DrawTextPro(
			NotoSansMedium,
			_text,
			RelativeRect.Position,
			new Vector2(0),
			0,
			25,
			0,
			Black
		);

		if (Hovered && IsMouseButtonPressed(MouseButton.Left))
		{
			Focused = true;
		}
		else if (IsMouseButtonPressed(MouseButton.Right))
		{
			Focused = false;
		}

		if (Focused)
		{
			SetMouseCursor(IBeam);

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

			if (IsKeyDown(Backspace) && _currentCharacters >= 0)
			{
				_currentCharacters--;
				if (_currentCharacters < 0) { _currentCharacters = 0; }
				_text = _text.Remove(_currentCharacters);
			}
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

	private int _framesCount = 0;
	public void HandleElementInteraction()
	{
		if (Focused)
		{
			_framesCount++;
			if (_currentCharacters < _maxCharacters)
			{
				if (_framesCount / 20 % 2 == 0)
				{
					DrawTextPro(
						NotoSansMedium,
						"|",
						new Vector2(RelativeRect.X + MeasureTextEx(NotoSansMedium, _text, 25, 0).X, RelativeRect.Y),
						new Vector2(0),
						0,
						25,
						0,
						Black
					);
				}
			}
		}
	}

	[GeneratedRegex(@"[^A-Za-z0-9 ]+", RegexOptions.Multiline)]
	private static partial Regex DefaultInputRegex();
}
