using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

[RequireComponent(typeof(BoxCollider))] // Obliga a tener un Collider
public class AmbienteZonaFMOD : MonoBehaviour
{
    [Header("Configuración de FMOD")]
    [SerializeField] private EventReference _ambienteEvent;

    [Header("Ajustes de Volumen")]
    [Range(0f, 1f)][SerializeField] private float _volumenMaximo = 5.0f;
    [SerializeField] private float _tiempoFade = 1.5f; // Segundos que tarda en subir/bajar el volumen

    private EventInstance _instancia;
    private Coroutine _fadeCoroutine;

    void Start()
    {
        if (!_ambienteEvent.IsNull)
        {
            _instancia = RuntimeManager.CreateInstance(_ambienteEvent);

            // Si el sonido es 3D (posicional), descomenta la siguiente línea:
            // RuntimeManager.AttachInstanceToGameObject(_instancia, transform);

            // Empezamos con volumen 0
            _instancia.setVolume(0f);
        }

        // Nos aseguramos de que el collider sea Trigger
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopFade();

            // Comprobar si ya está sonando, si no, darle Play
            PLAYBACK_STATE state;
            _instancia.getPlaybackState(out state);
            if (state != PLAYBACK_STATE.PLAYING) _instancia.start();

            // Iniciar Fade In
            _fadeCoroutine = StartCoroutine(FadeVolumen(0f, _volumenMaximo));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopFade();
            // Iniciar Fade Out y luego detener el sonido
            _fadeCoroutine = StartCoroutine(FadeVolumen(_volumenMaximo, 0f, true));
        }
    }

    private IEnumerator FadeVolumen(float startVol, float endVol, bool stopAtEnd = false)
    {
        float timer = 0;
        while (timer < _tiempoFade)
        {
            timer += Time.deltaTime;
            float newVol = Mathf.Lerp(startVol, endVol, timer / _tiempoFade);
            _instancia.setVolume(newVol);
            yield return null;
        }

        _instancia.setVolume(endVol);

        if (stopAtEnd)
        {
            _instancia.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void StopFade()
    {
        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
    }

    private void OnDestroy()
    {
        if (_instancia.isValid())
        {
            _instancia.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _instancia.release();
        }
    }
}
