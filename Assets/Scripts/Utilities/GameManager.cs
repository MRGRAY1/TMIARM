using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState CurrentState { get; private set; }

    void Awake()
    {
    }

    void OnEnable()
    {
        GameEvents.PlayGamePressed += StartGame;
        GameEvents.ExitPressed += QuitGame;
    }

    void OnDisable()
    {
        GameEvents.PlayGamePressed -= StartGame;
        GameEvents.ExitPressed -= QuitGame;
    }

    void StartGame()
    {
        SetState(GameState.Playing);
        SceneManager.LoadScene("GameScene");
    }

    // Change the game state
    public void SetState(GameState newState)
    {
        if (CurrentState == newState) return; // no change

        CurrentState = newState;
        Debug.Log("Game state changed to: " + CurrentState);

        // Notify listeners
        GameEvents.GameStateChanged?.Invoke(CurrentState);
    }

    void QuitGame()
    {
        Application.Quit();
    }

}


public enum GameState
{
    InMenu,
    Playing,
    Pause,
    CutScene,
    EndGame
}