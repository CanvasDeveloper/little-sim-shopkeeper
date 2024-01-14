using CanvasDEV.Runtime.Core;
using EasyTransition;
using UnityEngine.SceneManagement;
using UnityEngine;
using CanvasDEV.Runtime.Core.GameState;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private TransitionSettings transition;

    private void Start()
    {
        TransitionManager.Instance().onTransitionBegin += ChangeGameState;
    }

    public void LoadNextScene()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;

        TransitionManager.Instance().Transition(next, transition, 0.5f);
    }

    public void FakeTransition()
    {
        TransitionManager.Instance().Transition(transition, 0f);
    }

    public void LoadScene(int sceneToLoad)
    {
        TransitionManager.Instance().Transition(sceneToLoad, transition, 0.5f);
    }

    private void ChangeGameState()
    {
        GameStateHandler.ChangeState(GameState.Load);
    }
}