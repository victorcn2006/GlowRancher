using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    private const string BRIGHTNESS_KEY = "brightness";
    private const float DEFAULT_BRIGHTNESS = 0.5f;
    [SerializeField] private Image brightnessPanel;
    private Slider slider;
    private float savedBrightness;
    private void Awake() {
        if(slider == null) slider = GetComponent<Slider>();
    }
    private void Start() {
        if(brightnessPanel == null || slider == null) return;
        savedBrightness = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, DEFAULT_BRIGHTNESS);
        slider.value = savedBrightness;
        //Modificamos del color el parametro del alfa que es el que nos dara la sensacion de apagar la pantalla
        UpdateBrightnessPanel(savedBrightness);  
        slider.onValueChanged.AddListener(ChangeSlider);
    }
    private void OnDisable(){
        slider.onValueChanged.RemoveListener(ChangeSlider);
    }
    public void ChangeSlider(float brightness){
        PlayerPrefs.SetFloat(BRIGHTNESS_KEY, brightness);
        PlayerPrefs.Save();
        UpdateBrightnessPanel(brightness);
    }
    private void UpdateBrightnessPanel(float brightness) {
        if(brightnessPanel == null) return;
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, brightness);
    }
}
