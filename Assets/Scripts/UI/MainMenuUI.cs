using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : UIView
{
    Button playGameButton;
    Button settingsButton;
    Button exitButton;

    public MainMenuUI(VisualElement root) : base(root)
    {
        playGameButton = root.Q<Button>("PlayGameButton");
        settingsButton = root.Q<Button>("SettingsButton");
        exitButton = root.Q<Button>("ExitButton");

        playGameButton.clicked += () =>
        {
            GameEvents.PlayGamePressed?.Invoke();
        };

        settingsButton.clicked += () =>
        {
            GameEvents.SettingsScreenShown?.Invoke();
        };

        exitButton.clicked += () =>
        {
            GameEvents.ExitPressed?.Invoke();
        };
    }

    protected override void RegisterCallbacks()
    {
        //throw new System.NotImplementedException();
    }
}