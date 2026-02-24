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

    // Cache de componentes
    private Rigidbody _rb;
    private Transform _mainCamTransform;

    // Estado
    private bool _canJump = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (Camera.main != null)
            _mainCamTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        // Nos suscribimos al evento de salto del InputManager
        // Usamos una función lambda para llamar a nuestro método de salto
        InputManager.Instance.OnJumpPerformed.AddListener(HandleJump);
    }

    private void OnDisable()
    {
        // Importante desuscribirse para evitar errores de memoria
        if (InputManager.Instance != null)
            InputManager.Instance.OnJumpPerformed.RemoveListener(HandleJump);
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        // Obtenemos el input directamente del InputManager
        Vector2 input = InputManager.Instance.MoveInput;

        if (input.sqrMagnitude < 0.01f)
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            return;
        }

        // Calculamos direcciones basadas en la cámara
        Vector3 camForward = Vector3.Scale(_mainCamTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(_mainCamTransform.right, new Vector3(1, 0, 1)).normalized;

        Vector3 moveDirection = (camRight * input.x + camForward * input.y).normalized;

        // Aplicamos velocidad
        Vector3 targetVelocity = moveDirection * _walkSpeed;
        targetVelocity.y = _rb.velocity.y;

        _rb.velocity = targetVelocity;
    }

    private void HandleJump()
    {
        if (_canJump)
        {
            // FMOD Sound
            RuntimeManager.PlayOneShot(_jumpSound, transform.position);

            // Aplicamos fuerza de salto
            // Reseteamos la velocidad Y antes para que el salto siempre tenga la misma fuerza
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    public void SetCanJump(bool value) => _canJump = value;
}
