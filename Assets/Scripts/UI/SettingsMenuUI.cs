using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class SettingsMenuUI
{
    private Button backButton;
    public VisualElement root;
    public bool isShown;

    public SettingsMenuUI(VisualElement rootUI)
    {
        root = rootUI;
        Initialize();
    }

    public void Initialize()
    {
        backButton = root.Q<Button>("BackBtn");

        backButton.clicked += () => UIEvents.OnSettingsClickedEvent?.Invoke(this);
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