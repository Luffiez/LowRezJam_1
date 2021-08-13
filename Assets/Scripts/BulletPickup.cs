using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : Interactable
{
    public AudioClip pickupClip;
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
        SoundManager.instance.PlaySfx(pickupClip);
        playerStats.IncreaseMaxBullets();
        Destroy(gameObject);
    }
}
