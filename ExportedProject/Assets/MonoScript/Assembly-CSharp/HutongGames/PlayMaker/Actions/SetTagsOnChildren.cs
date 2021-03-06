using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Tooltip("Set the Tag on all children of a GameObject. Optionally filter by component.")]
	[ActionCategory(ActionCategory.GameObject)]
	public class SetTagsOnChildren : FsmStateAction
	{
		[Tooltip("GameObject Parent")]
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Tag)]
		[Tooltip("Set Tag To...")]
		[RequiredField]
		public FsmString tag;

		[Tooltip("Only set the Tag on children with this component.")]
		[UIHint(UIHint.ScriptComponent)]
		public FsmString filterByComponent;

		private Type componentFilter;

		public override void Reset()
		{
			gameObject = null;
			tag = null;
			filterByComponent = null;
		}

		public override void OnEnter()
		{
			SetTag(base.Fsm.GetOwnerDefaultTarget(gameObject));
			Finish();
		}

		private void SetTag(GameObject parent)
		{
			if (parent == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(filterByComponent.Value))
			{
				IEnumerator enumerator = parent.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Transform transform = (Transform)enumerator.Current;
						transform.gameObject.tag = tag.Value;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = enumerator as IDisposable) != null)
					{
						disposable.Dispose();
					}
				}
			}
			else
			{
				UpdateComponentFilter();
				if (componentFilter != null)
				{
					Component[] componentsInChildren = parent.GetComponentsInChildren(componentFilter);
					Component[] array = componentsInChildren;
					foreach (Component component in array)
					{
						component.gameObject.tag = tag.Value;
					}
				}
			}
			Finish();
		}

		private void UpdateComponentFilter()
		{
			componentFilter = ReflectionUtils.GetGlobalType(filterByComponent.Value);
			if (componentFilter == null)
			{
				componentFilter = ReflectionUtils.GetGlobalType("UnityEngine." + filterByComponent.Value);
			}
			if (componentFilter == null)
			{
				Debug.LogWarning("Couldn't get type: " + filterByComponent.Value);
			}
		}
	}
}
