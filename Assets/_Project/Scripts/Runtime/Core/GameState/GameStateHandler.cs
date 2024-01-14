using System;

public enum GameState
{
    Title,
    Gameplay,
    Chatting,
    Inventory,
    Shop,
}

public static class GameStateHandler
{
    public static event Action<GameState> StateChanged;

    private static GameState state;

    public static void ChangeState(GameState newState)
    {
        if(state == newState) return;

        state = newState;
        StateChanged?.Invoke(state);
    }
}