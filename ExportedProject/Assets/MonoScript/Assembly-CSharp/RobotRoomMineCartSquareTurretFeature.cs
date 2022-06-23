using Dungeonator;
using UnityEngine;

public class RobotRoomMineCartSquareTurretFeature : RobotRoomFeature
{
	public override bool AcceptableInIdea(RobotDaveIdea idea, IntVector2 dim, bool isInternal, int numFeatures)
	{
		if (dim.x < 8 || dim.y < 8)
		{
			return false;
		}
		if (!idea.UseMineCarts)
		{
			return false;
		}
		return true;
	}

	public override void Develop(PrototypeDungeonRoom room, RobotDaveIdea idea, int targetObjectLayer)
	{
		for (int i = LocalBasePosition.x; i < LocalBasePosition.x + LocalDimensions.x; i++)
		{
			for (int j = LocalBasePosition.y; j < LocalBasePosition.y + LocalDimensions.y; j++)
			{
				PrototypeDungeonRoomCellData prototypeDungeonRoomCellData = room.ForceGetCellDataAtPoint(i, j);
				prototypeDungeonRoomCellData.state = CellType.FLOOR;
			}
		}
		int num = Random.Range(1, LocalDimensions.x / 2 - 2);
		int num2 = Random.Range(1, LocalDimensions.y / 2 - 2);
		IntVector2 intVector = LocalBasePosition + new IntVector2(num, num2);
		IntVector2 dimensions = LocalDimensions - new IntVector2(2 * num, 2 * num2);
		SerializedPath serializedPath = GenerateRectanglePath(intVector, dimensions);
		serializedPath.tilesetPathGrid = 0;
		room.paths.Add(serializedPath);
		DungeonPlaceableBehaviour mineCartTurretPrefab = RobotDave.GetMineCartTurretPrefab();
		PrototypePlacedObjectData prototypePlacedObjectData = PlaceObject(mineCartTurretPrefab, room, intVector, targetObjectLayer);
		prototypePlacedObjectData.assignedPathIDx = room.paths.Count - 1;
	}
}
