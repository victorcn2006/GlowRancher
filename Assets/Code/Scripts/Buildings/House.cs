using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour, IInteractive
{
    [Header("UI References")]
    private GameObject _houseUIContainer;
    private HouseShopController _houseShopController;

    [Header("Control References")]
    private PlayerCameraMovement _cameraControl;

    private bool _isPanelActive = false;
    private float _timeSinceLastToggle = 0.16f;
    private const float TOGGLE_COOLDOWN = 0.16f;

    private void OnEnable()
    {
        SubscribeToInputs();
    }

    private void OnDisable()
    {
        UnsubscribeFromInputs();
    }

    private void SubscribeToInputs()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPerformed.AddListener(HandleKeyboardToggle);
            InputManager.Instance.OnPausePerformed.AddListener(ClosePanel);
        }
    }

    private void UnsubscribeFromInputs()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPerformed.RemoveListener(HandleKeyboardToggle);
            InputManager.Instance.OnPausePerformed.RemoveListener(ClosePanel);
        }
    }

    private void Start()
    {
        if (References.Instance != null) {
            _cameraControl = References.Instance._playerCameraMovement;
            _houseUIContainer = References.Instance._houseShopPanel;
        }
        // If subscription failed in OnEnable because InputManager wasn't ready, try again here
        SubscribeToInputs();

        // Force the panel to be closed at start and synchronize state
        _isPanelActive = true; 
        _timeSinceLastToggle = TOGGLE_COOLDOWN;
        ClosePanel();
    }

    public void HandleKeyboardToggle()
    {
        if (_isPanelActive)
        {
            ClosePanel();
        }
    }

    public void OpenPanel()
    {
        if (_timeSinceLastToggle >= TOGGLE_COOLDOWN)
        {
            if (_isPanelActive) return;
            
            _timeSinceLastToggle = 0;
            _isPanelActive = true;

            if (_houseUIContainer != null) 
                _houseUIContainer.SetActive(true);
            else
                Debug.LogWarning("House: _houseUIContainer is null!");

            if (_houseShopController != null) 
                _houseShopController.ActiveShop();
            else
                Debug.LogWarning("House: _houseShopController is null!");

            UpdateGameState(true);
            StartCoroutine(ToggleDelay());
        }
    }

    public void ClosePanel()
    {
        if (_timeSinceLastToggle >= TOGGLE_COOLDOWN)
        {
            if (!_isPanelActive) return;
            
            _timeSinceLastToggle = 0;
            _isPanelActive = false;

            if (_houseUIContainer != null) 
                _houseUIContainer.SetActive(false);
            
            if (_houseShopController != null) 
                _houseShopController.DesactiveShop();

            UpdateGameState(false);
            StartCoroutine(ToggleDelay());
        }
    }

    private IEnumerator ToggleDelay()
    {
        while (_timeSinceLastToggle < TOGGLE_COOLDOWN)
        {
            _timeSinceLastToggle += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private void UpdateGameState(bool isActive)
    {
        // Pause time only if panel is active
        Time.timeScale = isActive ? 0f : 1f;

        // Control cursor and camera
        if (_cameraControl != null)
        {
            _cameraControl.SetControlState(!isActive);
        }
        else
        {
            Debug.LogWarning("House: _cameraControl is null!");
        }

        // Show/Hide mouse cursor based on state
        Cursor.visible = isActive;
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void OnInteract()
    {
        OpenPanel();
    }
}
