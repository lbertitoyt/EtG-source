using System.Collections;
using System.Collections.Generic;
using Brave.BulletScript;
using FullInspector;
using UnityEngine;

[InspectorDropdownName("Bosses/MetalGearRat/Spinners1")]
public class MetalGearRatSpinners1 : Script
{
	public class CircleDummy : Bullet
	{
		public int NearTime;

		public int FireTick = -1;

		private MetalGearRatSpinners1 m_parent;

		private CircleDummy m_circleDummy;

		private Vector2 m_centerPoint;

		private float m_centerRadius;

		private float m_centerAngle;

		private float m_orbitSpeed;

		private float? m_initialRadiusBoost;

		public CircleDummy(MetalGearRatSpinners1 parent, Vector2 centerPoint, float centerRadius, float centerAngle, float orbitSpeed, float? initialRadiusBoostBoost = null)
			: base("spinner")
		{
			m_parent = parent;
			m_centerPoint = centerPoint;
			m_centerRadius = centerRadius;
			m_centerAngle = centerAngle;
			m_orbitSpeed = orbitSpeed;
			m_initialRadiusBoost = initialRadiusBoostBoost;
		}

		protected override IEnumerator Top()
		{
			float radius = m_centerRadius;
			base.ManualControl = true;
			while (true)
			{
				float? initialRadiusBoost = m_initialRadiusBoost;
				if (initialRadiusBoost.HasValue && base.Tick <= 60)
				{
					radius = m_centerRadius + Mathf.Lerp(m_initialRadiusBoost.Value, 0f, (float)base.Tick / 60f);
				}
				else
				{
					m_centerAngle += m_orbitSpeed / 60f;
				}
				base.Position = m_centerPoint + BraveMathCollege.DegreesToVector(m_centerAngle, radius);
				float playerDist = (BulletManager.PlayerPosition() - base.Position).magnitude;
				if (playerDist < 5.5f || NearTime < 0)
				{
					NearTime++;
				}
				else
				{
					NearTime = Mathf.Max(0, NearTime - 2);
				}
				if (NearTime >= 100)
				{
					FireTick = base.Tick;
					NearTime = -60;
				}
				yield return Wait(1);
			}
		}
	}

	public class CircleBullet : Bullet
	{
		private MetalGearRatSpinners1 m_parent;

		private CircleDummy m_circleDummy;

		private float m_rotationSpeed;

		private int m_index;

		private Vector2 m_offset;

		public CircleBullet(MetalGearRatSpinners1 parent, CircleDummy circleDummy, float rotationSpeed, int index, Vector2 offset)
			: base("spinner")
		{
			m_parent = parent;
			m_circleDummy = circleDummy;
			m_rotationSpeed = rotationSpeed;
			m_index = index;
			m_offset = offset;
		}

