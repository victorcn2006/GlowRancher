using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCameraMovement : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Sensibilidad ajustada para un movimiento fluido.")]
    [SerializeField] private float _rotationSensitivity = 1.5f;
    [SerializeField] private float _verticalLimit = 70f;

    [Header("References")]
    [SerializeField] private Transform _cameraTransform;

    private PlayerInput _playerInput;
    private InputAction _lookAction;
    private float _cameraVerticalAngle;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _lookAction = _playerInput.actions["Look"];

        // Es mejor ocultar el cursor en Start o mediante un GameManager, 
        // pero lo mantenemos aquí por funcionalidad.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Usamos LateUpdate para cámaras para asegurar que el movimiento 
    // del jugador ya ocurrió en Update
    private void LateUpdate()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        Vector2 input = _lookAction.ReadValue<Vector2>();

        if (input == Vector2.zero) return;

        // Multiplicamos por un factor base para que el valor de sensibilidad sea intuitivo
        float mouseX = input.x * _rotationSensitivity;
        float mouseY = input.y * _rotationSensitivity;

        // 1. Rotación Vertical (Cámara)
        _cameraVerticalAngle -= mouseY; // Invertido para que sea natural
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -_verticalLimit, _verticalLimit);
        _cameraTransform.localRotation = Quaternion.Euler(_cameraVerticalAngle, 0f, 0f);

        // 2. Rotación Horizontal (Cuerpo del Jugador)
        transform.Rotate(Vector3.up * mouseX);
    }

    public Vector3 GetCameraRotation() => _cameraTransform.eulerAngles;
}
