using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;

public class FMODMusicManager : MonoBehaviour
{
    [Header("Configuración de FMOD")]
    [SerializeField] private EventReference _musicEvent;

    [Header("Escenas donde debe sonar")]
    [SerializeField] private List<string> _scenesToPlayMusic;

    private EventInstance _musicInstance;
    public static FMODMusicManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton para que el objeto no se destruya
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Creamos la instancia una sola vez al inicio de la vida del Manager
            if (!_musicEvent.IsNull)
            {
                _musicInstance = RuntimeManager.CreateInstance(_musicEvent);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_scenesToPlayMusic.Contains(scene.name))
        {
            PlayMusic();
        }
        else
        {
            StopMusic();
        }
    }

    private void PlayMusic()
    {
        _musicInstance.getPlaybackState(out PLAYBACK_STATE state);

        // SOLO si la música está detenida o no ha empezado, le damos Play.
        // Si ya está sonando (PLAYING), no hacemos nada y seguirá fluyendo.
        if (state == PLAYBACK_STATE.STOPPED || state == PLAYBACK_STATE.STOPPING)
        {
            _musicInstance.start();
        }
    }

    private void StopMusic()
    {
        _musicInstance.getPlaybackState(out PLAYBACK_STATE state);

        // Solo la detenemos si no está ya detenida
        if (state != PLAYBACK_STATE.STOPPED && state != PLAYBACK_STATE.STOPPING)
        {
            _musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void OnDestroy()
    {
        // Limpieza final
        _musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _musicInstance.release();
    }
}
