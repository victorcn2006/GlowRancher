using UnityEngine;

public class SlimeStateMachine : MonoBehaviour, IAspirable
{
    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    private MovementBehaviour _movementBehaviour;
    [SerializeField] private SlimeBonesReference _slimeBonesReference;


    // --------------------------------------------RAYCAST SETTINGS--------------------------------------------\\
    [Header("VALORES GROUNDED")]
    private const float GROUND_CHECK_DISTANCE = 0.4f;
    [SerializeField] private LayerMask _groundLayer;


    // --------------------------------------------STATES--------------------------------------------\\
    private enum States { ON_FLOOR, ON_AIR, BEING_ASPIRED };
    private States _currentState;

    private void Start()
    {
        _movementBehaviour = GetComponent<MovementBehaviour>();
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
        _movementBehaviour.SetCanJump(true);
        ToOnAir();
    }

    private void OnAir()
    {
        _movementBehaviour.SetCanJump(false);
        ToOnFloor();
    }

    private void OnBeingAspired()
    {
        _movementBehaviour.SetCanJump(false);
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

        _movementBehaviour.SetGravity(false);

        foreach (GameObject obj in _slimeBonesReference.GetSlimeBonesList())
        {
            obj.GetComponent<Rigidbody>().useGravity = false;
        }

    }

    public void StopBeingAspired()
    {
        _movementBehaviour.SetGravity(true);

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
