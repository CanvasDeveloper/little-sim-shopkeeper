using UnityEngine;
using UnityEngine.UI;

namespace CanvasDEV.Runtime.Systems.UI.Core
{
    public abstract class ButtonBase : MonoBehaviour
    {
        [SerializeField] protected Button button;

        private void Awake()
        {
            if(button == null)
                button = GetComponent<Button>();
            
            button.onClick.AddListener(ButtonBehaviour);
        }

        protected abstract void ButtonBehaviour();
    }
}