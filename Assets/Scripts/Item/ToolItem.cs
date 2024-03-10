using System;
using Freelf.Elements;
using Freelf.Elements.Interfaces;
using Freelf.Item.Interfaces;
using UnityEngine;

public class ToolItem : BaseItem, IUse, IPickup
{
    // private IInteract _interactable;
    public bool IsPickedUp{ get; private set; }
    public bool IsInUse { get; private set; }
    [field: SerializeField]
    public override DataItem Data { get; protected set;}
    public float rayRadius = 1f;
    public Vector3 rayOffset = Vector3.zero;

    public void Pickup()
    {
        IsPickedUp = true;
        gameObject.SetActive(false);
        Debug.Log($"{Data.itemName} -> Picked up");
    }

    public void Use()
    {
        Debug.Log($"{Data.itemName} -> Used");
        var toolData = (ToolDataItem) Data;
        var colliders = Physics.OverlapSphere(transform.position + rayOffset, rayRadius, toolData.allowedLayers);
        if(toolData == null) return;
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out BaseElement element))
            {
                if (element.Data.level <= toolData.level) {
                    var matchedType = Array.Find(toolData.compatibleElements, x => x == element.Data.elementType);
                    if (matchedType == null) continue;
                    (element as IGather)?.Gather();
                }
            }
            Debug.Log($"Using {Data.itemName} on {collider.name}");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + rayOffset, rayRadius);
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