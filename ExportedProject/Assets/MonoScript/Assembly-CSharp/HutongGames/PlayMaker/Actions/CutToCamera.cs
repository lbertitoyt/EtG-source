using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Activates a Camera in the scene.")]
	[ActionCategory(ActionCategory.Camera)]
	public class CutToCamera : FsmStateAction
	{
		[Tooltip("The Camera to activate.")]
		[RequiredField]
		public Camera camera;

		[Tooltip("Makes the camera the new MainCamera. The old MainCamera will be untagged.")]
		public bool makeMainCamera;

		[Tooltip("Cut back to the original MainCamera when exiting this state.")]
		public bool cutBackOnExit;

		private Camera oldCamera;

		public override void Reset()
		{
			camera = null;
			makeMainCamera = true;
			cutBackOnExit = false;
		}

		public override void OnEnter()
		{
			if (camera == null)
			{
				LogError("Missing camera!");
				return;
			}
			oldCamera = Camera.main;
			SwitchCamera(Camera.main, camera);
			if (makeMainCamera)
			{
				camera.tag = "MainCamera";
			}
			Finish();
		}

		public override void OnExit()
		{
			if (cutBackOnExit)
			{
				SwitchCamera(camera, oldCamera);
			}
		}

		private static void SwitchCamera(Camera camera1, Camera camera2)
		{
			if (camera1 != null)
			{
				camera1.enabled = false;
			}
			if (camera2 != null)
			{
				camera2.enabled = true;
			}
		}
	}
}
