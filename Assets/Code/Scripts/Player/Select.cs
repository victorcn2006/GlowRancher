using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    private LightInteractionController _lightContrll;
    private InteractiveShop _interactiveShop;

    private LayerMask _mask;
    public float distance = 1.5f;

    [SerializeField] private InputAction _interact;


    // Start is called before the first frame update
    void Start()
    {
        _mask = LayerMask.GetMask("Raycast layer");
    }

    // Update is called once per frame
    private void InteractFilter()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, _mask)) 
        {
            if (hit.collider.tag == "InteractuableObject" ) 
            {
                hit.collider.transform.GetComponent<LightInteractionController>().ActivateObject();
            }

            if (hit.collider.tag == "InteractuableShop")
            {
                Debug.Log("Shop.Entra");
                hit.collider.transform.GetComponent<InteractiveShop>().ToggleShop(); // Canviem aquí
            }
            else
            {
                Debug.Log("Error");
            }
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            InteractFilter();
        }
    }
}
