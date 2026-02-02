using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public static PauseManager instance { get; private set; }

    [Header("UI Panels")]
    [RequiredField, SerializeField] private GameObject _pausePanel;
    [RequiredField, SerializeField] private GameObject _hud;
    [RequiredField, SerializeField] private GameObject _gamePanel;

    [Header("UI Navigation")]
    [SerializeField] private GameObject _firstButton;

    private bool _focusSet = false;
    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        if (_pausePanel != null)
            _pausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        InputManager.Instance.OnPausePerformed.AddListener(SetPause);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnPausePerformed.RemoveListener(SetPause);
    }


    public void SetPause() {

        // Toggle the pause state
        if (_pausePanel.activeSelf)
        {
            // Pause panel is active, so unpause
            DisablePausePanel();
            _focusSet = false;
            InputManager.Instance.IsPaused = false;
        }
        else if (!InputManager.Instance.IsWikiOpen)
        {
            // Pause panel is not active and wiki is not open, so pause
            ActivePausePanel();
            if (!_focusSet)
            {
                EventSystem.current.SetSelectedGameObject(_firstButton);
                _focusSet = true;
            }
            InputManager.Instance.IsPaused = true;
        }

    }

    private void ActivePausePanel() {
        _pausePanel.SetActive(true);
        _hud.SetActive(false);
        _gamePanel.SetActive(false);
        Time.timeScale = 0f;
    }

    private void DisablePausePanel(){
        _pausePanel.SetActive(false);
        _hud.SetActive(true);
        _gamePanel.SetActive(true);
        Time.timeScale = 1f;

    }
}
