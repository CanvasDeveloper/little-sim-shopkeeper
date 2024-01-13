using UnityEngine;
using UnityEngine.InputSystem;

namespace CanvasDEV.Runtime.Implementations.Player.Input
{
    public class PlayerInputController : MonoBehaviour, IInputController, PlayerControl.IGameplayActions
    {
        public Vector2 Movement { get; private set; }
        public bool Interact { get; private set; }
        public bool RunToggle { get; private set; }

        private PlayerControl _control;

        private void Awake()
        {
            _control = new();
        }

        private void Start()
        {
            EnableInput();
        }

        public void EnableInput()
        {
            _control.Gameplay.SetCallbacks(this);
            _control.Gameplay.Enable();
        }

        public void DisableInput()
        {
            _control.Disable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>().normalized;
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                Interact = context.ReadValue<bool>();
            }
        }

        public void OnRunToggle(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                RunToggle = !RunToggle;
            }
        }
    }
}