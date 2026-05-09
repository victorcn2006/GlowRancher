using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

[RequireComponent(typeof(Slider))]
public class FMODSliderController : MonoBehaviour
{
    [Header("FMOD Settings")]
    [Tooltip("Nombre del VCA (ambients, music o sfx)")]
    public string vcaName;

    [Header("UI Feedback")]
    public GameObject iconMuted;

    private FMOD.Studio.VCA _vcaController;
    private Slider _slider;
    private string _playerPrefKey;

    private void Start()
    {
        _slider = GetComponent<Slider>();

        // Formateamos la ruta automáticamente para que sea vca:/nombre
        string fullPath = vcaName.StartsWith("vca:/") ? vcaName : "vca:/" + vcaName;

        _vcaController = RuntimeManager.GetVCA(fullPath);
        _playerPrefKey = "Volume_" + vcaName;

        // Configuración del Slider
        _slider.minValue = 0f;
        _slider.maxValue = 1f;

        // Cargar y aplicar volumen previo
        float savedVolume = PlayerPrefs.GetFloat(_playerPrefKey, 1f);

        // Aplicamos a FMOD y al Slider
        _vcaController.setVolume(savedVolume);
        _slider.value = savedVolume;

        UpdateMuteIcon(savedVolume);

        // Listener para cambios en tiempo real
        _slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // 1. Cambiar volumen en el motor FMOD
        _vcaController.setVolume(volume);

        // 2. Guardar persistencia
        PlayerPrefs.SetFloat(_playerPrefKey, volume);

        // 3. Feedback visual
        UpdateMuteIcon(volume);
    }

    private void UpdateMuteIcon(float volume)
    {
        if (iconMuted != null)
        {
            // Se activa si el slider está al mínimo
            iconMuted.SetActive(volume <= 0.001f);
        }
    }
}
