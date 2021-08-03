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
    [Tooltip("The Smallest and larget x-values on the map.")]
    public float minX, maxX;
    [Tooltip("The Smallest and larget y-values on the map.")]
    public float minY, maxY;
}
