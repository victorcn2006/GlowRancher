using UnityEngine;

public class HouseShopController : MonoBehaviour
{
    private GameObject _shopUI;

    private void Start()
    {
        _shopUI = References.Instance._houseShopPanel;
    }

    public void ActiveShop()
    {
        if (_shopUI != null) 
            _shopUI.SetActive(true);
        else
            Debug.LogWarning("HouseShopController: _shopUI is null!");
    }

    public void DesactiveShop()
    {
        if (_shopUI != null) 
            _shopUI.SetActive(false);
    }

    public void BuyRangeUpgrade()
    {
        if (ConstructionRangeManager.Instance != null)
        {
            ConstructionRangeManager.Instance.UpgradeRange();
        }
        else
        {
            Debug.LogError("HouseShopController: ConstructionRangeManager.Instance is null!");
        }
    }
}
