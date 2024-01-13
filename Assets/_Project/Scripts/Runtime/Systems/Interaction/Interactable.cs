using System;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public event Action<Interactable, bool> OnInteract;
    public event Action<Interactable> OnInteractableFounded;
    public event Action<Interactable> OnInteractableLeave;

    public bool CanInteract { get; protected set; } = true;

    public bool Interact()
    {
        bool sucess = InteractBehaviour();

        OnInteract(this, sucess);

        return sucess;
    }

    public abstract bool InteractBehaviour();

    public void Founded()
    {
        OnInteractableFounded?.Invoke(this);
    }

    public void Leave()
    {
        OnInteractableLeave?.Invoke(this);   
    }

    public void EnableInteractable()
    {
        CanInteract = true;
    }

    public void DisableInteractable()
    {
        CanInteract = false;
    }
}
