using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using UnityEditor;
using UnityEngine.SceneManagement; // 1. Importante añadir esto

public class ActionButtonManager : MonoBehaviour
{

    [Header("Configuración de Sonido")]
    public EventReference clickSound; // 2. Variable para asignar el evento desde el Inspector

    public enum BUTTONS
    {
        PLAY,
        OPTIONS,
        CONTINUE,
        MAINMENU,
        CREDITS,
        EXIT
    }
    public BUTTONS currentButton;

    private Button _button;

    private void Awake()
    {
        if (_button == null) _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (_button != null) _button.onClick.AddListener(OnButtonPressed);
    }
    private void OnDisable()
    {
        if (_button != null) _button.onClick.RemoveListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        // 3. Reproducir el sonido de FMOD
        if (!clickSound.IsNull)
        {
            RuntimeManager.PlayOneShot(clickSound, transform.position);
        }

        // Tu lógica actual
        UIAudioManager.Instance?.PlayClick();

        switch (currentButton)
        {
            case BUTTONS.PLAY:
                SceneManager.LoadScene("LoadingScreen");
                break;
            case BUTTONS.OPTIONS:
                SceneManager.LoadScene("Options_Redesign");
                break;
            case BUTTONS.MAINMENU:
                SceneManager.LoadScene("MainMenu_Redesign");
                break;
            case BUTTONS.CONTINUE:
                PauseManager.instance?.SetPause();
                break;
            case BUTTONS.CREDITS:
                SceneManager.LoadScene("Credits");
                break;
            case BUTTONS.EXIT:
                GameManager.Instance.SaveStats();
                #if UNITY_EDITOR
                    EditorApplication.isPlaying = false; // Detiene el juego en el editor
                #else
                    Application.Quit();
                    
                    
                #endif
                break;
        }
    }
}
