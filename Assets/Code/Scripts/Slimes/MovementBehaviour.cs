using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    // --------------------------------------------OBJETIVOS------------------------------------------------------ \\
    // HACER QUE SE MUEVA ALEATORIAMENTE.                                                    TRUE
    // PONER CONDICIONES DE MOVIMIENTO.                                                      TRUE (Solo se mueve si está en el estado de OnFloor)
    // LIMITAR EL MOVIMIENTO PARA MAYOR CONTROL Y EVITAR BUGS.                               FALSE [PRIORIDAD - BAJA] 
    // IMPLEMENTAR CON EL SISTEMA DE HAMBRE (SE MUEVE HACE LA COMIDA MAS CERCANA)            TRUE
    // HACER QUE ROTE ANTES DE MOVERSE SALTAR ALEATORIAMENTE.                                FALSE [PRIORIDAD - ALTA]

    // --------------------------------------------LINKED SCRIPTS------------------------------------------------- \\
    [Header("LINKED SCRIPTS")]
    [SerializeField] private HungerSystem hungerSystem;
    [SerializeField] private FoodDetector foodDetector;

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
    [SerializeField]private float ROTATION_SPEED = 5f;
    // --------------------------------------------PRIVATE VARIABLES--------------------------------------------\\
    private float jumpTimer;

    private Rigidbody rb;

    public bool canJump = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canJump)
        {
            jumpTimer -= Time.deltaTime;
            if(jumpTimer <= 0)
            {
                GoJump();
                jumpTimer = Random.Range(MIN_TIME, MAX_TIME);
            }
        }
        
    }

    // --------------------------------------------JUMP FUNCTIONS--------------------------------------------\\

    private void GoJump()
    {
        Vector3 jumpDirection = GetJumpDirection();

        StartCoroutine(RotateAndJumpToDirection(jumpDirection));

    }

    private void Jump(Vector3 jumpDirection)
    {
        rb.AddForce(jumpDirection * JUMP_FORCE, ForceMode.Impulse);
    }

    private IEnumerator RotateAndJumpToDirection(Vector3 jumpDirection)
    {
        Vector3 lookDirection = new Vector3(jumpDirection.x, 0, jumpDirection.z); //solamente te quedas con la direccion horizontal
        if (lookDirection == Vector3.zero) yield break; //en caso de que el Vector3 sea (0,0,0) sales de la corrutina

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);


        float elapsed = 0f; //elapsed significa transcurrido/pasado, se utiliza en este caso como "porcentaje" entre 0 y 1 para rotar el objeto con el Slerp

        while (elapsed < 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsed);
            elapsed += Time.deltaTime * ROTATION_SPEED;
            yield return null; //Se espera al siguiente frame antes de seguir rotando
        }

        Jump(jumpDirection);

    }

    // --------------------------------------------GETTERS-------------------------------------------\\

    private Vector3 GetJumpDirection()
    {
        if (hungerSystem.IsHungry() && foodDetector.FoodOnInRangeFoodList())
        {
            Vector3 foodDirection = foodDetector.GetClosestFood().gameObject.transform.position - transform.position;

            foodDirection = LimitateFoodDirectionToRange(foodDirection);

            foodDirection.y = VERTICAL_JUMP_FORCE;

            return foodDirection;
        }
        else
        {
            return new Vector3(Random.Range(MIN_DISTANCE, MAX_DISTANCE), VERTICAL_JUMP_FORCE, Random.Range(MIN_DISTANCE, MAX_DISTANCE));
        }
    }

    // --------------------------------------------OTHERS--------------------------------------------\\

    private Vector3 LimitateFoodDirectionToRange(Vector3 foodDirection)
    {
        foodDirection.x = Mathf.Clamp(foodDirection.x, MIN_DISTANCE, MAX_DISTANCE);
        foodDirection.z = Mathf.Clamp(foodDirection.z, MIN_DISTANCE, MAX_DISTANCE);

        //Debug.Log(foodDirection);

        return foodDirection;
    }

    public void SetCanJump(bool a)
    {
        canJump = a;
    }

}
