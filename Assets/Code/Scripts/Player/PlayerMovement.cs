using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;

    [Header("Audio")]
    [SerializeField] private EventReference _jumpSound;

    [Header("Input References")]
    [SerializeField] private InputActionReference _moveAction;

    // Componentes cacheados
    private Rigidbody _rb;
    private Transform _mainCameraTransform;

    // Estado
    private Vector2 _inputDirection;
    private bool _canJump;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        // Cacheamos la cámara principal (es más eficiente que buscarla siempre)
        if (Camera.main != null)
            _mainCameraTransform = Camera.main.transform;
    }

    private void OnEnable() => _moveAction.action.Enable();
    private void OnDisable() => _moveAction.action.Disable();

    private void Update()
    {
        // El input se lee en Update para mayor precisión de respuesta
        _inputDirection = _moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        if (_mainCameraTransform == null) return;

        // Calculamos la dirección relativa a la cámara
        Vector3 forward = _mainCameraTransform.forward;
        Vector3 right = _mainCameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * _inputDirection.y + right * _inputDirection.x).normalized;

        // Aplicamos velocidad manteniendo la gravedad actual
        Vector3 targetVelocity = moveDirection * _walkSpeed;
        targetVelocity.y = _rb.velocity.y;

        _rb.velocity = targetVelocity;
    }

    /// <summary>
    /// Este método se conecta desde el componente PlayerInput (Events)
    /// </summary>
    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !_canJump) return;

        // Lógica de Salto
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z); // Limpiamos velocidad vertical previa
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

        // Audio
        if (!_jumpSound.IsNull)
        {
            RuntimeManager.PlayOneShot(_jumpSound, transform.position);
        }
    }

    public void SetCanJump(bool value)
    {
        _canJump = value;
    }
}
