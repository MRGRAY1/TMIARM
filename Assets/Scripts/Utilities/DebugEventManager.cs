using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugEventManager : MonoBehaviour
{
    [SerializeField] private List<EventLogEntry> eventsFired = new List<EventLogEntry>();

    private void OnEnable()
    {
        eventsFired.Clear();
        // ----- Game Events -----
        GameEvents.PlayerMovement += (object sender) => PrintDebug("PlayerMovement", sender);
        GameEvents.PlayerJump += (object sender) => PrintDebug("PlayerJump", sender);
        GameEvents.PlayerDash += (object sender) => PrintDebug("PlayerDash", sender);
        GameEvents.PlayerSprint += (object sender) => PrintDebug("PlayerSprint", sender);
        GameEvents.PlayerInteract += (object sender) => PrintDebug("PlayerInteract", sender);
        GameEvents.OpenInventory += (object sender) => PrintDebug("OpenInventory", sender);

        // ----- UI Events -----
        UIEvents.PlayGamePressed += (object sender) => PrintDebug("PlayGamePressed", sender);
        UIEvents.ExitPressed += (object sender) => PrintDebug("ExitPressed", sender);
        UIEvents.NotificationMessage += (object sender, string msg) => PrintDebug($"NotificationMessage {msg}", sender);
        UIEvents.OnSettingsClickedEvent += (object sender) => PrintDebug("OnSettingsClickedEvent", sender);
        UIEvents.OnBackClickedEvent += (object sender) => PrintDebug("OnBackClickedEvent", sender);
        UIEvents.PausedPressedEvent += (object sender) => PrintDebug("PausedPressedEvent", sender);
        UIEvents.CurrentViewChanged += (object sender, string viewName) => PrintDebug($"CurrentViewChanged {viewName}", sender);

        // ----- System Events -----
        SystemEvents.GoToMainMenuEvent += (object sender) => PrintDebug("GoToMainMenuEvent", sender);
        SystemEvents.ApplicationQuit += (object sender) => PrintDebug("ApplicationQuit", sender);
        SystemEvents.ApplicationFocusChanged += (object sender) => PrintDebug("ApplicationFocusChanged", sender);
        SystemEvents.ApplicationPauseChanged += (object sender) => PrintDebug("ApplicationPauseChanged", sender);
        SystemEvents.GameStateChanged += (object sender, GameState state) => PrintDebug($"GameStateChanged {state}", sender);
        SystemEvents.GameSceneChanged += (object sender, GameScenes scene) => PrintDebug($"GameSceneChanged {scene}", sender);
        SystemEvents.TogglePause += (object sender, bool paused) => PrintDebug($"TogglePause {paused}", sender);

        // ----- Audio Events -----
        AudioEvents.PlaySoundEffect += (object sender, string clip) => PrintDebug($"PlaySoundEffect {clip}", sender);
        AudioEvents.PlayMusic += (object sender, string clip) => PrintDebug($"PlayMusic {clip}", sender);
        AudioEvents.StopMusic += (object sender, string clip) => PrintDebug($"StopMusic {clip}", sender);
        AudioEvents.SetMasterVolume += (object sender, float vol) => PrintDebug($"SetMasterVolume {vol}", sender);
        AudioEvents.SetSFXVolume += (object sender, float vol) => PrintDebug($"SetSFXVolume {vol}", sender);
        AudioEvents.SetMusicVolume += (object sender, float vol) => PrintDebug($"SetMusicVolume {vol}", sender);

        // ----- Debug Events -----
        DebugEvents.DebugNotificationMessage += (object sender, string msg) => PrintDebug($"DebugNotificationMessage {msg}", sender);
        DebugEvents.ToggleDebugOverlay += (object sender) => PrintDebug("ToggleDebugOverlay", sender);
    }

    private void OnDisable()
    {
        // ----- Game Events -----
        GameEvents.PlayerMovement -= (object sender) => PrintDebug("PlayerMovement", sender);
        GameEvents.PlayerJump -= (object sender) => PrintDebug("PlayerJump", sender);
        GameEvents.PlayerDash -= (object sender) => PrintDebug("PlayerDash", sender);
        GameEvents.PlayerSprint -= (object sender) => PrintDebug("PlayerSprint", sender);
        GameEvents.PlayerInteract -= (object sender) => PrintDebug("PlayerInteract", sender);
        GameEvents.OpenInventory -= (object sender) => PrintDebug("OpenInventory", sender);

        // ----- UI Events -----
        UIEvents.PlayGamePressed -= (object sender) => PrintDebug("PlayGamePressed", sender);
        UIEvents.ExitPressed -= (object sender) => PrintDebug("ExitPressed", sender);
        UIEvents.NotificationMessage -= (object sender, string msg) => PrintDebug($"NotificationMessage {msg}", sender);
        UIEvents.OnSettingsClickedEvent -= (object sender) => PrintDebug("OnSettingsClickedEvent", sender);
        UIEvents.OnBackClickedEvent -= (object sender) => PrintDebug("OnBackClickedEvent", sender);
        UIEvents.PausedPressedEvent -= (object sender) => PrintDebug("PausedPressedEvent", sender);
        UIEvents.CurrentViewChanged += (object sender, string viewName) => PrintDebug($"CurrentViewChanged {viewName}", sender);

        // ----- System Events -----
        SystemEvents.GoToMainMenuEvent += (object sender) => PrintDebug("GoToMainMenuEvent", sender);
        SystemEvents.ApplicationQuit -= (object sender) => PrintDebug("ApplicationQuit", sender);
        SystemEvents.ApplicationFocusChanged -= (object sender) => PrintDebug("ApplicationFocusChanged", sender);
        SystemEvents.ApplicationPauseChanged -= (object sender) => PrintDebug("ApplicationPauseChanged", sender);
        SystemEvents.GameStateChanged -= (object sender, GameState state) => PrintDebug($"GameStateChanged {state}", sender);
        SystemEvents.GameSceneChanged -= (object sender, GameScenes scene) => PrintDebug($"GameSceneChanged {scene}", sender);
        SystemEvents.TogglePause -= (object sender, bool paused) => PrintDebug($"TogglePause {paused}", sender);

        // ----- Audio Events -----
        AudioEvents.PlaySoundEffect -= (object sender, string clip) => PrintDebug($"PlaySoundEffect {clip}", sender);
        AudioEvents.PlayMusic -= (object sender, string clip) => PrintDebug($"PlayMusic {clip}", sender);
        AudioEvents.StopMusic -= (object sender, string clip) => PrintDebug($"StopMusic {clip}", sender);
        AudioEvents.SetMasterVolume -= (object sender, float vol) => PrintDebug($"SetMasterVolume {vol}", sender);
        AudioEvents.SetSFXVolume -= (object sender, float vol) => PrintDebug($"SetSFXVolume {vol}", sender);
        AudioEvents.SetMusicVolume -= (object sender, float vol) => PrintDebug($"SetMusicVolume {vol}", sender);

        // ----- Debug Events -----
        DebugEvents.DebugNotificationMessage -= (object sender, string msg) => PrintDebug($"DebugNotificationMessage {msg}", sender);
        DebugEvents.ToggleDebugOverlay -= (object sender) => PrintDebug("ToggleDebugOverlay", sender);
    }


    private void PrintDebug(String eventSent, object sender)
    {
        eventsFired.Add(new EventLogEntry { eventName = eventSent, time = DateTime.Now.ToString("HH:mm:ss.fff"), sender = sender.ToString() });
    }

    [System.Serializable]
    public class EventLogEntry
    {
        public string eventName;
        public string time;
        public string sender;
    }
}