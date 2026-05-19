using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionPoint : MonoBehaviour
{
    private bool _canSuck;
    [SerializeField] private Inventory _inventory;
    private void OnTriggerEnter(Collider other)
    {
        if (_canSuck && other.TryGetComponent<IAspirable>(out var aspirable))
        {
            if (_inventory != null)
            {
                if (other.TryGetComponent<ItemPickUp>(out var objectPickUp))
                {
                    // Añadimos el objeto al inventario
                    if (_inventory.AñadirAlInventario(objectPickUp.icono, objectPickUp.nombre))
                    {
                        other.gameObject.SetActive(false); // desactivar el objeto en el mundo
                    }
                }
                else
                {
                    Debug.LogWarning($"SuctionPoint: Object {other.name} has IAspirable but is missing ItemPickUp!");
                }
            }
        }
    }

    public void SetCanSuck(bool state)
    {
        _canSuck = state;
    }
}
