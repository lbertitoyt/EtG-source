using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Substance")]
	[Tooltip("Set a named bool property in a Substance material. NOTE: Use Rebuild Textures after setting Substance properties.")]
	public class SetProceduralBoolean : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Substance Material.")]
		public FsmMaterial substanceMaterial;

		[Tooltip("The named bool property in the material.")]
		[RequiredField]
		public FsmString boolProperty;

		[RequiredField]
		[Tooltip("The value to set the property to.")]
		public FsmBool boolValue;

		[Tooltip("NOTE: Updating procedural materials every frame can be very slow!")]
		public bool everyFrame;

		public override void Reset()
		{
			substanceMaterial = null;
			boolProperty = string.Empty;
			boolValue = false;
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
				proceduralMaterial.SetProceduralBoolean(boolProperty.Value, boolValue.Value);
			}
		}
	}
}
