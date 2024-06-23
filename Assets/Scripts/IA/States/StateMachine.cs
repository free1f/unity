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
        private MeshRenderer _meshRenderer;
        private Material _material;

        public void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _material = _meshRenderer.material;
        }

        public void DebugMaterialColor(Color color)
        {
            _material.color = color;
        }

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

        public void ForcePreviousState()
        {
            if (_previousState != null)
            {
                ChangeState(_previousState);
            }
        }
    }
}