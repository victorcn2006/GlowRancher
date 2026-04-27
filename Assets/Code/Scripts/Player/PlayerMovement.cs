using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 9f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _doubleJumpForce = 13f;

    [Header("Audio")]
    [SerializeField] private EventReference _jumpSound;
    [SerializeField] private EventReference _footstepSound; // Nuevo: Evento de pasos
    [SerializeField] private float _stepInterval = 0.5f;   // Tiempo entre pasos al caminar
    [SerializeField] private float _runStepMultiplier = 0.7f; // Reducción de tiempo al correr

    public bool IsMoving => _rb != null && new Vector3(_rb.velocity.x, 0, _rb.velocity.z).magnitude > 0.1f;
    public bool IsRunning => _player != null && _player.CanRun && IsMoving;

    private Rigidbody _rb;
    private Transform _mainCamTransform;
    private Player _player;

    private bool _canJump = true;
    private bool _hasDoubleJumpItem = false;
    private bool _didDoubleJump = false;
    private float _stepTimer; // Temporizador interno

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();

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
        HandleFootsteps(); // Llamamos a la lógica de pasos
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

        float currentSpeed = IsRunning ? _runSpeed : _walkSpeed;

        Vector3 targetVelocity = moveDirection * currentSpeed;
        targetVelocity.y = _rb.velocity.y;
        _rb.velocity = targetVelocity;
    }

    private void HandleFootsteps()
    {
        // Solo sonar si el jugador se está moviendo y está en el suelo (canJump)
        if (IsMoving && _canJump)
        {
            _stepTimer -= Time.fixedDeltaTime;

            if (_stepTimer <= 0f)
            {
                PlayFootstep();

                // Ajustar ritmo según si corre o camina
                float currentInterval = IsRunning ? (_stepInterval * _runStepMultiplier) : _stepInterval;
                _stepTimer = currentInterval;
            }
        }
        else
        {
            _stepTimer = 0f; // Reiniciar para que el primer paso suene instantáneo al volver a moverte
        }
    }

    private void PlayFootstep()
    {
        RuntimeManager.PlayOneShot(_footstepSound, transform.position);
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
