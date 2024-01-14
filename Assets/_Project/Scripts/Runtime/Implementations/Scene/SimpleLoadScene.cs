using EasyTransition;
using UnityEngine;

public class SimpleLoadScene : MonoBehaviour
{
    [SerializeField] private int index;
    public TransitionSettings transition;

    private void Start()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        TransitionManager.Instance().Transition(index, transition, 0f);
    }
}