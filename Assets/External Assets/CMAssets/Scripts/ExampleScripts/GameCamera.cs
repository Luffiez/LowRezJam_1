using UnityEngine;

public class GameCamera : MonoBehaviour
{
	public float followSpeed = 1.5f;
	public Transform player;
	public Vector2 offset = Vector2.zero;
    public Vector2 freeRoamBox = Vector2.one;
	public bool followPlayer = true;
	public float maxDistanceBeforeTeleport = 5f;

	Vector2 targetPosition;
	RoomManager roomManager;

    void Start()
    {
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		if(RoomManager.instance)
			roomManager = RoomManager.instance;
	}

    private void Update()
    {
		if (player != null)
		{
			//Vector2 newPosition = transform.position;
			if(NeedsNewXPosition())
            {
				targetPosition.x = player.position.x + offset.x;
            }
			if(NeedsNewYPosition())
            {
				targetPosition.y = player.position.y + offset.y;
			}

			if (Vector2.Distance(transform.position, targetPosition) > maxDistanceBeforeTeleport)
				SetCameraAtTargetPosition();
		}
	}

	void LateUpdate()
	{
		if(followPlayer)
        {
			transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
			ClampToRoom();
        }
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

    public void SetCameraAtTargetPosition()
    {
        transform.position = targetPosition;
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

	void ClampToRoom()
    {
		Vector2 vec = transform.position;
		// TODO: read Tilemap values, largest/smallest y/x tile positions instead?

		// adding/subtracting 4 to each side since camera transform is in the center, edges will be +/- 4.
		if (vec.x - 4 < roomManager.currentRoom.roomCameraSettings.leftEdge)
			vec.x = roomManager.currentRoom.roomCameraSettings.leftEdge + 4;
		else if (vec.x + 4 > roomManager.currentRoom.roomCameraSettings.rightEdge)
			vec.x = roomManager.currentRoom.roomCameraSettings.rightEdge - 4;

        if (vec.y - 4 < roomManager.currentRoom.roomCameraSettings.bottomEdge)
            vec.y = roomManager.currentRoom.roomCameraSettings.bottomEdge + 4;
        else if (vec.y + 4 > roomManager.currentRoom.roomCameraSettings.topEdge)
            vec.y = roomManager.currentRoom.roomCameraSettings.topEdge - 4;

		transform.position = vec;
	}

    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(transform.position, freeRoamBox);
    }
}
