using CanvasDEV.Runtime.Systems.StateMachine;
using UnityEngine;

public class PlayerStateMachine : StateMachineBase
{
    [field:SerializeField] public PlayerComponents Components { get; private set; }

    public void ChangeToDefaultState()
    {
        ChangeState<PlayerMovement>();
    }

    public void ChangeToIdleState()
    {
        ChangeState<PlayerIdle>();
    }
}
