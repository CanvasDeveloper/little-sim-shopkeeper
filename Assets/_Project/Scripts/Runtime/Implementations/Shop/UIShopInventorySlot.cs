using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopInventorySlot : UIShopSlotBase
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI sellPriceGUI;
    [SerializeField] private TextMeshProUGUI amountGUI;

    public override void SetItem(InventoryItemData newItem)
    {
        base.SetItem(newItem);

        var cachedScriptable = _inventoryItemData.ItemData;

        itemIcon.sprite = cachedScriptable.itemIcon;
        sellPriceGUI.text = cachedScriptable.cost.ToString();
        amountGUI.text = "<" + _inventoryItemData.stack.ToString() + ">";

        button.interactable = _inventoryItemData.stack <= 0 == false;
    }
}
