using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomCameraSettings roomCameraSettings;
    List<Entity> entities = new List<Entity>();
    List<Enemy> enemies = new List<Enemy>();
    public Transform entryPoint;

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
        transform.position = new Vector2(0, 0);
        ResetRoomEntities();
        gameObject.SetActive(false);
    }

    private void ResetRoomEntities()
    {
        // make sure entity list does not have any items that are null.
        entities.RemoveAll(item => item == null);
        foreach (Entity entity in entities)
        {
            entity.ResetEntity();
        }
    }

    internal void Reset()
    {
        StartCoroutine(ResetRoom());
    }

    IEnumerator ResetRoom()
    {
        ScreenFade.instance.Fade(1);
        yield return new WaitForSeconds(0.25f);
        ResetRoomEntities();
        GameObject.FindGameObjectWithTag("Player").transform.position = entryPoint.position;
        ScreenFade.instance.Fade(-1);
    }
}

[System.Serializable]
public class RoomCameraSettings
{
    [Tooltip("The values for the rooms edges. Used for camera clamping.")]
    public float leftEdge, rightEdge, bottomEdge, topEdge;
}
