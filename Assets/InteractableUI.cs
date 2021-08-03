using TMPro;
using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    TMP_Text interactableText;
    Camera cam;

    Vector2 targetPos;
    Vector2 currentOffset;

    private void Start()
    {
        interactableText = GetComponent<TMP_Text>();
        cam = Camera.main;
    }

    private void Update()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        transform.position = cam.WorldToScreenPoint(targetPos + currentOffset); ;
    }

    public void Show(Vector2 target, Vector2 offset, string text)
    {
        targetPos = target;
        currentOffset = offset;
        SetPosition();
        interactableText.text = text;
        interactableText.enabled = true;
    }

    public void Hide()
    {
        interactableText.enabled = false;
    }

}
