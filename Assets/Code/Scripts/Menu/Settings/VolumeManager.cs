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

    [SerializeField] private SLIDERS sliderType = SLIDERS.GENERAL;
    [SerializeField] private GameObject imageMute;
    [SerializeField] private AudioMixer mixer;

    private string volumeParameter;
    private Slider slider;

    private void Awake() {
        slider = GetComponentInChildren<Slider>();

        switch (sliderType) {
            case SLIDERS.GENERAL: volumeParameter = "General"; break;
            case SLIDERS.MUSIC:   volumeParameter = "Music"; break;
            case SLIDERS.GUI:     volumeParameter = "GUI"; break;
        }
    }

    private void Start() {
        if (slider == null || mixer == null) return;

        // Cargar valor guardado
        string key = GetPlayerPrefsKey();
        if (PlayerPrefs.HasKey(key))
            slider.value = PlayerPrefs.GetFloat(key);

        ApplyVolume(slider.value);

        slider.onValueChanged.AddListener(OnSliderChanged);

        UpdateMuteIcon();
    }

    private void OnDisable() {
        if (slider != null)
            slider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private string GetPlayerPrefsKey() {
        return sliderType switch {
            SLIDERS.GENERAL => VOLUME_KEY,
            SLIDERS.MUSIC => MUSIC_KEY,
            SLIDERS.GUI => GUI_KEY,
            _ => VOLUME_KEY
        };
    }

    private void OnSliderChanged(float value) {
        if (value < 0.001f) {
            // Evita loops
            slider.onValueChanged.RemoveListener(OnSliderChanged);
            slider.value = 0f;
            slider.onValueChanged.AddListener(OnSliderChanged);
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

        mixer.SetFloat(volumeParameter, dB);
    }

    private void UpdateMuteIcon() {
        if (imageMute != null)
            imageMute.SetActive(slider.value <= 0.001f);
    }
}
