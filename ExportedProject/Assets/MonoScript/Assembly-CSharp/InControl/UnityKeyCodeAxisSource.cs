using UnityEngine;

namespace InControl
{
	public class UnityKeyCodeAxisSource : InputControlSource
	{
		public KeyCode NegativeKeyCode;

		public KeyCode PositiveKeyCode;

		public UnityKeyCodeAxisSource()
		{
		}

		public UnityKeyCodeAxisSource(KeyCode negativeKeyCode, KeyCode positiveKeyCode)
		{
			NegativeKeyCode = negativeKeyCode;
			PositiveKeyCode = positiveKeyCode;
		}

		public float GetValue(InputDevice inputDevice)
		{
			int num = 0;
			if (Input.GetKey(NegativeKeyCode))
			{
				num--;
			}
			if (Input.GetKey(PositiveKeyCode))
			{
				num++;
			}
			return num;
		}

		public bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(GetValue(inputDevice));
		}
	}
}
