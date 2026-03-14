using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private bool startInMenu = true;
    [SerializeField] private string LOADING_SCENE = "LoadingScreen";
    [SerializeField] private string MENU_SCENE = "MainMenu";
    [SerializeField] private string GAME_SCENE = "GameScene";

    void Awake()
    {
        manager = FindAnyObjectByType<GameManager>();

        if (manager == null)
        {
            Debug.LogError("GameManager not found in Bootstrap scene!");
            return;
        }

        if (startInMenu)
        {
            SceneLoader.TargetScene = MENU_SCENE;
        }
        else
        {
            SceneLoader.TargetScene = GAME_SCENE;
        }

        SceneManager.LoadScene(LOADING_SCENE);
    }
}