using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    public PlayerInputSystem playerInputs;

    public string currentMappings { get; private set; }

    [SerializeField] private Managers managers;

    private void Awake()
    {
        currentMappings = "temp";

        playerInputs = new PlayerInputSystem();

        // Gameplay inputs
        playerInputs.Main.Jump.started += ctx => GameEvents.PlayerJump?.Invoke(this);
        playerInputs.Main.Interaction.started += ctx => GameEvents.PlayerInteract?.Invoke(this);
        playerInputs.Main.Scroll.started += ScrollInventory;
        playerInputs.Main.One.started += _ => HotBarSelection(0);
        playerInputs.Main.Two.started += _ => HotBarSelection(1);
        playerInputs.Main.Three.started += _ => HotBarSelection(2);
        playerInputs.Main.Four.started += _ => HotBarSelection(3);
        playerInputs.Main.Five.started += _ => HotBarSelection(4);
        playerInputs.Main.Six.started += _ => HotBarSelection(5);
        playerInputs.Main.Phone.started += TogglePhoneOverlay;

        // Menu button (ESC / Start)
        playerInputs.Player_Always_Active.Cancel.started += ctx => UIEvents.PausedPressedEvent(this);

        //Testing
        //playerInputs.Debug_Always_Active.Test.started += ctx => Managers.Instance.GameManager.SetState(GameState.MainMenu);
        playerInputs.Debug_Always_Active.Test.started += ctx => DebugEvents.DebugNotificationMessage?.Invoke(this, "Test Notification from InputManager!");
        playerInputs.Debug_Always_Active.ToggleOverlay.started += ctx => DebugEvents.ToggleDebugOverlay?.Invoke(this);
    }

    private void TogglePhoneOverlay(InputAction.CallbackContext obj)
    {
        Managers.Instance.GameManager.IsPhoneShown = !Managers.Instance.GameManager.IsPhoneShown;
        UIEvents.TogglePhoneEvent?.Invoke(this);
    }


    private void OnEnable()
    {
        // Start with everything disabled and then enable the proper map
        playerInputs.Disable();
        playerInputs.Debug_Always_Active.Enable();
        SystemEvents.GameStateChanged += OnGameStateChanged;

        // Apply the correct action-map for the current game state immediately
        if (Managers.Instance != null && Managers.Instance.GameManager != null)
        {
            OnGameStateChanged(this, Managers.Instance.GameManager.CurrentState);
        }
    }


    private void ScrollInventory(InputAction.CallbackContext obj)
    {
        float value = obj.ReadValue<Vector2>().y;
        UIEvents.PlayerScroll?.Invoke(this, value);
    }

    private void HotBarSelection(int value)
    {
        UIEvents.HotBarSelected?.Invoke(this, (int)value);
    }

    private void OnDisable()
    {
        SystemEvents.GameStateChanged -= OnGameStateChanged;
        playerInputs.Debug_Always_Active.Disable();

        playerInputs.Disable();
    }

    private void OnGameStateChanged(object sender, GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                SwitchToMain();
                break;

            case GameState.Pause:
            case GameState.MainMenu:
                SwitchToUI();
                break;
        }
    }

    public Vector2 GetMovementVectorNormalized()
    {
        if (!playerInputs.Main.enabled ||
            Managers.Instance.GameManager.IsPhoneShown)
            return Vector2.zero;

        return playerInputs.Main.Movement.ReadValue<Vector2>().normalized;
    }

    public void SwitchToUI()
    {
        playerInputs.Main.Disable();
        playerInputs.UI.Enable();
        currentMappings = "UI";
    }

    public void SwitchToMain()
    {
        playerInputs.UI.Disable();
        playerInputs.Main.Enable();
        currentMappings = "Main";
    }
}