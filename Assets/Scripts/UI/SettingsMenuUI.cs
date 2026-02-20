using UnityEngine.UIElements;

public class SettingsMenuUI : UIView
{
    Button backButton;

    public SettingsMenuUI(VisualElement root) : base(root) { }

    protected override void RegisterCallbacks()
    {
        backButton = root.Q<Button>("BackButton");
        backButton.clicked += OnBack;
    }

    void OnBack()
    {
        GameEvents.SettingsScreenHidden?.Invoke();
    }

    protected override void UnregisterCallbacks()
    {
        backButton.clicked -= OnBack;
    }
}