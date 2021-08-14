using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRoom : Entity
{
    public bool triggerOnReset = false;
    public GameObject[] objectsToShow;
    public GameObject[] objectsToHide;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
         
        foreach (var item in objectsToShow)
        {
            if (item != null)
                item.SetActive(true);
        }

        foreach (var item in objectsToHide)
        {
            if(item != null)
                item.SetActive(false);
        }
    }

    public override void ResetEntity()
    {
        Debug.Log("Reset hidden room!");
        if(triggerOnReset)
        {
            foreach (var item in objectsToShow)
            {
                if (item != null)
                    item.SetActive(false);
            }

            foreach (var item in objectsToHide)
            {
                if (item != null)
                    item.SetActive(true);
            }
        }
    }
}
