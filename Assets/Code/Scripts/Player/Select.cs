using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    [Header("Configuración de Raycast")]
    [SerializeField] private float _distance = 1.5f;
    [SerializeField] private LayerMask _interactMask;

    [Header("Referencias")]
    [SerializeField] private Transform _raycastOrigin;

    private void Awake()
    {
        if(_raycastOrigin == null && Camera.main != null)
        {
            _raycastOrigin = Camera.main.transform;
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            PerformSelection();
        }
    }

    private void PerformSelection()
    {
        Vector3 origin = _raycastOrigin != null ? _raycastOrigin.position : transform.position;
        Vector3 direction = _raycastOrigin != null ? _raycastOrigin.forward : transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(origin, direction, out hit, _distance, _interactMask))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
                Debug.Log($"<color=cyan>[Interacción]</color> Ejecutada en: {hit.collider.name}");
            }
            else
            {
                Debug.LogWarning($"El objeto '{hit.collider.name}' está en la capa interactuable pero no tiene el componente IInteractable.");
            }
        }
    }
    #region Debug y Visualización
    private void OnDrawGizmos()
    {
        if (_raycastOrigin == null && Camera.main != null) _raycastOrigin = Camera.main.transform;

        if (_raycastOrigin != null)
        {
            Gizmos.color  = Color.yellow;
            Gizmos.DrawRay(_raycastOrigin.position, _raycastOrigin.forward * _distance);
        }
    }
    #endregion
}
