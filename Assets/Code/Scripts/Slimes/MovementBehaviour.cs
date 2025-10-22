using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    // --------------------------------------------OBJETIVOS--------------------------------------------\\
    // HACER QUE SE MUEVA ALEATORIAMENTE. TRUE
    // PONER CONDICIONES DE MOVIMIENTO. FALSE
    // LIMITAR EL MOVIMIENTO PARA MAYOR CONTROL Y EVITAR BUGS. FALSE

    // --------------------------------------------MOVEMENT PARAMETERS--------------------------------------------\\

    [Header("RANGO DE MOVIMIENTO PARA LOS SALTOS")]
    private const float MIN_DISTANCE = -5f;
    private const float MAX_DISTANCE = 5f;

    [Header("RANGO DE TIEMPO ENTRE SALTOS")]
    private const float MIN_TIME = 5f;
    private const float MAX_TIME = 10f;

    [Header("VALORES DEL SALTO")]
    private const float VERTICAL_JUMP_FORCE = 5f;
    private const float JUMP_FORCE = 3f;

    // --------------------------------------------PRIVATE VARIABLES--------------------------------------------\\
    private float jumpTimer;

    private Rigidbody rb;

    // --------------------------------------------PUBLIC VARIABLES--------------------------------------------\\
    public bool CanJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        jumpTimer = GetRandomJumpTime();
    }

    private void Update()
    {

        if (CanJump)
        {
            jumpTimer -= Time.deltaTime;
            if(jumpTimer <= 0)
            {
                Jump();
                jumpTimer = GetRandomJumpTime();
            }
        }
        
    }

    private void Jump()
    {
        rb.AddForce(GetRandomJumpDirection() * JUMP_FORCE, ForceMode.Impulse);
    }

    private float GetRandomJumpTime()
    {
        return Random.Range(MIN_TIME, MAX_TIME);
    }

    private Vector3 GetRandomJumpDirection()
    {
        return new Vector3(Random.Range(MIN_DISTANCE, MAX_DISTANCE), VERTICAL_JUMP_FORCE, Random.Range(MIN_DISTANCE, MAX_DISTANCE));
    }


}
