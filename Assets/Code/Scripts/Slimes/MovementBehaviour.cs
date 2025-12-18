using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{

    // --------------------------------------------LINKED SCRIPTS------------------------------------------------- \\
    [Header("SLIME SCRIPTS")]
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
    private float VERTICAL_JUMP_FORCE = 5f;
    private float JUMP_FORCE = 3f;

    [Header("VALORES DE ROTACIÓN")]
    [SerializeField]private float ROTATION_SPEED = 5f;
    // --------------------------------------------PRIVATE VARIABLES--------------------------------------------\\
    private float jumpTimer;

    private Rigidbody rb;

    private bool canJump = true;
    private bool hasRotated = false;

    private Vector3 jumpDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        jumpTimer = Random.Range(MIN_TIME, MAX_TIME);
    }

    private void Update()
    {
        Debug.Log(canJump);
        Debug.Log(jumpTimer);

        if (canJump)
        {
            jumpTimer -= Time.deltaTime;
            if (hasRotated)
            {
                Jump();
            }
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
        jumpDirection = GetJumpDirection();

        StartCoroutine(RotateAndJumpToDirection());
    }

    private IEnumerator RotateAndJumpToDirection()
    {
        Vector3 lookDirection = new Vector3(jumpDirection.x, 0, jumpDirection.z); 
        if (lookDirection == Vector3.zero) yield break; 

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);


        float elapsed = 0f; //elapsed significa transcurrido/pasado, se utiliza en este caso como "porcentaje" entre 0 y 1 para rotar el objeto con el Slerp

        while (elapsed < 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsed);
            elapsed += Time.deltaTime * ROTATION_SPEED;
            yield return null; //Se espera al siguiente frame antes de seguir rotando
        }

        hasRotated = true;

    }
    private void Jump()
    {
        rb.AddForce(jumpDirection * JUMP_FORCE, ForceMode.Impulse);
        hasRotated = false;
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
    // --------------------------------------------SETTERS--------------------------------------------\\

    public void SetCanJump(bool a){
        canJump = a;
    }

    // --------------------------------------------OTHERS--------------------------------------------\\

    private Vector3 LimitateFoodDirectionToRange(Vector3 foodDirection)
    {
        foodDirection.x = Mathf.Clamp(foodDirection.x, MIN_DISTANCE, MAX_DISTANCE);
        foodDirection.z = Mathf.Clamp(foodDirection.z, MIN_DISTANCE, MAX_DISTANCE);

        return foodDirection;
    }

    public void SetGravity(bool gravity)
    {
        rb.useGravity = gravity;
    }


}
