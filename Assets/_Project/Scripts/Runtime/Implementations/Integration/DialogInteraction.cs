using CanvasDEV.Runtime.Core.Interfaces;
using CanvasDEV.Runtime.Systems.DialogSystem.Core;
using System.Collections.Generic;
using UnityEngine;

public class DialogInteraction : Interactable, IBlocker
{
    [SerializeField] private DialogTrigger trigger;
    [field:SerializeField] public List<IBlockable> Blockables { get; set; }

    [SerializeField] private bool shouldBlockInteractor;

    public override bool InteractBehaviour()
    {
        if(trigger.Started)
        {
            return false;
        }

        if(shouldBlockInteractor)
        {
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
        foreach (var block in Blockables)
        {
            block.Blocked = false;
        }
    }
}
