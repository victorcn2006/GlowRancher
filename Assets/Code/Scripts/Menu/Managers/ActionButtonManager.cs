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
        EXIT
    }
    public BUTTONS currentButton;

    private Button button;

    private void Awake() {
        if (button == null) button = GetComponent<Button>();
    }

    private void OnEnable() {
        if (button != null) button.onClick.AddListener(OnButtonPressed);
    }
    private void OnDisable() {
        button.onClick.RemoveListener(OnButtonPressed);
    }
    private void OnButtonPressed() {
        UIAudioManager.Instance?.PlayClick();
        switch (currentButton)
        {
            case BUTTONS.PLAY:
                break;
            case BUTTONS.OPTIONS:
                InputManager.Instance?.SetPause(false);
                break;
            case BUTTONS.MAINMENU:
                InputManager.Instance?.SetPause(false);
                break;
            case BUTTONS.CONTINUE:
                InputManager.Instance?.SetPause(false);
                break;
            case BUTTONS.EXIT:
                Application.Quit();
                break;
        }
    }
}
