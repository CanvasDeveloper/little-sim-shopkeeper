using UnityEngine;

public class Interactor : MonoBehaviour
{
    private Interactable _currentInteractable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Interactable interactable))
        {
            _currentInteractable.Founded();
            _currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Interactable interactable))
        {
            if(_currentInteractable != null)
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
