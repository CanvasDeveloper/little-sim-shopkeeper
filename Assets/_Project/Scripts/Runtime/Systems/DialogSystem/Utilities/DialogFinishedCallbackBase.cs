using CanvasDEV.Runtime.Systems.DialogSystem.Core;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.DialogSystem.Utilities
{
    public abstract class DialogFinishedCallbackBase : MonoBehaviour
    {
        [SerializeField] protected DialogTrigger trigger;

        protected virtual void Start()
        {
            trigger.OnDialogFinished += Trigger_OnDialogFinished;
        }

        protected virtual void OnDestroy()
        {
            trigger.OnDialogFinished -= Trigger_OnDialogFinished;
        }

        protected abstract void Trigger_OnDialogFinished(bool allOptionsRead);
    }
}