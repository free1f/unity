using Freelf.Item.Interfaces;
using UnityEngine;

namespace Freelf.Item
{
    public class ToolItem : BaseItem, IUse, IPickup
    {
        // private IInteract _interactable;
        public bool IsPickedUp{ get; private set; }
        public bool IsInUse { get; private set; }
        public ToolDataItem Data;
        public float rayRadius = 1f;

        public void Pickup()
        {
            IsPickedUp = true;
            gameObject.SetActive(false);
            Debug.Log("Picked up");
        }

        public void Use()
        {
            var colliders = Physics.OverlapSphere(transform.position, rayRadius);
            foreach (var collider in colliders)
            {
                foreach (var tag in Data.allowedTags)
                {
                    if (collider.CompareTag(tag))
                    {
                        Debug.Log("Using " + Data.itemName);
                        return;
                    }
                }
            }
        }

        public override void Info()
        {
            Debug.Log("Tool: " + Data.itemName);
        }

        // protected override void OnTriggerEnter(Collider other)
        // {
        //     if (other.TryGetComponent(out IInteract interactable))
        //     {
        //         _interactable = interactable;
        //     }
        // }

        // protected override void OnTriggerStay(Collider other)
        // {
        //     if (_interactable == null) return;
        //     if (Input.GetKey(KeyCode.E) && !_isPickedUp)
        //     {
        //         _isPickedUp = true;
        //         _interactable.Interact(gameObject);
        //     }
        // }

        // protected override void OnTriggerExit(Collider other)
        // {
        //     if (other.TryGetComponent(out IInteract interactable))
        //     {
        //         _interactable = null;
        //     }
        // }
    }
}