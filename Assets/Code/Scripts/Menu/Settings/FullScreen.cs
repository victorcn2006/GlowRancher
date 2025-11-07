using UnityEngine;
using UnityEngine.UI;
public class FullScreen : MonoBehaviour{
    private Toggle toggle;

    private const string FULLSCREEN_KEY = "isFullScreen";

    private void Awake() {
        if(toggle == null) toggle = GetComponent<Toggle>();
    }
    private void Start() {
        if(toggle == null) return;
        bool savedFullScreen = PlayerPrefs.GetInt(FULLSCREEN_KEY, 1) == 1;
        toggle.isOn = savedFullScreen;
        toggle.onValueChanged.AddListener(EnableFullScreen);
                
    }
    private void OnDisable(){
        toggle.onValueChanged.RemoveListener(EnableFullScreen);
    }
    private void EnableFullScreen(bool fullScreen) {
        Screen.fullScreen = fullScreen;
        PlayerPrefs.SetInt(FULLSCREEN_KEY, fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
