using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public event Action<Interactable, bool> OnInteract;
    public event Action<Interactable> OnInteractableFounded;
    public event Action<Interactable> OnInteractableLeave;

    protected Interactor interactor;

    public bool CanInteract { get; protected set; } = true;

    public bool Interact()
    {
        bool sucess = InteractBehaviour();

        OnInteract?.Invoke(this, sucess);

        return sucess;
    }

    public abstract bool InteractBehaviour();

    public virtual void Founded(Interactor interactor)
    {
        this.interactor = interactor;

        OnInteractableFounded?.Invoke(this);
    }

    public virtual void Leave()
    {
        interactor = null;

        OnInteractableLeave?.Invoke(this);   
    }

    public virtual void EnableInteractable()
    {
        CanInteract = true;
    }

    public virtual void DisableInteractable()
    {
        CanInteract = false;
    }
}
