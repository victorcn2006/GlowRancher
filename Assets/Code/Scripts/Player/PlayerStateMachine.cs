using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerStateMachine : MonoBehaviour
{
    private enum State { OnFloor, OnAir }

    [Header("Detection Settings")]
    [SerializeField] private LayerMask _groundLayer;
    private const float GROUND_CHECK_DISTANCE = 1.1f;

    [Header("Audio")]
    [SerializeField] private EventReference _landSound;
    [SerializeField] private EventReference _moveSound;

    [Header("Footsteps")]
    [SerializeField] private float _stepIntervalWalk = 0.5f;
    [SerializeField] private float _stepIntervalRun = 0.3f;

    // Referencias
    private PlayerMovement _playerMovement;
    private Player _player;
    private Rigidbody _rb;

    private State _currentState;

    // Footsteps
    private float _stepTimer;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        bool isGrounded = CheckIsGrounded();
        _currentState = isGrounded ? State.OnFloor : State.OnAir;
        _playerMovement.SetCanJump(isGrounded);
    }

    private void Update()
    {
        HandleStateLogic();
        HandleFootsteps();
    }

    private void HandleStateLogic()
    {
        bool isGrounded = CheckIsGrounded();

        switch (_currentState)
        {
            case State.OnFloor:
                if (!isGrounded) TransitionTo(State.OnAir);
                break;

            case State.OnAir:
                if (isGrounded) TransitionTo(State.OnFloor);
                break;
        }
    }

    private void TransitionTo(State newState)
    {
        _currentState = newState;

        switch (_currentState)
        {
            case State.OnFloor:
                _playerMovement.SetCanJump(true);
                PlayLandingSound();
                break;

            case State.OnAir:
                _playerMovement.SetCanJump(false);
                break;
        }
    }

    private bool CheckIsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, GROUND_CHECK_DISTANCE, _groundLayer);
    }

    private void HandleFootsteps()
    {
        if (_currentState != State.OnFloor)
        {
            _stepTimer = 0f;
            return;
        }

        Vector3 horizontalVelocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

        if (horizontalVelocity.magnitude < 0.1f)
        {
            _stepTimer = 0f;
            return;
        }

        float interval = (_player != null && _player.CanRun) ? _stepIntervalRun : _stepIntervalWalk;

        _stepTimer -= Time.deltaTime;

        if (_stepTimer <= 0f)
        {
            PlayMoveSound();
            _stepTimer = interval;
        }
    }

    private void PlayLandingSound()
    {
        if (!_landSound.IsNull)
        {
            RuntimeManager.PlayOneShot(_landSound, transform.position);
        }
    }

    private void PlayMoveSound()
    {
        if (!_moveSound.IsNull)
        {
            RuntimeManager.PlayOneShot(_moveSound, transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * GROUND_CHECK_DISTANCE);
    }
}
