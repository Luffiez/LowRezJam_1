using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public Vector2 textOffset;
    public string interactableText;

    protected InteractableUI interactableUI;
    protected bool isInteractable = false;

    PlayerController playerController;

    private void Awake()
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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInteractable = true;
            interactableUI.Show(transform.position, textOffset, interactableText);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInteractable = false;
            interactableUI.Hide();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Vector3)textOffset, Vector3.one);
    }
}
