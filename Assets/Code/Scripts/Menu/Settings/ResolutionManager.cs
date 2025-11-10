using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour {
    private TMP_Dropdown dropdown;
    private Resolution[] resolutions;
    private int currentResolutionIndex;

    private const string RESOLUTION_INDEX_KEY = "resolutionIndex";
    
    private void Awake() {
        if (dropdown == null) dropdown = GetComponent<TMP_Dropdown>();
        InitializeDropdown();
    }

    private void Start() {
        CheckResolution();
        if(dropdown != null) dropdown.onValueChanged.AddListener(ChangeResolution);
    }
    private void OnDisable(){
        dropdown.onValueChanged.RemoveListener(ChangeResolution);
    }
    private void InitializeDropdown() {
        resolutions = Screen.resolutions;
    }
    private List<string> CreateDropdownOptions() {
        List<string> options = new List<string>();
        currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++){
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
            
            options.Add(option);
        }
        return options;
    }
    public void ChangeResolution(int index) {
        if (index < 0 || index >= resolutions.Length) return;
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt(RESOLUTION_INDEX_KEY, index);
        PlayerPrefs.Save();
    }
    public void SetupDropdown() {
        if(dropdown == null) return;
        dropdown.ClearOptions();
        List<string> options = CreateDropdownOptions();
        dropdown.AddOptions(options);
        
        int savedIndex = PlayerPrefs.GetInt(RESOLUTION_INDEX_KEY, currentResolutionIndex);
        dropdown.value = Mathf.Clamp(savedIndex, 0, resolutions.Length - 1);
        dropdown.RefreshShownValue();

        ChangeResolution(dropdown.value);
    }

    public void CheckResolution() {
        if(dropdown == null) return;
        SetupDropdown();
    }
}
