using Unity.VisualScripting;
using UnityEngine.UIElements;

public class DebugOverlayHUDUI
{
    public VisualElement root;
    private Label gameStateLabel;
    private Label gameSceneLabel;
    private Label inputMappingsLabel;

    private bool isDisplayed;

    public DebugOverlayHUDUI(VisualElement rootUI)
    {
        root = rootUI;
        this.Initialize();
    }

    public void Initialize()
    {
        Show();

        gameStateLabel = root.Q<Label>("GameState_Lbl");
        gameSceneLabel = root.Q<Label>("GameScene_Lbl");
        inputMappingsLabel = root.Q<Label>("InputMappings_Lbl");

        root.pickingMode = PickingMode.Ignore;
        root.focusable = false;
        root.BringToFront();

        SystemEvents.GameStateChanged += OnGameStateChanged;
        SystemEvents.GameSceneChanged += OnGameSceneChanged;

        DebugEvents.ToggleDebugOverlay += ToggleOverlay;
    }

    private void ToggleOverlay(object sender)
    {
        if (isDisplayed)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        isDisplayed = true;
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        isDisplayed = false;
        root.style.display = DisplayStyle.None;
    }

    private void OnGameStateChanged(object sender, GameState state)
    {
        UpdateLabels();
    }

    private void OnGameSceneChanged(object sender, GameScenes scene)
    {
        UpdateLabels();
    }

    private void UpdateLabels()
    {
        gameStateLabel.text = $"State: {Managers.Instance.GameManager.CurrentState}";
        gameSceneLabel.text = $"Scene: {Managers.Instance.GameManager.CurrentScene}";
        inputMappingsLabel.text = $"Input: {Managers.Instance.InputManager.currentMappings}";
    }
}