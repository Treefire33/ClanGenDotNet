using ClanGenDotNet.Scripts.UI.Interfaces;
using static ClanGenDotNet.Scripts.Resources;

namespace ClanGenDotNet.Scripts.UI;

public class UICheckbox : UIElement, IUIClickable, IUIElement
{
	private string _label;
	private readonly int _fontSize = 20;

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
			new Rectangle(0, 0, _currentTexture.width, _currentTexture.height),
			RelativeRect.RelativeRect,
			new Vector2(0, 0),
			0,
			WHITE
		);
		DrawTextPro(
			NotoSansMedium,
			_label,
			RelativeRect.Position + new Vector2(RelativeRect.Width, RelativeRect.Height / 4),
			new Vector2(0, 0),
			0,
			_fontSize,
			0,
			WHITE
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

		_currentTexture = Hovered
			? CheckboxImages[Convert.ToInt32(Checked)][1]
			: _pressed ? CheckboxImages[Convert.ToInt32(Checked)][2] : CheckboxImages[Convert.ToInt32(Checked)][0];
	}

	public void HandleElementInteraction()
	{
		if (!Active) { return; }
		if (Hovered && IsMouseButtonDown(MOUSE_BUTTON_LEFT))
		{
			Manager.PushEvent(new(this, Events.EventType.LeftMouseDown));
			_pressed = true;
		}
		else if (Hovered && _pressed && IsMouseButtonUp(MOUSE_BUTTON_LEFT))
		{
			_pressed = false;
			Manager.PushEvent(new(this, Events.EventType.LeftMouseClick));
			Checked = !Checked;
		}
	}

	public new void Kill()
	{
		base.Kill();
		if (_tooltip != null)
		{
			_tooltip.Kill();
			_tooltip = null;
		}
	}
}
