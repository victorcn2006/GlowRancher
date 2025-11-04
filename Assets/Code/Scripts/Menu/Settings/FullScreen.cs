using UnityEngine;
using UnityEngine.UI;
public class FullScreen : MonoBehaviour
{
    private Toggle toggle;

    private void Awake() {
        toggle = GetComponent<Toggle>();
    }
    private void Start() {
        bool savedFullScreen = PlayerPrefs.GetInt("isFullScreen", 1) == 1;
        Screen.fullScreen = savedFullScreen;
        toggle.isOn = savedFullScreen;
    }
    public void EnableFullScreen(bool fullScreen) {
        Screen.fullScreen = fullScreen;
        PlayerPrefs.SetInt("isFullScreen", fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
