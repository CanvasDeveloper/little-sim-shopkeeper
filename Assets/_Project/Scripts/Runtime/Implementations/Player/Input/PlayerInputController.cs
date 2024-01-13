using UnityEngine;
using UnityEngine.InputSystem;

namespace CanvasDEV.Runtime.Implementations.Player.Input
{
    public class PlayerInputController : MonoBehaviour, IInputController
    {
        [SerializeField] private InputActionAsset asset;
        [Space(10)]
        [SerializeField] private InputActionReference movementReference;
        [SerializeField] private InputActionReference interactReference;
        [SerializeField] private InputActionReference runToggleReference;

        public Vector2 Movement { get; private set; }
        public bool Interact { get; private set; }
        public bool RunToggle { get; private set; }

        private void Awake()
        {
            movementReference.action.performed += MovementPerformed;
            interactReference.action.started += InteractStarted;
            runToggleReference.action.started += RunToggleStarted;
        }

        private void OnDestroy()
        {
            movementReference.action.performed -= MovementPerformed;
            interactReference.action.started -= InteractStarted;
            runToggleReference.action.started -= RunToggleStarted;
        }

        public void EnableInput()
        {
            asset.Enable();
        }

        public void DisableInput()
        {
            asset.Disable();
        }

        private void MovementPerformed(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>().normalized;
        }

        private void InteractStarted(InputAction.CallbackContext context)
        {
            Interact = context.ReadValueAsButton();
        }

        private void RunToggleStarted(InputAction.CallbackContext context)
        {
            RunToggle = !RunToggle;
        }
    }
}