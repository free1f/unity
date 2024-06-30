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
        [Range(0, 1)]
        public float maxPercentageToAvoid = 0.5f;

        public void Enter()
        {
            
        }

        public void Execute()
        {
            if (stats.healthStat.CurrentValue.Value <= (int)(stats.healthStat.MaxValue * maxPercentageToAvoid))
            {
                Debug.Log("Avoiding");
                // var forwardTarget = target.TransformDirection(Vector3.forward);
                var forwardTarget = target.position - transform.position;
                var avoidDirection = transform.position + forwardTarget.normalized * -3f;
                agent.SetDestination(avoidDirection);
                if (Vector3.Dot(forwardTarget, transform.forward) < 0)
                {
                    // Heal itself or w/e
                    // Machine.ForceDefaultState();
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