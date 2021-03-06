using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the current transition information on a specified layer. Only valid when during a transition.")]
	public class GetAnimatorCurrentTransitionInfo : FsmStateActionAnimatorBase
	{
		[Tooltip("The target. An Animator component is required")]
		[CheckForComponent(typeof(Animator))]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("The layer's index")]
		[RequiredField]
		public FsmInt layerIndex;

		[Tooltip("The unique name of the Transition")]
		[UIHint(UIHint.Variable)]
		[ActionSection("Results")]
		public FsmString name;

		[Tooltip("The unique name of the Transition")]
		[UIHint(UIHint.Variable)]
		public FsmInt nameHash;

		[Tooltip("The user-specidied name of the Transition")]
		[UIHint(UIHint.Variable)]
		public FsmInt userNameHash;

		[Tooltip("Normalized time of the Transition")]
		[UIHint(UIHint.Variable)]
		public FsmFloat normalizedTime;

		private Animator _animator;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			layerIndex = null;
			name = null;
			nameHash = null;
			userNameHash = null;
			normalizedTime = null;
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
			GetTransitionInfo();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			GetTransitionInfo();
		}

		private void GetTransitionInfo()
		{
			if (_animator != null)
			{
				AnimatorTransitionInfo animatorTransitionInfo = _animator.GetAnimatorTransitionInfo(layerIndex.Value);
				if (!name.IsNone)
				{
					name.Value = _animator.GetLayerName(layerIndex.Value);
				}
				if (!nameHash.IsNone)
				{
					nameHash.Value = animatorTransitionInfo.nameHash;
				}
				if (!userNameHash.IsNone)
				{
					userNameHash.Value = animatorTransitionInfo.userNameHash;
				}
				if (!normalizedTime.IsNone)
				{
					normalizedTime.Value = animatorTransitionInfo.normalizedTime;
				}
			}
		}
	}
}
