using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeStateMachine : MonoBehaviour, IAspirable
{
    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    [SerializeField] private EnemySlimeMovementBehaviour _enemySlimeMovementBehaviour;
    [SerializeField] private SlimeBonesReference _slimeBonesReference;


    // --------------------------------------------RAYCAST SETTINGS--------------------------------------------\\
    [Header("VALORES GROUNDED")]
    private const float GROUND_CHECK_DISTANCE = 0.6f;
    [SerializeField] private LayerMask _groundLayer;


    // --------------------------------------------STATES--------------------------------------------\\
    private enum States { ON_FLOOR, ON_AIR, BEING_ASPIRED };
    private States _currentState;

    private void Start()
    {
        _enemySlimeMovementBehaviour = GetComponent<EnemySlimeMovementBehaviour>();
        _currentState = States.ON_FLOOR;
    }

    void Update()
    {
        switch (_currentState)
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
        _enemySlimeMovementBehaviour.SetCanJump(true);
        ToOnAir();
    }

    private void OnAir()
    {
        _enemySlimeMovementBehaviour.SetCanJump(false);
        ToOnFloor();
    }

    private void OnBeingAspired()
    {
        _enemySlimeMovementBehaviour.SetCanJump(false);
    }


    // STATES TRANSITIONS FUNCTIONS \\

    private void ToOnFloor()
    {
        if (Grounded())
        {
            _currentState = States.ON_FLOOR;
        }
    }

    private void ToOnAir()
    {
        if (!Grounded())
        {
            _currentState = States.ON_AIR;
        }
    }


    // ASPIRE TRANSITIONS \\

    public void BeingAspired()
    {
        _currentState = States.BEING_ASPIRED;

        _enemySlimeMovementBehaviour.SetGravity(false);

        foreach (GameObject obj in _slimeBonesReference.GetSlimeBonesList())
        {
            obj.GetComponent<Rigidbody>().useGravity = false;
        }

    }

    public void StopBeingAspired()
    {
        _enemySlimeMovementBehaviour.SetGravity(true);

        foreach (GameObject obj in _slimeBonesReference.GetSlimeBonesList())
        {
            obj.GetComponent<Rigidbody>().useGravity = true;
        }

        ToOnAir();
        ToOnFloor();
    }


    // OTHER FUNCTIONS \\
    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, GROUND_CHECK_DISTANCE, _groundLayer);
    }


    // RAYCAST LINE (DEBUG) \\
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * GROUND_CHECK_DISTANCE);

    }
}
