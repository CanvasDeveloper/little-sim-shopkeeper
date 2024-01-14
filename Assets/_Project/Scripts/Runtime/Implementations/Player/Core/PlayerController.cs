using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerComponents Components;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float runningSpeed = 1f;

    private Vector2 _movementInput;
    private bool _isRunning;

    public void Update()
    {
        var cachedAnimationComponent = Components.AnimationValues;

        if (Components.IsPlayerBlocked)
        {
            cachedAnimationComponent.OnMovement = false;
            cachedAnimationComponent.IsRunning = false;

            _isRunning = false;
            _movementInput = Vector2.zero;

            return;
        }

        _movementInput = Components.Input.Movement;
        _isRunning = Components.Input.RunToggle;


        bool isMoving = _movementInput.magnitude != 0f;

        if (isMoving)
        {
            cachedAnimationComponent.XDirection = _movementInput.x;
            cachedAnimationComponent.YDirection = _movementInput.y;
        }

        cachedAnimationComponent.OnMovement = isMoving;
        cachedAnimationComponent.IsRunning = _isRunning;
    }

    public void FixedUpdate()
    {
        if (Components.IsPlayerBlocked)
        {
            return;
        }

        var cachedRigidbody = Components.Rigidbody2D;
        var desiredSpeed = _isRunning ? runningSpeed : moveSpeed;

        Vector2 newPosition = cachedRigidbody.position + desiredSpeed * Time.fixedDeltaTime * _movementInput;
        cachedRigidbody.MovePosition(newPosition);
    }
}