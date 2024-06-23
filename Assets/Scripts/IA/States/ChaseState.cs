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
        public float velocity = 5f;
        public StateMachine Machine { get; set; }
        
        public void Enter()
        {
            Debug.Log("Enter ChaseState");
            agent.speed = velocity;
        }

        public void Execute()
        {
            if (Vector3.Distance(transform.position, target.position) > chaseDistance)
            {
                Debug.Log("Change to IdleState");
                Machine.ForceDefaultState();

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
