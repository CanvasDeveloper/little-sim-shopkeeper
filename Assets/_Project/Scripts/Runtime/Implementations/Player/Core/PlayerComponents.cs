using CanvasDEV.Runtime.Implementations.Player.Input;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerComponents : MonoBehaviour
{
    public PlayerInputController Input { get; private set; }
    public PlayerInventory Inventory { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }

    [field:Required]
    [field:SerializeField] public AnimationValues AnimationValues { get; private set; }

    public bool IsPlayerBlocked;

    private void Awake()
    {
        Input = GetComponent<PlayerInputController>();
        Inventory = GetComponent<PlayerInventory>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
}