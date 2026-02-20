using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;

    public GameManager GameManager;
    public AudioManager AudioManager;
    public UIManager UIManager;

    // UIManager Awake
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Make the root Managers GameObject persistent
        DontDestroyOnLoad(transform.root.gameObject);
        if (GameManager == null)
        {
            GameManager = gameObject.GetComponent<GameManager>();

        }
        if (AudioManager == null)
        {
            AudioManager = gameObject.GetComponent<AudioManager>();
        }
        if (UIManager == null)
        {
            UIManager = gameObject.GetComponent<UIManager>();
        }
    }
}
