public class PlayerInventory : InventoryBase
{
    public override void Open()
    {
        GameStateHandler.ChangeState(GameState.Inventory);
    }
}