using System;
using UnityEngine;

public static class GameEvents
{
    // Receiev object for debugging purposes
    // Menu Events
    public static Action<object> PlayGamePressed;
    public static Action<object> OpenInventory;
    public static Action<object> ExitPressed;
    public static Action<object, string> NotificationMessage;
    public static Action<object> OnSettingsClickedEvent;
    public static Action<object> OnBackClickedEvent;
    public static Action<object> PausedPressedEvent;
    public static Action<object> GoToMainMenuEvent;


    public static Action<object, string> CurrentViewChanged;

    // Player Events
    public static Action<object> PlayerMovement;
    public static Action<object> PlayerJump;
    public static Action<object> PlayerDash;
    public static Action<object> PlayerSprint;
    public static Action<object> PlayerInteract;

    // Game Manager Events 
    // ONLY GAME MANAGER EVENTS!!!!
    public static Action<object, GameState> GameStateChanged;
    public static Action<object, GameScenes> GameSceneChanged;
    public static Action<object, bool> TogglePause;

    // Debug Events
    public static Action<object, string> DebugNotificationMessage;
    public static Action<object> ToggleDebugOverlay;
}
