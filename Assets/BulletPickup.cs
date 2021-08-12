using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : Interactable
{
    PlayerStats playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    protected override void Interact()
    {
        Pickup();
    }

    void Pickup()
    {
        // TODO: display pickup message?
        playerStats.IncreaseMaxBullets();
        Destroy(gameObject);
    }
}
