using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.IA.States
{
    public class AttackState : MonoBehaviour, IState
    {
        public StateMachine Machine { get; set; }
        private float timeAttack = 2f;
        private bool isAttacking = false;

        public void Enter()
        {
            if (!isAttacking)
            {
                isAttacking = true;
            }
            Machine.DebugMaterialColor(Color.red);
        }

        public void Execute()
        {
            if (isAttacking)
            {
                timeAttack -= Time.deltaTime;       // Segundos del juego normalizados fps
                if (timeAttack <= 0)
                {
                    Debug.Log("Attack");
                    Machine.ForcePreviousState();
                }
            }
        }

        public void Exit()
        {
            timeAttack = 2f;
            isAttacking = false;
        }
    }
}