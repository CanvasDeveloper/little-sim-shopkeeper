using CanvasDEV.Runtime.Systems.UI.Core;
using System;

public class UIShopSlotBase : ButtonBase
{
    public event Action<UIShopSlotBase> OnClicked;

    protected InventoryItemData _inventoryItemData;

    protected override void ButtonBehaviour()
    {
        OnClicked?.Invoke(this);
    }

    public virtual void SetItem(InventoryItemData newItem)
    {
        _inventoryItemData = newItem;
    }

    public InventoryItemData GetItem()
    {
        return _inventoryItemData;
    }
}
