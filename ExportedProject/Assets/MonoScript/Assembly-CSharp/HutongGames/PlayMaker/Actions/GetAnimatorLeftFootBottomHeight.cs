using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Get the left foot bottom height.")]
	public class GetAnimatorLeftFootBottomHeight : FsmStateAction
	{
		[Tooltip("The Target. An Animator component is required")]
		[CheckForComponent(typeof(Animator))]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[ActionSection("Result")]
		[Tooltip("the left foot bottom height.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat leftFootHeight;

		[Tooltip("Repeat every frame. Useful when value is subject to change over time.")]
		public bool everyFrame;

		private Animator _animator;

		public override void Reset()
		{
			gameObject = null;
			leftFootHeight = null;
			everyFrame = false;
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
			_getLeftFootBottonHeight();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnLateUpdate()
		{
			_getLeftFootBottonHeight();
		}

		private void _getLeftFootBottonHeight()
		{
			if (_animator != null)
			{
				leftFootHeight.Value = _animator.leftFeetBottomHeight;
			}
		}
	}
}
