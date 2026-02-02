using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {
    public enum SLIDERS {
        GENERAL,
        MUSIC,
        GUI
    }

    private const string VOLUME_KEY = "generalAudio";
    private const string MUSIC_KEY = "musicAudio";
    private const string GUI_KEY = "guiAudio";

    [SerializeField] private SLIDERS _sliderType = SLIDERS.GENERAL;
    [SerializeField] private GameObject _imageMute;
    [SerializeField] private AudioMixer _mixer;

    private string _volumeParameter;
    private Slider _slider;

    private void Awake() {
        _slider = GetComponentInChildren<Slider>();

        switch (_sliderType) {
            case SLIDERS.GENERAL: _volumeParameter = "General"; break;
            case SLIDERS.MUSIC:   _volumeParameter = "Music"; break;
            case SLIDERS.GUI:     _volumeParameter = "GUI"; break;
        }
    }

    private void Start() {
        if (_slider == null || _mixer == null) return;

        // Cargar valor guardado
        string key = GetPlayerPrefsKey();
        if (PlayerPrefs.HasKey(key))
            _slider.value = PlayerPrefs.GetFloat(key);

        ApplyVolume(_slider.value);

        _slider.onValueChanged.AddListener(OnSliderChanged);

        UpdateMuteIcon();
    }

    private void OnDisable() {
        if (_slider != null)
            _slider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private string GetPlayerPrefsKey() {
        return _sliderType switch {
            SLIDERS.GENERAL => VOLUME_KEY,
            SLIDERS.MUSIC => MUSIC_KEY,
            SLIDERS.GUI => GUI_KEY,
            _ => VOLUME_KEY
        };
    }

    private void OnSliderChanged(float value) {
        if (value < 0.001f) {
            // Evita loops
            _slider.onValueChanged.RemoveListener(OnSliderChanged);
            _slider.value = 0f;
            _slider.onValueChanged.AddListener(OnSliderChanged);
            value = 0f;
        }

        PlayerPrefs.SetFloat(GetPlayerPrefsKey(), value);
        PlayerPrefs.Save();

        ApplyVolume(value);
        UpdateMuteIcon();
    }

    private void ApplyVolume(float value) {
        value = Mathf.Clamp01(value);

        // dB mínimos para silencio real
        float minDB = -80f;  
        float maxDB = 0f;

        // Curva logarítmica
        float curve = 0.3f;
        float curvedValue = Mathf.Pow(value, curve);
        float dB = Mathf.Lerp(minDB, maxDB, curvedValue);

        // Asegurar mute REAL
        if (value <= 0.0001f)
            dB = minDB;

        _mixer.SetFloat(_volumeParameter, dB);
    }

    private void UpdateMuteIcon() {
        if (_imageMute != null)
            _imageMute.SetActive(_slider.value <= 0.001f);
    }
}
