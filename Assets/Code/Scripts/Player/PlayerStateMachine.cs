using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerStateMachine : MonoBehaviour
{
    private enum States { ON_FLOOR, ON_AIR };

    [SerializeField] private States _currentState;

    [Header("References")]
    private PlayerMovement _playerMovement;

    [Header("Configuración de Sonido")]
    [SerializeField] private EventReference _landingSound;

    [Header("Detection Settings")]
    [SerializeField] private float _groundcheckdistance = 1.1f;
    [SerializeField] private LayerMask _groundLayer;

    // Propiedad para centralizar la detección del suelo
    private bool _isGround => Physics.Raycast(transform.position, Vector3.down, _groundcheckdistance, _groundLayer);

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        // Inicialización basada en la física actual
        _currentState = _isGround ? States.ON_FLOOR : States.ON_AIR;

        // Sincronizar el estado inicial con el movimiento
        _playerMovement.SetCanJump(_currentState == States.ON_FLOOR);
    }

    private void Update()
    {
        //Maquina de estados
        HandleStateLogic();
        //Funcion Para comprobar la trensicion del aire al suelo
        CheckTransition();
    }

    private void HandleStateLogic()
    {
        switch (_currentState)
        {
            case States.ON_FLOOR:
                // Lógica continua en el suelo
                break;
            case States.ON_AIR:
                // Lógica continua en el aire
                break;
        }
    }

    private void CheckTransition()
    {
        bool grounded = _isGround;

        //comprueba de que si el estado es ON_FLOOR pero no esta en el suelo salta a la funcion TRSANSITIONTO y cambia de estado al ON_AIR
        if (_currentState == States.ON_FLOOR && !grounded)
        {
            TransitionTo(States.ON_AIR);
        }
        //comprueba de que si el estado es ON_AIR pero no esta en el aire salta a la funcion TRSANSITIONTO y cambia de estado al ON_FLOOR
        else if (_currentState == States.ON_AIR && grounded)
        {
            TransitionTo(States.ON_FLOOR);
        }
    }

    private void TransitionTo(States newState)
    {
        //Hace los cambios necesarios del nuevo estado

        _currentState = newState;

        switch (_currentState)
        {
            case States.ON_FLOOR:
                _playerMovement.SetCanJump(true);
                PlayLandingSound();
                break;

            case States.ON_AIR:
                _playerMovement.SetCanJump(false);
                break;
        }
    }

    private void PlayLandingSound()
    {
        //Sonido del salto 
        if (!_landingSound.IsNull)
        {
            RuntimeManager.PlayOneShot(_landingSound, transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        //Dibuja la linea del RayCast cambiando el color del RayCast dependiendo del estado en el qual esta 
        Gizmos.color = _isGround ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundcheckdistance);
    }
}
