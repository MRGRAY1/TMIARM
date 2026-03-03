using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;


public class PauseMenuUI
{
    Button resumeButton;
    Button settingsButton;
    Button menuButton;

    public VisualElement root;
    public bool isShown;

    public PauseMenuUI(VisualElement rootUI)
    {
        root = rootUI;
        Initialize();
    }

    public void Initialize()
    {
        resumeButton = root.Q<Button>("Resume_Btn");
        settingsButton = root.Q<Button>("Settings_Btn");
        menuButton = root.Q<Button>("Menu_Btn");

        resumeButton.clicked += () => UIEvents.PausedPressedEvent?.Invoke(this);
        settingsButton.clicked += () => UIEvents.OnSettingsClickedEvent?.Invoke(this);
        menuButton.clicked += () => SystemEvents.GoToMainMenuEvent?.Invoke(this);
    }

    public void Show()
    {
        isShown = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        isShown = false;
        root.style.display = DisplayStyle.None;
    }
}