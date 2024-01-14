using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.Inventory
{
    [Serializable]
    public class InventoryItemData
    {
        public ItemDataBase ItemData;
        public int stack;
    }

    public abstract class InventoryBase : MonoBehaviour
    {
        public event Action<InventoryItemData> OnAddedItem;
        public event Action<InventoryItemData> OnRemovedItem;

        [SerializeField] protected List<InventoryItemData> items;

        public virtual void AddToInventory(ItemDataBase item, int amount)
        {
            if (item.stackable)
            {
                var foundedItem = items.FirstOrDefault(slot => slot.ItemData == item);

                if (foundedItem != null)
                {
                    foundedItem.stack += amount;
                    TriggerAddedEvent(foundedItem);
                    return;
                }
            }

            var newInventorytem = new InventoryItemData()
            {
                ItemData = item,
                stack = amount
            };

            items.Add(newInventorytem);
            TriggerAddedEvent(newInventorytem);
        }

        public virtual void RemoveFromInventory(ItemDataBase item, int amount)
        {
            var foundedItem = items.FirstOrDefault(slot => slot.ItemData == item);

            foundedItem.stack -= amount;

            if (foundedItem.stack <= 0)
            {
                items.Remove(foundedItem);
            }

            TriggerRemoveEvent(foundedItem);
        }

        protected void TriggerAddedEvent(InventoryItemData newInventorytem)
        {
            OnAddedItem?.Invoke(newInventorytem);
        }

        protected void TriggerRemoveEvent(InventoryItemData foundedItem)
        {
            OnRemovedItem?.Invoke(foundedItem);
        }


        public bool HasItemAmount(ItemDataBase item, int amount)
        {
            var foundedItem = items.FirstOrDefault(slot => slot.ItemData == item);

            return foundedItem != null && foundedItem.stack >= amount;
        }

        public List<InventoryItemData> GetCurrentItems()
        {
            return items;
        }

        public abstract void Open();
    }
}