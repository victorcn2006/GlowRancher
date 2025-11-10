using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;

    [Header("Variables")]
    public float speed;
    //public float upForce = 250f;
    public float force = 10f;
    public float jumpHeight = 0.7f; //que tan alt salta
    private bool isGrounded;

    /*[Header("View")]
    public float rotationSense = 150f; //la sens
    private float cameraVerticaleAngle;
    Vector3 rotationInput = Vector3.zero; // iniciem la rotacio a 0
    [SerializeField] private Camera playerCamera;*/

    [Header("References")]
    public InputActionReference move;
    public InputActionReference Jump;

    [Header("Worldphysics")]
    public float gravityForce = -20f; //gravetat del mon

    [Header("Masks")]
    public LayerMask Mask;

    [Header("MoveInputs")]
    Vector3 moveInput = Vector3.zero; //fem que ho incialitci a 0

    private PlayerInput playerInput;
    private Vector2 input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move.action.Enable();
        Jump.action.Enable();
        rb =GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>(); 
    }

    

    void Update()
    {
        //CheckGround();//per saber si esta en el terra
        //look(); //es la funcio la cual s'utilitza per girar la camera

        input = playerInput.actions["Move"].ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredMovementDirection = ((input.x * transform.right) + (input.y * transform.forward)).normalized;
        // ha pasat de ser un valor global a un local de talmanera de ja no va per una cuadricula si no que funciona de talmanera de que agafa la direcio de lamirada i va perella

        rb.AddForce(desiredMovementDirection * force);

    }

    //asegurar-se de que toqui el terra i no sortir volant
    /*private void CheckGround() 
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.2f, Mask);
        Debug.DrawRay(transform.position, Vector3.down * 2f, isGrounded ? Color.green : Color.red);
    }*/
    /*
    public void OnJump(InputAction.CallbackContext ctx) {
        isGrounded = true;
        if(ctx.performed && isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravityForce);

            // Cambia la velocidad del Rigidbody en el eje Y para simular el salto
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);

            Debug.Log("¡Salta!");
        }
    }*/
    /*
    //The jump
    public void playerJump() {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpHeight * -2f * gravityForce), rb.velocity.z);
            Debug.Log("salta?!");
        }
    }
    */

    //vista del player
    /*private void look()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSense * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSense * Time.deltaTime;

        cameraVerticaleAngle = cameraVerticaleAngle + rotationInput.y;
        cameraVerticaleAngle = Mathf.Clamp(cameraVerticaleAngle, -70, 70);

        transform.Rotate(Vector3.up * rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticaleAngle, 0f, 0f);
    }*/

}
