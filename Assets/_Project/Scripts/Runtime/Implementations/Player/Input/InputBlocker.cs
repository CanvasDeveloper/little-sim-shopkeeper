using UnityEngine;

public class InputBlocker : MonoBehaviour
{
	[SerializeField] private PlayerComponents components;

    private void Start()
    {
        GameStateHandler.StateChanged += GameStateHandler_StateChanged;
    }

    private void OnDestroy()
    {
		GameStateHandler.StateChanged -= GameStateHandler_StateChanged;
	}

    private void GameStateHandler_StateChanged(GameState newState)
    {
        components.IsPlayerBlocked = newState != GameState.Gameplay;
    }
}