using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCameraMovement : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Sensibilidad ajustada (valor sugerido: 1.0 a 2.0)")]
    [SerializeField] private float _rotationSensitivity = 0.2f;
    [SerializeField] private float _verticalLimit = 70f;

    [Header("References")]
    [SerializeField] private Transform _cameraTransform;

    private PlayerInput _playerInput;
    private InputAction _lookAction;
    private float _cameraVerticalAngle;

    // Propiedad que el WikiManager controlará
    public bool CanControlCamera { get; private set; } = true;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _lookAction = _playerInput.actions["Look"];

        // Estado inicial
        SetControlState(true);
    }

    private void LateUpdate()
    {
        // Si el control está desactivado (ej. Wiki abierta), no procesamos rotación
        if (!CanControlCamera) return;

        HandleRotation();
    }

    private void HandleRotation()
    {
        Vector2 input = _lookAction.ReadValue<Vector2>();
        if (input == Vector2.zero) return;

        float mouseX = input.x * _rotationSensitivity;
        float mouseY = input.y * _rotationSensitivity;

        // Rotación Vertical con Clamp
        _cameraVerticalAngle -= mouseY;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -_verticalLimit, _verticalLimit);
        _cameraTransform.localRotation = Quaternion.Euler(_cameraVerticalAngle, 0f, 0f);

        // Rotación Horizontal del cuerpo
        transform.Rotate(Vector3.up * mouseX);
    }

    // Método público para que otros scripts activen/desactiven la cámara y el ratón
    public void SetControlState(bool isEnabled)
    {
        CanControlCamera = isEnabled;

        // Gestionamos el cursor aquí mismo para centralizar
        Cursor.lockState = isEnabled ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isEnabled;
    }

    public Vector3 GetCameraRotation() => _cameraTransform.eulerAngles;
}
