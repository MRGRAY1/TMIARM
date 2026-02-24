using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : UIView
{
    private Button playButton;
    private Button settingsButton;
    private Button exitButton;

    public MainMenuUI(VisualElement root) : base(root)
    {
    }

    protected override void RegisterCallbacks()
    {
        playButton = root.Q<Button>("PlayGameButton");
        settingsButton = root.Q<Button>("SettingsButton");
        exitButton = root.Q<Button>("ExitButton");

        if (playButton != null) playButton.clicked += OnPlayClicked;
        if (settingsButton != null) settingsButton.clicked += OnSettingsClicked;
        if (exitButton != null) exitButton.clicked += OnExitClicked;
    }

    protected override void UnregisterCallbacks()
    {
        if (playButton != null) playButton.clicked -= OnPlayClicked;
        if (settingsButton != null) settingsButton.clicked -= OnSettingsClicked;
        if (exitButton != null) exitButton.clicked -= OnExitClicked;

    }

    private void OnPlayClicked()
    {
        Managers.Instance.GameManager.StartGame();
    }

    private void OnSettingsClicked()
    {
        Logger.Log("Settings button clicked");
        GameEvents.SettingsScreenShown?.Invoke(this);
    }

    private void OnExitClicked()
    {
        GameEvents.ExitPressed?.Invoke(this);
    }
}