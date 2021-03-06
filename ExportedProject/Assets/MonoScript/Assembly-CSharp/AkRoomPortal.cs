using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider))]
[AddComponentMenu("Wwise/AkRoomPortal")]
public class AkRoomPortal : AkUnityEventHandler
{
	public const int MAX_ROOMS_PER_PORTAL = 2;

	private readonly AkVector extent = new AkVector();

	private readonly AkTransform portalTransform = new AkTransform();

	private ulong backRoomID = AkRoom.INVALID_ROOM_ID;

	public List<int> closePortalTriggerList = new List<int>();

	private ulong frontRoomID = AkRoom.INVALID_ROOM_ID;

	public AkRoom[] rooms = new AkRoom[2];

	public ulong GetID()
	{
		return (ulong)GetInstanceID();
	}

	protected override void Awake()
	{
		BoxCollider component = GetComponent<BoxCollider>();
		component.isTrigger = true;
		portalTransform.Set(component.bounds.center.x, component.bounds.center.y, component.bounds.center.z, base.transform.forward.x, base.transform.forward.y, base.transform.forward.z, base.transform.up.x, base.transform.up.y, base.transform.up.z);
		extent.X = component.size.x * base.transform.localScale.x / 2f;
		extent.Y = component.size.y * base.transform.localScale.y / 2f;
		extent.Z = component.size.z * base.transform.localScale.z / 2f;
		frontRoomID = ((!(rooms[1] == null)) ? rooms[1].GetID() : AkRoom.INVALID_ROOM_ID);
		backRoomID = ((!(rooms[0] == null)) ? rooms[0].GetID() : AkRoom.INVALID_ROOM_ID);
		RegisterTriggers(closePortalTriggerList, ClosePortal);
		base.Awake();
		if (closePortalTriggerList.Contains(1151176110))
		{
			ClosePortal(null);
		}
	}

	protected override void Start()
	{
		base.Start();
		if (closePortalTriggerList.Contains(1281810935))
		{
			ClosePortal(null);
		}
	}

	public override void HandleEvent(GameObject in_gameObject)
	{
		Open();
	}

	public void ClosePortal(GameObject in_gameObject)
	{
		Close();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		UnregisterTriggers(closePortalTriggerList, ClosePortal);
		if (closePortalTriggerList.Contains(-358577003))
		{
			ClosePortal(null);
		}
	}

	public void Open()
	{
		ActivatePortal(true);
	}

	public void Close()
	{
		ActivatePortal(false);
	}

	private void ActivatePortal(bool active)
	{
		if (base.enabled)
		{
			if (frontRoomID != backRoomID)
			{
				AkSoundEngine.SetRoomPortal(GetID(), portalTransform, extent, active, frontRoomID, backRoomID);
			}
			else
			{
				Debug.LogError(base.name + " is not placed/oriented correctly");
			}
		}
	}

	public void FindOverlappingRooms(AkRoom.PriorityList[] roomList)
	{
		BoxCollider component = base.gameObject.GetComponent<BoxCollider>();
		if (!(component == null))
		{
			Vector3 halfExtents = new Vector3(component.size.x * base.transform.localScale.x / 2f, component.size.y * base.transform.localScale.y / 2f, component.size.z * base.transform.localScale.z / 4f);
			FillRoomList(Vector3.forward * -0.25f, halfExtents, roomList[0]);
			FillRoomList(Vector3.forward * 0.25f, halfExtents, roomList[1]);
		}
	}

	private void FillRoomList(Vector3 center, Vector3 halfExtents, AkRoom.PriorityList list)
	{
		list.rooms.Clear();
		center = base.transform.TransformPoint(center);
		Collider[] array = Physics.OverlapBox(center, halfExtents, base.transform.rotation, -1, QueryTriggerInteraction.Collide);
		Collider[] array2 = array;
		foreach (Collider collider in array2)
		{
			AkRoom component = collider.gameObject.GetComponent<AkRoom>();
			if (component != null && !list.Contains(component))
			{
				list.Add(component);
			}
		}
	}

	public void SetFrontRoom(AkRoom room)
	{
		rooms[1] = room;
		frontRoomID = ((!(rooms[1] == null)) ? rooms[1].GetID() : AkRoom.INVALID_ROOM_ID);
	}

	public void SetBackRoom(AkRoom room)
	{
		rooms[0] = room;
		backRoomID = ((!(rooms[0] == null)) ? rooms[0].GetID() : AkRoom.INVALID_ROOM_ID);
	}

	public void UpdateOverlappingRooms()
	{
		AkRoom.PriorityList[] array = new AkRoom.PriorityList[2]
		{
			new AkRoom.PriorityList(),
			new AkRoom.PriorityList()
		};
		FindOverlappingRooms(array);
		for (int i = 0; i < 2; i++)
		{
			if (!array[i].Contains(rooms[i]))
			{
				rooms[i] = array[i].GetHighestPriorityRoom();
			}
		}
		frontRoomID = ((!(rooms[1] == null)) ? rooms[1].GetID() : AkRoom.INVALID_ROOM_ID);
		backRoomID = ((!(rooms[0] == null)) ? rooms[0].GetID() : AkRoom.INVALID_ROOM_ID);
	}
}
