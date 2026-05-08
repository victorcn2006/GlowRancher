using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
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

        Debug.Log("Interactao");
        RaycastHit hit;

        Transform rayOrigin = _cameraTransform != null ? _cameraTransform : transform;

        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * distance, Color.red, 2f);

        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, distance, _mask))
        {
            
            if (hit.collider.TryGetComponent<IInteractive>(out var InteractiveElement))
            {
                InteractiveElement.OnInteract();
                Debug.Log("El rayo impactó con objeto interactuable.");
            }
        }
        else
        {
            Debug.Log("El rayo no impactó con ningún objeto interactuable.");
        }
    }
}
