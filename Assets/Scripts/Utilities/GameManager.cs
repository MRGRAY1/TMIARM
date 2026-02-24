using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState CurrentState { get; private set; }
    public GameScenes CurrentScene { get; private set; }

    [SerializeField] private string MENU_SCENE = "MainMenu";
    [SerializeField] private string GAME_SCENE = "GameScene";

    private bool gameSceneLoaded = false;

    private void Awake()
    {
        CurrentState = GameState.Loading;
        CurrentScene = GameScenes.Loading;

        GameEvents.PausedPressedEvent += TogglePause;
        GameEvents.PlayGamePressed += StartGame;
        GameEvents.GoToMainMenuEvent += GoToMainMenu;

    }
    private void OnDisable()
    {
        GameEvents.PausedPressedEvent -= TogglePause;
        GameEvents.PlayGamePressed -= StartGame;
        GameEvents.GoToMainMenuEvent -= GoToMainMenu;
    }
    #region Pause System
    public void TogglePause(object obj)
    {
        if (CurrentState != GameState.Playing && CurrentState != GameState.Pause)
            return;

        bool paused = CurrentState != GameState.Pause;
        SetPause(paused);
    }

    private void SetPause(bool paused)
    {
        if (paused)
        {
            CurrentState = GameState.Pause;
            Time.timeScale = 0f;
        }
        else
        {
            CurrentState = GameState.Playing;
            Time.timeScale = 1f;
        }

        GameEvents.TogglePause?.Invoke(this, paused);
        GameEvents.GameStateChanged?.Invoke(this.gameObject, CurrentState);
    }
    #endregion

    #region Scene & State Management
    public void StartGame(object obj)
    {
        // Load game scene only once
        if (!gameSceneLoaded)
        {
            SetScene(GameScenes.Game1, GAME_SCENE);
            gameSceneLoaded = true;
        }

        SetState(GameState.Playing);
    }

    public void GoToMainMenu(object obj)
    {
        SetScene(GameScenes.Menu, MENU_SCENE);
        SetState(GameState.MainMenu);
    }

    private void SetScene(GameScenes newScene, string sceneName)
    {
        if (CurrentScene == newScene)
            return; // Already in this scene

        CurrentScene = newScene;
        GameEvents.GameSceneChanged?.Invoke(this.gameObject, newScene);
        SceneManager.LoadScene(sceneName);
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
        GameEvents.GameStateChanged?.Invoke(this.gameObject, CurrentState);

        switch (newState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                break;

            case GameState.Playing:
                Time.timeScale = 1f;
                break;

            case GameState.Pause:
                Time.timeScale = 0f;
                break;
        }
    }
    #endregion
}

public enum GameState
{
    Loading,
    MainMenu,
    Playing,
    Pause,
    CutScene,
    EndGame
}

public enum GameScenes
{
    Menu,
    Game1,
    EndScene,
    Loading,
}