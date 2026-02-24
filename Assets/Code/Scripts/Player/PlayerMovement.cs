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

    private Rigidbody _rb;
    private Transform _mainCamTransform;
    private bool _canJump;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (Camera.main != null)
            _mainCamTransform = Camera.main.transform;
    }

    private void Update()
    {
        // Ya no necesitamos leer el input aquí manualmente, 
        // lo pediremos directamente en el FixedUpdate desde el Manager.
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        if (_mainCamTransform == null) return;

        // ACCESO AL MANAGER: Obtenemos el Vector2 directamente de la instancia global
        Vector2 input = InputManager.Instance.MoveInput;

        // Calculamos direcciones relativas a la cámara
        Vector3 forward = Vector3.ProjectOnPlane(_mainCamTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(_mainCamTransform.right, Vector3.up).normalized;

        Vector3 moveDirection = (forward * input.y + right * input.x);

        // Aplicamos velocidad
        Vector3 targetVelocity = moveDirection * _walkSpeed;
        targetVelocity.y = _rb.velocity.y;

        _rb.velocity = targetVelocity;
    }

    // Este método se activa por el evento del PlayerInput que ya tienes configurado
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && _canJump)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            PlayJumpSound();
        }
    }

    public void SetCanJump(bool value) => _canJump = value;

    private void PlayJumpSound()
    {
        if (!_jumpSound.IsNull)
            RuntimeManager.PlayOneShot(_jumpSound, transform.position);
    }
}
