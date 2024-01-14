using DG.Tweening;
using UnityEngine;

public class DOMovement : MonoBehaviour
{
    [SerializeField] private AnimationValues animationValues;

    [SerializeField] private Transform target;
    [SerializeField] private float moveTime;

    public void Move()
    {
        var direction = (target.position- transform.position).normalized;

        animationValues.XDirection = direction.x;
        animationValues.XDirection = direction.y;

        animationValues.OnMovement = true;

        transform.DOMove(target.position, moveTime).OnComplete(() =>
        {
            animationValues.XDirection = 0f;
            animationValues.XDirection = 0f;

            animationValues.OnMovement = false;
        });
    }
}
