using CanvasDEV.Runtime.Core.Interfaces;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.Interaction
{
    public class Interactor : MonoBehaviour, IBlockable
    {
        private Interactable _currentInteractable;

        public bool Blocked { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Interactable interactable))
            {
                if (interactable.CanInteract == false)
                    return;

                _currentInteractable = interactable;
                _currentInteractable.Founded(this);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Interactable interactable))
            {

                if (_currentInteractable != null)
                {
                    _currentInteractable.Leave();
                }

                _currentInteractable = null;
            }
        }

        public Interactable GetCurrentInteractable()
        {
            return _currentInteractable;
        }
    }
}