using System.Collections;
using TMPro;
using UnityEngine;

public class UIRoomName : MonoBehaviour
{
    RoomManager roomManager;
    TMP_Text roomNameText;

    CanvasGroup canvasGroup;

    private void Awake()
    {
        roomNameText = GetComponent<TMP_Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        roomManager = RoomManager.instance;
        UpdateRoomName();
    }

    public void UpdateRoomName()
    {
        roomNameText.text = roomManager.currentRoom.roomName;
        StartCoroutine(FadeOutName());
    }

    IEnumerator FadeOutName()
    {
        canvasGroup.alpha = 1;
        yield return new WaitForSeconds(2f);

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 0.75f;
            yield return new WaitForFixedUpdate();
        }
    }
}
