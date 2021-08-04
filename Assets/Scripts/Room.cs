using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomCameraSettings roomCameraSettings;
    // List<Enemy>

    internal void Show()
    {
        gameObject.SetActive(true);
    }

    internal void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ResetRoomEntities()
    {
        // TODO: foreach(enemy) => enemy.Reset();
    }
}

[System.Serializable]
public class RoomCameraSettings
{
    [Tooltip("The values for the rooms edges. Used for camera clamping.")]
    public float leftEdge, rightEdge, bottomEdge, topEdge;
}
