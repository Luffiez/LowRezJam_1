
public class SoulDoor : Door
{
    Room room;

    protected override void Awake()
    {
        base.Awake();
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
            string enemiesLeft = enemiesAlive + "/" + room.Enemies.Count + " enemies left.";
            interactableText = enemiesLeft;
            lockedText= enemiesLeft;
        }

        base.Show(text);
    }
}
