using System.Collections.Generic;
using UnityEngine;

public class UIShopkeeperManager : MonoBehaviour
{
    private PlayerInventory _playerInventory;
    private ShopkeeperInventory _shopkeeperInventory;

    [SerializeField] private GameObject canvas;

    [SerializeField] private UIShopSlot shopSlotPrefab;
    [SerializeField] private Transform slotParent;

    private List<UIShopSlotBase> _shopSlotInstances = new();


    private void Start()
    {
        GameStateHandler.StateChanged += GameStateHandler_StateChanged;

        _playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void OnDestroy()
    {
        GameStateHandler.StateChanged -= GameStateHandler_StateChanged;

        foreach (var slot in _shopSlotInstances)
        {
            slot.OnClicked -= PlayerBuy;
        }

        _shopSlotInstances.Clear();
    }

    private void GameStateHandler_StateChanged(GameState newState, object data)
    {
        if (newState != GameState.Shop)
        {
            return;
        }

        Populate((ShopkeeperInventory)data);
        Open();
    }

    private void Populate(ShopkeeperInventory data)
    {
        _shopkeeperInventory = data;

        foreach(var slot in _shopSlotInstances)
        {
            slot.OnClicked -= PlayerBuy;
            Destroy(slot.gameObject);
        }

        _shopSlotInstances.Clear();

        var inventory = data.GetCurrentItems();

        foreach(var item in inventory)
        {
            var slot = Instantiate(shopSlotPrefab, slotParent);
            slot.SetItem(item);
            slot.OnClicked += PlayerBuy;

            _shopSlotInstances.Add(slot);
        }
    }

    private void PlayerBuy(UIShopSlotBase slot)
    {
        if(!_playerInventory.HasMoneyTo(slot.GetItem().ItemData.cost))
        {
            return;
        }

        _playerInventory.RemoveMoney(slot.GetItem().ItemData.cost);

        _shopkeeperInventory.RemoveFromInventory(slot.GetItem().ItemData, 1);
        _playerInventory.AddToInventory(slot.GetItem().ItemData, 1);

        slot.SetItem(slot.GetItem());
    }

    public void Open()
    {
        canvas.SetActive(true);
    }

    public void Close()
    {
        GameStateHandler.ChangeState(GameState.Gameplay);
        canvas.SetActive(false);
    }
}
