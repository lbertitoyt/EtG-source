using UnityEngine;

public class dfJumpButtonEvents : MonoBehaviour
{
	public bool isMouseDown;

	public void OnMouseDown(dfControl control, dfMouseEventArgs mouseEvent)
	{
		isMouseDown = true;
	}

	public void OnMouseUp(dfControl control, dfMouseEventArgs mouseEvent)
	{
		isMouseDown = false;
	}
}
