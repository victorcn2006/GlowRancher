using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IAspirable
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void BeingAspired()
    {
        rb.useGravity = false;
    }

    public void StopBeingAspired()
    {
        rb.useGravity = true;
    }

    private void OnDestroy()
    {
        Aspirator.instance.RemoveAspirableObject(this.gameObject);
    }

}
