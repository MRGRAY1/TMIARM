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
    [SerializeField]
    private EventIndex _inventoryEvent;

    private bool IsMenuOpen;

    private void Awake()
    {
        player_Inputs = new PlayerInputSystem();
        player_Inputs.Main.Menu.performed += MenuEvent;
        player_Inputs.Main.Jump.started += JumpEvent;
        player_Inputs.Main.Interaction.started += InteractionEvent;
        player_Inputs.Main.Inventory.started += InventoryEvent;


    }

    private void InventoryEvent(InputAction.CallbackContext context)
    {
        GameEvents.OpenInventory?.Invoke();

    }

    private void InteractionEvent(InputAction.CallbackContext context)
    {
        GameEvents.Interact?.Invoke();
    }

    private void JumpEvent(InputAction.CallbackContext context)
    {
        GameEvents.Jump?.Invoke();
    }

    private void MenuEvent(InputAction.CallbackContext context)
    {
        if (!IsMenuOpen)
        {
            player_Inputs.Main.Disable();
            //player_Inputs.UI.Enable();
        }
        else
        {
            player_Inputs.Main.Enable();
            //player_Inputs.UI.Disable();
        }
        IsMenuOpen = !IsMenuOpen;
        GameEvents.OpenMenu?.Invoke();

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