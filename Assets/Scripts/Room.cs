using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomCameraSettings roomCameraSettings;
    List<Entity> entities = new List<Entity>();
    public Transform entryPoint;

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
        transform.position = new Vector2(0, 0);
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
