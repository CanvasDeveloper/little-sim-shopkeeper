using UnityEngine;
using UnityEngine.UI;

public class UIEquippableSlot : UIShopSlotBase
{
    [SerializeField] private EquipType equipType;

    [SerializeField] private Image itemIcon;

    public override void SetItem(InventoryItemData newItem)
    {
        base.SetItem(newItem);

        if(_inventoryItemData == null || _inventoryItemData.ItemData == null || _inventoryItemData.stack <= 0)
        {
            itemIcon.enabled = false;
            itemIcon.sprite = null;
            return;
        }

        var cachedScriptable = _inventoryItemData.ItemData;

        itemIcon.enabled = true;
        itemIcon.sprite = cachedScriptable.itemIcon;
    }

    public EquipType GetEquipeLocation()
    {
        return equipType;
    }
}
