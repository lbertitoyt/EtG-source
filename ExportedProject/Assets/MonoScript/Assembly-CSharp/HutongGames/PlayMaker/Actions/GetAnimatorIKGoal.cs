using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Gets the position, rotation and weights of an IK goal. A GameObject can be set to use for the position and rotation")]
	[ActionCategory(ActionCategory.Animator)]
	public class GetAnimatorIKGoal : FsmStateActionAnimatorBase
	{
		[Tooltip("The target. An Animator component is required")]
		[CheckForComponent(typeof(Animator))]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[ObjectType(typeof(AvatarIKGoal))]
		[Tooltip("The IK goal")]
		public FsmEnum iKGoal;

		[Tooltip("The gameObject to apply ik goal position and rotation to")]
		[UIHint(UIHint.Variable)]
		[ActionSection("Results")]
		public FsmGameObject goal;

		[Tooltip("Gets The position of the ik goal. If Goal GameObject define, position is used as an offset from Goal")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 position;

		[UIHint(UIHint.Variable)]
		[Tooltip("Gets The rotation of the ik goal.If Goal GameObject define, rotation is used as an offset from Goal")]
		public FsmQuaternion rotation;

		[Tooltip("Gets The translative weight of an IK goal (0 = at the original animation before IK, 1 = at the goal)")]
		[UIHint(UIHint.Variable)]
		public FsmFloat positionWeight;

		[Tooltip("Gets the rotational weight of an IK goal (0 = rotation before IK, 1 = rotation at the IK goal)")]
		[UIHint(UIHint.Variable)]
		public FsmFloat rotationWeight;

		private Animator _animator;

		private Transform _transform;

		private AvatarIKGoal _iKGoal;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			iKGoal = null;
			goal = null;
			position = null;
			rotation = null;
			positionWeight = null;
			rotationWeight = null;
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
			GameObject value = goal.Value;
			if (value != null)
			{
				_transform = value.transform;
			}
			DoGetIKGoal();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoGetIKGoal();
		}

		private void DoGetIKGoal()
		{
			if (!(_animator == null))
			{
				_iKGoal = (AvatarIKGoal)(object)iKGoal.Value;
				if (_transform != null)
				{
					_transform.position = _animator.GetIKPosition(_iKGoal);
					_transform.rotation = _animator.GetIKRotation(_iKGoal);
				}
				if (!position.IsNone)
				{
					position.Value = _animator.GetIKPosition(_iKGoal);
				}
				if (!rotation.IsNone)
				{
					rotation.Value = _animator.GetIKRotation(_iKGoal);
				}
				if (!positionWeight.IsNone)
				{
					positionWeight.Value = _animator.GetIKPositionWeight(_iKGoal);
				}
				if (!rotationWeight.IsNone)
				{
					rotationWeight.Value = _animator.GetIKRotationWeight(_iKGoal);
				}
			}
		}
	}
}
