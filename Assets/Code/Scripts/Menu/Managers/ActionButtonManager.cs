using System.Collections;
using System.Collections.Generic;
using FMODUnity; // Necesario para RuntimeManager
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonManager : MonoBehaviour
{
    [Header("Audio de FMOD")]
    public EventReference _sonidoButtonClick;

    public enum BUTTONS
    {
        PLAY,
        OPTIONS,
        CONTINUE,
        MAINMENU,
        EXIT
    }
    public BUTTONS currentButton;

    private Button button;

    private void Awake()
    {
        if (button == null) button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (button != null) button.onClick.AddListener(OnButtonPressed);
    }

    private void OnDisable()
    {
        if (button != null) button.onClick.RemoveListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        if (!_sonidoButtonClick.IsNull)
        {
            RuntimeManager.PlayOneShot(_sonidoButtonClick, transform.position);
        }
        UIAudioManager.Instance?.PlayClick();

        switch (currentButton)
        {
            case BUTTONS.PLAY:
                break;
            case BUTTONS.OPTIONS:
            case BUTTONS.MAINMENU:
            case BUTTONS.CONTINUE:
                InputManager.Instance?.SetPause(false);
                break;
            case BUTTONS.EXIT:
                Debug.Log("Saliendo del juego...");
                Application.Quit();
                break;
        }
    }
}
