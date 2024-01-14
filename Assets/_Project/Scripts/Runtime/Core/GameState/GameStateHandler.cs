using System;

namespace CanvasDEV.Runtime.Core.GameState
{
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
        public static event Action<GameState, object> StateChanged;

        private static GameState state;

        public static void ChangeState(GameState newState, object data = null)
        {
            if (state == newState) return;

            state = newState;
            StateChanged?.Invoke(state, data);
        }

        public static GameState GetCurrentState()
        {
            return state;
        }
    }
}