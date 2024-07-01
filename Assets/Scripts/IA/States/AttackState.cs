using System.Collections;
using System.Collections.Generic;
using Freelf.Character.Interfaces;
using UnityEngine;

namespace Freelf.IA.States
{
    public class AttackState : MonoBehaviour, IState
    {
        public StateMachine Machine { get; set; }
        private float timeAttack = 2f;
        private bool isAttacking = false;
        public Vector3 attackPosition;
        public float attackRadius = 1f;
        public LayerMask targetLayers;
        public int damage = 100;

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
                    var colliders = Physics.OverlapSphere(transform.TransformPoint(attackPosition), attackRadius, targetLayers);
                    foreach (var collider in colliders)
                    {
                        Debug.Log($"Attacking {collider.name}");
                        if(collider.TryGetComponent<IDamageable>(out var target)) 
                        {
                            target.TakeDamage(-damage);
                        }
                    }
                    Machine.ForcePreviousState();
                }
            }
        }

        public void Exit()
        {
            timeAttack = 2f;
            isAttacking = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.TransformPoint(attackPosition), attackRadius);
        }
    }
}