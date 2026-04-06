using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBox : MonoBehaviour, IInteractive
{
    private GameObject _building;


    public void FillBox(GameObject Building) => _building = Building;

    private void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    public void OnInteract()
    {
        //agregar codigo al interactuar(mostrar holograma y que se pueda construir el building)
        if (_building == null)
        {
            Debug.LogError("DeliveryBox: no hi ha cap building assignat!", this);
            return;
        }

        // Instancia el building a la posició/rotació de la caixa
        Instantiate(_building, transform.position, transform.rotation);

        // Destrueix la caixa un cop col·locat l'edifici
        Destroy(gameObject);
    }
}
