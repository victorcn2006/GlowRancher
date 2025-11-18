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

    private void OnDestroy()
    {
        // Si la gemma és destruïda, afegim el valor al banc de monedes
        if (data != null)
        {
            // Afegeix els diners al compte
            WalletCurrency.instance.Score(data.value);
            Debug.Log($"Has obtingut {data.value} monedes per la {data.gemName}");
        }
    }

}
