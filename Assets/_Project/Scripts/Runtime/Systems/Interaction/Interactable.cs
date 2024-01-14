using System;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.Interaction
{
    public abstract class Interactable : MonoBehaviour, IOutline
    {
        public event Action<Interactable, bool> OnInteract;
        public event Action<Interactable> OnInteractableFounded;
        public event Action<Interactable> OnInteractableLeave;

        public event Action<Color> OnAddedOutline;
        public event Action OnRemovedOutline;

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

            OnAddedOutline?.Invoke(Color.white);
            OnInteractableFounded?.Invoke(this);
        }

        public virtual void Leave()
        {
            interactor = null;

            OnInteractableLeave?.Invoke(this);
            OnRemovedOutline?.Invoke();
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
}