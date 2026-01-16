using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IAspirable
{
    public GemData data;
    public GemsPool.gemTypes type; // Assigna això al Prefab!

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
