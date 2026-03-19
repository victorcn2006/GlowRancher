using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScene : MonoBehaviour
{
    public static LoaderScene Instance;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
            

        /* LoaderScene[] objs = FindObjectsByType<LoaderScene>(FindObjectsInactive.Include, FindObjectsSortMode.None);

         if (objs.Length > 1)
         {
             Destroy(gameObject);
         }
         else
         {
             Instance = this;
             transform.parent = null;
             DontDestroyOnLoad(gameObject);

         }*/
    }
    public void LoadScene(string nameScene)
    {

        SceneManager.LoadScene(ConstantGame.SCENELOADINGSCREEN);
        StartCoroutine(LoadSceneAsync(nameScene));

    }

    private IEnumerator LoadSceneAsync(string nameScene)
    {
        yield return new WaitForSeconds(2f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);


        yield return new WaitUntil(() => operation.progress <= 0.9f);
    }
}
