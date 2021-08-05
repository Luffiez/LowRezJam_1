using UnityEngine;

public class Key : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Player picked up the key");
        Destroy(gameObject);
    }
}
