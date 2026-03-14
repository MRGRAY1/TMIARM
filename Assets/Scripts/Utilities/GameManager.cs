using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState CurrentState { get; private set; }
    public GameScenes CurrentScene { get; private set; }
    public GameItems CurrentItem { get; private set; }

    [SerializeField] private string MENU_SCENE = "MainMenu";
    [SerializeField] private string GAME_SCENE = "GameScene";
    [SerializeField] private string LOADING_SCENE = "LoadingScreen";

    public bool IsPhoneShown { get; set; }

    private void Awake()
    {
        CurrentState = GameState.Loading;
        CurrentScene = GameScenes.Loading;
        SetCurrentItem(this, 0);

        UIEvents.PausedPressedEvent += TogglePause;
        UIEvents.PlayGamePressed += StartGame;
        SystemEvents.GoToMainMenuEvent += GoToMainMenu;
        UIEvents.UpdateItemIndex += SetCurrentItem;
        SystemEvents.SceneReadyEvent += OnSceneReady;
    }

    private void OnDisable()
    {
        UIEvents.PausedPressedEvent -= TogglePause;
        UIEvents.PlayGamePressed -= StartGame;
        SystemEvents.GoToMainMenuEvent -= GoToMainMenu;
        UIEvents.UpdateItemIndex -= SetCurrentItem;
        SystemEvents.SceneReadyEvent -= OnSceneReady;
    }

    #region Pause System

    public void TogglePause(object obj)
    {
        if (CurrentScene == GameScenes.Menu)
            return;

        if (CurrentState == GameState.Playing)
        {
            SetState(GameState.Pause);
        }
        else if (CurrentState == GameState.Pause)
        {
            SetState(GameState.Playing);
        }
    }

    #endregion

    #region Scene & State Management

    private void OnSceneReady(object sender, GameState state)
    {
        CurrentScene = state == GameState.MainMenu ? GameScenes.Menu : GameScenes.Game1;
        SetState(state);
    }

    public void StartGame(object obj)
    {
        CurrentScene = GameScenes.Loading;
        SceneLoader.TargetScene = GAME_SCENE;
        SceneLoader.minimumTime = 5f;
        StartCoroutine(LoadSceneNextFrame(LOADING_SCENE));
    }

    public void GoToMainMenu(object obj)
    {
        Time.timeScale = 1f;
        CurrentScene = GameScenes.Loading;
        SceneLoader.TargetScene = MENU_SCENE;
        SceneLoader.minimumTime = 0.1f;
        StartCoroutine(LoadSceneNextFrame(LOADING_SCENE));
    }

    private IEnumerator LoadSceneNextFrame(string sceneName)
    {
        yield return null;
        SceneManager.LoadScene(sceneName);
    }

    public void SetState(GameState newState)
    {
        Logger.Log($"New State: {newState}");
        CurrentState = newState;

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

        SystemEvents.GameStateChanged?.Invoke(gameObject, newState);
        DebugEvents.GameUpdated?.Invoke(this);
    }

    private void SetCurrentItem(object arg1, int arg2)
    {
        switch (arg2)
        {
            case 0:
                CurrentItem = GameItems.EmptyHanded;
                break;
            case 1:
                CurrentItem = GameItems.Wrench;
                break;
            case 2:
                CurrentItem = GameItems.Hammer;
                break;
            case 3:
                CurrentItem = GameItems.ScrewDriver;
                break;
            case 4:
                CurrentItem = GameItems.Knife;
                break;
            case 5:
                CurrentItem = GameItems.Multimeter;
                break;
        }

        GameEvents.UpdateItemInHand?.Invoke(this);
        DebugEvents.GameUpdated?.Invoke(this);
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

public enum GameItems
{
    EmptyHanded,
    Wrench,
    Hammer,
    ScrewDriver,
    Knife,
    Multimeter
}