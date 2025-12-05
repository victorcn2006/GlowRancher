using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelShopController : MonoBehaviour
{
    [Header("Refs")]
    WalletCurrency wallet;

    public GameObject incinerator;

    [Header("Buttons")]
    [SerializeField]private Button ItemOne;

    // Start is called before the first frame update
    void Start()
    {
        incinerator.SetActive(false);

        wallet = WalletCurrency.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateItemOne() {
        incinerator.SetActive(true);
    }

    public void BuyIncinerator() {

        if (wallet.bank >= 5)
        {
            wallet.bank -= 5;
            wallet.SaveMoney();
            wallet.Score_txt.text = wallet.bank.ToString();

            Debug.Log("Incinerator purchased");
            incinerator.SetActive(true);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}
