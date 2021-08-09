using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerShoot playerShoot;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction fireAction;
    InputAction interactAction;

    public bool canMove = true;

    public Vector2 moveInput;
    public bool jump;
    public bool fire;
    public UnityEvent InteractEvent = new UnityEvent();

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        playerShoot = GetComponent<PlayerShoot>();

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
        if (!canMove)
        {
            if (moveInput != Vector2.zero)
                moveInput = Vector2.zero;
            return;
        }

        moveInput = moveAction.ReadValue<Vector2>();

        playerMovement.MoveX(moveInput.x);
        playerMovement.MoveY(jump);
        playerShoot.Shoot(fire);
    }


    #region Input Callbacks

    private void PerformedInteraction(InputAction.CallbackContext obj)
    {
        if(InteractEvent != null)
        {
            //Debug.Log("Interact");
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
