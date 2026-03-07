using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;

    public GameManager GameManager;
    public AudioManager AudioManager;
    public UIManager UIManager;
    public InputManager InputManager;

    // UIManager Awake
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
        Initialize();
    }

    private void Initialize()
    {
        if (GameManager == null)
            GameManager = GetComponentInChildren<GameManager>();
        if (AudioManager == null)
            AudioManager = GetComponentInChildren<AudioManager>();
        if (UIManager == null)
            UIManager = GetComponentInChildren<UIManager>();
        if (InputManager == null)
            InputManager = GetComponentInChildren<InputManager>();
        CoroutineRunner.Coroutines.Initialize(this);
        GameManager.SetState(GameState.Loading);
    }
}