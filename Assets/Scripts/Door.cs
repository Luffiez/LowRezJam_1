using UnityEngine;

public class Door : Interactable 
{
    public bool isLocked = true;
    public bool requiresKey = false;
    public string unlockedText;
    public string lockedText;
    public Sprite lockedSprite, unlockedSprite;
    public DoorConnection doorConnection;


    SpriteRenderer doorRenderer;

    private void Start()
    {  
        doorRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Show(string text)
    {
        if (isLocked)
            base.Show(text);
        else
            base.Show(unlockedText);
    }

    protected override void Interact()
    {
        Debug.Log("Interact with door");

        if (isLocked)
        {
            UnlockAndShowText();
        }
        else
            EnterDoor();
    }

    public void Unlock()
    {
        doorRenderer.sprite = unlockedSprite;
        isLocked = false;
    }

    void UnlockAndShowText()
    {
        if (requiresKey)// && !player.HasKey)
        {
            Show(lockedText);
            return;
        }

        Unlock();

        Show(unlockedText);
    }


    void EnterDoor()
    {
        RoomManager.instance.LoadRoom(doorConnection);
    }

    public override void ResetEntity()
    {
        isLocked = true;
        doorRenderer.sprite = lockedSprite;
    }
}

[System.Serializable]
public class DoorConnection
{
    public Room otherRoom;
    public Door otherDoor;
    public bool connectionIsToTheLeft;
}