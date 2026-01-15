using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelShopController : MonoBehaviour
{
    [Header("Refs")]
    private WalletCurrency _wallet;

    public GameObject incinerator;

    [Header("Buttons")]
    [SerializeField] private Button _itemOne;

    // Start is called before the first frame update
    void Start()
    {
        incinerator.SetActive(false);

        _wallet = WalletCurrency.instance;
    }

    public void ActivateItemOne() {
        incinerator.SetActive(true);
    }

    public void BuyIncinerator() {

        if (_wallet.bank >= 5)
        {
            _wallet.bank -= 5;
            _wallet.SaveMoney();
            _wallet.Score_txt.text = _wallet.bank.ToString();

            Debug.Log("Incinerator purchased");
            incinerator.SetActive(true);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}
