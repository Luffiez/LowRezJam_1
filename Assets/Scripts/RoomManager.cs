using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance = null;
    Transform playerTransform;
    public Room currentRoom;
    List<Room> rooms = new List<Room>();
    PlayerShoot playerShoot;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        rooms.AddRange(GetComponentsInChildren<Room>());
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerShoot = playerTransform.gameObject.GetComponent<PlayerShoot>();
    }

    public void LoadRoom(DoorConnection doorConnection)
    {
        doorConnection.otherRoom.Show();
        playerTransform.position = doorConnection.otherDoor.transform.position;
        currentRoom.Hide();
        currentRoom = doorConnection.otherRoom;
        playerShoot.DisableBullets();

        // TODO: play fancy transition animation.
    }
}
