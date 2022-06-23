public class ObsidianShellItem : PassiveItem
{
	public override void Pickup(PlayerController player)
	{
		if (!m_pickedUp)
		{
			base.Pickup(player);
		}
	}

	public override DebrisObject Drop(PlayerController player)
	{
		DebrisObject debrisObject = base.Drop(player);
		debrisObject.GetComponent<ObsidianShellItem>().m_pickedUpThisRun = true;
		return debrisObject;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}
}
