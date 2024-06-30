using System;
using System.Collections;
using System.Collections.Generic;
using Freelf.Audio;
using Freelf.Character.Interfaces;
using Freelf.Item.Interfaces;
using UnityEngine;

namespace Freelf.Item 
{
    public class WeaponItem : BaseItem, IPickup, IUse
    {
        [field: SerializeField]
        public override DataItem Data { get; protected set; }

        public bool IsPickedUp { get; private set; }

        public bool IsInUse { get; private set; }

        public int StaminaCost { get; private set; }
        public float rayRadius = 1f;
        public Vector3 rayOffset = Vector3.zero;

        public override void Info()
        {
            Debug.Log("Tool: " + Data.itemName);
        }

        public void Pickup()
        {
            IsPickedUp = true;
            gameObject.SetActive(false);
        }

        public void Use(Action onSuccess = null)
        {
            var weaponData = (WeaponDataItem) Data;
            
            if (weaponData == null) return;
            var colliders = Physics.OverlapSphere(transform.TransformPoint(rayOffset), rayRadius, weaponData.allowedLayers);
            foreach (var collider in colliders)
            {
                if(collider.TryGetComponent<IDamageable>(out var target))
                {
                    target.TakeDamage(weaponData.damage);
                    AudioHandler.instance.Play(weaponData.actionData.sound, SoundCategory.SFX);
                    onSuccess?.Invoke();
                }
            }
        }

        void Start()
        {
            var weaponData = (WeaponDataItem) Data;
            StaminaCost = weaponData.staminaCost;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.TransformPoint(rayOffset), rayRadius);
        }
    }
}
