using UnityEngine;
using UnityEngine.UIElements;

public class DebugOverlayHUDUI : UIView
{
    private Label GameState_Lbl;
    private Label GameScene_Lbl;
    private Label InputMappings_Lbl;

    public DebugOverlayHUDUI(VisualElement rootElement) : base(rootElement)
    {

    }

    protected override void RegisterCallbacks()
    {
        // Query labels
        GameState_Lbl = root.Q<Label>("GameState_Lbl");
        GameScene_Lbl = root.Q<Label>("GameScene_lbl");
        InputMappings_Lbl = root.Q<Label>("InputMappings_lbl");

        // Make overlay click-through
        root.pickingMode = PickingMode.Ignore;
        root.focusable = false;

        // Subscribe to game events
        GameEvents.GameStateChanged += OnGameStateChanged;
        GameEvents.GameSceneChanged += OnGameSceneChanged;
        GameEvents.ToggleDebugOverlay += ToggleDebugOverlay;
    }
    protected override void UnregisterCallbacks()
    {
        GameEvents.GameStateChanged -= OnGameStateChanged;
        GameEvents.GameSceneChanged -= OnGameSceneChanged;
        GameEvents.ToggleDebugOverlay -= ToggleDebugOverlay;
    }

    private void OnGameStateChanged(object sender, GameState state) => UpdateLabels();
    private void OnGameSceneChanged(object sender, GameScenes scene) => UpdateLabels();

    private void UpdateLabels()
    {
        GameState_Lbl.text = $"Game State: {Managers.Instance.GameManager.CurrentState}";
        GameScene_Lbl.text = $"Game Scene: {Managers.Instance.GameManager.CurrentScene}";
        InputMappings_Lbl.text = $"Input Mappings: {Managers.Instance.InputManager.currentMappings}";
    }

    public void ToggleDebugOverlay(object obj)
    {
        root.style.display = root.style.display == DisplayStyle.Flex
            ? DisplayStyle.None
            : DisplayStyle.Flex;
    }
}