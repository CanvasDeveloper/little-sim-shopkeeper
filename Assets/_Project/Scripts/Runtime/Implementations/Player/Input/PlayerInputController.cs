using CanvasDEV.Runtime.Systems.Input.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CanvasDEV.Runtime.Implementations.Player.Input
{
    public class InputButton
    {
        public bool IsPressed;
    }

    public class PlayerInputController : MonoBehaviour, IInputController, PlayerControl.IGameplayActions
    {
        public Vector2 Movement { get; private set; }
        public InputButton InteractButton { get; private set; } = new();
        public InputButton InventoryButton { get; private set; } = new();
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
                StartCoroutine(ButtonBehaviour(InteractButton));
            }
        }

        public void OnRunToggle(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                RunToggle = !RunToggle;
            }
        }

        public void OnOpenInventory(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                StartCoroutine(ButtonBehaviour(InventoryButton));
            }
        }

        public IEnumerator ButtonBehaviour(InputButton button)
        {
            button.IsPressed = true;

            yield return new WaitForEndOfFrame();

            button.IsPressed = false;

        }
    }
}