using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    // --------------------------------------------LINKED SCRIPTS--------------------------------------------\\
    private PlayerMovement _playerMovement;

    [Header("Configuración de Sonido")]
    public EventReference jumpSound;
    // --------------------------------------------RAYCAST SETTINGS--------------------------------------------\\
    [Header("VALORES GROUNDED")]
    private const float GROUND_CHECK_DISTANCE = 1.1f;
    [SerializeField] private LayerMask _groundLayer;

    // --------------------------------------------STATES--------------------------------------------\\
    private enum States { ON_FLOOR, ON_AIR };
    private States _currentState;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _currentState = States.ON_FLOOR;
    }

    void Update()
    {
        // DEBUGS ESTADO Y SI PUEDE SALTAR
        //Debug.Log(playerMovement.canJump);
        //Debug.Log(currentState);

        switch (_currentState)
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
            _currentState = States.ON_FLOOR;
            _playerMovement.SetCanJump(true);

            // EJECUTA EL SONIDO SOLO AL DETECTAR EL SUELO
            if (!jumpSound.IsNull)
            {
                RuntimeManager.PlayOneShot(jumpSound, transform.position);
            }
        }
        else
        {
            _playerMovement.SetCanJump(false);
        }
    }

    private void ToOnAir()
    {
        if (!Grounded())
        {
            _currentState = States.ON_AIR;
            _playerMovement.SetCanJump(false);
        }
        else _playerMovement.SetCanJump(true);
    }

    // OTHER FUNCTIONS \\
    private bool Grounded()
    {
        // El Raycast solo debe informar si hay suelo o no
        return Physics.Raycast(transform.position, Vector3.down, GROUND_CHECK_DISTANCE, _groundLayer);
    }

    // RAYCAST LINE (DEBUG) \\
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * GROUND_CHECK_DISTANCE);

    }
}
