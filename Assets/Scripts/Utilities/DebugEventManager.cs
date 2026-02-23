using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugEventManager : MonoBehaviour
{
    [SerializeField]
    private List<EventLogEntry> eventsFired = new List<EventLogEntry>();

    private void OnEnable()
    {
        eventsFired.Clear();
        GameEvents.HomeScreenShown += (object sender) => PrintDebug("HomeScreenShown", sender);
        GameEvents.SettingsScreenShown += (object sender) => PrintDebug("SettingsScreenShown", sender);
        GameEvents.SettingsScreenHidden += (object sender) => PrintDebug("SettingsScreenHidden", sender);
        GameEvents.PlayGamePressed += (object sender) => PrintDebug("PlayGamePressed", sender);
        GameEvents.OpenMenu += (object sender) => PrintDebug("OpenMenu", sender);
        GameEvents.OpenInventory += (object sender) => PrintDebug("OpenInventory", sender);
        GameEvents.ExitPressed += (object sender) => PrintDebug("ExitPressed", sender);
        GameEvents.TogglePause += (object sender, bool state) => PrintDebug("TogglePause", sender);
        GameEvents.CurrentViewChanged += (object sender, string view) => PrintDebug("CurrentViewChanged", sender);
        GameEvents.PlayerMovement += (object sender) => PrintDebug("Movement", sender);
        GameEvents.PlayerJump += (object sender) => PrintDebug("PlayerMovement", sender);
        GameEvents.PlayerDash += (object sender) => PrintDebug("PlayerDash", sender);
        GameEvents.PlayerSprint += (object sender) => PrintDebug("PlayerSprint", sender);
        GameEvents.PlayerInteract += (object sender) => PrintDebug("PlayerInteract", sender);
        GameEvents.GameStateChanged += (object sender, GameState newState) => PrintDebug($"GameStateChanged {newState.ToString()}", sender);
        GameEvents.GameSceneChanged += (object sender, GameScenes newScene) => PrintDebug($"GameSceneChanged {newScene.ToString()}", sender);
        GameEvents.DebugNotificationMessage += (object sender, string message) => PrintDebug($"DebugNotificationMessage {message}", sender);
        GameEvents.ToggleDebugOverlay += (object sender) => PrintDebug("DebugToggleOverlay", sender);
    }
    private void OnDisable()
    {
        GameEvents.HomeScreenShown -= (object sender) => PrintDebug("HomeScreenShown", sender);
        GameEvents.SettingsScreenShown -= (object sender) => PrintDebug("SettingsScreenShown", sender);
        GameEvents.SettingsScreenHidden -= (object sender) => PrintDebug("SettingsScreenHidden", sender);
        GameEvents.PlayGamePressed -= (object sender) => PrintDebug("PlayGamePressed", sender);
        GameEvents.OpenMenu -= (object sender) => PrintDebug("OpenMenu", sender);
        GameEvents.OpenInventory -= (object sender) => PrintDebug("OpenInventory", sender);
        GameEvents.ExitPressed -= (object sender) => PrintDebug("ExitPressed", sender);
        GameEvents.TogglePause -= (object sender, bool state) => PrintDebug("TogglePause", sender);
        GameEvents.CurrentViewChanged -= (object sender, string view) => PrintDebug("CurrentViewChanged", sender);
        GameEvents.PlayerMovement -= (object sender) => PrintDebug("PlayerMovement", sender);
        GameEvents.PlayerJump -= (object sender) => PrintDebug("PlayerJump", sender);
        GameEvents.PlayerDash -= (object sender) => PrintDebug("PlayerDash", sender);
        GameEvents.PlayerSprint -= (object sender) => PrintDebug("PlayerSprint", sender);
        GameEvents.PlayerInteract -= (object sender) => PrintDebug("PlayerInteract", sender);
        GameEvents.GameStateChanged -= (object sender, GameState newState) => PrintDebug("GameStateChanged", sender);
        GameEvents.GameSceneChanged -= (object sender, GameScenes newScene) => PrintDebug("GameSceneChanged", sender);
        GameEvents.DebugNotificationMessage -= (object sender, string message) => PrintDebug($"DebugNotificationMessage {message}", sender);
        GameEvents.ToggleDebugOverlay -= (object sender) => PrintDebug("DebugToggleOverlay", sender);
    }


    private void PrintDebug(String eventSent, object sender)
    {
        eventsFired.Add(new EventLogEntry
        {
            eventName = eventSent,
            time = DateTime.Now.ToString("HH:mm:ss.fff"),
            sender = sender.ToString()
        });
    }
    [System.Serializable]
    public class EventLogEntry
    {
        public string eventName;
        public string time;
        public string sender;
    }
}
