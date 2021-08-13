using UnityEngine;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public AudioClip mouseEnterClip;
    public AudioClip mouseClickClip;

    SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.instance;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (soundManager && mouseEnterClip)
            soundManager.PlaySfx(mouseEnterClip);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (soundManager && mouseClickClip)
            soundManager.PlaySfx(mouseClickClip);
    }
}
