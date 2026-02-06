using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity; // 1. Importante añadir esto

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
                break;
            case BUTTONS.OPTIONS:
            case BUTTONS.MAINMENU:
            case BUTTONS.CONTINUE:
                PauseManager.instance?.SetPause();
                break;
            case BUTTONS.EXIT:
                Application.Quit();
                break;
        }
    }
}
