using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Freelf.IA.States
{
    public class AvoidState : MonoBehaviour, IState
    {
        public StateMachine Machine { get; set; }
        public IAStats stats;
        public Transform target;
        public NavMeshAgent agent;

        public void Enter()
        {
            
        }

        public void Execute()
        {
            if (stats.healthStat.CurrentValue.Value <= (int)(stats.healthStat.MaxValue * 0.5f))
            {
                var forwardTarget = target.TransformDirection(Vector3.forward);
                var distance = Vector3.Normalize(forwardTarget - transform.position);
                if (Vector3.Dot(forwardTarget, distance) < 0)
                {
                    // Heal itself or w/e
                    // Machine.ForceDefaultState();
                }
                else
                {
                    // Create a new direction to avoid the target
                    var avoidDirection = transform.position + distance;
                    agent.SetDestination(avoidDirection);
                }
            }
            else
            {
                Machine.ForceDefaultState();
            }
        }

        public void Exit()
        {
            
        }
    }
}