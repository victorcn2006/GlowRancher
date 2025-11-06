using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    lightInteractionController lightContrll;

    LayerMask mask;
    public float distance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Raycast layer");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, mask)) 
        {
            if (hit.collider.tag == "InteractuableObject") 
            {
                if (Input.GetKeyDown(KeyCode.E)) { 
                    hit.collider.transform.GetComponent<lightInteractionController>().ActivateObject();
                }
            }
        }
    }
}
