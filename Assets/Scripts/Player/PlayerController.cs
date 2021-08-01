using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.currentActionMap.FindAction("Move");

        jumpAction = playerInput.currentActionMap.FindAction("Jump");
        jumpAction.started += StartJump;
        jumpAction.canceled += CancelJump;

        fireAction = playerInput.currentActionMap.FindAction("Fire");
        fireAction.started += StartFire;
        fireAction.canceled += CancelFire;

        fireAction = playerInput.currentActionMap.FindAction("Interact");
        fireAction.started += StartInteract;
        fireAction.canceled += CancelInteract;
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        // playerMovement.Move(moveInput.x);
        // playerMovement.Jump(jump);
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
