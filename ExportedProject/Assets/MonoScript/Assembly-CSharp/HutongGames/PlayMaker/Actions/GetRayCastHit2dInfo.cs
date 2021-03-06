using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets info on the last 2d Raycast or LineCast and store in variables.")]
	public class GetRayCastHit2dInfo : FsmStateAction
	{
		[Tooltip("Get the GameObject hit by the last Raycast and store it in a variable.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject gameObjectHit;

		[Title("Hit Point")]
		[Tooltip("Get the world position of the ray hit point and store it in a variable.")]
		[UIHint(UIHint.Variable)]
		public FsmVector2 point;

		[Tooltip("Get the normal at the hit point and store it in a variable.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 normal;

		[Tooltip("Get the distance along the ray to the hit point and store it in a variable.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat distance;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObjectHit = null;
			point = null;
			normal = null;
			distance = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			StoreRaycastInfo();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			StoreRaycastInfo();
		}

		private void StoreRaycastInfo()
		{
			RaycastHit2D lastRaycastHit2DInfo = Fsm.GetLastRaycastHit2DInfo(base.Fsm);
			if (lastRaycastHit2DInfo.collider != null)
			{
				gameObjectHit.Value = lastRaycastHit2DInfo.collider.gameObject;
				point.Value = lastRaycastHit2DInfo.point;
				normal.Value = lastRaycastHit2DInfo.normal;
				distance.Value = lastRaycastHit2DInfo.fraction;
			}
		}
	}
}
