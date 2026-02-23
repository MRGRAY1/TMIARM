using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState CurrentState { get; private set; }
    public GameScenes CurrentScene { get; private set; }

    [SerializeField]
    private const string MENU_SCENE = "MainMenu";
    private const string GAME_SCENE = "GameScene";

    private void Awake()
    {

    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void SetSceneChange(object sender, GameScenes scenes)
    {
        if (scenes == GameScenes.Menu)
        {
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            SetState(GameState.InMenu);
            SetScene(GameScenes.Menu, MENU_SCENE);
        }
        else if (scenes == GameScenes.Game1)
        {
            SetState(GameState.Playing);
            SetScene(GameScenes.Game1, GAME_SCENE);
            SetPause(false);
        }
    }


    public void TogglePause()
    {
        // Only allow toggling if currently playing
        if (CurrentState != GameState.Playing && CurrentState != GameState.Pause)
            return;

        // Flip paused state
        bool paused = CurrentState != GameState.Pause;
        SetPause(paused);
    }


    private void SetPause(bool paused)
    {
        if (paused)
        {
            SetState(GameState.Pause);
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            SetState(GameState.Playing);
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }

        GameEvents.TogglePause?.Invoke(this, paused);
    }

    private void SetScene(GameScenes newScene, string sceneName)
    {
        CurrentScene = newScene;
        GameEvents.GameSceneChanged?.Invoke(this.gameObject, newScene);
        SceneManager.LoadScene(sceneName);
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
        GameEvents.GameStateChanged?.Invoke(this.gameObject, this.CurrentState);
    }
}

public enum GameState
{
    Loading,
    InMenu,
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
}