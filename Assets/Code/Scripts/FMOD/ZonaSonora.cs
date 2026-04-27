using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(Collider))]
public class ZonaSonora : MonoBehaviour
{
    [Header("Configuración de FMOD")]
    [Tooltip("Arrastra aquí el evento de FMOD desde el Event Browser")]
    [SerializeField] private EventReference _zonaSound;

    [Header("Ajustes de Sonido")]
    [Range(0f, 5f)]
    [SerializeField] private float _volumen = 1.0f; // 1.0 es el valor por defecto

    private EventInstance _instancia;

    void Start()
    {
        // Verificar si la referencia es válida antes de crear la instancia
        if (!_zonaSound.IsNull)
        {
            _instancia = RuntimeManager.CreateInstance(_zonaSound);

            // Si el sonido es 3D, esto hace que siga la posición de este GameObject
            RuntimeManager.AttachInstanceToGameObject(_instancia, transform);

            // Aplicamos el volumen inicial
            _instancia.setVolume(_volumen);
        }
        else
        {
            Debug.LogWarning($"No se ha asignado ningún evento FMOD en {gameObject.name}");
        }
    }

    // Update se asegura de que si cambias el volumen en el Inspector en tiempo real, se aplique
    void Update()
    {
        PLAYBACK_STATE state;
        _instancia.getPlaybackState(out state);

        if (state == PLAYBACK_STATE.PLAYING)
        {
            _instancia.setVolume(_volumen);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo se activa si el objeto que entra tiene el Tag "Player"
        if (other.CompareTag("Player"))
        {
            PLAYBACK_STATE state;
            _instancia.getPlaybackState(out state);

            if (state != PLAYBACK_STATE.PLAYING)
            {
                _instancia.start();
                Debug.Log($"Reproduciendo: {gameObject.name}");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // STOP_MODE.ALLOWFADEOUT permite que se escuche el "Release" del ADSR configurado en FMOD
            _instancia.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            Debug.Log($"Deteniendo: {gameObject.name}");
        }
    }

    private void OnDestroy()
    {
        // Es vital liberar la memoria de la instancia cuando el objeto se destruye
        if (_instancia.isValid())
        {
            _instancia.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _instancia.release();
        }
    }
}
