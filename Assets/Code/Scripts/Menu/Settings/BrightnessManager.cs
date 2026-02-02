using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    private const string BRIGHTNESS_KEY = "brightness";
    private const float DEFAULT_BRIGHTNESS = 0.5f;
    [SerializeField] private Image _brighnessPanel;
    private Slider _slider;
    private float _savedBrightness;
    private void Awake() {
        if(_slider == null) _slider = GetComponent<Slider>();
    }
    private void Start() {
        if(_brighnessPanel == null || _slider == null) return;
        _savedBrightness = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, DEFAULT_BRIGHTNESS);
        _slider.value = _savedBrightness;
        //Modificamos del color el parametro del alfa que es el que nos dara la sensacion de apagar la pantalla
        Update_brighnessPanel(_savedBrightness);  
        _slider.onValueChanged.AddListener(Change_slider);
    }
    private void OnDisable(){
        _slider.onValueChanged.RemoveListener(Change_slider);
    }
    public void Change_slider(float brightness){
        PlayerPrefs.SetFloat(BRIGHTNESS_KEY, brightness);
        PlayerPrefs.Save();
        Update_brighnessPanel(brightness);
    }
    private void Update_brighnessPanel(float brightness) {
        if(_brighnessPanel == null) return;
        _brighnessPanel.color = new Color(_brighnessPanel.color.r, _brighnessPanel.color.g, _brighnessPanel.color.b, brightness);
    }
}
