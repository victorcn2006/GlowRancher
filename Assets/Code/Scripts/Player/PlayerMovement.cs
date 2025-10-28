using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{


    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    private PlayerStateMachine playerStateMachine;


    // --------------------------------------------OTHERS--------------------------------------------\\
    private Rigidbody rb;

    private bool canJump;

    [Header("References")]
    public InputActionReference move;
    public InputActionReference Jump;

    private PlayerInput playerInput;

    void Start()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();

        move.action.Enable();
        Jump.action.Enable();
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void SetCanJump(bool a)
    {
        canJump = a;
    }

}
