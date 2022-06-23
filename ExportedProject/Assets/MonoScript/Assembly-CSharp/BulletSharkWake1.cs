using System.Collections;
using Brave.BulletScript;
using FullInspector;
using UnityEngine;

[InspectorDropdownName("BulletShark/Wake1")]
public class BulletSharkWake1 : Script
{
	protected override IEnumerator Top()
	{
		for (int i = 0; i < 27; i++)
		{
			float startSpeed = Random.Range(4.5f, 7.5f);
			float endSpeed2 = Random.Range(-1.25f, -0.25f);
			int deathTimer = Random.Range(10, 60);
			if (Random.value < 0.22f)
			{
				Fire(new Direction(-90f, DirectionType.Relative), new Speed(startSpeed), new SpeedChangingBullet("tellBullet", 9f, 60));
				Fire(new Direction(90f, DirectionType.Relative), new Speed(startSpeed), new SpeedChangingBullet("tellBullet", 9f, 60));
			}
			else
			{
				Fire(new Direction(-90f, DirectionType.Relative), new Speed(startSpeed), new SpeedChangingBullet(endSpeed2, 60, deathTimer));
				Fire(new Direction(90f, DirectionType.Relative), new Speed(startSpeed), new SpeedChangingBullet(endSpeed2, 60, deathTimer));
			}
			if (i % 3 == 1)
			{
				endSpeed2 = BraveUtility.RandomSign() * Random.Range(1f, 2f);
				Fire(bullet: new SpeedChangingBullet(endSpeed2, 60, Random.Range(10, 60)), direction: new Direction(90f, DirectionType.Relative), speed: new Speed());
			}
			yield return Wait(5);
		}
	}
}
