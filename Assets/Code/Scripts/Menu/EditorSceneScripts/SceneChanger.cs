using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] protected SceneField sceneToLoad;

    public void ChangeScene()
    {
        if (LoaderScene.Instance != null)
        {
            LoaderScene.Instance.LoadScene(sceneToLoad.SceneName);
        }
        else
        {
            Debug.LogWarning("No hay una instancia de LoaderScene en la jerarquía.");
        }
    }
}
