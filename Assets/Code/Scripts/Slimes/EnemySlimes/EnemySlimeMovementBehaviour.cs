using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeMovementBehaviour : MonoBehaviour
{

    // --------------------------------------------LINKED SCRIPTS------------------------------------------------- \\
    [Header("LINKED SCRIPTS")]
    [SerializeField] private PlayerDetector _playerDetector;

    // --------------------------------------------MOVEMENT PARAMETERS-------------------------------------------- \\

    [Header("RANGO DE MOVIMIENTO PARA LOS SALTOS")]
    private const float MIN_DISTANCE = -5f;        //coordenadas negativas minimas
    private const float MAX_DISTANCE = 5f;         //coordenadas positivas máximas

    [Header("RANGO DE TIEMPO ENTRE SALTOS")]
    private const float MIN_TIME = 5f;
    private const float MAX_TIME = 10f;

    [Header("VALORES DEL SALTO")]
    private const float VERTICAL_JUMP_FORCE = 5f;
    private const float JUMP_FORCE = 3f;

    [Header("VALORES DE ROTACIÓN")]
    [SerializeField] private const float ROTATION_SPEED = 5f;
    // --------------------------------------------PRIVATE VARIABLES--------------------------------------------\\
    private float _jumpTimer;

    private Rigidbody _rb;

    private bool _canJump = true;
    private bool _hasRotated = false;

    private Vector3 _jumpDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _jumpTimer = Random.Range(MIN_TIME, MAX_TIME);
    }

    private void Update()
    {

        if (_canJump)
        {
            _jumpTimer -= Time.deltaTime;
            if (_hasRotated)
            {
                Jump();
            }
            if(_jumpTimer <= 0)
            {
                GoJump();
                _jumpTimer = Random.Range(MIN_TIME, MAX_TIME);
            }
        }
    }

    // --------------------------------------------JUMP FUNCTIONS--------------------------------------------\\

    private void GoJump()
    {
        _jumpDirection = GetJumpDirection();

        StartCoroutine(RotateAndJumpToDirection());
    }

    private IEnumerator RotateAndJumpToDirection()
    {
        Vector3 lookDirection = new Vector3(_jumpDirection.x, 0, _jumpDirection.z); 
        if (lookDirection == Vector3.zero) yield break; 

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);


        float elapsed = 0f; //elapsed significa transcurrido/pasado, se utiliza en este caso como "porcentaje" entre 0 y 1 para rotar el objeto con el Slerp

        while (elapsed < 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsed);
            elapsed += Time.deltaTime * ROTATION_SPEED;
            yield return null; //Se espera al siguiente frame antes de seguir rotando
        }

        _hasRotated = true;

    }
    private void Jump()
    {
        _rb.AddForce(_jumpDirection * JUMP_FORCE, ForceMode.Impulse);
        _hasRotated = false;
    }

    // --------------------------------------------GETTERS-------------------------------------------\\

    private Vector3 GetJumpDirection()
    {
        if (_playerDetector.CheckPlayerInRange())
        {
            Vector3 foodDirection = _playerDetector.GetPlayer().transform.position - transform.position;

            foodDirection = LimitateplayerDirectionToRange(foodDirection);

            foodDirection.y = VERTICAL_JUMP_FORCE;

            return foodDirection;
        }
        else
        {
            return new Vector3(Random.Range(MIN_DISTANCE, MAX_DISTANCE), VERTICAL_JUMP_FORCE, Random.Range(MIN_DISTANCE, MAX_DISTANCE));
        }
    }
    // --------------------------------------------SETTERS--------------------------------------------\\

    public void SetCanJump(bool a)
    {
        _canJump = a;
    }

    // --------------------------------------------OTHERS--------------------------------------------\\

    private Vector3 LimitateplayerDirectionToRange(Vector3 foodDirection)
    {
        foodDirection.x = Mathf.Clamp(foodDirection.x, MIN_DISTANCE, MAX_DISTANCE);
        foodDirection.z = Mathf.Clamp(foodDirection.z, MIN_DISTANCE, MAX_DISTANCE);

        //Debug.Log(foodDirection);

        return foodDirection;
    }

    public void SetGravity(bool gravity)
    {
        _rb.useGravity = gravity;
    }


}
