using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopInventorySlot : UIShopSlotBase
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI sellPriceGUI;
    [SerializeField] private GameObject sellIcon;
    [SerializeField] private TextMeshProUGUI amountGUI;

    public override void SetItem(InventoryItemData newItem)
    {
        base.SetItem(newItem);

        if(newItem == null || newItem.stack <= 0)
        {
            itemIcon.enabled = false;
            itemIcon.sprite = null;
            sellPriceGUI.text = "";
            amountGUI.text = "";
            sellIcon.SetActive(false);

            button.interactable = false;

            return;
        }

        var cachedScriptable = _inventoryItemData.ItemData;

        sellIcon.SetActive(true);
        itemIcon.enabled = true;
        itemIcon.sprite = cachedScriptable.itemIcon;
        sellPriceGUI.text = cachedScriptable.cost.ToString();
        amountGUI.text = "<" + _inventoryItemData.stack.ToString() + ">";

        button.interactable = _inventoryItemData.stack <= 0 == false;
    }
}
