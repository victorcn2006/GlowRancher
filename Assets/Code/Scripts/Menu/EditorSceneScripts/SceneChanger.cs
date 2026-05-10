using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] protected SceneField sceneToLoad;

    public void ChangeScene()
    {
        if (LoaderScene.Instance != null)
        {
            if (sceneToLoad.SceneName == "World")
            {
                LoaderScene.Instance.LoadScene("LoadingScreen");
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad.SceneName);
            }
        }
        else
        {
            Debug.LogWarning("Cambio de escena Fallido");
        }
    }
}
