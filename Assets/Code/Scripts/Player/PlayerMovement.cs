using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _doubleJumpForce = 8f; // Fuerza mayor para el segundo salto

    [Header("Audio")]
    [SerializeField] private EventReference _jumpSound;

    // Cache de componentes
    private Rigidbody _rb;
    private Transform _mainCamTransform;

    // Estado
    private bool _canJump = true;          // ¿Está en el suelo?
    private bool _hasDoubleJumpItem = false; // ¿Ha recogido el ítem?
    private bool _didDoubleJump = false;    // ¿Ya usó el doble salto en el aire?

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (Camera.main != null)
            _mainCamTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        // Suscripción al evento de salto del InputManager
        if (InputManager.Instance != null)
            InputManager.Instance.OnJumpPerformed.AddListener(HandleJump);
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnJumpPerformed.RemoveListener(HandleJump);
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        Vector2 input = InputManager.Instance.MoveInput;

        if (input.sqrMagnitude < 0.01f)
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            return;
        }

        Vector3 camForward = Vector3.Scale(_mainCamTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(_mainCamTransform.right, new Vector3(1, 0, 1)).normalized;

        Vector3 moveDirection = (camRight * input.x + camForward * input.y).normalized;

        Vector3 targetVelocity = moveDirection * _walkSpeed;
        targetVelocity.y = _rb.velocity.y;

        _rb.velocity = targetVelocity;
    }

    private void HandleJump()
    {
        // 1. SALTO NORMAL (Desde el suelo)
        if (_canJump)
        {
            PerformJump(_jumpForce);
            _didDoubleJump = false; // Resetear el uso del doble salto al saltar desde el suelo
        }
        // 2. DOBLE SALTO (En el aire, si tiene el item y no lo ha usado aún)
        else if (_hasDoubleJumpItem && !_didDoubleJump)
        {
            _didDoubleJump = true;
            PerformJump(_doubleJumpForce); // Usamos la fuerza potenciada
        }
    }

    private void PerformJump(float force)
    {
        // Sonido FMOD
        RuntimeManager.PlayOneShot(_jumpSound, transform.position);

        // Reset de velocidad vertical para que el salto sea siempre igual de potente
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

        // Aplicar fuerza hacia arriba
        _rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    // Métodos para ser llamados desde otros scripts
    public void SetCanJump(bool value)
    {
        _canJump = value;
        if (_canJump) _didDoubleJump = false; // Reset al tocar suelo
    }

    public void EnableDoubleJumpItem()
    {
        _hasDoubleJumpItem = true;
        Debug.Log("¡Habilidad de Doble Salto desbloqueada!");
    }
}
