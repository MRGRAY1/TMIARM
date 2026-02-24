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

        playButton.clicked += OnPlayClicked;
        settingsButton.clicked += OnSettingsClicked;
        exitButton.clicked += OnExitClicked;
    }

    protected override void UnregisterCallbacks()
    {
        playButton.clicked -= OnPlayClicked;
        settingsButton.clicked -= OnSettingsClicked;
        exitButton.clicked -= OnExitClicked;

    }

    private void OnPlayClicked()
    {
        GameEvents.PlayGamePressed?.Invoke(this);
    }

    private void OnSettingsClicked()
    {
        Logger.Log("Settings button clicked");
        GameEvents.OnSettingsClickedEvent?.Invoke(this);
    }

    private void OnExitClicked()
    {
        GameEvents.ExitPressed?.Invoke(this);
    }

}