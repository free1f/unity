using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freelf.IA.States
{
    public class IdleState : MonoBehaviour, IState
    {
        public Transform target;
        public float chaseDistance = 5f;

        public StateMachine Machine { get; set; }
        public ChaseState chaseState;
        public AvoidState avoidState;
        public IAStats stats;

        public void Enter()
        {
            Debug.Log("Enter IdleState");
            Machine.DebugMaterialColor(Color.blue);
        }

        public void Execute()
        {
            Debug.Log("Execute IdleState");
            if (stats.healthStat.CurrentValue.Value <= (int)(stats.healthStat.MaxValue * 0.5f))
            {
                Debug.Log("Change to AvoidState");
                Machine.ChangeState(avoidState);
                return;
            }
            if (Vector3.Distance(transform.position, target.position) < chaseDistance)
            {
                Debug.Log("Change to ChaseState");
                Machine.ChangeState(chaseState);
            }
        }

        public void Exit()
        {
            Debug.Log("Exit IdleState");
        }
    }   
}