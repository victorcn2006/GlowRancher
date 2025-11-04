using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    [SerializeField] private Image brightnessPanel;
    private Slider slider;
    private float sliderValue;

    private void Start() {
        if(slider == null)
            slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("brightness", 0.5f);
        //Modificamos del color el parametro del alfa que es el que nos dara la sensacion de apagar la pantalla
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, sliderValue);  
    }
    public void ChangeSlider(float valor){
        sliderValue = valor;
        PlayerPrefs.SetFloat("brightness", sliderValue);
        brightnessPanel.color = new Color(brightnessPanel.color.r, brightnessPanel.color.g, brightnessPanel.color.b, sliderValue);
    }
}