		protected override IEnumerator Top()
		{
			Projectile.specRigidbody.AddCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.HighObstacle, CollisionLayer.LowObstacle));
			int remainingLife = -1;
			bool isWarning = false;
			base.ManualControl = true;
			Projectile.specRigidbody.CollideWithTileMap = false;
			Projectile.BulletScriptSettings.surviveRigidbodyCollisions = true;
			while (!m_parent.Destroyed && remainingLife != 0)
			{
				if (m_parent.IsEnded || m_parent.Done)
				{
					remainingLife = ((remainingLife >= 0) ? (remainingLife - 1) : Random.Range(0, 60));
				}
				m_offset = m_offset.Rotate(m_rotationSpeed / 60f);
				base.Position = m_circleDummy.Position + m_offset;
				if (m_circleDummy.FireTick == base.Tick && remainingLife < 0)
				{
					Vector2 vector = m_circleDummy.Position - base.Position;
					TimedBullet timedBullet = new TimedBullet(30);
					Fire(new Direction(vector.ToAngle()), new Speed(vector.magnitude * 2f), timedBullet);
					timedBullet.Projectile.IgnoreTileCollisionsFor(1f);
				}
				bool shouldWarn = false;
				if (m_circleDummy.NearTime > 0 && remainingLife < 0)
				{
					shouldWarn = m_circleDummy.NearTime + 30 >= 100;
				}
				if (shouldWarn && !isWarning)
				{
					tk2dSpriteAnimationClip defaultClip = Projectile.spriteAnimator.DefaultClip;
					float clipStartTime = (float)m_circleDummy.NearTime / 60f % defaultClip.BaseClipLength;
					Projectile.spriteAnimator.Play(defaultClip, clipStartTime, defaultClip.fps);
					isWarning = true;
				}
				else if (!shouldWarn && isWarning)
				{
					Projectile.spriteAnimator.StopAndResetFrameToDefault();
					isWarning = false;
				}
				yield return Wait(1);
			}
			Vanish(true);
		}
	}

	public class OrbitBullet : Bullet
	{
		private MetalGearRatSpinners1 m_parent;

		private Vector2 m_centerPoint;

		private float m_radius;

		private float m_angle;

		private float m_orbitSpeed;

		private float? m_initialRadiusBoost;

		public OrbitBullet(MetalGearRatSpinners1 parent, float radius, float angle, float orbitSpeed, float? initialRadiusBoost = null)
			: base("spinner")
		{
			m_parent = parent;
			m_radius = radius;
			m_angle = angle;
			m_orbitSpeed = orbitSpeed;
			m_initialRadiusBoost = initialRadiusBoost;
		}

		protected override IEnumerator Top()
		{
			Projectile.specRigidbody.AddCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.HighObstacle, CollisionLayer.LowObstacle));
			float radius = m_radius;
			int remainingLife = -1;
			base.ManualControl = true;
			Projectile.specRigidbody.CollideWithTileMap = false;
			Projectile.BulletScriptSettings.surviveRigidbodyCollisions = true;
			while (!m_parent.Destroyed && remainingLife != 0)
			{
				if (m_parent.IsEnded || m_parent.Done)
				{
					remainingLife = ((remainingLife >= 0) ? (remainingLife - 1) : Random.Range(0, 60));
				}
				float? initialRadiusBoost = m_initialRadiusBoost;
				if (initialRadiusBoost.HasValue && base.Tick <= 60)
				{
					radius = m_radius + Mathf.Lerp(m_initialRadiusBoost.Value, 0f, (float)base.Tick / 60f);
				}
				else
				{
					m_angle += m_orbitSpeed / 60f;
				}
				base.Position = m_parent.CenterPoint + BraveMathCollege.DegreesToVector(m_angle, radius + Mathf.SmoothStep(-0.25f, 0.25f, Mathf.PingPong(base.Tick, 30f) / 30f));
				yield return Wait(1);
			}
			Vanish(true);
		}
	}

	private const int BulletsPerCircle = 21;

	private const float CircleRadius = 5f;

	private const int NearTimeForAttack = 100;

	private List<CircleDummy> m_circleDummies = new List<CircleDummy>();

	private Vector2 CenterPoint { get; set; }

	private bool Done { get; set; }

	protected override IEnumerator Top()
	{
		yield return Wait(30);
		base.EndOnBlank = true;
		CenterPoint = base.BulletBank.aiActor.ParentRoom.area.UnitCenter + new Vector2(0f, 4.5f);
		m_circleDummies.Clear();
		StartTask(SpawnInnerRing());
		StartTask(SpawnOuterRing());
		int spinTime = 960;
		for (int i = 0; i < spinTime; i++)
		{
			for (int j = 0; j < m_circleDummies.Count; j++)
			{
				m_circleDummies[j].DoTick();
			}
			if (i == spinTime - 60)
			{
				Done = true;
			}
			yield return Wait(1);
		}
	}

	private IEnumerator SpawnInnerRing()
	{
		int spinTime = 500;
		int numCircles = 3;
		float initialRadiusBoost = 12f;
		float orbitSpeed = 360f / ((float)spinTime / 60f);
		float radius = 7.5f;
		float rotationSpeed = -45f;
		SpawnCircleDebris(radius, 140f, 0f, 0.35f, 0f, initialRadiusBoost);
		SpawnCircleDebris(radius, 130f, 0f, 0.37f, 0f, initialRadiusBoost);
		SpawnCircleDebris(radius, 120f, 0f, 0.31f, 0f, initialRadiusBoost);
		SpawnCircleDebris(radius, 110f, 0f, 0.23f, 0f, initialRadiusBoost);
		SpawnCircleDebris(radius, 100f, 0f, 0.27f, 0f, initialRadiusBoost);
		SpawnCircleDebris(radius, 90f, 0f, 0.29f, 0f, initialRadiusBoost);
		SpawnCircleDebris(radius, 80f, 0f, 0.27f, 0f, initialRadiusBoost);
		SpawnCircleDebris(radius, 70f, 0f, 0.23f, 0f, initialRadiusBoost);
		SpawnCircleDebris(radius, 60f, 0f, 0.31f, 0f, initialRadiusBoost);
		SpawnCircleDebris(radius, 50f, 0f, 0.37f, 0f, initialRadiusBoost);
		SpawnCircleDebris(radius, 40f, 0f, 0.35f, 0f, initialRadiusBoost);
		for (int i = 0; i < numCircles; i++)
		{
			float spawnAngle = 90f;
			SpawnCircle(radius, spawnAngle, orbitSpeed, rotationSpeed, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, orbitSpeed, 0.95f, -50f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, orbitSpeed, 0.95f, -30f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, orbitSpeed, 0.95f, 30f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, orbitSpeed, 0.95f, 50f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, orbitSpeed, 0.78f, -40f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, orbitSpeed, 0.78f, 60f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, orbitSpeed, 0.78f, 40f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, orbitSpeed, 0.6f, -50f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, orbitSpeed, 0.6f, 50f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, orbitSpeed, 0.45f, 60f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, orbitSpeed, 0.25f, 60f, initialRadiusBoost);
			yield return Wait(spinTime / numCircles);
		}
	}

	private IEnumerator SpawnOuterRing()
	{
		int spinTime = 1100;
		int numCircles = 8;
		float initialRadiusBoost = 12f;
		float orbitSpeed = 360f / ((float)spinTime / 60f);
		float radius = 18.5f;
		float rotationSpeed = 45f;
		float deltaAngle = 0f;
		for (int i = 0; i < numCircles; i++)
		{
			float spawnAngle = deltaAngle;
			SpawnCircle(radius, spawnAngle, 0f - orbitSpeed, rotationSpeed, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 0f, -17.5f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 0f, 17.5f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 0.15f, 22.5f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 0.3f, 22.5f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 0.45f, 22.5f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 0.6f, 22.5f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 0.75f, -19f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 0.75f, 19f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 0.9f, 22.5f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 1.05f, -17.5f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 1.05f, 17.5f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 1.2f, 22.5f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 1.2f, 0f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 1.35f, -15f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 1.35f, 15f, initialRadiusBoost);
			SpawnCircleDebris(radius, spawnAngle, 0f - orbitSpeed, 1.5f, 22.5f, initialRadiusBoost);
			deltaAngle += 360f / (float)numCircles;
		}
		yield return Wait(1);
	}

	private void SpawnCircle(float spawnRadius, float spawnAngle, float orbitSpeed, float rotationSpeed, float? initialRadiusBoost = null)
	{
		float magnitude = spawnRadius + ((!initialRadiusBoost.HasValue) ? 0f : initialRadiusBoost.Value);
		Vector2 vector = CenterPoint + BraveMathCollege.DegreesToVector(spawnAngle, magnitude);
		CircleDummy circleDummy = new CircleDummy(this, CenterPoint, spawnRadius, spawnAngle, orbitSpeed, initialRadiusBoost);
		circleDummy.Position = vector;
		circleDummy.Direction = base.AimDirection;
		circleDummy.BulletManager = BulletManager;
		circleDummy.Initialize();
		m_circleDummies.Add(circleDummy);
		for (int i = 0; i < 21; i++)
		{
			float angle = SubdivideCircle(0f, 21, i);
			Vector2 vector2 = vector + BraveMathCollege.DegreesToVector(angle, 5f);
			Fire(Offset.OverridePosition(vector2), new CircleBullet(this, circleDummy, rotationSpeed, i, vector2 - vector));
		}
	}

	private void SpawnCircleDebris(float spawnRadius, float spawnAngle, float orbitSpeed, float tRadius, float deltaAngle, float? initialRadiusBoost = null)
	{
		float angle = spawnAngle + deltaAngle;
		float num = Mathf.LerpUnclamped(spawnRadius - 5f, spawnRadius + 5f, tRadius);
		Vector2 overridePosition = CenterPoint + BraveMathCollege.DegreesToVector(angle, num + ((!initialRadiusBoost.HasValue) ? 0f : initialRadiusBoost.Value));
		Fire(Offset.OverridePosition(overridePosition), new OrbitBullet(this, num, angle, orbitSpeed, initialRadiusBoost));
	}
}
