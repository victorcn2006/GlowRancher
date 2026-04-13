using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header("")]
    public MarketManager MarketManager;

    private void OnCollisionEnter(Collision collision)
    {
        //OnCollisionEnter
        if (collision.collider.tag == "Gem")
        {
            MarketManager.UpdateMarketPrices();
            int gemValue = collision.gameObject.GetComponent<Gem>().GetValue();
            WalletCurrency.instance.Score(gemValue);
            Debug.Log($"Has obtingut {gemValue} monedes per la {collision.gameObject.GetComponent<Gem>().data.gemName}");
            collision.gameObject.SetActive(false);
        }
    }
}
