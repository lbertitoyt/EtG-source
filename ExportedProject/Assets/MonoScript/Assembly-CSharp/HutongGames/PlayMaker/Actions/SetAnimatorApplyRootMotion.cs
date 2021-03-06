using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Set Apply Root Motion: If true, Root is controlled by animations")]
	[ActionCategory(ActionCategory.Animator)]
	public class SetAnimatorApplyRootMotion : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Target. An Animator component is required")]
		[CheckForComponent(typeof(Animator))]
		public FsmOwnerDefault gameObject;

		[Tooltip("If true, Root is controlled by animations")]
		public FsmBool applyRootMotion;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			applyRootMotion = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			_animator = ownerDefaultTarget.GetComponent<Animator>();
			if (_animator == null)
			{
				Finish();
				return;
			}
			DoApplyRootMotion();
			Finish();
		}

		private void DoApplyRootMotion()
		{
			if (!(_animator == null))
			{
				_animator.applyRootMotion = applyRootMotion.Value;
			}
		}
	}
}
