namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Sets all items in an Array to their default value: 0, empty string, false, or null depending on their type. Optionally defines a reset value to use.")]
	public class ArrayClear : FsmStateAction
	{
		[Tooltip("The Array Variable to clear.")]
		[UIHint(UIHint.Variable)]
		public FsmArray array;

		[Tooltip("Optional reset value. Leave as None for default value.")]
		[MatchElementType("array")]
		public FsmVar resetValue;

		public override void Reset()
		{
			array = null;
			resetValue = new FsmVar
			{
				useVariable = true
			};
		}

		public override void OnEnter()
		{
			int length = array.Length;
			array.Reset();
			array.Resize(length);
			if (!resetValue.IsNone)
			{
				resetValue.UpdateValue();
				object value = resetValue.GetValue();
				for (int i = 0; i < length; i++)
				{
					array.Set(i, value);
				}
			}
			Finish();
		}
	}
}
