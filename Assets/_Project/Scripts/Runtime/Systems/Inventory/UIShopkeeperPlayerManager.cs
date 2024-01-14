using System.Collections.Generic;
using UnityEngine;
//TODO: Split this class in: PlayerInventory, ShopkeeperInventory, MoneyManagement

public class UIShopkeeperPlayerManager: MonoBehaviour
{
    private PlayerInventory _playerInventory;
    private ShopkeeperInventory _shopkeeperInventory;

    [SerializeField] private UIShopInventorySlot inventoryShopSlotPrefab;
    [SerializeField] private Transform inventorySlotParent;

    private List<UIShopSlotBase> _inventorySlotInstances = new();

    private void Start()
    {
        GameStateHandler.StateChanged += GameStateHandler_StateChanged;

        _playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void OnDestroy()
    {
        GameStateHandler.StateChanged -= GameStateHandler_StateChanged;

        foreach (var slot in _inventorySlotInstances)
        {
            slot.OnClicked -= PlayerSell;
        }

        _inventorySlotInstances.Clear();
    }

    private void GameStateHandler_StateChanged(GameState newState, object data)
    {
        if (newState != GameState.Shop)
        {
            return;
        }

        PopulatePlayerInventory(_playerInventory);
    }

    private void PopulatePlayerInventory(PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;

        foreach (var slot in _inventorySlotInstances)
        {
            slot.OnClicked -= PlayerSell;
            Destroy(slot.gameObject);
        }

        _inventorySlotInstances.Clear();

        var inventory = playerInventory.GetCurrentItems();

        foreach (var item in inventory)
        {
            var slot = Instantiate(inventoryShopSlotPrefab, inventorySlotParent);
            slot.SetItem(item);
            slot.OnClicked += PlayerSell;

            _inventorySlotInstances.Add(slot);
        }
    }

    private void PlayerSell(UIShopSlotBase slot)
    {
        _playerInventory.AddMoney(slot.GetItem().ItemData.cost);

        _playerInventory.RemoveFromInventory(slot.GetItem().ItemData, 1);
        _shopkeeperInventory.AddToInventory(slot.GetItem().ItemData, 1);

        slot.SetItem(slot.GetItem());
    }
}
