using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField]
    private bool startInMenu = true;

    void Awake()
    {
        manager = FindAnyObjectByType<GameManager>();

        if (manager == null)
        {
            Debug.LogError("GameManager not found in Bootstrap scene!");
            return;
        }

        // set first scene by game state
        if (startInMenu)
        {
            Managers.Instance.GameManager.GoToMainMenu(this);
        }
        else
        {
            Managers.Instance.GameManager.StartGame(this);
        }

    }
}