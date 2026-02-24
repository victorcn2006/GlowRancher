using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;

    [Header("Audio")]
    [SerializeField] private EventReference _jumpSound;

    [Header("Input References")]
    [SerializeField] private InputActionReference _moveAction;

    // Cache de componentes
    private Rigidbody _rb;
    private Transform _mainCamTransform;

    // Estado
    private bool _canJump = true;
    private Vector2 _movementInput;

    private void Awake()
    {
        // RequireComponent garantiza que esto no sea nulo
        _rb = GetComponent<Rigidbody>();

        // Cacheamos la cámara principal (es más rápido que buscarla en cada frame)
        if (Camera.main != null)
            _mainCamTransform = Camera.main.transform;
    }

    private void OnEnable() => _moveAction.action.Enable();
    private void OnDisable() => _moveAction.action.Disable();

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        _movementInput = _moveAction.action.ReadValue<Vector2>();

        if (_movementInput.sqrMagnitude < 0.01f)
        {
            // Si no hay input, mantenemos la velocidad vertical pero frenamos la horizontal
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            return;
        }

        // Calculamos direcciones basadas en la cámara
        Vector3 camForward = Vector3.Scale(_mainCamTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(_mainCamTransform.right, new Vector3(1, 0, 1)).normalized;

        Vector3 moveDirection = (camRight * _movementInput.x + camForward * _movementInput.y).normalized;

        // Aplicamos velocidad manteniendo la inercia vertical (gravedad/salto)
        Vector3 targetVelocity = moveDirection * _walkSpeed;
        targetVelocity.y = _rb.velocity.y;

        _rb.velocity = targetVelocity;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _canJump)
        {
            // FMOD Sound
            RuntimeManager.PlayOneShot(_jumpSound, transform.position);

            // Aplicamos fuerza hacia arriba ignorando la masa para una respuesta inmediata
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    public void SetCanJump(bool value) => _canJump = value;
}
