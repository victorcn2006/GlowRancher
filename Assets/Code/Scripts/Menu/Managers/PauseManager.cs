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
    [RequiredField, SerializeField] private GameObject gamePanel;

    private void Awake() {
        pausePanel.SetActive(false);
    }

    private void Start() {
        if (UIInputManager.Instance != null)
        {
            UIInputManager.Instance.OnPauseStateChanged += ChangeStatePause;
        }
        else
        {
            Debug.LogError("PauseManager: InputManager instance not found!");
        }
    }

    private void OnDestroy() {
        if (UIInputManager.Instance != null)
        {
            UIInputManager.Instance.OnPauseStateChanged -= ChangeStatePause;
        }
    }

    private void ChangeStatePause(bool isPaused) {
        if (isPaused && !InputManager.Instance.wikiOpen)
        {
            ShowPauseMenu();
        }
        else
        {
            HidePauseMenu();
        }
    }

    private void ShowPauseMenu() {
        pausePanel.SetActive(true);
        hud.SetActive(false);
        gamePanel.SetActive(false);

        // Set focus to first button
        if (firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }

    private void HidePauseMenu() {
        pausePanel.SetActive(false);
        hud.SetActive(true);
        gamePanel.SetActive(true);

        // Clear selected object when unpausing
        EventSystem.current.SetSelectedGameObject(null);
    }
}
