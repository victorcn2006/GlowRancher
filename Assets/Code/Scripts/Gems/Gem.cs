using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IAspirable
{
    public GemData data;

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

    public int GetValue() {
        return data.value;
    }

}
