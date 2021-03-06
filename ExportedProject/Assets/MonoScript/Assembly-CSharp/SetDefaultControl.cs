using System.Collections;
using UnityEngine;

[AddComponentMenu("Daikon Forge/Examples/General/Set Default Control")]
public class SetDefaultControl : MonoBehaviour
{
	public dfControl defaultControl;

	private dfControl thisControl;

	public void Awake()
	{
		thisControl = GetComponent<dfControl>();
	}

	public void OnEnable()
	{
		if (defaultControl != null && defaultControl.IsVisible)
		{
			defaultControl.Focus();
		}
	}

	public IEnumerator OnIsVisibleChanged(dfControl control, bool value)
	{
		if (control == thisControl && value && defaultControl != null)
		{
			yield return new WaitForEndOfFrame();
			defaultControl.Focus();
		}
	}
}
