using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Mesh")]
	[Tooltip("Gets the number of vertices in a GameObject's mesh. Useful in conjunction with GetVertexPosition.")]
	public class GetVertexCount : FsmStateAction
	{
		[Tooltip("The GameObject to check.")]
		[CheckForComponent(typeof(MeshFilter))]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[RequiredField]
		[Tooltip("Store the vertex count in a variable.")]
		public FsmInt storeCount;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			storeCount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetVertexCount();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetVertexCount();
		}

		private void DoGetVertexCount()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				MeshFilter component = ownerDefaultTarget.GetComponent<MeshFilter>();
				if (component == null)
				{
					LogError("Missing MeshFilter!");
				}
				else
				{
					storeCount.Value = component.mesh.vertexCount;
				}
			}
		}
	}
}
