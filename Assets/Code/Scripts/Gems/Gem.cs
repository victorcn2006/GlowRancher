using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IAspirable
{
    public GemData data;
    public GemsPool.gemTypes type; // Assigna això al Prefab!

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

    public int GetValue()
    {
        // Consultem al MarketManager el valor actual segons l'estat del joc
        if (MarketManager.Instance != null)
        {
            return MarketManager.Instance.GetCurrentPrice(type, data.value);
        }
        return data.value;
    }

}
