using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelShopController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _incinerator;
    [SerializeField] private GameObject _planter;
    [SerializeField] private GameObject _slimeCage;
    [SerializeField] private GameObject _silo;
    [SerializeField] private GameObject _hook;

    [Header("Buttons")]
    [SerializeField] private Button _itemOne;
    [SerializeField] private Button _itemPlanter;
    [SerializeField] private Button _itemSlimeCage;
    [SerializeField] private Button _itemSilo;
    [SerializeField] private Button _itemHook;

    // Start is called before the first frame update
    void Start()
    {
        _incinerator.SetActive(false);
        _planter.SetActive(false);
        _slimeCage.SetActive(false);
        _silo.SetActive(false);
        _hook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Activates
    public void ActivateItemOne()
    {
        _incinerator.SetActive(true);
        _planter.SetActive(false);
        _slimeCage.SetActive(false);
        _silo.SetActive(false);
        _hook.SetActive(false);
    }

    public void ActivatePlanter()
    {
        _planter.SetActive(true);
        _incinerator.SetActive(false);
        _slimeCage.SetActive(false);
        _silo.SetActive(false);
        _hook.SetActive(false);
    }

    public void ActivateSlimeCage()
    {
        _slimeCage.SetActive(true);
        _incinerator.SetActive(false);
        _planter.SetActive(false);
        _silo.SetActive(false);
        _hook.SetActive(false);
    }

    public void ActivateSilo()
    {
        _silo.SetActive(true);
        _incinerator.SetActive(false);
        _planter.SetActive(false);
        _slimeCage.SetActive(false);
        _hook.SetActive(false);
    }

    public void ActivateHook()
    {
        _hook.SetActive(true);
        _incinerator.SetActive(false);
        _planter.SetActive(false);
        _slimeCage.SetActive(false);
        _silo.SetActive(false);
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
            _incinerator.SetActive(true);
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
    // --- SECCIÓN DE ACTIVACIÓN VISUAL ---

    // Esta función sirve para activar un ítem específico desde fuera
    public void ActiveShop()
    {
        _shopPanel.SetActive(true);
    }

    public void DesactiveShop()
    {
        _shopPanel.SetActive(false);
    }
}
