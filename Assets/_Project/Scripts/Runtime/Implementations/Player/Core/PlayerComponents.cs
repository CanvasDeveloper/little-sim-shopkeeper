using CanvasDEV.Runtime.Implementations.Player.Input;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerComponents : MonoBehaviour
{
    public PlayerInputController Input { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }


    private void Awake()
    {
        Input = GetComponent<PlayerInputController>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
}