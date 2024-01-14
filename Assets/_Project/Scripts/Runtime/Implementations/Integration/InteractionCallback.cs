using CanvasDEV.Runtime.Systems.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class InteractionCallback : MonoBehaviour
{
    [SerializeField] private Interactable interactable;

    [SerializeField] private UnityEvent OnSucess;
    [SerializeField] private UnityEvent OnFail;

    private void Start()
    {
        interactable.OnInteract += Interactable_OnInteract;
    }

    private void OnDestroy()
    {
        interactable.OnInteract -= Interactable_OnInteract;
    }

    private void Interactable_OnInteract(Interactable interactable, bool result)
    {
        if(result)
        {
            OnSucess?.Invoke();
        }
        else
        {
            OnFail?.Invoke();
        }
    }
}
