using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IAspirable
{
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void BeingAspired()
    {
        _rb.useGravity = false;
    }

    public void StopBeingAspired()
    {
        _rb.useGravity = true;
    }

    private void OnDestroy()
    {
        Aspirator.instance.RemoveAspirableObject(this.gameObject);
    }

}
