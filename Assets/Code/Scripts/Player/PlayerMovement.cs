using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{


    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    private PlayerStateMachine _playerStateMachine;
    private PlayerCameraMovement _playerCameraMovement;


    // --------------------------------------------OTHERS--------------------------------------------\\
    private Rigidbody _rb;

    private bool _canJump;

    [Header("References")]
    [SerializeField] private InputActionReference _move;

    private const float WALK_SPEED = 5f;
    private const float JUMP_FORCE = 5f;

    private PlayerInput _playerInput;
    private Vector2 _movementInput;



    void Start()
    {
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        _playerCameraMovement = GetComponent<PlayerCameraMovement>();

        _move.action.Enable();
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        _movementInput = _move.action.ReadValue<Vector2>();

        Transform camTransform = _playerCameraMovement.transform;

        Vector3 camForward = camTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = camTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 moveDirection = (camRight * _movementInput.x + camForward * _movementInput.y).normalized;

        Vector3 newVelocity = moveDirection * WALK_SPEED;
        newVelocity.y = _rb.velocity.y;
        _rb.velocity = newVelocity;
        
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _canJump)
        {
            _rb.AddForce(new Vector3(0,1,0) * JUMP_FORCE, ForceMode.Impulse);
        }
    }
    
    public void SetCanJump(bool a)
    {
        _canJump = a;
    }
}
