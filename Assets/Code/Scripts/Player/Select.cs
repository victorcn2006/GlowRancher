using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    lightInteractionController lightContrll;
    InteractiveShop interactiveShop;

    LayerMask mask;
    public float distance = 1.5f;

    [SerializeField] private InputAction interact;


    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Raycast layer");
    }

    // Update is called once per frame
    private void InteractFilter()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask)) 
        {
            if (hit.collider.tag == "InteractuableObject" ) 
            {
                hit.collider.transform.GetComponent<lightInteractionController>().ActivateObject();
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
