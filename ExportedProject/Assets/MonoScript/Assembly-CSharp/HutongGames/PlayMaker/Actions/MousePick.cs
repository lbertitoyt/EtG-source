using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Perform a Mouse Pick on the scene from the Main Camera and stores the results. Use Ray Distance to set how close the camera must be to pick the object.")]
	[ActionCategory(ActionCategory.Input)]
	public class MousePick : FsmStateAction
	{
		[Tooltip("Set the length of the ray to cast from the Main Camera.")]
		[RequiredField]
		public FsmFloat rayDistance = 100f;

		[Tooltip("Set Bool variable true if an object was picked, false if not.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeDidPickObject;

		[Tooltip("Store the picked GameObject.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeGameObject;

		[Tooltip("Store the point of contact.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storePoint;

		[Tooltip("Store the normal at the point of contact.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeNormal;

		[Tooltip("Store the distance to the point of contact.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeDistance;

		[Tooltip("Pick only from these layers.")]
		[UIHint(UIHint.Layer)]
		public FsmInt[] layerMask;

		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			rayDistance = 100f;
			storeDidPickObject = null;
			storeGameObject = null;
			storePoint = null;
			storeNormal = null;
			storeDistance = null;
			layerMask = new FsmInt[0];
			invertMask = false;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoMousePick();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoMousePick();
		}

		private void DoMousePick()
		{
			RaycastHit raycastHit = ActionHelpers.MousePick(rayDistance.Value, ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value));
			bool flag = raycastHit.collider != null;
			storeDidPickObject.Value = flag;
			if (flag)
			{
				storeGameObject.Value = raycastHit.collider.gameObject;
				storeDistance.Value = raycastHit.distance;
				storePoint.Value = raycastHit.point;
				storeNormal.Value = raycastHit.normal;
			}
			else
			{
				storeGameObject.Value = null;
				storeDistance.Value = float.PositiveInfinity;
				storePoint.Value = Vector3.zero;
				storeNormal.Value = Vector3.zero;
			}
		}
	}
}
