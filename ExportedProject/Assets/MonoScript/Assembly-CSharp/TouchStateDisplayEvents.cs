using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Daikon Forge/Examples/Touch/Display Events")]
public class TouchStateDisplayEvents : MonoBehaviour
{
	public dfLabel _label;

	private List<string> messages = new List<string>();

	public void Start()
	{
		if (_label == null)
		{
			_label = GetComponent<dfLabel>();
			_label.Text = "Touch State";
		}
	}

	public void OnDragDrop(dfControl control, dfDragEventArgs dragEvent)
	{
		string text = ((dragEvent.Data != null) ? dragEvent.Data.ToString() : "(null)");
		display("DragDrop: " + text);
		dragEvent.State = dfDragDropState.Dropped;
		dragEvent.Use();
	}

	public void OnEnterFocus(dfControl control, dfFocusEventArgs args)
	{
		display("EnterFocus");
	}

	public void OnLeaveFocus(dfControl control, dfFocusEventArgs args)
	{
		display("LeaveFocus");
	}

	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
	{
		display("Click");
	}

	public void OnDoubleClick(dfControl control, dfMouseEventArgs mouseEvent)
	{
		display("DoubleClick");
	}

	public void OnMouseDown(dfControl control, dfMouseEventArgs mouseEvent)
	{
		display("MouseDown");
	}

	public void OnMouseEnter(dfControl control, dfMouseEventArgs mouseEvent)
	{
		display("MouseEnter");
	}

	public void OnMouseLeave(dfControl control, dfMouseEventArgs mouseEvent)
	{
		display("MouseLeave");
	}

	public void OnMouseMove(dfControl control, dfMouseEventArgs mouseEvent)
	{
		display("MouseMove: " + screenToGUI(mouseEvent.Position));
	}

	public void OnMouseUp(dfControl control, dfMouseEventArgs mouseEvent)
	{
		display("MouseUp");
	}

	public void OnMultiTouch(dfControl control, dfTouchEventArgs touchData)
	{
		string text = "Multi-Touch:\n";
		for (int i = 0; i < touchData.Touches.Count; i++)
		{
			text += string.Format(arg1: screenToGUI(touchData.Touches[i].position), format: "\tFinger {0}: {1}\n", arg0: i + 1);
		}
		display(text);
	}

	private void display(string text)
	{
		messages.Add(text);
		if (messages.Count > 6)
		{
			messages.RemoveAt(0);
		}
		_label.Text = string.Join("\n", messages.ToArray());
	}

	private Vector2 screenToGUI(Vector2 position)
	{
		position.y = _label.GetManager().GetScreenSize().y - position.y;
		return position;
	}
}
