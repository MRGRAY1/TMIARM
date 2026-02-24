using UnityEngine.UIElements;
using System;

public class SettingsMenuUI : UIView
{
    private Button backButton;

    public SettingsMenuUI(VisualElement rootElement) : base(rootElement)
    {
    }

    protected override void RegisterCallbacks()
    {
        backButton = root.Q<Button>("BackBtn");
        backButton.clicked += () => GameEvents.OnBackClickedEvent?.Invoke(this);
    }

    protected override void UnregisterCallbacks()
    {
        backButton.clicked -= () => GameEvents.OnBackClickedEvent?.Invoke(this);
    }
}