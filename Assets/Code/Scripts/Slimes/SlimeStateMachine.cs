using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStateMachine : MonoBehaviour, IAspirable
{
    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    private MovementBehaviour movementBehaviour;


    // --------------------------------------------RAYCAST SETTINGS--------------------------------------------\\
    [Header("VALORES GROUNDED")]
    private const float groundCheckDistance = 0.7f;
    [SerializeField] private LayerMask groundLayer;


    // --------------------------------------------STATES--------------------------------------------\\
    private enum States { ON_FLOOR, ON_AIR, BEING_ASPIRED };
    private States currentState;

    private void Start()
    {
        movementBehaviour = GetComponent<MovementBehaviour>();
        currentState = States.ON_FLOOR;
    }

    void Update()
    {
        Debug.Log(currentState);
        switch (currentState)
        {

            case States.ON_FLOOR:
                OnFloor();
                break;
            case States.ON_AIR:
                OnAir();
                break;
            case States.BEING_ASPIRED:
                OnBeingAspired();
                break;
        }
    }


    // STATES FUNCTIONS \\

    private void OnFloor()
    {
        movementBehaviour.SetCanJump(true);
        ToOnAir();
    }

    private void OnAir()
    {
        movementBehaviour.SetCanJump(false);
        ToOnFloor();
    }

    private void OnBeingAspired()
    {
        movementBehaviour.SetCanJump(false);

    }


    // STATES TRANSITIONS FUNCTIONS \\

    private void ToOnFloor()
    {
        if (Grounded())
        {
            currentState = States.ON_FLOOR;
        }
    }

    private void ToOnAir()
    {
        if (!Grounded())
        {
            currentState = States.ON_AIR;
        }
    }


    // ASPIRE TRANSITIONS \\

    public void BeingAspired()
    {
        currentState = States.BEING_ASPIRED;

        movementBehaviour.SetGravity(false);

        GetComponent<Rigidbody>().useGravity = false;


    }

    public void StopBeingAspired()
    {
        movementBehaviour.SetGravity(true);

        GetComponent<Rigidbody>().useGravity = true;

        ToOnAir();
        ToOnFloor();
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
