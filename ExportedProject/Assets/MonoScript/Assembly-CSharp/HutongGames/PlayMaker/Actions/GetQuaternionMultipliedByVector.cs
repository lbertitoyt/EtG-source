namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Get the vector3 from a quaternion multiplied by a vector.")]
	[ActionCategory(ActionCategory.Quaternion)]
	public class GetQuaternionMultipliedByVector : QuaternionBaseAction
	{
		[Tooltip("The quaternion to multiply")]
		[RequiredField]
		public FsmQuaternion quaternion;

		[RequiredField]
		[Tooltip("The vector3 to multiply")]
		public FsmVector3 vector3;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting vector3")]
		public FsmVector3 result;

		public override void Reset()
		{
			quaternion = null;
			vector3 = null;
			result = null;
			everyFrame = false;
			everyFrameOption = everyFrameOptions.Update;
		}

		public override void OnEnter()
		{
			DoQuatMult();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update)
			{
				DoQuatMult();
			}
		}

		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate)
			{
				DoQuatMult();
			}
		}

		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate)
			{
				DoQuatMult();
			}
		}

		private void DoQuatMult()
		{
			result.Value = quaternion.Value * vector3.Value;
		}
	}
}
