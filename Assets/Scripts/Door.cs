using UnityEngine;

public class Door : Interactable
{
    public DoorConnection doorConnection;

    protected override void Interact()
    {
        Debug.Log("Open door");
        RoomManager.instance.LoadRoom(doorConnection);
    }
}

[System.Serializable]
public class DoorConnection
{
    public Room otherRoom;
    public Door otherDoor;
}