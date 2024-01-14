using UnityEngine;

public class AnimationValues : MonoBehaviour, IDirectionalAnimationSource
{
    public float XDirection { get; set; } = 0f;
    public float YDirection { get; set; } = 0f;
    public bool OnMovement { get; set; } = false;
    public bool IsRunning { get; set; } = false;
}