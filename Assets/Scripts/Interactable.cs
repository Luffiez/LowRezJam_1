using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : Entity
{
    public Vector2 textOffset;
    public string interactableText;

    protected InteractableUI interactableUI;
    protected bool isInteractable = false;

    PlayerController playerController;

    private void Start()
    {
        interactableUI = FindObjectOfType<InteractableUI>();
    }

    private void OnEnable()
    {
        if (!playerController)
            playerController = FindObjectOfType<PlayerController>();
        
        playerController.InteractEvent.AddListener(TryInteract);
    }

    private void OnDisable()
    {
        if(isInteractable)
        {
            isInteractable = false;
            interactableUI.Hide();
        }
        if (playerController)
            playerController.InteractEvent.RemoveListener(Interact);
    }

    private void TryInteract()
    {
        if (isInteractable)
            Interact();
    }

    protected virtual void Interact()
    {
        Debug.Log("Interacted!");
    }

    protected virtual void Show(string text)
    {
        isInteractable = true;
        interactableUI.Show(transform.position, textOffset, text);
    }

    protected virtual void Hide()
    {
        isInteractable = false;
        interactableUI.Hide();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Show(interactableText);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Hide();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)textOffset, Vector3.one);
    }
}
