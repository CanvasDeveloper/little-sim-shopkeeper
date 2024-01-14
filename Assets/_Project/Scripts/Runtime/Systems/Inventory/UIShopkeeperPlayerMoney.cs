using TMPro;
using UnityEngine;

public class UIShopkeeperPlayerMoney : MonoBehaviour
{
    private PlayerInventory _playerInventory;

    [SerializeField] private TextMeshProUGUI playerMoneyGUI;

    private void Start()
    {
        GameStateHandler.StateChanged += GameStateHandler_StateChanged;

        _playerInventory = FindObjectOfType<PlayerInventory>();
        _playerInventory.OnUpdateMoney += PlayerInventory_OnUpdateMoney;
    }

    private void OnDestroy()
    {
        GameStateHandler.StateChanged -= GameStateHandler_StateChanged;
        _playerInventory.OnUpdateMoney -= PlayerInventory_OnUpdateMoney;
    }

    private void GameStateHandler_StateChanged(GameState newState, object data)
    {
        if (newState != GameState.Shop)
        {
            return;
        }

        playerMoneyGUI.text = _playerInventory.GetMoney().ToString();
    }

    private void PlayerInventory_OnUpdateMoney(int money)
    {
        playerMoneyGUI.text = money.ToString();
    }
}
