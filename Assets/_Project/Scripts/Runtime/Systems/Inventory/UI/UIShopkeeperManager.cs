using CanvasDEV.Runtime.Core.GameState;
using CanvasDEV.Runtime.Systems.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.Inventory.UI
{
    public class UIShopkeeperManager : MonoBehaviour
    {
        private ShopkeeperInventory _shopkeeperInventory;

        [SerializeField] private GameObject canvas;

        [SerializeField] private UIShopSlot shopSlotPrefab;
        [SerializeField] private Transform slotParent;

        private List<UIShopSlotBase> _shopSlotInstances = new();


        private void Start()
        {
            GameStateHandler.StateChanged += GameStateHandler_StateChanged;
        }

        private void OnDestroy()
        {
            GameStateHandler.StateChanged -= GameStateHandler_StateChanged;
            ClearOnDestroyShop();
        }

        private void GameStateHandler_StateChanged(GameState newState, object data)
        {
            if (newState != GameState.Shop)
                return;

            _shopkeeperInventory = (ShopkeeperInventory)data;
            _shopkeeperInventory.OnAddedItem += (newItem) => UpdateSlots();
            _shopkeeperInventory.OnRemovedItem += (newItem) => UpdateSlots();

            Populate(_shopkeeperInventory);
            Open();
        }

        private void Populate(ShopkeeperInventory data)
        {
            foreach (var slot in _shopSlotInstances)
            {
                slot.OnClicked -= PlayerBuy;
                Destroy(slot.gameObject);
            }

            _shopSlotInstances.Clear();

            var inventory = data.GetCurrentItems();

            foreach (var item in inventory)
            {
                var slot = Instantiate(shopSlotPrefab, slotParent);
                slot.OnClicked -= PlayerBuy;
                slot.SetItem(item);
                slot.OnClicked += PlayerBuy;

                _shopSlotInstances.Add(slot);
            }
        }

        private void PlayerBuy(UIShopSlotBase slot)
        {
            var cachedItem = slot.GetItem().ItemData;

            if (!PlayerInventory.Instance.HasMoneyTo(cachedItem.cost))
                return;

            PlayerInventory.Instance.RemoveMoney(cachedItem.cost);
            PlayerInventory.Instance.AddToInventory(cachedItem, 1);

            _shopkeeperInventory.RemoveFromInventory(cachedItem, 1);

            UpdateSlots();
        }

        private void UpdateSlots()
        {
            _shopSlotInstances.ForEach(x => x.SetItem(x.GetItem()));
        }

        public void Open()
        {
            canvas.SetActive(true);

            UpdateSlots();
        }

        public void Close()
        {
            _shopkeeperInventory.ForceRemoveEmptyItems();

            _shopkeeperInventory.OnAddedItem -= (newItem) => UpdateSlots();
            _shopkeeperInventory.OnRemovedItem -= (newItem) => UpdateSlots();

            _shopkeeperInventory = null;

            GameStateHandler.ChangeState(GameState.Gameplay);
            canvas.SetActive(false);
        }

        private void ClearOnDestroyShop()
        {
            _shopSlotInstances.ForEach(x => x.OnClicked -= PlayerBuy);
            _shopSlotInstances.Clear();

            if (_shopkeeperInventory != null)
            {
                _shopkeeperInventory.OnAddedItem -= (newItem) => UpdateSlots();
                _shopkeeperInventory.OnRemovedItem -= (newItem) => UpdateSlots();
            }
        }
    }
}