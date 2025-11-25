using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeStateMachine : MonoBehaviour, IAspirable
{
    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    [SerializeField] private EnemySlimeMovementBehaviour enemySlimeMovementBehaviour;
    [SerializeField] private SlimeBonesReference slimeBonesReference;


    // --------------------------------------------RAYCAST SETTINGS--------------------------------------------\\
    [Header("VALORES GROUNDED")]
    private const float groundCheckDistance = 0.6f;
    [SerializeField] private LayerMask groundLayer;


    // --------------------------------------------STATES--------------------------------------------\\
    private enum States { ON_FLOOR, ON_AIR, BEING_ASPIRED };
    private States currentState;

    private void Start()
    {
        enemySlimeMovementBehaviour = GetComponent<EnemySlimeMovementBehaviour>();
        currentState = States.ON_FLOOR;
    }

    void Update()
    {
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
        enemySlimeMovementBehaviour.SetCanJump(true);
        ToOnAir();
    }

    private void OnAir()
    {
        enemySlimeMovementBehaviour.SetCanJump(false);
        ToOnFloor();
    }

    private void OnBeingAspired()
    {
        enemySlimeMovementBehaviour.SetCanJump(false);

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

        enemySlimeMovementBehaviour.SetGravity(false);

        foreach (GameObject obj in slimeBonesReference.GetSlimeBonesList())
        {
            obj.GetComponent<Rigidbody>().useGravity = false;
        }

    }

    public void StopBeingAspired()
    {
        enemySlimeMovementBehaviour.SetGravity(true);

        foreach (GameObject obj in slimeBonesReference.GetSlimeBonesList())
        {
            obj.GetComponent<Rigidbody>().useGravity = true;
        }

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
