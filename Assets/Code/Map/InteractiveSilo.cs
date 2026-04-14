using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSilo : MonoBehaviour, IInteractive
{
    [Header("Referencias de Interfaz")]
    [SerializeField] private GameObject _siloUIContainer;

    [Header("Referencias de Control")]
    [SerializeField] private PlayerCameraMovement _cameraControl;

    private bool _isSiloActive = false;

    private float _timeSinceLastOpenedClosed = 0.16f;
    private const float TIMEBETWEENOPENCLOSE = 0.16f;

    private void OnEnable()
    {
        // Suscribimos al evento global de teclado
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteractPerformed.AddListener(HandleKeyboardToggle);
        InputManager.Instance.OnPausePerformed.AddListener(CloseSilo);
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteractPerformed.RemoveListener(HandleKeyboardToggle);
        InputManager.Instance.OnPausePerformed.RemoveListener(CloseSilo);
    }

    private void Start()
    {
    }

    // Este método solo se dispara cuando presionas la tecla de Shop (ej. ESC o E)
    private void HandleKeyboardToggle()
    {
        //if (InputManager.Instance.IsPaused) return;

        // IMPORTANTE: Solo permitimos cerrar con el teclado si ya está abierta.
        // Esto evita que la tienda se abra desde lejos.
        if (_isSiloActive)
        {
            CloseSilo();
        }
    }

    public void OpenSilo()
    {
        if (_timeSinceLastOpenedClosed >= TIMEBETWEENOPENCLOSE)
        {
            if (_isSiloActive) return; // Ya está abierta, no hacemos nada
            _timeSinceLastOpenedClosed = 0;
            Debug.Log("Abriendo Mapa...");
            _isSiloActive = true;

            if (_siloUIContainer != null) _siloUIContainer.SetActive(true);


            UpdateGameState(true);
            StartCoroutine(_InputDelay());
        }
    }

    public void CloseSilo()
    {

        if (_timeSinceLastOpenedClosed >= TIMEBETWEENOPENCLOSE)
        {
            if (!_isSiloActive) return; // Ya está abierta, no hacemos nada
            _timeSinceLastOpenedClosed = 0;

            Debug.Log("Cerrando Silo...");
            _isSiloActive = false;

            if (_siloUIContainer != null) _siloUIContainer.SetActive(false);


            UpdateGameState(false);
            StartCoroutine(_InputDelay());
        }
    }

    IEnumerator _InputDelay()
    {
        while (_timeSinceLastOpenedClosed < TIMEBETWEENOPENCLOSE)
        {
            _timeSinceLastOpenedClosed += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public void UpdateGameState(bool siloOpen)
    {
        // Pausar el tiempo solo si la tienda está abierta
        Time.timeScale = siloOpen ? 0f : 1f;

        // Controlar el cursor y la cámara
        if (_cameraControl != null)
        {
            _cameraControl.SetControlState(!siloOpen);
        }

        // Mostrar/Ocultar el mouse según el estado
        Cursor.visible = siloOpen;
    }

    public void OnInteract()
    {
        OpenSilo();
    }
}
