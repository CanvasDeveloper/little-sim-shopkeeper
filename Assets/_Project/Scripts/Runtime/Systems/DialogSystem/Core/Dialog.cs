using System;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.DialogSystem.Core
{
    public enum Emotion
    {
        Normal,
        Smile,
        Happy,
        Shocked
    }

    [Serializable]
    public class Dialog
    {
        public string speecherName;

        public Emotion emotion;

        [TextArea]
        public string message;

        public bool waitForOptions;

        public DialogEvent onFinishMessage;
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