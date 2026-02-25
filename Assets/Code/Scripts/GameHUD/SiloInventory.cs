using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiloInventory : MonoBehaviour
{
    private List<Item> _itemList;

    public SiloInventory() {
        _itemList = new List<Item>();
        AddItem(new Item { itemType = Item.ItemType.CARROT, amount=1});
        AddItem(new Item { itemType = Item.ItemType.CAULIFLOWER, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.POTATO, amount = 1 });
        Debug.Log(_itemList.Count);
    }

    public void AddItem(Item item) {
        _itemList.Add(item);
    }

    public List<Item> GetItemList() {
        return _itemList;
    }
}
