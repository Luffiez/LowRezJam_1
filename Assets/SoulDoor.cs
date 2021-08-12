using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDoor : Door
{
    Room room;

    private void Awake()
    {
        room = GetComponentInParent<Room>();

    }

    protected override void Show(string text)
    {
        int enemiesAlive = room.EnemiesAlive();
        if (enemiesAlive == 0)
        {
            isLocked = false;
        }
        else
        {
            interactableText = enemiesAlive + "/" + room.Enemies.Count;
            lockedText= enemiesAlive + "/" + room.Enemies.Count;
        }

        base.Show(text);
    }
}
