namespace ClanGenDotNet.Scripts.UI;

public class UIAutoResizableContainer(ClanGenRect posScale, UIManager manager) 
	: UIContainer(posScale, manager),
	IUIElement,
	IUIContainer
{
	private ClanGenRect _originalSize = posScale;
	private void ResizeContainer()
	{
		RelativeRect = _originalSize;
		foreach (var element in ContainedElements)
		{
			//RelativeRect.X = Math.Max(element.RelativeRect.X, RelativeRect.X);
			//RelativeRect.Y = Math.Max(element.RelativeRect.Y, RelativeRect.Y);
			RelativeRect.Width = Math.Max(element.RelativeRect.BottomRight.X, RelativeRect.Width);
			RelativeRect.Height = Math.Max(element.RelativeRect.BottomRight.Y, RelativeRect.Height);
		}
	}

	//Create new AddElement functions to resize container whenever
	new public void AddElement(UIElement element)
	{
		element.Layer += Layer;
		ContainedElements.Add(element);
		if (!Active)
		{
			element.SetActive(false);
		}
		if (!Visible)
		{
			element.Visible = false;
		}
		element.IsContained = true;
		ResizeContainer();
	}

	new public void AddElement(UIElement element, bool modPosition)
	{
		AddElement(element);
		if (modPosition) { element.RelativeRect.Position += RelativeRect.Position; }
		ResizeContainer();
	}
}
