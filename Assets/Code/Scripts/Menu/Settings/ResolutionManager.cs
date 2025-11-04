using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ResolutionManager : MonoBehaviour
{
    private TMP_Dropdown dropDown;
    Resolution[] resolutions;
    private int currentResolutionIndex;
    private void Awake() {
        if (dropDown == null)
            dropDown = GetComponent<TMP_Dropdown>();
        resolutions = Screen.resolutions;
    }
    private void Start() {
        CheckResolution();
        dropDown.onValueChanged.AddListener(ChangeResolution);
    }
    public void CheckResolution() {
        dropDown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        dropDown.AddOptions(options);

        int savedIndex = PlayerPrefs.GetInt("resolutionIndex", currentResolutionIndex);
        dropDown.value = Mathf.Clamp(savedIndex, 0, resolutions.Length - 1);
        dropDown.RefreshShownValue();

        ChangeResolution(dropDown.value);
    }
    public void ChangeResolution(int index) {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("resolutionIndex", index);
    }
}
