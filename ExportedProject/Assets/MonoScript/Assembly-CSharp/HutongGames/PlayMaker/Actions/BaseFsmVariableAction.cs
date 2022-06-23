using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[ActionCategory(ActionCategory.StateMachine)]
	public abstract class BaseFsmVariableAction : FsmStateAction
	{
		[Tooltip("The event to send if the FSM is not found.")]
		[ActionSection("Events")]
		public FsmEvent fsmNotFound;

		[Tooltip("The event to send if the Variable is not found.")]
		public FsmEvent variableNotFound;

		private GameObject cachedGameObject;

		private string cachedFsmName;

		protected PlayMakerFSM fsm;

		public override void Reset()
		{
			fsmNotFound = null;
			variableNotFound = null;
		}

		protected bool UpdateCache(GameObject go, string fsmName)
		{
			if (go == null)
			{
				return false;
			}
			if (fsm == null || cachedGameObject != go || cachedFsmName != fsmName)
			{
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName);
				cachedGameObject = go;
				cachedFsmName = fsmName;
				if (fsm == null)
				{
					LogWarning("Could not find FSM: " + fsmName);
					base.Fsm.Event(fsmNotFound);
				}
			}
			return true;
		}

		protected void DoVariableNotFound(string variableName)
		{
			LogWarning("Could not find variable: " + variableName);
			base.Fsm.Event(variableNotFound);
		}
	}
}
