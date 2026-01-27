using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    private PlayerInputSystem player_Inputs;

    private Vector2 inputVector = new Vector2(0, 0);
    [SerializeField]
    private EventIndex _menuEvent;
    [SerializeField]
    private EventIndex _jumpEvent;
    [SerializeField]
    private EventIndex _interactEvent;



    private void Awake()
    {
        player_Inputs = new PlayerInputSystem();
        player_Inputs.Main.Menu.performed += MenuEvent;
        player_Inputs.Main.Jump.started += JumpEvent;
        player_Inputs.Main.Interaction.started += InteractionEvent;

    }

    private void InteractionEvent(InputAction.CallbackContext context)
    {
        EventBus.Publish<bool>(_interactEvent, true);
    }

    private void JumpEvent(InputAction.CallbackContext context)
    {
        EventBus.Publish<bool>(_jumpEvent, true);
    }

    private void MenuEvent(InputAction.CallbackContext context)
    {
        EventBus.Publish<bool>(_menuEvent, true);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        inputVector = player_Inputs.Main.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
    private void OnEnable()
    {
        player_Inputs.Enable();
    }
    private void OnDisable()
    {
        player_Inputs.Disable();
    }
}