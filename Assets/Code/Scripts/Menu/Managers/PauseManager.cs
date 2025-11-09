using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [RequiredField, SerializeField] private GameObject pausePanel;

    private void Awake() {
        pausePanel.SetActive(false);
    }

    private void Update() {
        if(InputManager.Instance.isPaused)
            pausePanel.SetActive(true);
        else
            pausePanel.SetActive(false);
    }



}
