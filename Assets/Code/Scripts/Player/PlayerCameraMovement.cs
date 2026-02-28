using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCameraMovement : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Sensibilidad (Sugerido: 0.1 a 0.5 para Input System)")]
    [SerializeField] private float _rotationSensitivity = 0.2f;
    [SerializeField] private float _verticalLimit = 70f;

    [Header("References")]
    [SerializeField] private Transform _cameraTransform;

    private float _cameraVerticalAngle;

    // Controlado por otros sistemas (ej. WikiManager)
    public bool CanControlCamera { get; private set; } = true;

    private void Awake()
    {
        // Si no se asignó en el inspector, intenta buscar la cámara en los hijos
        if (_cameraTransform == null)
            _cameraTransform = GetComponentInChildren<Camera>().transform;

        SetControlState(true);
    }

    private void LateUpdate()
    {
        // Si el control está desactivado, no procesamos la rotación
        if (!CanControlCamera) return;

        HandleRotation();
    }

    private void HandleRotation()
    {
        // Leemos directamente del InputManager
        Vector2 input = InputManager.Instance.LookInput;

        if (input == Vector2.zero) return;

        // Multiplicamos por sensibilidad
        float mouseX = input.x * _rotationSensitivity;
        float mouseY = input.y * _rotationSensitivity;

        // 1. Rotación Vertical (Cámara) - Usamos resta para que el eje Y no esté invertido
        _cameraVerticalAngle -= mouseY;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -_verticalLimit, _verticalLimit);
        _cameraTransform.localRotation = Quaternion.Euler(_cameraVerticalAngle, 0f, 0f);

        // 2. Rotación Horizontal (Cuerpo del Player)
        transform.Rotate(Vector3.up * mouseX);
    }

    public void SetControlState(bool isEnabled)
    {
        CanControlCamera = isEnabled;

        // Gestión centralizada del cursor
        Cursor.lockState = isEnabled ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isEnabled;
    }

    public Vector3 GetCameraRotation() => _cameraTransform.eulerAngles;
}
