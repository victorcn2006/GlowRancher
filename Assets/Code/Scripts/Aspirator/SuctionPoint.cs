using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionPoint : MonoBehaviour
{
    private bool canSuck;
    [SerializeField] private Inventory inventory;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Chuclando");
        if (other.gameObject.GetComponent<IAspirable>() != null && canSuck)
        {
            Debug.Log("Chuclando algo chucable");
            
            Debug.Log(inventory.gameObject.name);
            if (inventory != null)
            {
                ItemPickUp objectPickUp = other.GetComponent<ItemPickUp>();
                // Añadimos el objeto al inventario
                if (inventory.AñadirAlInventario(objectPickUp.icono, objectPickUp.nombre))
                {
                    //other.GetComponentInParent<GameObject>().SetActive(false);
                    other.gameObject.SetActive(false); // desactivar el objeto en el mundo
                }
            }
        }
    }

    public void SetCanSuck(bool state)
    {
        canSuck = state;
    }
}
