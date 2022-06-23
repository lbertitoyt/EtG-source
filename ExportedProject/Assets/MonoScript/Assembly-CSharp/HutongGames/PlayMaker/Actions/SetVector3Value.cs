namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Sets the value of a Vector3 Variable.")]
	[ActionCategory(ActionCategory.Vector3)]
	public class SetVector3Value : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[RequiredField]
		public FsmVector3 vector3Variable;

		[RequiredField]
		public FsmVector3 vector3Value;

		public bool everyFrame;

		public override void Reset()
		{
			vector3Variable = null;
			vector3Value = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			vector3Variable.Value = vector3Value.Value;
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			vector3Variable.Value = vector3Value.Value;
		}
	}
}
