using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelShopController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private BuildingManager _buildingManager;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _incinerator;
    [SerializeField] private GameObject _planter;
    [SerializeField] private GameObject _slimeCage;
    [SerializeField] private GameObject _silo;
    [SerializeField] private GameObject _hook;
    [SerializeField] private GameObject _doubleJump;

    [Header("Buttons")]
    [SerializeField] private Button _itemOne;
    [SerializeField] private Button _itemPlanter;
    [SerializeField] private Button _itemSlimeCage;
    [SerializeField] private Button _itemSilo;
    [SerializeField] private Button _itemHook;
    [SerializeField] private Button _itemDoubleJump;

    #region Prices
    [Header("Prices")]
    [SerializeField] private int _incineratorPrice;
    [Tooltip("Price for the Incinerator")]
    [SerializeField] private int _planterPrice;
    [Tooltip("Price for the Planter")]
    [SerializeField] private int _slimeCagePrice;
    [Tooltip("Price for the Cage")]
    [SerializeField] private int _siloPrice;
    [Tooltip("Price for the Silo")]
    [SerializeField] private int _fusionerPrice;
    [Tooltip("Price for the Fusioner")]
    [SerializeField] private int _doubleJumpPrice;
    [Tooltip("Price for the Double Jump")]
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _incinerator.SetActive(false);
        _planter.SetActive(false);
        _slimeCage.SetActive(false);
        _silo.SetActive(false);
        _hook.SetActive(false);
        _doubleJump.SetActive(false);
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
    public void ActivateDoubleJump()
    {
        _hook.SetActive(false);
        _incinerator.SetActive(false);
        _planter.SetActive(false);
        _slimeCage.SetActive(false);
        _silo.SetActive(false);
        _doubleJump.SetActive(true);
    }
    #endregion

    public void BuyIncinerator()
    {

        if (WalletCurrency.instance.bank >= _incineratorPrice)
        {
            WalletCurrency.instance.bank -= _incineratorPrice;
            WalletCurrency.instance.SaveMoney();
            WalletCurrency.instance.Score_txt.text = WalletCurrency.instance.bank.ToString();

            Debug.Log("Incinerator purchased");
            _incinerator.SetActive(true);
            _buildingManager.IncineratorBuyed();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void BuyPlanter()
    {

        if (WalletCurrency.instance.bank >= _planterPrice)
        {
            WalletCurrency.instance.bank -= _planterPrice;
            WalletCurrency.instance.SaveMoney();
            WalletCurrency.instance.Score_txt.text = WalletCurrency.instance.bank.ToString();
            _buildingManager.PlanterBuyed();
            Debug.Log("Planter purchased");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void BuySlimeCage()
    {

        if (WalletCurrency.instance.bank >= _slimeCagePrice)
        {
            WalletCurrency.instance.bank -= _slimeCagePrice;
            WalletCurrency.instance.SaveMoney();
            WalletCurrency.instance.Score_txt.text = WalletCurrency.instance.bank.ToString();
            _buildingManager.CageBuyed();
            Debug.Log("Slime Cage purchased");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void BuySilo()
    {

        if (WalletCurrency.instance.bank >= _siloPrice)
        {
            WalletCurrency.instance.bank -= _siloPrice;
            WalletCurrency.instance.SaveMoney();
            WalletCurrency.instance.Score_txt.text = WalletCurrency.instance.bank.ToString();
            _buildingManager.SiloBuyed();
            Debug.Log("Silo purchased");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void BuyFusioner()
    {

        if (WalletCurrency.instance.bank >= _fusionerPrice)
        {
            WalletCurrency.instance.bank -= _fusionerPrice;
            WalletCurrency.instance.SaveMoney();
            WalletCurrency.instance.Score_txt.text = WalletCurrency.instance.bank.ToString();
            _buildingManager.FusionerBuyed();
            Debug.Log("Hook purchased");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void BuyDoubleJump()
    {

        if (WalletCurrency.instance.bank >= _fusionerPrice)
        {
            WalletCurrency.instance.bank -= _fusionerPrice;
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
