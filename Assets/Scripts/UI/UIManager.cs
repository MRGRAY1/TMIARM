using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class UIManager : MonoBehaviour
{
    [Header("UI Documents")]
    [SerializeField] private UIDocument mainMenuDocument;
    [SerializeField] private UIDocument settingsDocument;
    [SerializeField] private UIDocument pauseMenuDocument;
    [SerializeField] private UIDocument overlayHUDDocument;

    private Stack<UIView> modalStack = new Stack<UIView>();

    private MainMenuUI mainMenuView;
    private SettingsMenuUI settingsView;
    private PauseMenuUI pauseMenuView;
    private OverlayHUDUI overlayHUDView;

    private void Awake()
    {
        SetupViews();
        HideAll();
    }

    private void SetupViews()
    {
        // Initialize UI views
        if (mainMenuDocument != null)
            mainMenuView = new MainMenuUI(mainMenuDocument.rootVisualElement);

        if (settingsDocument != null)
            settingsView = new SettingsMenuUI(settingsDocument.rootVisualElement);

        if (pauseMenuDocument != null)
            pauseMenuView = new PauseMenuUI(pauseMenuDocument.rootVisualElement);

        if (overlayHUDDocument != null)
            overlayHUDView = new OverlayHUDUI(overlayHUDDocument.rootVisualElement);

        // Debug logging
        if (mainMenuView == null) Logger.Log("UIManager: mainMenuView is null");
        if (pauseMenuView == null) Logger.Log("UIManager: pauseMenuView is null");
        if (settingsView == null) Logger.Log("UIManager: settingsView is null");
        if (overlayHUDView == null) Logger.Log("UIManager: overlayHUDView is null");
    }

    private void OnEnable()
    {
        GameEvents.GameStateChanged += OnGameStateChanged;
        GameEvents.OnSettingsClickedEvent += ShowSettings;
        GameEvents.OnBackClickedEvent += HideCurrentModal;
    }

    private void OnDisable()
    {
        GameEvents.GameStateChanged -= OnGameStateChanged;
        GameEvents.OnSettingsClickedEvent -= ShowSettings;
        GameEvents.OnBackClickedEvent -= HideCurrentModal;
    }

    private void OnGameStateChanged(object sender, GameState state)
    {
        // Reset all UI
        HideAll();
        modalStack.Clear();

        switch (state)
        {
            case GameState.MainMenu:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ShowMainMenu(this);
                break;

            case GameState.Playing:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                ShowOverlay(this);
                break;

            case GameState.Pause:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ShowPauseMenu(this);
                break;
        }
    }

    private void ShowModal(UIView view)
    {
        if (view == null)
        {
            Logger.Log("UIManager: ShowModal called with null view");
            return;
        }

        // Hide previous modal if exists
        if (modalStack.Count > 0)
            modalStack.Peek().Hide();

        modalStack.Push(view);
        view.Show();
    }

    private void HideCurrentModal(object sender)
    {
        if (modalStack.Count == 0)
            return;

        modalStack.Pop().Hide();

        if (modalStack.Count > 0)
            modalStack.Peek().Show();
    }

    private void HideAll()
    {
        mainMenuView?.Hide();
        pauseMenuView?.Hide();
        settingsView?.Hide();
        overlayHUDView?.Hide();
    }

    private void ShowMainMenu(object sender)
    {
        ShowModal(mainMenuView);
    }
    private void ShowOverlay(object sender)
    {
        ShowModal(overlayHUDView);
    }
    private void ShowPauseMenu(object sender)
    {
        ShowModal(pauseMenuView);
    }

    private void ShowSettings(object sender)
    {
        if (settingsView == null)
        {
            Logger.Log("UIManager: settingsView is null!");
            return;
        }

        // Show settings if either pause menu or main menu is currently active
        if ((pauseMenuView != null && pauseMenuView.Root.visible) ||
            (mainMenuView != null && mainMenuView.Root.visible))
        {
            Logger.Log("UIManager: Showing settingsView");
            ShowModal(settingsView);
        }
        else
        {
            Logger.LogWarning("UIManager: Cannot show Settings unless pause menu or main menu is active");
        }
    }
}