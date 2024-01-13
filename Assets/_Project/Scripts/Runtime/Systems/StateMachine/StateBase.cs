using UnityEngine;

namespace CanvasDEV.Runtime.Systems.StateMachine
{
    public abstract class StateBase : MonoBehaviour
    {
        protected StateMachineBase machine;

        public abstract void Setup(StateMachineBase machine);

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnLateUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnExit() { }
    }
}