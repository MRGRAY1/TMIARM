using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<object> PlayerMovement;
    public static Action<object> PlayerJump;
    public static Action<object> PlayerDash;
    public static Action<object> PlayerSprint;
    public static Action<object> PlayerInteract;
    public static Action<object> OpenInventory;
}

public static class UIEvents
{
    public static Action<object> PlayGamePressed;
    public static Action<object> ExitPressed;
    public static Action<object, string> NotificationMessage;
    public static Action<object> OnSettingsClickedEvent;
    public static Action<object> OnBackClickedEvent;
    public static Action<object> PausedPressedEvent;
    public static Action<object, string> CurrentViewChanged;
}

public static class SystemEvents
{
    public static Action<object> GoToMainMenuEvent;
    public static Action<object> ApplicationQuit;
    public static Action<object> ApplicationFocusChanged;
    public static Action<object> ApplicationPauseChanged;
    public static Action<object, GameState> GameStateChanged;
    public static Action<object, GameScenes> GameSceneChanged;
    public static Action<object, bool> TogglePause;
}

public static class AudioEvents
{
    public static Action<object, string> PlaySoundEffect;
    public static Action<object, string> PlayMusic;
    public static Action<object, string> StopMusic;
    public static Action<object, float> SetMasterVolume;
    public static Action<object, float> SetSFXVolume;
    public static Action<object, float> SetMusicVolume;
}

public static class DebugEvents
{
    public static Action<object, string> DebugNotificationMessage;
    public static Action<object> ToggleDebugOverlay;
}

public static class SceneEvents
{
    // Sequence events:
    public static Action ExitApplication;
    public static Action PreloadCompleted;
    public static Action<float> LoadProgressUpdated;

    // Scene management events
    public static Action ReloadScene;
    public static Action LoadNextScene;

    // Additively load the scene with the given path
    public static Action<string> LoadSceneByPath;

    // Additively unload the scene with the given path
    public static Action<string> UnloadSceneByPath;

    // Additively load the scene with given index
    public static Action<int> SceneIndexLoaded;

    // Unload of the previous scene is complete
    public static Action LastSceneUnloaded;
}