using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Playables;

public class LightKeySphere : MonoBehaviour, IInteractive
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _canvasHUD;
    [SerializeField] private GameObject _cutsceneRoot;
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private MonolitoManager _monolitoManager;
    [SerializeField] private EventReference _purifySound;

    [Header("Zonas a Purificar")]
    public List<ZonaSonora> zonasAPurificar; // Agrupa todas aquí en el Inspector

    private void Start()
    {
        if (_cutsceneRoot != null) _cutsceneRoot.SetActive(false);
    }

    // 1. Este es el único punto de entrada
    public void OnInteract()
    {
        // Deshabilitamos el colisionador o el script para evitar que el usuario 
        // pulse interactuar varias veces durante la cinemática
        var collider = GetComponent<Collider>();
        if (collider != null) collider.enabled = false;

        StartCoroutine(FullSequenceRoutine());
    }

    private IEnumerator FullSequenceRoutine()
    {
        // 2. PASO 1: Lógica inmediata de juego
        if (_monolitoManager != null)
            _monolitoManager.ActivateMonolito();

        // 3. PASO 2: Purificación y Sonido (Ocurre al instante)
        PurificarZonas();

        if (!_purifySound.IsNull)
        {
            RuntimeManager.PlayOneShot(_purifySound, transform.position);
        }
         
        // 4. PASO 3: Preparar la cinemática
        if (_player != null) _player.SetActive(false);
        if (_canvasHUD != null) _canvasHUD.SetActive(false);
        if (_cutsceneRoot != null) _cutsceneRoot.SetActive(true);

        // 5. PASO 4: Ejecutar y esperar a la cinemática
        if (_director != null)
        {
            _director.Play();

          

            yield return new WaitUntil(() => _director.state != PlayState.Playing);
            // 6. PASO 5: Finalizar y devolver el control
            if (_cutsceneRoot != null) _cutsceneRoot.SetActive(false);
            if (_player != null) _player.SetActive(true);
            if (_canvasHUD != null) _canvasHUD.SetActive(true);
            

        }



        // Si quieres que el objeto desaparezca tras usarlo:
        // gameObject.SetActive(false);
    }

    public void PurificarZonas()
    {
        foreach (var zona in zonasAPurificar)
        {
            if (zona != null) zona.CambiarEstado(0f);
        }
    }
}
