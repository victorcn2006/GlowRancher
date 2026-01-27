using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelShopController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject incinerator;
    [SerializeField] private GameObject planter;
    [SerializeField] private GameObject SlimeCage;
    [SerializeField] private GameObject Silo;
    [SerializeField] private GameObject Hook;

    [Header("Buttons")]
    [SerializeField] private Button _itemOne;
    [SerializeField] private Button _itemPlanter;
    [SerializeField] private Button _itemSlimeCage;
    [SerializeField] private Button _itemSilo;
    [SerializeField] private Button _itemHook;

    // Start is called before the first frame update
    void Start()
    {
        incinerator.SetActive(false);
        planter.SetActive(false);
        SlimeCage.SetActive(false);
        Silo.SetActive(false);
        Hook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Activates
    public void ActivateItemOne()
    {
        incinerator.SetActive(true);
        planter.SetActive(false);
        SlimeCage.SetActive(false);
        Silo.SetActive(false);
        Hook.SetActive(false);
    }

    public void ActivatePlanter()
    {
        planter.SetActive(true);
        incinerator.SetActive(false);
        SlimeCage.SetActive(false);
        Silo.SetActive(false);
        Hook.SetActive(false);
    }

    public void ActivateSlimeCage()
    {
        SlimeCage.SetActive(true);
        incinerator.SetActive(false);
        planter.SetActive(false);
        Silo.SetActive(false);
        Hook.SetActive(false);
    }

    public void ActivateSilo()
    {
        Silo.SetActive(true);
        incinerator.SetActive(false);
        planter.SetActive(false);
        SlimeCage.SetActive(false);
        Hook.SetActive(false);
    }

    public void ActivateHook()
    {
        Hook.SetActive(true);
        incinerator.SetActive(false);
        planter.SetActive(false);
        SlimeCage.SetActive(false);
        Silo.SetActive(false);
    }
    #endregion

    public void BuyIncinerator()
    {

        if (WalletCurrency.instance.bank >= 5)
        {
            WalletCurrency.instance.bank -= 5;
            WalletCurrency.instance.SaveMoney();
            WalletCurrency.instance.Score_txt.text = WalletCurrency.instance.bank.ToString();

            Debug.Log("Incinerator purchased");
            incinerator.SetActive(true);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void BuyPlanter()
    {

        if (WalletCurrency.instance.bank >= 5)
        {
            WalletCurrency.instance.bank -= 5;
            WalletCurrency.instance.SaveMoney();
            WalletCurrency.instance.Score_txt.text = WalletCurrency.instance.bank.ToString();

            Debug.Log("Planter purchased");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void BuySlimeCage()
    {

        if (WalletCurrency.instance.bank >= 5)
        {
            WalletCurrency.instance.bank -= 5;
            WalletCurrency.instance.SaveMoney();
            WalletCurrency.instance.Score_txt.text = WalletCurrency.instance.bank.ToString();

            Debug.Log("Slime Cage purchased");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void BuySilo()
    {

        if (WalletCurrency.instance.bank >= 5)
        {
            WalletCurrency.instance.bank -= 5;
            WalletCurrency.instance.SaveMoney();
            WalletCurrency.instance.Score_txt.text = WalletCurrency.instance.bank.ToString();

            Debug.Log("Silo purchased");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void BuyHook()
    {

        if (WalletCurrency.instance.bank >= 5)
        {
            WalletCurrency.instance.bank -= 5;
            WalletCurrency.instance.SaveMoney();
            WalletCurrency.instance.Score_txt.text = WalletCurrency.instance.bank.ToString();

            Debug.Log("Hook purchased");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}
