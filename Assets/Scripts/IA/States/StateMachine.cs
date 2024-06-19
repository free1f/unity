using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.IA.States
{
    public class StateMachine : MonoBehaviour
    {
        private IState _currentState;
        private IState _previousState;
        private IState _defaultState;

        public void ChangeState(IState newState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _previousState = _currentState;
            _currentState = newState;
            _currentState.Enter();
        }

        public void ExecuteState()
        {
            if (_currentState != null)
            {
                _currentState.Execute();
            }
        }

        public void SetDefaultState(IState state)
        {
            _defaultState = state;
            ChangeState(_defaultState);
        }

        public void ForceDefaultState()
        {
            if (_defaultState != null)
            {
                ChangeState(_defaultState);
            }
        }
    }
}