using System.Linq;

public class ShopkeeperInventory : InventoryBase
{
    public override void RemoveFromInventory(ItemDataBase item, int amount)
    {
        var foundedItem = items.FirstOrDefault(slot => slot.ItemData == item);

        foundedItem.stack -= amount;

        if (foundedItem.stack <= 0)
        {
            foundedItem.stack = 0;
        }

        TriggerRemoveEvent(foundedItem);
    }

    public override void Open()
    {
        GameStateHandler.ChangeState(GameState.Shop);
    }
}
