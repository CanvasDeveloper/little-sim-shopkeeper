using System;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.DialogSystem.Core
{
    public class DialogEvent : ScriptableObject
    {
        public event Action<DialogEvent> OnDialogEvent;

        public void Invoke()
        {
            OnDialogEvent?.Invoke(this);
        }
    }
}