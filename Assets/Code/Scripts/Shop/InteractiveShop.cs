using System.Collections;
using UnityEngine;

public class InteractiveShop : MonoBehaviour
{
    [Header("Referencias de Interfaz")]
    [SerializeField] private GameObject _shopUIContainer;
    [SerializeField] private PanelShopController _panelShop;

    [Header("Referencias de Control")]
    [SerializeField] private PlayerCameraMovement _cameraControl;

    private bool _isShopActive = false;

    float timeSinceLastOpenedClosed = 0.16f;
    const float timeBetweenOpenClose = 0.16f;

    private void OnEnable()
    {
        // Suscribimos al evento global de teclado
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteractPerformed.AddListener(HandleKeyboardToggle);
        InputManager.Instance.OnPausePerformed.AddListener(CloseShop);
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnInteractPerformed.RemoveListener(HandleKeyboardToggle);
        InputManager.Instance.OnPausePerformed.RemoveListener(CloseShop);
    }

    private void Start()
    {
        CloseShop(); // Empezar siempre cerrada
    }

    // Este método solo se dispara cuando presionas la tecla de Shop (ej. ESC o E)
    private void HandleKeyboardToggle()
    {
        //if (InputManager.Instance.IsPaused) return;

        // IMPORTANTE: Solo permitimos cerrar con el teclado si ya está abierta.
        // Esto evita que la tienda se abra desde lejos.
        if (_isShopActive)
        {
            CloseShop();
        }
    }

    public void OpenShop()
    {
        if (timeSinceLastOpenedClosed >= timeBetweenOpenClose)
        {
            if (_isShopActive) return; // Ya está abierta, no hacemos nada
            timeSinceLastOpenedClosed = 0;
            _isShopActive = true;

            if (_shopUIContainer != null) _shopUIContainer.SetActive(true);
            if (_panelShop != null) _panelShop.ActiveShop();

            UpdateGameState(true);
            StartCoroutine(_InputDelay());
        }
    }

    public void CloseShop()
    {

        if (timeSinceLastOpenedClosed >= timeBetweenOpenClose)
        {
            if (!_isShopActive) return; // Ya está abierta, no hacemos nada
            timeSinceLastOpenedClosed = 0;

            _isShopActive = false;

            if (_shopUIContainer != null) _shopUIContainer.SetActive(false);
            if (_panelShop != null) _panelShop.DesactiveShop();

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

    private void UpdateGameState(bool shopOpen)
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
