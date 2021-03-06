using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Set a named color property in a Substance material. NOTE: Use Rebuild Textures after setting Substance properties.")]
	[ActionCategory("Substance")]
	public class SetProceduralColor : FsmStateAction
	{
		[Tooltip("The Substance Material.")]
		[RequiredField]
		public FsmMaterial substanceMaterial;

		[RequiredField]
		[Tooltip("The named color property in the material.")]
		public FsmString colorProperty;

		[Tooltip("The value to set the property to.")]
		[RequiredField]
		public FsmColor colorValue;

		[Tooltip("NOTE: Updating procedural materials every frame can be very slow!")]
		public bool everyFrame;

		public override void Reset()
		{
			substanceMaterial = null;
			colorProperty = string.Empty;
			colorValue = Color.white;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetProceduralFloat();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetProceduralFloat();
		}

		private void DoSetProceduralFloat()
		{
			ProceduralMaterial proceduralMaterial = substanceMaterial.Value as ProceduralMaterial;
			if (proceduralMaterial == null)
			{
				LogError("The Material is not a Substance Material!");
			}
			else
			{
				proceduralMaterial.SetProceduralColor(colorProperty.Value, colorValue.Value);
			}
		}
	}
}
