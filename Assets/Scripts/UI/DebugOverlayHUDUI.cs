using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DebugOverlayHUDUI : UIView
{
    // Debug labels
    private Label GameState_Lbl;
    private Label GameScene_Lbl;
    private Label InputMappings_Lbl;

    private string CurrGameState;
    private string CurrGameScene;
    private string CurrInputMappings;

    public DebugOverlayHUDUI(VisualElement rootElement) : base(rootElement)
    {
        // Query labels
        GameState_Lbl = root.Q<Label>("GameState_Lbl");
        GameScene_Lbl = root.Q<Label>("GameScene_lbl");
        InputMappings_Lbl = root.Q<Label>("InputMappings_lbl");

        // Make overlay click-through
        root.pickingMode = PickingMode.Ignore;
        root.focusable = false;

        // Subscribe to game events for updating labels
        GameEvents.GameSceneChanged += NewGameScene;
        GameEvents.GameStateChanged += NewGameState;
        GameEvents.ToggleDebugOverlay += ToggleDebug;
    }

    private void ToggleDebug(object obj)
    {
        UpdateLabels();
    }

    private void NewGameState(object sender, GameState state)
    {
        UpdateLabels();
    }

    private void NewGameScene(object sender, GameScenes scenes)
    {
        UpdateLabels();
    }

    private void UpdateLabels()
    {
        CurrGameScene = Managers.Instance.GameManager.CurrentScene.ToString();
        CurrGameState = Managers.Instance.GameManager.CurrentState.ToString();
        CurrInputMappings = Managers.Instance.InputManager.currentMappings.ToString();

        GameState_Lbl.text = $"Game State: {CurrGameState}";
        GameScene_Lbl.text = $"Game Scene: {CurrGameScene}";
        InputMappings_Lbl.text = $"Input Mappings: {CurrInputMappings}";
    }

    protected override void RegisterCallbacks()
    {
        // No local callbacks needed
    }

    protected override void UnregisterCallbacks()
    {
        // Unsubscribe from events
        GameEvents.GameSceneChanged -= NewGameScene;
        GameEvents.GameStateChanged -= NewGameState;

    }

    public override void ToggleDebugOverlay(object obj)
    {
        // No-op; visibility is controlled globally by the ScriptableObject
    }
}