using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    [Header("UI Documents")]
    [SerializeField] private UIDocument mainMenuDocument;
    [SerializeField] private UIDocument settingsDocument;
    [SerializeField] private UIDocument pauseMenuDocument;
    [SerializeField] private UIDocument overlayHUDDocument;

    // Modal stack
    private Stack<UIView> modalStack = new Stack<UIView>();

    // Overlay elements (HUD)
    private Dictionary<string, UIView> overlays = new Dictionary<string, UIView>();

    // Scene-specific container (optional parent for scene panels)
    private Transform sceneUIContainer;

    // UIViews
    private MainMenuUI mainMenuView;
    private SettingsMenuUI settingsView;
    private PauseMenuUI pauseMenuView;
    private OverlayHUDUI overlayHUDView;

    private void Awake()
    {
        // Initialize views
        SetupViews();
    }

    private void SetupViews()
    {
        // Main Menu
        if (mainMenuDocument != null)
            mainMenuView = new MainMenuUI(mainMenuDocument.rootVisualElement);

        // Settings
        if (settingsDocument != null)
            settingsView = new SettingsMenuUI(settingsDocument.rootVisualElement);

        // Pause Menu
        if (pauseMenuDocument != null)
            pauseMenuView = new PauseMenuUI(pauseMenuDocument.rootVisualElement);

        // Overlay HUD
        if (overlayHUDDocument != null)
        {
            overlayHUDView = new OverlayHUDUI(overlayHUDDocument.rootVisualElement);
            overlays.Add("HUD", overlayHUDView);
            overlayHUDView.Show(); // always visible
        }

        // Hide modals at start
        settingsView?.Hide();
        pauseMenuView?.Hide();
    }

    private void OnEnable()
    {
        // Subscribe to events
        GameEvents.HomeScreenShown += ShowMainMenu;
        GameEvents.SettingsScreenShown += ShowSettings;
        GameEvents.SettingsScreenHidden += HideCurrentModal;
        GameEvents.PauseScreenShown += ShowPauseMenu;
        GameEvents.PauseScreenHidden += HideCurrentModal;
    }

    private void OnDisable()
    {
        // Unsubscribe
        GameEvents.HomeScreenShown -= ShowMainMenu;
        GameEvents.SettingsScreenShown -= ShowSettings;
        GameEvents.SettingsScreenHidden -= HideCurrentModal;
        GameEvents.PauseScreenShown -= ShowPauseMenu;
        GameEvents.PauseScreenHidden -= HideCurrentModal;
    }

    #region Modal Stack Methods

    // Show a modal screen
    private void ShowModal(UIView view)
    {
        if (view == null) return;

        // Hide current top modal
        if (modalStack.Count > 0)
            modalStack.Peek().Hide();

        modalStack.Push(view);
        view.Show();
    }

    // Hide current modal
    private void HideCurrentModal()
    {
        if (modalStack.Count == 0) return;

        var top = modalStack.Pop();
        top.Hide();

        // Show previous modal if exists
        if (modalStack.Count > 0)
            modalStack.Peek().Show();
    }

    #endregion

    #region Event Handlers

    private void ShowMainMenu()
    {
        ShowModal(mainMenuView);
    }

    private void ShowSettings()
    {
        ShowModal(settingsView);
    }

    private void ShowPauseMenu()
    {
        ShowModal(pauseMenuView);
    }

    #endregion

    #region Scene UI

    // Optionally instantiate scene-specific UI under a container
    public void RegisterSceneUI(UIView view)
    {
        if (view == null) return;
        view.Show();
    }

    public void UnregisterSceneUI(UIView view)
    {
        if (view == null) return;
        view.Hide();
    }

    #endregion
}