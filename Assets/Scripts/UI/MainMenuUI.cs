using UnityEngine.UIElements;

public class MainMenuUI : UIView
{
    Button playButton;
    Button settingsButton;
    Button exitButton;

    public MainMenuUI(VisualElement root) : base(root) { }

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

    void OnPlayClicked() => GameEvents.PlayGamePressed?.Invoke();
    void OnSettingsClicked() => GameEvents.SettingsScreenShown?.Invoke();
    void OnExitClicked() => GameEvents.ExitPressed?.Invoke();
}