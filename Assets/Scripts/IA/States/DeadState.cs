using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.IA.States;
using UnityEngine;

namespace Freelf.IA.States
{
    public class DeadState : MonoBehaviour, IState
    {
        public StateMachine Machine { get; set; }
        public IAStats stats;

        public void Enter()
        {
            Machine.DebugMaterialColor(Color.black);
            stats.healthStat.CurrentValue.AddListener(OnHealthChanged);
        }

        private void OnHealthChanged(int value)
        {
            if (value > 0)
            {
                Machine.ForceDefaultState();
            }
        }

        public void Execute()
        {
            return;
        }

        public void Exit()
        {
            stats.healthStat.CurrentValue.RemoveListener(OnHealthChanged);
        }
    }
}