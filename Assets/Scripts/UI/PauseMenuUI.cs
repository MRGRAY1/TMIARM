using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuUI : UIView
{
    Button resumeButton;
    Button settingsButton;
    Button menuButton;
    private VisualElement debugContainer;
    private DebugOverlayHUDUI debugOverlayHUD;

    public PauseMenuUI(VisualElement rootElement) : base(rootElement)
    {
        resumeButton = root.Q<Button>("Resume_Btn");
        settingsButton = root.Q<Button>("Settings_Btn");
        menuButton = root.Q<Button>("Menu_Btn");
        debugContainer = root.Q<VisualElement>("DebugDocument");
        debugOverlayHUD = new DebugOverlayHUDUI(debugContainer);

        if (debugContainer != null) GameEvents.ToggleDebugOverlay += ToggleDebugOverlay;

        if (resumeButton == null)
        {
            Logger.Log("resumeButton is null");
            return;
        }
        if (settingsButton == null)
        {
            Logger.Log("settingsButton is null");
            return;
        }
        if (menuButton == null)
        {
            Logger.Log("menuButton is null");
            return;
        }

        resumeButton.clicked += OnResumeClicked;
        settingsButton.clicked += OnSettingsClicked;
        menuButton.clicked += OnMenuClicked;
    }
    protected override void RegisterCallbacks()
    {

    }

    private void OnResumeClicked()
    {
        Managers.Instance.GameManager.TogglePause();

    }

    private void OnSettingsClicked()
    {
        GameEvents.SettingsScreenShown?.Invoke(this);
    }

    //
    private void OnMenuClicked()
    {
        Managers.Instance.GameManager.SetState(GameState.MainMenu);

    }

    protected override void UnregisterCallbacks()
    {
        resumeButton.clicked -= OnResumeClicked;
        settingsButton.clicked -= OnSettingsClicked;
        menuButton.clicked -= OnMenuClicked;
    }
    public override void ToggleDebugOverlay(object obj)
    {
        if (debugContainer.style.display == DisplayStyle.Flex)
            debugContainer.style.display = DisplayStyle.None;
        else
            debugContainer.style.display = DisplayStyle.Flex;
    }
}
