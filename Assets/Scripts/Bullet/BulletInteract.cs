using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInteract : Interactable
{
    // Start is called before the first frame update


    protected override void Interact()
    {
        transform.parent.gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
