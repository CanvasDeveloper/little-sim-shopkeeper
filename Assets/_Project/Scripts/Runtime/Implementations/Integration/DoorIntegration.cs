using CanvasDEV.Runtime.Systems.Interaction;
using UnityEngine;

public class DoorIntegration : Interactable
{
    [SerializeField] private int sceneToLoad;

    public override bool InteractBehaviour()
    {
        SceneLoader.Instance.LoadScene(sceneToLoad);

        return true;
    }
}
