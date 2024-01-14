using CanvasDEV.Runtime.Core;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.DialogSystem.Core
{
    [CreateAssetMenu(fileName = "New Dialogs", menuName = CanvasDevKeys.ScriptablePath + "Dialog/Dialog Data")]
    public class DialogData : ScriptableObject
    {
        public EmotionData emotionData;

        public Dialog[] firstTimeDialogs;

        [Header("Options")]
        public Dialogs[] options;

        public Dialog[] secondTimeDialogs;

        public float timeBetweenChars = 0.025f;

        private void OnValidate()
        {
            foreach (var dialog in options)
            {
                dialog.hasSelected = false;
            }
        }
    }
}