using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [RequiredField, SerializeField] private GameObject pausePanel;
    [RequiredField, SerializeField] private GameObject hud;
    [SerializeField] private GameObject firstButton;
    //[RequiredField, SerializeField] private GameObject wiki;
    [RequiredField, SerializeField] private GameObject gamePanel;
    private bool focusSet = false;
    private void Awake() {
        pausePanel.SetActive(false);
    }

    private void Update() {
        if(InputManager.Instance != null && InputManager.Instance.isPaused && InputManager.Instance.wikiOpen != true)
        {
            pausePanel.SetActive(true);
            hud.SetActive(false);
            //wiki.SetActive(false);
            gamePanel.SetActive(false);
            if (!focusSet)
            {
                EventSystem.current.SetSelectedGameObject(firstButton);
                focusSet = true;
            }
        }
        else
        {
            pausePanel.SetActive(false);
            hud.SetActive(true);
            gamePanel.SetActive(true);
            focusSet = false;
        }
            
    }
}
