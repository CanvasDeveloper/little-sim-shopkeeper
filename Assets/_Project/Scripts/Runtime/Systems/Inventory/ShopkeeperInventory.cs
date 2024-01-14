using CanvasDEV.Runtime.Core.GameState;
using System.Linq;

namespace CanvasDEV.Runtime.Systems.Inventory
{
    public class ShopkeeperInventory : InventoryBase
    {
        public override void RemoveFromInventory(ItemDataBase item, int amount)
        {
            var foundedItem = items.FirstOrDefault(slot => slot.ItemData == item && slot.stack > 0);

            foundedItem.stack -= amount;

            if (foundedItem.stack <= 0)
            {
                foundedItem.stack = 0;
            }

            TriggerRemoveEvent(foundedItem);
        }

        public void ForceRemoveEmptyItems()
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                InventoryItemData item = items[i];
                if (item.stack <= 0)
                {
                    items.Remove(item);
                }
            }
        }

        public override void Open()
        {
            GameStateHandler.ChangeState(GameState.Shop, this);
        }
    }
}