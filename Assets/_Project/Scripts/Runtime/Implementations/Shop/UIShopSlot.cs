using CanvasDEV.Runtime.Systems.Inventory;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopSlot : UIShopSlotBase
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI nameGUI;
    [SerializeField] private TextMeshProUGUI priceGUI;
    [SerializeField] private TextMeshProUGUI amountGUI;
    [SerializeField] private GameObject soldOutGUI;

    public override void SetItem(InventoryItemData newItem)
    {
        base.SetItem(newItem);

        var cachedScriptable = _inventoryItemData.ItemData;

        itemIcon.sprite = cachedScriptable.itemIcon;
        nameGUI.text = cachedScriptable.itemName;
        priceGUI.text = cachedScriptable.cost.ToString();
        amountGUI.text = "<" + _inventoryItemData.stack.ToString() + ">";

        button.interactable = _inventoryItemData.stack <= 0 == false;
        soldOutGUI.SetActive(_inventoryItemData.stack <= 0);
    }
}
