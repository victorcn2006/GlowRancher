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

    // Referencias y Estado
    private PlayerMovement _playerMovement;
    private State _currentState;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        // Inicialización basada en el estado real inicial
        bool isGrounded = CheckIsGrounded();
        _currentState = isGrounded ? State.OnFloor : State.OnAir;
        _playerMovement.SetCanJump(isGrounded);
    }

    private void Update()
    {
        HandleStateLogic();
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

    private void PlayLandingSound()
    {
        if (!_landSound.IsNull)
        {
            RuntimeManager.PlayOneShot(_landSound, transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * GROUND_CHECK_DISTANCE);
    }
}
