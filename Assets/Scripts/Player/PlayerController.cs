using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerMovement playerMovement;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction fireAction;
    InputAction interactAction;

    public Vector2 moveInput;
    public bool jump;
    public bool fire;
    public UnityEvent InteractEvent = new UnityEvent();

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.currentActionMap.FindAction("Move");

        jumpAction = playerInput.currentActionMap.FindAction("Jump");
        jumpAction.started += StartJump;
        jumpAction.canceled += CancelJump;

        fireAction = playerInput.currentActionMap.FindAction("Fire");
        fireAction.started += StartFire;
        fireAction.canceled += CancelFire;

        interactAction = playerInput.currentActionMap.FindAction("Interact");
        interactAction.performed += PerformedInteraction;
    }

    private void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        playerMovement.MoveX(moveInput.x);
        playerMovement.MoveY(jump);
        
        // playerFire.Fire(fire);
    }

    #region Input Callbacks

    private void PerformedInteraction(InputAction.CallbackContext obj)
    {
        if(InteractEvent != null)
        {
            Debug.Log("Interact");
            InteractEvent.Invoke();
        }
    }

    private void CancelFire(InputAction.CallbackContext obj)
    {
        fire = false;
    }

    private void StartFire(InputAction.CallbackContext obj)
    {
        fire = true;
    }

    private void CancelJump(InputAction.CallbackContext obj)
    {
        jump = false;
    }

    private void StartJump(InputAction.CallbackContext obj)
    {
        jump = true;
    }
    #endregion
}
