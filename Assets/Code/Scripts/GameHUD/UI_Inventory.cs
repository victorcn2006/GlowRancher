using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private const float ITEM_SLOT_SIZE = 30f;
    private SiloInventory _inventory;
    private Transform _itemSlotContainer;
    private Transform _itemSlotTemplate;

    private void Awake()
    {
        _itemSlotContainer = transform.Find("Inventory Layout/itemSlotContainer");
        _itemSlotTemplate = transform.Find("itemSlotTemplate");
        if( _itemSlotTemplate == null)
        {
            Debug.Log("B");
        }
    }

    public void SetInventory(SiloInventory inventory) {
        _inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems() {
        foreach(Item item in _inventory.GetItemList())
        {
            int x = 0;
            int y = 0;
            RectTransform itemSlotRectTransform = Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * ITEM_SLOT_SIZE, y * ITEM_SLOT_SIZE);
            x++;
            if (x > 4) {
                x = 0;
                y++;
            }
        }
    }

}
