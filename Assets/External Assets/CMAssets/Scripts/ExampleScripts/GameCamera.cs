using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GameCamera : MonoBehaviour
{
	public float followSpeed = 1.5f;
	public Transform player;
	public Vector2 offset = Vector2.zero;
    public Vector2 freeRoamBox = Vector2.one;

	Vector2 targetPosition;
	Room currentRoom;

    void Start()
    {
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		if(RoomManager.instance)
			currentRoom = RoomManager.instance.currentRoom;
	}

    private void Update()
    {
		if (player != null)
		{
			Vector2 newPositions = transform.position;
			if(NeedsNewXPosition())
            {
				newPositions.x = player.position.x + offset.x;
            }
			if(NeedsNewYPosition())
            {
				newPositions.y = player.position.y + offset.y;
			}
			targetPosition = ClampToRoom(newPositions);
		}
	}

	void LateUpdate()
	{
		transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
	}

	private bool NeedsNewXPosition()
    {
		float xDiff = Mathf.Abs(transform.position.x - (player.position.x + offset.x));
		if (xDiff > freeRoamBox.x)
		{
			return true;
		}

		return false;
	}

	bool NeedsNewYPosition()
    {
		float yDiff = Mathf.Abs(transform.position.y - (player.position.y + offset.y));
        if (yDiff > freeRoamBox.y)
        {
			return true;
        }

		return false;
    }

	Vector2 ClampToRoom(Vector2 vec)
    {
		// TODO: read Tilemap values, largest/smallest y/x tile positions instead?
		vec.x = Mathf.Clamp(vec.x, currentRoom.roomCameraSettings.minX, currentRoom.roomCameraSettings.maxX);
		vec.y = Mathf.Clamp(vec.y, currentRoom.roomCameraSettings.minY, currentRoom.roomCameraSettings.maxY);
		return vec;
	}

    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(transform.position, freeRoamBox);
    }
}
