using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    //[SerializeField]Gem gem;
    private void OnCollisionEnter(Collision collision)
    {
        //OnCollisionEnter
        if (collision.collider.tag == "Gem")
        {
            int gemValue = collision.gameObject.GetComponent<Gem>().GetValue();
            WalletCurrency.instance.Score(gemValue);
            Debug.Log($"Has obtingut {gemValue} monedes per la {collision.gameObject.GetComponent<Gem>().data.gemName}");
            collision.gameObject.SetActive(false);
        }
    }
}
