using UnityEngine;

public class UIShopkeeperPlayerManager: MonoBehaviour
{
    private PlayerInventory _playerInventory;
    private ShopkeeperInventory _shopkeeperInventory;

    [SerializeField] private UIShopSlotBase[] inventorySlotInstances = new UIShopSlotBase[0];

    private void Start()
    {
        GameStateHandler.StateChanged += GameStateHandler_StateChanged;

        _playerInventory = FindObjectOfType<PlayerInventory>();

        _playerInventory.OnAddedItem += (newItem) => UpdateSlots();
        _playerInventory.OnRemovedItem += (newItem) => UpdateSlots();
    }

    private void OnDestroy()
    {
        GameStateHandler.StateChanged -= GameStateHandler_StateChanged;

        foreach (var slot in inventorySlotInstances)
        {
            slot.OnClicked -= PlayerSell;
        }

        _playerInventory.OnAddedItem -= (newItem) => UpdateSlots();
        _playerInventory.OnRemovedItem -= (newItem) => UpdateSlots();
    }

    private void GameStateHandler_StateChanged(GameState newState, object data)
    {
        if (newState != GameState.Shop)
        {
            return;
        }

        _shopkeeperInventory = (ShopkeeperInventory)data;

        PopulatePlayerInventory(_playerInventory);
    }

    private void PopulatePlayerInventory(PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;

        var inventory = playerInventory.GetCurrentItems();

        UpdateSlots();
    }

    private void PlayerSell(UIShopSlotBase slot)
    {
        if(slot.GetItem() == null)
        {
            return;
        }

        _playerInventory.AddMoney(slot.GetItem().ItemData.cost);

        _playerInventory.RemoveFromInventory(slot.GetItem().ItemData, 1);
        _shopkeeperInventory.AddToInventory(slot.GetItem().ItemData, 1);

        UpdateSlots();
    }

    private void UpdateSlots()
    {
        var inventory = _playerInventory.GetCurrentItems();

        for (int i = 0; i < inventorySlotInstances.Length; i++)
        {
            var slot = inventorySlotInstances[i];
            var item = (i < inventory.Count) ? inventory[i] : null;

            slot.SetItem(item);
            slot.OnClicked += PlayerSell;
        }
    }
}
