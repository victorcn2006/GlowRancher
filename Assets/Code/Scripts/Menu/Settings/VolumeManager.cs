using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeManager : MonoBehaviour
{
    public enum SLIDERS {
        GENERAL,
        MUSIC,
        GUI
    }
    private Slider slider;
    private float sliderValue;
    public GameObject imageMute;
    [SerializeField] AudioMixer mixer;
    private string volumeParameter;

    [SerializeField] private SLIDERS sliderType = SLIDERS.GENERAL;
    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        switch (sliderType)
        {
            case SLIDERS.GENERAL: volumeParameter = "General"; break;
            case SLIDERS.MUSIC: volumeParameter = "Music"; break;
            case SLIDERS.GUI: volumeParameter = "GUI"; break;
        }
    }

    private void Start()
    {
        switch (sliderType)
        {
            case SLIDERS.GENERAL: sliderValue = PlayerPrefs.GetFloat("generalAudio", 0.5f); break;
            case SLIDERS.MUSIC: sliderValue = PlayerPrefs.GetFloat("musicAudio", 0.5f); break;
            case SLIDERS.GUI: sliderValue = PlayerPrefs.GetFloat("guiAudio", 0.5f); break;
        }
        slider.value = sliderValue;
        SetVolume(slider.value);
        CheckMute();
    }
    public void ChangeSlider(float valor){
        sliderValue = valor;
        switch (sliderType)
        {
            case SLIDERS.GENERAL: PlayerPrefs.SetFloat("generalAudio", sliderValue); break;
            case SLIDERS.MUSIC: PlayerPrefs.SetFloat("musicAudio", sliderValue); break;
            case SLIDERS.GUI: PlayerPrefs.SetFloat("guiAudio", sliderValue); break;
        }
        SetVolume(slider.value);
        CheckMute();
    }
    private void SetVolume(float value)
    {
        // Evitar valores negativos o cero
        value = Mathf.Clamp(value, 0.0001f, 1f);
        float minDB = -60f;
        float maxDB = 15f;
        float curve = 0.3f;
        float curvedValue = Mathf.Pow(value, curve); // curva más suave entre 0 y 1
        float dB = Mathf.Lerp(minDB, maxDB, curvedValue);
        mixer.SetFloat(volumeParameter, dB);

    }
    public void CheckMute(){
        imageMute.SetActive(slider.value <= 0.001f);
    }
}
