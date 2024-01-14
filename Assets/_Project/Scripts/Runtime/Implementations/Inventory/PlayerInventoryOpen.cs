using UnityEngine;

public class PlayerInventoryOpen : MonoBehaviour
{
    [SerializeField] private PlayerComponents components;
    [SerializeField] private PlayerInventory inventory;

    private void Update()
    {
        if (components.IsPlayerBlocked)
        {
            return;
        }

        if (components.Input.InventoryButton.IsPressed)
        {
            inventory.Open();
        }
    }
}
