using System.Collections;
using UnityEngine;

public static class CoroutineRunner
{
    private class CoroutineRunnerBehaviour : MonoBehaviour { }
    private static CoroutineRunnerBehaviour _runner;

    public static Coroutine StartCoroutine(IEnumerator routine)
    {
        if (_runner == null)
        {
            var go = new GameObject("CoroutineRunner");
            go.hideFlags = HideFlags.HideAndDontSave;
            _runner = go.AddComponent<CoroutineRunnerBehaviour>();
        }
        return _runner.StartCoroutine(routine);
    }
}