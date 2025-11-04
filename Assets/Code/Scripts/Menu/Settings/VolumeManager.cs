using UnityEngine;
using UnityEngine.UI;
public class VolumeManager : MonoBehaviour
{
    private Slider slider;
    private float sliderValue;
    public GameObject imageMute;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        sliderValue = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        slider.value = sliderValue;
        AudioListener.volume = slider.value;
        CheckMute();
    }
    public void ChangeSlider(float valor){ 
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        CheckMute();
    }
    public void CheckMute(){
        imageMute.SetActive(slider.value <= 0.001f);
    }
}
