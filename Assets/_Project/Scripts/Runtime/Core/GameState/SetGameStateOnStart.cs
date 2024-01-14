using UnityEngine;

namespace CanvasDEV.Runtime.Core.GameState
{
    public class SetGameStateOnStart : MonoBehaviour
    {
        public void Start()
        {
            GameStateHandler.ChangeState(GameState.Gameplay);
        }
    }
}