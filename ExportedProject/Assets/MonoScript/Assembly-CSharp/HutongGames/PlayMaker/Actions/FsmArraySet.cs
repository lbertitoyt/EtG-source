using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Obsolete("This action was wip and accidentally released.")]
	[Tooltip("Set an item in an Array Variable in another FSM.")]
	[ActionCategory(ActionCategory.Array)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	public class FsmArraySet : FsmStateAction
	{
		[Tooltip("The GameObject that owns the FSM.")]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object.")]
		public FsmString fsmName;

		[Tooltip("The name of the FSM variable.")]
		[RequiredField]
		public FsmString variableName;

		[Tooltip("Set the value of the variable.")]
		public FsmString setValue;

		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		private GameObject goLastFrame;

		private PlayMakerFSM fsm;

		public override void Reset()
		{
			gameObject = null;
			fsmName = string.Empty;
			setValue = null;
		}

		public override void OnEnter()
		{
			DoSetFsmString();
			if (!everyFrame)
			{
				Finish();
			}
		}

		private void DoSetFsmString()
		{
			if (setValue == null)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (ownerDefaultTarget != goLastFrame)
			{
				goLastFrame = ownerDefaultTarget;
				fsm = ActionHelpers.GetGameObjectFsm(ownerDefaultTarget, fsmName.Value);
			}
			if (fsm == null)
			{
				LogWarning("Could not find FSM: " + fsmName.Value);
				return;
			}
			FsmString fsmString = fsm.FsmVariables.GetFsmString(variableName.Value);
			if (fsmString != null)
			{
				fsmString.Value = setValue.Value;
			}
			else
			{
				LogWarning("Could not find variable: " + variableName.Value);
			}
		}

		public override void OnUpdate()
		{
			DoSetFsmString();
		}
	}
}
