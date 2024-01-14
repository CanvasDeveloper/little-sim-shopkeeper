using CanvasDEV.Runtime.Core.GameState;
using CanvasDEV.Runtime.Systems.Inventory;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.Inventory.UI
{
    public class UIInventoryManager : MonoBehaviour
    {
        private EquipmentInventory _equipmentInventory;

        [SerializeField] private GameObject canvas;

        [SerializeField] private UIShopSlotBase[] inventorySlotInstances = new UIShopSlotBase[0];

        private void Start()
        {
            GameStateHandler.StateChanged += GameStateHandler_StateChanged;

            PlayerInventory.Instance.OnAddedItem += (newItem) => UpdateSlots();
            PlayerInventory.Instance.OnRemovedItem += (newItem) => UpdateSlots();

            _equipmentInventory = PlayerInventory.Instance.GetComponentInChildren<EquipmentInventory>();
        }

        private void OnDestroy()
        {
            GameStateHandler.StateChanged -= GameStateHandler_StateChanged;

            if (PlayerInventory.Instance != null)
            {
                PlayerInventory.Instance.OnAddedItem -= (newItem) => UpdateSlots();
                PlayerInventory.Instance.OnRemovedItem -= (newItem) => UpdateSlots();
            }
        }

        private void GameStateHandler_StateChanged(GameState newState, object data)
        {
            if (newState != GameState.Inventory)
                return;

            UpdateSlots();
            Open();
        }

        private void UpdateSlots()
        {
            var inventory = PlayerInventory.Instance.GetCurrentItems();

            for (int i = 0; i < inventorySlotInstances.Length; i++)
            {
                var slot = inventorySlotInstances[i];
                var item = i < inventory.Count ? inventory[i] : null;

                slot.OnClicked -= PlayerEquip;
                slot.SetItem(item);
                slot.OnClicked += PlayerEquip;
            }
        }

        private void PlayerEquip(UIShopSlotBase slot)
        {
            _equipmentInventory.EquipItem((EquippableItemData)slot.GetItem().ItemData);
        }

        private void Open()
        {
            canvas.SetActive(true);
        }

        public void Close()
        {
            GameStateHandler.ChangeState(GameState.Gameplay);
            canvas.SetActive(false);
        }
    }
}