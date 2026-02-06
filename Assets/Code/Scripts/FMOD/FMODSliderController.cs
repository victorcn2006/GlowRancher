using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

public class FMODSliderController : MonoBehaviour
{
    [Header("FMOD Settings")]
    public string vcaPath;

    [Header("UI Feedback")]
    public GameObject iconMuted; // Arrastra aquí la imagen de "Mute" o "X"

    private FMOD.Studio.VCA _vcaController;
    private Slider _slider;
    private float _currentVolume;

    private void Start()
    {
        _vcaController = RuntimeManager.GetVCA(vcaPath);
        _slider = GetComponent<Slider>();

        if (_slider == null)
        {
            Debug.Log("Slider can`t found");
            return;
        }

        if (_slider != null)
        {
            _vcaController.getVolume(out _currentVolume);
            _slider.value = _currentVolume;

            // Ejecutar una vez al inicio para que el icono esté correcto
            UpdateMuteIcon(_currentVolume);

            _slider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        _vcaController.setVolume(volume);
        UpdateMuteIcon(volume);
    }

    // Nueva función para controlar la visibilidad del icono
    private void UpdateMuteIcon(float volume)
    {
        if (iconMuted != null)
        {
            // Se activa si el volumen es 0 o casi 0
            iconMuted.SetActive(volume <= 0.001f);
        }
    }
  
}
