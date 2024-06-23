using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freelf.IA.States;

namespace Freelf.IA 
{ 
    public class ChaseIA : StateMachine
    {
        public IdleState idleState;
        public ChaseState chaseState;

        void Start()
        {
            idleState.Machine = this;
            chaseState.Machine = this;
            SetDefaultState(idleState);
        }

        void Update()
        {
            ExecuteState();
        }
    }
}
