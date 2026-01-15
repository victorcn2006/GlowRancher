using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IAspirable
{
    public GemData data;

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

    public int GetValue() {
        return data.value;
    }

}
