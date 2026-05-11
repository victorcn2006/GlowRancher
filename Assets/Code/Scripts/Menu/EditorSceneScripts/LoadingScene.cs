using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadAsync());
    }


    private IEnumerator LoadAsync()
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation operation = SceneManager.LoadSceneAsync("World");
        while (!operation.isDone)
            yield return null;
    }
}
