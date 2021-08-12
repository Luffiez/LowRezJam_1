using UnityEngine;

public class Door : Interactable 
{
    public bool isLocked = true;
    [Tooltip("Key or Enemies slain")]
    public bool requiresUnlock = false;
    public string unlockedText;
    public string lockedText;
    public Sprite lockedSprite, unlockedSprite;
    public DoorConnection doorConnection;


    SpriteRenderer doorRenderer;

    protected override void Awake()
    {
        base.Awake();
        doorRenderer = GetComponentInChildren<SpriteRenderer>();
        if (isLocked)
            doorRenderer.sprite = lockedSprite;
        else
            doorRenderer.sprite = unlockedSprite;
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
        if(!doorRenderer)
            doorRenderer = GetComponentInChildren<SpriteRenderer>();

        doorRenderer.sprite = unlockedSprite;
        isLocked = false;
    }

    void UnlockAndShowText()
    {
        if (requiresUnlock)// && !player.HasKey)
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