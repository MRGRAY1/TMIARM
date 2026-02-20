using UnityEngine.UIElements;

public class SettingsMenuUI : UIView
{
    Button MainMenuBtn;

    public SettingsMenuUI(VisualElement root) : base(root) { }

    protected override void RegisterCallbacks()
    {
        MainMenuBtn = root.Q<Button>("MainMenuBtn");
        MainMenuBtn.clicked += OnBackClicked;
    }

    protected override void UnregisterCallbacks()
    {
        MainMenuBtn.clicked -= OnBackClicked;
    }

    void OnBackClicked() => GameEvents.SettingsScreenHidden?.Invoke();
}