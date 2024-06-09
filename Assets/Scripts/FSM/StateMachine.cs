using System;
using System.Collections.Generic;

namespace FSM
{
    public class StateMachine<ISpecificState> where ISpecificState : IState
    {
        private Dictionary<Type, ISpecificState> _states = new Dictionary<Type, ISpecificState>();
        private ISpecificState _currentState;

        public void AddState(ISpecificState state)
        {
            Type type = state.GetType();
            if (_states.ContainsKey(type))
                throw new InvalidOperationException($"State by type \"{nameof(type)}\" has already in \"_states\" dictionary");

            bool firstState = false;
            if (_currentState == null) firstState = true;

            _states.Add(type, state);
            
            if (firstState) StartBehaviour(state);
        }

        public void GoTo<INewState>() where INewState : ISpecificState
        {
            Type type = typeof(INewState);

            if (type == _currentState.GetType()) return;

            if (_states.TryGetValue(type, out ISpecificState state))
            {
                _currentState.Exit();
                _currentState = state;
                _currentState.Enter();
            }
            else
            {
                throw new InvalidOperationException($"State \"{nameof(type)}\" is not intialized in \"_states\" dictionary");
            }
        }

        public void UseActiveState() => _currentState?.Operate();

        private void StartBehaviour(ISpecificState state)
        {
            if (state == null)
                throw new InvalidOperationException($"First state is null");

            _currentState = state;
            _currentState.Enter();
        }
    }
}