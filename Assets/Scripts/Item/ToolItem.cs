using System;
using Freelf.Audio;
using Freelf.Elements;
using Freelf.Elements.Interfaces;
using Freelf.Item.Interfaces;
using UnityEngine;

namespace Freelf.Item
{
    public class ToolItem : BaseItem, IUse, IPickup
    {
        public bool IsPickedUp{ get; private set; }
        public bool IsInUse { get; private set; }
        [field: SerializeField]
        public override DataItem Data { get; protected set;}

        public int StaminaCost { get; private set; }

        public float rayRadius = 1f;
        public Vector3 rayOffset = Vector3.zero;
        
        private void Start()
        {
            var toolData = (ToolDataItem) Data;
            StaminaCost = toolData.staminaCost;
        }

        public void Pickup()
        {
            IsPickedUp = true;
            gameObject.SetActive(false);
            Debug.Log($"{Data.itemName} -> Picked up");
        }

        public void Use(Action onSuccess = null)
        {
            Debug.Log($"{Data.itemName} -> Used");
            var toolData = (ToolDataItem) Data;
            
            var colliders = Physics.OverlapSphere(transform.TransformPoint(rayOffset), rayRadius, toolData.allowedLayers);
            
            if(toolData == null) return;
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out BaseElement element))
                {
                    if (element.Data.level <= toolData.level) {
                        var matchedType = Array.Find(toolData.compatibleElements, x => x == element.Data.elementType);
                        if (matchedType == null) continue;
                        (element as IGather)?.Gather();
                        AudioHandler.instance.Play(toolData.actionData.sound, SoundCategory.SFX);
                        onSuccess?.Invoke();
                    }
                }
                Debug.Log($"Using {Data.itemName} on {collider.name}");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.TransformPoint(rayOffset), rayRadius);
        }

        public override void Info()
        {
            Debug.Log("Tool: " + Data.itemName);
        }
    }
}