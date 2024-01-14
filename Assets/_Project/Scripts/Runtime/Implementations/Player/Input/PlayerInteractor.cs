using CanvasDEV.Runtime.Systems.Interaction;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private PlayerComponents components;
    [SerializeField] private Interactor interactor;

    private void Update()
    {
        if(components.IsPlayerBlocked)
        {
            return;
        }

        if(components.Input.InteractButton.IsPressed)
        {
            var interactable = interactor.GetCurrentInteractable();

            if(interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
