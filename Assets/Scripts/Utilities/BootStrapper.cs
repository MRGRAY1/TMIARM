using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private Managers manager;

    void Awake()
    {
        // Load Main Menu
        manager.GameManager.SetState(GameState.InMenu);
        SceneManager.LoadScene("MainMenu");
    }
}