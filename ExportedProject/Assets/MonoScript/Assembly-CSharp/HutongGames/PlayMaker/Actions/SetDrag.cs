using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=4734.0")]
	[Tooltip("Sets the Drag of a Game Object's Rigid Body.")]
	[ActionCategory(ActionCategory.Physics)]
	public class SetDrag : ComponentAction<Rigidbody>
	{
		[CheckForComponent(typeof(Rigidbody))]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[HasFloatSlider(0f, 10f)]
		[RequiredField]
		public FsmFloat drag;

		[Tooltip("Repeat every frame. Typically this would be set to True.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			drag = 1f;
		}

		public override void OnEnter()
		{
			DoSetDrag();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetDrag();
		}

		private void DoSetDrag()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				base.rigidbody.drag = drag.Value;
			}
		}
	}
}
