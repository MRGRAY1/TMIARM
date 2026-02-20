using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIDocument))]
public class UIManager : MonoBehaviour
{
    UIDocument document;

    UIView currentView;
    UIView previousView;

    MainMenuUI mainMenuView;
    SettingsMenuUI settingsMenuView;

    void OnEnable()
    {
        document = GetComponent<UIDocument>();
        SetupViews();

        GameEvents.SettingsScreenShown += ShowSettings;
        GameEvents.SettingsScreenHidden += HideSettings;
        GameEvents.PlayGamePressed += StartGame;
        GameEvents.ExitPressed += ExitGame;

        ShowMainMenu();
    }

    void OnDisable()
    {
        GameEvents.SettingsScreenShown -= ShowSettings;
        GameEvents.SettingsScreenHidden -= HideSettings;
        GameEvents.PlayGamePressed -= StartGame;
        GameEvents.ExitPressed -= ExitGame;
    }

    void SetupViews()
    {
        var root = document.rootVisualElement;

        mainMenuView = new MainMenuUI(root.Q("MainMenuScreen"));
        settingsMenuView = new SettingsMenuUI(root.Q("SettingsScreen"));
    }

    void ShowMainMenu()
    {
        currentView?.Hide();
        currentView = mainMenuView;
        currentView.Show();
    }

    void ShowSettings()
    {
        previousView = currentView;
        currentView.Hide();
        currentView = settingsMenuView;
        currentView.Show();
    }

    void HideSettings()
    {
        currentView.Hide();
        currentView = previousView;
        currentView.Show();
    }

    void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}