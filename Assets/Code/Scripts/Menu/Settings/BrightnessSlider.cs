using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BrightnessSlider : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if (BrightnessManager.instance != null)
        {
            float val = BrightnessManager.instance.GetBrightness();
            _slider.SetValueWithoutNotify(val);
        }
        else
        {
            Debug.LogError("[BrightnessSlider] BrightnessManager.instance is NULL! Make sure the manager exists in the first scene.");
        }
        
    }

    public void OnSliderValueChanged(float value)
    {
        Debug.Log($"[BrightnessSlider] Slider moved to: {value}");
        if (BrightnessManager.instance != null)
        {
            BrightnessManager.instance.SetBrightness(value);
        }
    }
}
