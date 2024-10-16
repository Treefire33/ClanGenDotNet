﻿using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
using static ClanGenDotNet.Scripts.Resources;

namespace ClanGenDotNet.Scripts.UI;

public class UICheckbox: UIElement, IUIClickable, IUIElement
{
	private string _label;
	private int _fontSize = 20;

	public bool Checked = false;

	private bool _pressed = false;
	private Texture2D _currentTexture;

	private UITooltip? _tooltip = null;

	public UICheckbox(ClanGenRect posScale, string label, UIManager manager, string? tooltip = null)
		: base(posScale, manager)
	{
		_label = label;
		if (tooltip != null)
		{
			_tooltip = new UITooltip(
				tooltip,
				this,
				manager
			);
		}
	}

	public override void Update()
	{
		base.Update();
		DrawTexturePro(
			_currentTexture,
			new Rectangle(0, 0, new Vector2(_currentTexture.Width, _currentTexture.Height)),
			RelativeRect.RelativeRect,
			new (0, 0),
			0,
			Color.White
		);
		DrawTextPro(
			NotoSansMedium,
			_label,
			RelativeRect.RelativeRect.Position + new Vector2(RelativeRect.Width, RelativeRect.Height/4),
			new(0, 0),
			0,
			_fontSize,
			0,
			Color.White
		);
	}

	public void SetText(string text)
	{
		_label = text;
	}

	public bool GetChecked()
	{
		return Checked;
	}

	public void ChangeTexture()
	{
		if (!Active)
		{
			_currentTexture = CheckboxImages[Convert.ToInt32(Checked)][3];
			return;
		}

		if (Hovered)
		{
			_currentTexture = CheckboxImages[Convert.ToInt32(Checked)][1];
		}
		else if (_pressed)
		{
			_currentTexture = CheckboxImages[Convert.ToInt32(Checked)][2];
		}
		else
		{
			_currentTexture = CheckboxImages[Convert.ToInt32(Checked)][0];
		}
	}

	public void HandleElementInteraction()
	{
		if(!Active) { return; }
		if (Hovered && IsMouseButtonDown(MouseButton.Left))
		{
			_manager.PushEvent(new(this, Events.EventType.LeftMouseDown));
			_pressed = true;
		}
		else if (Hovered && _pressed && IsMouseButtonUp(MouseButton.Left))
		{
			_pressed = false;
			_manager.PushEvent(new(this, Events.EventType.LeftMouseClick));
			Checked = !Checked;
		}
	}

	new public void Kill()
	{
		base.Kill();
		if (_tooltip != null)
		{
			_tooltip.Kill();
			_tooltip = null;
		}
	}
}
