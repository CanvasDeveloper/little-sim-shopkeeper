using CanvasDEV.Runtime.Systems.StateMachine;
using UnityEngine;

public class NpcStateMachine : StateMachineBase
{
    [field: SerializeField] public NpcComponents Components { get; private set; }
}