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
    }


    private void OnDisable()
    {
        UIEvents.PausedPressedEvent -= TogglePause;
        UIEvents.PlayGamePressed -= StartGame;
        SystemEvents.GoToMainMenuEvent -= GoToMainMenu;
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

    public void StartGame(object obj)
    {
        StartCoroutine(LoadSceneNextFrame(GAME_SCENE, GameScenes.Game1));
        SetState(GameState.Playing);
    }

    public void GoToMainMenu(object obj)
    {
        // Delay by one frame to ensure UI updates
        StartCoroutine(LoadSceneNextFrame(MENU_SCENE, GameScenes.Menu));
        SetState(GameState.MainMenu);
    }

    private IEnumerator LoadSceneNextFrame(string sceneName, GameScenes newScene)
    {
        yield return null; // wait one frame
        SetScene(newScene, sceneName);
    }

    private void SetScene(GameScenes newScene, string sceneName)
    {
        if (CurrentScene == newScene)
            return; // Already in this scene

        CurrentScene = newScene;
        SystemEvents.GameSceneChanged?.Invoke(this.gameObject, newScene);
        DebugEvents.GameUpdated?.Invoke(this);
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