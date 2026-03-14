using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static string TargetScene;
    public static float minimumTime = 1f;

    [SerializeField] private UIDocument loadingDocument;
    private ProgressBar progressBar;

    void Start()
    {
        var root = loadingDocument.rootVisualElement;
        progressBar = root.Q<ProgressBar>("LoadingProgressBar"); // match your UXML element name

        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(TargetScene);
        op.allowSceneActivation = false;

        float elapsed = 0f;

        while (!op.isDone)
        {
            elapsed += Time.unscaledDeltaTime; // <-- change this line

            float loadProgress = op.progress / 0.9f;
            float timeProgress = elapsed / minimumTime;

            if (progressBar != null)
                progressBar.value = Mathf.Min(loadProgress, timeProgress) * 100f;

            if (op.progress >= 0.9f && elapsed >= minimumTime)
            {
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}