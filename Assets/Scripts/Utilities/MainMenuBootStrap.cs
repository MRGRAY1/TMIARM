using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    void Start()
    {
        SystemEvents.SceneReadyEvent?.Invoke(this, GameState.MainMenu);
    }
}