using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 9f; // Nueva velocidad de carrera
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _doubleJumpForce = 8f;

    [Header("Audio")]
    [SerializeField] private EventReference _jumpSound;

    // Cache de componentes
    private Rigidbody _rb;
    private Transform _mainCamTransform;
    private Player _player; // Referencia al script de lógica del jugador

    // Estado
    private bool _canJump = true;
    private bool _hasDoubleJumpItem = false;
    private bool _didDoubleJump = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GetComponent<Player>(); // Obtenemos la referencia al Player

        if (Camera.main != null)
            _mainCamTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
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

        // Aquí está la clave:
        float currentSpeed = (_player != null && _player.CanRun) ? _runSpeed : _walkSpeed;

        Vector3 targetVelocity = moveDirection * currentSpeed;
        targetVelocity.y = _rb.velocity.y;
        _rb.velocity = targetVelocity;
    }

    private void HandleJump()
    {
        if (_canJump)
        {
            PerformJump(_jumpForce);
            _didDoubleJump = false;
        }
        else if (_hasDoubleJumpItem && !_didDoubleJump)
        {
            _didDoubleJump = true;
            PerformJump(_doubleJumpForce);
        }
    }

    private void PerformJump(float force)
    {
        RuntimeManager.PlayOneShot(_jumpSound, transform.position);
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        _rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    public void SetCanJump(bool value)
    {
        _canJump = value;
        if (_canJump) _didDoubleJump = false;
    }

    public void EnableDoubleJumpItem()
    {
        _hasDoubleJumpItem = true;
        Debug.Log("¡Habilidad de Doble Salto desbloqueada!");
    }
}
