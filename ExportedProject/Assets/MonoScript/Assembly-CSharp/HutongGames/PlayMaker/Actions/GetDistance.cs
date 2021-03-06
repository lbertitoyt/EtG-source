using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Measures the Distance betweens 2 Game Objects and stores the result in a Float Variable.")]
	[ActionCategory(ActionCategory.GameObject)]
	public class GetDistance : FsmStateAction
	{
		[Tooltip("Measure distance from this GameObject.")]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Target GameObject.")]
		[RequiredField]
		public FsmGameObject target;

		[UIHint(UIHint.Variable)]
		[RequiredField]
		[Tooltip("Store the distance in a float variable.")]
		public FsmFloat storeResult;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			target = null;
			storeResult = null;
			everyFrame = true;
		}

		public override void OnEnter()
		{
			DoGetDistance();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetDistance();
		}

		private void DoGetDistance()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null) && !(target.Value == null) && storeResult != null)
			{
				storeResult.Value = Vector3.Distance(ownerDefaultTarget.transform.position, target.Value.transform.position);
			}
		}
	}
}
