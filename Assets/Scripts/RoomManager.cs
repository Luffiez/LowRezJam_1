using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance = null;
    public Room currentRoom;
    Transform playerTransform;
    List<Room> rooms = new List<Room>();
    PlayerStats playerStats;

    bool isLoadingRoom = false;
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
        playerStats = playerTransform.gameObject.GetComponent<PlayerStats>();
    }

    public void LoadRoom(DoorConnection doorConnection)
    {
        if(!isLoadingRoom)
            StartCoroutine(LoadRoomAnimation(doorConnection));
    }

    public void ResetRoom()
    {
        if (!isLoadingRoom)
            StartCoroutine(ResetCurrentRoom());
    }

    IEnumerator LoadRoomAnimation(DoorConnection doorConnection)
    {
        isLoadingRoom = true;
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
        playerStats.ResetStats();
        ScreenFade.instance.Fade(-1);

        yield return new WaitForSeconds(0.2f);
        isLoadingRoom = false;
    }

    IEnumerator ResetCurrentRoom()
    {
        isLoadingRoom = true;
        ScreenFade.instance.Fade(1);
        yield return new WaitForSeconds(0.3f);

        currentRoom.Hide();
        currentRoom.Show();
        playerTransform.position = currentRoom.entryPoint.position;
        gameCamera.SetCameraAtTargetPosition();
        playerShoot.DisableBullets();
        playerStats.ResetStats();

        ScreenFade.instance.Fade(-1);
        yield return new WaitForSeconds(0.2f);
        isLoadingRoom = false;
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
