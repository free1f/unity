using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.IA.States
{
    public interface IState
    {
        StateMachine Machine { get; set; }
        void Enter();
        void Execute();
        void Exit();
    }   
}