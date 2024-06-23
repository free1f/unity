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
        public AttackState attackState;
        public AvoidState avoidState;

        void Start()
        {
            idleState.Machine = this;
            chaseState.Machine = this;
            attackState.Machine = this;
            avoidState.Machine = this;
            SetDefaultState(idleState);
        }

        void Update()
        {
            ExecuteState();
        }
    }
}
