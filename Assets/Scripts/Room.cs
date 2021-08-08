using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomCameraSettings roomCameraSettings;
    List<Entity> entities = new List<Entity>();

    private void Start()
    {
        entities.AddRange(GetComponentsInChildren<Entity>());
    }

    internal void Show()
    {
        gameObject.SetActive(true);
    }

    internal void Hide()
    {
        ResetRoomEntities();
        gameObject.SetActive(false);
    }

    private void ResetRoomEntities()
    {
        Debug.LogError("Reset Enteties");
        foreach (Entity entity in entities)
        {
            entity.ResetEntity();
        }
    }
}

[System.Serializable]
public class RoomCameraSettings
{
    [Tooltip("The values for the rooms edges. Used for camera clamping.")]
    public float leftEdge, rightEdge, bottomEdge, topEdge;
}
