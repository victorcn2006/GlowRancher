using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionPoint : MonoBehaviour
{
    private bool _canSuck;
    [SerializeField] private Inventory _inventory;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Chuclando");
        if (other.gameObject.GetComponent<IAspirable>() != null && _canSuck)
        {
            Debug.Log("Chuclando algo chucable");
            
            Debug.Log(_inventory.gameObject.name);
            if (_inventory != null)
            {
                ItemPickUp objectPickUp = other.GetComponent<ItemPickUp>();
                // Añadimos el objeto al inventario
                if (_inventory.AñadirAlInventario(objectPickUp.icono, objectPickUp.nombre))
                {
                    //other.GetComponentInParent<GameObject>().SetActive(false);
                    other.gameObject.SetActive(false); // desactivar el objeto en el mundo
                }
            }
        }
    }

    public void SetCanSuck(bool state)
    {
        _canSuck = state;
    }
}
