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

    //[SerializeField]
    //private DebugScriptableObjects debugOverlaySO;

    private void Awake()
    {
        SetupViews();
        HideAll();
    }

    private void SetupViews()
    {
        if (mainMenuDocument != null)
            mainMenuView = new MainMenuUI(mainMenuDocument.rootVisualElement);

        if (settingsDocument != null)
            settingsView = new SettingsMenuUI(settingsDocument.rootVisualElement);

        if (pauseMenuDocument != null)
            pauseMenuView = new PauseMenuUI(pauseMenuDocument.rootVisualElement);

        if (overlayHUDDocument != null)
            overlayHUDView = new OverlayHUDUI(overlayHUDDocument.rootVisualElement);

        //if (debugOverlaySO.IsDebugOverlayVisible)
        //{
        //    GameEvents.ToggleDebugOverlay?.Invoke(this);
        //}

    }

    private void OnEnable()
    {
        GameEvents.GameStateChanged += GameStateChanged;
        GameEvents.HomeScreenShown += ShowMainMenu;
        GameEvents.SettingsScreenShown += ShowSettings;
        GameEvents.SettingsScreenHidden += HideCurrentModal;
        GameEvents.ToggleDebugOverlay += ToggleDebugOverlay;
    }

    private void OnDisable()
    {
        GameEvents.GameStateChanged -= GameStateChanged;
        GameEvents.HomeScreenShown -= ShowMainMenu;
        GameEvents.SettingsScreenShown -= ShowSettings;
        GameEvents.SettingsScreenHidden -= HideCurrentModal;
        GameEvents.ToggleDebugOverlay -= ToggleDebugOverlay;
    }

    private void ToggleDebugOverlay(object obj)
    {
        //debugOverlaySO.IsDebugOverlayVisible = !debugOverlaySO.IsDebugOverlayVisible;
    }

    private void GameStateChanged(object sender, GameState state)
    {
        // reset everything
        HideAll();
        modalStack.Clear();

        switch (state)
        {
            case GameState.InMenu:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pauseMenuView?.Hide();
                mainMenuView?.Show();
                break;

            case GameState.Playing:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                overlayHUDView?.Show();
                break;

            case GameState.Pause:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pauseMenuView?.Show();
                break;
        }
    }

    private void ShowModal(UIView view)
    {
        if (view == null) return;

        if (modalStack.Count > 0)
            modalStack.Peek().Hide();

        modalStack.Push(view);
        view.Show();
    }

    private void HideCurrentModal(object sender)
    {
        if (modalStack.Count == 0) return;

        modalStack.Pop().Hide();

        if (modalStack.Count > 0)
            modalStack.Peek().Show();
    }

    private void HideAll()
    {
        mainMenuView?.Hide();
        settingsView?.Hide();
        pauseMenuView?.Hide();
        overlayHUDView?.Hide();
    }

    private void ShowMainMenu(object sender)
    {
        ShowModal(mainMenuView);
    }

    private void ShowSettings(object sender)
    {
        ShowModal(settingsView);
    }
}