using UnityEngine;

public class Room : MonoBehaviour
{

    // List<Enemy>

    internal void Show()
    {
        gameObject.SetActive(true);
    }

    internal void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ResetRoomEntities()
    {
        // TODO: foreach(enemy) => enemy.Reset();
    }
}
