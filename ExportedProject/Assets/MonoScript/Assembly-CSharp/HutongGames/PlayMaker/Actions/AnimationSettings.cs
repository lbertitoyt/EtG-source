using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Set the Wrap Mode, Blend Mode, Layer and Speed of an Animation.\nNOTE: Settings are applied once, on entering the state, NOT continuously. To dynamically control an animation's settings, use Set Animation Speede etc.")]
	[ActionCategory(ActionCategory.Animation)]
	public class AnimationSettings : BaseAnimationAction
	{
		[CheckForComponent(typeof(Animation))]
		[Tooltip("A GameObject with an Animation Component.")]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation.")]
		[RequiredField]
		public FsmString animName;

		[Tooltip("The behavior of the animation when it wraps.")]
		public WrapMode wrapMode;

		[Tooltip("How the animation is blended with other animations on the Game Object.")]
		public AnimationBlendMode blendMode;

		[Tooltip("The speed of the animation. 1 = normal; 2 = double speed...")]
		[HasFloatSlider(0f, 5f)]
		public FsmFloat speed;

		[Tooltip("The animation layer")]
		public FsmInt layer;

		public override void Reset()
		{
			gameObject = null;
			animName = null;
			wrapMode = WrapMode.Loop;
			blendMode = AnimationBlendMode.Blend;
			speed = 1f;
			layer = 0;
		}

		public override void OnEnter()
		{
			DoAnimationSettings();
			Finish();
		}

		private void DoAnimationSettings()
		{
			if (string.IsNullOrEmpty(animName.Value))
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			AnimationState animationState = base.animation[animName.Value];
			if (animationState == null)
			{
				LogWarning("Missing animation: " + animName.Value);
				return;
			}
			animationState.wrapMode = wrapMode;
			animationState.blendMode = blendMode;
			if (!layer.IsNone)
			{
				animationState.layer = layer.Value;
			}
			if (!speed.IsNone)
			{
				animationState.speed = speed.Value;
			}
		}
	}
}