using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class MainMenuUI
{
    public VisualElement root;

    private Button playButton;
    private Button settingsButton;
    private Button exitButton;

    public MainMenuUI(VisualElement rootUI)
    {
        root = rootUI;
        Initialize();
    }

    public void Initialize()
    {
        playButton = root.Q<Button>("PlayGameButton");
        settingsButton = root.Q<Button>("SettingsButton");
        exitButton = root.Q<Button>("ExitButton");

        playButton.clicked += () => UIEvents.PlayGamePressed?.Invoke(this);
        settingsButton.clicked += () => UIEvents.OnSettingsClickedEvent?.Invoke(this);
        // exitButton.clicked += () => UIEvents.ExitPressed?.Invoke(this);
        exitButton.clicked += () => OnButtonClicked("quit");
    }

    public void Show()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        root.style.display = DisplayStyle.None;
    }

    private void OnButtonClicked(string buttonName)
    {
        Logger.Log($"Button was pressed: {buttonName}");
    }
}