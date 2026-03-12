using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveMap : MonoBehaviour
{
    [Header("Referencias de Interfaz")]
    [SerializeField] private GameObject _mapUIContainer;

    [Header("Referencias de Control")]
    [SerializeField] private PlayerCameraMovement _cameraControl;

    private bool _isMapActive = false;

    float timeSinceLastOpenedClosed = 0.16f;
    const float timeBetweenOpenClose = 0.16f;

    private void OnEnable()
    {
        // Suscribimos al evento global de teclado
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteractPerformed.AddListener(HandleKeyboardToggle);
        InputManager.Instance.OnPausePerformed.AddListener(CloseMap);
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteractPerformed.RemoveListener(HandleKeyboardToggle);
        InputManager.Instance.OnPausePerformed.RemoveListener(CloseMap);
    }

    private void Start()
    {
        CloseMap(); // Empezar siempre cerrada
    }

    // Este método solo se dispara cuando presionas la tecla de Shop (ej. ESC o E)
    private void HandleKeyboardToggle()
    {
        //if (InputManager.Instance.IsPaused) return;

        // IMPORTANTE: Solo permitimos cerrar con el teclado si ya está abierta.
        // Esto evita que la tienda se abra desde lejos.
        if (_isMapActive)
        {
            CloseMap();
        }
    }

    public void OpenMap()
    {
        if (timeSinceLastOpenedClosed >= timeBetweenOpenClose)
        {
            if (_isMapActive) return; // Ya está abierta, no hacemos nada
            timeSinceLastOpenedClosed = 0;
            Debug.Log("Abriendo Mapa...");
            _isMapActive = true;

            if (_mapUIContainer != null) _mapUIContainer.SetActive(true);
            

            UpdateGameState(true);
            StartCoroutine(_InputDelay());
        }
    }

    public void CloseMap()
    {

        if (timeSinceLastOpenedClosed >= timeBetweenOpenClose)
        {
            if (!_isMapActive) return; // Ya está abierta, no hacemos nada
            timeSinceLastOpenedClosed = 0;

            Debug.Log("Cerrando Mapa...");
            _isMapActive = false;

            if (_mapUIContainer != null) _mapUIContainer.SetActive(false);
            

            UpdateGameState(false);
            StartCoroutine(_InputDelay());
        }
    }

    IEnumerator _InputDelay()
    {
        while (timeSinceLastOpenedClosed < timeBetweenOpenClose)
        {
            timeSinceLastOpenedClosed += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public void UpdateGameState(bool shopOpen)
    {
        // Pausar el tiempo solo si la tienda está abierta
        Time.timeScale = shopOpen ? 0f : 1f;

        // Controlar el cursor y la cámara
        if (_cameraControl != null)
        {
            _cameraControl.SetControlState(!shopOpen);
        }

        // Mostrar/Ocultar el mouse según el estado
        Cursor.visible = shopOpen;
        Cursor.lockState = shopOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
