using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freelf.IA.States;
using System;

namespace Freelf.IA 
{ 
    public class ChaseIA : StateMachine
    {
        public IdleState idleState;
        public ChaseState chaseState;
        public AttackState attackState;
        public AvoidState avoidState;
        public DeadState deadState;
        public IAStats stats;

        void Start()
        {
            idleState.Machine = this;
            chaseState.Machine = this;
            attackState.Machine = this;
            avoidState.Machine = this;
            deadState.Machine = this;
            SetDefaultState(idleState);
        }

        void Update()
        {
            ExecuteState();
            if (Input.GetKeyDown(KeyCode.L))
            {
                stats.healthStat.CurrentValue.Value -= 100;
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                stats.healthStat.CurrentValue.Value += 100;
            }
        }

        public void OnEnable()
        {
            stats.healthStat.CurrentValue.AddListener(OnHealthChanged);
        }

        private void OnHealthChanged(int value)
        {
            Debug.Log("OnHealthChanged: " + value);
            if (value <= 0)
            {
                ChangeState(deadState);
            }
        }

        public void OnDisable()
        {
            stats.healthStat.CurrentValue.RemoveListener(OnHealthChanged);
        }
    }
}
