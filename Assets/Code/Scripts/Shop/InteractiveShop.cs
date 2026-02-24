using UnityEngine;

public class InteractiveShop : MonoBehaviour
{
    [Header("Referencias de Interfaz")]
    [SerializeField] private GameObject _shopUIContainer;    // El Canvas o Panel de la tienda
    [SerializeField] private PanelShopController _panelShop; // El script que maneja los botones de compra

    [Header("Referencias de Control")]
    [SerializeField] private PlayerCameraMovement _cameraControl; // El script de la cámara del jugador

    private bool _isShopActive = false;
    private bool _shopInteractive = false;

    private void OnEnable()
    {
        // Permitimos que también se cierre/abra con la tecla asignada en el InputManager
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
        // Aseguramos que la tienda empiece cerrada
        _isShopActive = false;
        CloseShop();
    }

    public void ToggleShop()
    {
        // Si el juego está en pausa general, no hacemos nada
        if (InputManager.Instance.IsPaused) return;

        _isShopActive = !_isShopActive;
        if (_isShopActive) OpenShop();
        else CloseShop();
    }

    public void OpenShop()
    {
        if ()
        _isShopActive = true;
        _panelShop.ActiveShop();
        UpdateGameState(true);
    }

    public void CloseShop()
    {
        _isShopActive = false;
        _shopUIContainer.SetActive(false);

        // Limpiamos la selección de ítems interna
        if (_panelShop != null)
        {
            _panelShop.DesactiveShop();
        }

        UpdateGameState(false);
    }

    private void UpdateGameState(bool shopOpen)
    {
        // Pausar o reanudar el tiempo
        Time.timeScale = shopOpen ? 0f : 1f;

        // Bloquear/Desbloquear cámara y ratón
        if (_cameraControl != null)
        {
            _cameraControl.SetControlState(!shopOpen);
        }
        else
        {
            Debug.LogError("Falta referencia a PlayerCameraMovement en InteractiveShop.");
        }
    }
}
