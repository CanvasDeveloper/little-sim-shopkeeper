using TNRD;
using UnityEngine;

public class DirectionalAnimation : MonoBehaviour
{
    private readonly int LookDirecionXParam = Animator.StringToHash("LookDirectionX");
    private readonly int LookDirecionYParam = Animator.StringToHash("LookDirectionY");
    private readonly int OnMovementParam = Animator.StringToHash("OnMovement");
    private readonly int IsRunningParam = Animator.StringToHash("IsRunning");

    [SerializeField] private SerializableInterface<IDirectionalAnimationSource> source;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        _animator.SetFloat(LookDirecionXParam, source.Value.XDirection);
        _animator.SetFloat(LookDirecionYParam, source.Value.YDirection);
        _animator.SetBool(OnMovementParam, source.Value.OnMovement);
        _animator.SetBool(IsRunningParam, source.Value.IsRunning);
    }
}
