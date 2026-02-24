using UnityEngine;

public class InteractiveShop : MonoBehaviour
{
    [Header("Referencias de Interfaz")]
    [SerializeField] private GameObject _shopUIContainer;    // El Canvas o Panel de la tienda
    [SerializeField] private PanelShopController _panelShop; // El script que maneja los botones

    [Header("Referencias de Control")]
    [SerializeField] private PlayerCameraMovement _cameraControl; // El script de la cámara

    private bool _isShopActive = false;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnShopPerformed.AddListener(ToggleShop);
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnShopPerformed.RemoveListener(ToggleShop);
    }

    private void Start()
    {
        // Estado inicial: Tienda cerrada
        _isShopActive = false;
        CloseShop();
    }

    public void ToggleShop()
    {
        // Si el juego está en pausa (menú principal de pausa), no abrir tienda
        if (InputManager.Instance != null && InputManager.Instance.IsPaused) return;

        _isShopActive = !_isShopActive;

        if (_isShopActive) OpenShop();
        else CloseShop();
    }

    public void OpenShop()
    {
        _isShopActive = true;

        if (_shopUIContainer != null) _shopUIContainer.SetActive(true);
        if (_panelShop != null) _panelShop.ActiveShop();

        UpdateGameState(true);
    }

    public void CloseShop()
    {
        _isShopActive = false;

        if (_shopUIContainer != null) _shopUIContainer.SetActive(false);
        if (_panelShop != null) _panelShop.DesactiveShop();

        UpdateGameState(false);
    }

    private void UpdateGameState(bool shopOpen)
    {
        // Pausar/Reanudar tiempo
        Time.timeScale = shopOpen ? 0f : 1f;

        // Controlar Cámara y Cursor
        if (_cameraControl != null)
        {
            _cameraControl.SetControlState(!shopOpen);
        }

        // Configuración del Cursor
        if (shopOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
