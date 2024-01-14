using System;
using System.Linq;
using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private UISlotBase[] slots;

    private void Start()
    {
        GameStateHandler.StateChanged += GameStateHandler_StateChanged;
    }

    private void OnDestroy()
    {
        GameStateHandler.StateChanged -= GameStateHandler_StateChanged;
    }

    private void GameStateHandler_StateChanged(GameState newState, object data)
    {
        if(newState != GameState.Inventory)
        {
            return;
        }

        Populate((PlayerInventory)data);
        Open();
    }

    private void Populate(PlayerInventory data)
    {
        
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

    private void UpdateUI(InventoryItemData inventoryItemData)
    {
        UISlotBase emptySlot = slots.FirstOrDefault(slot => slot.GetItem() == null);

        if (emptySlot == null)
        {
            Debug.LogWarning("You dont have empty slots");
            return;
        }

        emptySlot.UpdateItem(inventoryItemData);
    }
}