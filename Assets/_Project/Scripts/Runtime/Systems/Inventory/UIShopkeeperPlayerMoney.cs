using TMPro;
using UnityEngine;

public class UIShopkeeperPlayerMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerMoneyGUI;

    private void Start()
    {
        GameStateHandler.StateChanged += GameStateHandler_StateChanged;
        PlayerInventory.Instance.OnUpdateMoney += PlayerInventory_OnUpdateMoney;
    }

    private void OnDestroy()
    {
        GameStateHandler.StateChanged -= GameStateHandler_StateChanged;

        if(PlayerInventory.Instance != null)
        { 
            PlayerInventory.Instance.OnUpdateMoney -= PlayerInventory_OnUpdateMoney;
        }
       
    }

    private void GameStateHandler_StateChanged(GameState newState, object data)
    {
        playerMoneyGUI.text = PlayerInventory.Instance.GetMoney().ToString();
    }

    private void PlayerInventory_OnUpdateMoney(int money)
    {
        playerMoneyGUI.text = money.ToString();
    }
}
