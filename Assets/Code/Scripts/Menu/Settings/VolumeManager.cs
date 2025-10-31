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
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
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
        if(sliderValue == 0)
        {
            imageMute.SetActive(true);
        } else
        {
            imageMute.SetActive(false);
        }
    }
}
