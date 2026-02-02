using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonManager : MonoBehaviour{
    public enum BUTTONS{
        PLAY,
        OPTIONS,
        CONTINUE,
        MAINMENU,
        CREDITS,
        EXIT
    }
    public BUTTONS currentButton;

    private Button _button;

    private void Awake() {
        if (_button == null) _button = GetComponent<Button>();
    }

    private void OnEnable() {
        if (_button != null) _button.onClick.AddListener(OnButtonPressed);
    }
    private void OnDisable() {
        _button.onClick.RemoveListener(OnButtonPressed);
    }
    private void OnButtonPressed() {
        UIAudioManager.Instance?.PlayClick();
        switch (currentButton)
        {
            case BUTTONS.PLAY:
                break;
            case BUTTONS.OPTIONS:
                PauseManager.instance?.SetPause();
                break;
            case BUTTONS.MAINMENU:
                PauseManager.instance?.SetPause();
                break;
            case BUTTONS.CONTINUE:
                PauseManager.instance?.SetPause();
                break;
            case BUTTONS.CREDITS:
                PauseManager.instance?.SetPause();
                break;
            case BUTTONS.EXIT:
                Application.Quit();
                break;
        }
    }
}
