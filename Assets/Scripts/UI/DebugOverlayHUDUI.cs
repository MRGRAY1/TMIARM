using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DebugOverlayHUDUI
{
    public VisualElement root;
    private Label gameStateLabel;
    private Label gameSceneLabel;
    private Label inputMappingsLabel;
    private Label currentItemLabel;
    private Label timeScaleLabel;
    private Label deltaTimeLabel;

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
        currentItemLabel = root.Q<Label>("CurrentItem_Lbl");
        timeScaleLabel = root.Q<Label>("TimeScale_Lbl");
        deltaTimeLabel = root.Q<Label>("DeltaTime_Lbl");

        root.pickingMode = PickingMode.Ignore;
        root.focusable = false;
        root.BringToFront();

        DebugEvents.GameUpdated += UpdateLabels;
        DebugEvents.ToggleDebugOverlay += ToggleOverlay;
    }

    private void UpdateLabels(object obj)
    {
        gameStateLabel.text = $"State: {Managers.Instance.GameManager.CurrentState}";
        gameSceneLabel.text = $"Scene: {Managers.Instance.GameManager.CurrentScene}";
        inputMappingsLabel.text = $"Input: {Managers.Instance.InputManager.currentMappings}";
        currentItemLabel.text = $"Item: {Managers.Instance.GameManager.CurrentItem}";
        timeScaleLabel.text = $"Time Scale: {Time.timeScale}";
        deltaTimeLabel.text = $"Delta Time: {Time.deltaTime}";
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
}