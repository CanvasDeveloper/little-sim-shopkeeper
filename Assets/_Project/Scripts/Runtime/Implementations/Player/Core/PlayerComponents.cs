using UnityEngine;

[RequireComponent(typeof(IInputController))]
public class PlayerComponents : MonoBehaviour
{
    public IInputController InputController { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }


    private void Awake()
    {
        InputController = GetComponent<IInputController>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
}