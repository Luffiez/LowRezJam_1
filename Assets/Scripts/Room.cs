using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomCameraSettings roomCameraSettings;
    List<Entity> entities = new List<Entity>();
    List<Enemy> enemies = new List<Enemy>();

    public List<Enemy> Enemies { get => enemies; }

    private void Awake()
    {
        entities.AddRange(GetComponentsInChildren<Entity>());
        foreach (Entity entity in entities)
        {
            if(entity is Enemy)
            {
                Enemies.Add(entity as Enemy);
            }
        }
    }

    public int EnemiesAlive()
    {
        int amount = enemies.Count;
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.gameObject.activeSelf)
                amount--;
        }
        return amount;
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
