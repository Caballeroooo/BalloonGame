using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : Singleton<AsyncSceneLoader>
{
    public void LoadScene(int buildIndex)
    {
        StartCoroutine(LoadSceneAsync(buildIndex));
    }

    private IEnumerator LoadSceneAsync(int buildIndex)
    {
        var loadingOperation = SceneManager.LoadSceneAsync(buildIndex);
        yield return new WaitUntil(() => loadingOperation.isDone);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndex));
    }
}