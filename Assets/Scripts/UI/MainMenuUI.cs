using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : UIView
{
    private Button playButton;
    private Button settingsButton;
    private Button exitButton;

    private VisualElement debugContainer;
    private DebugOverlayHUDUI debugOverlayHUD;

    public MainMenuUI(VisualElement root) : base(root) { }

    protected override void RegisterCallbacks()
    {
        playButton = root.Q<Button>("PlayGameButton");
        settingsButton = root.Q<Button>("SettingsButton");
        exitButton = root.Q<Button>("ExitButton");
        debugContainer = root.Q<VisualElement>("DebugDocument");

        // Only construct and subscribe if the debug container exists
        if (debugContainer != null)
        {
            debugOverlayHUD = new DebugOverlayHUDUI(debugContainer);
            GameEvents.ToggleDebugOverlay += ToggleDebugOverlay;
        }
        else
        {
            Debug.Log("MainMenuUI: DebugDocument not found, skipping debug overlay setup.");
        }

        if (playButton != null) playButton.clicked += OnPlayClicked;
        if (settingsButton != null) settingsButton.clicked += OnSettingsClicked;
        if (exitButton != null) exitButton.clicked += OnExitClicked;
    }

    protected override void UnregisterCallbacks()
    {
        if (playButton != null) playButton.clicked -= OnPlayClicked;
        if (settingsButton != null) settingsButton.clicked -= OnSettingsClicked;
        if (exitButton != null) exitButton.clicked -= OnExitClicked;

        if (debugContainer != null)
        {
            GameEvents.ToggleDebugOverlay -= ToggleDebugOverlay;
            debugOverlayHUD?.Dispose();
            debugOverlayHUD = null;
        }
    }

    private void OnPlayClicked()
    {
        // Unpause the game if needed
        UnityEngine.Time.timeScale = 1f;
        Managers.Instance.GameManager.SetSceneChange(this, GameScenes.Game1);
    }

    private void OnSettingsClicked()
    {
        GameEvents.SettingsScreenShown?.Invoke(this);
    }

    private void OnExitClicked()
    {
        GameEvents.ExitPressed?.Invoke(this);
    }

    // Keep same signature used elsewhere in the project
    public override void ToggleDebugOverlay(object obj)
    {
        if (debugContainer == null) return;

        if (debugContainer.style.display == DisplayStyle.Flex)
            debugContainer.style.display = DisplayStyle.None;
        else
            debugContainer.style.display = DisplayStyle.Flex;
    }
}