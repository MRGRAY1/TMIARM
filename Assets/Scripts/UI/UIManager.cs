using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIDocument mainUIDocument;

    // Views
    private MainMenuUI mainMenuView;
    private SettingsMenuUI settingsView;
    private PauseMenuUI pauseMenuView;
    private OverlayHUDUI overlayHUDView;
    private DebugOverlayHUDUI debugOverlayView;

    private VisualElement root;

    public UIDocument Document => mainUIDocument;

    private void OnEnable()
    {
        SubscribeToEvents();
        Initialize();
    }

    private void Initialize()
    {
        NullRefChecker.Validate(this);

        VisualElement root = mainUIDocument.rootVisualElement;

        mainMenuView = new MainMenuUI(root.Q<VisualElement>("MainMenu_Doc"));
        settingsView = new SettingsMenuUI(root.Q<VisualElement>("SettingsMenu_Doc"));
        overlayHUDView = new OverlayHUDUI(root.Q<VisualElement>("Overlay_Doc"));
        pauseMenuView = new PauseMenuUI(root.Q<VisualElement>("PauseMenu_Doc"));
        debugOverlayView = new DebugOverlayHUDUI(root.Q<VisualElement>("Debug_Doc"));

        HideAll();
    }

    // Unregister the listeners to prevent errors
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        SystemEvents.GameStateChanged += GameStateChanged;
        UIEvents.OnSettingsClickedEvent += ToggleSettings;
        UIEvents.PausedPressedEvent += TogglePause;
    }


    private void UnsubscribeFromEvents()
    {
        SystemEvents.GameStateChanged -= GameStateChanged;
        UIEvents.OnSettingsClickedEvent -= ToggleSettings;
    }

    private void GameStateChanged(object arg1, GameState arg2)
    {
        HideAll();
        switch (arg2)
        {
            case GameState.MainMenu:
                ShowMainMenu();
                break;
            case GameState.Pause:
                ShowPauseMenu();
                break;
            case GameState.Playing:
                ShowGameOverlay();
                break;
            default:
                break;
        }

        Logger.Log($"MainMenuView: {mainMenuView.root.style.display}");
        Logger.Log($"SettingsView: {settingsView.root.style.display}");
        Logger.Log($"PauseView: {pauseMenuView.root.style.display}");
        Logger.Log($"OverviewView: {overlayHUDView.root.style.display}");
        Logger.Log($"DebugView: {debugOverlayView.root.style.display}");
    }


    private void ShowMainMenu()
    {
        mainMenuView.Show();
    }

    private void HideMainMenu()
    {
        mainMenuView.Hide();
    }

    private void ToggleSettings(object obj)
    {
        if (settingsView.isShown)
        {
            HideSettings();
        }
        else
        {
            ShowSettings();
        }
    }

    private void ShowSettings()
    {
        settingsView.Show();
    }

    private void HideSettings()
    {
        settingsView.Hide();
    }

    private void TogglePause(object obj)
    {
        if (pauseMenuView.isShown)
        {
            HidePauseMenu();
        }
        else
        {
            ShowPauseMenu();
        }
    }

    private void ShowPauseMenu()
    {
        pauseMenuView.Show();
    }

    private void HidePauseMenu()
    {
        pauseMenuView.Hide();
    }

    private void ShowGameOverlay()
    {
        overlayHUDView.Show();
    }

    private void HideGameOverlay()
    {
        overlayHUDView.Hide();
    }


    private void HideAll()
    {
        HideMainMenu();
        HideSettings();
        HidePauseMenu();
        HideGameOverlay();
    }
}