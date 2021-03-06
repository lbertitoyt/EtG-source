using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Controls whether 2D physics affects the Game Object.")]
	[ActionCategory(ActionCategory.Physics2D)]
	public class SetIsKinematic2d : ComponentAction<Rigidbody2D>
	{
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		[CheckForComponent(typeof(Rigidbody2D))]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("The isKinematic value")]
		[RequiredField]
		public FsmBool isKinematic;

		public override void Reset()
		{
			gameObject = null;
			isKinematic = false;
		}

		public override void OnEnter()
		{
			DoSetIsKinematic();
			Finish();
		}

		private void DoSetIsKinematic()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				base.rigidbody2d.isKinematic = isKinematic.Value;
			}
		}
	}
}
