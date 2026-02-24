using UnityEngine.UIElements;

public class PauseMenuUI : UIView
{
    private Button resumeButton;
    private Button settingsButton;
    private Button menuButton;



    public PauseMenuUI(VisualElement rootElement) : base(rootElement)
    { }

    protected override void RegisterCallbacks()
    {
        resumeButton = root.Q<Button>("Resume_Btn");
        settingsButton = root.Q<Button>("Settings_Btn");
        menuButton = root.Q<Button>("Menu_Btn");
        resumeButton.clicked += () => GameEvents.PausedPressedEvent?.Invoke(this);
        settingsButton.clicked += () => GameEvents.OnSettingsClickedEvent?.Invoke(this);
        menuButton.clicked += () => GameEvents.GoToMainMenuEvent?.Invoke(this);
    }

    protected override void UnregisterCallbacks()
    {
        resumeButton.clicked -= () => GameEvents.PausedPressedEvent?.Invoke(this);
        settingsButton.clicked -= () => GameEvents.OnSettingsClickedEvent?.Invoke(this);
        menuButton.clicked -= () => GameEvents.GoToMainMenuEvent?.Invoke(this);
    }
}