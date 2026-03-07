using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PhoneMenuUI
{
    public bool isShown;
    VisualElement root;
    VisualElement MainPage;
    List<VisualElement> menuButtons;

    public PhoneMenuUI(VisualElement rootUI)
    {
        root = rootUI;
        Initialize();
    }

    public void Initialize()
    {
        MainPage = root.Q<VisualElement>("MainScreen");
        menuButtons = MainPage.Query<VisualElement>(className: "Icon").ToList();

        for (int i = 0; i < menuButtons.Count; i++)
        {
            int index = i; // capture for closure
            menuButtons[i].RegisterCallback<PointerDownEvent>(evt => OnItemClicked(index));
        }
    }

    private void OnItemClicked(int index)
    {
        Debug.Log($"Item {index} clicked");
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