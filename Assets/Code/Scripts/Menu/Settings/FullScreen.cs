using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    private Toggle _toggle;
    private const string FULLSCREEN_KEY = "isFullScreen";

    private void Awake()
    {
        if (_toggle == null) _toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        if (_toggle == null)
            return;

        bool savedFullScreen = PlayerPrefs.GetInt(FULLSCREEN_KEY, 1) == 1;

        // Set the toggle WITHOUT triggering the listener
        _toggle.SetIsOnWithoutNotify(savedFullScreen);

        // Apply the saved fullscreen state
        Screen.fullScreen = savedFullScreen;

        // NOW add the listener for future changes
        _toggle.onValueChanged.AddListener(EnableFullScreen);
    }

    private void OnDisable()
    {
        if (_toggle != null)
            _toggle.onValueChanged.RemoveListener(EnableFullScreen);
    }

    public void EnableFullScreen(bool fullScreen)
    {
        Debug.Log("Fullscreen changed to: " + fullScreen);
        Screen.fullScreen = fullScreen;
        PlayerPrefs.SetInt(FULLSCREEN_KEY, fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}
