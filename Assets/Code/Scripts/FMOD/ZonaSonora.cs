using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(Collider))]
public class ZonaSonora : MonoBehaviour
{
    [Header("Configuración de FMOD")]
    [SerializeField] private EventReference _zonaSound;

    [Tooltip("Nombre exacto del parámetro en FMOD")]
    [SerializeField] private string _nombreParametro = "dark";

    [Header("Ajustes de Sonido")]
    [Range(0f, 5f)]
    [SerializeField] private float _volumen = 1.0f;

    [Tooltip("1 = Corrupto, 0 = Purificado")]
    [Range(0f, 1f)]
    [SerializeField] private float _estadoInicialDark = 1.0f;

    private EventInstance _instancia;

    void Start()
    {
        if (!_zonaSound.IsNull)
        {
            _instancia = RuntimeManager.CreateInstance(_zonaSound);
            RuntimeManager.AttachInstanceToGameObject(_instancia, transform);

            // Aplicamos el volumen y el parámetro INICIAL antes de que suene
            _instancia.setVolume(_volumen);
            _instancia.setParameterByName(_nombreParametro, _estadoInicialDark);
        }
        else
        {
            Debug.LogWarning($"No se ha asignado ningún evento FMOD en {gameObject.name}");
        }
    }

    void Update()
    {
        if (_instancia.isValid())
        {
            PLAYBACK_STATE state;
            _instancia.getPlaybackState(out state);

            if (state == PLAYBACK_STATE.PLAYING)
            {
                _instancia.setVolume(_volumen);
                // Opcional: Esto permite mover el slider de 'dark' en el Inspector y verlo reflejado en vivo
                _instancia.setParameterByName(_nombreParametro, _estadoInicialDark);
            }
        }
    }

    /// <summary>
    /// Llama a esta función desde otro script para cambiar el estado de la zona.
    /// Ejemplo: zonaSonora.CambiarEstado(0f); // Para purificar
    /// </summary>
    public void CambiarEstado(float nuevoValor)
    {
        _estadoInicialDark = Mathf.Clamp01(nuevoValor);
        if (_instancia.isValid())
        {
            _instancia.setParameterByName(_nombreParametro, _estadoInicialDark);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PLAYBACK_STATE state;
            _instancia.getPlaybackState(out state);

            if (state != PLAYBACK_STATE.PLAYING)
            {
                // Aseguramos el parámetro justo antes de dar Play
                _instancia.setParameterByName(_nombreParametro, _estadoInicialDark);
                _instancia.start();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _instancia.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnDestroy()    
    {
        if (_instancia.isValid())
        {
            _instancia.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _instancia.release();
        }
    }

    //eso seria el codigo donde ira cuando el player completo un puzzle i podemos poner que zons se purifican, en este caso el bosque y el lago, pero se pueden agregar mas zonas y cambiar su estado a purificado o corrupto dependiendo de la situacion
    /*public class ControlMision : MonoBehaviour
    {
        [Header("Zonas a Purificar")]
        public ZonaSonora zonaBosque;
        public ZonaSonora zonaLago;

        public void PurificarZonas()
        {
            // Solo estos dos cambiarán a estado 0 (Purificado)
            zonaBosque.CambiarEstado(0f);
            zonaLago.CambiarEstado(0f);
        }
    }*/
}
