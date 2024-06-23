using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Freelf.IA.States
{
    public class ChaseState : MonoBehaviour, IState
    {
        public Transform target;
        public NavMeshAgent agent;
        public float chaseDistance = 5f;
        public float attackDistance = 1f;
        public float velocity = 5f;
        public StateMachine Machine { get; set; }
        public AttackState attackState;
        public AvoidState avoidState;
        public IAStats stats;
        
        public void Enter()
        {
            Debug.Log("Enter ChaseState");
            agent.speed = velocity;
            Machine.DebugMaterialColor(Color.yellow);
        }

        public void Execute()
        {
            if (stats.healthStat.CurrentValue.Value <= (int)(stats.healthStat.MaxValue * 0.5f))
            {
                Debug.Log("Change to AvoidState");
                Machine.ChangeState(avoidState);
                return;
            }
            if (Vector3.Distance(transform.position, target.position) > chaseDistance)
            {
                Debug.Log("Change to Default IdleState");
                Machine.ForceDefaultState();

            }
            else if (Vector3.Distance(transform.position, target.position) <= attackDistance)
            {
                Debug.Log("Change to AttackState");
                Machine.ChangeState(attackState);
            }
            else
            {
                agent.SetDestination(target.position);
            }
        }

        public void Exit()
        {
            Debug.Log("Exit ChaseState");
        }
    }
}
