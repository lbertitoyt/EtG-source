using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Copy an Array Variable from another FSM.")]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	public class GetFsmArray : BaseFsmVariableAction
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

		[UIHint(UIHint.Variable)]
		[Tooltip("Get the content of the array variable.")]
		[RequiredField]
		public FsmArray storeValue;

		[Tooltip("If true, makes copies. if false, values share the same reference and editing one array item value will affect the source and vice versa. Warning, this only affect the current items of the source array. Adding or removing items doesn't affect other FsmArrays.")]
		public bool copyValues;

		public override void Reset()
		{
			gameObject = null;
			fsmName = string.Empty;
			variableName = null;
			storeValue = null;
			copyValues = true;
		}

		public override void OnEnter()
		{
			DoSetFsmArrayCopy();
			Finish();
		}

		private void DoSetFsmArrayCopy()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!UpdateCache(ownerDefaultTarget, fsmName.Value))
			{
				return;
			}
			FsmArray fsmArray = fsm.FsmVariables.GetFsmArray(variableName.Value);
			if (fsmArray != null)
			{
				if (fsmArray.ElementType != storeValue.ElementType)
				{
					LogError(string.Concat("Can only copy arrays with the same elements type. Found <", fsmArray.ElementType, "> and <", storeValue.ElementType, ">"));
				}
				else
				{
					storeValue.Resize(0);
					if (copyValues)
					{
						storeValue.Values = fsmArray.Values.Clone() as object[];
					}
					else
					{
						storeValue.Values = fsmArray.Values;
					}
				}
			}
			else
			{
				DoVariableNotFound(variableName.Value);
			}
		}
	}
}
