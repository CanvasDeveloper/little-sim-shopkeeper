using UnityEngine;

namespace CanvasDEV.Runtime.Systems.StateMachine
{
    public abstract class StateBase : MonoBehaviour
    {
        public abstract void Setup(StateMachineBase machine);

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnExit() { }

        public virtual bool CanEnterState()
        {
            return true;
        }
    }
}