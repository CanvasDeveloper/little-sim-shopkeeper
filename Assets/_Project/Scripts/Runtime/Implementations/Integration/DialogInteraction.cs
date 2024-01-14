using CanvasDEV.Runtime.Core.GameState;
using CanvasDEV.Runtime.Core.Interfaces;
using CanvasDEV.Runtime.Systems.DialogSystem.Core;
using CanvasDEV.Runtime.Systems.Interaction;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteraction : Interactable, IBlocker
{
    [SerializeField] private DialogTrigger trigger;
    public List<IBlockable> Blockables { get; set; } = new();

    [SerializeField] private bool shouldBlockInteractor;
    [SerializeField] private bool callGameState;

    public override bool InteractBehaviour()
    {
        if(trigger.Started)
        {
            return false;
        }

        if(shouldBlockInteractor)
        {
            GameStateHandler.ChangeState(GameState.Chatting);
            trigger.RegisterBlocker(this);
        }

        trigger.StartDialog();

        return true;
    }

    public override void Founded(Interactor interactor)
    {
        base.Founded(interactor);

        Blockables.Add(interactor);
    }

    public override void Leave()
    {
        base.Leave();

        Blockables.Clear();
    }

    public void Block()
    {
        foreach(var block in Blockables)
        {
            block.Blocked = true;
        }
    }

    public void UnBlock()
    {
        if(callGameState)
        {
            GameStateHandler.ChangeState(GameState.Gameplay);
        }

        foreach (var block in Blockables)
        {
            block.Blocked = false;
        }
    }
}
