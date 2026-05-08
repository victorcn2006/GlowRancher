using UnityEngine;

public class HouseShopController : MonoBehaviour
{
    [SerializeField] private GameObject _shopUI;

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
}
