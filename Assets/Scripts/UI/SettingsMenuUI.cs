using UnityEngine.UIElements;

public class SettingsMenuUI : UIView
{
    Button BackBtn;
    private VisualElement debugContainer;
    private DebugOverlayHUDUI debugOverlayHUD;

    public SettingsMenuUI(VisualElement root) : base(root)
    {
        debugContainer = root.Q<VisualElement>("DebugDocument");
        debugOverlayHUD = new DebugOverlayHUDUI(debugContainer);

        if (debugContainer != null) GameEvents.ToggleDebugOverlay += ToggleDebugOverlay;
    }

    protected override void RegisterCallbacks()
    {
        BackBtn = root.Q<Button>("BackBtn");

        BackBtn.clicked += OnBackClicked;
    }

    protected override void UnregisterCallbacks()
    {
        BackBtn.clicked -= OnBackClicked;
    }

    void OnBackClicked() => GameEvents.SettingsScreenHidden?.Invoke(this);

    public override void ToggleDebugOverlay(object obj)
    {
        if (debugContainer.style.display == DisplayStyle.Flex)
            debugContainer.style.display = DisplayStyle.None;
        else
            debugContainer.style.display = DisplayStyle.Flex;
    }
}