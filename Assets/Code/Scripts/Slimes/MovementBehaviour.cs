using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    // --------------------------------------------OBJETIVOS--------------------------------------------\\
    // HACER QUE SE MUEVA ALEATORIAMENTE. FALSE
    // PONER CONDICIONES DE MOVIMIENTO. FALSE
    // LIMITAR EL MOVIMIENTO PARA MAYOR CONTROL Y EVITAR BUGS. FALSE

    // --------------------------------------------MOVEMENT PARAMETERS--------------------------------------------\\

    [Header("VALORES PARA RANGO DE MOVIMIENTO")]
    [SerializeField] private float MIN_DISTANCE = -5f;
    [SerializeField] private float MAX_DISTANCE = 5f;

    [Header("VALORES PARA RANGO DE TIEMPO")]
    [SerializeField] private float MIN_TIME = 1f;
    [SerializeField] private float MAX_TIME = 5f;

    [Header("VALORES DEL SALTO")]
    [SerializeField] private float JUMP_FORCE = 5f;

    private float movementTimer;

    private bool CAN_JUMP;

    private void Update()
    {
        if (CAN_JUMP)
        {
            movementTimer -= Time.deltaTime;
            if(movementTimer <= 0)
            {
                Jump();
                movementTimer = Random.Range(MIN_TIME, MAX_TIME);
            }
        }
        
        
    }

    private void Jump()
    {
        //agregar impulso hacia la direccion asignada
    }

    void FixUpdate() //FixUpdate porque se moverá por fisicas.
    {
        
    }

    private Vector2 GetRandomCoordinate()
    {
        Vector2 nextRelativePosition = new Vector2(Random.Range(MIN_DISTANCE, MAX_DISTANCE), Random.Range(MIN_DISTANCE, MAX_DISTANCE));
        return nextRelativePosition;
    }

}
