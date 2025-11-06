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
    
    private void Awake(){
        if(slider == null) slider = GetComponentInChildren<Slider>();
        switch (sliderType){
            case SLIDERS.GENERAL: volumeParameter = "General"; break;
            case SLIDERS.MUSIC: volumeParameter = "Music"; break;
            case SLIDERS.GUI: volumeParameter = "GUI"; break;
        }
    }

    private void Start(){
        if(slider == null || mixer == null) return;
        
        // Cargar valor guardado si existe (sin usar default)
        string key = GetPlayerPrefsKey();
        if(PlayerPrefs.HasKey(key)){
            slider.value = PlayerPrefs.GetFloat(key);
        }
        
        SetVolume(slider.value);
        CheckMute();
        slider.onValueChanged.AddListener(ChangeSlider);
    }
    
    private string GetPlayerPrefsKey(){
        switch (sliderType){
            case SLIDERS.GENERAL: return VOLUME_KEY;
            case SLIDERS.MUSIC: return MUSIC_KEY;
            case SLIDERS.GUI: return GUI_KEY;
            default: return VOLUME_KEY;
        }
    }
    private void OnDisable(){
        if(slider != null) slider.onValueChanged.RemoveListener(ChangeSlider);
    }
    private void ChangeSlider(float volume){
        if(slider == null || mixer == null) return;
        
        string key = GetPlayerPrefsKey();
        PlayerPrefs.SetFloat(key, volume);
        PlayerPrefs.Save();
        SetVolume(volume);
        CheckMute();
    }
    private void SetVolume(float value){
        if(mixer == null || string.IsNullOrEmpty(volumeParameter)) return;
        // Evitar valores negativos o cero
        value = Mathf.Clamp(value, 0.0001f, 1f);
        float minDB = -60f;
        float maxDB = 15f;
        float curve = 0.3f;
        float curvedValue = Mathf.Pow(value, curve);
        float dB = Mathf.Lerp(minDB, maxDB, curvedValue);
        mixer.SetFloat(volumeParameter, dB);

    }
    private void CheckMute(){
        if(imageMute == null || slider == null) return;
        imageMute.SetActive(slider.value <= 0.001f);
    }
}
