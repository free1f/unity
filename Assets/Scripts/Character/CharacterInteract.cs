using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Character.DataTransfer;
using Freelf.Character.Interfaces;
using Freelf.Environment;
using Freelf.Item;
using UnityEngine;

namespace Freelf.Character
{
    public class CharacterInteract : CharacterComponent, IFixedTick, IAttached<InteractData>
    {
        public float rayDistance = 2f;
        public float rayRadius = 0.5f;
        public Vector3 rayOffset = Vector3.zero;
        public Transform body;
        public LayerMask allowedLayers;
        private bool _isInteracting;
        private float _interactingCooldown = 0.5f;
        private InteractData Data;
        
        private readonly List<BaseItem> foundItems = new List<BaseItem>(); // Esto puede cambiar a un objeto más genérico o abstracto
        public override void Init() {}

        public void WaitToInteract()
        {
            if (Data.input.IsHold && _interactingCooldown <= 0)
            {
                _isInteracting = true;
                _interactingCooldown = 0.5f;
            }
            else
            {
                _isInteracting = false;
                _interactingCooldown -= Time.deltaTime;
            }

            if (_isInteracting) TryInteract();
        }

        private void TryInteract()
        {
            var colliders = Physics.OverlapSphere(body.position + rayOffset, rayRadius, allowedLayers);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<BaseItem>(out var item))
                {
                    foundItems.Add(item);
                }
                // Provisional
                if (collider.TryGetComponent<Campfire>(out var campfire))
                {
                    campfire.StartFire();
                }
            }

            if (foundItems.Count == 0) return;
            foundItems.Sort((a, b) => Vector3.Distance(a.transform.position, body.position).CompareTo(Vector3.Distance(b.transform.position, body.position)));

            // toolItems[0].Pickup();
            Data.OnInteract?.Invoke(foundItems[0]);
            foundItems.Clear();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(body.position, rayRadius);
        }

        public void Attached(ref InteractData value)
        {
            Data = value;
        }

        public void FixedTick()
        {
            WaitToInteract();
        }
    }
}