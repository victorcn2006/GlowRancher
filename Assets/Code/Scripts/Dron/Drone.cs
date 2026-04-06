using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drone : MonoBehaviour
{

    private GameObject _objectToDelivery;
    private GameObject _boxToDelivery;
    private DeliveryBox _deliveryBox;

    private void Awake()
    {
        _boxToDelivery = transform.GetChild(0).gameObject;
        _deliveryBox = _boxToDelivery.GetComponent<DeliveryBox>();
    }

    public void FillBox(GameObject BuildingToDelivery) => _deliveryBox.FillBox(BuildingToDelivery);

    public void DropBox()
    {
        _boxToDelivery.layer = LayerMask.NameToLayer("Raycast layer");
        _boxToDelivery.transform.SetParent(null);
        _boxToDelivery.GetComponent<Rigidbody>().isKinematic = false;
        _boxToDelivery.GetComponent<Rigidbody>().useGravity = true;
    }

}
