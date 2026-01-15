using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour {
    private TMP_Dropdown _dropdown;
    private Resolution[] _resolutions;
    private int _currentResolutionIndex;

    private const string RESOLUTION_INDEX_KEY = "resolutionIndex";
    
    private void Awake() {
        if (_dropdown == null) _dropdown = GetComponent<TMP_Dropdown>();
        InitializeDropdown();
    }

    private void Start() {
        CheckResolution();
        if(_dropdown != null) _dropdown.onValueChanged.AddListener(ChangeResolution);
    }
    private void OnDisable(){
        _dropdown.onValueChanged.RemoveListener(ChangeResolution);
    }
    private void InitializeDropdown() {
        _resolutions = Screen.resolutions;
    }
    private List<string> CreateDropdownOptions() {
        List<string> options = new List<string>();
        _currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++){
            string option = $"{_resolutions[i].width} x {_resolutions[i].height}";
            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height){
                _currentResolutionIndex = i;
            }
            
            options.Add(option);
        }
        return options;
    }
    public void ChangeResolution(int index) {
        if (index < 0 || index >= _resolutions.Length) return;
        Resolution resolution = _resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt(RESOLUTION_INDEX_KEY, index);
        PlayerPrefs.Save();
    }
    public void SetupDropdown() {
        if(_dropdown == null) return;
        _dropdown.ClearOptions();
        List<string> options = CreateDropdownOptions();
        _dropdown.AddOptions(options);
        
        int savedIndex = PlayerPrefs.GetInt(RESOLUTION_INDEX_KEY, _currentResolutionIndex);
        _dropdown.value = Mathf.Clamp(savedIndex, 0, _resolutions.Length - 1);
        _dropdown.RefreshShownValue();

        ChangeResolution(_dropdown.value);
    }

    public void CheckResolution() {
        if(_dropdown == null) return;
        SetupDropdown();
    }
}
