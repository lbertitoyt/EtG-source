using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Gets an item in an Array Variable in another FSM.")]
	public class GetFsmArrayItem : BaseFsmVariableIndexAction
	{
		[Tooltip("The GameObject that owns the FSM.")]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Optional name of FSM on Game Object.")]
		[UIHint(UIHint.FsmName)]
		public FsmString fsmName;

		[RequiredField]
		[Tooltip("The name of the FSM variable.")]
		[UIHint(UIHint.FsmArray)]
		public FsmString variableName;

		[Tooltip("The index into the array.")]
		public FsmInt index;

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the value of the array at the specified index.")]
		[RequiredField]
		public FsmVar storeValue;

		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			fsmName = string.Empty;
			storeValue = null;
		}

		public override void OnEnter()
		{
			DoGetFsmArray();
			if (!everyFrame)
			{
				Finish();
			}
		}

		private void DoGetFsmArray()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!UpdateCache(ownerDefaultTarget, fsmName.Value))
			{
				return;
			}
			FsmArray fsmArray = fsm.FsmVariables.GetFsmArray(variableName.Value);
			if (fsmArray != null)
			{
				if (index.Value < 0 || index.Value >= fsmArray.Length)
				{
					base.Fsm.Event(indexOutOfRange);
					Finish();
				}
				else if (fsmArray.ElementType == storeValue.NamedVar.VariableType)
				{
					storeValue.SetValue(fsmArray.Get(index.Value));
				}
				else
				{
					LogWarning("Incompatible variable type: " + variableName.Value);
				}
			}
			else
			{
				DoVariableNotFound(variableName.Value);
			}
		}

		public override void OnUpdate()
		{
			DoGetFsmArray();
		}
	}
}
