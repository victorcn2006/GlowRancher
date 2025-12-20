using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{


    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    private PlayerStateMachine playerStateMachine;
    private PlayerCameraMovement playerCameraMovement;


    // --------------------------------------------OTHERS--------------------------------------------\\
    private Rigidbody rb;

    private bool canJump;

    [Header("References")]
    [SerializeField] private InputActionReference move;

    private const float walkSpeed = 5f;
    private const float JUMP_FORCE = 5f;

    private PlayerInput playerInput;
    private Vector2 movementInput;



    private void Start()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
        playerCameraMovement = GetComponent<PlayerCameraMovement>();

        move.action.Enable();
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnJumpPressed += Jump;
        }
    }
    private void OnDestroy() {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnJumpPressed -= Jump;
        }
    }
    
    private void FixedUpdate() {
        Move();
    }
    
    public void Move()
    {
        movementInput = move.action.ReadValue<Vector2>();

        Transform camTransform = playerCameraMovement.transform;

        Vector3 camForward = camTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = camTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 moveDirection = (camRight * movementInput.x + camForward * movementInput.y).normalized;

        Vector3 newVelocity = moveDirection * walkSpeed;
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;
        
    }
    public void Jump()
    {
        if (canJump)
        {
            rb.AddForce(new Vector3(0,1,0) * JUMP_FORCE, ForceMode.Impulse);
        }
    }
    
    public void SetCanJump(bool value)
    {
        canJump = value;
    }
}
