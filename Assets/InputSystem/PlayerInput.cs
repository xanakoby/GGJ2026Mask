using DesignPatterns.Generics;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : Singleton<PlayerInput>, PlayerControls.IPlayerInputActions
{
    public float MovementX { get; private set; }
    public float MovementY { get; private set; }

    public Action OnPlayerMoveAction;
    public Action OnPlayerStandAction;
    public Action OnPlayerJumpAction;
    public Action OnPlayerStopHoldJumpAction;
    public Action OnPauseAction;
    public Action OnInteractionAction;
    public Action OnHoldSwitchMask;
    public Action OnUnHoldSwitchMask;

    public PlayerControls controllers;

    public override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {
        if (controllers == null)
        {
            controllers = new PlayerControls();
        }
        controllers.PlayerInput.SetCallbacks(this);
        controllers.PlayerInput.Enable();
    }
    private void OnDisable()
    {
        if (controllers != null)
        {
            controllers.PlayerInput.SetCallbacks(null);
            controllers.PlayerInput.Disable();
        }
    }

    private void OnDestroy()
    {
        controllers.PlayerInput.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 movement = context.ReadValue<Vector2>();
            MovementX = movement.x;
            MovementY = movement.y;
            OnPlayerMoveAction?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            MovementX = 0f;
            MovementY = 0f;
            OnPlayerStandAction?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnPlayerJumpAction?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            OnPlayerStopHoldJumpAction?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnInteractionAction?.Invoke();
        }
    }

    public void OnChangeMask(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnHoldSwitchMask?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            OnUnHoldSwitchMask?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnPauseAction?.Invoke();
        }
    }
}
