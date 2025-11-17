using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [RequiredField, SerializeField] private GameObject pausePanel;
    [RequiredField, SerializeField] private GameObject hud;
    //[RequiredField, SerializeField] private GameObject wiki;
    [RequiredField, SerializeField] private GameObject gamePanel;
    private void Awake() {
        pausePanel.SetActive(false);
    }

    private void Update() {
        if(InputManager.Instance.isPaused && InputManager.Instance != null)
        {
            pausePanel.SetActive(true);
            hud.SetActive(false);
            //wiki.SetActive(false);
            gamePanel.SetActive(false);
        }
        else
        {
            pausePanel.SetActive(false);
           // wiki.SetActive(true);
            hud.SetActive(true);
            gameObject.SetActive(true);
        }
            
    }
}
