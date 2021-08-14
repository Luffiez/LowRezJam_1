using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenRoom : MonoBehaviour
{
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
}
