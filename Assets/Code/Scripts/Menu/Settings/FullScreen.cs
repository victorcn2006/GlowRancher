using UnityEngine;
using UnityEngine.UI;
public class FullScreen : MonoBehaviour{
    private Toggle _toggle;

    private const string FULLSCREEN_KEY = "isFullScreen";

    private void Awake() {
        if(_toggle == null) _toggle = GetComponent<Toggle>();
    }
    private void Start() {
        if(_toggle == null) return;
        bool savedFullScreen = PlayerPrefs.GetInt(FULLSCREEN_KEY, 1) == 1;
        _toggle.isOn = savedFullScreen;
        _toggle.onValueChanged.AddListener(EnableFullScreen);
                
    }
    private void OnDisable(){
        _toggle.onValueChanged.RemoveListener(EnableFullScreen);
    }
    private void EnableFullScreen(bool fullScreen) {
        Screen.fullScreen = fullScreen;
        PlayerPrefs.SetInt(FULLSCREEN_KEY, fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
