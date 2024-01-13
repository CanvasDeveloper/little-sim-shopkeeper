using UnityEngine;

public class PlayerInteract : PlayerStateBase
{
    [SerializeField] private Interactor interactor;

    public override bool CanEnterState()
    {
        return interactor.GetCurrentInteractable() != null;
    }

    public override void OnEnter()
    {
        interactor.GetCurrentInteractable().Interact();
    }
}