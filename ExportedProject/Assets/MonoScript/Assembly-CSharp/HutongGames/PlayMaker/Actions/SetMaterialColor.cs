using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Sets a named color value in a game object's material.")]
	[ActionCategory(ActionCategory.Material)]
	public class SetMaterialColor : ComponentAction<Renderer>
	{
		[CheckForComponent(typeof(Renderer))]
		[Tooltip("The GameObject that the material is applied to.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		[Tooltip("A named color parameter in the shader.")]
		[UIHint(UIHint.NamedColor)]
		public FsmString namedColor;

		[Tooltip("Set the parameter value.")]
		[RequiredField]
		public FsmColor color;

		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			material = null;
			namedColor = "_Color";
			color = Color.black;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetMaterialColor();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetMaterialColor();
		}

		private void DoSetMaterialColor()
		{
			if (color.IsNone)
			{
				return;
			}
			string text = namedColor.Value;
			if (text == string.Empty)
			{
				text = "_Color";
			}
			if (material.Value != null)
			{
				material.Value.SetColor(text, color.Value);
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				if (base.renderer.material == null)
				{
					LogError("Missing Material!");
				}
				else if (materialIndex.Value == 0)
				{
					base.renderer.material.SetColor(text, color.Value);
				}
				else if (base.renderer.materials.Length > materialIndex.Value)
				{
					Material[] materials = base.renderer.materials;
					materials[materialIndex.Value].SetColor(text, color.Value);
					base.renderer.materials = materials;
				}
			}
		}
	}
}
