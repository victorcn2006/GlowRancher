using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    private PlayerMovement playerMovement;


    // --------------------------------------------RAYCAST SETTINGS--------------------------------------------\\
    [Header("VALORES GROUNDED")]
    private const float groundCheckDistance = 1.1f;
    [SerializeField] private LayerMask groundLayer;

    // --------------------------------------------STATES--------------------------------------------\\
    private enum States { ON_FLOOR, ON_AIR };
    private States currentState;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        currentState = States.ON_FLOOR;
    }

    void Update()
    {
        // DEBUGS ESTADO Y SI PUEDE SALTAR
        //Debug.Log(playerMovement.canJump);
        //Debug.Log(currentState);

        switch (currentState)
        {
            case States.ON_FLOOR:
                OnFloor();
                break;
            case States.ON_AIR:
                OnAir();
                break;
        }
    }

    // STATES FUNCTIONS \\
    private void OnFloor()
    {
        ToOnAir();
    }

    private void OnAir()
    {
        ToOnFloor();
    }

    // STATES TRANSITIONS FUNCTIONS \\
    private void ToOnFloor()
    {
        if (Grounded())
        {
            currentState = States.ON_FLOOR;
            playerMovement.SetCanJump(true);
        }
        else playerMovement.SetCanJump(false);
    }

    private void ToOnAir()
    {
        if (!Grounded())
        {
            currentState = States.ON_AIR;
            playerMovement.SetCanJump(false);
        }
        else playerMovement.SetCanJump(true);
    }

    // OTHER FUNCTIONS \\
    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    // RAYCAST LINE (DEBUG) \\
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);

    }
}
