using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NpcComponents : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D { get; private set; }

    [field: Required]
    [field: SerializeField] public AnimationValues AnimationValues { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
}