using System;
using UnityEngine;
using UnityEngine.Events;

namespace CanvasDEV.Runtime.Systems.DialogSystem.Core
{
    [Serializable]
    public class Dialog
    {
        public string speecherName;


        [TextArea]
        public string message;

        public string animation = "Trainer_Talking_1";

        public bool waitForOptions;

        public UnityAction onFinishMessage;
    }

    [Serializable]
    public class Dialogs
    {
        public string optionTitle;

        public Dialog[] dialogs;

        public bool hasSelected;
        public bool requiredOption;
    }
}