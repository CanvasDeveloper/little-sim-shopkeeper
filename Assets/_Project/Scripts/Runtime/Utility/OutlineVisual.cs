using System;
using TNRD;
using UnityEngine;

public interface IOutline
{
    public event Action<Color> OnAddedOutline;
    public event Action OnRemovedOutline;
}

public class OutlineVisual : MonoBehaviour
{
    [SerializeField] private SerializableInterface<IOutline> _outlinable;

    [SerializeField] private SpriteRenderer[] outlines;

    private void Start()
    {
        _outlinable.Value.OnAddedOutline += ApplyFoundVisual;
        _outlinable.Value.OnRemovedOutline += RemoveVisual;

        foreach (var o in outlines)
        {
            o.material.SetFloat("_OutlineAlpha", 0);
        }
    }

    private void OnDestroy()
    {
        _outlinable.Value.OnAddedOutline -= ApplyFoundVisual;
        _outlinable.Value.OnRemovedOutline -= RemoveVisual;
    }

    private void RemoveVisual()
    {
        foreach (var o in outlines)
        {
            o.material.SetFloat("_OutlineAlpha", 0);
        }
    }

    private void ApplyFoundVisual(Color color)
    {
        foreach (var o in outlines)
        {
            o.material.SetFloat("_OutlineAlpha", 1);
            o.material.SetColor("_OutlineColor", color);
        }
    }
}