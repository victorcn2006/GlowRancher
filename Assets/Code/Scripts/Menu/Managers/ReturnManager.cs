using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnManager : MonoBehaviour
{
    [SerializeField] private string _targetSceneName = "MainMenu";
    private void OnEnable()
    {
        if(InputManager.Instance != null)
        {
            InputManager.Instance.OnPausePerformed.AddListener(ChangeToScene);
        }
    }
    private void OnDisable()
    {
        if (InputManager.Instance != null) {
            InputManager.Instance.OnPausePerformed.RemoveListener(ChangeToScene);
        }
    }

    private void ChangeToScene() {
        SceneManager.LoadScene(_targetSceneName);
    }
}
