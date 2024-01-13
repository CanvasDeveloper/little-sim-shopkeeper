using CanvasDEV.Runtime.Systems.StateMachine;
using UnityEngine;

public class PlayerStateMachine : StateMachineBase
{
    [field:SerializeField] public PlayerComponents Components { get; private set; }
}
