using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Gets the pressed state of a Key.")]
	[ActionCategory(ActionCategory.Input)]
	public class GetKey : FsmStateAction
	{
		[Tooltip("The key to test.")]
		[RequiredField]
		public KeyCode key;

		[Tooltip("Store if the key is down (True) or up (False).")]
		[UIHint(UIHint.Variable)]
		[RequiredField]
		public FsmBool storeResult;

		[Tooltip("Repeat every frame. Useful if you're waiting for a key press/release.")]
		public bool everyFrame;

		public override void Reset()
		{
			key = KeyCode.None;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetKey();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetKey();
		}

		private void DoGetKey()
		{
			storeResult.Value = Input.GetKey(key);
		}
	}
}
