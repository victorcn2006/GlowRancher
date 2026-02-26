using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    private LayerMask _mask;
    public float distance = 2.5f; // Aumentado un poco para mejor sensación

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPerformed.AddListener(HandleInteraction);
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractPerformed.RemoveListener(HandleInteraction);
        }
    }

    void Start()
    {
        // Asegúrate de que los objetos tengan la capa "Raycast layer"
        if (_mask == 0)
            _mask = LayerMask.GetMask("Raycast layer");
    }

    private void HandleInteraction()
    {
        RaycastHit hit;
        // Debug visual del rayo
        Debug.DrawRay(transform.position, transform.forward * distance, Color.red, 0.5f);

        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, _mask))
        {
            // Caso 1: Objeto interactuable simple
            if (hit.collider.CompareTag("InteractuableObject"))
            {
                if (hit.collider.TryGetComponent<LightInteractionController>(out var controller))
                {
                    controller.ActivateObject();
                }
            }
            
            // Caso 2: La Tienda
            if (hit.collider.CompareTag("InteractuableShop"))
            {
                Debug.Log("Interacción con tienda detectada.");
                if (hit.collider.TryGetComponent<InteractiveShop>(out var shop))
                {
                    shop.OpenShop();
                }
            }
        }
        else
        {
            Debug.Log("El rayo no impactó con ningún objeto interactuable.");
        }
    }
}
