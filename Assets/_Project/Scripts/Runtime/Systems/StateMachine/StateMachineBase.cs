using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CanvasDEV.Runtime.Systems.StateMachine
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        [SerializeField] protected List<StateBase> states;

        protected StateBase currentState;

        protected virtual void Awake()
        {
            states.ForEach(state => state.Setup(this));
        }

        protected virtual void Start()
        {
            ChangeState(states[0]);
        }

        protected virtual void Update()
        {
            if (currentState == null)
            {
                return;
            }

            currentState.OnUpdate();
        }

        protected virtual void LateUpdate()
        {
            if (currentState == null)
            {
                return;
            }

            currentState.OnLateUpdate();
        }

        protected virtual void FixedUpdate()
        {
            if (currentState == null)
            {
                return;
            }

            currentState.OnFixedUpdate();
        }

        private void ChangeState(StateBase toState)
        {
            if (currentState == toState)
            {
                return;
            }

            if(!toState.CanEnterState())
            {
                return;
            }

            if (currentState != null)
            {
                currentState.OnExit();
            }

            currentState = toState;

            currentState.OnEnter();
        }

        public void ChangeState<T>() where T : StateBase
        {
            StateBase foundedState = states.FirstOrDefault(state => state.GetType() == typeof(T));

            if (foundedState == null)
            {
                Debug.LogWarning($"State of type {typeof(T)} not founded! Called by: {gameObject.name}");
                return;
            }

            if (currentState.GetType() == foundedState.GetType())
            {
                return;
            }

            if(!foundedState.CanEnterState())
            {
                Debug.LogWarning($"Cannot enter on this state{foundedState.GetType()}! Called by: {gameObject.name}");
                return;
            }

            if (currentState != null)
            {
                currentState.OnExit();
            }

            currentState = foundedState;

            currentState.OnEnter();
        }

        [Button]
        public void GetStates()
        {
            states.Clear();

            states.AddRange(GetComponentsInChildren<StateBase>());
        }
    }
}