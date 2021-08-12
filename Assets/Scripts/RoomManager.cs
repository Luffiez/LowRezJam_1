using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance = null;
    public float animationSpeed = 2f;
    public Room currentRoom;
    Transform playerTransform;
    List<Room> rooms = new List<Room>();

    GameCamera gameCamera;
    PlayerShoot playerShoot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        gameCamera = FindObjectOfType<GameCamera>();

        rooms.AddRange(GetComponentsInChildren<Room>());
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerShoot = playerTransform.gameObject.GetComponent<PlayerShoot>();
    }

    public void LoadRoom(DoorConnection doorConnection)
    {
        StartCoroutine(LoadRoomAnimation(doorConnection));
    }

    IEnumerator LoadRoomAnimation(DoorConnection doorConnection)
    {
        //float xOffset = GetXOffsetToNewRoom(currentRoom, doorConnection.otherRoom, doorConnection.connectionIsToTheLeft);
        //float yOffset = playerTransform.position.y - doorConnection.otherDoor.transform.position.y;
        //Vector2 targetPos = new Vector2(xOffset, yOffset);
        //doorConnection.otherRoom.transform.position = targetPos;
        ScreenFade.instance.Fade(1);
        yield return new WaitForSeconds(0.3f);
        doorConnection.otherRoom.Show();


        playerTransform.position = doorConnection.otherDoor.transform.position;
        gameCamera.SetCameraAtTargetPosition();
        doorConnection.otherDoor.Unlock();
        doorConnection.otherRoom.entryPoint = doorConnection.otherDoor.transform;
        currentRoom.Hide();
        currentRoom = doorConnection.otherRoom;
        playerShoot.DisableBullets();
        yield return new WaitForSeconds(0.3f);

        ScreenFade.instance.Fade(-1);
    }

    float GetXOffsetToNewRoom(Room room, Room otherRoom, bool isToTheLeft)
    {
        float offset = 0;
        if(isToTheLeft)
            offset = room.roomCameraSettings.leftEdge - otherRoom.roomCameraSettings.rightEdge;
        else
            offset = room.roomCameraSettings.rightEdge + otherRoom.roomCameraSettings.leftEdge;

        return offset;
    }
}
