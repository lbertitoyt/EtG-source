using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Blends an Animation towards a Target Weight over a specified Time.\nOptionally sends an Event when finished.")]
	public class BlendAnimation : BaseAnimationAction
	{
		[Tooltip("The GameObject to animate.")]
		[CheckForComponent(typeof(Animation))]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation to blend.")]
		[RequiredField]
		public FsmString animName;

		[HasFloatSlider(0f, 1f)]
		[RequiredField]
		[Tooltip("Target weight to blend to.")]
		public FsmFloat targetWeight;

		[Tooltip("How long should the blend take.")]
		[HasFloatSlider(0f, 5f)]
		[RequiredField]
		public FsmFloat time;

		[Tooltip("Event to send when the blend has finished.")]
		public FsmEvent finishEvent;

		private DelayedEvent delayedFinishEvent;

		public override void Reset()
		{
			gameObject = null;
			animName = null;
			targetWeight = 1f;
			time = 0.3f;
			finishEvent = null;
		}

		public override void OnEnter()
		{
			DoBlendAnimation((gameObject.OwnerOption != 0) ? gameObject.GameObject.Value : base.Owner);
		}

		public override void OnUpdate()
		{
			if (DelayedEvent.WasSent(delayedFinishEvent))
			{
				Finish();
			}
		}

		private void DoBlendAnimation(GameObject go)
		{
			if (go == null)
			{
				return;
			}
			Animation animation = go.GetComponent<Animation>();
			if (animation == null)
			{
				LogWarning("Missing Animation component on GameObject: " + go.name);
				Finish();
				return;
			}
			AnimationState animationState = animation[animName.Value];
			if (animationState == null)
			{
				LogWarning("Missing animation: " + animName.Value);
				Finish();
				return;
			}
			float value = time.Value;
			animation.Blend(animName.Value, targetWeight.Value, value);
			if (finishEvent != null)
			{
				delayedFinishEvent = base.Fsm.DelayedEvent(finishEvent, animationState.length);
			}
			else
			{
				Finish();
			}
		}
	}
}
