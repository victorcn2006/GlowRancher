using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [RequiredField, SerializeField] private GameObject _pausePanel;
    [RequiredField, SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _firstButton;
    //[RequiredField, SerializeField] private GameObject wiki;
    [RequiredField, SerializeField] private GameObject _gamePanel;
    private bool _focusSet = false;
    private void Awake() {
        _pausePanel.SetActive(false);
    }

    private void Update() {
        if(InputManager.Instance != null && InputManager.Instance.isPaused && InputManager.Instance.wikiOpen != true)
        {
            _pausePanel.SetActive(true);
            _hud.SetActive(false);
            //wiki.SetActive(false);
            _gamePanel.SetActive(false);
            if (!_focusSet)
            {
                EventSystem.current.SetSelectedGameObject(_firstButton);
                _focusSet = true;
            }
        }
        else
        {
            _pausePanel.SetActive(false);
            _hud.SetActive(true);
            _gamePanel.SetActive(true);
            _focusSet = false;
        }
            
    }
}
