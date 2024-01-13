using UnityEngine;

public class NpcMovement : NpcStateBase
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float runningSpeed = 1f;

    public override void OnUpdate()
    {
        //_movementInput = machine.Components.Input.Movement;
        //_isRunning = machine.Components.Input.RunToggle;

        var cachedAnimationComponent = machine.Components.AnimationValues;

        //bool isMoving = _movementInput.magnitude != 0f;

        //if (isMoving)
        //{
        //    cachedAnimationComponent.XDirection = _movementInput.x;
        //    cachedAnimationComponent.YDirection = _movementInput.y;
        //}

        //cachedAnimationComponent.OnMovement = isMoving;
        //cachedAnimationComponent.IsRunning = _isRunning;
    }

    public override void OnFixedUpdate()
    {
        //var cachedRigidbody = machine.Components.Rigidbody2D;
        //var desiredSpeed = _isRunning ? runningSpeed : moveSpeed;

        //Vector2 newPosition = cachedRigidbody.position + desiredSpeed * Time.fixedDeltaTime * _movementDirection;
        //cachedRigidbody.MovePosition(newPosition);
    }
}