using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStateMachine : MonoBehaviour
{

    // --------------------------------------------RAYCAST SETTINGS--------------------------------------------\\
    [Header("Valores Grounded")]
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;

    // --------------------------------------------STATES--------------------------------------------\\
    private enum States {ON_FLOOR, ON_AIR };
    private States currentState;

    // --------------------------------------------PRIVATE VARIABLES--------------------------------------------\\
    [SerializeField] private MovementBehaviour movementBehaviour;

    void Update()
    {

        Debug.Log(movementBehaviour.CanJump);
        Debug.Log(currentState);

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
            movementBehaviour.CanJump = true;
        }
    }

    private void ToOnAir()
    {
        if (!Grounded())
        {
            currentState = States.ON_AIR;
            movementBehaviour.CanJump = false;
        }
    }

    // OTHER FUNCTIONS \\

    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);

    }
}
