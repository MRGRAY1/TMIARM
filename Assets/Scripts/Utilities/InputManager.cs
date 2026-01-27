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



    private void Awake()
    {
        player_Inputs = new PlayerInputSystem();

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