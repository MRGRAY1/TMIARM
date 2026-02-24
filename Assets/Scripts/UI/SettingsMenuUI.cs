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

}