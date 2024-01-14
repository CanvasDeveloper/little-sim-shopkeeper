using CanvasDEV.Runtime.Core.GameState;
using CanvasDEV.Runtime.Systems.Inventory;
using System;
using System.Linq;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.Inventory.UI
{
    public class UIEquipmentManager : MonoBehaviour
    {
        private EquipmentInventory _playerEquipmentInventory;

        [SerializeField] private UIEquippableSlot[] equipmentSlotInstances = new UIEquippableSlot[0];

        private void Start()
        {
            GameStateHandler.StateChanged += GameStateHandler_StateChanged;

            _playerEquipmentInventory = PlayerInventory.Instance.GetComponentInChildren<EquipmentInventory>();

            _playerEquipmentInventory.OnUpdateEquipment += UpdateEquipmentSlots;
        }

        private void OnDestroy()
        {
            GameStateHandler.StateChanged -= GameStateHandler_StateChanged;

            _playerEquipmentInventory.OnUpdateEquipment -= UpdateEquipmentSlots;
        }

        private void GameStateHandler_StateChanged(GameState newState, object data)
        {
            if (newState != GameState.Inventory)
                return;

            UpdateEquipmentSlots();
        }

        private void UpdateEquipmentSlots()
        {
            var items = _playerEquipmentInventory.GetItems();

            foreach (var item in items)
            {
                var slot = equipmentSlotInstances.FirstOrDefault(x => x.GetEquipeLocation() == item.Key);

                var tempItem = new InventoryItemData
                {
                    ItemData = item.Value,
                    stack = 1
                };

                slot.OnClicked -= PlayerRemove;
                slot.SetItem(tempItem);
                slot.OnClicked += PlayerRemove;
            }
        }

        private void PlayerRemove(UIShopSlotBase slot)
        {
            if (slot.GetItem() == null)
            {
                return;
            }

            _playerEquipmentInventory.RemoveEquip((EquippableItemData)slot.GetItem().ItemData);
            slot.SetItem(null);
        }
    }
}