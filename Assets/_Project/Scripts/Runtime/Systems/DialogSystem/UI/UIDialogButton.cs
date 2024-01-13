using CanvasDEV.Runtime.Systems.UI.Core;
using System;
using TMPro;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.DialogSystem.UI
{
    public class UIDialogButton : ButtonBase
    {
        public event Action<int> OnButtonClicked;

        [SerializeField] protected TextMeshProUGUI text;
        private int _index;

        protected override void ButtonBehaviour()
        {
            OnButtonClicked?.Invoke(_index);
        }

        internal void SetOption(string optionTitle, int index)
        {
            text.text = optionTitle;
            _index = index;
        }
    }
}