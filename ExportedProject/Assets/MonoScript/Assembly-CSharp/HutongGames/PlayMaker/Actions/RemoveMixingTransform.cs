using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Removes a mixing transform previously added with Add Mixing Transform. If transform has been added as recursive, then it will be removed as recursive. Once you remove all mixing transforms added to animation state all curves become animated again.")]
	[ActionCategory(ActionCategory.Animation)]
	public class RemoveMixingTransform : BaseAnimationAction
	{
		[Tooltip("The GameObject playing the animation.")]
		[CheckForComponent(typeof(Animation))]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The name of the animation.")]
		public FsmString animationName;

		[Tooltip("The mixing transform to remove. E.g., root/upper_body/left_shoulder")]
		[RequiredField]
		public FsmString transfrom;

		public override void Reset()
		{
			gameObject = null;
			animationName = string.Empty;
		}

		public override void OnEnter()
		{
			DoRemoveMixingTransform();
			Finish();
		}

		private void DoRemoveMixingTransform()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				AnimationState animationState = base.animation[animationName.Value];
				if (!(animationState == null))
				{
					Transform mix = ownerDefaultTarget.transform.Find(transfrom.Value);
					animationState.AddMixingTransform(mix);
				}
			}
		}
	}
}
