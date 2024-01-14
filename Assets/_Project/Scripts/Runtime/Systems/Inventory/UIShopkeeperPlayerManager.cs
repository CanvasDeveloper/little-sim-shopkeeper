using System.Collections.Generic;
using UnityEngine;

public class UIShopkeeperPlayerManager: MonoBehaviour
{
    private ShopkeeperInventory _shopkeeperInventory;

    [SerializeField] private List<UIShopSlotBase> inventorySlotInstances = new();

    private void Start()
    {
        GameStateHandler.StateChanged += GameStateHandler_StateChanged;

        PlayerInventory.Instance.OnAddedItem += (newItem) => UpdateSlots();
        PlayerInventory.Instance.OnRemovedItem += (newItem) => UpdateSlots();
    }

    private void OnDestroy()
    {
        GameStateHandler.StateChanged -= GameStateHandler_StateChanged;

        inventorySlotInstances.ForEach(x => x.OnClicked -= PlayerSell);

        if (PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.OnAddedItem -= (newItem) => UpdateSlots();
            PlayerInventory.Instance.OnRemovedItem -= (newItem) => UpdateSlots();
        }
    }

    private void GameStateHandler_StateChanged(GameState newState, object data)
    {
        if (newState != GameState.Shop)
            return;

        _shopkeeperInventory = (ShopkeeperInventory)data;

        UpdateSlots();
    }

    private void PlayerSell(UIShopSlotBase slot)
    {
        if(slot.GetItem() == null)
            return;

        var cachedItem = slot.GetItem().ItemData;

        PlayerInventory.Instance.AddMoney(cachedItem.cost);
        PlayerInventory.Instance.RemoveFromInventory(cachedItem, 1);

        _shopkeeperInventory.AddToInventory(cachedItem, 1);

        UpdateSlots();
    }

    private void UpdateSlots()
    {
        var inventory = PlayerInventory.Instance.GetCurrentItems();

        for (int i = 0; i < inventorySlotInstances.Count; i++)
        {
            var slot = inventorySlotInstances[i];
            var item = (i < inventory.Count) ? inventory[i] : null;

            slot.OnClicked -= PlayerSell;
            slot.SetItem(item);
            slot.OnClicked += PlayerSell;
        }
    }
}
