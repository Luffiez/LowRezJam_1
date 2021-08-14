using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInteract : Interactable
{
    protected override void Interact()
    {
        transform.parent.gameObject.SetActive(false);
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
