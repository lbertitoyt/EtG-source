namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Sets the value of a Vector2 Variable.")]
	[ActionCategory(ActionCategory.Vector2)]
	public class SetVector2Value : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 target")]
		public FsmVector2 vector2Variable;

		[Tooltip("The vector2 source")]
		[RequiredField]
		public FsmVector2 vector2Value;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			vector2Variable = null;
			vector2Value = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			vector2Variable.Value = vector2Value.Value;
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			vector2Variable.Value = vector2Value.Value;
		}
	}
}
