using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

public class FMODSliderController : MonoBehaviour
{
    [Header("FMOD Settings")]
    public string vcaPath;
    // Usamos una constante para evitar errores de escritura al llamar a PlayerPrefs
    private string PLAYER_PREF_KEY;

    [Header("UI Feedback")]
    public GameObject iconMuted;

    private FMOD.Studio.VCA _vcaController;
    private Slider _slider;

    private void Start()
    {
        _vcaController = RuntimeManager.GetVCA(vcaPath);
        _slider = GetComponent<Slider>();

        // Creamos una clave única basada en el path del VCA (por si tienes varios sliders)
        PLAYER_PREF_KEY = "Volume_" + vcaPath;

        if (_slider == null)
        {
            Debug.LogError("Slider not found on " + gameObject.name);
            return;
        }

        // 1. Cargar el valor guardado. Si no existe, usamos 1 (volumen máximo) por defecto.
        float savedVolume = PlayerPrefs.GetFloat(PLAYER_PREF_KEY, 1f);

        // 2. Aplicar el volumen cargado a FMOD y al Slider
        _vcaController.setVolume(savedVolume);
        _slider.value = savedVolume;

        // 3. Actualizar el icono de mute
        UpdateMuteIcon(savedVolume);

        // 4. Escuchar cambios del slider
        _slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Aplicar volumen en FMOD
        _vcaController.setVolume(volume);

        // Guardar el valor en el dispositivo
        PlayerPrefs.SetFloat(PLAYER_PREF_KEY, volume);
        PlayerPrefs.Save(); // Asegura que se escriba en el disco de inmediato

        UpdateMuteIcon(volume);
    }

    private void UpdateMuteIcon(float volume)
    {
        if (iconMuted != null)
        {
            iconMuted.SetActive(volume <= 0.001f);
        }
    }
}
