using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public bool interact;

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
        interactAction.started += StartInteract;
        interactAction.canceled += CancelInteract;
    }

    private void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        playerMovement.MoveX(moveInput.x);
        playerMovement.MoveY(jump);
        
        // playerFire.Fire(fire);
    }

    #region Input Callbacks
    private void CancelInteract(InputAction.CallbackContext obj)
    {
        interact = false;
    }

    private void StartInteract(InputAction.CallbackContext obj)
    {
        interact = true;
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
