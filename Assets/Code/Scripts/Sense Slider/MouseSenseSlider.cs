using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSenseSlider : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider _sensitivitySlider;

    private const string SENSITIVITY_KEY = "MouseSensitivity";
    private const float DEFAULT_SENSITIVITY = 0.3f;

    private void Start()
    {
        float saved = PlayerPrefs.GetFloat(SENSITIVITY_KEY, DEFAULT_SENSITIVITY);
        _sensitivitySlider.value = saved;

        _sensitivitySlider.onValueChanged.AddListener(ApplySensitivity);
    }

    private void OnDestroy()
    {
        _sensitivitySlider.onValueChanged.RemoveListener(ApplySensitivity);
    }

    private void ApplySensitivity(float value)
    {
        // Només guarda, PlayerCameraMovement ho llegirà sol al Awake
        PlayerPrefs.SetFloat(SENSITIVITY_KEY, value);
    }
}
