using UnityEngine;

public class GameSceneBootStrap : MonoBehaviour
{
    void Start()
    {
        SystemEvents.SceneReadyEvent?.Invoke(this, GameState.Playing);
    }
}