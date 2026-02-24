using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
        playerInputs.Main.Inventory.started += ctx => GameEvents.OpenInventory?.Invoke(this);

        // Menu button (ESC / Start)
        playerInputs.Main.Menu.started += ctx => Managers.Instance.GameManager.TogglePause();
        playerInputs.UI.Cancel.started += ctx => Managers.Instance.GameManager.TogglePause();

        //Testing
        playerInputs.Debug_Always_Active.Test.started += ctx => Managers.Instance.GameManager.SetState(GameState.MainMenu);
        //playerInputs.Debug_Always_Active.Test.started += ctx => GameEvents.NotificationMessage?.Invoke(this, "Test Notification from InputManager!");
        playerInputs.Debug_Always_Active.ToggleOverlay.started += ctx => GameEvents.ToggleDebugOverlay?.Invoke(this);
    }



    private void OnEnable()
    {
        // Start with everything disabled and then enable the proper map
        playerInputs.Disable();
        playerInputs.Debug_Always_Active.Enable();
        GameEvents.GameStateChanged += OnGameStateChanged;

        // Apply the correct action-map for the current game state immediately
        if (Managers.Instance != null && Managers.Instance.GameManager != null)
        {
            OnGameStateChanged(this, Managers.Instance.GameManager.CurrentState);
        }
    }

    private void OnDisable()
    {
        GameEvents.GameStateChanged -= OnGameStateChanged;
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
        if (!playerInputs.Main.enabled)
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