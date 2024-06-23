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

        public void Enter()
        {
            Debug.Log("Enter IdleState");
        }

        public void Execute()
        {
            Debug.Log("Execute IdleState");
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