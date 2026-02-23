using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameManager manager;

    void Awake()
    {
        manager = FindAnyObjectByType<GameManager>();

        if (manager == null)
        {
            Debug.LogError("GameManager not found in Bootstrap scene!");
            return;
        }

        // set Menu as first scene
        Managers.Instance.GameManager.SetSceneChange(this, GameScenes.Menu);

    }
}