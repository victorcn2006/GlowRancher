using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour {
    private TMP_Dropdown _dropdown;
    private Resolution[] _uniqueResolutions;
    private int _currentResolutionIndex;
    private const string RESOLUTION_INDEX_KEY = "resolutionIndex";

    private void Start() {
        _dropdown = GetComponent<TMP_Dropdown>();
        if (_dropdown == null) {
            Debug.LogWarning("There's no dropdown component");
            return;
        }

        InitializeResoltions();
        SetupDropdown();

        _dropdown.onValueChanged.AddListener(ChangeResolution);
    }
    private void OnDisable(){
        if (_dropdown != null) _dropdown.onValueChanged.RemoveListener(ChangeResolution);
    }
    private void InitializeResoltions() {
        //By default Screen.resolutions has duplicate entries
        Resolution[] allResolutions = Screen.resolutions;
        //Save only unique entries
        List<Resolution> uniqueResolutions = new List<Resolution>();

        foreach (Resolution resolution in allResolutions)
        {
            string resolutionKey = $"{resolution.width}x{resolution.height}";

            // Check if we already have this resolution
            bool alreadyExists = false;
            foreach (Resolution existing in uniqueResolutions)
            {
                if (existing.width == resolution.width && existing.height == resolution.height)
                {
                    alreadyExists = true;
                    break;
                }
            }

            if (!alreadyExists)
            {
                uniqueResolutions.Add(resolution);
            }
        }

        _uniqueResolutions = uniqueResolutions.ToArray(); //Converts a list to an array

    }
    private List<string> CreateDropdownOptions() {
        List<string> options = new List<string>();
        _currentResolutionIndex = 0;

        for (int i = 0; i < _uniqueResolutions.Length; i++){
            string option = $"{_uniqueResolutions[i].width} x {_uniqueResolutions[i].height}";
            if (_uniqueResolutions[i].width == Screen.currentResolution.width &&
                _uniqueResolutions[i].height == Screen.currentResolution.height){

                _currentResolutionIndex = i;

            }
            options.Add(option);
        }
        return options;
    }
    public void ChangeResolution(int index) {

        if (index < 0 || index >= _uniqueResolutions.Length)
        {
            Debug.LogWarning($"Invalid resolution index: {index}");
            return;
        }

        Resolution resolution = _uniqueResolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt(RESOLUTION_INDEX_KEY, index);
        PlayerPrefs.Save();
    }
    public void SetupDropdown() {
        _dropdown.ClearOptions();
        List<string> options = CreateDropdownOptions();
        _dropdown.AddOptions(options);
        
        int savedIndex = PlayerPrefs.GetInt(RESOLUTION_INDEX_KEY, _currentResolutionIndex);
        _dropdown.value = Mathf.Clamp(savedIndex, 0, _uniqueResolutions.Length - 1);
        _dropdown.RefreshShownValue();

        ChangeResolution(_dropdown.value);
    }
    /*
    public void CheckResolution() {
        if(_dropdown == null) return;
        InitializeResoltions();
        SetupDropdown();
    }*/
}
